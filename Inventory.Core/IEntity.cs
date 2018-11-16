using System;

namespace Dk.AirSupport.ManagementConsole.Core
{
    public interface IEntity
    {
        Guid Id { get; set; }       
        DateTime Created { get; set; }        
        DateTime Updated { get; set; }                
        DateTime Deleted { get; set; }
        bool SoftDelete { get; set; }
    }
}
