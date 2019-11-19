using System;
using System.Collections.Generic;

namespace Inventory.Domain.Service
{
    public class PipeLine<T>
    {
        private readonly List<IFilter<T>> _filters = new List<IFilter<T>>();
        public PipeLine<T> Register(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public void Execute(T input)
        {
            _filters.ForEach(f => f.Execute(input));
        }
    }
}
