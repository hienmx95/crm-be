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
    public interface ICustomerSalesOrderRepository
    {
        Task<int> Count(CustomerSalesOrderFilter CustomerSalesOrderFilter);
        Task<List<CustomerSalesOrder>> List(CustomerSalesOrderFilter CustomerSalesOrderFilter);
        Task<List<CustomerSalesOrder>> List(List<long> Ids);
        Task<CustomerSalesOrder> Get(long Id);
        Task<bool> Create(CustomerSalesOrder CustomerSalesOrder);
        Task<bool> Update(CustomerSalesOrder CustomerSalesOrder);
        Task<bool> Delete(CustomerSalesOrder CustomerSalesOrder);
        Task<bool> BulkMerge(List<CustomerSalesOrder> CustomerSalesOrders);
        Task<bool> BulkDelete(List<CustomerSalesOrder> CustomerSalesOrders);
    }
    public class CustomerSalesOrderRepository : ICustomerSalesOrderRepository
    {
        private DataContext DataContext;
        public CustomerSalesOrderRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerSalesOrderDAO> DynamicFilter(IQueryable<CustomerSalesOrderDAO> query, CustomerSalesOrderFilter filter)
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
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.CustomerTypeId != null && filter.CustomerTypeId.HasValue)
                query = query.Where(q => q.CustomerTypeId, filter.CustomerTypeId);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.OpportunityId != null && filter.OpportunityId.HasValue)
                query = query.Where(q => q.OpportunityId.HasValue).Where(q => q.OpportunityId, filter.OpportunityId);
            if (filter.ContractId != null && filter.ContractId.HasValue)
                query = query.Where(q => q.ContractId.HasValue).Where(q => q.ContractId, filter.ContractId);
            if (filter.OrderPaymentStatusId != null && filter.OrderPaymentStatusId.HasValue)
                query = query.Where(q => q.OrderPaymentStatusId.HasValue).Where(q => q.OrderPaymentStatusId, filter.OrderPaymentStatusId);
            if (filter.RequestStateId != null && filter.RequestStateId.HasValue)
                query = query.Where(q => q.RequestStateId.HasValue).Where(q => q.RequestStateId, filter.RequestStateId);
            if (filter.EditedPriceStatusId != null && filter.EditedPriceStatusId.HasValue)
                query = query.Where(q => q.EditedPriceStatusId, filter.EditedPriceStatusId);
            if (filter.ShippingName != null && filter.ShippingName.HasValue)
                query = query.Where(q => q.ShippingName, filter.ShippingName);
            if (filter.OrderDate != null && filter.OrderDate.HasValue)
                query = query.Where(q => q.OrderDate, filter.OrderDate);
            if (filter.DeliveryDate != null && filter.DeliveryDate.HasValue)
                query = query.Where(q => q.DeliveryDate == null).Union(query.Where(q => q.DeliveryDate.HasValue).Where(q => q.DeliveryDate, filter.DeliveryDate));
            if (filter.SalesEmployeeId != null && filter.SalesEmployeeId.HasValue)
                query = query.Where(q => q.SalesEmployeeId, filter.SalesEmployeeId);
            if (filter.Note != null && filter.Note.HasValue)
                query = query.Where(q => q.Note, filter.Note);
            if (filter.InvoiceAddress != null && filter.InvoiceAddress.HasValue)
                query = query.Where(q => q.InvoiceAddress, filter.InvoiceAddress);
            if (filter.InvoiceNationId != null && filter.InvoiceNationId.HasValue)
                query = query.Where(q => q.InvoiceNationId.HasValue).Where(q => q.InvoiceNationId, filter.InvoiceNationId);
            if (filter.InvoiceProvinceId != null && filter.InvoiceProvinceId.HasValue)
                query = query.Where(q => q.InvoiceProvinceId.HasValue).Where(q => q.InvoiceProvinceId, filter.InvoiceProvinceId);
            if (filter.InvoiceDistrictId != null && filter.InvoiceDistrictId.HasValue)
                query = query.Where(q => q.InvoiceDistrictId.HasValue).Where(q => q.InvoiceDistrictId, filter.InvoiceDistrictId);
            if (filter.InvoiceWardId != null && filter.InvoiceWardId.HasValue)
                query = query.Where(q => q.InvoiceWardId.HasValue).Where(q => q.InvoiceWardId, filter.InvoiceWardId);
            if (filter.InvoiceZIPCode != null && filter.InvoiceZIPCode.HasValue)
                query = query.Where(q => q.InvoiceZIPCode, filter.InvoiceZIPCode);
            if (filter.DeliveryAddress != null && filter.DeliveryAddress.HasValue)
                query = query.Where(q => q.DeliveryAddress, filter.DeliveryAddress);
            if (filter.DeliveryNationId != null && filter.DeliveryNationId.HasValue)
                query = query.Where(q => q.DeliveryNationId.HasValue).Where(q => q.DeliveryNationId, filter.DeliveryNationId);
            if (filter.DeliveryProvinceId != null && filter.DeliveryProvinceId.HasValue)
                query = query.Where(q => q.DeliveryProvinceId.HasValue).Where(q => q.DeliveryProvinceId, filter.DeliveryProvinceId);
            if (filter.DeliveryDistrictId != null && filter.DeliveryDistrictId.HasValue)
                query = query.Where(q => q.DeliveryDistrictId.HasValue).Where(q => q.DeliveryDistrictId, filter.DeliveryDistrictId);
            if (filter.DeliveryWardId != null && filter.DeliveryWardId.HasValue)
                query = query.Where(q => q.DeliveryWardId.HasValue).Where(q => q.DeliveryWardId, filter.DeliveryWardId);
            if (filter.DeliveryZIPCode != null && filter.DeliveryZIPCode.HasValue)
                query = query.Where(q => q.DeliveryZIPCode, filter.DeliveryZIPCode);
            if (filter.SubTotal != null && filter.SubTotal.HasValue)
                query = query.Where(q => q.SubTotal, filter.SubTotal);
            if (filter.GeneralDiscountPercentage != null && filter.GeneralDiscountPercentage.HasValue)
                query = query.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, filter.GeneralDiscountPercentage);
            if (filter.GeneralDiscountAmount != null && filter.GeneralDiscountAmount.HasValue)
                query = query.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, filter.GeneralDiscountAmount);
            if (filter.TotalTaxOther != null && filter.TotalTaxOther.HasValue)
                query = query.Where(q => q.TotalTaxOther, filter.TotalTaxOther);
            if (filter.TotalTax != null && filter.TotalTax.HasValue)
                query = query.Where(q => q.TotalTax, filter.TotalTax);
            if (filter.Total != null && filter.Total.HasValue)
                query = query.Where(q => q.Total, filter.Total);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.OrganizationId != null && filter.OrganizationId.HasValue)
                query = query.Where(q => q.OrganizationId, filter.OrganizationId);
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.Customer.CompanyId, filter.CompanyId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerSalesOrderDAO> OrFilter(IQueryable<CustomerSalesOrderDAO> query, CustomerSalesOrderFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerSalesOrderDAO> initQuery = query.Where(q => false);
            foreach (CustomerSalesOrderFilter CustomerSalesOrderFilter in filter.OrFilter)
            {
                IQueryable<CustomerSalesOrderDAO> queryable = query;
                if (CustomerSalesOrderFilter.Id != null && CustomerSalesOrderFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerSalesOrderFilter.Id);
                if (CustomerSalesOrderFilter.Code != null && CustomerSalesOrderFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerSalesOrderFilter.Code);
                if (CustomerSalesOrderFilter.CustomerTypeId != null && CustomerSalesOrderFilter.CustomerTypeId.HasValue)
                    queryable = queryable.Where(q => q.CustomerTypeId, CustomerSalesOrderFilter.CustomerTypeId);
                if (CustomerSalesOrderFilter.CustomerId != null && CustomerSalesOrderFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, CustomerSalesOrderFilter.CustomerId);
                if (CustomerSalesOrderFilter.OpportunityId != null && CustomerSalesOrderFilter.OpportunityId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityId.HasValue).Where(q => q.OpportunityId, CustomerSalesOrderFilter.OpportunityId);
                if (CustomerSalesOrderFilter.ContractId != null && CustomerSalesOrderFilter.ContractId.HasValue)
                    queryable = queryable.Where(q => q.ContractId.HasValue).Where(q => q.ContractId, CustomerSalesOrderFilter.ContractId);
                if (CustomerSalesOrderFilter.OrderPaymentStatusId != null && CustomerSalesOrderFilter.OrderPaymentStatusId.HasValue)
                    queryable = queryable.Where(q => q.OrderPaymentStatusId.HasValue).Where(q => q.OrderPaymentStatusId, CustomerSalesOrderFilter.OrderPaymentStatusId);
                if (CustomerSalesOrderFilter.RequestStateId != null && CustomerSalesOrderFilter.RequestStateId.HasValue)
                    queryable = queryable.Where(q => q.RequestStateId.HasValue).Where(q => q.RequestStateId, CustomerSalesOrderFilter.RequestStateId);
                if (CustomerSalesOrderFilter.EditedPriceStatusId != null && CustomerSalesOrderFilter.EditedPriceStatusId.HasValue)
                    queryable = queryable.Where(q => q.EditedPriceStatusId, CustomerSalesOrderFilter.EditedPriceStatusId);
                if (CustomerSalesOrderFilter.ShippingName != null && CustomerSalesOrderFilter.ShippingName.HasValue)
                    queryable = queryable.Where(q => q.ShippingName, CustomerSalesOrderFilter.ShippingName);
                if (CustomerSalesOrderFilter.OrderDate != null && CustomerSalesOrderFilter.OrderDate.HasValue)
                    queryable = queryable.Where(q => q.OrderDate, CustomerSalesOrderFilter.OrderDate);
                if (CustomerSalesOrderFilter.DeliveryDate != null && CustomerSalesOrderFilter.DeliveryDate.HasValue)
                    queryable = queryable.Where(q => q.DeliveryDate.HasValue).Where(q => q.DeliveryDate, CustomerSalesOrderFilter.DeliveryDate);
                if (CustomerSalesOrderFilter.SalesEmployeeId != null && CustomerSalesOrderFilter.SalesEmployeeId.HasValue)
                    queryable = queryable.Where(q => q.SalesEmployeeId, CustomerSalesOrderFilter.SalesEmployeeId);
                if (CustomerSalesOrderFilter.Note != null && CustomerSalesOrderFilter.Note.HasValue)
                    queryable = queryable.Where(q => q.Note, CustomerSalesOrderFilter.Note);
                if (CustomerSalesOrderFilter.InvoiceAddress != null && CustomerSalesOrderFilter.InvoiceAddress.HasValue)
                    queryable = queryable.Where(q => q.InvoiceAddress, CustomerSalesOrderFilter.InvoiceAddress);
                if (CustomerSalesOrderFilter.InvoiceNationId != null && CustomerSalesOrderFilter.InvoiceNationId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceNationId.HasValue).Where(q => q.InvoiceNationId, CustomerSalesOrderFilter.InvoiceNationId);
                if (CustomerSalesOrderFilter.InvoiceProvinceId != null && CustomerSalesOrderFilter.InvoiceProvinceId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceProvinceId.HasValue).Where(q => q.InvoiceProvinceId, CustomerSalesOrderFilter.InvoiceProvinceId);
                if (CustomerSalesOrderFilter.InvoiceDistrictId != null && CustomerSalesOrderFilter.InvoiceDistrictId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceDistrictId.HasValue).Where(q => q.InvoiceDistrictId, CustomerSalesOrderFilter.InvoiceDistrictId);
                if (CustomerSalesOrderFilter.InvoiceWardId != null && CustomerSalesOrderFilter.InvoiceWardId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceWardId.HasValue).Where(q => q.InvoiceWardId, CustomerSalesOrderFilter.InvoiceWardId);
                if (CustomerSalesOrderFilter.InvoiceZIPCode != null && CustomerSalesOrderFilter.InvoiceZIPCode.HasValue)
                    queryable = queryable.Where(q => q.InvoiceZIPCode, CustomerSalesOrderFilter.InvoiceZIPCode);
                if (CustomerSalesOrderFilter.DeliveryAddress != null && CustomerSalesOrderFilter.DeliveryAddress.HasValue)
                    queryable = queryable.Where(q => q.DeliveryAddress, CustomerSalesOrderFilter.DeliveryAddress);
                if (CustomerSalesOrderFilter.DeliveryNationId != null && CustomerSalesOrderFilter.DeliveryNationId.HasValue)
                    queryable = queryable.Where(q => q.DeliveryNationId.HasValue).Where(q => q.DeliveryNationId, CustomerSalesOrderFilter.DeliveryNationId);
                if (CustomerSalesOrderFilter.DeliveryProvinceId != null && CustomerSalesOrderFilter.DeliveryProvinceId.HasValue)
                    queryable = queryable.Where(q => q.DeliveryProvinceId.HasValue).Where(q => q.DeliveryProvinceId, CustomerSalesOrderFilter.DeliveryProvinceId);
                if (CustomerSalesOrderFilter.DeliveryDistrictId != null && CustomerSalesOrderFilter.DeliveryDistrictId.HasValue)
                    queryable = queryable.Where(q => q.DeliveryDistrictId.HasValue).Where(q => q.DeliveryDistrictId, CustomerSalesOrderFilter.DeliveryDistrictId);
                if (CustomerSalesOrderFilter.DeliveryWardId != null && CustomerSalesOrderFilter.DeliveryWardId.HasValue)
                    queryable = queryable.Where(q => q.DeliveryWardId.HasValue).Where(q => q.DeliveryWardId, CustomerSalesOrderFilter.DeliveryWardId);
                if (CustomerSalesOrderFilter.DeliveryZIPCode != null && CustomerSalesOrderFilter.DeliveryZIPCode.HasValue)
                    queryable = queryable.Where(q => q.DeliveryZIPCode, CustomerSalesOrderFilter.DeliveryZIPCode);
                if (CustomerSalesOrderFilter.SubTotal != null && CustomerSalesOrderFilter.SubTotal.HasValue)
                    queryable = queryable.Where(q => q.SubTotal, CustomerSalesOrderFilter.SubTotal);
                if (CustomerSalesOrderFilter.GeneralDiscountPercentage != null && CustomerSalesOrderFilter.GeneralDiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, CustomerSalesOrderFilter.GeneralDiscountPercentage);
                if (CustomerSalesOrderFilter.GeneralDiscountAmount != null && CustomerSalesOrderFilter.GeneralDiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, CustomerSalesOrderFilter.GeneralDiscountAmount);
                if (CustomerSalesOrderFilter.TotalTaxOther != null && CustomerSalesOrderFilter.TotalTaxOther.HasValue)
                    queryable = queryable.Where(q => q.TotalTaxOther, CustomerSalesOrderFilter.TotalTaxOther);
                if (CustomerSalesOrderFilter.TotalTax != null && CustomerSalesOrderFilter.TotalTax.HasValue)
                    queryable = queryable.Where(q => q.TotalTax, CustomerSalesOrderFilter.TotalTax);
                if (CustomerSalesOrderFilter.Total != null && CustomerSalesOrderFilter.Total.HasValue)
                    queryable = queryable.Where(q => q.Total, CustomerSalesOrderFilter.Total);
                if (CustomerSalesOrderFilter.CreatorId != null && CustomerSalesOrderFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CustomerSalesOrderFilter.CreatorId);
                if (CustomerSalesOrderFilter.OrganizationId != null && CustomerSalesOrderFilter.OrganizationId.HasValue)
                    queryable = queryable.Where(q => q.OrganizationId, CustomerSalesOrderFilter.OrganizationId);
                if (CustomerSalesOrderFilter.RowId != null && CustomerSalesOrderFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CustomerSalesOrderFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerSalesOrderDAO> DynamicOrder(IQueryable<CustomerSalesOrderDAO> query, CustomerSalesOrderFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerSalesOrderOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerSalesOrderOrder.CustomerType:
                            query = query.OrderBy(q => q.CustomerTypeId);
                            break;
                        case CustomerSalesOrderOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case CustomerSalesOrderOrder.Opportunity:
                            query = query.OrderBy(q => q.OpportunityId);
                            break;
                        case CustomerSalesOrderOrder.Contract:
                            query = query.OrderBy(q => q.ContractId);
                            break;
                        case CustomerSalesOrderOrder.OrderPaymentStatus:
                            query = query.OrderBy(q => q.OrderPaymentStatusId);
                            break;
                        case CustomerSalesOrderOrder.RequestState:
                            query = query.OrderBy(q => q.RequestStateId);
                            break;
                        case CustomerSalesOrderOrder.EditedPriceStatus:
                            query = query.OrderBy(q => q.EditedPriceStatusId);
                            break;
                        case CustomerSalesOrderOrder.ShippingName:
                            query = query.OrderBy(q => q.ShippingName);
                            break;
                        case CustomerSalesOrderOrder.OrderDate:
                            query = query.OrderBy(q => q.OrderDate);
                            break;
                        case CustomerSalesOrderOrder.DeliveryDate:
                            query = query.OrderBy(q => q.DeliveryDate);
                            break;
                        case CustomerSalesOrderOrder.SalesEmployee:
                            query = query.OrderBy(q => q.SalesEmployeeId);
                            break;
                        case CustomerSalesOrderOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        case CustomerSalesOrderOrder.InvoiceAddress:
                            query = query.OrderBy(q => q.InvoiceAddress);
                            break;
                        case CustomerSalesOrderOrder.InvoiceNation:
                            query = query.OrderBy(q => q.InvoiceNationId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceProvince:
                            query = query.OrderBy(q => q.InvoiceProvinceId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceDistrict:
                            query = query.OrderBy(q => q.InvoiceDistrictId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceWard:
                            query = query.OrderBy(q => q.InvoiceWardId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceZIPCode:
                            query = query.OrderBy(q => q.InvoiceZIPCode);
                            break;
                        case CustomerSalesOrderOrder.DeliveryAddress:
                            query = query.OrderBy(q => q.DeliveryAddress);
                            break;
                        case CustomerSalesOrderOrder.DeliveryNation:
                            query = query.OrderBy(q => q.DeliveryNationId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryProvince:
                            query = query.OrderBy(q => q.DeliveryProvinceId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryDistrict:
                            query = query.OrderBy(q => q.DeliveryDistrictId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryWard:
                            query = query.OrderBy(q => q.DeliveryWardId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryZIPCode:
                            query = query.OrderBy(q => q.DeliveryZIPCode);
                            break;
                        case CustomerSalesOrderOrder.SubTotal:
                            query = query.OrderBy(q => q.SubTotal);
                            break;
                        case CustomerSalesOrderOrder.GeneralDiscountPercentage:
                            query = query.OrderBy(q => q.GeneralDiscountPercentage);
                            break;
                        case CustomerSalesOrderOrder.GeneralDiscountAmount:
                            query = query.OrderBy(q => q.GeneralDiscountAmount);
                            break;
                        case CustomerSalesOrderOrder.TotalTaxOther:
                            query = query.OrderBy(q => q.TotalTaxOther);
                            break;
                        case CustomerSalesOrderOrder.TotalTax:
                            query = query.OrderBy(q => q.TotalTax);
                            break;
                        case CustomerSalesOrderOrder.Total:
                            query = query.OrderBy(q => q.Total);
                            break;
                        case CustomerSalesOrderOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CustomerSalesOrderOrder.Organization:
                            query = query.OrderBy(q => q.OrganizationId);
                            break;
                        case CustomerSalesOrderOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerSalesOrderOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerSalesOrderOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerSalesOrderOrder.CustomerType:
                            query = query.OrderByDescending(q => q.CustomerTypeId);
                            break;
                        case CustomerSalesOrderOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case CustomerSalesOrderOrder.Opportunity:
                            query = query.OrderByDescending(q => q.OpportunityId);
                            break;
                        case CustomerSalesOrderOrder.Contract:
                            query = query.OrderByDescending(q => q.ContractId);
                            break;
                        case CustomerSalesOrderOrder.OrderPaymentStatus:
                            query = query.OrderByDescending(q => q.OrderPaymentStatusId);
                            break;
                        case CustomerSalesOrderOrder.RequestState:
                            query = query.OrderByDescending(q => q.RequestStateId);
                            break;
                        case CustomerSalesOrderOrder.EditedPriceStatus:
                            query = query.OrderByDescending(q => q.EditedPriceStatusId);
                            break;
                        case CustomerSalesOrderOrder.ShippingName:
                            query = query.OrderByDescending(q => q.ShippingName);
                            break;
                        case CustomerSalesOrderOrder.OrderDate:
                            query = query.OrderByDescending(q => q.OrderDate);
                            break;
                        case CustomerSalesOrderOrder.DeliveryDate:
                            query = query.OrderByDescending(q => q.DeliveryDate);
                            break;
                        case CustomerSalesOrderOrder.SalesEmployee:
                            query = query.OrderByDescending(q => q.SalesEmployeeId);
                            break;
                        case CustomerSalesOrderOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
                            break;
                        case CustomerSalesOrderOrder.InvoiceAddress:
                            query = query.OrderByDescending(q => q.InvoiceAddress);
                            break;
                        case CustomerSalesOrderOrder.InvoiceNation:
                            query = query.OrderByDescending(q => q.InvoiceNationId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceProvince:
                            query = query.OrderByDescending(q => q.InvoiceProvinceId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceDistrict:
                            query = query.OrderByDescending(q => q.InvoiceDistrictId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceWard:
                            query = query.OrderByDescending(q => q.InvoiceWardId);
                            break;
                        case CustomerSalesOrderOrder.InvoiceZIPCode:
                            query = query.OrderByDescending(q => q.InvoiceZIPCode);
                            break;
                        case CustomerSalesOrderOrder.DeliveryAddress:
                            query = query.OrderByDescending(q => q.DeliveryAddress);
                            break;
                        case CustomerSalesOrderOrder.DeliveryNation:
                            query = query.OrderByDescending(q => q.DeliveryNationId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryProvince:
                            query = query.OrderByDescending(q => q.DeliveryProvinceId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryDistrict:
                            query = query.OrderByDescending(q => q.DeliveryDistrictId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryWard:
                            query = query.OrderByDescending(q => q.DeliveryWardId);
                            break;
                        case CustomerSalesOrderOrder.DeliveryZIPCode:
                            query = query.OrderByDescending(q => q.DeliveryZIPCode);
                            break;
                        case CustomerSalesOrderOrder.SubTotal:
                            query = query.OrderByDescending(q => q.SubTotal);
                            break;
                        case CustomerSalesOrderOrder.GeneralDiscountPercentage:
                            query = query.OrderByDescending(q => q.GeneralDiscountPercentage);
                            break;
                        case CustomerSalesOrderOrder.GeneralDiscountAmount:
                            query = query.OrderByDescending(q => q.GeneralDiscountAmount);
                            break;
                        case CustomerSalesOrderOrder.TotalTaxOther:
                            query = query.OrderByDescending(q => q.TotalTaxOther);
                            break;
                        case CustomerSalesOrderOrder.TotalTax:
                            query = query.OrderByDescending(q => q.TotalTax);
                            break;
                        case CustomerSalesOrderOrder.Total:
                            query = query.OrderByDescending(q => q.Total);
                            break;
                        case CustomerSalesOrderOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CustomerSalesOrderOrder.Organization:
                            query = query.OrderByDescending(q => q.OrganizationId);
                            break;
                        case CustomerSalesOrderOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerSalesOrder>> DynamicSelect(IQueryable<CustomerSalesOrderDAO> query, CustomerSalesOrderFilter filter)
        {
            List<CustomerSalesOrder> CustomerSalesOrders = await query.Select(q => new CustomerSalesOrder()
            {
                Id = filter.Selects.Contains(CustomerSalesOrderSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerSalesOrderSelect.Code) ? q.Code : default(string),
                CustomerTypeId = filter.Selects.Contains(CustomerSalesOrderSelect.CustomerType) ? q.CustomerTypeId : default(long),
                CustomerId = filter.Selects.Contains(CustomerSalesOrderSelect.Customer) ? q.CustomerId : default(long),
                OpportunityId = filter.Selects.Contains(CustomerSalesOrderSelect.Opportunity) ? q.OpportunityId : default(long?),
                ContractId = filter.Selects.Contains(CustomerSalesOrderSelect.Contract) ? q.ContractId : default(long?),
                OrderPaymentStatusId = filter.Selects.Contains(CustomerSalesOrderSelect.OrderPaymentStatus) ? q.OrderPaymentStatusId : default(long?),
                RequestStateId = filter.Selects.Contains(CustomerSalesOrderSelect.RequestState) ? q.RequestStateId : default(long?),
                EditedPriceStatusId = filter.Selects.Contains(CustomerSalesOrderSelect.EditedPriceStatus) ? q.EditedPriceStatusId : default(long),
                ShippingName = filter.Selects.Contains(CustomerSalesOrderSelect.ShippingName) ? q.ShippingName : default(string),
                OrderDate = filter.Selects.Contains(CustomerSalesOrderSelect.OrderDate) ? q.OrderDate : default(DateTime),
                DeliveryDate = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryDate) ? q.DeliveryDate : default(DateTime?),
                SalesEmployeeId = filter.Selects.Contains(CustomerSalesOrderSelect.SalesEmployee) ? q.SalesEmployeeId : default(long),
                Note = filter.Selects.Contains(CustomerSalesOrderSelect.Note) ? q.Note : default(string),
                InvoiceAddress = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceAddress) ? q.InvoiceAddress : default(string),
                InvoiceNationId = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceNation) ? q.InvoiceNationId : default(long?),
                InvoiceProvinceId = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceProvince) ? q.InvoiceProvinceId : default(long?),
                InvoiceDistrictId = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceDistrict) ? q.InvoiceDistrictId : default(long?),
                InvoiceWardId = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceWard) ? q.InvoiceWardId : default(long?),
                InvoiceZIPCode = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceZIPCode) ? q.InvoiceZIPCode : default(string),
                DeliveryAddress = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryAddress) ? q.DeliveryAddress : default(string),
                DeliveryNationId = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryNation) ? q.DeliveryNationId : default(long?),
                DeliveryProvinceId = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryProvince) ? q.DeliveryProvinceId : default(long?),
                DeliveryDistrictId = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryDistrict) ? q.DeliveryDistrictId : default(long?),
                DeliveryWardId = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryWard) ? q.DeliveryWardId : default(long?),
                DeliveryZIPCode = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryZIPCode) ? q.DeliveryZIPCode : default(string),
                SubTotal = filter.Selects.Contains(CustomerSalesOrderSelect.SubTotal) ? q.SubTotal : default(decimal),
                GeneralDiscountPercentage = filter.Selects.Contains(CustomerSalesOrderSelect.GeneralDiscountPercentage) ? q.GeneralDiscountPercentage : default(decimal?),
                GeneralDiscountAmount = filter.Selects.Contains(CustomerSalesOrderSelect.GeneralDiscountAmount) ? q.GeneralDiscountAmount : default(decimal?),
                TotalTaxOther = filter.Selects.Contains(CustomerSalesOrderSelect.TotalTaxOther) ? q.TotalTaxOther : default(decimal),
                TotalTax = filter.Selects.Contains(CustomerSalesOrderSelect.TotalTax) ? q.TotalTax : default(decimal),
                Total = filter.Selects.Contains(CustomerSalesOrderSelect.Total) ? q.Total : default(decimal),
                CreatorId = filter.Selects.Contains(CustomerSalesOrderSelect.Creator) ? q.CreatorId : default(long),
                OrganizationId = filter.Selects.Contains(CustomerSalesOrderSelect.Organization) ? q.OrganizationId : default(long),
                RowId = filter.Selects.Contains(CustomerSalesOrderSelect.Row) ? q.RowId : default(Guid),
                Contract = filter.Selects.Contains(CustomerSalesOrderSelect.Contract) && q.Contract != null ? new Contract
                {
                    Id = q.Contract.Id,
                    Code = q.Contract.Code,
                    Name = q.Contract.Name,
                    TotalValue = q.Contract.TotalValue,
                    ValidityDate = q.Contract.ValidityDate,
                    ExpirationDate = q.Contract.ExpirationDate,
                    DeliveryUnit = q.Contract.DeliveryUnit,
                    InvoiceAddress = q.Contract.InvoiceAddress,
                    InvoiceZipCode = q.Contract.InvoiceZipCode,
                    ReceiveAddress = q.Contract.ReceiveAddress,
                    ReceiveZipCode = q.Contract.ReceiveZipCode,
                    TermAndCondition = q.Contract.TermAndCondition,
                    InvoiceNationId = q.Contract.InvoiceNationId,
                    InvoiceProvinceId = q.Contract.InvoiceProvinceId,
                    InvoiceDistrictId = q.Contract.InvoiceDistrictId,
                    ReceiveNationId = q.Contract.ReceiveNationId,
                    ReceiveProvinceId = q.Contract.ReceiveProvinceId,
                    ReceiveDistrictId = q.Contract.ReceiveDistrictId,
                    ContractTypeId = q.Contract.ContractTypeId,
                    CompanyId = q.Contract.CompanyId,
                    OpportunityId = q.Contract.OpportunityId,
                    OrganizationId = q.Contract.OrganizationId,
                    AppUserId = q.Contract.AppUserId,
                    ContractStatusId = q.Contract.ContractStatusId,
                    CreatorId = q.Contract.CreatorId,
                    CustomerId = q.Contract.CustomerId,
                    CurrencyId = q.Contract.CurrencyId,
                    PaymentStatusId = q.Contract.PaymentStatusId,
                    SubTotal = q.Contract.SubTotal,
                    GeneralDiscountPercentage = q.Contract.GeneralDiscountPercentage,
                    GeneralDiscountAmount = q.Contract.GeneralDiscountAmount,
                    TotalTaxAmountOther = q.Contract.TotalTaxAmountOther,
                    TotalTaxAmount = q.Contract.TotalTaxAmount,
                    Total = q.Contract.Total,
                } : null,
                Creator = filter.Selects.Contains(CustomerSalesOrderSelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                    DisplayName = q.Creator.DisplayName,
                    Address = q.Creator.Address,
                    Email = q.Creator.Email,
                    Phone = q.Creator.Phone,
                    SexId = q.Creator.SexId,
                    Birthday = q.Creator.Birthday,
                    Avatar = q.Creator.Avatar,
                    Department = q.Creator.Department,
                    OrganizationId = q.Creator.OrganizationId,
                    Longitude = q.Creator.Longitude,
                    Latitude = q.Creator.Latitude,
                    StatusId = q.Creator.StatusId,
                    RowId = q.Creator.RowId,
                    Used = q.Creator.Used,
                } : null,
                Customer = filter.Selects.Contains(CustomerSalesOrderSelect.Customer) && q.Customer != null ? new Customer
                {
                    Id = q.Customer.Id,
                    Code = q.Customer.Code,
                    Name = q.Customer.Name,
                    Phone = q.Customer.Phone,
                    Address = q.Customer.Address,
                    NationId = q.Customer.NationId,
                    ProvinceId = q.Customer.ProvinceId,
                    DistrictId = q.Customer.DistrictId,
                    WardId = q.Customer.WardId,
                    CustomerTypeId = q.Customer.CustomerTypeId,
                    Birthday = q.Customer.Birthday,
                    Email = q.Customer.Email,
                    ProfessionId = q.Customer.ProfessionId,
                    CustomerResourceId = q.Customer.CustomerResourceId,
                    SexId = q.Customer.SexId,
                    StatusId = q.Customer.StatusId,
                    CompanyId = q.Customer.CompanyId,
                    ParentCompanyId = q.Customer.ParentCompanyId,
                    TaxCode = q.Customer.TaxCode,
                    Fax = q.Customer.Fax,
                    Website = q.Customer.Website,
                    NumberOfEmployee = q.Customer.NumberOfEmployee,
                    BusinessTypeId = q.Customer.BusinessTypeId,
                    Investment = q.Customer.Investment,
                    RevenueAnnual = q.Customer.RevenueAnnual,
                    IsSupplier = q.Customer.IsSupplier,
                    Descreption = q.Customer.Descreption,
                    Used = q.Customer.Used,
                    RowId = q.Customer.RowId,
                    Company = q.Customer.Company == null ? null : new Company
                    {
                        Id = q.Customer.Company.Id,
                        Name = q.Customer.Company.Name,
                    },
                } : null,
                CustomerType = filter.Selects.Contains(CustomerSalesOrderSelect.CustomerType) && q.CustomerType != null ? new CustomerType
                {
                    Id = q.CustomerType.Id,
                    Code = q.CustomerType.Code,
                    Name = q.CustomerType.Name,
                } : null,
                DeliveryDistrict = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryDistrict) && q.DeliveryDistrict != null ? new District
                {
                    Id = q.DeliveryDistrict.Id,
                    Code = q.DeliveryDistrict.Code,
                    Name = q.DeliveryDistrict.Name,
                    Priority = q.DeliveryDistrict.Priority,
                    ProvinceId = q.DeliveryDistrict.ProvinceId,
                    StatusId = q.DeliveryDistrict.StatusId,
                    RowId = q.DeliveryDistrict.RowId,
                    Used = q.DeliveryDistrict.Used,
                } : null,
                DeliveryNation = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryNation) && q.DeliveryNation != null ? new Nation
                {
                    Id = q.DeliveryNation.Id,
                    Code = q.DeliveryNation.Code,
                    Name = q.DeliveryNation.Name,
                    Priority = q.DeliveryNation.Priority,
                    StatusId = q.DeliveryNation.StatusId,
                    Used = q.DeliveryNation.Used,
                    RowId = q.DeliveryNation.RowId,
                } : null,
                DeliveryProvince = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryProvince) && q.DeliveryProvince != null ? new Province
                {
                    Id = q.DeliveryProvince.Id,
                    Code = q.DeliveryProvince.Code,
                    Name = q.DeliveryProvince.Name,
                    Priority = q.DeliveryProvince.Priority,
                    StatusId = q.DeliveryProvince.StatusId,
                    RowId = q.DeliveryProvince.RowId,
                    Used = q.DeliveryProvince.Used,
                } : null,
                DeliveryWard = filter.Selects.Contains(CustomerSalesOrderSelect.DeliveryWard) && q.DeliveryWard != null ? new Ward
                {
                    Id = q.DeliveryWard.Id,
                    Code = q.DeliveryWard.Code,
                    Name = q.DeliveryWard.Name,
                    Priority = q.DeliveryWard.Priority,
                    DistrictId = q.DeliveryWard.DistrictId,
                    StatusId = q.DeliveryWard.StatusId,
                    RowId = q.DeliveryWard.RowId,
                    Used = q.DeliveryWard.Used,
                } : null,
                EditedPriceStatus = filter.Selects.Contains(CustomerSalesOrderSelect.EditedPriceStatus) && q.EditedPriceStatus != null ? new EditedPriceStatus
                {
                    Id = q.EditedPriceStatus.Id,
                    Code = q.EditedPriceStatus.Code,
                    Name = q.EditedPriceStatus.Name,
                } : null,
                InvoiceDistrict = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceDistrict) && q.InvoiceDistrict != null ? new District
                {
                    Id = q.InvoiceDistrict.Id,
                    Code = q.InvoiceDistrict.Code,
                    Name = q.InvoiceDistrict.Name,
                    Priority = q.InvoiceDistrict.Priority,
                    ProvinceId = q.InvoiceDistrict.ProvinceId,
                    StatusId = q.InvoiceDistrict.StatusId,
                    RowId = q.InvoiceDistrict.RowId,
                    Used = q.InvoiceDistrict.Used,
                } : null,
                InvoiceNation = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceNation) && q.InvoiceNation != null ? new Nation
                {
                    Id = q.InvoiceNation.Id,
                    Code = q.InvoiceNation.Code,
                    Name = q.InvoiceNation.Name,
                    Priority = q.InvoiceNation.Priority,
                    StatusId = q.InvoiceNation.StatusId,
                    Used = q.InvoiceNation.Used,
                    RowId = q.InvoiceNation.RowId,
                } : null,
                InvoiceProvince = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceProvince) && q.InvoiceProvince != null ? new Province
                {
                    Id = q.InvoiceProvince.Id,
                    Code = q.InvoiceProvince.Code,
                    Name = q.InvoiceProvince.Name,
                    Priority = q.InvoiceProvince.Priority,
                    StatusId = q.InvoiceProvince.StatusId,
                    RowId = q.InvoiceProvince.RowId,
                    Used = q.InvoiceProvince.Used,
                } : null,
                InvoiceWard = filter.Selects.Contains(CustomerSalesOrderSelect.InvoiceWard) && q.InvoiceWard != null ? new Ward
                {
                    Id = q.InvoiceWard.Id,
                    Code = q.InvoiceWard.Code,
                    Name = q.InvoiceWard.Name,
                    Priority = q.InvoiceWard.Priority,
                    DistrictId = q.InvoiceWard.DistrictId,
                    StatusId = q.InvoiceWard.StatusId,
                    RowId = q.InvoiceWard.RowId,
                    Used = q.InvoiceWard.Used,
                } : null,
                Opportunity = filter.Selects.Contains(CustomerSalesOrderSelect.Opportunity) && q.Opportunity != null ? new Opportunity
                {
                    Id = q.Opportunity.Id,
                    Name = q.Opportunity.Name,
                    CompanyId = q.Opportunity.CompanyId,
                    CustomerLeadId = q.Opportunity.CustomerLeadId,
                    ClosingDate = q.Opportunity.ClosingDate,
                    SaleStageId = q.Opportunity.SaleStageId,
                    ProbabilityId = q.Opportunity.ProbabilityId,
                    PotentialResultId = q.Opportunity.PotentialResultId,
                    LeadSourceId = q.Opportunity.LeadSourceId,
                    AppUserId = q.Opportunity.AppUserId,
                    CurrencyId = q.Opportunity.CurrencyId,
                    Amount = q.Opportunity.Amount,
                    ForecastAmount = q.Opportunity.ForecastAmount,
                    Description = q.Opportunity.Description,
                    RefuseReciveSMS = q.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = q.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = q.Opportunity.OpportunityResultTypeId,
                    CreatorId = q.Opportunity.CreatorId,
                } : null,
                OrderPaymentStatus = filter.Selects.Contains(CustomerSalesOrderSelect.OrderPaymentStatus) && q.OrderPaymentStatus != null ? new OrderPaymentStatus
                {
                    Id = q.OrderPaymentStatus.Id,
                    Code = q.OrderPaymentStatus.Code,
                    Name = q.OrderPaymentStatus.Name,
                } : null,
                Organization = filter.Selects.Contains(CustomerSalesOrderSelect.Organization) && q.Organization != null ? new Organization
                {
                    Id = q.Organization.Id,
                    Code = q.Organization.Code,
                    Name = q.Organization.Name,
                    ParentId = q.Organization.ParentId,
                    Path = q.Organization.Path,
                    Level = q.Organization.Level,
                    StatusId = q.Organization.StatusId,
                    Phone = q.Organization.Phone,
                    Email = q.Organization.Email,
                    Address = q.Organization.Address,
                    RowId = q.Organization.RowId,
                    Used = q.Organization.Used,
                } : null,
                RequestState = filter.Selects.Contains(CustomerSalesOrderSelect.RequestState) && q.RequestState != null ? new RequestState
                {
                    Id = q.RequestState.Id,
                    Code = q.RequestState.Code,
                    Name = q.RequestState.Name,
                } : null,
                SalesEmployee = filter.Selects.Contains(CustomerSalesOrderSelect.SalesEmployee) && q.SalesEmployee != null ? new AppUser
                {
                    Id = q.SalesEmployee.Id,
                    Username = q.SalesEmployee.Username,
                    DisplayName = q.SalesEmployee.DisplayName,
                    Address = q.SalesEmployee.Address,
                    Email = q.SalesEmployee.Email,
                    Phone = q.SalesEmployee.Phone,
                    SexId = q.SalesEmployee.SexId,
                    Birthday = q.SalesEmployee.Birthday,
                    Avatar = q.SalesEmployee.Avatar,
                    Department = q.SalesEmployee.Department,
                    OrganizationId = q.SalesEmployee.OrganizationId,
                    Longitude = q.SalesEmployee.Longitude,
                    Latitude = q.SalesEmployee.Latitude,
                    StatusId = q.SalesEmployee.StatusId,
                    RowId = q.SalesEmployee.RowId,
                    Used = q.SalesEmployee.Used,
                } : null,
            }).ToListAsync();
            return CustomerSalesOrders;
        }

        public async Task<int> Count(CustomerSalesOrderFilter filter)
        {
            IQueryable<CustomerSalesOrderDAO> CustomerSalesOrders = DataContext.CustomerSalesOrder.AsNoTracking();
            CustomerSalesOrders = DynamicFilter(CustomerSalesOrders, filter);
            return await CustomerSalesOrders.CountAsync();
        }

        public async Task<List<CustomerSalesOrder>> List(CustomerSalesOrderFilter filter)
        {
            if (filter == null) return new List<CustomerSalesOrder>();
            IQueryable<CustomerSalesOrderDAO> CustomerSalesOrderDAOs = DataContext.CustomerSalesOrder.AsNoTracking();
            CustomerSalesOrderDAOs = DynamicFilter(CustomerSalesOrderDAOs, filter);
            CustomerSalesOrderDAOs = DynamicOrder(CustomerSalesOrderDAOs, filter);
            List<CustomerSalesOrder> CustomerSalesOrders = await DynamicSelect(CustomerSalesOrderDAOs, filter);
            return CustomerSalesOrders;
        }

        public async Task<List<CustomerSalesOrder>> List(List<long> Ids)
        {
            List<CustomerSalesOrder> CustomerSalesOrders = await DataContext.CustomerSalesOrder.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerSalesOrder()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                CustomerTypeId = x.CustomerTypeId,
                CustomerId = x.CustomerId,
                OpportunityId = x.OpportunityId,
                ContractId = x.ContractId,
                OrderPaymentStatusId = x.OrderPaymentStatusId,
                RequestStateId = x.RequestStateId,
                EditedPriceStatusId = x.EditedPriceStatusId,
                ShippingName = x.ShippingName,
                OrderDate = x.OrderDate,
                DeliveryDate = x.DeliveryDate,
                SalesEmployeeId = x.SalesEmployeeId,
                Note = x.Note,
                InvoiceAddress = x.InvoiceAddress,
                InvoiceNationId = x.InvoiceNationId,
                InvoiceProvinceId = x.InvoiceProvinceId,
                InvoiceDistrictId = x.InvoiceDistrictId,
                InvoiceWardId = x.InvoiceWardId,
                InvoiceZIPCode = x.InvoiceZIPCode,
                DeliveryAddress = x.DeliveryAddress,
                DeliveryNationId = x.DeliveryNationId,
                DeliveryProvinceId = x.DeliveryProvinceId,
                DeliveryDistrictId = x.DeliveryDistrictId,
                DeliveryWardId = x.DeliveryWardId,
                DeliveryZIPCode = x.DeliveryZIPCode,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TotalTaxOther = x.TotalTaxOther,
                TotalTax = x.TotalTax,
                Total = x.Total,
                CreatorId = x.CreatorId,
                OrganizationId = x.OrganizationId,
                RowId = x.RowId,
                Contract = x.Contract == null ? null : new Contract
                {
                    Id = x.Contract.Id,
                    Code = x.Contract.Code,
                    Name = x.Contract.Name,
                    TotalValue = x.Contract.TotalValue,
                    ValidityDate = x.Contract.ValidityDate,
                    ExpirationDate = x.Contract.ExpirationDate,
                    DeliveryUnit = x.Contract.DeliveryUnit,
                    InvoiceAddress = x.Contract.InvoiceAddress,
                    InvoiceZipCode = x.Contract.InvoiceZipCode,
                    ReceiveAddress = x.Contract.ReceiveAddress,
                    ReceiveZipCode = x.Contract.ReceiveZipCode,
                    TermAndCondition = x.Contract.TermAndCondition,
                    InvoiceNationId = x.Contract.InvoiceNationId,
                    InvoiceProvinceId = x.Contract.InvoiceProvinceId,
                    InvoiceDistrictId = x.Contract.InvoiceDistrictId,
                    ReceiveNationId = x.Contract.ReceiveNationId,
                    ReceiveProvinceId = x.Contract.ReceiveProvinceId,
                    ReceiveDistrictId = x.Contract.ReceiveDistrictId,
                    ContractTypeId = x.Contract.ContractTypeId,
                    CompanyId = x.Contract.CompanyId,
                    OpportunityId = x.Contract.OpportunityId,
                    OrganizationId = x.Contract.OrganizationId,
                    AppUserId = x.Contract.AppUserId,
                    ContractStatusId = x.Contract.ContractStatusId,
                    CreatorId = x.Contract.CreatorId,
                    CustomerId = x.Contract.CustomerId,
                    CurrencyId = x.Contract.CurrencyId,
                    PaymentStatusId = x.Contract.PaymentStatusId,
                    SubTotal = x.Contract.SubTotal,
                    GeneralDiscountPercentage = x.Contract.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.Contract.GeneralDiscountAmount,
                    TotalTaxAmountOther = x.Contract.TotalTaxAmountOther,
                    TotalTaxAmount = x.Contract.TotalTaxAmount,
                    Total = x.Contract.Total,
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                Customer = x.Customer == null ? null : new Customer
                {
                    Id = x.Customer.Id,
                    Code = x.Customer.Code,
                    Name = x.Customer.Name,
                    Phone = x.Customer.Phone,
                    Address = x.Customer.Address,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    CustomerTypeId = x.Customer.CustomerTypeId,
                    Birthday = x.Customer.Birthday,
                    Email = x.Customer.Email,
                    ProfessionId = x.Customer.ProfessionId,
                    CustomerResourceId = x.Customer.CustomerResourceId,
                    SexId = x.Customer.SexId,
                    StatusId = x.Customer.StatusId,
                    CompanyId = x.Customer.CompanyId,
                    ParentCompanyId = x.Customer.ParentCompanyId,
                    TaxCode = x.Customer.TaxCode,
                    Fax = x.Customer.Fax,
                    Website = x.Customer.Website,
                    NumberOfEmployee = x.Customer.NumberOfEmployee,
                    BusinessTypeId = x.Customer.BusinessTypeId,
                    Investment = x.Customer.Investment,
                    RevenueAnnual = x.Customer.RevenueAnnual,
                    IsSupplier = x.Customer.IsSupplier,
                    Descreption = x.Customer.Descreption,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
                },
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Code = x.CustomerType.Code,
                    Name = x.CustomerType.Name,
                },
                DeliveryDistrict = x.DeliveryDistrict == null ? null : new District
                {
                    Id = x.DeliveryDistrict.Id,
                    Code = x.DeliveryDistrict.Code,
                    Name = x.DeliveryDistrict.Name,
                    Priority = x.DeliveryDistrict.Priority,
                    ProvinceId = x.DeliveryDistrict.ProvinceId,
                    StatusId = x.DeliveryDistrict.StatusId,
                    RowId = x.DeliveryDistrict.RowId,
                    Used = x.DeliveryDistrict.Used,
                },
                DeliveryNation = x.DeliveryNation == null ? null : new Nation
                {
                    Id = x.DeliveryNation.Id,
                    Code = x.DeliveryNation.Code,
                    Name = x.DeliveryNation.Name,
                    Priority = x.DeliveryNation.Priority,
                    StatusId = x.DeliveryNation.StatusId,
                    Used = x.DeliveryNation.Used,
                    RowId = x.DeliveryNation.RowId,
                },
                DeliveryProvince = x.DeliveryProvince == null ? null : new Province
                {
                    Id = x.DeliveryProvince.Id,
                    Code = x.DeliveryProvince.Code,
                    Name = x.DeliveryProvince.Name,
                    Priority = x.DeliveryProvince.Priority,
                    StatusId = x.DeliveryProvince.StatusId,
                    RowId = x.DeliveryProvince.RowId,
                    Used = x.DeliveryProvince.Used,
                },
                DeliveryWard = x.DeliveryWard == null ? null : new Ward
                {
                    Id = x.DeliveryWard.Id,
                    Code = x.DeliveryWard.Code,
                    Name = x.DeliveryWard.Name,
                    Priority = x.DeliveryWard.Priority,
                    DistrictId = x.DeliveryWard.DistrictId,
                    StatusId = x.DeliveryWard.StatusId,
                    RowId = x.DeliveryWard.RowId,
                    Used = x.DeliveryWard.Used,
                },
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                InvoiceDistrict = x.InvoiceDistrict == null ? null : new District
                {
                    Id = x.InvoiceDistrict.Id,
                    Code = x.InvoiceDistrict.Code,
                    Name = x.InvoiceDistrict.Name,
                    Priority = x.InvoiceDistrict.Priority,
                    ProvinceId = x.InvoiceDistrict.ProvinceId,
                    StatusId = x.InvoiceDistrict.StatusId,
                    RowId = x.InvoiceDistrict.RowId,
                    Used = x.InvoiceDistrict.Used,
                },
                InvoiceNation = x.InvoiceNation == null ? null : new Nation
                {
                    Id = x.InvoiceNation.Id,
                    Code = x.InvoiceNation.Code,
                    Name = x.InvoiceNation.Name,
                    Priority = x.InvoiceNation.Priority,
                    StatusId = x.InvoiceNation.StatusId,
                    Used = x.InvoiceNation.Used,
                    RowId = x.InvoiceNation.RowId,
                },
                InvoiceProvince = x.InvoiceProvince == null ? null : new Province
                {
                    Id = x.InvoiceProvince.Id,
                    Code = x.InvoiceProvince.Code,
                    Name = x.InvoiceProvince.Name,
                    Priority = x.InvoiceProvince.Priority,
                    StatusId = x.InvoiceProvince.StatusId,
                    RowId = x.InvoiceProvince.RowId,
                    Used = x.InvoiceProvince.Used,
                },
                InvoiceWard = x.InvoiceWard == null ? null : new Ward
                {
                    Id = x.InvoiceWard.Id,
                    Code = x.InvoiceWard.Code,
                    Name = x.InvoiceWard.Name,
                    Priority = x.InvoiceWard.Priority,
                    DistrictId = x.InvoiceWard.DistrictId,
                    StatusId = x.InvoiceWard.StatusId,
                    RowId = x.InvoiceWard.RowId,
                    Used = x.InvoiceWard.Used,
                },
                Opportunity = x.Opportunity == null ? null : new Opportunity
                {
                    Id = x.Opportunity.Id,
                    Name = x.Opportunity.Name,
                    CompanyId = x.Opportunity.CompanyId,
                    CustomerLeadId = x.Opportunity.CustomerLeadId,
                    ClosingDate = x.Opportunity.ClosingDate,
                    SaleStageId = x.Opportunity.SaleStageId,
                    ProbabilityId = x.Opportunity.ProbabilityId,
                    PotentialResultId = x.Opportunity.PotentialResultId,
                    LeadSourceId = x.Opportunity.LeadSourceId,
                    AppUserId = x.Opportunity.AppUserId,
                    CurrencyId = x.Opportunity.CurrencyId,
                    Amount = x.Opportunity.Amount,
                    ForecastAmount = x.Opportunity.ForecastAmount,
                    Description = x.Opportunity.Description,
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                    CreatorId = x.Opportunity.CreatorId,
                },
                OrderPaymentStatus = x.OrderPaymentStatus == null ? null : new OrderPaymentStatus
                {
                    Id = x.OrderPaymentStatus.Id,
                    Code = x.OrderPaymentStatus.Code,
                    Name = x.OrderPaymentStatus.Name,
                },
                Organization = x.Organization == null ? null : new Organization
                {
                    Id = x.Organization.Id,
                    Code = x.Organization.Code,
                    Name = x.Organization.Name,
                    ParentId = x.Organization.ParentId,
                    Path = x.Organization.Path,
                    Level = x.Organization.Level,
                    StatusId = x.Organization.StatusId,
                    Phone = x.Organization.Phone,
                    Email = x.Organization.Email,
                    Address = x.Organization.Address,
                    RowId = x.Organization.RowId,
                    Used = x.Organization.Used,
                },
                RequestState = x.RequestState == null ? null : new RequestState
                {
                    Id = x.RequestState.Id,
                    Code = x.RequestState.Code,
                    Name = x.RequestState.Name,
                },
                SalesEmployee = x.SalesEmployee == null ? null : new AppUser
                {
                    Id = x.SalesEmployee.Id,
                    Username = x.SalesEmployee.Username,
                    DisplayName = x.SalesEmployee.DisplayName,
                    Address = x.SalesEmployee.Address,
                    Email = x.SalesEmployee.Email,
                    Phone = x.SalesEmployee.Phone,
                    SexId = x.SalesEmployee.SexId,
                    Birthday = x.SalesEmployee.Birthday,
                    Avatar = x.SalesEmployee.Avatar,
                    Department = x.SalesEmployee.Department,
                    OrganizationId = x.SalesEmployee.OrganizationId,
                    Longitude = x.SalesEmployee.Longitude,
                    Latitude = x.SalesEmployee.Latitude,
                    StatusId = x.SalesEmployee.StatusId,
                    RowId = x.SalesEmployee.RowId,
                    Used = x.SalesEmployee.Used,
                },
            }).ToListAsync();
            
            List<CustomerSalesOrderContent> CustomerSalesOrderContents = await DataContext.CustomerSalesOrderContent.AsNoTracking()
                .Where(x => Ids.Contains(x.CustomerSalesOrderId))
                .Select(x => new CustomerSalesOrderContent
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
                    EditedPriceStatus = new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = new Item
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
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
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
            foreach(CustomerSalesOrder CustomerSalesOrder in CustomerSalesOrders)
            {
                CustomerSalesOrder.CustomerSalesOrderContents = CustomerSalesOrderContents
                    .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                    .ToList();
            }
            List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories = await DataContext.CustomerSalesOrderPaymentHistory.AsNoTracking()
                .Where(x => Ids.Contains(x.CustomerSalesOrderId))
                .Where(x => x.DeletedAt == null)
                .Select(x => new CustomerSalesOrderPaymentHistory
                {
                    Id = x.Id,
                    CustomerSalesOrderId = x.CustomerSalesOrderId,
                    PaymentMilestone = x.PaymentMilestone,
                    PaymentPercentage = x.PaymentPercentage,
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                }).ToListAsync();
            foreach(CustomerSalesOrder CustomerSalesOrder in CustomerSalesOrders)
            {
                CustomerSalesOrder.CustomerSalesOrderPaymentHistories = CustomerSalesOrderPaymentHistories
                    .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                    .ToList();
            }
            List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions = await DataContext.CustomerSalesOrderPromotion.AsNoTracking()
                .Where(x => Ids.Contains(x.CustomerSalesOrderId))
                .Select(x => new CustomerSalesOrderPromotion
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
                    Item = new Item
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
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
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
            foreach(CustomerSalesOrder CustomerSalesOrder in CustomerSalesOrders)
            {
                CustomerSalesOrder.CustomerSalesOrderPromotions = CustomerSalesOrderPromotions
                    .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                    .ToList();
            }

            return CustomerSalesOrders;
        }

        public async Task<CustomerSalesOrder> Get(long Id)
        {
            CustomerSalesOrder CustomerSalesOrder = await DataContext.CustomerSalesOrder.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerSalesOrder()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                CustomerTypeId = x.CustomerTypeId,
                CustomerId = x.CustomerId,
                OpportunityId = x.OpportunityId,
                ContractId = x.ContractId,
                OrderPaymentStatusId = x.OrderPaymentStatusId,
                RequestStateId = x.RequestStateId,
                EditedPriceStatusId = x.EditedPriceStatusId,
                ShippingName = x.ShippingName,
                OrderDate = x.OrderDate,
                DeliveryDate = x.DeliveryDate,
                SalesEmployeeId = x.SalesEmployeeId,
                Note = x.Note,
                InvoiceAddress = x.InvoiceAddress,
                InvoiceNationId = x.InvoiceNationId,
                InvoiceProvinceId = x.InvoiceProvinceId,
                InvoiceDistrictId = x.InvoiceDistrictId,
                InvoiceWardId = x.InvoiceWardId,
                InvoiceZIPCode = x.InvoiceZIPCode,
                DeliveryAddress = x.DeliveryAddress,
                DeliveryNationId = x.DeliveryNationId,
                DeliveryProvinceId = x.DeliveryProvinceId,
                DeliveryDistrictId = x.DeliveryDistrictId,
                DeliveryWardId = x.DeliveryWardId,
                DeliveryZIPCode = x.DeliveryZIPCode,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TotalTaxOther = x.TotalTaxOther,
                TotalTax = x.TotalTax,
                Total = x.Total,
                CreatorId = x.CreatorId,
                OrganizationId = x.OrganizationId,
                RowId = x.RowId,
                Contract = x.Contract == null ? null : new Contract
                {
                    Id = x.Contract.Id,
                    Code = x.Contract.Code,
                    Name = x.Contract.Name,
                    TotalValue = x.Contract.TotalValue,
                    ValidityDate = x.Contract.ValidityDate,
                    ExpirationDate = x.Contract.ExpirationDate,
                    DeliveryUnit = x.Contract.DeliveryUnit,
                    InvoiceAddress = x.Contract.InvoiceAddress,
                    InvoiceZipCode = x.Contract.InvoiceZipCode,
                    ReceiveAddress = x.Contract.ReceiveAddress,
                    ReceiveZipCode = x.Contract.ReceiveZipCode,
                    TermAndCondition = x.Contract.TermAndCondition,
                    InvoiceNationId = x.Contract.InvoiceNationId,
                    InvoiceProvinceId = x.Contract.InvoiceProvinceId,
                    InvoiceDistrictId = x.Contract.InvoiceDistrictId,
                    ReceiveNationId = x.Contract.ReceiveNationId,
                    ReceiveProvinceId = x.Contract.ReceiveProvinceId,
                    ReceiveDistrictId = x.Contract.ReceiveDistrictId,
                    ContractTypeId = x.Contract.ContractTypeId,
                    CompanyId = x.Contract.CompanyId,
                    OpportunityId = x.Contract.OpportunityId,
                    OrganizationId = x.Contract.OrganizationId,
                    AppUserId = x.Contract.AppUserId,
                    ContractStatusId = x.Contract.ContractStatusId,
                    CreatorId = x.Contract.CreatorId,
                    CustomerId = x.Contract.CustomerId,
                    CurrencyId = x.Contract.CurrencyId,
                    PaymentStatusId = x.Contract.PaymentStatusId,
                    SubTotal = x.Contract.SubTotal,
                    GeneralDiscountPercentage = x.Contract.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.Contract.GeneralDiscountAmount,
                    TotalTaxAmountOther = x.Contract.TotalTaxAmountOther,
                    TotalTaxAmount = x.Contract.TotalTaxAmount,
                    Total = x.Contract.Total,
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                Customer = x.Customer == null ? null : new Customer
                {
                    Id = x.Customer.Id,
                    Code = x.Customer.Code,
                    Name = x.Customer.Name,
                    Phone = x.Customer.Phone,
                    Address = x.Customer.Address,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    CustomerTypeId = x.Customer.CustomerTypeId,
                    Birthday = x.Customer.Birthday,
                    Email = x.Customer.Email,
                    ProfessionId = x.Customer.ProfessionId,
                    CustomerResourceId = x.Customer.CustomerResourceId,
                    SexId = x.Customer.SexId,
                    StatusId = x.Customer.StatusId,
                    CompanyId = x.Customer.CompanyId,
                    ParentCompanyId = x.Customer.ParentCompanyId,
                    TaxCode = x.Customer.TaxCode,
                    Fax = x.Customer.Fax,
                    Website = x.Customer.Website,
                    NumberOfEmployee = x.Customer.NumberOfEmployee,
                    BusinessTypeId = x.Customer.BusinessTypeId,
                    Investment = x.Customer.Investment,
                    RevenueAnnual = x.Customer.RevenueAnnual,
                    IsSupplier = x.Customer.IsSupplier,
                    Descreption = x.Customer.Descreption,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
                    Company = x.Customer.Company == null ? null : new Company
                    {
                        Id = x.Customer.Company.Id,
                        Name = x.Customer.Company.Name,
                    }
                },
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Code = x.CustomerType.Code,
                    Name = x.CustomerType.Name,
                },
                DeliveryDistrict = x.DeliveryDistrict == null ? null : new District
                {
                    Id = x.DeliveryDistrict.Id,
                    Code = x.DeliveryDistrict.Code,
                    Name = x.DeliveryDistrict.Name,
                    Priority = x.DeliveryDistrict.Priority,
                    ProvinceId = x.DeliveryDistrict.ProvinceId,
                    StatusId = x.DeliveryDistrict.StatusId,
                    RowId = x.DeliveryDistrict.RowId,
                    Used = x.DeliveryDistrict.Used,
                },
                DeliveryNation = x.DeliveryNation == null ? null : new Nation
                {
                    Id = x.DeliveryNation.Id,
                    Code = x.DeliveryNation.Code,
                    Name = x.DeliveryNation.Name,
                    Priority = x.DeliveryNation.Priority,
                    StatusId = x.DeliveryNation.StatusId,
                    Used = x.DeliveryNation.Used,
                    RowId = x.DeliveryNation.RowId,
                },
                DeliveryProvince = x.DeliveryProvince == null ? null : new Province
                {
                    Id = x.DeliveryProvince.Id,
                    Code = x.DeliveryProvince.Code,
                    Name = x.DeliveryProvince.Name,
                    Priority = x.DeliveryProvince.Priority,
                    StatusId = x.DeliveryProvince.StatusId,
                    RowId = x.DeliveryProvince.RowId,
                    Used = x.DeliveryProvince.Used,
                },
                DeliveryWard = x.DeliveryWard == null ? null : new Ward
                {
                    Id = x.DeliveryWard.Id,
                    Code = x.DeliveryWard.Code,
                    Name = x.DeliveryWard.Name,
                    Priority = x.DeliveryWard.Priority,
                    DistrictId = x.DeliveryWard.DistrictId,
                    StatusId = x.DeliveryWard.StatusId,
                    RowId = x.DeliveryWard.RowId,
                    Used = x.DeliveryWard.Used,
                },
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                InvoiceDistrict = x.InvoiceDistrict == null ? null : new District
                {
                    Id = x.InvoiceDistrict.Id,
                    Code = x.InvoiceDistrict.Code,
                    Name = x.InvoiceDistrict.Name,
                    Priority = x.InvoiceDistrict.Priority,
                    ProvinceId = x.InvoiceDistrict.ProvinceId,
                    StatusId = x.InvoiceDistrict.StatusId,
                    RowId = x.InvoiceDistrict.RowId,
                    Used = x.InvoiceDistrict.Used,
                },
                InvoiceNation = x.InvoiceNation == null ? null : new Nation
                {
                    Id = x.InvoiceNation.Id,
                    Code = x.InvoiceNation.Code,
                    Name = x.InvoiceNation.Name,
                    Priority = x.InvoiceNation.Priority,
                    StatusId = x.InvoiceNation.StatusId,
                    Used = x.InvoiceNation.Used,
                    RowId = x.InvoiceNation.RowId,
                },
                InvoiceProvince = x.InvoiceProvince == null ? null : new Province
                {
                    Id = x.InvoiceProvince.Id,
                    Code = x.InvoiceProvince.Code,
                    Name = x.InvoiceProvince.Name,
                    Priority = x.InvoiceProvince.Priority,
                    StatusId = x.InvoiceProvince.StatusId,
                    RowId = x.InvoiceProvince.RowId,
                    Used = x.InvoiceProvince.Used,
                },
                InvoiceWard = x.InvoiceWard == null ? null : new Ward
                {
                    Id = x.InvoiceWard.Id,
                    Code = x.InvoiceWard.Code,
                    Name = x.InvoiceWard.Name,
                    Priority = x.InvoiceWard.Priority,
                    DistrictId = x.InvoiceWard.DistrictId,
                    StatusId = x.InvoiceWard.StatusId,
                    RowId = x.InvoiceWard.RowId,
                    Used = x.InvoiceWard.Used,
                },
                Opportunity = x.Opportunity == null ? null : new Opportunity
                {
                    Id = x.Opportunity.Id,
                    Name = x.Opportunity.Name,
                    CompanyId = x.Opportunity.CompanyId,
                    CustomerLeadId = x.Opportunity.CustomerLeadId,
                    ClosingDate = x.Opportunity.ClosingDate,
                    SaleStageId = x.Opportunity.SaleStageId,
                    ProbabilityId = x.Opportunity.ProbabilityId,
                    PotentialResultId = x.Opportunity.PotentialResultId,
                    LeadSourceId = x.Opportunity.LeadSourceId,
                    AppUserId = x.Opportunity.AppUserId,
                    CurrencyId = x.Opportunity.CurrencyId,
                    Amount = x.Opportunity.Amount,
                    ForecastAmount = x.Opportunity.ForecastAmount,
                    Description = x.Opportunity.Description,
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                    CreatorId = x.Opportunity.CreatorId,
                },
                OrderPaymentStatus = x.OrderPaymentStatus == null ? null : new OrderPaymentStatus
                {
                    Id = x.OrderPaymentStatus.Id,
                    Code = x.OrderPaymentStatus.Code,
                    Name = x.OrderPaymentStatus.Name,
                },
                Organization = x.Organization == null ? null : new Organization
                {
                    Id = x.Organization.Id,
                    Code = x.Organization.Code,
                    Name = x.Organization.Name,
                    ParentId = x.Organization.ParentId,
                    Path = x.Organization.Path,
                    Level = x.Organization.Level,
                    StatusId = x.Organization.StatusId,
                    Phone = x.Organization.Phone,
                    Email = x.Organization.Email,
                    Address = x.Organization.Address,
                    RowId = x.Organization.RowId,
                    Used = x.Organization.Used,
                },
                RequestState = x.RequestState == null ? null : new RequestState
                {
                    Id = x.RequestState.Id,
                    Code = x.RequestState.Code,
                    Name = x.RequestState.Name,
                },
                SalesEmployee = x.SalesEmployee == null ? null : new AppUser
                {
                    Id = x.SalesEmployee.Id,
                    Username = x.SalesEmployee.Username,
                    DisplayName = x.SalesEmployee.DisplayName,
                    Address = x.SalesEmployee.Address,
                    Email = x.SalesEmployee.Email,
                    Phone = x.SalesEmployee.Phone,
                    SexId = x.SalesEmployee.SexId,
                    Birthday = x.SalesEmployee.Birthday,
                    Avatar = x.SalesEmployee.Avatar,
                    Department = x.SalesEmployee.Department,
                    OrganizationId = x.SalesEmployee.OrganizationId,
                    Longitude = x.SalesEmployee.Longitude,
                    Latitude = x.SalesEmployee.Latitude,
                    StatusId = x.SalesEmployee.StatusId,
                    RowId = x.SalesEmployee.RowId,
                    Used = x.SalesEmployee.Used,
                },
            }).FirstOrDefaultAsync();

            if (CustomerSalesOrder == null)
                return null;
            CustomerSalesOrder.CustomerSalesOrderContents = await DataContext.CustomerSalesOrderContent.AsNoTracking()
                .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                .Select(x => new CustomerSalesOrderContent
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
                    EditedPriceStatus = new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = new Item
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
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    TaxType = new TaxType
                    {
                        Id = x.TaxType.Id,
                        Code = x.TaxType.Code,
                        Name = x.TaxType.Name,
                    },
                    UnitOfMeasure = new UnitOfMeasure
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
            CustomerSalesOrder.CustomerSalesOrderPaymentHistories = await DataContext.CustomerSalesOrderPaymentHistory.AsNoTracking()
                .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new CustomerSalesOrderPaymentHistory
                {
                    Id = x.Id,
                    CustomerSalesOrderId = x.CustomerSalesOrderId,
                    PaymentMilestone = x.PaymentMilestone,
                    PaymentPercentage = x.PaymentPercentage,
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                }).ToListAsync();
            CustomerSalesOrder.CustomerSalesOrderPromotions = await DataContext.CustomerSalesOrderPromotion.AsNoTracking()
                .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                .Select(x => new CustomerSalesOrderPromotion
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
                    Item = new Item
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
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
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

            return CustomerSalesOrder;
        }
        public async Task<bool> Create(CustomerSalesOrder CustomerSalesOrder)
        {
            CustomerSalesOrderDAO CustomerSalesOrderDAO = new CustomerSalesOrderDAO();
            CustomerSalesOrderDAO.Id = CustomerSalesOrder.Id;
            CustomerSalesOrderDAO.Code = CustomerSalesOrder.Code;
            CustomerSalesOrderDAO.CustomerTypeId = CustomerSalesOrder.CustomerTypeId;
            CustomerSalesOrderDAO.CustomerId = CustomerSalesOrder.CustomerId;
            CustomerSalesOrderDAO.OpportunityId = CustomerSalesOrder.OpportunityId;
            CustomerSalesOrderDAO.ContractId = CustomerSalesOrder.ContractId;
            CustomerSalesOrderDAO.OrderPaymentStatusId = CustomerSalesOrder.OrderPaymentStatusId;
            CustomerSalesOrderDAO.RequestStateId = CustomerSalesOrder.RequestStateId;
            CustomerSalesOrderDAO.EditedPriceStatusId = CustomerSalesOrder.EditedPriceStatusId;
            CustomerSalesOrderDAO.ShippingName = CustomerSalesOrder.ShippingName;
            CustomerSalesOrderDAO.OrderDate = CustomerSalesOrder.OrderDate;
            CustomerSalesOrderDAO.DeliveryDate = CustomerSalesOrder.DeliveryDate;
            CustomerSalesOrderDAO.SalesEmployeeId = CustomerSalesOrder.SalesEmployeeId;
            CustomerSalesOrderDAO.Note = CustomerSalesOrder.Note;
            CustomerSalesOrderDAO.InvoiceAddress = CustomerSalesOrder.InvoiceAddress;
            CustomerSalesOrderDAO.InvoiceNationId = CustomerSalesOrder.InvoiceNationId;
            CustomerSalesOrderDAO.InvoiceProvinceId = CustomerSalesOrder.InvoiceProvinceId;
            CustomerSalesOrderDAO.InvoiceDistrictId = CustomerSalesOrder.InvoiceDistrictId;
            CustomerSalesOrderDAO.InvoiceWardId = CustomerSalesOrder.InvoiceWardId;
            CustomerSalesOrderDAO.InvoiceZIPCode = CustomerSalesOrder.InvoiceZIPCode;
            CustomerSalesOrderDAO.DeliveryAddress = CustomerSalesOrder.DeliveryAddress;
            CustomerSalesOrderDAO.DeliveryNationId = CustomerSalesOrder.DeliveryNationId;
            CustomerSalesOrderDAO.DeliveryProvinceId = CustomerSalesOrder.DeliveryProvinceId;
            CustomerSalesOrderDAO.DeliveryDistrictId = CustomerSalesOrder.DeliveryDistrictId;
            CustomerSalesOrderDAO.DeliveryWardId = CustomerSalesOrder.DeliveryWardId;
            CustomerSalesOrderDAO.DeliveryZIPCode = CustomerSalesOrder.DeliveryZIPCode;
            CustomerSalesOrderDAO.SubTotal = CustomerSalesOrder.SubTotal;
            CustomerSalesOrderDAO.GeneralDiscountPercentage = CustomerSalesOrder.GeneralDiscountPercentage;
            CustomerSalesOrderDAO.GeneralDiscountAmount = CustomerSalesOrder.GeneralDiscountAmount;
            CustomerSalesOrderDAO.TotalTaxOther = CustomerSalesOrder.TotalTaxOther;
            CustomerSalesOrderDAO.TotalTax = CustomerSalesOrder.TotalTax;
            CustomerSalesOrderDAO.Total = CustomerSalesOrder.Total;
            CustomerSalesOrderDAO.CreatorId = CustomerSalesOrder.CreatorId;
            CustomerSalesOrderDAO.OrganizationId = CustomerSalesOrder.OrganizationId;
            CustomerSalesOrderDAO.RowId = CustomerSalesOrder.RowId;
            CustomerSalesOrderDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerSalesOrderDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerSalesOrder.Add(CustomerSalesOrderDAO);
            await DataContext.SaveChangesAsync();
            CustomerSalesOrder.Id = CustomerSalesOrderDAO.Id;
            await SaveReference(CustomerSalesOrder);
            return true;
        }

        public async Task<bool> Update(CustomerSalesOrder CustomerSalesOrder)
        {
            CustomerSalesOrderDAO CustomerSalesOrderDAO = DataContext.CustomerSalesOrder.Where(x => x.Id == CustomerSalesOrder.Id).FirstOrDefault();
            if (CustomerSalesOrderDAO == null)
                return false;
            CustomerSalesOrderDAO.Id = CustomerSalesOrder.Id;
            CustomerSalesOrderDAO.Code = CustomerSalesOrder.Code;
            CustomerSalesOrderDAO.CustomerTypeId = CustomerSalesOrder.CustomerTypeId;
            CustomerSalesOrderDAO.CustomerId = CustomerSalesOrder.CustomerId;
            CustomerSalesOrderDAO.OpportunityId = CustomerSalesOrder.OpportunityId;
            CustomerSalesOrderDAO.ContractId = CustomerSalesOrder.ContractId;
            CustomerSalesOrderDAO.OrderPaymentStatusId = CustomerSalesOrder.OrderPaymentStatusId;
            CustomerSalesOrderDAO.RequestStateId = CustomerSalesOrder.RequestStateId;
            CustomerSalesOrderDAO.EditedPriceStatusId = CustomerSalesOrder.EditedPriceStatusId;
            CustomerSalesOrderDAO.ShippingName = CustomerSalesOrder.ShippingName;
            CustomerSalesOrderDAO.OrderDate = CustomerSalesOrder.OrderDate;
            CustomerSalesOrderDAO.DeliveryDate = CustomerSalesOrder.DeliveryDate;
            CustomerSalesOrderDAO.SalesEmployeeId = CustomerSalesOrder.SalesEmployeeId;
            CustomerSalesOrderDAO.Note = CustomerSalesOrder.Note;
            CustomerSalesOrderDAO.InvoiceAddress = CustomerSalesOrder.InvoiceAddress;
            CustomerSalesOrderDAO.InvoiceNationId = CustomerSalesOrder.InvoiceNationId;
            CustomerSalesOrderDAO.InvoiceProvinceId = CustomerSalesOrder.InvoiceProvinceId;
            CustomerSalesOrderDAO.InvoiceDistrictId = CustomerSalesOrder.InvoiceDistrictId;
            CustomerSalesOrderDAO.InvoiceWardId = CustomerSalesOrder.InvoiceWardId;
            CustomerSalesOrderDAO.InvoiceZIPCode = CustomerSalesOrder.InvoiceZIPCode;
            CustomerSalesOrderDAO.DeliveryAddress = CustomerSalesOrder.DeliveryAddress;
            CustomerSalesOrderDAO.DeliveryNationId = CustomerSalesOrder.DeliveryNationId;
            CustomerSalesOrderDAO.DeliveryProvinceId = CustomerSalesOrder.DeliveryProvinceId;
            CustomerSalesOrderDAO.DeliveryDistrictId = CustomerSalesOrder.DeliveryDistrictId;
            CustomerSalesOrderDAO.DeliveryWardId = CustomerSalesOrder.DeliveryWardId;
            CustomerSalesOrderDAO.DeliveryZIPCode = CustomerSalesOrder.DeliveryZIPCode;
            CustomerSalesOrderDAO.SubTotal = CustomerSalesOrder.SubTotal;
            CustomerSalesOrderDAO.GeneralDiscountPercentage = CustomerSalesOrder.GeneralDiscountPercentage;
            CustomerSalesOrderDAO.GeneralDiscountAmount = CustomerSalesOrder.GeneralDiscountAmount;
            CustomerSalesOrderDAO.TotalTaxOther = CustomerSalesOrder.TotalTaxOther;
            CustomerSalesOrderDAO.TotalTax = CustomerSalesOrder.TotalTax;
            CustomerSalesOrderDAO.Total = CustomerSalesOrder.Total;
            CustomerSalesOrderDAO.CreatorId = CustomerSalesOrder.CreatorId;
            CustomerSalesOrderDAO.OrganizationId = CustomerSalesOrder.OrganizationId;
            CustomerSalesOrderDAO.RowId = CustomerSalesOrder.RowId;
            CustomerSalesOrderDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerSalesOrder);
            return true;
        }

        public async Task<bool> Delete(CustomerSalesOrder CustomerSalesOrder)
        {
            await DataContext.CustomerSalesOrder.Where(x => x.Id == CustomerSalesOrder.Id).UpdateFromQueryAsync(x => new CustomerSalesOrderDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerSalesOrder> CustomerSalesOrders)
        {
            List<CustomerSalesOrderDAO> CustomerSalesOrderDAOs = new List<CustomerSalesOrderDAO>();
            foreach (CustomerSalesOrder CustomerSalesOrder in CustomerSalesOrders)
            {
                CustomerSalesOrderDAO CustomerSalesOrderDAO = new CustomerSalesOrderDAO();
                CustomerSalesOrderDAO.Id = CustomerSalesOrder.Id;
                CustomerSalesOrderDAO.Code = CustomerSalesOrder.Code;
                CustomerSalesOrderDAO.CustomerTypeId = CustomerSalesOrder.CustomerTypeId;
                CustomerSalesOrderDAO.CustomerId = CustomerSalesOrder.CustomerId;
                CustomerSalesOrderDAO.OpportunityId = CustomerSalesOrder.OpportunityId;
                CustomerSalesOrderDAO.ContractId = CustomerSalesOrder.ContractId;
                CustomerSalesOrderDAO.OrderPaymentStatusId = CustomerSalesOrder.OrderPaymentStatusId;
                CustomerSalesOrderDAO.RequestStateId = CustomerSalesOrder.RequestStateId;
                CustomerSalesOrderDAO.EditedPriceStatusId = CustomerSalesOrder.EditedPriceStatusId;
                CustomerSalesOrderDAO.ShippingName = CustomerSalesOrder.ShippingName;
                CustomerSalesOrderDAO.OrderDate = CustomerSalesOrder.OrderDate;
                CustomerSalesOrderDAO.DeliveryDate = CustomerSalesOrder.DeliveryDate;
                CustomerSalesOrderDAO.SalesEmployeeId = CustomerSalesOrder.SalesEmployeeId;
                CustomerSalesOrderDAO.Note = CustomerSalesOrder.Note;
                CustomerSalesOrderDAO.InvoiceAddress = CustomerSalesOrder.InvoiceAddress;
                CustomerSalesOrderDAO.InvoiceNationId = CustomerSalesOrder.InvoiceNationId;
                CustomerSalesOrderDAO.InvoiceProvinceId = CustomerSalesOrder.InvoiceProvinceId;
                CustomerSalesOrderDAO.InvoiceDistrictId = CustomerSalesOrder.InvoiceDistrictId;
                CustomerSalesOrderDAO.InvoiceWardId = CustomerSalesOrder.InvoiceWardId;
                CustomerSalesOrderDAO.InvoiceZIPCode = CustomerSalesOrder.InvoiceZIPCode;
                CustomerSalesOrderDAO.DeliveryAddress = CustomerSalesOrder.DeliveryAddress;
                CustomerSalesOrderDAO.DeliveryNationId = CustomerSalesOrder.DeliveryNationId;
                CustomerSalesOrderDAO.DeliveryProvinceId = CustomerSalesOrder.DeliveryProvinceId;
                CustomerSalesOrderDAO.DeliveryDistrictId = CustomerSalesOrder.DeliveryDistrictId;
                CustomerSalesOrderDAO.DeliveryWardId = CustomerSalesOrder.DeliveryWardId;
                CustomerSalesOrderDAO.DeliveryZIPCode = CustomerSalesOrder.DeliveryZIPCode;
                CustomerSalesOrderDAO.SubTotal = CustomerSalesOrder.SubTotal;
                CustomerSalesOrderDAO.GeneralDiscountPercentage = CustomerSalesOrder.GeneralDiscountPercentage;
                CustomerSalesOrderDAO.GeneralDiscountAmount = CustomerSalesOrder.GeneralDiscountAmount;
                CustomerSalesOrderDAO.TotalTaxOther = CustomerSalesOrder.TotalTaxOther;
                CustomerSalesOrderDAO.TotalTax = CustomerSalesOrder.TotalTax;
                CustomerSalesOrderDAO.Total = CustomerSalesOrder.Total;
                CustomerSalesOrderDAO.CreatorId = CustomerSalesOrder.CreatorId;
                CustomerSalesOrderDAO.OrganizationId = CustomerSalesOrder.OrganizationId;
                CustomerSalesOrderDAO.RowId = CustomerSalesOrder.RowId;
                CustomerSalesOrderDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerSalesOrderDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerSalesOrderDAOs.Add(CustomerSalesOrderDAO);
            }
            await DataContext.BulkMergeAsync(CustomerSalesOrderDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerSalesOrder> CustomerSalesOrders)
        {
            List<long> Ids = CustomerSalesOrders.Select(x => x.Id).ToList();
            await DataContext.CustomerSalesOrder
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerSalesOrderDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerSalesOrder CustomerSalesOrder)
        {
            await DataContext.CustomerSalesOrderContent
                .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                .DeleteFromQueryAsync();
            List<CustomerSalesOrderContentDAO> CustomerSalesOrderContentDAOs = new List<CustomerSalesOrderContentDAO>();
            if (CustomerSalesOrder.CustomerSalesOrderContents != null)
            {
                foreach (CustomerSalesOrderContent CustomerSalesOrderContent in CustomerSalesOrder.CustomerSalesOrderContents)
                {
                    CustomerSalesOrderContentDAO CustomerSalesOrderContentDAO = new CustomerSalesOrderContentDAO();
                    CustomerSalesOrderContentDAO.Id = CustomerSalesOrderContent.Id;
                    CustomerSalesOrderContentDAO.CustomerSalesOrderId = CustomerSalesOrder.Id;
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
                await DataContext.CustomerSalesOrderContent.BulkMergeAsync(CustomerSalesOrderContentDAOs);
            }
            List<CustomerSalesOrderPaymentHistoryDAO> CustomerSalesOrderPaymentHistoryDAOs = await DataContext.CustomerSalesOrderPaymentHistory
                .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id).ToListAsync();
            CustomerSalesOrderPaymentHistoryDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            if (CustomerSalesOrder.CustomerSalesOrderPaymentHistories != null)
            {
                foreach (CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory in CustomerSalesOrder.CustomerSalesOrderPaymentHistories)
                {
                    CustomerSalesOrderPaymentHistoryDAO CustomerSalesOrderPaymentHistoryDAO = CustomerSalesOrderPaymentHistoryDAOs
                        .Where(x => x.Id == CustomerSalesOrderPaymentHistory.Id && x.Id != 0).FirstOrDefault();
                    if (CustomerSalesOrderPaymentHistoryDAO == null)
                    {
                        CustomerSalesOrderPaymentHistoryDAO = new CustomerSalesOrderPaymentHistoryDAO();
                        CustomerSalesOrderPaymentHistoryDAO.Id = CustomerSalesOrderPaymentHistory.Id;
                        CustomerSalesOrderPaymentHistoryDAO.CustomerSalesOrderId = CustomerSalesOrder.Id;
                        CustomerSalesOrderPaymentHistoryDAO.PaymentMilestone = CustomerSalesOrderPaymentHistory.PaymentMilestone;
                        CustomerSalesOrderPaymentHistoryDAO.PaymentPercentage = CustomerSalesOrderPaymentHistory.PaymentPercentage;
                        CustomerSalesOrderPaymentHistoryDAO.PaymentAmount = CustomerSalesOrderPaymentHistory.PaymentAmount;
                        CustomerSalesOrderPaymentHistoryDAO.Description = CustomerSalesOrderPaymentHistory.Description;
                        CustomerSalesOrderPaymentHistoryDAO.IsPaid = CustomerSalesOrderPaymentHistory.IsPaid;
                        CustomerSalesOrderPaymentHistoryDAOs.Add(CustomerSalesOrderPaymentHistoryDAO);
                        CustomerSalesOrderPaymentHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                        CustomerSalesOrderPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                        CustomerSalesOrderPaymentHistoryDAO.DeletedAt = null;
                    }
                    else
                    {
                        CustomerSalesOrderPaymentHistoryDAO.Id = CustomerSalesOrderPaymentHistory.Id;
                        CustomerSalesOrderPaymentHistoryDAO.CustomerSalesOrderId = CustomerSalesOrder.Id;
                        CustomerSalesOrderPaymentHistoryDAO.PaymentMilestone = CustomerSalesOrderPaymentHistory.PaymentMilestone;
                        CustomerSalesOrderPaymentHistoryDAO.PaymentPercentage = CustomerSalesOrderPaymentHistory.PaymentPercentage;
                        CustomerSalesOrderPaymentHistoryDAO.PaymentAmount = CustomerSalesOrderPaymentHistory.PaymentAmount;
                        CustomerSalesOrderPaymentHistoryDAO.Description = CustomerSalesOrderPaymentHistory.Description;
                        CustomerSalesOrderPaymentHistoryDAO.IsPaid = CustomerSalesOrderPaymentHistory.IsPaid;
                        CustomerSalesOrderPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                        CustomerSalesOrderPaymentHistoryDAO.DeletedAt = null;
                    }
                }
                await DataContext.CustomerSalesOrderPaymentHistory.BulkMergeAsync(CustomerSalesOrderPaymentHistoryDAOs);
            }
            await DataContext.CustomerSalesOrderPromotion
                .Where(x => x.CustomerSalesOrderId == CustomerSalesOrder.Id)
                .DeleteFromQueryAsync();
            List<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotionDAOs = new List<CustomerSalesOrderPromotionDAO>();
            if (CustomerSalesOrder.CustomerSalesOrderPromotions != null)
            {
                foreach (CustomerSalesOrderPromotion CustomerSalesOrderPromotion in CustomerSalesOrder.CustomerSalesOrderPromotions)
                {
                    CustomerSalesOrderPromotionDAO CustomerSalesOrderPromotionDAO = new CustomerSalesOrderPromotionDAO();
                    CustomerSalesOrderPromotionDAO.Id = CustomerSalesOrderPromotion.Id;
                    CustomerSalesOrderPromotionDAO.CustomerSalesOrderId = CustomerSalesOrder.Id;
                    CustomerSalesOrderPromotionDAO.ItemId = CustomerSalesOrderPromotion.ItemId;
                    CustomerSalesOrderPromotionDAO.UnitOfMeasureId = CustomerSalesOrderPromotion.UnitOfMeasureId;
                    CustomerSalesOrderPromotionDAO.Quantity = CustomerSalesOrderPromotion.Quantity;
                    CustomerSalesOrderPromotionDAO.RequestedQuantity = CustomerSalesOrderPromotion.RequestedQuantity;
                    CustomerSalesOrderPromotionDAO.PrimaryUnitOfMeasureId = CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId;
                    CustomerSalesOrderPromotionDAO.Factor = CustomerSalesOrderPromotion.Factor;
                    CustomerSalesOrderPromotionDAO.Note = CustomerSalesOrderPromotion.Note;
                    CustomerSalesOrderPromotionDAOs.Add(CustomerSalesOrderPromotionDAO);
                }
                await DataContext.CustomerSalesOrderPromotion.BulkMergeAsync(CustomerSalesOrderPromotionDAOs);
            }
        }
        
    }
}
