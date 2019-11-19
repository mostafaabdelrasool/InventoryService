using System;
namespace Inventory.Domain.Service
{
    public interface IFilter<in T>
    {
        void Execute(T msg);
    }
}
