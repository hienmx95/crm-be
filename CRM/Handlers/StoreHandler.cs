using CRM.Entities;
using CRM.Enums;
using CRM.Helpers;
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
    public class StoreHandler : Handler
    {
        private string SyncKey => $"{Name}.Sync";
        public override string Name => nameof(Store);

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
            List<EventMessage<Store>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<Store>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<Store>> StoreEventMessages = await ListEventMessage<Store>(context, SyncKey, RowIds);
            List<Store> Stores = StoreEventMessages.Select(x => x.Content).ToList();
            var StoreIds = Stores.Select(x => x.Id).ToList();
            List<StoreDAO> StoreInDB = await context.Store.Where(x => StoreIds.Contains(x.Id)).ToListAsync();
            foreach (var RowId in RowIds)
            {
                EventMessage<Store> EventMessage = EventMessageReceived.Where(e => e.RowId == RowId).OrderByDescending(e => e.Time).FirstOrDefault();
                if (EventMessage != null)
                    Stores.Add(EventMessage.Content);
            }
            try
            {
                List<StoreDAO> StoreDAOs = new List<StoreDAO>();
                List<ImageDAO> ImageDAOs = new List<ImageDAO>();
                List<StoreImageMappingDAO> StoreImageMappingDAOs = new List<StoreImageMappingDAO>();
                foreach (var Store in Stores)
                {
                    StoreDAO StoreDAO = StoreInDB.Where(x => x.Id == Store.Id).FirstOrDefault();
                    if (StoreDAO == null)
                    {
                        StoreDAO = new StoreDAO();
                    }
                    StoreDAO.Id = Store.Id;
                    StoreDAO.Code = Store.Code;
                    StoreDAO.Name = Store.Name;
                    StoreDAO.Address = Store.Address;
                    StoreDAO.AppUserId = Store.AppUserId;
                    StoreDAO.CodeDraft = Store.CodeDraft;
                    StoreDAO.CreatedAt = Store.CreatedAt;
                    StoreDAO.UpdatedAt = Store.UpdatedAt;
                    StoreDAO.DeletedAt = Store.DeletedAt;
                    StoreDAO.DeliveryAddress = Store.DeliveryAddress;
                    StoreDAO.DeliveryLatitude = Store.DeliveryLatitude;
                    StoreDAO.DeliveryLongitude = Store.DeliveryLongitude;
                    StoreDAO.ProvinceId = Store.ProvinceId;
                    StoreDAO.DistrictId = Store.DistrictId;
                    StoreDAO.WardId = Store.WardId;
                    StoreDAO.Latitude = Store.Latitude;
                    StoreDAO.Longitude = Store.Longitude;
                    StoreDAO.LegalEntity = Store.LegalEntity;
                    StoreDAO.OrganizationId = Store.OrganizationId;
                    StoreDAO.OwnerEmail = Store.OwnerEmail;
                    StoreDAO.OwnerName = Store.OwnerName;
                    StoreDAO.OwnerPhone = Store.OwnerPhone;
                    StoreDAO.ParentStoreId = Store.ParentStoreId;
                    StoreDAO.RowId = Store.RowId;
                    StoreDAO.StatusId = Store.StatusId;
                    StoreDAO.StoreGroupingId = Store.StoreGroupingId;
                    StoreDAO.StoreStatusId = Store.StoreStatusId;
                    StoreDAO.StoreTypeId = Store.StoreTypeId;
                    StoreDAO.TaxCode = Store.TaxCode;
                    StoreDAO.Telephone = Store.Telephone;
                    StoreDAO.UnsignAddress = Store.UnsignAddress;
                    StoreDAO.UnsignName = Store.UnsignName;
                    StoreDAO.Used = Store.Used;
                    StoreDAOs.Add(StoreDAO);

                    foreach (var StoreImageMapping in Store.StoreImageMappings)
                    {
                        StoreImageMappingDAO StoreImageMappingDAO = new StoreImageMappingDAO
                        {
                            StoreId = StoreImageMapping.StoreId,
                            ImageId = StoreImageMapping.ImageId,
                        };
                        StoreImageMappingDAOs.Add(StoreImageMappingDAO);
                        ImageDAOs.Add(new ImageDAO
                        {
                            Id = StoreImageMapping.Image.Id,
                            Url = StoreImageMapping.Image.Url,
                            ThumbnailUrl = StoreImageMapping.Image.ThumbnailUrl,
                            RowId = StoreImageMapping.Image.RowId,
                            Name = StoreImageMapping.Image.Name,
                            CreatedAt = StoreImageMapping.Image.CreatedAt,
                            UpdatedAt = StoreImageMapping.Image.UpdatedAt,
                            DeletedAt = StoreImageMapping.Image.DeletedAt,
                        });
                    }
                }

                await context.StoreImageMapping
                .Where(x => StoreIds.Contains(x.StoreId))
                .DeleteFromQueryAsync();
                await context.BulkMergeAsync(ImageDAOs);
                await context.BulkMergeAsync(StoreDAOs);
                await context.BulkMergeAsync(StoreImageMappingDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(StoreHandler));
            }
        }
    }
}
