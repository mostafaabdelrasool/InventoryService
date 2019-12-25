using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Order.model
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
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
        public CustomerDTO Customer { get; set; }
        public Employees Employee { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
    public class OrderDetailDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total { get; set; }
        public ProductDTO Product { get; set; }
    }
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get;  set; }
        public string ProductCode { get;  set; }
        public string ProductSize { get; set; }
    }
    public class CustomerDTO
    {
        public string Address { get; set; }
        public string companyName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public Guid Id { get; set; }
    }
}
