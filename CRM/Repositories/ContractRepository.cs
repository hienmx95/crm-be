using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Enums;

namespace CRM.Repositories
{
    public interface IContractRepository
    {
        Task<int> Count(ContractFilter ContractFilter);
        Task<List<Contract>> List(ContractFilter ContractFilter);
        Task<Contract> Get(long Id);
        Task<bool> Create(Contract Contract);
        Task<bool> Update(Contract Contract);
        Task<bool> Delete(Contract Contract);
        Task<bool> BulkMerge(List<Contract> Contracts);
        Task<bool> BulkDelete(List<Contract> Contracts);
    }
    public class ContractRepository : IContractRepository
    {
        private DataContext DataContext;
        public ContractRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContractDAO> DynamicFilter(IQueryable<ContractDAO> query, ContractFilter filter)
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
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, filter.CompanyId);
            if (filter.OpportunityId != null && filter.OpportunityId.HasValue)
                query = query.Where(q => q.OpportunityId.HasValue).Where(q => q.OpportunityId, filter.OpportunityId);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.ContractTypeId != null && filter.ContractTypeId.HasValue)
                query = query.Where(q => q.ContractTypeId, filter.ContractTypeId);
            if (filter.TotalValue != null && filter.TotalValue.HasValue)
                query = query.Where(q => q.TotalValue, filter.TotalValue);
            if (filter.CurrencyId != null && filter.CurrencyId.HasValue)
                query = query.Where(q => q.CurrencyId, filter.CurrencyId);
            if (filter.ValidityDate != null && filter.ValidityDate.HasValue)
                query = query.Where(q => q.ValidityDate, filter.ValidityDate);
            if (filter.ExpirationDate != null && filter.ExpirationDate.HasValue)
                query = query.Where(q => q.ExpirationDate, filter.ExpirationDate);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.DeliveryUnit != null && filter.DeliveryUnit.HasValue)
                query = query.Where(q => q.DeliveryUnit, filter.DeliveryUnit);
            if (filter.ContractStatusId != null && filter.ContractStatusId.HasValue)
                query = query.Where(q => q.ContractStatusId, filter.ContractStatusId);
            if (filter.PaymentStatusId != null && filter.PaymentStatusId.HasValue)
                query = query.Where(q => q.PaymentStatusId, filter.PaymentStatusId);
            if (filter.InvoiceAddress != null && filter.InvoiceAddress.HasValue)
                query = query.Where(q => q.InvoiceAddress, filter.InvoiceAddress);
            if (filter.InvoiceNationId != null && filter.InvoiceNationId.HasValue)
                query = query.Where(q => q.InvoiceNationId.HasValue).Where(q => q.InvoiceNationId, filter.InvoiceNationId);
            if (filter.InvoiceProvinceId != null && filter.InvoiceProvinceId.HasValue)
                query = query.Where(q => q.InvoiceProvinceId.HasValue).Where(q => q.InvoiceProvinceId, filter.InvoiceProvinceId);
            if (filter.InvoiceDistrictId != null && filter.InvoiceDistrictId.HasValue)
                query = query.Where(q => q.InvoiceDistrictId.HasValue).Where(q => q.InvoiceDistrictId, filter.InvoiceDistrictId);
            if (filter.InvoiceZipCode != null && filter.InvoiceZipCode.HasValue)
                query = query.Where(q => q.InvoiceZipCode, filter.InvoiceZipCode);
            if (filter.ReceiveAddress != null && filter.ReceiveAddress.HasValue)
                query = query.Where(q => q.ReceiveAddress, filter.ReceiveAddress);
            if (filter.ReceiveNationId != null && filter.ReceiveNationId.HasValue)
                query = query.Where(q => q.ReceiveNationId.HasValue).Where(q => q.ReceiveNationId, filter.ReceiveNationId);
            if (filter.ReceiveProvinceId != null && filter.ReceiveProvinceId.HasValue)
                query = query.Where(q => q.ReceiveProvinceId.HasValue).Where(q => q.ReceiveProvinceId, filter.ReceiveProvinceId);
            if (filter.ReceiveDistrictId != null && filter.ReceiveDistrictId.HasValue)
                query = query.Where(q => q.ReceiveDistrictId.HasValue).Where(q => q.ReceiveDistrictId, filter.ReceiveDistrictId);
            if (filter.ReceiveZipCode != null && filter.ReceiveZipCode.HasValue)
                query = query.Where(q => q.ReceiveZipCode, filter.ReceiveZipCode);
            if (filter.SubTotal != null && filter.SubTotal.HasValue)
                query = query.Where(q => q.SubTotal, filter.SubTotal);
            if (filter.GeneralDiscountPercentage != null && filter.GeneralDiscountPercentage.HasValue)
                query = query.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, filter.GeneralDiscountPercentage);
            if (filter.GeneralDiscountAmount != null && filter.GeneralDiscountAmount.HasValue)
                query = query.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, filter.GeneralDiscountAmount);
            if (filter.TotalTaxAmountOther != null && filter.TotalTaxAmountOther.HasValue)
                query = query.Where(q => q.TotalTaxAmountOther.HasValue).Where(q => q.TotalTaxAmountOther, filter.TotalTaxAmountOther);
            if (filter.TotalTaxAmount != null && filter.TotalTaxAmount.HasValue)
                query = query.Where(q => q.TotalTaxAmount.HasValue).Where(q => q.TotalTaxAmount, filter.TotalTaxAmount);
            if (filter.Total != null && filter.Total.HasValue)
                query = query.Where(q => q.Total, filter.Total);
            if (filter.TermAndCondition != null && filter.TermAndCondition.HasValue)
                query = query.Where(q => q.TermAndCondition, filter.TermAndCondition);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.OrganizationId != null && filter.OrganizationId.HasValue)
                query = query.Where(q => q.OrganizationId, filter.OrganizationId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContractDAO> OrFilter(IQueryable<ContractDAO> query, ContractFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContractDAO> initQuery = query.Where(q => false);
            foreach (ContractFilter ContractFilter in filter.OrFilter)
            {
                IQueryable<ContractDAO> queryable = query;
                if (ContractFilter.Id != null && ContractFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContractFilter.Id);
                if (ContractFilter.Code != null && ContractFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ContractFilter.Code);
                if (ContractFilter.Name != null && ContractFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ContractFilter.Name);
                if (ContractFilter.CompanyId != null && ContractFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, ContractFilter.CompanyId);
                if (ContractFilter.OpportunityId != null && ContractFilter.OpportunityId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityId.HasValue).Where(q => q.OpportunityId, ContractFilter.OpportunityId);
                if (ContractFilter.CustomerId != null && ContractFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, ContractFilter.CustomerId);
                if (ContractFilter.ContractTypeId != null && ContractFilter.ContractTypeId.HasValue)
                    queryable = queryable.Where(q => q.ContractTypeId, ContractFilter.ContractTypeId);
                if (ContractFilter.TotalValue != null && ContractFilter.TotalValue.HasValue)
                    queryable = queryable.Where(q => q.TotalValue, ContractFilter.TotalValue);
                if (ContractFilter.CurrencyId != null && ContractFilter.CurrencyId.HasValue)
                    queryable = queryable.Where(q => q.CurrencyId, ContractFilter.CurrencyId);
                if (ContractFilter.ValidityDate != null && ContractFilter.ValidityDate.HasValue)
                    queryable = queryable.Where(q => q.ValidityDate, ContractFilter.ValidityDate);
                if (ContractFilter.ExpirationDate != null && ContractFilter.ExpirationDate.HasValue)
                    queryable = queryable.Where(q => q.ExpirationDate, ContractFilter.ExpirationDate);
                if (ContractFilter.AppUserId != null && ContractFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, ContractFilter.AppUserId);
                if (ContractFilter.DeliveryUnit != null && ContractFilter.DeliveryUnit.HasValue)
                    queryable = queryable.Where(q => q.DeliveryUnit, ContractFilter.DeliveryUnit);
                if (ContractFilter.ContractStatusId != null && ContractFilter.ContractStatusId.HasValue)
                    queryable = queryable.Where(q => q.ContractStatusId, ContractFilter.ContractStatusId);
                if (ContractFilter.PaymentStatusId != null && ContractFilter.PaymentStatusId.HasValue)
                    queryable = queryable.Where(q => q.PaymentStatusId, ContractFilter.PaymentStatusId);
                if (ContractFilter.InvoiceAddress != null && ContractFilter.InvoiceAddress.HasValue)
                    queryable = queryable.Where(q => q.InvoiceAddress, ContractFilter.InvoiceAddress);
                if (ContractFilter.InvoiceNationId != null && ContractFilter.InvoiceNationId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceNationId.HasValue).Where(q => q.InvoiceNationId, ContractFilter.InvoiceNationId);
                if (ContractFilter.InvoiceProvinceId != null && ContractFilter.InvoiceProvinceId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceProvinceId.HasValue).Where(q => q.InvoiceProvinceId, ContractFilter.InvoiceProvinceId);
                if (ContractFilter.InvoiceDistrictId != null && ContractFilter.InvoiceDistrictId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceDistrictId.HasValue).Where(q => q.InvoiceDistrictId, ContractFilter.InvoiceDistrictId);
                if (ContractFilter.InvoiceZipCode != null && ContractFilter.InvoiceZipCode.HasValue)
                    queryable = queryable.Where(q => q.InvoiceZipCode, ContractFilter.InvoiceZipCode);
                if (ContractFilter.ReceiveAddress != null && ContractFilter.ReceiveAddress.HasValue)
                    queryable = queryable.Where(q => q.ReceiveAddress, ContractFilter.ReceiveAddress);
                if (ContractFilter.ReceiveNationId != null && ContractFilter.ReceiveNationId.HasValue)
                    queryable = queryable.Where(q => q.ReceiveNationId.HasValue).Where(q => q.ReceiveNationId, ContractFilter.ReceiveNationId);
                if (ContractFilter.ReceiveProvinceId != null && ContractFilter.ReceiveProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ReceiveProvinceId.HasValue).Where(q => q.ReceiveProvinceId, ContractFilter.ReceiveProvinceId);
                if (ContractFilter.ReceiveDistrictId != null && ContractFilter.ReceiveDistrictId.HasValue)
                    queryable = queryable.Where(q => q.ReceiveDistrictId.HasValue).Where(q => q.ReceiveDistrictId, ContractFilter.ReceiveDistrictId);
                if (ContractFilter.ReceiveZipCode != null && ContractFilter.ReceiveZipCode.HasValue)
                    queryable = queryable.Where(q => q.ReceiveZipCode, ContractFilter.ReceiveZipCode);
                if (ContractFilter.SubTotal != null && ContractFilter.SubTotal.HasValue)
                    queryable = queryable.Where(q => q.SubTotal, ContractFilter.SubTotal);
                if (ContractFilter.GeneralDiscountPercentage != null && ContractFilter.GeneralDiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountPercentage.HasValue).Where(q => q.GeneralDiscountPercentage, ContractFilter.GeneralDiscountPercentage);
                if (ContractFilter.GeneralDiscountAmount != null && ContractFilter.GeneralDiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountAmount.HasValue).Where(q => q.GeneralDiscountAmount, ContractFilter.GeneralDiscountAmount);
                if (ContractFilter.TotalTaxAmountOther != null && ContractFilter.TotalTaxAmountOther.HasValue)
                    queryable = queryable.Where(q => q.TotalTaxAmountOther.HasValue).Where(q => q.TotalTaxAmountOther, ContractFilter.TotalTaxAmountOther);
                if (ContractFilter.TotalTaxAmount != null && ContractFilter.TotalTaxAmount.HasValue)
                    queryable = queryable.Where(q => q.TotalTaxAmount.HasValue).Where(q => q.TotalTaxAmount, ContractFilter.TotalTaxAmount);
                if (ContractFilter.Total != null && ContractFilter.Total.HasValue)
                    queryable = queryable.Where(q => q.Total, ContractFilter.Total);
                if (ContractFilter.TermAndCondition != null && ContractFilter.TermAndCondition.HasValue)
                    queryable = queryable.Where(q => q.TermAndCondition, ContractFilter.TermAndCondition);
                if (ContractFilter.CreatorId != null && ContractFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, ContractFilter.CreatorId);
                if (ContractFilter.OrganizationId != null && ContractFilter.OrganizationId.HasValue)
                    queryable = queryable.Where(q => q.OrganizationId, ContractFilter.OrganizationId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<ContractDAO> DynamicOrder(IQueryable<ContractDAO> query, ContractFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContractOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContractOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ContractOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ContractOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case ContractOrder.Opportunity:
                            query = query.OrderBy(q => q.OpportunityId);
                            break;
                        case ContractOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case ContractOrder.ContractType:
                            query = query.OrderBy(q => q.ContractTypeId);
                            break;
                        case ContractOrder.TotalValue:
                            query = query.OrderBy(q => q.TotalValue);
                            break;
                        case ContractOrder.Currency:
                            query = query.OrderBy(q => q.CurrencyId);
                            break;
                        case ContractOrder.ValidityDate:
                            query = query.OrderBy(q => q.ValidityDate);
                            break;
                        case ContractOrder.ExpirationDate:
                            query = query.OrderBy(q => q.ExpirationDate);
                            break;
                        case ContractOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case ContractOrder.DeliveryUnit:
                            query = query.OrderBy(q => q.DeliveryUnit);
                            break;
                        case ContractOrder.ContractStatus:
                            query = query.OrderBy(q => q.ContractStatusId);
                            break;
                        case ContractOrder.PaymentStatus:
                            query = query.OrderBy(q => q.PaymentStatusId);
                            break;
                        case ContractOrder.InvoiceAddress:
                            query = query.OrderBy(q => q.InvoiceAddress);
                            break;
                        case ContractOrder.InvoiceNation:
                            query = query.OrderBy(q => q.InvoiceNationId);
                            break;
                        case ContractOrder.InvoiceProvince:
                            query = query.OrderBy(q => q.InvoiceProvinceId);
                            break;
                        case ContractOrder.InvoiceDistrict:
                            query = query.OrderBy(q => q.InvoiceDistrictId);
                            break;
                        case ContractOrder.InvoiceZipCode:
                            query = query.OrderBy(q => q.InvoiceZipCode);
                            break;
                        case ContractOrder.ReceiveAddress:
                            query = query.OrderBy(q => q.ReceiveAddress);
                            break;
                        case ContractOrder.ReceiveNation:
                            query = query.OrderBy(q => q.ReceiveNationId);
                            break;
                        case ContractOrder.ReceiveProvince:
                            query = query.OrderBy(q => q.ReceiveProvinceId);
                            break;
                        case ContractOrder.ReceiveDistrict:
                            query = query.OrderBy(q => q.ReceiveDistrictId);
                            break;
                        case ContractOrder.ReceiveZipCode:
                            query = query.OrderBy(q => q.ReceiveZipCode);
                            break;
                        case ContractOrder.SubTotal:
                            query = query.OrderBy(q => q.SubTotal);
                            break;
                        case ContractOrder.GeneralDiscountPercentage:
                            query = query.OrderBy(q => q.GeneralDiscountPercentage);
                            break;
                        case ContractOrder.GeneralDiscountAmount:
                            query = query.OrderBy(q => q.GeneralDiscountAmount);
                            break;
                        case ContractOrder.TotalTaxAmountOther:
                            query = query.OrderBy(q => q.TotalTaxAmountOther);
                            break;
                        case ContractOrder.TotalTaxAmount:
                            query = query.OrderBy(q => q.TotalTaxAmount);
                            break;
                        case ContractOrder.Total:
                            query = query.OrderBy(q => q.Total);
                            break;
                        case ContractOrder.TermAndCondition:
                            query = query.OrderBy(q => q.TermAndCondition);
                            break;
                        case ContractOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case ContractOrder.Organization:
                            query = query.OrderBy(q => q.OrganizationId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContractOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContractOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ContractOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ContractOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case ContractOrder.Opportunity:
                            query = query.OrderByDescending(q => q.OpportunityId);
                            break;
                        case ContractOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case ContractOrder.ContractType:
                            query = query.OrderByDescending(q => q.ContractTypeId);
                            break;
                        case ContractOrder.TotalValue:
                            query = query.OrderByDescending(q => q.TotalValue);
                            break;
                        case ContractOrder.Currency:
                            query = query.OrderByDescending(q => q.CurrencyId);
                            break;
                        case ContractOrder.ValidityDate:
                            query = query.OrderByDescending(q => q.ValidityDate);
                            break;
                        case ContractOrder.ExpirationDate:
                            query = query.OrderByDescending(q => q.ExpirationDate);
                            break;
                        case ContractOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case ContractOrder.DeliveryUnit:
                            query = query.OrderByDescending(q => q.DeliveryUnit);
                            break;
                        case ContractOrder.ContractStatus:
                            query = query.OrderByDescending(q => q.ContractStatusId);
                            break;
                        case ContractOrder.PaymentStatus:
                            query = query.OrderByDescending(q => q.PaymentStatusId);
                            break;
                        case ContractOrder.InvoiceAddress:
                            query = query.OrderByDescending(q => q.InvoiceAddress);
                            break;
                        case ContractOrder.InvoiceNation:
                            query = query.OrderByDescending(q => q.InvoiceNationId);
                            break;
                        case ContractOrder.InvoiceProvince:
                            query = query.OrderByDescending(q => q.InvoiceProvinceId);
                            break;
                        case ContractOrder.InvoiceDistrict:
                            query = query.OrderByDescending(q => q.InvoiceDistrictId);
                            break;
                        case ContractOrder.InvoiceZipCode:
                            query = query.OrderByDescending(q => q.InvoiceZipCode);
                            break;
                        case ContractOrder.ReceiveAddress:
                            query = query.OrderByDescending(q => q.ReceiveAddress);
                            break;
                        case ContractOrder.ReceiveNation:
                            query = query.OrderByDescending(q => q.ReceiveNationId);
                            break;
                        case ContractOrder.ReceiveProvince:
                            query = query.OrderByDescending(q => q.ReceiveProvinceId);
                            break;
                        case ContractOrder.ReceiveDistrict:
                            query = query.OrderByDescending(q => q.ReceiveDistrictId);
                            break;
                        case ContractOrder.ReceiveZipCode:
                            query = query.OrderByDescending(q => q.ReceiveZipCode);
                            break;
                        case ContractOrder.SubTotal:
                            query = query.OrderByDescending(q => q.SubTotal);
                            break;
                        case ContractOrder.GeneralDiscountPercentage:
                            query = query.OrderByDescending(q => q.GeneralDiscountPercentage);
                            break;
                        case ContractOrder.GeneralDiscountAmount:
                            query = query.OrderByDescending(q => q.GeneralDiscountAmount);
                            break;
                        case ContractOrder.TotalTaxAmountOther:
                            query = query.OrderByDescending(q => q.TotalTaxAmountOther);
                            break;
                        case ContractOrder.TotalTaxAmount:
                            query = query.OrderByDescending(q => q.TotalTaxAmount);
                            break;
                        case ContractOrder.Total:
                            query = query.OrderByDescending(q => q.Total);
                            break;
                        case ContractOrder.TermAndCondition:
                            query = query.OrderByDescending(q => q.TermAndCondition);
                            break;
                        case ContractOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case ContractOrder.Organization:
                            query = query.OrderByDescending(q => q.OrganizationId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Contract>> DynamicSelect(IQueryable<ContractDAO> query, ContractFilter filter)
        {
            List<Contract> Contracts = await query.Select(q => new Contract()
            {
                Id = filter.Selects.Contains(ContractSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ContractSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ContractSelect.Name) ? q.Name : default(string),
                CompanyId = filter.Selects.Contains(ContractSelect.Company) ? q.CompanyId : default(long?),
                OpportunityId = filter.Selects.Contains(ContractSelect.Opportunity) ? q.OpportunityId : default(long?),
                CustomerId = filter.Selects.Contains(ContractSelect.Customer) ? q.CustomerId : default(long),
                ContractTypeId = filter.Selects.Contains(ContractSelect.ContractType) ? q.ContractTypeId : default(long),
                TotalValue = filter.Selects.Contains(ContractSelect.TotalValue) ? q.TotalValue : default(decimal),
                CurrencyId = filter.Selects.Contains(ContractSelect.Currency) ? q.CurrencyId : default(long),
                ValidityDate = filter.Selects.Contains(ContractSelect.ValidityDate) ? q.ValidityDate : default(DateTime),
                ExpirationDate = filter.Selects.Contains(ContractSelect.ExpirationDate) ? q.ExpirationDate : default(DateTime),
                AppUserId = filter.Selects.Contains(ContractSelect.AppUser) ? q.AppUserId : default(long),
                DeliveryUnit = filter.Selects.Contains(ContractSelect.DeliveryUnit) ? q.DeliveryUnit : default(string),
                ContractStatusId = filter.Selects.Contains(ContractSelect.ContractStatus) ? q.ContractStatusId : default(long),
                PaymentStatusId = filter.Selects.Contains(ContractSelect.PaymentStatus) ? q.PaymentStatusId : default(long),
                InvoiceAddress = filter.Selects.Contains(ContractSelect.InvoiceAddress) ? q.InvoiceAddress : default(string),
                InvoiceNationId = filter.Selects.Contains(ContractSelect.InvoiceNation) ? q.InvoiceNationId : default(long?),
                InvoiceProvinceId = filter.Selects.Contains(ContractSelect.InvoiceProvince) ? q.InvoiceProvinceId : default(long?),
                InvoiceDistrictId = filter.Selects.Contains(ContractSelect.InvoiceDistrict) ? q.InvoiceDistrictId : default(long?),
                InvoiceZipCode = filter.Selects.Contains(ContractSelect.InvoiceZipCode) ? q.InvoiceZipCode : default(string),
                ReceiveAddress = filter.Selects.Contains(ContractSelect.ReceiveAddress) ? q.ReceiveAddress : default(string),
                ReceiveNationId = filter.Selects.Contains(ContractSelect.ReceiveNation) ? q.ReceiveNationId : default(long?),
                ReceiveProvinceId = filter.Selects.Contains(ContractSelect.ReceiveProvince) ? q.ReceiveProvinceId : default(long?),
                ReceiveDistrictId = filter.Selects.Contains(ContractSelect.ReceiveDistrict) ? q.ReceiveDistrictId : default(long?),
                ReceiveZipCode = filter.Selects.Contains(ContractSelect.ReceiveZipCode) ? q.ReceiveZipCode : default(string),
                SubTotal = filter.Selects.Contains(ContractSelect.SubTotal) ? q.SubTotal : default(decimal),
                GeneralDiscountPercentage = filter.Selects.Contains(ContractSelect.GeneralDiscountPercentage) ? q.GeneralDiscountPercentage : default(decimal?),
                GeneralDiscountAmount = filter.Selects.Contains(ContractSelect.GeneralDiscountAmount) ? q.GeneralDiscountAmount : default(decimal?),
                TotalTaxAmountOther = filter.Selects.Contains(ContractSelect.TotalTaxAmountOther) ? q.TotalTaxAmountOther : default(decimal?),
                TotalTaxAmount = filter.Selects.Contains(ContractSelect.TotalTaxAmount) ? q.TotalTaxAmount : default(decimal?),
                Total = filter.Selects.Contains(ContractSelect.Total) ? q.Total : default(decimal),
                TermAndCondition = filter.Selects.Contains(ContractSelect.TermAndCondition) ? q.TermAndCondition : default(string),
                CreatorId = filter.Selects.Contains(ContractSelect.Creator) ? q.CreatorId : default(long),
                OrganizationId = filter.Selects.Contains(ContractSelect.Organization) ? q.OrganizationId : default(long),
                AppUser = filter.Selects.Contains(ContractSelect.AppUser) && q.AppUser != null ? new AppUser
                {
                    Id = q.AppUser.Id,
                    Username = q.AppUser.Username,
                    DisplayName = q.AppUser.DisplayName,
                    Address = q.AppUser.Address,
                    Email = q.AppUser.Email,
                    Phone = q.AppUser.Phone,
                    SexId = q.AppUser.SexId,
                    Birthday = q.AppUser.Birthday,
                    Avatar = q.AppUser.Avatar,
                    Department = q.AppUser.Department,
                    OrganizationId = q.AppUser.OrganizationId,
                    Longitude = q.AppUser.Longitude,
                    Latitude = q.AppUser.Latitude,
                    StatusId = q.AppUser.StatusId,
                    RowId = q.AppUser.RowId,
                    Used = q.AppUser.Used,
                } : null,
                Company = filter.Selects.Contains(ContractSelect.Company) && q.Company != null ? new Company
                {
                    Id = q.Company.Id,
                    Name = q.Company.Name,
                    Phone = q.Company.Phone,
                    FAX = q.Company.FAX,
                    PhoneOther = q.Company.PhoneOther,
                    Email = q.Company.Email,
                    EmailOther = q.Company.EmailOther,
                    ZIPCode = q.Company.ZIPCode,
                    Revenue = q.Company.Revenue,
                    Website = q.Company.Website,
                    Address = q.Company.Address,
                    NationId = q.Company.NationId,
                    ProvinceId = q.Company.ProvinceId,
                    DistrictId = q.Company.DistrictId,
                    NumberOfEmployee = q.Company.NumberOfEmployee,
                    RefuseReciveEmail = q.Company.RefuseReciveEmail,
                    RefuseReciveSMS = q.Company.RefuseReciveSMS,
                    CustomerLeadId = q.Company.CustomerLeadId,
                    ParentId = q.Company.ParentId,
                    Path = q.Company.Path,
                    Level = q.Company.Level,
                    ProfessionId = q.Company.ProfessionId,
                    AppUserId = q.Company.AppUserId,
                    CreatorId = q.Company.CreatorId,
                    CurrencyId = q.Company.CurrencyId,
                    CompanyStatusId = q.Company.CompanyStatusId,
                    Description = q.Company.Description,
                    RowId = q.Company.RowId,
                } : null,
                ContractStatus = filter.Selects.Contains(ContractSelect.ContractStatus) && q.ContractStatus != null ? new ContractStatus
                {
                    Id = q.ContractStatus.Id,
                    Name = q.ContractStatus.Name,
                    Code = q.ContractStatus.Code,
                } : null,
                ContractType = filter.Selects.Contains(ContractSelect.ContractType) && q.ContractType != null ? new ContractType
                {
                    Id = q.ContractType.Id,
                    Name = q.ContractType.Name,
                    Code = q.ContractType.Code,
                } : null,
                Creator = filter.Selects.Contains(ContractSelect.Creator) && q.Creator != null ? new AppUser
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
                Currency = filter.Selects.Contains(ContractSelect.Currency) && q.Currency != null ? new Currency
                {
                    Id = q.Currency.Id,
                    Code = q.Currency.Code,
                    Name = q.Currency.Name,
                } : null,
                Customer = filter.Selects.Contains(ContractSelect.Customer) && q.Customer != null ? new Customer
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
                    CreatorId = q.Customer.CreatorId,
                    Used = q.Customer.Used,
                    RowId = q.Customer.RowId,
                } : null,
                InvoiceDistrict = filter.Selects.Contains(ContractSelect.InvoiceDistrict) && q.InvoiceDistrict != null ? new District
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
                InvoiceNation = filter.Selects.Contains(ContractSelect.InvoiceNation) && q.InvoiceNation != null ? new Nation
                {
                    Id = q.InvoiceNation.Id,
                    Code = q.InvoiceNation.Code,
                    Name = q.InvoiceNation.Name,
                    Priority = q.InvoiceNation.Priority,
                    StatusId = q.InvoiceNation.StatusId,
                    Used = q.InvoiceNation.Used,
                    RowId = q.InvoiceNation.RowId,
                } : null,
                InvoiceProvince = filter.Selects.Contains(ContractSelect.InvoiceProvince) && q.InvoiceProvince != null ? new Province
                {
                    Id = q.InvoiceProvince.Id,
                    Code = q.InvoiceProvince.Code,
                    Name = q.InvoiceProvince.Name,
                    Priority = q.InvoiceProvince.Priority,
                    StatusId = q.InvoiceProvince.StatusId,
                    RowId = q.InvoiceProvince.RowId,
                    Used = q.InvoiceProvince.Used,
                } : null,
                Opportunity = filter.Selects.Contains(ContractSelect.Opportunity) && q.Opportunity != null ? new Opportunity
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
                Organization = filter.Selects.Contains(ContractSelect.Organization) && q.Organization != null ? new Organization
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
                PaymentStatus = filter.Selects.Contains(ContractSelect.PaymentStatus) && q.PaymentStatus != null ? new PaymentStatus
                {
                    Id = q.PaymentStatus.Id,
                    Code = q.PaymentStatus.Code,
                    Name = q.PaymentStatus.Name,
                } : null,
                ReceiveDistrict = filter.Selects.Contains(ContractSelect.ReceiveDistrict) && q.ReceiveDistrict != null ? new District
                {
                    Id = q.ReceiveDistrict.Id,
                    Code = q.ReceiveDistrict.Code,
                    Name = q.ReceiveDistrict.Name,
                    Priority = q.ReceiveDistrict.Priority,
                    ProvinceId = q.ReceiveDistrict.ProvinceId,
                    StatusId = q.ReceiveDistrict.StatusId,
                    RowId = q.ReceiveDistrict.RowId,
                    Used = q.ReceiveDistrict.Used,
                } : null,
                ReceiveNation = filter.Selects.Contains(ContractSelect.ReceiveNation) && q.ReceiveNation != null ? new Nation
                {
                    Id = q.ReceiveNation.Id,
                    Code = q.ReceiveNation.Code,
                    Name = q.ReceiveNation.Name,
                    Priority = q.ReceiveNation.Priority,
                    StatusId = q.ReceiveNation.StatusId,
                    Used = q.ReceiveNation.Used,
                    RowId = q.ReceiveNation.RowId,
                } : null,
                ReceiveProvince = filter.Selects.Contains(ContractSelect.ReceiveProvince) && q.ReceiveProvince != null ? new Province
                {
                    Id = q.ReceiveProvince.Id,
                    Code = q.ReceiveProvince.Code,
                    Name = q.ReceiveProvince.Name,
                    Priority = q.ReceiveProvince.Priority,
                    StatusId = q.ReceiveProvince.StatusId,
                    RowId = q.ReceiveProvince.RowId,
                    Used = q.ReceiveProvince.Used,
                } : null,
            }).ToListAsync();
            return Contracts;
        }

        public async Task<int> Count(ContractFilter filter)
        {
            IQueryable<ContractDAO> Contracts = DataContext.Contract.AsNoTracking();
            Contracts = DynamicFilter(Contracts, filter);
            return await Contracts.CountAsync();
        }

        public async Task<List<Contract>> List(ContractFilter filter)
        {
            if (filter == null) return new List<Contract>();
            IQueryable<ContractDAO> ContractDAOs = DataContext.Contract.AsNoTracking();
            ContractDAOs = DynamicFilter(ContractDAOs, filter);
            ContractDAOs = DynamicOrder(ContractDAOs, filter);
            List<Contract> Contracts = await DynamicSelect(ContractDAOs, filter);
            return Contracts;
        }

        public async Task<List<Contract>> List(List<long> Ids)
        {
            List<Contract> Contracts = await DataContext.Contract.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Contract()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                CompanyId = x.CompanyId,
                OpportunityId = x.OpportunityId,
                CustomerId = x.CustomerId,
                ContractTypeId = x.ContractTypeId,
                TotalValue = x.TotalValue,
                CurrencyId = x.CurrencyId,
                ValidityDate = x.ValidityDate,
                ExpirationDate = x.ExpirationDate,
                AppUserId = x.AppUserId,
                DeliveryUnit = x.DeliveryUnit,
                ContractStatusId = x.ContractStatusId,
                PaymentStatusId = x.PaymentStatusId,
                InvoiceAddress = x.InvoiceAddress,
                InvoiceNationId = x.InvoiceNationId,
                InvoiceProvinceId = x.InvoiceProvinceId,
                InvoiceDistrictId = x.InvoiceDistrictId,
                InvoiceZipCode = x.InvoiceZipCode,
                ReceiveAddress = x.ReceiveAddress,
                ReceiveNationId = x.ReceiveNationId,
                ReceiveProvinceId = x.ReceiveProvinceId,
                ReceiveDistrictId = x.ReceiveDistrictId,
                ReceiveZipCode = x.ReceiveZipCode,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TotalTaxAmountOther = x.TotalTaxAmountOther,
                TotalTaxAmount = x.TotalTaxAmount,
                Total = x.Total,
                TermAndCondition = x.TermAndCondition,
                CreatorId = x.CreatorId,
                OrganizationId = x.OrganizationId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                ContractStatus = x.ContractStatus == null ? null : new ContractStatus
                {
                    Id = x.ContractStatus.Id,
                    Name = x.ContractStatus.Name,
                    Code = x.ContractStatus.Code,
                },
                ContractType = x.ContractType == null ? null : new ContractType
                {
                    Id = x.ContractType.Id,
                    Name = x.ContractType.Name,
                    Code = x.ContractType.Code,
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
                Currency = x.Currency == null ? null : new Currency
                {
                    Id = x.Currency.Id,
                    Code = x.Currency.Code,
                    Name = x.Currency.Name,
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
                    CreatorId = x.Customer.CreatorId,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
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
                PaymentStatus = x.PaymentStatus == null ? null : new PaymentStatus
                {
                    Id = x.PaymentStatus.Id,
                    Code = x.PaymentStatus.Code,
                    Name = x.PaymentStatus.Name,
                },
                ReceiveDistrict = x.ReceiveDistrict == null ? null : new District
                {
                    Id = x.ReceiveDistrict.Id,
                    Code = x.ReceiveDistrict.Code,
                    Name = x.ReceiveDistrict.Name,
                    Priority = x.ReceiveDistrict.Priority,
                    ProvinceId = x.ReceiveDistrict.ProvinceId,
                    StatusId = x.ReceiveDistrict.StatusId,
                    RowId = x.ReceiveDistrict.RowId,
                    Used = x.ReceiveDistrict.Used,
                },
                ReceiveNation = x.ReceiveNation == null ? null : new Nation
                {
                    Id = x.ReceiveNation.Id,
                    Code = x.ReceiveNation.Code,
                    Name = x.ReceiveNation.Name,
                    Priority = x.ReceiveNation.Priority,
                    StatusId = x.ReceiveNation.StatusId,
                    Used = x.ReceiveNation.Used,
                    RowId = x.ReceiveNation.RowId,
                },
                ReceiveProvince = x.ReceiveProvince == null ? null : new Province
                {
                    Id = x.ReceiveProvince.Id,
                    Code = x.ReceiveProvince.Code,
                    Name = x.ReceiveProvince.Name,
                    Priority = x.ReceiveProvince.Priority,
                    StatusId = x.ReceiveProvince.StatusId,
                    RowId = x.ReceiveProvince.RowId,
                    Used = x.ReceiveProvince.Used,
                },
            }).ToListAsync();

            List<ContractContactMapping> ContractContactMappings = await DataContext.ContractContactMapping.AsNoTracking()
                .Where(x => Ids.Contains(x.ContractId))
                .Where(x => x.Contact.DeletedAt == null)
                .Select(x => new ContractContactMapping
                {
                    ContractId = x.ContractId,
                    ContactId = x.ContactId,
                    Contact = new Contact
                    {
                        Id = x.Contact.Id,
                        Name = x.Contact.Name,
                        ProfessionId = x.Contact.ProfessionId,
                        CompanyId = x.Contact.CompanyId,
                        ContactStatusId = x.Contact.ContactStatusId,
                        Address = x.Contact.Address,
                        NationId = x.Contact.NationId,
                        ProvinceId = x.Contact.ProvinceId,
                        DistrictId = x.Contact.DistrictId,
                        CustomerLeadId = x.Contact.CustomerLeadId,
                        ImageId = x.Contact.ImageId,
                        Description = x.Contact.Description,
                        EmailOther = x.Contact.EmailOther,
                        DateOfBirth = x.Contact.DateOfBirth,
                        Phone = x.Contact.Phone,
                        PhoneHome = x.Contact.PhoneHome,
                        FAX = x.Contact.FAX,
                        Email = x.Contact.Email,
                        Department = x.Contact.Department,
                        ZIPCode = x.Contact.ZIPCode,
                        SexId = x.Contact.SexId,
                        AppUserId = x.Contact.AppUserId,
                        RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                        RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                        PositionId = x.Contact.PositionId,
                        CreatorId = x.Contact.CreatorId,
                    },
                }).ToListAsync();
            foreach (Contract Contract in Contracts)
            {
                Contract.ContractContactMappings = ContractContactMappings
                    .Where(x => x.ContractId == Contract.Id)
                    .ToList();
            }
            List<ContractFileGrouping> ContractFileGroupings = await DataContext.ContractFileGrouping.AsNoTracking()
                .Where(x => Ids.Contains(x.ContractId))
                .Where(x => x.DeletedAt == null)
                .Select(x => new ContractFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ContractId = x.ContractId,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    RowId = x.RowId,
                    Creator = new AppUser
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
                    FileType = new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    },
                }).ToListAsync();
            foreach (Contract Contract in Contracts)
            {
                Contract.ContractFileGroupings = ContractFileGroupings
                    .Where(x => x.ContractId == Contract.Id)
                    .ToList();
            }
            List<ContractItemDetail> ContractItemDetails = await DataContext.ContractItemDetail.AsNoTracking()
                .Where(x => Ids.Contains(x.ContractId))
                .Select(x => new ContractItemDetail
                {
                    Id = x.Id,
                    ContractId = x.ContractId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestQuantity = x.RequestQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxPercentageOther = x.TaxPercentageOther,
                    TaxAmountOther = x.TaxAmountOther,
                    TotalPrice = x.TotalPrice,
                    Factor = x.Factor,
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
            foreach (Contract Contract in Contracts)
            {
                Contract.ContractItemDetails = ContractItemDetails
                    .Where(x => x.ContractId == Contract.Id)
                    .ToList();
            }
            List<ContractPaymentHistory> ContractPaymentHistories = await DataContext.ContractPaymentHistory.AsNoTracking()
                .Where(x => Ids.Contains(x.ContractId))
                .Where(x => x.DeletedAt == null)
                .Select(x => new ContractPaymentHistory
                {
                    Id = x.Id,
                    ContractId = x.ContractId,
                    PaymentMilestone = x.PaymentMilestone,
                    PaymentPercentage = x.PaymentPercentage,
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                }).ToListAsync();
            foreach (Contract Contract in Contracts)
            {
                Contract.ContractPaymentHistories = ContractPaymentHistories
                    .Where(x => x.ContractId == Contract.Id)
                    .ToList();
            }

            return Contracts;
        }

        public async Task<Contract> Get(long Id)
        {
            Contract Contract = await DataContext.Contract.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new Contract()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                CompanyId = x.CompanyId,
                OpportunityId = x.OpportunityId,
                CustomerId = x.CustomerId,
                ContractTypeId = x.ContractTypeId,
                TotalValue = x.TotalValue,
                CurrencyId = x.CurrencyId,
                ValidityDate = x.ValidityDate,
                ExpirationDate = x.ExpirationDate,
                AppUserId = x.AppUserId,
                DeliveryUnit = x.DeliveryUnit,
                ContractStatusId = x.ContractStatusId,
                PaymentStatusId = x.PaymentStatusId,
                InvoiceAddress = x.InvoiceAddress,
                InvoiceNationId = x.InvoiceNationId,
                InvoiceProvinceId = x.InvoiceProvinceId,
                InvoiceDistrictId = x.InvoiceDistrictId,
                InvoiceZipCode = x.InvoiceZipCode,
                ReceiveAddress = x.ReceiveAddress,
                ReceiveNationId = x.ReceiveNationId,
                ReceiveProvinceId = x.ReceiveProvinceId,
                ReceiveDistrictId = x.ReceiveDistrictId,
                ReceiveZipCode = x.ReceiveZipCode,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TotalTaxAmountOther = x.TotalTaxAmountOther,
                TotalTaxAmount = x.TotalTaxAmount,
                Total = x.Total,
                TermAndCondition = x.TermAndCondition,
                CreatorId = x.CreatorId,
                OrganizationId = x.OrganizationId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                ContractStatus = x.ContractStatus == null ? null : new ContractStatus
                {
                    Id = x.ContractStatus.Id,
                    Name = x.ContractStatus.Name,
                    Code = x.ContractStatus.Code,
                },
                ContractType = x.ContractType == null ? null : new ContractType
                {
                    Id = x.ContractType.Id,
                    Name = x.ContractType.Name,
                    Code = x.ContractType.Code,
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
                Currency = x.Currency == null ? null : new Currency
                {
                    Id = x.Currency.Id,
                    Code = x.Currency.Code,
                    Name = x.Currency.Name,
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
                    CreatorId = x.Customer.CreatorId,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
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
                PaymentStatus = x.PaymentStatus == null ? null : new PaymentStatus
                {
                    Id = x.PaymentStatus.Id,
                    Code = x.PaymentStatus.Code,
                    Name = x.PaymentStatus.Name,
                },
                ReceiveDistrict = x.ReceiveDistrict == null ? null : new District
                {
                    Id = x.ReceiveDistrict.Id,
                    Code = x.ReceiveDistrict.Code,
                    Name = x.ReceiveDistrict.Name,
                    Priority = x.ReceiveDistrict.Priority,
                    ProvinceId = x.ReceiveDistrict.ProvinceId,
                    StatusId = x.ReceiveDistrict.StatusId,
                    RowId = x.ReceiveDistrict.RowId,
                    Used = x.ReceiveDistrict.Used,
                },
                ReceiveNation = x.ReceiveNation == null ? null : new Nation
                {
                    Id = x.ReceiveNation.Id,
                    Code = x.ReceiveNation.Code,
                    Name = x.ReceiveNation.Name,
                    Priority = x.ReceiveNation.Priority,
                    StatusId = x.ReceiveNation.StatusId,
                    Used = x.ReceiveNation.Used,
                    RowId = x.ReceiveNation.RowId,
                },
                ReceiveProvince = x.ReceiveProvince == null ? null : new Province
                {
                    Id = x.ReceiveProvince.Id,
                    Code = x.ReceiveProvince.Code,
                    Name = x.ReceiveProvince.Name,
                    Priority = x.ReceiveProvince.Priority,
                    StatusId = x.ReceiveProvince.StatusId,
                    RowId = x.ReceiveProvince.RowId,
                    Used = x.ReceiveProvince.Used,
                },
            }).FirstOrDefaultAsync();

            if (Contract == null)
                return null;
            Contract.ContractContactMappings = await DataContext.ContractContactMapping.AsNoTracking()
                .Where(x => x.ContractId == Contract.Id)
                .Where(x => x.Contact.DeletedAt == null)
                .Select(x => new ContractContactMapping
                {
                    ContractId = x.ContractId,
                    ContactId = x.ContactId,
                    Contact = new Contact
                    {
                        Id = x.Contact.Id,
                        Name = x.Contact.Name,
                        ProfessionId = x.Contact.ProfessionId,
                        CompanyId = x.Contact.CompanyId,
                        ContactStatusId = x.Contact.ContactStatusId,
                        Address = x.Contact.Address,
                        NationId = x.Contact.NationId,
                        ProvinceId = x.Contact.ProvinceId,
                        DistrictId = x.Contact.DistrictId,
                        CustomerLeadId = x.Contact.CustomerLeadId,
                        ImageId = x.Contact.ImageId,
                        Description = x.Contact.Description,
                        EmailOther = x.Contact.EmailOther,
                        DateOfBirth = x.Contact.DateOfBirth,
                        Phone = x.Contact.Phone,
                        PhoneHome = x.Contact.PhoneHome,
                        FAX = x.Contact.FAX,
                        Email = x.Contact.Email,
                        Department = x.Contact.Department,
                        ZIPCode = x.Contact.ZIPCode,
                        SexId = x.Contact.SexId,
                        AppUserId = x.Contact.AppUserId,
                        RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                        RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                        PositionId = x.Contact.PositionId,
                        CreatorId = x.Contact.CreatorId,
                    },
                }).ToListAsync();
            Contract.ContractFileGroupings = await DataContext.ContractFileGrouping.AsNoTracking()
                .Where(x => x.ContractId == Contract.Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new ContractFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ContractId = x.ContractId,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    RowId = x.RowId,
                    Creator = new AppUser
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
                    FileType = new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    },
                }).ToListAsync();
            var ContractFileGroupingIds = Contract.ContractFileGroupings.Select(x => x.Id).ToList();

            var ContractFileMappings = await DataContext.ContractFileMapping.Where(x => ContractFileGroupingIds.Contains(x.ContractFileGroupingId))
                .Select(x => new ContractFileMapping
                {
                    ContractFileGroupingId = x.ContractFileGroupingId,
                    FileId = x.FileId,
                    File = x.File == null ? null : new File
                    {
                        Id = x.File.Id,
                        AppUserId = x.File.AppUserId,
                        CreatedAt = x.File.CreatedAt,
                        Name = x.File.Name,
                        Url = x.File.Url,
                        UpdatedAt = x.File.UpdatedAt,
                        RowId = x.File.RowId,
                        AppUser = x.File.AppUser == null ? null : new AppUser
                        {
                            Id = x.File.AppUser.Id,
                            Username = x.File.AppUser.Username,
                            DisplayName = x.File.AppUser.DisplayName,
                        },
                    },
                }).ToListAsync();

            foreach (ContractFileGrouping ContractFileGrouping in Contract.ContractFileGroupings)
            {
                ContractFileGrouping.ContractFileMappings = ContractFileMappings.Where(x => x.ContractFileGroupingId == ContractFileGrouping.Id).ToList();
            }
            Contract.ContractItemDetails = await DataContext.ContractItemDetail.AsNoTracking()
                .Where(x => x.ContractId == Contract.Id)
                .Select(x => new ContractItemDetail
                {
                    Id = x.Id,
                    ContractId = x.ContractId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestQuantity = x.RequestQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxPercentageOther = x.TaxPercentageOther,
                    TaxAmountOther = x.TaxAmountOther,
                    TotalPrice = x.TotalPrice,
                    Factor = x.Factor,
                    TaxTypeId = x.TaxTypeId,
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
            Contract.ContractPaymentHistories = await DataContext.ContractPaymentHistory.AsNoTracking()
                .Where(x => x.ContractId == Contract.Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new ContractPaymentHistory
                {
                    Id = x.Id,
                    ContractId = x.ContractId,
                    PaymentMilestone = x.PaymentMilestone,
                    PaymentPercentage = x.PaymentPercentage,
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                }).ToListAsync();

            return Contract;
        }
        public async Task<bool> Create(Contract Contract)
        {
            ContractDAO ContractDAO = new ContractDAO();
            ContractDAO.Id = Contract.Id;
            ContractDAO.Code = Contract.Code;
            ContractDAO.Name = Contract.Name;
            ContractDAO.CompanyId = Contract.CompanyId;
            ContractDAO.OpportunityId = Contract.OpportunityId;
            ContractDAO.CustomerId = Contract.CustomerId;
            ContractDAO.ContractTypeId = Contract.ContractTypeId;
            ContractDAO.TotalValue = Contract.TotalValue;
            ContractDAO.CurrencyId = Contract.CurrencyId;
            ContractDAO.ValidityDate = Contract.ValidityDate;
            ContractDAO.ExpirationDate = Contract.ExpirationDate;
            ContractDAO.AppUserId = Contract.AppUserId;
            ContractDAO.DeliveryUnit = Contract.DeliveryUnit;
            ContractDAO.ContractStatusId = Contract.ContractStatusId;
            ContractDAO.PaymentStatusId = Contract.PaymentStatusId;
            ContractDAO.InvoiceAddress = Contract.InvoiceAddress;
            ContractDAO.InvoiceNationId = Contract.InvoiceNationId;
            ContractDAO.InvoiceProvinceId = Contract.InvoiceProvinceId;
            ContractDAO.InvoiceDistrictId = Contract.InvoiceDistrictId;
            ContractDAO.InvoiceZipCode = Contract.InvoiceZipCode;
            ContractDAO.ReceiveAddress = Contract.ReceiveAddress;
            ContractDAO.ReceiveNationId = Contract.ReceiveNationId;
            ContractDAO.ReceiveProvinceId = Contract.ReceiveProvinceId;
            ContractDAO.ReceiveDistrictId = Contract.ReceiveDistrictId;
            ContractDAO.ReceiveZipCode = Contract.ReceiveZipCode;
            ContractDAO.SubTotal = Contract.SubTotal;
            ContractDAO.GeneralDiscountPercentage = Contract.GeneralDiscountPercentage;
            ContractDAO.GeneralDiscountAmount = Contract.GeneralDiscountAmount;
            ContractDAO.TotalTaxAmountOther = Contract.TotalTaxAmountOther;
            ContractDAO.TotalTaxAmount = Contract.TotalTaxAmount;
            ContractDAO.Total = Contract.Total;
            ContractDAO.TermAndCondition = Contract.TermAndCondition;
            ContractDAO.CreatorId = Contract.CreatorId;
            ContractDAO.OrganizationId = Contract.OrganizationId;
            ContractDAO.CreatedAt = StaticParams.DateTimeNow;
            ContractDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Contract.Add(ContractDAO);
            await DataContext.SaveChangesAsync();
            Contract.Id = ContractDAO.Id;
            await SaveReference(Contract);
            return true;
        }

        public async Task<bool> Update(Contract Contract)
        {
            ContractDAO ContractDAO = DataContext.Contract.Where(x => x.Id == Contract.Id).FirstOrDefault();
            if (ContractDAO == null)
                return false;
            ContractDAO.Id = Contract.Id;
            ContractDAO.Code = Contract.Code;
            ContractDAO.Name = Contract.Name;
            ContractDAO.CompanyId = Contract.CompanyId;
            ContractDAO.OpportunityId = Contract.OpportunityId;
            ContractDAO.CustomerId = Contract.CustomerId;
            ContractDAO.ContractTypeId = Contract.ContractTypeId;
            ContractDAO.TotalValue = Contract.TotalValue;
            ContractDAO.CurrencyId = Contract.CurrencyId;
            ContractDAO.ValidityDate = Contract.ValidityDate;
            ContractDAO.ExpirationDate = Contract.ExpirationDate;
            ContractDAO.AppUserId = Contract.AppUserId;
            ContractDAO.DeliveryUnit = Contract.DeliveryUnit;
            ContractDAO.ContractStatusId = Contract.ContractStatusId;
            ContractDAO.PaymentStatusId = Contract.PaymentStatusId;
            ContractDAO.InvoiceAddress = Contract.InvoiceAddress;
            ContractDAO.InvoiceNationId = Contract.InvoiceNationId;
            ContractDAO.InvoiceProvinceId = Contract.InvoiceProvinceId;
            ContractDAO.InvoiceDistrictId = Contract.InvoiceDistrictId;
            ContractDAO.InvoiceZipCode = Contract.InvoiceZipCode;
            ContractDAO.ReceiveAddress = Contract.ReceiveAddress;
            ContractDAO.ReceiveNationId = Contract.ReceiveNationId;
            ContractDAO.ReceiveProvinceId = Contract.ReceiveProvinceId;
            ContractDAO.ReceiveDistrictId = Contract.ReceiveDistrictId;
            ContractDAO.ReceiveZipCode = Contract.ReceiveZipCode;
            ContractDAO.SubTotal = Contract.SubTotal;
            ContractDAO.GeneralDiscountPercentage = Contract.GeneralDiscountPercentage;
            ContractDAO.GeneralDiscountAmount = Contract.GeneralDiscountAmount;
            ContractDAO.TotalTaxAmountOther = Contract.TotalTaxAmountOther;
            ContractDAO.TotalTaxAmount = Contract.TotalTaxAmount;
            ContractDAO.Total = Contract.Total;
            ContractDAO.TermAndCondition = Contract.TermAndCondition;
            ContractDAO.CreatorId = Contract.CreatorId;
            ContractDAO.OrganizationId = Contract.OrganizationId;
            ContractDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Contract);
            return true;
        }

        public async Task<bool> Delete(Contract Contract)
        {
            await DataContext.Contract.Where(x => x.Id == Contract.Id).UpdateFromQueryAsync(x => new ContractDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<Contract> Contracts)
        {
            List<ContractDAO> ContractDAOs = new List<ContractDAO>();
            foreach (Contract Contract in Contracts)
            {
                ContractDAO ContractDAO = new ContractDAO();
                ContractDAO.Id = Contract.Id;
                ContractDAO.Code = Contract.Code;
                ContractDAO.Name = Contract.Name;
                ContractDAO.CompanyId = Contract.CompanyId;
                ContractDAO.OpportunityId = Contract.OpportunityId;
                ContractDAO.CustomerId = Contract.CustomerId;
                ContractDAO.ContractTypeId = Contract.ContractTypeId;
                ContractDAO.TotalValue = Contract.TotalValue;
                ContractDAO.CurrencyId = Contract.CurrencyId;
                ContractDAO.ValidityDate = Contract.ValidityDate;
                ContractDAO.ExpirationDate = Contract.ExpirationDate;
                ContractDAO.AppUserId = Contract.AppUserId;
                ContractDAO.DeliveryUnit = Contract.DeliveryUnit;
                ContractDAO.ContractStatusId = Contract.ContractStatusId;
                ContractDAO.PaymentStatusId = Contract.PaymentStatusId;
                ContractDAO.InvoiceAddress = Contract.InvoiceAddress;
                ContractDAO.InvoiceNationId = Contract.InvoiceNationId;
                ContractDAO.InvoiceProvinceId = Contract.InvoiceProvinceId;
                ContractDAO.InvoiceDistrictId = Contract.InvoiceDistrictId;
                ContractDAO.InvoiceZipCode = Contract.InvoiceZipCode;
                ContractDAO.ReceiveAddress = Contract.ReceiveAddress;
                ContractDAO.ReceiveNationId = Contract.ReceiveNationId;
                ContractDAO.ReceiveProvinceId = Contract.ReceiveProvinceId;
                ContractDAO.ReceiveDistrictId = Contract.ReceiveDistrictId;
                ContractDAO.ReceiveZipCode = Contract.ReceiveZipCode;
                ContractDAO.SubTotal = Contract.SubTotal;
                ContractDAO.GeneralDiscountPercentage = Contract.GeneralDiscountPercentage;
                ContractDAO.GeneralDiscountAmount = Contract.GeneralDiscountAmount;
                ContractDAO.TotalTaxAmountOther = Contract.TotalTaxAmountOther;
                ContractDAO.TotalTaxAmount = Contract.TotalTaxAmount;
                ContractDAO.Total = Contract.Total;
                ContractDAO.TermAndCondition = Contract.TermAndCondition;
                ContractDAO.CreatorId = Contract.CreatorId;
                ContractDAO.OrganizationId = Contract.OrganizationId;
                ContractDAO.CreatedAt = StaticParams.DateTimeNow;
                ContractDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContractDAOs.Add(ContractDAO);
            }
            await DataContext.BulkMergeAsync(ContractDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Contract> Contracts)
        {
            List<long> Ids = Contracts.Select(x => x.Id).ToList();
            await DataContext.Contract
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContractDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Contract Contract)
        {
            await DataContext.ContractContactMapping
                .Where(x => x.ContractId == Contract.Id)
                .DeleteFromQueryAsync();
            List<ContractContactMappingDAO> ContractContactMappingDAOs = new List<ContractContactMappingDAO>();
            if (Contract.ContractContactMappings != null)
            {
                foreach (ContractContactMapping ContractContactMapping in Contract.ContractContactMappings)
                {
                    ContractContactMappingDAO ContractContactMappingDAO = new ContractContactMappingDAO();
                    ContractContactMappingDAO.ContractId = Contract.Id;
                    ContractContactMappingDAO.ContactId = ContractContactMapping.ContactId;
                    ContractContactMappingDAOs.Add(ContractContactMappingDAO);
                }
                await DataContext.ContractContactMapping.BulkMergeAsync(ContractContactMappingDAOs);
            }
            await DataContext.ContractItemDetail
                .Where(x => x.ContractId == Contract.Id)
                .DeleteFromQueryAsync();
            List<ContractItemDetailDAO> ContractItemDetailDAOs = new List<ContractItemDetailDAO>();
            if (Contract.ContractItemDetails != null)
            {
                foreach (ContractItemDetail ContractItemDetail in Contract.ContractItemDetails)
                {
                    ContractItemDetailDAO ContractItemDetailDAO = new ContractItemDetailDAO();
                    ContractItemDetailDAO.Id = ContractItemDetail.Id;
                    ContractItemDetailDAO.ContractId = Contract.Id;
                    ContractItemDetailDAO.ItemId = ContractItemDetail.ItemId;
                    ContractItemDetailDAO.UnitOfMeasureId = ContractItemDetail.UnitOfMeasureId;
                    ContractItemDetailDAO.PrimaryUnitOfMeasureId = ContractItemDetail.PrimaryUnitOfMeasureId;
                    ContractItemDetailDAO.Quantity = ContractItemDetail.Quantity;
                    ContractItemDetailDAO.RequestQuantity = ContractItemDetail.RequestQuantity;
                    ContractItemDetailDAO.PrimaryPrice = ContractItemDetail.PrimaryPrice;
                    ContractItemDetailDAO.SalePrice = ContractItemDetail.SalePrice;
                    ContractItemDetailDAO.DiscountPercentage = ContractItemDetail.DiscountPercentage;
                    ContractItemDetailDAO.DiscountAmount = ContractItemDetail.DiscountAmount;
                    ContractItemDetailDAO.GeneralDiscountPercentage = ContractItemDetail.GeneralDiscountPercentage;
                    ContractItemDetailDAO.GeneralDiscountAmount = ContractItemDetail.GeneralDiscountAmount;
                    ContractItemDetailDAO.TaxPercentage = ContractItemDetail.TaxPercentage;
                    ContractItemDetailDAO.TaxAmount = ContractItemDetail.TaxAmount;
                    ContractItemDetailDAO.TaxPercentageOther = ContractItemDetail.TaxPercentageOther;
                    ContractItemDetailDAO.TaxAmountOther = ContractItemDetail.TaxAmountOther;
                    ContractItemDetailDAO.TotalPrice = ContractItemDetail.TotalPrice;
                    ContractItemDetailDAO.Factor = ContractItemDetail.Factor;
                    ContractItemDetailDAO.TaxTypeId = ContractItemDetail.TaxTypeId;
                    ContractItemDetailDAOs.Add(ContractItemDetailDAO);
                }
                await DataContext.ContractItemDetail.BulkMergeAsync(ContractItemDetailDAOs);
            }

            List<ContractFileGroupingDAO> ContractFileGroupingDAOs = await DataContext.ContractFileGrouping.Where(x => x.ContractId == Contract.Id).ToListAsync();
            ContractFileGroupingDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            await DataContext.ContractFileMapping.Where(x => x.ContractFileGrouping.ContractId == Contract.Id).DeleteFromQueryAsync();
            List<ContractFileMappingDAO> ContractFileMappingDAOs = new List<ContractFileMappingDAO>();
            if (Contract.ContractFileGroupings != null)
            {
                foreach (var ContractFileGrouping in Contract.ContractFileGroupings)
                {
                    ContractFileGroupingDAO ContractFileGroupingDAO = ContractFileGroupingDAOs.Where(x => x.Id == ContractFileGrouping.Id && x.Id != 0).FirstOrDefault();
                    if (ContractFileGroupingDAO == null)
                    {
                        ContractFileGroupingDAO = new ContractFileGroupingDAO
                        {
                            CreatorId = ContractFileGrouping.CreatorId,
                            ContractId = Contract.Id,
                            Description = ContractFileGrouping.Description,
                            Title = ContractFileGrouping.Title,
                            FileTypeId = ContractFileGrouping.FileTypeId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow,
                            RowId = Guid.NewGuid()
                        };
                        ContractFileGroupingDAOs.Add(ContractFileGroupingDAO);
                        ContractFileGrouping.RowId = ContractFileGroupingDAO.RowId;
                    }
                    else
                    {
                        ContractFileGroupingDAO.Description = ContractFileGrouping.Description;
                        ContractFileGroupingDAO.Title = ContractFileGrouping.Title;
                        ContractFileGroupingDAO.FileTypeId = ContractFileGrouping.FileTypeId;
                        ContractFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                        ContractFileGroupingDAO.DeletedAt = null;
                        ContractFileGrouping.RowId = ContractFileGroupingDAO.RowId;
                    }
                }
            }
            await DataContext.BulkMergeAsync(ContractFileGroupingDAOs);
            if (Contract.ContractFileGroupings != null)
            {
                foreach (var ContractFileGrouping in Contract.ContractFileGroupings)
                {
                    ContractFileGrouping.Id = ContractFileGroupingDAOs.Where(x => x.RowId == ContractFileGrouping.RowId).Select(x => x.Id).FirstOrDefault();
                    if (ContractFileGrouping.ContractFileMappings != null)
                    {
                        foreach (var ContractFileMapping in ContractFileGrouping.ContractFileMappings)
                        {
                            ContractFileMappingDAO ContractFileMappingDAO = new ContractFileMappingDAO
                            {
                                FileId = ContractFileMapping.FileId,
                                ContractFileGroupingId = ContractFileGrouping.Id,
                            };
                            ContractFileMappingDAOs.Add(ContractFileMappingDAO);
                        }
                    }
                }
            }
            await DataContext.BulkMergeAsync(ContractFileMappingDAOs);

            List<ContractPaymentHistoryDAO> ContractPaymentHistoryDAOs = await DataContext.ContractPaymentHistory
                .Where(x => x.ContractId == Contract.Id).ToListAsync();
            ContractPaymentHistoryDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            if (Contract.ContractPaymentHistories != null)
            {
                foreach (ContractPaymentHistory ContractPaymentHistory in Contract.ContractPaymentHistories)
                {
                    ContractPaymentHistoryDAO ContractPaymentHistoryDAO = ContractPaymentHistoryDAOs
                        .Where(x => x.Id == ContractPaymentHistory.Id && x.Id != 0).FirstOrDefault();
                    if (ContractPaymentHistoryDAO == null)
                    {
                        ContractPaymentHistoryDAO = new ContractPaymentHistoryDAO();
                        ContractPaymentHistoryDAO.Id = ContractPaymentHistory.Id;
                        ContractPaymentHistoryDAO.ContractId = Contract.Id;
                        ContractPaymentHistoryDAO.PaymentMilestone = ContractPaymentHistory.PaymentMilestone;
                        ContractPaymentHistoryDAO.PaymentPercentage = ContractPaymentHistory.PaymentPercentage;
                        ContractPaymentHistoryDAO.PaymentAmount = ContractPaymentHistory.PaymentAmount;
                        ContractPaymentHistoryDAO.Description = ContractPaymentHistory.Description;
                        ContractPaymentHistoryDAO.IsPaid = ContractPaymentHistory.IsPaid;
                        ContractPaymentHistoryDAOs.Add(ContractPaymentHistoryDAO);
                        ContractPaymentHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                        ContractPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                        ContractPaymentHistoryDAO.DeletedAt = null;
                    }
                    else
                    {
                        ContractPaymentHistoryDAO.Id = ContractPaymentHistory.Id;
                        ContractPaymentHistoryDAO.ContractId = Contract.Id;
                        ContractPaymentHistoryDAO.PaymentMilestone = ContractPaymentHistory.PaymentMilestone;
                        ContractPaymentHistoryDAO.PaymentPercentage = ContractPaymentHistory.PaymentPercentage;
                        ContractPaymentHistoryDAO.PaymentAmount = ContractPaymentHistory.PaymentAmount;
                        ContractPaymentHistoryDAO.Description = ContractPaymentHistory.Description;
                        ContractPaymentHistoryDAO.IsPaid = ContractPaymentHistory.IsPaid;
                        ContractPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                        ContractPaymentHistoryDAO.DeletedAt = null;
                    }
                }
                await DataContext.ContractPaymentHistory.BulkMergeAsync(ContractPaymentHistoryDAOs);
            }
        }
    }
}
