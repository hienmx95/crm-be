using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface IContractPaymentHistoryRepository
    {
        Task<int> Count(ContractPaymentHistoryFilter ContractPaymentHistoryFilter);
        Task<List<ContractPaymentHistory>> List(ContractPaymentHistoryFilter ContractPaymentHistoryFilter);
        Task<ContractPaymentHistory> Get(long Id);
        Task<bool> Create(ContractPaymentHistory ContractPaymentHistory);
        Task<bool> Update(ContractPaymentHistory ContractPaymentHistory);
        Task<bool> Delete(ContractPaymentHistory ContractPaymentHistory);
        Task<bool> BulkMerge(List<ContractPaymentHistory> ContractPaymentHistories);
        Task<bool> BulkDelete(List<ContractPaymentHistory> ContractPaymentHistories);
    }
    public class ContractPaymentHistoryRepository : IContractPaymentHistoryRepository
    {
        private DataContext DataContext;
        public ContractPaymentHistoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContractPaymentHistoryDAO> DynamicFilter(IQueryable<ContractPaymentHistoryDAO> query, ContractPaymentHistoryFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.ContractId != null)
                query = query.Where(q => q.ContractId, filter.ContractId);
            if (filter.PaymentMilestone != null)
                query = query.Where(q => q.PaymentMilestone, filter.PaymentMilestone);
            if (filter.PaymentPercentage != null)
                query = query.Where(q => q.PaymentPercentage, filter.PaymentPercentage);
            if (filter.PaymentAmount != null)
                query = query.Where(q => q.PaymentAmount, filter.PaymentAmount);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<ContractPaymentHistoryDAO> OrFilter(IQueryable<ContractPaymentHistoryDAO> query, ContractPaymentHistoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContractPaymentHistoryDAO> initQuery = query.Where(q => false);
            foreach (ContractPaymentHistoryFilter ContractPaymentHistoryFilter in filter.OrFilter)
            {
                IQueryable<ContractPaymentHistoryDAO> queryable = query;
                if (ContractPaymentHistoryFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, ContractPaymentHistoryFilter.Id);
                if (ContractPaymentHistoryFilter.ContractId != null)
                    queryable = queryable.Where(q => q.ContractId, ContractPaymentHistoryFilter.ContractId);
                if (ContractPaymentHistoryFilter.PaymentMilestone != null)
                    queryable = queryable.Where(q => q.PaymentMilestone, ContractPaymentHistoryFilter.PaymentMilestone);
                if (ContractPaymentHistoryFilter.PaymentPercentage != null)
                    queryable = queryable.Where(q => q.PaymentPercentage, ContractPaymentHistoryFilter.PaymentPercentage);
                if (ContractPaymentHistoryFilter.PaymentAmount != null)
                    queryable = queryable.Where(q => q.PaymentAmount, ContractPaymentHistoryFilter.PaymentAmount);
                if (ContractPaymentHistoryFilter.Description != null)
                    queryable = queryable.Where(q => q.Description, ContractPaymentHistoryFilter.Description);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContractPaymentHistoryDAO> DynamicOrder(IQueryable<ContractPaymentHistoryDAO> query, ContractPaymentHistoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContractPaymentHistoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContractPaymentHistoryOrder.Contract:
                            query = query.OrderBy(q => q.ContractId);
                            break;
                        case ContractPaymentHistoryOrder.PaymentMilestone:
                            query = query.OrderBy(q => q.PaymentMilestone);
                            break;
                        case ContractPaymentHistoryOrder.PaymentPercentage:
                            query = query.OrderBy(q => q.PaymentPercentage);
                            break;
                        case ContractPaymentHistoryOrder.PaymentAmount:
                            query = query.OrderBy(q => q.PaymentAmount);
                            break;
                        case ContractPaymentHistoryOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContractPaymentHistoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContractPaymentHistoryOrder.Contract:
                            query = query.OrderByDescending(q => q.ContractId);
                            break;
                        case ContractPaymentHistoryOrder.PaymentMilestone:
                            query = query.OrderByDescending(q => q.PaymentMilestone);
                            break;
                        case ContractPaymentHistoryOrder.PaymentPercentage:
                            query = query.OrderByDescending(q => q.PaymentPercentage);
                            break;
                        case ContractPaymentHistoryOrder.PaymentAmount:
                            query = query.OrderByDescending(q => q.PaymentAmount);
                            break;
                        case ContractPaymentHistoryOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContractPaymentHistory>> DynamicSelect(IQueryable<ContractPaymentHistoryDAO> query, ContractPaymentHistoryFilter filter)
        {
            List<ContractPaymentHistory> ContractPaymentHistories = await query.Select(q => new ContractPaymentHistory()
            {
                Id = filter.Selects.Contains(ContractPaymentHistorySelect.Id) ? q.Id : default(long),
                ContractId = filter.Selects.Contains(ContractPaymentHistorySelect.Contract) ? q.ContractId : default(long),
                PaymentMilestone = filter.Selects.Contains(ContractPaymentHistorySelect.PaymentMilestone) ? q.PaymentMilestone : default(string),
                PaymentPercentage = filter.Selects.Contains(ContractPaymentHistorySelect.PaymentPercentage) ? q.PaymentPercentage : default(decimal?),
                PaymentAmount = filter.Selects.Contains(ContractPaymentHistorySelect.PaymentAmount) ? q.PaymentAmount : default(decimal?),
                Description = filter.Selects.Contains(ContractPaymentHistorySelect.Description) ? q.Description : default(string),
                IsPaid = filter.Selects.Contains(ContractPaymentHistorySelect.IsPaid) ? q.IsPaid : default(bool),
                Contract = filter.Selects.Contains(ContractPaymentHistorySelect.Contract) && q.Contract != null ? new Contract
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
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return ContractPaymentHistories;
        }

        public async Task<int> Count(ContractPaymentHistoryFilter filter)
        {
            IQueryable<ContractPaymentHistoryDAO> ContractPaymentHistories = DataContext.ContractPaymentHistory.AsNoTracking();
            ContractPaymentHistories = DynamicFilter(ContractPaymentHistories, filter);
            return await ContractPaymentHistories.CountAsync();
        }

        public async Task<List<ContractPaymentHistory>> List(ContractPaymentHistoryFilter filter)
        {
            if (filter == null) return new List<ContractPaymentHistory>();
            IQueryable<ContractPaymentHistoryDAO> ContractPaymentHistoryDAOs = DataContext.ContractPaymentHistory.AsNoTracking();
            ContractPaymentHistoryDAOs = DynamicFilter(ContractPaymentHistoryDAOs, filter);
            ContractPaymentHistoryDAOs = DynamicOrder(ContractPaymentHistoryDAOs, filter);
            List<ContractPaymentHistory> ContractPaymentHistories = await DynamicSelect(ContractPaymentHistoryDAOs, filter);
            return ContractPaymentHistories;
        }

        public async Task<ContractPaymentHistory> Get(long Id)
        {
            ContractPaymentHistory ContractPaymentHistory = await DataContext.ContractPaymentHistory.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new ContractPaymentHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                ContractId = x.ContractId,
                PaymentMilestone = x.PaymentMilestone,
                PaymentPercentage = x.PaymentPercentage,
                PaymentAmount = x.PaymentAmount,
                Description = x.Description,
                IsPaid = x.IsPaid,
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
                },
            }).FirstOrDefaultAsync();

            if (ContractPaymentHistory == null)
                return null;

            return ContractPaymentHistory;
        }
        public async Task<bool> Create(ContractPaymentHistory ContractPaymentHistory)
        {
            ContractPaymentHistoryDAO ContractPaymentHistoryDAO = new ContractPaymentHistoryDAO();
            ContractPaymentHistoryDAO.Id = ContractPaymentHistory.Id;
            ContractPaymentHistoryDAO.ContractId = ContractPaymentHistory.ContractId;
            ContractPaymentHistoryDAO.PaymentMilestone = ContractPaymentHistory.PaymentMilestone;
            ContractPaymentHistoryDAO.PaymentPercentage = ContractPaymentHistory.PaymentPercentage;
            ContractPaymentHistoryDAO.PaymentAmount = ContractPaymentHistory.PaymentAmount;
            ContractPaymentHistoryDAO.Description = ContractPaymentHistory.Description;
            ContractPaymentHistoryDAO.IsPaid = ContractPaymentHistory.IsPaid;
            ContractPaymentHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
            ContractPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.ContractPaymentHistory.Add(ContractPaymentHistoryDAO);
            await DataContext.SaveChangesAsync();
            ContractPaymentHistory.Id = ContractPaymentHistoryDAO.Id;
            await SaveReference(ContractPaymentHistory);
            return true;
        }

        public async Task<bool> Update(ContractPaymentHistory ContractPaymentHistory)
        {
            ContractPaymentHistoryDAO ContractPaymentHistoryDAO = DataContext.ContractPaymentHistory.Where(x => x.Id == ContractPaymentHistory.Id).FirstOrDefault();
            if (ContractPaymentHistoryDAO == null)
                return false;
            ContractPaymentHistoryDAO.Id = ContractPaymentHistory.Id;
            ContractPaymentHistoryDAO.ContractId = ContractPaymentHistory.ContractId;
            ContractPaymentHistoryDAO.PaymentMilestone = ContractPaymentHistory.PaymentMilestone;
            ContractPaymentHistoryDAO.PaymentPercentage = ContractPaymentHistory.PaymentPercentage;
            ContractPaymentHistoryDAO.PaymentAmount = ContractPaymentHistory.PaymentAmount;
            ContractPaymentHistoryDAO.Description = ContractPaymentHistory.Description;
            ContractPaymentHistoryDAO.IsPaid = ContractPaymentHistory.IsPaid;
            ContractPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(ContractPaymentHistory);
            return true;
        }

        public async Task<bool> Delete(ContractPaymentHistory ContractPaymentHistory)
        {
            await DataContext.ContractPaymentHistory.Where(x => x.Id == ContractPaymentHistory.Id).UpdateFromQueryAsync(x => new ContractPaymentHistoryDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ContractPaymentHistory> ContractPaymentHistories)
        {
            List<ContractPaymentHistoryDAO> ContractPaymentHistoryDAOs = new List<ContractPaymentHistoryDAO>();
            foreach (ContractPaymentHistory ContractPaymentHistory in ContractPaymentHistories)
            {
                ContractPaymentHistoryDAO ContractPaymentHistoryDAO = new ContractPaymentHistoryDAO();
                ContractPaymentHistoryDAO.Id = ContractPaymentHistory.Id;
                ContractPaymentHistoryDAO.ContractId = ContractPaymentHistory.ContractId;
                ContractPaymentHistoryDAO.PaymentMilestone = ContractPaymentHistory.PaymentMilestone;
                ContractPaymentHistoryDAO.PaymentPercentage = ContractPaymentHistory.PaymentPercentage;
                ContractPaymentHistoryDAO.PaymentAmount = ContractPaymentHistory.PaymentAmount;
                ContractPaymentHistoryDAO.Description = ContractPaymentHistory.Description;
                ContractPaymentHistoryDAO.IsPaid = ContractPaymentHistory.IsPaid;
                ContractPaymentHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                ContractPaymentHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContractPaymentHistoryDAOs.Add(ContractPaymentHistoryDAO);
            }
            await DataContext.BulkMergeAsync(ContractPaymentHistoryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ContractPaymentHistory> ContractPaymentHistories)
        {
            List<long> Ids = ContractPaymentHistories.Select(x => x.Id).ToList();
            await DataContext.ContractPaymentHistory
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContractPaymentHistoryDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(ContractPaymentHistory ContractPaymentHistory)
        {
        }
        
    }
}
