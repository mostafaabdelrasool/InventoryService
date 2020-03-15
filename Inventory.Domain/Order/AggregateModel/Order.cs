using System;
using System.Collections.Generic;
using Domain.Service;
using Inventory.Domain.Models;

namespace Inventory.Domain.Order.AggregateModel
{
    public class Order:Entity,IAggregateRoot
    {
        private readonly List<OrderDetails> _orderDetails;
        public Guid CustomerId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public Guid? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string Phone { get; set; }
        public decimal? Total { get; set; }
        public decimal? OverallTotal { get; set; }
        public string OrderNumber { get; set; }
        public ShipStatus ShipStatus { get; set; }
        public Customers Customer { get; set; }
        public Employees Employee { get; set; }
        public Shippers ShipViaNavigation { get; set; }
        public IReadOnlyCollection<OrderDetails> OrderItems => _orderDetails;
        protected Order()
        {
            _orderDetails = new List<OrderDetails>();
        }
    }
}
