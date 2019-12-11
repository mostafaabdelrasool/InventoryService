using Domain.Service;
using System;

namespace Product.Domain
{
    public class Entity : IEntity<int>
    {
        public Entity()
        {
            this.CreateDate = DateTime.Now;
            this.ModifyDate = new DateTime();
            this.DeleteDate = new DateTime();
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }        
        public DateTime DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
