using System;

namespace Inventory.Domain
{
    public class Entity : IEntity
    {
        public Entity()
        {
            this.Id = new Guid();
            this.CreateDate = DateTime.Now;
            this.ModifyDate = new DateTime();
            this.DeleteDate = new DateTime();
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }        
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
