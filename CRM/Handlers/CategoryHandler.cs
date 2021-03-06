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
using CRM.Enums;
using CRM.Helpers;

namespace CRM.Handlers
{
    public class CategoryHandler : Handler
    {
        private string SyncKey => Name + ".Sync";

        public override string Name => nameof(Category);

        public override void QueueBind(IModel channel, string queue, string exchange)
        {
            channel.QueueBind(queue, exchange, $"{Name}.*", null);
        }
        public override async Task Handle(DataContext context, string routingKey, string content)
        {
            if (routingKey == SyncKey)
            {
                await Sync(context, content);
            }
        }

        private async Task Sync(DataContext context, string json)
        {
            List<EventMessage<Category>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Category>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Category>> CategoryEventMessages = await ListEventMessage<Category>(context, SyncKey, RowIds);
            List<Category> Categorys = CategoryEventMessages.Select(x => x.Content).ToList();
            List<CategoryDAO> CategoryInDB = await context.Category.ToListAsync();
            try
            {
                List<CategoryDAO> CategoryDAOs = new List<CategoryDAO>();
                List<ImageDAO> ImageDAOs = new List<ImageDAO>();
                foreach (Category Category in Categorys)
                {
                    CategoryDAO CategoryDAO = CategoryInDB.Where(x => x.Id == Category.Id).FirstOrDefault();
                    if (CategoryDAO == null)
                    {
                        CategoryDAO = new CategoryDAO();
                    }
                    CategoryDAO.Id = Category.Id;
                    CategoryDAO.Code = Category.Code;
                    CategoryDAO.CreatedAt = Category.CreatedAt;
                    CategoryDAO.UpdatedAt = Category.UpdatedAt;
                    CategoryDAO.DeletedAt = Category.DeletedAt;
                    CategoryDAO.Id = Category.Id;
                    CategoryDAO.Name = Category.Name;
                    CategoryDAO.RowId = Category.RowId;
                    CategoryDAO.StatusId = Category.StatusId;
                    CategoryDAO.Level = Category.Level;
                    CategoryDAO.ParentId = Category.ParentId;
                    CategoryDAO.ImageId = Category.ImageId;
                    CategoryDAO.Path = Category.Path;
                    CategoryDAO.Used = Category.Used;
                    CategoryDAOs.Add(CategoryDAO);

                    if(Category.Image != null)
                    {
                        ImageDAOs.Add(new ImageDAO
                        {
                            Id = Category.Image.Id,
                            Url = Category.Image.Url,
                            ThumbnailUrl = Category.Image.ThumbnailUrl,
                            RowId = Category.Image.RowId,
                            Name = Category.Image.Name,
                            CreatedAt = Category.Image.CreatedAt,
                            UpdatedAt = Category.Image.UpdatedAt,
                            DeletedAt = Category.Image.DeletedAt,
                        });
                    }
                }
                await context.BulkMergeAsync(ImageDAOs);
                await context.BulkMergeAsync(CategoryDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(CategoryHandler));
            }

        }
    }
}
