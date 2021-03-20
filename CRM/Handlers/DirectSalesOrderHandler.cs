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
using CRM.Enums;

namespace CRM.Handlers
{
    public class DirectSalesOrderHandler : Handler
    {
        private string SyncKey => $"{Name}.Sync";

        public override string Name => nameof(DirectSalesOrder);

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
            List<EventMessage<DirectSalesOrder>> EventMessageReceived = JsonConvert.DeserializeObject<List<EventMessage<DirectSalesOrder>>>(json);
            await SaveEventMessage(context, SyncKey, EventMessageReceived);
            List<Guid> RowIds = EventMessageReceived.Select(a => a.RowId).Distinct().ToList();
            List<EventMessage<DirectSalesOrder>> DirectSalesOrderEventMessages = await ListEventMessage<DirectSalesOrder>(context, SyncKey, RowIds);
            List<DirectSalesOrder> DirectSalesOrders = DirectSalesOrderEventMessages.Select(x => x.Content).Where(x => x.RequestStateId == RequestStateEnum.APPROVED.Id).ToList();

            List<DirectSalesOrderContent> DirectSalesOrderContents = DirectSalesOrders.Where(x => x.DirectSalesOrderContents != null).SelectMany(x => x.DirectSalesOrderContents).ToList();
            List<DirectSalesOrderPromotion> DirectSalesOrderPromotions = DirectSalesOrders.Where(x => x.DirectSalesOrderPromotions != null).SelectMany(x => x.DirectSalesOrderPromotions).ToList();
            try
            {
                List<DirectSalesOrderDAO> DirectSalesOrderDAOs = DirectSalesOrders
                    .Select(x => new DirectSalesOrderDAO
                    {
                        Id = x.Id,
                        Code = x.Code,
                        OrganizationId = x.OrganizationId,
                        BuyerStoreId = x.BuyerStoreId,
                        PhoneNumber = x.PhoneNumber,
                        StoreAddress = x.StoreAddress,
                        DeliveryAddress = x.DeliveryAddress,
                        SaleEmployeeId = x.SaleEmployeeId,
                        CreatorId = x.CreatorId,
                        OrderDate = x.OrderDate,
                        DeliveryDate = x.DeliveryDate,
                        RequestStateId = x.RequestStateId,
                        EditedPriceStatusId = x.EditedPriceStatusId,
                        Note = x.Note,
                        SubTotal = x.SubTotal,
                        GeneralDiscountAmount = x.GeneralDiscountAmount,
                        GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                        TotalTaxAmount = x.TotalTaxAmount,
                        TotalAfterTax = x.TotalAfterTax,
                        PromotionCode = x.PromotionCode,
                        PromotionValue = x.PromotionValue,
                        Total = x.Total,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        DeletedAt = x.DeletedAt,
                        RowId = x.RowId,
                    }).ToList();
                await context.BulkMergeAsync(DirectSalesOrderDAOs);

                List<DirectSalesOrderContentDAO> DirectSalesOrderContentDAOs = DirectSalesOrderContents
                    .Select(x => new DirectSalesOrderContentDAO
                    {
                        Id = x.Id,
                        DirectSalesOrderId = x.DirectSalesOrderId,
                        ItemId = x.ItemId,
                        UnitOfMeasureId = x.UnitOfMeasureId,
                        Quantity = x.Quantity,
                        PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                        RequestedQuantity = x.RequestedQuantity,
                        PrimaryPrice = x.PrimaryPrice,
                        SalePrice = x.SalePrice,
                        EditedPriceStatusId = x.EditedPriceStatusId,
                        DiscountAmount = x.DiscountAmount,
                        DiscountPercentage = x.DiscountPercentage,
                        GeneralDiscountAmount = x.GeneralDiscountAmount,
                        GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                        TaxPercentage = x.TaxPercentage,
                        TaxAmount = x.TaxAmount,
                        Amount = x.Amount,
                        Factor = x.Factor,
                    }).ToList();
                await context.BulkMergeAsync(DirectSalesOrderContentDAOs);

                List<DirectSalesOrderPromotionDAO> DirectSalesOrderPromotionDAOs = DirectSalesOrderPromotions
                    .Select(x => new DirectSalesOrderPromotionDAO
                    {
                        Id = x.Id,
                        DirectSalesOrderId = x.DirectSalesOrderId,
                        ItemId = x.ItemId,
                        UnitOfMeasureId = x.UnitOfMeasureId,
                        Quantity = x.Quantity,
                        PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                        RequestedQuantity = x.RequestedQuantity,
                        Factor = x.Factor,
                        Note = x.Note,
                    }).ToList();
                await context.BulkMergeAsync(DirectSalesOrderPromotionDAOs);
            }
            catch (Exception ex)
            {
                Log(ex, nameof(DirectSalesOrderHandler));
            }
        }
    }
}
