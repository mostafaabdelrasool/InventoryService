using System;

namespace Inventory.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }       
        DateTime CreateDate { get; set; }        
        DateTime ModifyDate { get; set; }                
        DateTime DeleteDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
