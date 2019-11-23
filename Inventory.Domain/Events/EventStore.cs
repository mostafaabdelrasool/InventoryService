using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Domain.Events
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
        int GetExpectedVersion(IEnumerable<Event> events);
    }

    public class EventStore : IEventStore
    {

        private readonly Dictionary<Guid, List<Event>> _current = new Dictionary<Guid, List<Event>>();

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            List<Event> eventDescriptors;

            // try to get event descriptors list for given aggregate id
            // otherwise -> create empty dictionary
            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<Event>();
                _current.Add(aggregateId, eventDescriptors);
            }
            // check whether latest event version matches current aggregate version
            // otherwise -> throw exception
            else if (eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }
            var i = expectedVersion;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.SetVersion(i);

                // push event to the event descriptors list for current aggregate
                eventDescriptors.Add(new Event(aggregateId, @event.EventType, i,@event.Data));

                // publish current event to the bus for further processing by subscribers
                //_publisher.Publish(@event);
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<Event> eventDescriptors;

            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }

            return eventDescriptors.ToList();
        }

        public int GetExpectedVersion(IEnumerable<Event> events)
        {
            return events.Max(x => x.Version);
        }
    }

    public class AggregateNotFoundException : Exception
    {
    }

    public class ConcurrencyException : Exception
    {
    }
}
