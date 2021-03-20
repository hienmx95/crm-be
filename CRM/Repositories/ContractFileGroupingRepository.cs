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
    public interface IContractFileGroupingRepository
    {
        Task<int> Count(ContractFileGroupingFilter ContractFileGroupingFilter);
        Task<List<ContractFileGrouping>> List(ContractFileGroupingFilter ContractFileGroupingFilter);
        Task<List<ContractFileGrouping>> List(List<long> Ids);
        Task<ContractFileGrouping> Get(long Id);
        Task<bool> Create(ContractFileGrouping ContractFileGrouping);
        Task<bool> Update(ContractFileGrouping ContractFileGrouping);
        Task<bool> Delete(ContractFileGrouping ContractFileGrouping);
        Task<bool> BulkMerge(List<ContractFileGrouping> ContractFileGroupings);
        Task<bool> BulkDelete(List<ContractFileGrouping> ContractFileGroupings);
    }
    public class ContractFileGroupingRepository : IContractFileGroupingRepository
    {
        private DataContext DataContext;
        public ContractFileGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContractFileGroupingDAO> DynamicFilter(IQueryable<ContractFileGroupingDAO> query, ContractFileGroupingFilter filter)
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
            if (filter.Title != null && filter.Title.HasValue)
                query = query.Where(q => q.Title, filter.Title);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.ContractId != null && filter.ContractId.HasValue)
                query = query.Where(q => q.ContractId, filter.ContractId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.FileTypeId != null && filter.FileTypeId.HasValue)
                query = query.Where(q => q.FileTypeId, filter.FileTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContractFileGroupingDAO> OrFilter(IQueryable<ContractFileGroupingDAO> query, ContractFileGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContractFileGroupingDAO> initQuery = query.Where(q => false);
            foreach (ContractFileGroupingFilter ContractFileGroupingFilter in filter.OrFilter)
            {
                IQueryable<ContractFileGroupingDAO> queryable = query;
                if (ContractFileGroupingFilter.Id != null && ContractFileGroupingFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContractFileGroupingFilter.Id);
                if (ContractFileGroupingFilter.Title != null && ContractFileGroupingFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, ContractFileGroupingFilter.Title);
                if (ContractFileGroupingFilter.Description != null && ContractFileGroupingFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, ContractFileGroupingFilter.Description);
                if (ContractFileGroupingFilter.ContractId != null && ContractFileGroupingFilter.ContractId.HasValue)
                    queryable = queryable.Where(q => q.ContractId, ContractFileGroupingFilter.ContractId);
                if (ContractFileGroupingFilter.CreatorId != null && ContractFileGroupingFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, ContractFileGroupingFilter.CreatorId);
                if (ContractFileGroupingFilter.FileTypeId != null && ContractFileGroupingFilter.FileTypeId.HasValue)
                    queryable = queryable.Where(q => q.FileTypeId, ContractFileGroupingFilter.FileTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContractFileGroupingDAO> DynamicOrder(IQueryable<ContractFileGroupingDAO> query, ContractFileGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContractFileGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContractFileGroupingOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case ContractFileGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ContractFileGroupingOrder.Contract:
                            query = query.OrderBy(q => q.ContractId);
                            break;
                        case ContractFileGroupingOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case ContractFileGroupingOrder.FileType:
                            query = query.OrderBy(q => q.FileTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContractFileGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContractFileGroupingOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case ContractFileGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ContractFileGroupingOrder.Contract:
                            query = query.OrderByDescending(q => q.ContractId);
                            break;
                        case ContractFileGroupingOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case ContractFileGroupingOrder.FileType:
                            query = query.OrderByDescending(q => q.FileTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContractFileGrouping>> DynamicSelect(IQueryable<ContractFileGroupingDAO> query, ContractFileGroupingFilter filter)
        {
            List<ContractFileGrouping> ContractFileGroupings = await query.Select(q => new ContractFileGrouping()
            {
                Id = filter.Selects.Contains(ContractFileGroupingSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(ContractFileGroupingSelect.Title) ? q.Title : default(string),
                Description = filter.Selects.Contains(ContractFileGroupingSelect.Description) ? q.Description : default(string),
                ContractId = filter.Selects.Contains(ContractFileGroupingSelect.Contract) ? q.ContractId : default(long),
                CreatorId = filter.Selects.Contains(ContractFileGroupingSelect.Creator) ? q.CreatorId : default(long),
                FileTypeId = filter.Selects.Contains(ContractFileGroupingSelect.FileType) ? q.FileTypeId : default(long),
                Contract = filter.Selects.Contains(ContractFileGroupingSelect.Contract) && q.Contract != null ? new Contract
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
                Creator = filter.Selects.Contains(ContractFileGroupingSelect.Creator) && q.Creator != null ? new AppUser
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
                FileType = filter.Selects.Contains(ContractFileGroupingSelect.FileType) && q.FileType != null ? new FileType
                {
                    Id = q.FileType.Id,
                    Code = q.FileType.Code,
                    Name = q.FileType.Name,
                } : null,
            }).ToListAsync();
            return ContractFileGroupings;
        }

        public async Task<int> Count(ContractFileGroupingFilter filter)
        {
            IQueryable<ContractFileGroupingDAO> ContractFileGroupings = DataContext.ContractFileGrouping.AsNoTracking();
            ContractFileGroupings = DynamicFilter(ContractFileGroupings, filter);
            return await ContractFileGroupings.CountAsync();
        }

        public async Task<List<ContractFileGrouping>> List(ContractFileGroupingFilter filter)
        {
            if (filter == null) return new List<ContractFileGrouping>();
            IQueryable<ContractFileGroupingDAO> ContractFileGroupingDAOs = DataContext.ContractFileGrouping.AsNoTracking();
            ContractFileGroupingDAOs = DynamicFilter(ContractFileGroupingDAOs, filter);
            ContractFileGroupingDAOs = DynamicOrder(ContractFileGroupingDAOs, filter);
            List<ContractFileGrouping> ContractFileGroupings = await DynamicSelect(ContractFileGroupingDAOs, filter);
            return ContractFileGroupings;
        }

        public async Task<List<ContractFileGrouping>> List(List<long> Ids)
        {
            List<ContractFileGrouping> ContractFileGroupings = await DataContext.ContractFileGrouping.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ContractFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ContractId = x.ContractId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
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
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
                },
            }).ToListAsync();
            

            return ContractFileGroupings;
        }

        public async Task<ContractFileGrouping> Get(long Id)
        {
            ContractFileGrouping ContractFileGrouping = await DataContext.ContractFileGrouping.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new ContractFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ContractId = x.ContractId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
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
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
                },
            }).FirstOrDefaultAsync();

            if (ContractFileGrouping == null)
                return null;

            return ContractFileGrouping;
        }
        public async Task<bool> Create(ContractFileGrouping ContractFileGrouping)
        {
            ContractFileGroupingDAO ContractFileGroupingDAO = new ContractFileGroupingDAO();
            ContractFileGroupingDAO.Id = ContractFileGrouping.Id;
            ContractFileGroupingDAO.Title = ContractFileGrouping.Title;
            ContractFileGroupingDAO.Description = ContractFileGrouping.Description;
            ContractFileGroupingDAO.ContractId = ContractFileGrouping.ContractId;
            ContractFileGroupingDAO.CreatorId = ContractFileGrouping.CreatorId;
            ContractFileGroupingDAO.FileTypeId = ContractFileGrouping.FileTypeId;
            ContractFileGroupingDAO.RowId = ContractFileGrouping.RowId;
            ContractFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
            ContractFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.ContractFileGrouping.Add(ContractFileGroupingDAO);
            await DataContext.SaveChangesAsync();
            ContractFileGrouping.Id = ContractFileGroupingDAO.Id;
            await SaveReference(ContractFileGrouping);
            return true;
        }

        public async Task<bool> Update(ContractFileGrouping ContractFileGrouping)
        {
            ContractFileGroupingDAO ContractFileGroupingDAO = DataContext.ContractFileGrouping.Where(x => x.Id == ContractFileGrouping.Id).FirstOrDefault();
            if (ContractFileGroupingDAO == null)
                return false;
            ContractFileGroupingDAO.Id = ContractFileGrouping.Id;
            ContractFileGroupingDAO.Title = ContractFileGrouping.Title;
            ContractFileGroupingDAO.Description = ContractFileGrouping.Description;
            ContractFileGroupingDAO.ContractId = ContractFileGrouping.ContractId;
            ContractFileGroupingDAO.CreatorId = ContractFileGrouping.CreatorId;
            ContractFileGroupingDAO.FileTypeId = ContractFileGrouping.FileTypeId;
            ContractFileGroupingDAO.RowId = ContractFileGrouping.RowId;
            ContractFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(ContractFileGrouping);
            return true;
        }

        public async Task<bool> Delete(ContractFileGrouping ContractFileGrouping)
        {
            await DataContext.ContractFileGrouping.Where(x => x.Id == ContractFileGrouping.Id).UpdateFromQueryAsync(x => new ContractFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ContractFileGrouping> ContractFileGroupings)
        {
            List<ContractFileGroupingDAO> ContractFileGroupingDAOs = new List<ContractFileGroupingDAO>();
            foreach (ContractFileGrouping ContractFileGrouping in ContractFileGroupings)
            {
                ContractFileGroupingDAO ContractFileGroupingDAO = new ContractFileGroupingDAO();
                ContractFileGroupingDAO.Id = ContractFileGrouping.Id;
                ContractFileGroupingDAO.Title = ContractFileGrouping.Title;
                ContractFileGroupingDAO.Description = ContractFileGrouping.Description;
                ContractFileGroupingDAO.ContractId = ContractFileGrouping.ContractId;
                ContractFileGroupingDAO.CreatorId = ContractFileGrouping.CreatorId;
                ContractFileGroupingDAO.FileTypeId = ContractFileGrouping.FileTypeId;
                ContractFileGroupingDAO.RowId = ContractFileGrouping.RowId;
                ContractFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
                ContractFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContractFileGroupingDAOs.Add(ContractFileGroupingDAO);
            }
            await DataContext.BulkMergeAsync(ContractFileGroupingDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ContractFileGrouping> ContractFileGroupings)
        {
            List<long> Ids = ContractFileGroupings.Select(x => x.Id).ToList();
            await DataContext.ContractFileGrouping
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContractFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(ContractFileGrouping ContractFileGrouping)
        {
        }
        
    }
}
