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

namespace CRM.Handlers
{
    public class SupplierHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(Supplier);

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
            List<EventMessage<Supplier>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Supplier>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Supplier>> SupplierEventMessages = await ListEventMessage<Supplier>(context, SyncKey, RowIds);
            List<Supplier> Suppliers = SupplierEventMessages.Select(x => x.Content).ToList();
            try
            {
                List<SupplierDAO> SupplierDAOs = Suppliers
                    .Select(x => new SupplierDAO
                    {
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        DeletedAt = x.DeletedAt,
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        TaxCode = x.TaxCode,
                        Phone = x.Phone,
                        Email = x.Email,
                        Address = x.Address,
                        NationId = x.NationId,
                        ProvinceId = x.ProvinceId,
                        DistrictId = x.DistrictId,
                        WardId = x.WardId,
                        OwnerName = x.OwnerName,
                        PersonInChargeId = x.PersonInChargeId,
                        StatusId = x.StatusId,
                        Description = x.Description,
                        Used = x.Used,
                        RowId = x.RowId,
                    }).ToList();
                await context.BulkMergeAsync(SupplierDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(SupplierHandler));
            }
        }
    }
}
