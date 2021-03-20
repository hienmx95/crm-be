using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IOrderQuoteContentRepository
    {
        Task<int> Count(OrderQuoteContentFilter OrderQuoteContentFilter);
        Task<List<OrderQuoteContent>> List(OrderQuoteContentFilter OrderQuoteContentFilter);
        Task<List<OrderQuoteContent>> List(List<long> Ids);
        Task<OrderQuoteContent> Get(long Id);
        Task<bool> Create(OrderQuoteContent OrderQuoteContent);
        Task<bool> Update(OrderQuoteContent OrderQuoteContent);
        Task<bool> Delete(OrderQuoteContent OrderQuoteContent);
        Task<bool> BulkMerge(List<OrderQuoteContent> OrderQuoteContents);
        Task<bool> BulkDelete(List<OrderQuoteContent> OrderQuoteContents);
    }
    public class OrderQuoteContentRepository : IOrderQuoteContentRepository
    {
        private DataContext DataContext;
        public OrderQuoteContentRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OrderQuoteContentDAO> DynamicFilter(IQueryable<OrderQuoteContentDAO> query, OrderQuoteContentFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.OrderQuoteId != null && filter.OrderQuoteId.HasValue)
                query = query.Where(q => q.OrderQuoteId, filter.OrderQuoteId);
            if (filter.ItemId != null && filter.ItemId.HasValue)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.UnitOfMeasureId != null && filter.UnitOfMeasureId.HasValue)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.Quantity != null && filter.Quantity.HasValue)
                query = query.Where(q => q.Quantity, filter.Quantity);
            if (filter.RequestedQuantity != null && filter.RequestedQuantity.HasValue)
                query = query.Where(q => q.RequestedQuantity, filter.RequestedQuantity);
            if (filter.PrimaryUnitOfMeasureId != null && filter.PrimaryUnitOfMeasureId.HasValue)
                query = query.Where(q => q.PrimaryUnitOfMeasureId, filter.PrimaryUnitOfMeasureId);
            if (filter.PrimaryPrice != null && filter.PrimaryPrice.HasValue)
                query = query.Where(q => q.PrimaryPrice, filter.PrimaryPrice);
            if (filter.SalePrice != null && filter.SalePrice.HasValue)
                query = query.Where(q => q.SalePrice, filter.SalePrice);
            if (filter.DiscountPercentage != null && filter.DiscountPercentage.HasValue)
                query = query.Where(q => q.DiscountPercentage.HasValue).Where(q => q.DiscountPercentage, filter.DiscountPercentage);
            if (filter.DiscountAmount != null && filter.DiscountAmount.HasValue)
                query = query.Where(q => q.DiscountAmount.HasValue).Where(q => q.DiscountAmount, filter.DiscountAmount);
            if (filter.GeneralDiscountPercentage != null && filter.GeneralDiscountPercentage.HasValue)
                query = query.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, filter.GeneralDiscountPercentage);
            if (filter.GeneralDiscountAmount != null && filter.GeneralDiscountAmount.HasValue)
                query = query.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, filter.GeneralDiscountAmount);
            if (filter.TaxPercentage != null && filter.TaxPercentage.HasValue)
                query = query.Where(q => q.TaxPercentage.HasValue).Where(q => q.TaxPercentage, filter.TaxPercentage);
            if (filter.TaxAmount != null && filter.TaxAmount.HasValue)
                query = query.Where(q => q.TaxAmount.HasValue).Where(q => q.TaxAmount, filter.TaxAmount);
            if (filter.TaxAmountOther != null && filter.TaxAmountOther.HasValue)
                query = query.Where(q => q.TaxAmountOther.HasValue).Where(q => q.TaxAmountOther, filter.TaxAmountOther);
            if (filter.TaxPercentageOther != null && filter.TaxPercentageOther.HasValue)
                query = query.Where(q => q.TaxPercentageOther.HasValue).Where(q => q.TaxPercentageOther, filter.TaxPercentageOther);
            if (filter.Amount != null && filter.Amount.HasValue)
                query = query.Where(q => q.Amount, filter.Amount);
            if (filter.Factor != null && filter.Factor.HasValue)
                query = query.Where(q => q.Factor.HasValue).Where(q => q.Factor, filter.Factor);
            if (filter.EditedPriceStatusId != null && filter.EditedPriceStatusId.HasValue)
                query = query.Where(q => q.EditedPriceStatusId, filter.EditedPriceStatusId);
            if (filter.TaxTypeId != null && filter.TaxTypeId.HasValue)
                query = query.Where(q => q.TaxTypeId, filter.TaxTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<OrderQuoteContentDAO> OrFilter(IQueryable<OrderQuoteContentDAO> query, OrderQuoteContentFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OrderQuoteContentDAO> initQuery = query.Where(q => false);
            foreach (OrderQuoteContentFilter OrderQuoteContentFilter in filter.OrFilter)
            {
                IQueryable<OrderQuoteContentDAO> queryable = query;
                if (OrderQuoteContentFilter.Id != null && OrderQuoteContentFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OrderQuoteContentFilter.Id);
                if (OrderQuoteContentFilter.OrderQuoteId != null && OrderQuoteContentFilter.OrderQuoteId.HasValue)
                    queryable = queryable.Where(q => q.OrderQuoteId, OrderQuoteContentFilter.OrderQuoteId);
                if (OrderQuoteContentFilter.ItemId != null && OrderQuoteContentFilter.ItemId.HasValue)
                    queryable = queryable.Where(q => q.ItemId, OrderQuoteContentFilter.ItemId);
                if (OrderQuoteContentFilter.UnitOfMeasureId != null && OrderQuoteContentFilter.UnitOfMeasureId.HasValue)
                    queryable = queryable.Where(q => q.UnitOfMeasureId, OrderQuoteContentFilter.UnitOfMeasureId);
                if (OrderQuoteContentFilter.Quantity != null && OrderQuoteContentFilter.Quantity.HasValue)
                    queryable = queryable.Where(q => q.Quantity, OrderQuoteContentFilter.Quantity);
                if (OrderQuoteContentFilter.RequestedQuantity != null && OrderQuoteContentFilter.RequestedQuantity.HasValue)
                    queryable = queryable.Where(q => q.RequestedQuantity, OrderQuoteContentFilter.RequestedQuantity);
                if (OrderQuoteContentFilter.PrimaryUnitOfMeasureId != null && OrderQuoteContentFilter.PrimaryUnitOfMeasureId.HasValue)
                    queryable = queryable.Where(q => q.PrimaryUnitOfMeasureId, OrderQuoteContentFilter.PrimaryUnitOfMeasureId);
                if (OrderQuoteContentFilter.PrimaryPrice != null && OrderQuoteContentFilter.PrimaryPrice.HasValue)
                    queryable = queryable.Where(q => q.PrimaryPrice, OrderQuoteContentFilter.PrimaryPrice);
                if (OrderQuoteContentFilter.SalePrice != null && OrderQuoteContentFilter.SalePrice.HasValue)
                    queryable = queryable.Where(q => q.SalePrice, OrderQuoteContentFilter.SalePrice);
                if (OrderQuoteContentFilter.DiscountPercentage != null && OrderQuoteContentFilter.DiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.DiscountPercentage.HasValue).Where(q => q.DiscountPercentage, OrderQuoteContentFilter.DiscountPercentage);
                if (OrderQuoteContentFilter.DiscountAmount != null && OrderQuoteContentFilter.DiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.DiscountAmount.HasValue).Where(q => q.DiscountAmount, OrderQuoteContentFilter.DiscountAmount);
                if (OrderQuoteContentFilter.GeneralDiscountPercentage != null && OrderQuoteContentFilter.GeneralDiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, OrderQuoteContentFilter.GeneralDiscountPercentage);
                if (OrderQuoteContentFilter.GeneralDiscountAmount != null && OrderQuoteContentFilter.GeneralDiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, OrderQuoteContentFilter.GeneralDiscountAmount);
                if (OrderQuoteContentFilter.TaxPercentage != null && OrderQuoteContentFilter.TaxPercentage.HasValue)
                    queryable = queryable.Where(q => q.TaxPercentage.HasValue).Where(q => q.TaxPercentage, OrderQuoteContentFilter.TaxPercentage);
                if (OrderQuoteContentFilter.TaxAmount != null && OrderQuoteContentFilter.TaxAmount.HasValue)
                    queryable = queryable.Where(q => q.TaxAmount.HasValue).Where(q => q.TaxAmount, OrderQuoteContentFilter.TaxAmount);
                if (OrderQuoteContentFilter.TaxAmountOther != null && OrderQuoteContentFilter.TaxAmountOther.HasValue)
                    queryable = queryable.Where(q => q.TaxAmountOther.HasValue).Where(q => q.TaxAmountOther, OrderQuoteContentFilter.TaxAmountOther);
                if (OrderQuoteContentFilter.TaxPercentageOther != null && OrderQuoteContentFilter.TaxPercentageOther.HasValue)
                    queryable = queryable.Where(q => q.TaxPercentageOther.HasValue).Where(q => q.TaxPercentageOther, OrderQuoteContentFilter.TaxPercentageOther);
                if (OrderQuoteContentFilter.Amount != null && OrderQuoteContentFilter.Amount.HasValue)
                    queryable = queryable.Where(q => q.Amount, OrderQuoteContentFilter.Amount);
                if (OrderQuoteContentFilter.Factor != null && OrderQuoteContentFilter.Factor.HasValue)
                    queryable = queryable.Where(q => q.Factor.HasValue).Where(q => q.Factor, OrderQuoteContentFilter.Factor);
                if (OrderQuoteContentFilter.EditedPriceStatusId != null && OrderQuoteContentFilter.EditedPriceStatusId.HasValue)
                    queryable = queryable.Where(q => q.EditedPriceStatusId, OrderQuoteContentFilter.EditedPriceStatusId);
                if (OrderQuoteContentFilter.TaxTypeId != null && OrderQuoteContentFilter.TaxTypeId.HasValue)
                    queryable = queryable.Where(q => q.TaxTypeId, OrderQuoteContentFilter.TaxTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<OrderQuoteContentDAO> DynamicOrder(IQueryable<OrderQuoteContentDAO> query, OrderQuoteContentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OrderQuoteContentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderQuoteContentOrder.OrderQuote:
                            query = query.OrderBy(q => q.OrderQuoteId);
                            break;
                        case OrderQuoteContentOrder.Item:
                            query = query.OrderBy(q => q.ItemId);
                            break;
                        case OrderQuoteContentOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasureId);
                            break;
                        case OrderQuoteContentOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        case OrderQuoteContentOrder.RequestedQuantity:
                            query = query.OrderBy(q => q.RequestedQuantity);
                            break;
                        case OrderQuoteContentOrder.PrimaryUnitOfMeasure:
                            query = query.OrderBy(q => q.PrimaryUnitOfMeasureId);
                            break;
                        case OrderQuoteContentOrder.PrimaryPrice:
                            query = query.OrderBy(q => q.PrimaryPrice);
                            break;
                        case OrderQuoteContentOrder.SalePrice:
                            query = query.OrderBy(q => q.SalePrice);
                            break;
                        case OrderQuoteContentOrder.DiscountPercentage:
                            query = query.OrderBy(q => q.DiscountPercentage);
                            break;
                        case OrderQuoteContentOrder.DiscountAmount:
                            query = query.OrderBy(q => q.DiscountAmount);
                            break;
                        case OrderQuoteContentOrder.GeneralDiscountPercentage:
                            query = query.OrderBy(q => q.GeneralDiscountPercentage);
                            break;
                        case OrderQuoteContentOrder.GeneralDiscountAmount:
                            query = query.OrderBy(q => q.GeneralDiscountAmount);
                            break;
                        case OrderQuoteContentOrder.TaxPercentage:
                            query = query.OrderBy(q => q.TaxPercentage);
                            break;
                        case OrderQuoteContentOrder.TaxAmount:
                            query = query.OrderBy(q => q.TaxAmount);
                            break;
                        case OrderQuoteContentOrder.TaxAmountOther:
                            query = query.OrderBy(q => q.TaxAmountOther);
                            break;
                        case OrderQuoteContentOrder.TaxPercentageOther:
                            query = query.OrderBy(q => q.TaxPercentageOther);
                            break;
                        case OrderQuoteContentOrder.Amount:
                            query = query.OrderBy(q => q.Amount);
                            break;
                        case OrderQuoteContentOrder.Factor:
                            query = query.OrderBy(q => q.Factor);
                            break;
                        case OrderQuoteContentOrder.EditedPriceStatus:
                            query = query.OrderBy(q => q.EditedPriceStatusId);
                            break;
                        case OrderQuoteContentOrder.TaxType:
                            query = query.OrderBy(q => q.TaxTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OrderQuoteContentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderQuoteContentOrder.OrderQuote:
                            query = query.OrderByDescending(q => q.OrderQuoteId);
                            break;
                        case OrderQuoteContentOrder.Item:
                            query = query.OrderByDescending(q => q.ItemId);
                            break;
                        case OrderQuoteContentOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasureId);
                            break;
                        case OrderQuoteContentOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                        case OrderQuoteContentOrder.RequestedQuantity:
                            query = query.OrderByDescending(q => q.RequestedQuantity);
                            break;
                        case OrderQuoteContentOrder.PrimaryUnitOfMeasure:
                            query = query.OrderByDescending(q => q.PrimaryUnitOfMeasureId);
                            break;
                        case OrderQuoteContentOrder.PrimaryPrice:
                            query = query.OrderByDescending(q => q.PrimaryPrice);
                            break;
                        case OrderQuoteContentOrder.SalePrice:
                            query = query.OrderByDescending(q => q.SalePrice);
                            break;
                        case OrderQuoteContentOrder.DiscountPercentage:
                            query = query.OrderByDescending(q => q.DiscountPercentage);
                            break;
                        case OrderQuoteContentOrder.DiscountAmount:
                            query = query.OrderByDescending(q => q.DiscountAmount);
                            break;
                        case OrderQuoteContentOrder.GeneralDiscountPercentage:
                            query = query.OrderByDescending(q => q.GeneralDiscountPercentage);
                            break;
                        case OrderQuoteContentOrder.GeneralDiscountAmount:
                            query = query.OrderByDescending(q => q.GeneralDiscountAmount);
                            break;
                        case OrderQuoteContentOrder.TaxPercentage:
                            query = query.OrderByDescending(q => q.TaxPercentage);
                            break;
                        case OrderQuoteContentOrder.TaxAmount:
                            query = query.OrderByDescending(q => q.TaxAmount);
                            break;
                        case OrderQuoteContentOrder.TaxAmountOther:
                            query = query.OrderByDescending(q => q.TaxAmountOther);
                            break;
                        case OrderQuoteContentOrder.TaxPercentageOther:
                            query = query.OrderByDescending(q => q.TaxPercentageOther);
                            break;
                        case OrderQuoteContentOrder.Amount:
                            query = query.OrderByDescending(q => q.Amount);
                            break;
                        case OrderQuoteContentOrder.Factor:
                            query = query.OrderByDescending(q => q.Factor);
                            break;
                        case OrderQuoteContentOrder.EditedPriceStatus:
                            query = query.OrderByDescending(q => q.EditedPriceStatusId);
                            break;
                        case OrderQuoteContentOrder.TaxType:
                            query = query.OrderByDescending(q => q.TaxTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderQuoteContent>> DynamicSelect(IQueryable<OrderQuoteContentDAO> query, OrderQuoteContentFilter filter)
        {
            List<OrderQuoteContent> OrderQuoteContents = await query.Select(q => new OrderQuoteContent()
            {
                Id = filter.Selects.Contains(OrderQuoteContentSelect.Id) ? q.Id : default(long),
                OrderQuoteId = filter.Selects.Contains(OrderQuoteContentSelect.OrderQuote) ? q.OrderQuoteId : default(long),
                ItemId = filter.Selects.Contains(OrderQuoteContentSelect.Item) ? q.ItemId : default(long),
                UnitOfMeasureId = filter.Selects.Contains(OrderQuoteContentSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                Quantity = filter.Selects.Contains(OrderQuoteContentSelect.Quantity) ? q.Quantity : default(long),
                RequestedQuantity = filter.Selects.Contains(OrderQuoteContentSelect.RequestedQuantity) ? q.RequestedQuantity : default(long),
                PrimaryUnitOfMeasureId = filter.Selects.Contains(OrderQuoteContentSelect.PrimaryUnitOfMeasure) ? q.PrimaryUnitOfMeasureId : default(long),
                PrimaryPrice = filter.Selects.Contains(OrderQuoteContentSelect.PrimaryPrice) ? q.PrimaryPrice : default(decimal),
                SalePrice = filter.Selects.Contains(OrderQuoteContentSelect.SalePrice) ? q.SalePrice : default(decimal),
                DiscountPercentage = filter.Selects.Contains(OrderQuoteContentSelect.DiscountPercentage) ? q.DiscountPercentage : default(decimal?),
                DiscountAmount = filter.Selects.Contains(OrderQuoteContentSelect.DiscountAmount) ? q.DiscountAmount : default(decimal?),
                GeneralDiscountPercentage = filter.Selects.Contains(OrderQuoteContentSelect.GeneralDiscountPercentage) ? q.GeneralDiscountPercentage : default(decimal?),
                GeneralDiscountAmount = filter.Selects.Contains(OrderQuoteContentSelect.GeneralDiscountAmount) ? q.GeneralDiscountAmount : default(decimal?),
                TaxPercentage = filter.Selects.Contains(OrderQuoteContentSelect.TaxPercentage) ? q.TaxPercentage : default(decimal?),
                TaxAmount = filter.Selects.Contains(OrderQuoteContentSelect.TaxAmount) ? q.TaxAmount : default(decimal?),
                TaxAmountOther = filter.Selects.Contains(OrderQuoteContentSelect.TaxAmountOther) ? q.TaxAmountOther : default(decimal?),
                TaxPercentageOther = filter.Selects.Contains(OrderQuoteContentSelect.TaxPercentageOther) ? q.TaxPercentageOther : default(decimal?),
                Amount = filter.Selects.Contains(OrderQuoteContentSelect.Amount) ? q.Amount : default(decimal),
                Factor = filter.Selects.Contains(OrderQuoteContentSelect.Factor) ? q.Factor : default(long?),
                EditedPriceStatusId = filter.Selects.Contains(OrderQuoteContentSelect.EditedPriceStatus) ? q.EditedPriceStatusId : default(long),
                TaxTypeId = filter.Selects.Contains(OrderQuoteContentSelect.TaxType) ? q.TaxTypeId : default(long),
                EditedPriceStatus = filter.Selects.Contains(OrderQuoteContentSelect.EditedPriceStatus) && q.EditedPriceStatus != null ? new EditedPriceStatus
                {
                    Id = q.EditedPriceStatus.Id,
                    Code = q.EditedPriceStatus.Code,
                    Name = q.EditedPriceStatus.Name,
                } : null,
                Item = filter.Selects.Contains(OrderQuoteContentSelect.Item) && q.Item != null ? new Item
                {
                    Id = q.Item.Id,
                    ProductId = q.Item.ProductId,
                    Code = q.Item.Code,
                    Name = q.Item.Name,
                    ScanCode = q.Item.ScanCode,
                    SalePrice = q.Item.SalePrice,
                    RetailPrice = q.Item.RetailPrice,
                    StatusId = q.Item.StatusId,
                    Used = q.Item.Used,
                    RowId = q.Item.RowId,
                } : null,
                OrderQuote = filter.Selects.Contains(OrderQuoteContentSelect.OrderQuote) && q.OrderQuote != null ? new OrderQuote
                {
                    Id = q.OrderQuote.Id,
                    Subject = q.OrderQuote.Subject,
                    CompanyId = q.OrderQuote.CompanyId,
                    ContactId = q.OrderQuote.ContactId,
                    OpportunityId = q.OrderQuote.OpportunityId,
                    EditedPriceStatusId = q.OrderQuote.EditedPriceStatusId,
                    EndAt = q.OrderQuote.EndAt,
                    AppUserId = q.OrderQuote.AppUserId,
                    OrderQuoteStatusId = q.OrderQuote.OrderQuoteStatusId,
                    Note = q.OrderQuote.Note,
                    InvoiceAddress = q.OrderQuote.InvoiceAddress,
                    InvoiceNationId = q.OrderQuote.InvoiceNationId,
                    InvoiceProvinceId = q.OrderQuote.InvoiceProvinceId,
                    InvoiceDistrictId = q.OrderQuote.InvoiceDistrictId,
                    InvoiceZIPCode = q.OrderQuote.InvoiceZIPCode,
                    Address = q.OrderQuote.Address,
                    NationId = q.OrderQuote.NationId,
                    ProvinceId = q.OrderQuote.ProvinceId,
                    DistrictId = q.OrderQuote.DistrictId,
                    ZIPCode = q.OrderQuote.ZIPCode,
                    SubTotal = q.OrderQuote.SubTotal,
                    GeneralDiscountPercentage = q.OrderQuote.GeneralDiscountPercentage,
                    GeneralDiscountAmount = q.OrderQuote.GeneralDiscountAmount,
                    TotalTaxAmountOther = q.OrderQuote.TotalTaxAmountOther,
                    TotalTaxAmount = q.OrderQuote.TotalTaxAmount,
                    Total = q.OrderQuote.Total,
                    CreatorId = q.OrderQuote.CreatorId,
                } : null,
                PrimaryUnitOfMeasure = filter.Selects.Contains(OrderQuoteContentSelect.PrimaryUnitOfMeasure) && q.PrimaryUnitOfMeasure != null ? new UnitOfMeasure
                {
                    Id = q.PrimaryUnitOfMeasure.Id,
                    Code = q.PrimaryUnitOfMeasure.Code,
                    Name = q.PrimaryUnitOfMeasure.Name,
                    Description = q.PrimaryUnitOfMeasure.Description,
                    StatusId = q.PrimaryUnitOfMeasure.StatusId,
                    Used = q.PrimaryUnitOfMeasure.Used,
                    RowId = q.PrimaryUnitOfMeasure.RowId,
                } : null,
                TaxType = filter.Selects.Contains(OrderQuoteContentSelect.TaxType) && q.TaxType != null ? new TaxType
                {
                    Id = q.TaxType.Id,
                    Code = q.TaxType.Code,
                    Name = q.TaxType.Name,
                    Percentage = q.TaxType.Percentage,
                    StatusId = q.TaxType.StatusId,
                    Used = q.TaxType.Used,
                    RowId = q.TaxType.RowId,
                } : null,
                UnitOfMeasure = filter.Selects.Contains(OrderQuoteContentSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new UnitOfMeasure
                {
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
                    Description = q.UnitOfMeasure.Description,
                    StatusId = q.UnitOfMeasure.StatusId,
                    Used = q.UnitOfMeasure.Used,
                    RowId = q.UnitOfMeasure.RowId,
                } : null,
            }).ToListAsync();
            return OrderQuoteContents;
        }

        public async Task<int> Count(OrderQuoteContentFilter filter)
        {
            IQueryable<OrderQuoteContentDAO> OrderQuoteContents = DataContext.OrderQuoteContent.AsNoTracking();
            OrderQuoteContents = DynamicFilter(OrderQuoteContents, filter);
            return await OrderQuoteContents.CountAsync();
        }

        public async Task<List<OrderQuoteContent>> List(OrderQuoteContentFilter filter)
        {
            if (filter == null) return new List<OrderQuoteContent>();
            IQueryable<OrderQuoteContentDAO> OrderQuoteContentDAOs = DataContext.OrderQuoteContent.AsNoTracking();
            OrderQuoteContentDAOs = DynamicFilter(OrderQuoteContentDAOs, filter);
            OrderQuoteContentDAOs = DynamicOrder(OrderQuoteContentDAOs, filter);
            List<OrderQuoteContent> OrderQuoteContents = await DynamicSelect(OrderQuoteContentDAOs, filter);
            return OrderQuoteContents;
        }

        public async Task<List<OrderQuoteContent>> List(List<long> Ids)
        {
            List<OrderQuoteContent> OrderQuoteContents = await DataContext.OrderQuoteContent.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OrderQuoteContent()
            {
                Id = x.Id,
                OrderQuoteId = x.OrderQuoteId,
                ItemId = x.ItemId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Quantity = x.Quantity,
                RequestedQuantity = x.RequestedQuantity,
                PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                PrimaryPrice = x.PrimaryPrice,
                SalePrice = x.SalePrice,
                DiscountPercentage = x.DiscountPercentage,
                DiscountAmount = x.DiscountAmount,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TaxPercentage = x.TaxPercentage,
                TaxAmount = x.TaxAmount,
                TaxAmountOther = x.TaxAmountOther,
                TaxPercentageOther = x.TaxPercentageOther,
                Amount = x.Amount,
                Factor = x.Factor,
                EditedPriceStatusId = x.EditedPriceStatusId,
                TaxTypeId = x.TaxTypeId,
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                Item = x.Item == null ? null : new Item
                {
                    Id = x.Item.Id,
                    ProductId = x.Item.ProductId,
                    Code = x.Item.Code,
                    Name = x.Item.Name,
                    ScanCode = x.Item.ScanCode,
                    SalePrice = x.Item.SalePrice,
                    RetailPrice = x.Item.RetailPrice,
                    StatusId = x.Item.StatusId,
                    Used = x.Item.Used,
                    RowId = x.Item.RowId,
                },
                OrderQuote = x.OrderQuote == null ? null : new OrderQuote
                {
                    Id = x.OrderQuote.Id,
                    Subject = x.OrderQuote.Subject,
                    CompanyId = x.OrderQuote.CompanyId,
                    ContactId = x.OrderQuote.ContactId,
                    OpportunityId = x.OrderQuote.OpportunityId,
                    EditedPriceStatusId = x.OrderQuote.EditedPriceStatusId,
                    EndAt = x.OrderQuote.EndAt,
                    AppUserId = x.OrderQuote.AppUserId,
                    OrderQuoteStatusId = x.OrderQuote.OrderQuoteStatusId,
                    Note = x.OrderQuote.Note,
                    InvoiceAddress = x.OrderQuote.InvoiceAddress,
                    InvoiceNationId = x.OrderQuote.InvoiceNationId,
                    InvoiceProvinceId = x.OrderQuote.InvoiceProvinceId,
                    InvoiceDistrictId = x.OrderQuote.InvoiceDistrictId,
                    InvoiceZIPCode = x.OrderQuote.InvoiceZIPCode,
                    Address = x.OrderQuote.Address,
                    NationId = x.OrderQuote.NationId,
                    ProvinceId = x.OrderQuote.ProvinceId,
                    DistrictId = x.OrderQuote.DistrictId,
                    ZIPCode = x.OrderQuote.ZIPCode,
                    SubTotal = x.OrderQuote.SubTotal,
                    GeneralDiscountPercentage = x.OrderQuote.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.OrderQuote.GeneralDiscountAmount,
                    TotalTaxAmountOther = x.OrderQuote.TotalTaxAmountOther,
                    TotalTaxAmount = x.OrderQuote.TotalTaxAmount,
                    Total = x.OrderQuote.Total,
                    CreatorId = x.OrderQuote.CreatorId,
                },
                PrimaryUnitOfMeasure = x.PrimaryUnitOfMeasure == null ? null : new UnitOfMeasure
                {
                    Id = x.PrimaryUnitOfMeasure.Id,
                    Code = x.PrimaryUnitOfMeasure.Code,
                    Name = x.PrimaryUnitOfMeasure.Name,
                    Description = x.PrimaryUnitOfMeasure.Description,
                    StatusId = x.PrimaryUnitOfMeasure.StatusId,
                    Used = x.PrimaryUnitOfMeasure.Used,
                    RowId = x.PrimaryUnitOfMeasure.RowId,
                },
                TaxType = x.TaxType == null ? null : new TaxType
                {
                    Id = x.TaxType.Id,
                    Code = x.TaxType.Code,
                    Name = x.TaxType.Name,
                    Percentage = x.TaxType.Percentage,
                    StatusId = x.TaxType.StatusId,
                    Used = x.TaxType.Used,
                    RowId = x.TaxType.RowId,
                },
                UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                {
                    Id = x.UnitOfMeasure.Id,
                    Code = x.UnitOfMeasure.Code,
                    Name = x.UnitOfMeasure.Name,
                    Description = x.UnitOfMeasure.Description,
                    StatusId = x.UnitOfMeasure.StatusId,
                    Used = x.UnitOfMeasure.Used,
                    RowId = x.UnitOfMeasure.RowId,
                },
            }).ToListAsync();


            return OrderQuoteContents;
        }

        public async Task<OrderQuoteContent> Get(long Id)
        {
            OrderQuoteContent OrderQuoteContent = await DataContext.OrderQuoteContent.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new OrderQuoteContent()
            {
                Id = x.Id,
                OrderQuoteId = x.OrderQuoteId,
                ItemId = x.ItemId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Quantity = x.Quantity,
                RequestedQuantity = x.RequestedQuantity,
                PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                PrimaryPrice = x.PrimaryPrice,
                SalePrice = x.SalePrice,
                DiscountPercentage = x.DiscountPercentage,
                DiscountAmount = x.DiscountAmount,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TaxPercentage = x.TaxPercentage,
                TaxAmount = x.TaxAmount,
                TaxAmountOther = x.TaxAmountOther,
                TaxPercentageOther = x.TaxPercentageOther,
                Amount = x.Amount,
                Factor = x.Factor,
                EditedPriceStatusId = x.EditedPriceStatusId,
                TaxTypeId = x.TaxTypeId,
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                Item = x.Item == null ? null : new Item
                {
                    Id = x.Item.Id,
                    ProductId = x.Item.ProductId,
                    Code = x.Item.Code,
                    Name = x.Item.Name,
                    ScanCode = x.Item.ScanCode,
                    SalePrice = x.Item.SalePrice,
                    RetailPrice = x.Item.RetailPrice,
                    StatusId = x.Item.StatusId,
                    Used = x.Item.Used,
                    RowId = x.Item.RowId,
                },
                OrderQuote = x.OrderQuote == null ? null : new OrderQuote
                {
                    Id = x.OrderQuote.Id,
                    Subject = x.OrderQuote.Subject,
                    CompanyId = x.OrderQuote.CompanyId,
                    ContactId = x.OrderQuote.ContactId,
                    OpportunityId = x.OrderQuote.OpportunityId,
                    EditedPriceStatusId = x.OrderQuote.EditedPriceStatusId,
                    EndAt = x.OrderQuote.EndAt,
                    AppUserId = x.OrderQuote.AppUserId,
                    OrderQuoteStatusId = x.OrderQuote.OrderQuoteStatusId,
                    Note = x.OrderQuote.Note,
                    InvoiceAddress = x.OrderQuote.InvoiceAddress,
                    InvoiceNationId = x.OrderQuote.InvoiceNationId,
                    InvoiceProvinceId = x.OrderQuote.InvoiceProvinceId,
                    InvoiceDistrictId = x.OrderQuote.InvoiceDistrictId,
                    InvoiceZIPCode = x.OrderQuote.InvoiceZIPCode,
                    Address = x.OrderQuote.Address,
                    NationId = x.OrderQuote.NationId,
                    ProvinceId = x.OrderQuote.ProvinceId,
                    DistrictId = x.OrderQuote.DistrictId,
                    ZIPCode = x.OrderQuote.ZIPCode,
                    SubTotal = x.OrderQuote.SubTotal,
                    GeneralDiscountPercentage = x.OrderQuote.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.OrderQuote.GeneralDiscountAmount,
                    TotalTaxAmountOther = x.OrderQuote.TotalTaxAmountOther,
                    TotalTaxAmount = x.OrderQuote.TotalTaxAmount,
                    Total = x.OrderQuote.Total,
                    CreatorId = x.OrderQuote.CreatorId,
                },
                PrimaryUnitOfMeasure = x.PrimaryUnitOfMeasure == null ? null : new UnitOfMeasure
                {
                    Id = x.PrimaryUnitOfMeasure.Id,
                    Code = x.PrimaryUnitOfMeasure.Code,
                    Name = x.PrimaryUnitOfMeasure.Name,
                    Description = x.PrimaryUnitOfMeasure.Description,
                    StatusId = x.PrimaryUnitOfMeasure.StatusId,
                    Used = x.PrimaryUnitOfMeasure.Used,
                    RowId = x.PrimaryUnitOfMeasure.RowId,
                },
                TaxType = x.TaxType == null ? null : new TaxType
                {
                    Id = x.TaxType.Id,
                    Code = x.TaxType.Code,
                    Name = x.TaxType.Name,
                    Percentage = x.TaxType.Percentage,
                    StatusId = x.TaxType.StatusId,
                    Used = x.TaxType.Used,
                    RowId = x.TaxType.RowId,
                },
                UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                {
                    Id = x.UnitOfMeasure.Id,
                    Code = x.UnitOfMeasure.Code,
                    Name = x.UnitOfMeasure.Name,
                    Description = x.UnitOfMeasure.Description,
                    StatusId = x.UnitOfMeasure.StatusId,
                    Used = x.UnitOfMeasure.Used,
                    RowId = x.UnitOfMeasure.RowId,
                },
            }).FirstOrDefaultAsync();

            if (OrderQuoteContent == null)
                return null;

            return OrderQuoteContent;
        }
        public async Task<bool> Create(OrderQuoteContent OrderQuoteContent)
        {
            OrderQuoteContentDAO OrderQuoteContentDAO = new OrderQuoteContentDAO();
            OrderQuoteContentDAO.Id = OrderQuoteContent.Id;
            OrderQuoteContentDAO.OrderQuoteId = OrderQuoteContent.OrderQuoteId;
            OrderQuoteContentDAO.ItemId = OrderQuoteContent.ItemId;
            OrderQuoteContentDAO.UnitOfMeasureId = OrderQuoteContent.UnitOfMeasureId;
            OrderQuoteContentDAO.Quantity = OrderQuoteContent.Quantity;
            OrderQuoteContentDAO.RequestedQuantity = OrderQuoteContent.RequestedQuantity;
            OrderQuoteContentDAO.PrimaryUnitOfMeasureId = OrderQuoteContent.PrimaryUnitOfMeasureId;
            OrderQuoteContentDAO.PrimaryPrice = OrderQuoteContent.PrimaryPrice;
            OrderQuoteContentDAO.SalePrice = OrderQuoteContent.SalePrice;
            OrderQuoteContentDAO.DiscountPercentage = OrderQuoteContent.DiscountPercentage;
            OrderQuoteContentDAO.DiscountAmount = OrderQuoteContent.DiscountAmount;
            OrderQuoteContentDAO.GeneralDiscountPercentage = OrderQuoteContent.GeneralDiscountPercentage;
            OrderQuoteContentDAO.GeneralDiscountAmount = OrderQuoteContent.GeneralDiscountAmount;
            OrderQuoteContentDAO.TaxPercentage = OrderQuoteContent.TaxPercentage;
            OrderQuoteContentDAO.TaxAmount = OrderQuoteContent.TaxAmount;
            OrderQuoteContentDAO.TaxAmountOther = OrderQuoteContent.TaxAmountOther;
            OrderQuoteContentDAO.TaxPercentageOther = OrderQuoteContent.TaxPercentageOther;
            OrderQuoteContentDAO.Amount = OrderQuoteContent.Amount;
            OrderQuoteContentDAO.Factor = OrderQuoteContent.Factor;
            OrderQuoteContentDAO.EditedPriceStatusId = OrderQuoteContent.EditedPriceStatusId;
            OrderQuoteContentDAO.TaxTypeId = OrderQuoteContent.TaxTypeId;
            DataContext.OrderQuoteContent.Add(OrderQuoteContentDAO);
            await DataContext.SaveChangesAsync();
            OrderQuoteContent.Id = OrderQuoteContentDAO.Id;
            await SaveReference(OrderQuoteContent);
            return true;
        }

        public async Task<bool> Update(OrderQuoteContent OrderQuoteContent)
        {
            OrderQuoteContentDAO OrderQuoteContentDAO = DataContext.OrderQuoteContent.Where(x => x.Id == OrderQuoteContent.Id).FirstOrDefault();
            if (OrderQuoteContentDAO == null)
                return false;
            OrderQuoteContentDAO.Id = OrderQuoteContent.Id;
            OrderQuoteContentDAO.OrderQuoteId = OrderQuoteContent.OrderQuoteId;
            OrderQuoteContentDAO.ItemId = OrderQuoteContent.ItemId;
            OrderQuoteContentDAO.UnitOfMeasureId = OrderQuoteContent.UnitOfMeasureId;
            OrderQuoteContentDAO.Quantity = OrderQuoteContent.Quantity;
            OrderQuoteContentDAO.RequestedQuantity = OrderQuoteContent.RequestedQuantity;
            OrderQuoteContentDAO.PrimaryUnitOfMeasureId = OrderQuoteContent.PrimaryUnitOfMeasureId;
            OrderQuoteContentDAO.PrimaryPrice = OrderQuoteContent.PrimaryPrice;
            OrderQuoteContentDAO.SalePrice = OrderQuoteContent.SalePrice;
            OrderQuoteContentDAO.DiscountPercentage = OrderQuoteContent.DiscountPercentage;
            OrderQuoteContentDAO.DiscountAmount = OrderQuoteContent.DiscountAmount;
            OrderQuoteContentDAO.GeneralDiscountPercentage = OrderQuoteContent.GeneralDiscountPercentage;
            OrderQuoteContentDAO.GeneralDiscountAmount = OrderQuoteContent.GeneralDiscountAmount;
            OrderQuoteContentDAO.TaxPercentage = OrderQuoteContent.TaxPercentage;
            OrderQuoteContentDAO.TaxAmount = OrderQuoteContent.TaxAmount;
            OrderQuoteContentDAO.TaxAmountOther = OrderQuoteContent.TaxAmountOther;
            OrderQuoteContentDAO.TaxPercentageOther = OrderQuoteContent.TaxPercentageOther;
            OrderQuoteContentDAO.Amount = OrderQuoteContent.Amount;
            OrderQuoteContentDAO.Factor = OrderQuoteContent.Factor;
            OrderQuoteContentDAO.EditedPriceStatusId = OrderQuoteContent.EditedPriceStatusId;
            OrderQuoteContentDAO.TaxTypeId = OrderQuoteContent.TaxTypeId;
            await DataContext.SaveChangesAsync();
            await SaveReference(OrderQuoteContent);
            return true;
        }

        public async Task<bool> Delete(OrderQuoteContent OrderQuoteContent)
        {
            await DataContext.OrderQuoteContent.Where(x => x.Id == OrderQuoteContent.Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<bool> BulkMerge(List<OrderQuoteContent> OrderQuoteContents)
        {
            List<OrderQuoteContentDAO> OrderQuoteContentDAOs = new List<OrderQuoteContentDAO>();
            foreach (OrderQuoteContent OrderQuoteContent in OrderQuoteContents)
            {
                OrderQuoteContentDAO OrderQuoteContentDAO = new OrderQuoteContentDAO();
                OrderQuoteContentDAO.Id = OrderQuoteContent.Id;
                OrderQuoteContentDAO.OrderQuoteId = OrderQuoteContent.OrderQuoteId;
                OrderQuoteContentDAO.ItemId = OrderQuoteContent.ItemId;
                OrderQuoteContentDAO.UnitOfMeasureId = OrderQuoteContent.UnitOfMeasureId;
                OrderQuoteContentDAO.Quantity = OrderQuoteContent.Quantity;
                OrderQuoteContentDAO.RequestedQuantity = OrderQuoteContent.RequestedQuantity;
                OrderQuoteContentDAO.PrimaryUnitOfMeasureId = OrderQuoteContent.PrimaryUnitOfMeasureId;
                OrderQuoteContentDAO.PrimaryPrice = OrderQuoteContent.PrimaryPrice;
                OrderQuoteContentDAO.SalePrice = OrderQuoteContent.SalePrice;
                OrderQuoteContentDAO.DiscountPercentage = OrderQuoteContent.DiscountPercentage;
                OrderQuoteContentDAO.DiscountAmount = OrderQuoteContent.DiscountAmount;
                OrderQuoteContentDAO.GeneralDiscountPercentage = OrderQuoteContent.GeneralDiscountPercentage;
                OrderQuoteContentDAO.GeneralDiscountAmount = OrderQuoteContent.GeneralDiscountAmount;
                OrderQuoteContentDAO.TaxPercentage = OrderQuoteContent.TaxPercentage;
                OrderQuoteContentDAO.TaxAmount = OrderQuoteContent.TaxAmount;
                OrderQuoteContentDAO.TaxAmountOther = OrderQuoteContent.TaxAmountOther;
                OrderQuoteContentDAO.TaxPercentageOther = OrderQuoteContent.TaxPercentageOther;
                OrderQuoteContentDAO.Amount = OrderQuoteContent.Amount;
                OrderQuoteContentDAO.Factor = OrderQuoteContent.Factor;
                OrderQuoteContentDAO.EditedPriceStatusId = OrderQuoteContent.EditedPriceStatusId;
                OrderQuoteContentDAO.TaxTypeId = OrderQuoteContent.TaxTypeId;
                OrderQuoteContentDAOs.Add(OrderQuoteContentDAO);
            }
            await DataContext.BulkMergeAsync(OrderQuoteContentDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<OrderQuoteContent> OrderQuoteContents)
        {
            List<long> Ids = OrderQuoteContents.Select(x => x.Id).ToList();
            await DataContext.OrderQuoteContent
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(OrderQuoteContent OrderQuoteContent)
        {
        }

    }
}
