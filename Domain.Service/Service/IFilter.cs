using System;
namespace Domain.Service
{
    public interface IFilter<in T>
    {
        void Execute(T msg);
    }
}
