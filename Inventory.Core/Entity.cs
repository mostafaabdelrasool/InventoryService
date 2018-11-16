using System;

namespace Dk.AirSupport.ManagementConsole.Core
{
    public class Entity : IEntity
    {
        public Entity()
        {
            this.Id = new Guid();
            this.Created = DateTime.Now;
            this.Updated = new DateTime(0, 0, 0);
            this.Deleted = new DateTime(0, 0, 0);
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }        
        public DateTime Deleted { get; set; }
        public bool SoftDelete { get; set; }
    }
}
