using Dapper;
using Inventory.Application.Config;
using Inventory.Application.Order.model;
using Inventory.Domain.Models;
using Inventory.Domain.Order;
using Inventory.Infrastructrue.Json;
using Inventory.Persistance.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Order.Query
{
    public class QueryOrderEvent : IQueryOrderEvent
    {
        private readonly string _connectionString;

        public QueryOrderEvent(IOptions<ConnectionsConfig> config)
        {
            _connectionString = config.Value.ConnectionString;
        }
        public async Task<OrderDTO> GetLastEvent(Guid OrderId)
        {
            //        using (var connection = new SqlConnection(_connectionString))
            //        {
            //            connection.Open();

            //            var result = await connection.QueryAsync<dynamic>(
            //        @"select Data,version from OrderEvent where version =(
            //         Select Max(version) 
            //            From OrderEvent 
            //              where AggregateId=@OrderId and (EventType=@UpdateEvent or EventType=@CreateEvent))
            //and AggregateId=@OrderId"
            //             , new
            //             {
            //                 OrderId = OrderId,
            //                 UpdateEvent = OrderEventType.OrderUpdated.Name,
            //                 CreateEvent = OrderEventType.OrderCreated.Name
            //             }
            //         );
            //            if (result.AsList().Count > 0)
            //                return mapToOrder(result.AsList()[0]);
            //            connection.Close();
            //            return null;
            //        }
            return null;
        }

        private OrderDTO mapToOrder(dynamic dynamic)
        {
            string data = dynamic.Data;
            return data.ToType<OrderDTO>();
        }
    }
}
