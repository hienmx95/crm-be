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
    public interface ICustomerSalesOrderContentRepository
    {
        Task<int> Count(CustomerSalesOrderContentFilter CustomerSalesOrderContentFilter);
        Task<List<CustomerSalesOrderContent>> List(CustomerSalesOrderContentFilter CustomerSalesOrderContentFilter);
        Task<List<CustomerSalesOrderContent>> List(List<long> Ids);
        Task<CustomerSalesOrderContent> Get(long Id);
        Task<bool> Create(CustomerSalesOrderContent CustomerSalesOrderContent);
        Task<bool> Update(CustomerSalesOrderContent CustomerSalesOrderContent);
        Task<bool> Delete(CustomerSalesOrderContent CustomerSalesOrderContent);
        Task<bool> BulkMerge(List<CustomerSalesOrderContent> CustomerSalesOrderContents);
        Task<bool> BulkDelete(List<CustomerSalesOrderContent> CustomerSalesOrderContents);
    }
    public class CustomerSalesOrderContentRepository : ICustomerSalesOrderContentRepository
    {
        private DataContext DataContext;
        public CustomerSalesOrderContentRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerSalesOrderContentDAO> DynamicFilter(IQueryable<CustomerSalesOrderContentDAO> query, CustomerSalesOrderContentFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerSalesOrderId != null && filter.CustomerSalesOrderId.HasValue)
                query = query.Where(q => q.CustomerSalesOrderId, filter.CustomerSalesOrderId);
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
            if (filter.SalePrice != null && filter.SalePrice.HasValue)
                query = query.Where(q => q.SalePrice, filter.SalePrice);
            if (filter.PrimaryPrice != null && filter.PrimaryPrice.HasValue)
                query = query.Where(q => q.PrimaryPrice, filter.PrimaryPrice);
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
            if (filter.TaxPercentageOther != null && filter.TaxPercentageOther.HasValue)
                query = query.Where(q => q.TaxPercentageOther.HasValue).Where(q => q.TaxPercentageOther, filter.TaxPercentageOther);
            if (filter.TaxAmountOther != null && filter.TaxAmountOther.HasValue)
                query = query.Where(q => q.TaxAmountOther.HasValue).Where(q => q.TaxAmountOther, filter.TaxAmountOther);
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

        private IQueryable<CustomerSalesOrderContentDAO> OrFilter(IQueryable<CustomerSalesOrderContentDAO> query, CustomerSalesOrderContentFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerSalesOrderContentDAO> initQuery = query.Where(q => false);
            foreach (CustomerSalesOrderContentFilter CustomerSalesOrderContentFilter in filter.OrFilter)
            {
                IQueryable<CustomerSalesOrderContentDAO> queryable = query;
                if (CustomerSalesOrderContentFilter.Id != null && CustomerSalesOrderContentFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerSalesOrderContentFilter.Id);
                if (CustomerSalesOrderContentFilter.CustomerSalesOrderId != null && CustomerSalesOrderContentFilter.CustomerSalesOrderId.HasValue)
                    queryable = queryable.Where(q => q.CustomerSalesOrderId, CustomerSalesOrderContentFilter.CustomerSalesOrderId);
                if (CustomerSalesOrderContentFilter.ItemId != null && CustomerSalesOrderContentFilter.ItemId.HasValue)
                    queryable = queryable.Where(q => q.ItemId, CustomerSalesOrderContentFilter.ItemId);
                if (CustomerSalesOrderContentFilter.UnitOfMeasureId != null && CustomerSalesOrderContentFilter.UnitOfMeasureId.HasValue)
                    queryable = queryable.Where(q => q.UnitOfMeasureId, CustomerSalesOrderContentFilter.UnitOfMeasureId);
                if (CustomerSalesOrderContentFilter.Quantity != null && CustomerSalesOrderContentFilter.Quantity.HasValue)
                    queryable = queryable.Where(q => q.Quantity, CustomerSalesOrderContentFilter.Quantity);
                if (CustomerSalesOrderContentFilter.RequestedQuantity != null && CustomerSalesOrderContentFilter.RequestedQuantity.HasValue)
                    queryable = queryable.Where(q => q.RequestedQuantity, CustomerSalesOrderContentFilter.RequestedQuantity);
                if (CustomerSalesOrderContentFilter.PrimaryUnitOfMeasureId != null && CustomerSalesOrderContentFilter.PrimaryUnitOfMeasureId.HasValue)
                    queryable = queryable.Where(q => q.PrimaryUnitOfMeasureId, CustomerSalesOrderContentFilter.PrimaryUnitOfMeasureId);
                if (CustomerSalesOrderContentFilter.SalePrice != null && CustomerSalesOrderContentFilter.SalePrice.HasValue)
                    queryable = queryable.Where(q => q.SalePrice, CustomerSalesOrderContentFilter.SalePrice);
                if (CustomerSalesOrderContentFilter.PrimaryPrice != null && CustomerSalesOrderContentFilter.PrimaryPrice.HasValue)
                    queryable = queryable.Where(q => q.PrimaryPrice, CustomerSalesOrderContentFilter.PrimaryPrice);
                if (CustomerSalesOrderContentFilter.DiscountPercentage != null && CustomerSalesOrderContentFilter.DiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.DiscountPercentage.HasValue).Where(q => q.DiscountPercentage, CustomerSalesOrderContentFilter.DiscountPercentage);
                if (CustomerSalesOrderContentFilter.DiscountAmount != null && CustomerSalesOrderContentFilter.DiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.DiscountAmount.HasValue).Where(q => q.DiscountAmount, CustomerSalesOrderContentFilter.DiscountAmount);
                if (CustomerSalesOrderContentFilter.GeneralDiscountPercentage != null && CustomerSalesOrderContentFilter.GeneralDiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, CustomerSalesOrderContentFilter.GeneralDiscountPercentage);
                if (CustomerSalesOrderContentFilter.GeneralDiscountAmount != null && CustomerSalesOrderContentFilter.GeneralDiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, CustomerSalesOrderContentFilter.GeneralDiscountAmount);
                if (CustomerSalesOrderContentFilter.TaxPercentage != null && CustomerSalesOrderContentFilter.TaxPercentage.HasValue)
                    queryable = queryable.Where(q => q.TaxPercentage.HasValue).Where(q => q.TaxPercentage, CustomerSalesOrderContentFilter.TaxPercentage);
                if (CustomerSalesOrderContentFilter.TaxAmount != null && CustomerSalesOrderContentFilter.TaxAmount.HasValue)
                    queryable = queryable.Where(q => q.TaxAmount.HasValue).Where(q => q.TaxAmount, CustomerSalesOrderContentFilter.TaxAmount);
                if (CustomerSalesOrderContentFilter.TaxPercentageOther != null && CustomerSalesOrderContentFilter.TaxPercentageOther.HasValue)
                    queryable = queryable.Where(q => q.TaxPercentageOther.HasValue).Where(q => q.TaxPercentageOther, CustomerSalesOrderContentFilter.TaxPercentageOther);
                if (CustomerSalesOrderContentFilter.TaxAmountOther != null && CustomerSalesOrderContentFilter.TaxAmountOther.HasValue)
                    queryable = queryable.Where(q => q.TaxAmountOther.HasValue).Where(q => q.TaxAmountOther, CustomerSalesOrderContentFilter.TaxAmountOther);
                if (CustomerSalesOrderContentFilter.Amount != null && CustomerSalesOrderContentFilter.Amount.HasValue)
                    queryable = queryable.Where(q => q.Amount, CustomerSalesOrderContentFilter.Amount);
                if (CustomerSalesOrderContentFilter.Factor != null && CustomerSalesOrderContentFilter.Factor.HasValue)
                    queryable = queryable.Where(q => q.Factor.HasValue).Where(q => q.Factor, CustomerSalesOrderContentFilter.Factor);
                if (CustomerSalesOrderContentFilter.EditedPriceStatusId != null && CustomerSalesOrderContentFilter.EditedPriceStatusId.HasValue)
                    queryable = queryable.Where(q => q.EditedPriceStatusId, CustomerSalesOrderContentFilter.EditedPriceStatusId);
                if (CustomerSalesOrderContentFilter.TaxTypeId != null && CustomerSalesOrderContentFilter.TaxTypeId.HasValue)
                    queryable = queryable.Where(q => q.TaxTypeId, CustomerSalesOrderContentFilter.TaxTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerSalesOrderContentDAO> DynamicOrder(IQueryable<CustomerSalesOrderContentDAO> query, CustomerSalesOrderContentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderContentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerSalesOrderContentOrder.CustomerSalesOrder:
                            query = query.OrderBy(q => q.CustomerSalesOrderId);
                            break;
                        case CustomerSalesOrderContentOrder.Item:
                            query = query.OrderBy(q => q.ItemId);
                            break;
                        case CustomerSalesOrderContentOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasureId);
                            break;
                        case CustomerSalesOrderContentOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        case CustomerSalesOrderContentOrder.RequestedQuantity:
                            query = query.OrderBy(q => q.RequestedQuantity);
                            break;
                        case CustomerSalesOrderContentOrder.PrimaryUnitOfMeasure:
                            query = query.OrderBy(q => q.PrimaryUnitOfMeasureId);
                            break;
                        case CustomerSalesOrderContentOrder.SalePrice:
                            query = query.OrderBy(q => q.SalePrice);
                            break;
                        case CustomerSalesOrderContentOrder.PrimaryPrice:
                            query = query.OrderBy(q => q.PrimaryPrice);
                            break;
                        case CustomerSalesOrderContentOrder.DiscountPercentage:
                            query = query.OrderBy(q => q.DiscountPercentage);
                            break;
                        case CustomerSalesOrderContentOrder.DiscountAmount:
                            query = query.OrderBy(q => q.DiscountAmount);
                            break;
                        case CustomerSalesOrderContentOrder.GeneralDiscountPercentage:
                            query = query.OrderBy(q => q.GeneralDiscountPercentage);
                            break;
                        case CustomerSalesOrderContentOrder.GeneralDiscountAmount:
                            query = query.OrderBy(q => q.GeneralDiscountAmount);
                            break;
                        case CustomerSalesOrderContentOrder.TaxPercentage:
                            query = query.OrderBy(q => q.TaxPercentage);
                            break;
                        case CustomerSalesOrderContentOrder.TaxAmount:
                            query = query.OrderBy(q => q.TaxAmount);
                            break;
                        case CustomerSalesOrderContentOrder.TaxPercentageOther:
                            query = query.OrderBy(q => q.TaxPercentageOther);
                            break;
                        case CustomerSalesOrderContentOrder.TaxAmountOther:
                            query = query.OrderBy(q => q.TaxAmountOther);
                            break;
                        case CustomerSalesOrderContentOrder.Amount:
                            query = query.OrderBy(q => q.Amount);
                            break;
                        case CustomerSalesOrderContentOrder.Factor:
                            query = query.OrderBy(q => q.Factor);
                            break;
                        case CustomerSalesOrderContentOrder.EditedPriceStatus:
                            query = query.OrderBy(q => q.EditedPriceStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderContentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerSalesOrderContentOrder.CustomerSalesOrder:
                            query = query.OrderByDescending(q => q.CustomerSalesOrderId);
                            break;
                        case CustomerSalesOrderContentOrder.Item:
                            query = query.OrderByDescending(q => q.ItemId);
                            break;
                        case CustomerSalesOrderContentOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasureId);
                            break;
                        case CustomerSalesOrderContentOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                        case CustomerSalesOrderContentOrder.RequestedQuantity:
                            query = query.OrderByDescending(q => q.RequestedQuantity);
                            break;
                        case CustomerSalesOrderContentOrder.PrimaryUnitOfMeasure:
                            query = query.OrderByDescending(q => q.PrimaryUnitOfMeasureId);
                            break;
                        case CustomerSalesOrderContentOrder.SalePrice:
                            query = query.OrderByDescending(q => q.SalePrice);
                            break;
                        case CustomerSalesOrderContentOrder.PrimaryPrice:
                            query = query.OrderByDescending(q => q.PrimaryPrice);
                            break;
                        case CustomerSalesOrderContentOrder.DiscountPercentage:
                            query = query.OrderByDescending(q => q.DiscountPercentage);
                            break;
                        case CustomerSalesOrderContentOrder.DiscountAmount:
                            query = query.OrderByDescending(q => q.DiscountAmount);
                            break;
                        case CustomerSalesOrderContentOrder.GeneralDiscountPercentage:
                            query = query.OrderByDescending(q => q.GeneralDiscountPercentage);
                            break;
                        case CustomerSalesOrderContentOrder.GeneralDiscountAmount:
                            query = query.OrderByDescending(q => q.GeneralDiscountAmount);
                            break;
                        case CustomerSalesOrderContentOrder.TaxPercentage:
                            query = query.OrderByDescending(q => q.TaxPercentage);
                            break;
                        case CustomerSalesOrderContentOrder.TaxAmount:
                            query = query.OrderByDescending(q => q.TaxAmount);
                            break;
                        case CustomerSalesOrderContentOrder.TaxPercentageOther:
                            query = query.OrderByDescending(q => q.TaxPercentageOther);
                            break;
                        case CustomerSalesOrderContentOrder.TaxAmountOther:
                            query = query.OrderByDescending(q => q.TaxAmountOther);
                            break;
                        case CustomerSalesOrderContentOrder.Amount:
                            query = query.OrderByDescending(q => q.Amount);
                            break;
                        case CustomerSalesOrderContentOrder.Factor:
                            query = query.OrderByDescending(q => q.Factor);
                            break;
                        case CustomerSalesOrderContentOrder.EditedPriceStatus:
                            query = query.OrderByDescending(q => q.EditedPriceStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerSalesOrderContent>> DynamicSelect(IQueryable<CustomerSalesOrderContentDAO> query, CustomerSalesOrderContentFilter filter)
        {
            List<CustomerSalesOrderContent> CustomerSalesOrderContents = await query.Select(q => new CustomerSalesOrderContent()
            {
                Id = filter.Selects.Contains(CustomerSalesOrderContentSelect.Id) ? q.Id : default(long),
                CustomerSalesOrderId = filter.Selects.Contains(CustomerSalesOrderContentSelect.CustomerSalesOrder) ? q.CustomerSalesOrderId : default(long),
                ItemId = filter.Selects.Contains(CustomerSalesOrderContentSelect.Item) ? q.ItemId : default(long),
                UnitOfMeasureId = filter.Selects.Contains(CustomerSalesOrderContentSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                Quantity = filter.Selects.Contains(CustomerSalesOrderContentSelect.Quantity) ? q.Quantity : default(long),
                RequestedQuantity = filter.Selects.Contains(CustomerSalesOrderContentSelect.RequestedQuantity) ? q.RequestedQuantity : default(long),
                PrimaryUnitOfMeasureId = filter.Selects.Contains(CustomerSalesOrderContentSelect.PrimaryUnitOfMeasure) ? q.PrimaryUnitOfMeasureId : default(long),
                SalePrice = filter.Selects.Contains(CustomerSalesOrderContentSelect.SalePrice) ? q.SalePrice : default(decimal),
                PrimaryPrice = filter.Selects.Contains(CustomerSalesOrderContentSelect.PrimaryPrice) ? q.PrimaryPrice : default(decimal),
                DiscountPercentage = filter.Selects.Contains(CustomerSalesOrderContentSelect.DiscountPercentage) ? q.DiscountPercentage : default(decimal?),
                DiscountAmount = filter.Selects.Contains(CustomerSalesOrderContentSelect.DiscountAmount) ? q.DiscountAmount : default(decimal?),
                GeneralDiscountPercentage = filter.Selects.Contains(CustomerSalesOrderContentSelect.GeneralDiscountPercentage) ? q.GeneralDiscountPercentage : default(decimal?),
                GeneralDiscountAmount = filter.Selects.Contains(CustomerSalesOrderContentSelect.GeneralDiscountAmount) ? q.GeneralDiscountAmount : default(decimal?),
                TaxPercentage = filter.Selects.Contains(CustomerSalesOrderContentSelect.TaxPercentage) ? q.TaxPercentage : default(decimal?),
                TaxAmount = filter.Selects.Contains(CustomerSalesOrderContentSelect.TaxAmount) ? q.TaxAmount : default(decimal?),
                TaxPercentageOther = filter.Selects.Contains(CustomerSalesOrderContentSelect.TaxPercentageOther) ? q.TaxPercentageOther : default(decimal?),
                TaxAmountOther = filter.Selects.Contains(CustomerSalesOrderContentSelect.TaxAmountOther) ? q.TaxAmountOther : default(decimal?),
                Amount = filter.Selects.Contains(CustomerSalesOrderContentSelect.Amount) ? q.Amount : default(decimal),
                Factor = filter.Selects.Contains(CustomerSalesOrderContentSelect.Factor) ? q.Factor : default(long?),
                EditedPriceStatusId = filter.Selects.Contains(CustomerSalesOrderContentSelect.EditedPriceStatus) ? q.EditedPriceStatusId : default(long),
                TaxTypeId = filter.Selects.Contains(CustomerSalesOrderContentSelect.TaxType) ? q.TaxTypeId : default(long),
                CustomerSalesOrder = filter.Selects.Contains(CustomerSalesOrderContentSelect.CustomerSalesOrder) && q.CustomerSalesOrder != null ? new CustomerSalesOrder
                {
                    Id = q.CustomerSalesOrder.Id,
                    Code = q.CustomerSalesOrder.Code,
                    CustomerTypeId = q.CustomerSalesOrder.CustomerTypeId,
                    CustomerId = q.CustomerSalesOrder.CustomerId,
                    OpportunityId = q.CustomerSalesOrder.OpportunityId,
                    ContractId = q.CustomerSalesOrder.ContractId,
                    OrderPaymentStatusId = q.CustomerSalesOrder.OrderPaymentStatusId,
                    RequestStateId = q.CustomerSalesOrder.RequestStateId,
                    EditedPriceStatusId = q.CustomerSalesOrder.EditedPriceStatusId,
                    ShippingName = q.CustomerSalesOrder.ShippingName,
                    OrderDate = q.CustomerSalesOrder.OrderDate,
                    DeliveryDate = q.CustomerSalesOrder.DeliveryDate,
                    SalesEmployeeId = q.CustomerSalesOrder.SalesEmployeeId,
                    Note = q.CustomerSalesOrder.Note,
                    InvoiceAddress = q.CustomerSalesOrder.InvoiceAddress,
                    InvoiceNationId = q.CustomerSalesOrder.InvoiceNationId,
                    InvoiceProvinceId = q.CustomerSalesOrder.InvoiceProvinceId,
                    InvoiceDistrictId = q.CustomerSalesOrder.InvoiceDistrictId,
                    InvoiceWardId = q.CustomerSalesOrder.InvoiceWardId,
                    InvoiceZIPCode = q.CustomerSalesOrder.InvoiceZIPCode,
                    DeliveryAddress = q.CustomerSalesOrder.DeliveryAddress,
                    DeliveryNationId = q.CustomerSalesOrder.DeliveryNationId,
                    DeliveryProvinceId = q.CustomerSalesOrder.DeliveryProvinceId,
                    DeliveryDistrictId = q.CustomerSalesOrder.DeliveryDistrictId,
                    DeliveryWardId = q.CustomerSalesOrder.DeliveryWardId,
                    DeliveryZIPCode = q.CustomerSalesOrder.DeliveryZIPCode,
                    SubTotal = q.CustomerSalesOrder.SubTotal,
                    GeneralDiscountPercentage = q.CustomerSalesOrder.GeneralDiscountPercentage,
                    GeneralDiscountAmount = q.CustomerSalesOrder.GeneralDiscountAmount,
                    TotalTaxOther = q.CustomerSalesOrder.TotalTaxOther,
                    TotalTax = q.CustomerSalesOrder.TotalTax,
                    Total = q.CustomerSalesOrder.Total,
                    CreatorId = q.CustomerSalesOrder.CreatorId,
                    OrganizationId = q.CustomerSalesOrder.OrganizationId,
                    RowId = q.CustomerSalesOrder.RowId,
                } : null,
                EditedPriceStatus = filter.Selects.Contains(CustomerSalesOrderContentSelect.EditedPriceStatus) && q.EditedPriceStatus != null ? new EditedPriceStatus
                {
                    Id = q.EditedPriceStatus.Id,
                    Code = q.EditedPriceStatus.Code,
                    Name = q.EditedPriceStatus.Name,
                } : null,
                Item = filter.Selects.Contains(CustomerSalesOrderContentSelect.Item) && q.Item != null ? new Item
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
                PrimaryUnitOfMeasure = filter.Selects.Contains(CustomerSalesOrderContentSelect.PrimaryUnitOfMeasure) && q.PrimaryUnitOfMeasure != null ? new UnitOfMeasure
                {
                    Id = q.PrimaryUnitOfMeasure.Id,
                    Code = q.PrimaryUnitOfMeasure.Code,
                    Name = q.PrimaryUnitOfMeasure.Name,
                    Description = q.PrimaryUnitOfMeasure.Description,
                    StatusId = q.PrimaryUnitOfMeasure.StatusId,
                    Used = q.PrimaryUnitOfMeasure.Used,
                    RowId = q.PrimaryUnitOfMeasure.RowId,
                } : null,
                TaxType = filter.Selects.Contains(CustomerSalesOrderContentSelect.TaxType) && q.TaxType != null ? new TaxType
                {
                    Id = q.TaxType.Id,
                    Code = q.TaxType.Code,
                    Name = q.TaxType.Name,
                } : null,
                UnitOfMeasure = filter.Selects.Contains(CustomerSalesOrderContentSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new UnitOfMeasure
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
            return CustomerSalesOrderContents;
        }

        public async Task<int> Count(CustomerSalesOrderContentFilter filter)
        {
            IQueryable<CustomerSalesOrderContentDAO> CustomerSalesOrderContents = DataContext.CustomerSalesOrderContent.AsNoTracking();
            CustomerSalesOrderContents = DynamicFilter(CustomerSalesOrderContents, filter);
            return await CustomerSalesOrderContents.CountAsync();
        }

        public async Task<List<CustomerSalesOrderContent>> List(CustomerSalesOrderContentFilter filter)
        {
            if (filter == null) return new List<CustomerSalesOrderContent>();
            IQueryable<CustomerSalesOrderContentDAO> CustomerSalesOrderContentDAOs = DataContext.CustomerSalesOrderContent.AsNoTracking();
            CustomerSalesOrderContentDAOs = DynamicFilter(CustomerSalesOrderContentDAOs, filter);
            CustomerSalesOrderContentDAOs = DynamicOrder(CustomerSalesOrderContentDAOs, filter);
            List<CustomerSalesOrderContent> CustomerSalesOrderContents = await DynamicSelect(CustomerSalesOrderContentDAOs, filter);
            return CustomerSalesOrderContents;
        }

        public async Task<List<CustomerSalesOrderContent>> List(List<long> Ids)
        {
            List<CustomerSalesOrderContent> CustomerSalesOrderContents = await DataContext.CustomerSalesOrderContent.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerSalesOrderContent()
            {
                Id = x.Id,
                CustomerSalesOrderId = x.CustomerSalesOrderId,
                ItemId = x.ItemId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Quantity = x.Quantity,
                RequestedQuantity = x.RequestedQuantity,
                PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                SalePrice = x.SalePrice,
                PrimaryPrice = x.PrimaryPrice,
                DiscountPercentage = x.DiscountPercentage,
                DiscountAmount = x.DiscountAmount,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TaxPercentage = x.TaxPercentage,
                TaxAmount = x.TaxAmount,
                TaxPercentageOther = x.TaxPercentageOther,
                TaxAmountOther = x.TaxAmountOther,
                Amount = x.Amount,
                Factor = x.Factor,
                EditedPriceStatusId = x.EditedPriceStatusId,
                TaxTypeId = x.TaxTypeId,
                CustomerSalesOrder = x.CustomerSalesOrder == null ? null : new CustomerSalesOrder
                {
                    Id = x.CustomerSalesOrder.Id,
                    Code = x.CustomerSalesOrder.Code,
                    CustomerTypeId = x.CustomerSalesOrder.CustomerTypeId,
                    CustomerId = x.CustomerSalesOrder.CustomerId,
                    OpportunityId = x.CustomerSalesOrder.OpportunityId,
                    ContractId = x.CustomerSalesOrder.ContractId,
                    OrderPaymentStatusId = x.CustomerSalesOrder.OrderPaymentStatusId,
                    RequestStateId = x.CustomerSalesOrder.RequestStateId,
                    EditedPriceStatusId = x.CustomerSalesOrder.EditedPriceStatusId,
                    ShippingName = x.CustomerSalesOrder.ShippingName,
                    OrderDate = x.CustomerSalesOrder.OrderDate,
                    DeliveryDate = x.CustomerSalesOrder.DeliveryDate,
                    SalesEmployeeId = x.CustomerSalesOrder.SalesEmployeeId,
                    Note = x.CustomerSalesOrder.Note,
                    InvoiceAddress = x.CustomerSalesOrder.InvoiceAddress,
                    InvoiceNationId = x.CustomerSalesOrder.InvoiceNationId,
                    InvoiceProvinceId = x.CustomerSalesOrder.InvoiceProvinceId,
                    InvoiceDistrictId = x.CustomerSalesOrder.InvoiceDistrictId,
                    InvoiceWardId = x.CustomerSalesOrder.InvoiceWardId,
                    InvoiceZIPCode = x.CustomerSalesOrder.InvoiceZIPCode,
                    DeliveryAddress = x.CustomerSalesOrder.DeliveryAddress,
                    DeliveryNationId = x.CustomerSalesOrder.DeliveryNationId,
                    DeliveryProvinceId = x.CustomerSalesOrder.DeliveryProvinceId,
                    DeliveryDistrictId = x.CustomerSalesOrder.DeliveryDistrictId,
                    DeliveryWardId = x.CustomerSalesOrder.DeliveryWardId,
                    DeliveryZIPCode = x.CustomerSalesOrder.DeliveryZIPCode,
                    SubTotal = x.CustomerSalesOrder.SubTotal,
                    GeneralDiscountPercentage = x.CustomerSalesOrder.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.CustomerSalesOrder.GeneralDiscountAmount,
                    TotalTaxOther = x.CustomerSalesOrder.TotalTaxOther,
                    TotalTax = x.CustomerSalesOrder.TotalTax,
                    Total = x.CustomerSalesOrder.Total,
                    CreatorId = x.CustomerSalesOrder.CreatorId,
                    OrganizationId = x.CustomerSalesOrder.OrganizationId,
                    RowId = x.CustomerSalesOrder.RowId,
                },
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
            

            return CustomerSalesOrderContents;
        }

        public async Task<CustomerSalesOrderContent> Get(long Id)
        {
            CustomerSalesOrderContent CustomerSalesOrderContent = await DataContext.CustomerSalesOrderContent.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerSalesOrderContent()
            {
                Id = x.Id,
                CustomerSalesOrderId = x.CustomerSalesOrderId,
                ItemId = x.ItemId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Quantity = x.Quantity,
                RequestedQuantity = x.RequestedQuantity,
                PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                SalePrice = x.SalePrice,
                PrimaryPrice = x.PrimaryPrice,
                DiscountPercentage = x.DiscountPercentage,
                DiscountAmount = x.DiscountAmount,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TaxPercentage = x.TaxPercentage,
                TaxAmount = x.TaxAmount,
                TaxPercentageOther = x.TaxPercentageOther,
                TaxAmountOther = x.TaxAmountOther,
                Amount = x.Amount,
                Factor = x.Factor,
                EditedPriceStatusId = x.EditedPriceStatusId,
                TaxTypeId = x.TaxTypeId,
                CustomerSalesOrder = x.CustomerSalesOrder == null ? null : new CustomerSalesOrder
                {
                    Id = x.CustomerSalesOrder.Id,
                    Code = x.CustomerSalesOrder.Code,
                    CustomerTypeId = x.CustomerSalesOrder.CustomerTypeId,
                    CustomerId = x.CustomerSalesOrder.CustomerId,
                    OpportunityId = x.CustomerSalesOrder.OpportunityId,
                    ContractId = x.CustomerSalesOrder.ContractId,
                    OrderPaymentStatusId = x.CustomerSalesOrder.OrderPaymentStatusId,
                    RequestStateId = x.CustomerSalesOrder.RequestStateId,
                    EditedPriceStatusId = x.CustomerSalesOrder.EditedPriceStatusId,
                    ShippingName = x.CustomerSalesOrder.ShippingName,
                    OrderDate = x.CustomerSalesOrder.OrderDate,
                    DeliveryDate = x.CustomerSalesOrder.DeliveryDate,
                    SalesEmployeeId = x.CustomerSalesOrder.SalesEmployeeId,
                    Note = x.CustomerSalesOrder.Note,
                    InvoiceAddress = x.CustomerSalesOrder.InvoiceAddress,
                    InvoiceNationId = x.CustomerSalesOrder.InvoiceNationId,
                    InvoiceProvinceId = x.CustomerSalesOrder.InvoiceProvinceId,
                    InvoiceDistrictId = x.CustomerSalesOrder.InvoiceDistrictId,
                    InvoiceWardId = x.CustomerSalesOrder.InvoiceWardId,
                    InvoiceZIPCode = x.CustomerSalesOrder.InvoiceZIPCode,
                    DeliveryAddress = x.CustomerSalesOrder.DeliveryAddress,
                    DeliveryNationId = x.CustomerSalesOrder.DeliveryNationId,
                    DeliveryProvinceId = x.CustomerSalesOrder.DeliveryProvinceId,
                    DeliveryDistrictId = x.CustomerSalesOrder.DeliveryDistrictId,
                    DeliveryWardId = x.CustomerSalesOrder.DeliveryWardId,
                    DeliveryZIPCode = x.CustomerSalesOrder.DeliveryZIPCode,
                    SubTotal = x.CustomerSalesOrder.SubTotal,
                    GeneralDiscountPercentage = x.CustomerSalesOrder.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.CustomerSalesOrder.GeneralDiscountAmount,
                    TotalTaxOther = x.CustomerSalesOrder.TotalTaxOther,
                    TotalTax = x.CustomerSalesOrder.TotalTax,
                    Total = x.CustomerSalesOrder.Total,
                    CreatorId = x.CustomerSalesOrder.CreatorId,
                    OrganizationId = x.CustomerSalesOrder.OrganizationId,
                    RowId = x.CustomerSalesOrder.RowId,
                },
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

            if (CustomerSalesOrderContent == null)
                return null;

            return CustomerSalesOrderContent;
        }
        public async Task<bool> Create(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            CustomerSalesOrderContentDAO CustomerSalesOrderContentDAO = new CustomerSalesOrderContentDAO();
            CustomerSalesOrderContentDAO.Id = CustomerSalesOrderContent.Id;
            CustomerSalesOrderContentDAO.CustomerSalesOrderId = CustomerSalesOrderContent.CustomerSalesOrderId;
            CustomerSalesOrderContentDAO.ItemId = CustomerSalesOrderContent.ItemId;
            CustomerSalesOrderContentDAO.UnitOfMeasureId = CustomerSalesOrderContent.UnitOfMeasureId;
            CustomerSalesOrderContentDAO.Quantity = CustomerSalesOrderContent.Quantity;
            CustomerSalesOrderContentDAO.RequestedQuantity = CustomerSalesOrderContent.RequestedQuantity;
            CustomerSalesOrderContentDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderContent.PrimaryUnitOfMeasureId;
            CustomerSalesOrderContentDAO.SalePrice = CustomerSalesOrderContent.SalePrice;
            CustomerSalesOrderContentDAO.PrimaryPrice = CustomerSalesOrderContent.PrimaryPrice;
            CustomerSalesOrderContentDAO.DiscountPercentage = CustomerSalesOrderContent.DiscountPercentage;
            CustomerSalesOrderContentDAO.DiscountAmount = CustomerSalesOrderContent.DiscountAmount;
            CustomerSalesOrderContentDAO.GeneralDiscountPercentage = CustomerSalesOrderContent.GeneralDiscountPercentage;
            CustomerSalesOrderContentDAO.GeneralDiscountAmount = CustomerSalesOrderContent.GeneralDiscountAmount;
            CustomerSalesOrderContentDAO.TaxPercentage = CustomerSalesOrderContent.TaxPercentage;
            CustomerSalesOrderContentDAO.TaxAmount = CustomerSalesOrderContent.TaxAmount;
            CustomerSalesOrderContentDAO.TaxPercentageOther = CustomerSalesOrderContent.TaxPercentageOther;
            CustomerSalesOrderContentDAO.TaxAmountOther = CustomerSalesOrderContent.TaxAmountOther;
            CustomerSalesOrderContentDAO.Amount = CustomerSalesOrderContent.Amount;
            CustomerSalesOrderContentDAO.Factor = CustomerSalesOrderContent.Factor;
            CustomerSalesOrderContentDAO.EditedPriceStatusId = CustomerSalesOrderContent.EditedPriceStatusId;
            CustomerSalesOrderContentDAO.TaxTypeId = CustomerSalesOrderContent.TaxTypeId;
            DataContext.CustomerSalesOrderContent.Add(CustomerSalesOrderContentDAO);
            await DataContext.SaveChangesAsync();
            CustomerSalesOrderContent.Id = CustomerSalesOrderContentDAO.Id;
            await SaveReference(CustomerSalesOrderContent);
            return true;
        }

        public async Task<bool> Update(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            CustomerSalesOrderContentDAO CustomerSalesOrderContentDAO = DataContext.CustomerSalesOrderContent.Where(x => x.Id == CustomerSalesOrderContent.Id).FirstOrDefault();
            if (CustomerSalesOrderContentDAO == null)
                return false;
            CustomerSalesOrderContentDAO.Id = CustomerSalesOrderContent.Id;
            CustomerSalesOrderContentDAO.CustomerSalesOrderId = CustomerSalesOrderContent.CustomerSalesOrderId;
            CustomerSalesOrderContentDAO.ItemId = CustomerSalesOrderContent.ItemId;
            CustomerSalesOrderContentDAO.UnitOfMeasureId = CustomerSalesOrderContent.UnitOfMeasureId;
            CustomerSalesOrderContentDAO.Quantity = CustomerSalesOrderContent.Quantity;
            CustomerSalesOrderContentDAO.RequestedQuantity = CustomerSalesOrderContent.RequestedQuantity;
            CustomerSalesOrderContentDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderContent.PrimaryUnitOfMeasureId;
            CustomerSalesOrderContentDAO.SalePrice = CustomerSalesOrderContent.SalePrice;
            CustomerSalesOrderContentDAO.PrimaryPrice = CustomerSalesOrderContent.PrimaryPrice;
            CustomerSalesOrderContentDAO.DiscountPercentage = CustomerSalesOrderContent.DiscountPercentage;
            CustomerSalesOrderContentDAO.DiscountAmount = CustomerSalesOrderContent.DiscountAmount;
            CustomerSalesOrderContentDAO.GeneralDiscountPercentage = CustomerSalesOrderContent.GeneralDiscountPercentage;
            CustomerSalesOrderContentDAO.GeneralDiscountAmount = CustomerSalesOrderContent.GeneralDiscountAmount;
            CustomerSalesOrderContentDAO.TaxPercentage = CustomerSalesOrderContent.TaxPercentage;
            CustomerSalesOrderContentDAO.TaxAmount = CustomerSalesOrderContent.TaxAmount;
            CustomerSalesOrderContentDAO.TaxPercentageOther = CustomerSalesOrderContent.TaxPercentageOther;
            CustomerSalesOrderContentDAO.TaxAmountOther = CustomerSalesOrderContent.TaxAmountOther;
            CustomerSalesOrderContentDAO.Amount = CustomerSalesOrderContent.Amount;
            CustomerSalesOrderContentDAO.Factor = CustomerSalesOrderContent.Factor;
            CustomerSalesOrderContentDAO.EditedPriceStatusId = CustomerSalesOrderContent.EditedPriceStatusId;
            CustomerSalesOrderContentDAO.TaxTypeId = CustomerSalesOrderContent.TaxTypeId;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerSalesOrderContent);
            return true;
        }

        public async Task<bool> Delete(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            await DataContext.CustomerSalesOrderContent.Where(x => x.Id == CustomerSalesOrderContent.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerSalesOrderContent> CustomerSalesOrderContents)
        {
            List<CustomerSalesOrderContentDAO> CustomerSalesOrderContentDAOs = new List<CustomerSalesOrderContentDAO>();
            foreach (CustomerSalesOrderContent CustomerSalesOrderContent in CustomerSalesOrderContents)
            {
                CustomerSalesOrderContentDAO CustomerSalesOrderContentDAO = new CustomerSalesOrderContentDAO();
                CustomerSalesOrderContentDAO.Id = CustomerSalesOrderContent.Id;
                CustomerSalesOrderContentDAO.CustomerSalesOrderId = CustomerSalesOrderContent.CustomerSalesOrderId;
                CustomerSalesOrderContentDAO.ItemId = CustomerSalesOrderContent.ItemId;
                CustomerSalesOrderContentDAO.UnitOfMeasureId = CustomerSalesOrderContent.UnitOfMeasureId;
                CustomerSalesOrderContentDAO.Quantity = CustomerSalesOrderContent.Quantity;
                CustomerSalesOrderContentDAO.RequestedQuantity = CustomerSalesOrderContent.RequestedQuantity;
                CustomerSalesOrderContentDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderContent.PrimaryUnitOfMeasureId;
                CustomerSalesOrderContentDAO.SalePrice = CustomerSalesOrderContent.SalePrice;
                CustomerSalesOrderContentDAO.PrimaryPrice = CustomerSalesOrderContent.PrimaryPrice;
                CustomerSalesOrderContentDAO.DiscountPercentage = CustomerSalesOrderContent.DiscountPercentage;
                CustomerSalesOrderContentDAO.DiscountAmount = CustomerSalesOrderContent.DiscountAmount;
                CustomerSalesOrderContentDAO.GeneralDiscountPercentage = CustomerSalesOrderContent.GeneralDiscountPercentage;
                CustomerSalesOrderContentDAO.GeneralDiscountAmount = CustomerSalesOrderContent.GeneralDiscountAmount;
                CustomerSalesOrderContentDAO.TaxPercentage = CustomerSalesOrderContent.TaxPercentage;
                CustomerSalesOrderContentDAO.TaxAmount = CustomerSalesOrderContent.TaxAmount;
                CustomerSalesOrderContentDAO.TaxPercentageOther = CustomerSalesOrderContent.TaxPercentageOther;
                CustomerSalesOrderContentDAO.TaxAmountOther = CustomerSalesOrderContent.TaxAmountOther;
                CustomerSalesOrderContentDAO.Amount = CustomerSalesOrderContent.Amount;
                CustomerSalesOrderContentDAO.Factor = CustomerSalesOrderContent.Factor;
                CustomerSalesOrderContentDAO.EditedPriceStatusId = CustomerSalesOrderContent.EditedPriceStatusId;
                CustomerSalesOrderContentDAO.TaxTypeId = CustomerSalesOrderContent.TaxTypeId;
                CustomerSalesOrderContentDAOs.Add(CustomerSalesOrderContentDAO);
            }
            await DataContext.BulkMergeAsync(CustomerSalesOrderContentDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerSalesOrderContent> CustomerSalesOrderContents)
        {
            List<long> Ids = CustomerSalesOrderContents.Select(x => x.Id).ToList();
            await DataContext.CustomerSalesOrderContent
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
        }
        
    }
}
