using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Handlers
{
    public class StoreGroupingHandler : Handler
    {
        private string SyncKey => $"{Name}.Sync";
        public override string Name => nameof(StoreGrouping);

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
            List<EventMessage<StoreGrouping>> EventMessageReviced = JsonConvert.DeserializeObject<List<EventMessage<StoreGrouping>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReviced);
            List<Guid> RowIds = EventMessageReviced.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<StoreGrouping>> StoreGroupingEventMessages = await ListEventMessage<StoreGrouping>(context, SyncKey, RowIds);

            List<StoreGrouping> StoreGroupings = new List<StoreGrouping>();
            foreach (var RowId in RowIds)
            {
                EventMessage<StoreGrouping> EventMessage = StoreGroupingEventMessages.Where(e => e.RowId == RowId).OrderByDescending(e => e.Time).FirstOrDefault();
                if (EventMessage != null)
                    StoreGroupings.Add(EventMessage.Content);
            }
            try
            {
                List<StoreGroupingDAO> StoreGroupingDAOs = StoreGroupings.Select(x => new StoreGroupingDAO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    RowId = x.RowId,
                    StatusId = x.StatusId,
                    ParentId = x.ParentId,
                    Path = x.Path,
                    Level = x.Level,
                    Used = x.Used,
                }).ToList();
                await context.BulkMergeAsync(StoreGroupingDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(StoreGroupingHandler));
            }
        }
    }
}
