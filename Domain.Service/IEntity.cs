using System;

namespace Domain.Service
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }       
        DateTime CreateDate { get; set; }        
        DateTime ModifyDate { get; set; }                
        DateTime DeleteDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
