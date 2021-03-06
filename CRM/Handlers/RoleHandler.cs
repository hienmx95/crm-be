using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Handlers
{
    public class RoleHandler : Handler
    {
        private string UsedKey => $"CRM.{Name}.Used";
        public override string Name => nameof(Role);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"CRM.{Name}.*", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == UsedKey)
                await Used(context, content);
        }

        private async Task Used(DataContext context, string json)
        {
            List<EventMessage<Role>> EventMessageReviced = JsonConvert.DeserializeObject<List<EventMessage<Role>>>(json);
            List<long> RoleIds = EventMessageReviced.Select(em => em.Content.Id).ToList();
            await context.Role.Where(a => RoleIds.Contains(a.Id)).UpdateFromQueryAsync(a => new RoleDAO { Used = true });
        }
    }
}
