using CRM.Common;
using CRM.Entities;
using CRM.Models;
using CRM.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.Enums;

namespace CRM.Handlers
{
    public class ProductTypeHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(ProductType);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"{Name}.*", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == SyncKey)
                await Sync(context, content);
        }

        private async Task Sync(DataContext context, string json)
        {
            List<EventMessage<ProductType>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<ProductType>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<ProductType>> ProductTypeEventMessages = await ListEventMessage<ProductType>(context, SyncKey, RowIds);
            List<ProductType> ProductTypes = ProductTypeEventMessages.Select(x => x.Content).ToList();
            List<ProductTypeDAO> ProductTypeInDB = await context.ProductType.ToListAsync();
            try
            {
                List<ProductTypeDAO> ProductTypeDAOs = new List<ProductTypeDAO>();
                foreach (var ProductType in ProductTypes)
                {
                    ProductTypeDAO ProductTypeDAO = ProductTypeInDB.Where(x => x.Id == ProductType.Id).FirstOrDefault();
                    if (ProductTypeDAO == null)
                    {
                        ProductTypeDAO = new ProductTypeDAO();
                    }
                    ProductTypeDAO.Id = ProductType.Id;
                    ProductTypeDAO.CreatedAt = ProductType.CreatedAt;
                    ProductTypeDAO.UpdatedAt = ProductType.UpdatedAt;
                    ProductTypeDAO.DeletedAt = ProductType.DeletedAt;
                    ProductTypeDAO.Id = ProductType.Id;
                    ProductTypeDAO.Code = ProductType.Code;
                    ProductTypeDAO.Name = ProductType.Name;
                    ProductTypeDAO.StatusId = ProductType.StatusId;
                    ProductTypeDAO.Description = ProductType.Description;
                    ProductTypeDAO.Used = ProductType.Used;
                    ProductTypeDAO.RowId = ProductType.RowId;
                    ProductTypeDAOs.Add(ProductTypeDAO);
                }
                await context.BulkMergeAsync(ProductTypeDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(ProductTypeHandler));
            }
        }
    }
}
