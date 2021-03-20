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
    public interface ICustomerSalesOrderPaymentHistoryRepository
    {
        Task<int> Count(CustomerSalesOrderPaymentHistoryFilter CustomerSalesOrderPaymentHistoryFilter);
        Task<List<CustomerSalesOrderPaymentHistory>> List(CustomerSalesOrderPaymentHistoryFilter CustomerSalesOrderPaymentHistoryFilter);
        Task<List<CustomerSalesOrderPaymentHistory>> List(List<long> Ids);
        Task<CustomerSalesOrderPaymentHistory> Get(long Id);
        Task<bool> Create(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory);
        Task<bool> Update(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory);
        Task<bool> Delete(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory);
        Task<bool> BulkMerge(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories);
        Task<bool> BulkDelete(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories);
    }
    public class CustomerSalesOrderPaymentHistoryRepository : ICustomerSalesOrderPaymentHistoryRepository
    {
        private DataContext DataContext;
        public CustomerSalesOrderPaymentHistoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerSalesOrderPaymentHistoryDAO> DynamicFilter(IQueryable<CustomerSalesOrderPaymentHistoryDAO> query, CustomerSalesOrderPaymentHistoryFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null && filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null && filter.UpdatedAt.HasValue)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerSalesOrderId != null && filter.CustomerSalesOrderId.HasValue)
                query = query.Where(q => q.CustomerSalesOrderId, filter.CustomerSalesOrderId);
            if (filter.PaymentMilestone != null && filter.PaymentMilestone.HasValue)
                query = query.Where(q => q.PaymentMilestone, filter.PaymentMilestone);
            if (filter.PaymentPercentage != null && filter.PaymentPercentage.HasValue)
                query = query.Where(q => q.PaymentPercentage.HasValue).Where(q => q.PaymentPercentage, filter.PaymentPercentage);
            if (filter.PaymentAmount != null && filter.PaymentAmount.HasValue)
                query = query.Where(q => q.PaymentAmount.HasValue).Where(q => q.PaymentAmount, filter.PaymentAmount);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerSalesOrderPaymentHistoryDAO> OrFilter(IQueryable<CustomerSalesOrderPaymentHistoryDAO> query, CustomerSalesOrderPaymentHistoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerSalesOrderPaymentHistoryDAO> initQuery = query.Where(q => false);
            foreach (CustomerSalesOrderPaymentHistoryFilter CustomerSalesOrderPaymentHistoryFilter in filter.OrFilter)
            {
                IQueryable<CustomerSalesOrderPaymentHistoryDAO> queryable = query;
                if (CustomerSalesOrderPaymentHistoryFilter.Id != null && CustomerSalesOrderPaymentHistoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerSalesOrderPaymentHistoryFilter.Id);
                if (CustomerSalesOrderPaymentHistoryFilter.CustomerSalesOrderId != null && CustomerSalesOrderPaymentHistoryFilter.CustomerSalesOrderId.HasValue)
                    queryable = queryable.Where(q => q.CustomerSalesOrderId, CustomerSalesOrderPaymentHistoryFilter.CustomerSalesOrderId);
                if (CustomerSalesOrderPaymentHistoryFilter.PaymentMilestone != null && CustomerSalesOrderPaymentHistoryFilter.PaymentMilestone.HasValue)
                    queryable = queryable.Where(q => q.PaymentMilestone, CustomerSalesOrderPaymentHistoryFilter.PaymentMilestone);
                if (CustomerSalesOrderPaymentHistoryFilter.PaymentPercentage != null && CustomerSalesOrderPaymentHistoryFilter.PaymentPercentage.HasValue)
                    queryable = queryable.Where(q => q.PaymentPercentage.HasValue).Where(q => q.PaymentPercentage, CustomerSalesOrderPaymentHistoryFilter.PaymentPercentage);
                if (CustomerSalesOrderPaymentHistoryFilter.PaymentAmount != null && CustomerSalesOrderPaymentHistoryFilter.PaymentAmount.HasValue)
                    queryable = queryable.Where(q => q.PaymentAmount.HasValue).Where(q => q.PaymentAmount, CustomerSalesOrderPaymentHistoryFilter.PaymentAmount);
                if (CustomerSalesOrderPaymentHistoryFilter.Description != null && CustomerSalesOrderPaymentHistoryFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerSalesOrderPaymentHistoryFilter.Description);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerSalesOrderPaymentHistoryDAO> DynamicOrder(IQueryable<CustomerSalesOrderPaymentHistoryDAO> query, CustomerSalesOrderPaymentHistoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderPaymentHistoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.CustomerSalesOrder:
                            query = query.OrderBy(q => q.CustomerSalesOrderId);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.PaymentMilestone:
                            query = query.OrderBy(q => q.PaymentMilestone);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.PaymentPercentage:
                            query = query.OrderBy(q => q.PaymentPercentage);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.PaymentAmount:
                            query = query.OrderBy(q => q.PaymentAmount);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.IsPaid:
                            query = query.OrderBy(q => q.IsPaid);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderPaymentHistoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.CustomerSalesOrder:
                            query = query.OrderByDescending(q => q.CustomerSalesOrderId);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.PaymentMilestone:
                            query = query.OrderByDescending(q => q.PaymentMilestone);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.PaymentPercentage:
                            query = query.OrderByDescending(q => q.PaymentPercentage);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.PaymentAmount:
                            query = query.OrderByDescending(q => q.PaymentAmount);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerSalesOrderPaymentHistoryOrder.IsPaid:
                            query = query.OrderByDescending(q => q.IsPaid);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerSalesOrderPaymentHistory>> DynamicSelect(IQueryable<CustomerSalesOrderPaymentHistoryDAO> query, CustomerSalesOrderPaymentHistoryFilter filter)
        {
            List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories = await query.Select(q => new CustomerSalesOrderPaymentHistory()
            {
                Id = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.Id) ? q.Id : default(long),
                CustomerSalesOrderId = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.CustomerSalesOrder) ? q.CustomerSalesOrderId : default(long),
                PaymentMilestone = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.PaymentMilestone) ? q.PaymentMilestone : default(string),
                PaymentPercentage = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.PaymentPercentage) ? q.PaymentPercentage : default(decimal?),
                PaymentAmount = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.PaymentAmount) ? q.PaymentAmount : default(decimal?),
                Description = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.Description) ? q.Description : default(string),
                IsPaid = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.IsPaid) ? q.IsPaid : default(bool?),
                CustomerSalesOrder = filter.Selects.Contains(CustomerSalesOrderPaymentHistorySelect.CustomerSalesOrder) && q.CustomerSalesOrder != null ? new CustomerSalesOrder
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
            }).ToListAsync();
            return CustomerSalesOrderPaymentHistories;
        }

        public async Task<int> Count(CustomerSalesOrderPaymentHistoryFilter filter)
        {
            IQueryable<CustomerSalesOrderPaymentHistoryDAO> CustomerSalesOrderPaymentHistories = DataContext.CustomerSalesOrderPaymentHistory.AsNoTracking();
            CustomerSalesOrderPaymentHistories = DynamicFilter(CustomerSalesOrderPaymentHistories, filter);
            return await CustomerSalesOrderPaymentHistories.CountAsync();
        }

        public async Task<List<CustomerSalesOrderPaymentHistory>> List(CustomerSalesOrderPaymentHistoryFilter filter)
        {
            if (filter == null) return new List<CustomerSalesOrderPaymentHistory>();
            IQueryable<CustomerSalesOrderPaymentHistoryDAO> CustomerSalesOrderPaymentHistoryDAOs = DataContext.CustomerSalesOrderPaymentHistory.AsNoTracking();
            CustomerSalesOrderPaymentHistoryDAOs = DynamicFilter(CustomerSalesOrderPaymentHistoryDAOs, filter);
            CustomerSalesOrderPaymentHistoryDAOs = DynamicOrder(CustomerSalesOrderPaymentHistoryDAOs, filter);
            List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories = await DynamicSelect(CustomerSalesOrderPaymentHistoryDAOs, filter);
            return CustomerSalesOrderPaymentHistories;
        }

        public async Task<List<CustomerSalesOrderPaymentHistory>> List(List<long> Ids)
        {
            List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories = await DataContext.CustomerSalesOrderPaymentHistory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerSalesOrderPaymentHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                CustomerSalesOrderId = x.CustomerSalesOrderId,
                PaymentMilestone = x.PaymentMilestone,
                PaymentPercentage = x.PaymentPercentage,
                PaymentAmount = x.PaymentAmount,
                Description = x.Description,
                IsPaid = x.IsPaid,
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
            }).ToListAsync();
            

            return CustomerSalesOrderPaymentHistories;
        }

        public async Task<CustomerSalesOrderPaymentHistory> Get(long Id)
        {
            CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory = await DataContext.CustomerSalesOrderPaymentHistory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerSalesOrderPaymentHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                CustomerSalesOrderId = x.CustomerSalesOrderId,
                PaymentMilestone = x.PaymentMilestone,
                PaymentPercentage = x.PaymentPercentage,
                PaymentAmount = x.PaymentAmount,
                Description = x.Description,
                IsPaid = x.IsPaid,
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
            }).FirstOrDefaultAsync();

            if (CustomerSalesOrderPaymentHistory == null)
                return null;

            return CustomerSalesOrderPaymentHistory;
        }
        public async Task<bool> Create(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            CustomerSalesOrderPaymentHistoryDAO CustomerSalesOrderPaymentHistoryDAO = new CustomerSalesOrderPaymentHistoryDAO();
            CustomerSalesOrderPaymentHistoryDAO.Id = CustomerSalesOrderPaymentHistory.Id;
            CustomerSalesOrderPaymentHistoryDAO.CustomerSalesOrderId = CustomerSalesOrderPaymentHistory.CustomerSalesOrderId;
            CustomerSalesOrderPaymentHistoryDAO.PaymentMilestone = CustomerSalesOrderPaymentHistory.PaymentMilestone;
            CustomerSalesOrderPaymentHistoryDAO.PaymentPercentage = CustomerSalesOrderPaymentHistory.PaymentPercentage;
            CustomerSalesOrderPaymentHistoryDAO.PaymentAmount = CustomerSalesOrderPaymentHistory.PaymentAmount;
            CustomerSalesOrderPaymentHistoryDAO.Description = CustomerSalesOrderPaymentHistory.Description;
            CustomerSalesOrderPaymentHistoryDAO.IsPaid = CustomerSalesOrderPaymentHistory.IsPaid;
            CustomerSalesOrderPaymentHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerSalesOrderPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerSalesOrderPaymentHistory.Add(CustomerSalesOrderPaymentHistoryDAO);
            await DataContext.SaveChangesAsync();
            CustomerSalesOrderPaymentHistory.Id = CustomerSalesOrderPaymentHistoryDAO.Id;
            await SaveReference(CustomerSalesOrderPaymentHistory);
            return true;
        }

        public async Task<bool> Update(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            CustomerSalesOrderPaymentHistoryDAO CustomerSalesOrderPaymentHistoryDAO = DataContext.CustomerSalesOrderPaymentHistory.Where(x => x.Id == CustomerSalesOrderPaymentHistory.Id).FirstOrDefault();
            if (CustomerSalesOrderPaymentHistoryDAO == null)
                return false;
            CustomerSalesOrderPaymentHistoryDAO.Id = CustomerSalesOrderPaymentHistory.Id;
            CustomerSalesOrderPaymentHistoryDAO.CustomerSalesOrderId = CustomerSalesOrderPaymentHistory.CustomerSalesOrderId;
            CustomerSalesOrderPaymentHistoryDAO.PaymentMilestone = CustomerSalesOrderPaymentHistory.PaymentMilestone;
            CustomerSalesOrderPaymentHistoryDAO.PaymentPercentage = CustomerSalesOrderPaymentHistory.PaymentPercentage;
            CustomerSalesOrderPaymentHistoryDAO.PaymentAmount = CustomerSalesOrderPaymentHistory.PaymentAmount;
            CustomerSalesOrderPaymentHistoryDAO.Description = CustomerSalesOrderPaymentHistory.Description;
            CustomerSalesOrderPaymentHistoryDAO.IsPaid = CustomerSalesOrderPaymentHistory.IsPaid;
            CustomerSalesOrderPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerSalesOrderPaymentHistory);
            return true;
        }

        public async Task<bool> Delete(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            await DataContext.CustomerSalesOrderPaymentHistory.Where(x => x.Id == CustomerSalesOrderPaymentHistory.Id).UpdateFromQueryAsync(x => new CustomerSalesOrderPaymentHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories)
        {
            List<CustomerSalesOrderPaymentHistoryDAO> CustomerSalesOrderPaymentHistoryDAOs = new List<CustomerSalesOrderPaymentHistoryDAO>();
            foreach (CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory in CustomerSalesOrderPaymentHistories)
            {
                CustomerSalesOrderPaymentHistoryDAO CustomerSalesOrderPaymentHistoryDAO = new CustomerSalesOrderPaymentHistoryDAO();
                CustomerSalesOrderPaymentHistoryDAO.Id = CustomerSalesOrderPaymentHistory.Id;
                CustomerSalesOrderPaymentHistoryDAO.CustomerSalesOrderId = CustomerSalesOrderPaymentHistory.CustomerSalesOrderId;
                CustomerSalesOrderPaymentHistoryDAO.PaymentMilestone = CustomerSalesOrderPaymentHistory.PaymentMilestone;
                CustomerSalesOrderPaymentHistoryDAO.PaymentPercentage = CustomerSalesOrderPaymentHistory.PaymentPercentage;
                CustomerSalesOrderPaymentHistoryDAO.PaymentAmount = CustomerSalesOrderPaymentHistory.PaymentAmount;
                CustomerSalesOrderPaymentHistoryDAO.Description = CustomerSalesOrderPaymentHistory.Description;
                CustomerSalesOrderPaymentHistoryDAO.IsPaid = CustomerSalesOrderPaymentHistory.IsPaid;
                CustomerSalesOrderPaymentHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerSalesOrderPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerSalesOrderPaymentHistoryDAOs.Add(CustomerSalesOrderPaymentHistoryDAO);
            }
            await DataContext.BulkMergeAsync(CustomerSalesOrderPaymentHistoryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories)
        {
            List<long> Ids = CustomerSalesOrderPaymentHistories.Select(x => x.Id).ToList();
            await DataContext.CustomerSalesOrderPaymentHistory
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerSalesOrderPaymentHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
        }
        
    }
}
