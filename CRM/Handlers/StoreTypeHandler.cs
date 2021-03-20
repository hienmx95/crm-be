using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Helpers;
using CRM.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Handlers
{
    public class StoreTypeHandler : Handler
    {
        private string SyncKey => $"{Name}.Sync" ;
        public override string Name => nameof(StoreType);

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
            List<EventMessage<StoreType>> EventMessageReviced = JsonConvert.DeserializeObject<List<EventMessage<StoreType>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReviced);
            List<Guid> RowIds = EventMessageReviced.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<StoreType>> StoreTypeEventMessages = await ListEventMessage<StoreType>(context, SyncKey, RowIds);

            List<StoreType> StoreTypes = new List<StoreType>();
            foreach (var RowId in RowIds)
            {
                EventMessage<StoreType> EventMessage = StoreTypeEventMessages.Where(e => e.RowId == RowId).OrderByDescending(e => e.Time).FirstOrDefault();
                if (EventMessage != null)
                    StoreTypes.Add(EventMessage.Content);
            }
            try
            {
                List<StoreTypeDAO> StoreTypeDAOs = StoreTypes.Select(x => new StoreTypeDAO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                    ColorId = x.ColorId,
                    Used = x.Used,
                }).ToList();
                await context.BulkMergeAsync(StoreTypeDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(StoreTypeHandler));
            }
        }
    }
}
