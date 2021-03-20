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
    public interface ICustomerSalesOrderPromotionRepository
    {
        Task<int> Count(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter);
        Task<List<CustomerSalesOrderPromotion>> List(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter);
        Task<List<CustomerSalesOrderPromotion>> List(List<long> Ids);
        Task<CustomerSalesOrderPromotion> Get(long Id);
        Task<bool> Create(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<bool> Update(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<bool> Delete(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<bool> BulkMerge(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions);
        Task<bool> BulkDelete(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions);
    }
    public class CustomerSalesOrderPromotionRepository : ICustomerSalesOrderPromotionRepository
    {
        private DataContext DataContext;
        public CustomerSalesOrderPromotionRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerSalesOrderPromotionDAO> DynamicFilter(IQueryable<CustomerSalesOrderPromotionDAO> query, CustomerSalesOrderPromotionFilter filter)
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
            if (filter.Factor != null && filter.Factor.HasValue)
                query = query.Where(q => q.Factor.HasValue).Where(q => q.Factor, filter.Factor);
            if (filter.Note != null && filter.Note.HasValue)
                query = query.Where(q => q.Note, filter.Note);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerSalesOrderPromotionDAO> OrFilter(IQueryable<CustomerSalesOrderPromotionDAO> query, CustomerSalesOrderPromotionFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerSalesOrderPromotionDAO> initQuery = query.Where(q => false);
            foreach (CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter in filter.OrFilter)
            {
                IQueryable<CustomerSalesOrderPromotionDAO> queryable = query;
                if (CustomerSalesOrderPromotionFilter.Id != null && CustomerSalesOrderPromotionFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerSalesOrderPromotionFilter.Id);
                if (CustomerSalesOrderPromotionFilter.CustomerSalesOrderId != null && CustomerSalesOrderPromotionFilter.CustomerSalesOrderId.HasValue)
                    queryable = queryable.Where(q => q.CustomerSalesOrderId, CustomerSalesOrderPromotionFilter.CustomerSalesOrderId);
                if (CustomerSalesOrderPromotionFilter.ItemId != null && CustomerSalesOrderPromotionFilter.ItemId.HasValue)
                    queryable = queryable.Where(q => q.ItemId, CustomerSalesOrderPromotionFilter.ItemId);
                if (CustomerSalesOrderPromotionFilter.UnitOfMeasureId != null && CustomerSalesOrderPromotionFilter.UnitOfMeasureId.HasValue)
                    queryable = queryable.Where(q => q.UnitOfMeasureId, CustomerSalesOrderPromotionFilter.UnitOfMeasureId);
                if (CustomerSalesOrderPromotionFilter.Quantity != null && CustomerSalesOrderPromotionFilter.Quantity.HasValue)
                    queryable = queryable.Where(q => q.Quantity, CustomerSalesOrderPromotionFilter.Quantity);
                if (CustomerSalesOrderPromotionFilter.RequestedQuantity != null && CustomerSalesOrderPromotionFilter.RequestedQuantity.HasValue)
                    queryable = queryable.Where(q => q.RequestedQuantity, CustomerSalesOrderPromotionFilter.RequestedQuantity);
                if (CustomerSalesOrderPromotionFilter.PrimaryUnitOfMeasureId != null && CustomerSalesOrderPromotionFilter.PrimaryUnitOfMeasureId.HasValue)
                    queryable = queryable.Where(q => q.PrimaryUnitOfMeasureId, CustomerSalesOrderPromotionFilter.PrimaryUnitOfMeasureId);
                if (CustomerSalesOrderPromotionFilter.Factor != null && CustomerSalesOrderPromotionFilter.Factor.HasValue)
                    queryable = queryable.Where(q => q.Factor.HasValue).Where(q => q.Factor, CustomerSalesOrderPromotionFilter.Factor);
                if (CustomerSalesOrderPromotionFilter.Note != null && CustomerSalesOrderPromotionFilter.Note.HasValue)
                    queryable = queryable.Where(q => q.Note, CustomerSalesOrderPromotionFilter.Note);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerSalesOrderPromotionDAO> DynamicOrder(IQueryable<CustomerSalesOrderPromotionDAO> query, CustomerSalesOrderPromotionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderPromotionOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerSalesOrderPromotionOrder.CustomerSalesOrder:
                            query = query.OrderBy(q => q.CustomerSalesOrderId);
                            break;
                        case CustomerSalesOrderPromotionOrder.Item:
                            query = query.OrderBy(q => q.ItemId);
                            break;
                        case CustomerSalesOrderPromotionOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasureId);
                            break;
                        case CustomerSalesOrderPromotionOrder.Quantity:
                            query = query.OrderBy(q => q.Quantity);
                            break;
                        case CustomerSalesOrderPromotionOrder.RequestedQuantity:
                            query = query.OrderBy(q => q.RequestedQuantity);
                            break;
                        case CustomerSalesOrderPromotionOrder.PrimaryUnitOfMeasure:
                            query = query.OrderBy(q => q.PrimaryUnitOfMeasureId);
                            break;
                        case CustomerSalesOrderPromotionOrder.Factor:
                            query = query.OrderBy(q => q.Factor);
                            break;
                        case CustomerSalesOrderPromotionOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderPromotionOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerSalesOrderPromotionOrder.CustomerSalesOrder:
                            query = query.OrderByDescending(q => q.CustomerSalesOrderId);
                            break;
                        case CustomerSalesOrderPromotionOrder.Item:
                            query = query.OrderByDescending(q => q.ItemId);
                            break;
                        case CustomerSalesOrderPromotionOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasureId);
                            break;
                        case CustomerSalesOrderPromotionOrder.Quantity:
                            query = query.OrderByDescending(q => q.Quantity);
                            break;
                        case CustomerSalesOrderPromotionOrder.RequestedQuantity:
                            query = query.OrderByDescending(q => q.RequestedQuantity);
                            break;
                        case CustomerSalesOrderPromotionOrder.PrimaryUnitOfMeasure:
                            query = query.OrderByDescending(q => q.PrimaryUnitOfMeasureId);
                            break;
                        case CustomerSalesOrderPromotionOrder.Factor:
                            query = query.OrderByDescending(q => q.Factor);
                            break;
                        case CustomerSalesOrderPromotionOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerSalesOrderPromotion>> DynamicSelect(IQueryable<CustomerSalesOrderPromotionDAO> query, CustomerSalesOrderPromotionFilter filter)
        {
            List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions = await query.Select(q => new CustomerSalesOrderPromotion()
            {
                Id = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.Id) ? q.Id : default(long),
                CustomerSalesOrderId = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.CustomerSalesOrder) ? q.CustomerSalesOrderId : default(long),
                ItemId = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.Item) ? q.ItemId : default(long),
                UnitOfMeasureId = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                Quantity = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.Quantity) ? q.Quantity : default(long),
                RequestedQuantity = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.RequestedQuantity) ? q.RequestedQuantity : default(long),
                PrimaryUnitOfMeasureId = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.PrimaryUnitOfMeasure) ? q.PrimaryUnitOfMeasureId : default(long),
                Factor = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.Factor) ? q.Factor : default(long?),
                Note = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.Note) ? q.Note : default(string),
                CustomerSalesOrder = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.CustomerSalesOrder) && q.CustomerSalesOrder != null ? new CustomerSalesOrder
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
                Item = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.Item) && q.Item != null ? new Item
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
                PrimaryUnitOfMeasure = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.PrimaryUnitOfMeasure) && q.PrimaryUnitOfMeasure != null ? new UnitOfMeasure
                {
                    Id = q.PrimaryUnitOfMeasure.Id,
                    Code = q.PrimaryUnitOfMeasure.Code,
                    Name = q.PrimaryUnitOfMeasure.Name,
                    Description = q.PrimaryUnitOfMeasure.Description,
                    StatusId = q.PrimaryUnitOfMeasure.StatusId,
                    Used = q.PrimaryUnitOfMeasure.Used,
                    RowId = q.PrimaryUnitOfMeasure.RowId,
                } : null,
                UnitOfMeasure = filter.Selects.Contains(CustomerSalesOrderPromotionSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new UnitOfMeasure
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
            return CustomerSalesOrderPromotions;
        }

        public async Task<int> Count(CustomerSalesOrderPromotionFilter filter)
        {
            IQueryable<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotions = DataContext.CustomerSalesOrderPromotion.AsNoTracking();
            CustomerSalesOrderPromotions = DynamicFilter(CustomerSalesOrderPromotions, filter);
            return await CustomerSalesOrderPromotions.CountAsync();
        }

        public async Task<List<CustomerSalesOrderPromotion>> List(CustomerSalesOrderPromotionFilter filter)
        {
            if (filter == null) return new List<CustomerSalesOrderPromotion>();
            IQueryable<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotionDAOs = DataContext.CustomerSalesOrderPromotion.AsNoTracking();
            CustomerSalesOrderPromotionDAOs = DynamicFilter(CustomerSalesOrderPromotionDAOs, filter);
            CustomerSalesOrderPromotionDAOs = DynamicOrder(CustomerSalesOrderPromotionDAOs, filter);
            List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions = await DynamicSelect(CustomerSalesOrderPromotionDAOs, filter);
            return CustomerSalesOrderPromotions;
        }

        public async Task<List<CustomerSalesOrderPromotion>> List(List<long> Ids)
        {
            List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions = await DataContext.CustomerSalesOrderPromotion.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerSalesOrderPromotion()
            {
                Id = x.Id,
                CustomerSalesOrderId = x.CustomerSalesOrderId,
                ItemId = x.ItemId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Quantity = x.Quantity,
                RequestedQuantity = x.RequestedQuantity,
                PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                Factor = x.Factor,
                Note = x.Note,
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
            

            return CustomerSalesOrderPromotions;
        }

        public async Task<CustomerSalesOrderPromotion> Get(long Id)
        {
            CustomerSalesOrderPromotion CustomerSalesOrderPromotion = await DataContext.CustomerSalesOrderPromotion.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerSalesOrderPromotion()
            {
                Id = x.Id,
                CustomerSalesOrderId = x.CustomerSalesOrderId,
                ItemId = x.ItemId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                Quantity = x.Quantity,
                RequestedQuantity = x.RequestedQuantity,
                PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                Factor = x.Factor,
                Note = x.Note,
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

            if (CustomerSalesOrderPromotion == null)
                return null;

            return CustomerSalesOrderPromotion;
        }
        public async Task<bool> Create(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            CustomerSalesOrderPromotionDAO CustomerSalesOrderPromotionDAO = new CustomerSalesOrderPromotionDAO();
            CustomerSalesOrderPromotionDAO.Id = CustomerSalesOrderPromotion.Id;
            CustomerSalesOrderPromotionDAO.CustomerSalesOrderId = CustomerSalesOrderPromotion.CustomerSalesOrderId;
            CustomerSalesOrderPromotionDAO.ItemId = CustomerSalesOrderPromotion.ItemId;
            CustomerSalesOrderPromotionDAO.UnitOfMeasureId = CustomerSalesOrderPromotion.UnitOfMeasureId;
            CustomerSalesOrderPromotionDAO.Quantity = CustomerSalesOrderPromotion.Quantity;
            CustomerSalesOrderPromotionDAO.RequestedQuantity = CustomerSalesOrderPromotion.RequestedQuantity;
            CustomerSalesOrderPromotionDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId;
            CustomerSalesOrderPromotionDAO.Factor = CustomerSalesOrderPromotion.Factor;
            CustomerSalesOrderPromotionDAO.Note = CustomerSalesOrderPromotion.Note;
            DataContext.CustomerSalesOrderPromotion.Add(CustomerSalesOrderPromotionDAO);
            await DataContext.SaveChangesAsync();
            CustomerSalesOrderPromotion.Id = CustomerSalesOrderPromotionDAO.Id;
            await SaveReference(CustomerSalesOrderPromotion);
            return true;
        }

        public async Task<bool> Update(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            CustomerSalesOrderPromotionDAO CustomerSalesOrderPromotionDAO = DataContext.CustomerSalesOrderPromotion.Where(x => x.Id == CustomerSalesOrderPromotion.Id).FirstOrDefault();
            if (CustomerSalesOrderPromotionDAO == null)
                return false;
            CustomerSalesOrderPromotionDAO.Id = CustomerSalesOrderPromotion.Id;
            CustomerSalesOrderPromotionDAO.CustomerSalesOrderId = CustomerSalesOrderPromotion.CustomerSalesOrderId;
            CustomerSalesOrderPromotionDAO.ItemId = CustomerSalesOrderPromotion.ItemId;
            CustomerSalesOrderPromotionDAO.UnitOfMeasureId = CustomerSalesOrderPromotion.UnitOfMeasureId;
            CustomerSalesOrderPromotionDAO.Quantity = CustomerSalesOrderPromotion.Quantity;
            CustomerSalesOrderPromotionDAO.RequestedQuantity = CustomerSalesOrderPromotion.RequestedQuantity;
            CustomerSalesOrderPromotionDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId;
            CustomerSalesOrderPromotionDAO.Factor = CustomerSalesOrderPromotion.Factor;
            CustomerSalesOrderPromotionDAO.Note = CustomerSalesOrderPromotion.Note;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerSalesOrderPromotion);
            return true;
        }

        public async Task<bool> Delete(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            await DataContext.CustomerSalesOrderPromotion.Where(x => x.Id == CustomerSalesOrderPromotion.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions)
        {
            List<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotionDAOs = new List<CustomerSalesOrderPromotionDAO>();
            foreach (CustomerSalesOrderPromotion CustomerSalesOrderPromotion in CustomerSalesOrderPromotions)
            {
                CustomerSalesOrderPromotionDAO CustomerSalesOrderPromotionDAO = new CustomerSalesOrderPromotionDAO();
                CustomerSalesOrderPromotionDAO.Id = CustomerSalesOrderPromotion.Id;
                CustomerSalesOrderPromotionDAO.CustomerSalesOrderId = CustomerSalesOrderPromotion.CustomerSalesOrderId;
                CustomerSalesOrderPromotionDAO.ItemId = CustomerSalesOrderPromotion.ItemId;
                CustomerSalesOrderPromotionDAO.UnitOfMeasureId = CustomerSalesOrderPromotion.UnitOfMeasureId;
                CustomerSalesOrderPromotionDAO.Quantity = CustomerSalesOrderPromotion.Quantity;
                CustomerSalesOrderPromotionDAO.RequestedQuantity = CustomerSalesOrderPromotion.RequestedQuantity;
                CustomerSalesOrderPromotionDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId;
                CustomerSalesOrderPromotionDAO.Factor = CustomerSalesOrderPromotion.Factor;
                CustomerSalesOrderPromotionDAO.Note = CustomerSalesOrderPromotion.Note;
                CustomerSalesOrderPromotionDAOs.Add(CustomerSalesOrderPromotionDAO);
            }
            await DataContext.BulkMergeAsync(CustomerSalesOrderPromotionDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions)
        {
            List<long> Ids = CustomerSalesOrderPromotions.Select(x => x.Id).ToList();
            await DataContext.CustomerSalesOrderPromotion
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
        }
        
    }
}
