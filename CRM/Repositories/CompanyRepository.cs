using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using System.ComponentModel;
using CRM.Enums;
using System.Reflection;

namespace CRM.Repositories
{
    public interface ICompanyRepository
    {
        Task<int> Count(CompanyFilter CompanyFilter);
        Task<List<Company>> List(CompanyFilter CompanyFilter);
        Task<Company> Get(long Id);
        Task<bool> Create(Company Company);
        Task<bool> Update(Company Company);
        Task<bool> Delete(Company Company);
        Task<bool> BulkMerge(List<Company> Companys);
        Task<bool> BulkDelete(List<Company> Companys);
    }
    public class CompanyRepository : ICompanyRepository
    {
        private DataContext DataContext;
        public CompanyRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CompanyDAO> DynamicFilter(IQueryable<CompanyDAO> query, CompanyFilter filter)
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
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Phone != null && filter.Phone.HasValue)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.FAX != null && filter.FAX.HasValue)
                query = query.Where(q => q.FAX, filter.FAX);
            if (filter.PhoneOther != null && filter.PhoneOther.HasValue)
                query = query.Where(q => q.PhoneOther, filter.PhoneOther);
            if (filter.Email != null && filter.Email.HasValue)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.EmailOther != null && filter.EmailOther.HasValue)
                query = query.Where(q => q.EmailOther, filter.EmailOther);
            if (filter.ZIPCode != null && filter.ZIPCode.HasValue)
                query = query.Where(q => q.ZIPCode, filter.ZIPCode);
            if (filter.Revenue != null && filter.Revenue.HasValue)
                query = query.Where(q => q.Revenue.HasValue).Where(q => q.Revenue, filter.Revenue);
            if (filter.Website != null && filter.Website.HasValue)
                query = query.Where(q => q.Website, filter.Website);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.NationId != null && filter.NationId.HasValue)
                query = query.Where(q => q.NationId.HasValue).Where(q => q.NationId, filter.NationId);
            if (filter.ProvinceId != null && filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null && filter.DistrictId.HasValue)
                query = query.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, filter.DistrictId);
            if (filter.NumberOfEmployee != null && filter.NumberOfEmployee.HasValue)
                query = query.Where(q => q.NumberOfEmployee.HasValue).Where(q => q.NumberOfEmployee, filter.NumberOfEmployee);
            if (filter.CustomerLeadId != null && filter.CustomerLeadId.HasValue)
                query = query.Where(q => q.CustomerLeadId.HasValue).Where(q => q.CustomerLeadId, filter.CustomerLeadId);
            if (filter.ParentId != null && filter.ParentId.HasValue)
                query = query.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, filter.ParentId);
            if (filter.Path != null && filter.Path.HasValue)
                query = query.Where(q => q.Path, filter.Path);
            if (filter.Level != null && filter.Level.HasValue)
                query = query.Where(q => q.Level.HasValue).Where(q => q.Level, filter.Level);
            if (filter.ProfessionId != null && filter.ProfessionId.HasValue)
                query = query.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, filter.ProfessionId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            //if (filter.OrganizationId != null)
            //{
            //    if (filter.OrganizationId.Equal != null)
            //    {
            //        OrganizationDAO OrganizationDAO = DataContext.Organization
            //            .Where(o => o.Id == filter.OrganizationId.Equal.Value).FirstOrDefault();
            //        query = query.Where(q => q.Organization.Path.StartsWith(OrganizationDAO.Path));
            //    }
            //    if (filter.OrganizationId.NotEqual != null)
            //    {
            //        OrganizationDAO OrganizationDAO = DataContext.Organization
            //            .Where(o => o.Id == filter.OrganizationId.NotEqual.Value).FirstOrDefault();
            //        query = query.Where(q => !q.Organization.Path.StartsWith(OrganizationDAO.Path));
            //    }
            //    if (filter.OrganizationId.In != null)
            //    {
            //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
            //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
            //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => filter.OrganizationId.In.Contains(o.Id)).ToList();
            //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
            //        List<long> Ids = Branches.Select(o => o.Id).ToList();
            //        query = query.Where(q => Ids.Contains(q.OrganizationId));
            //    }
            //    if (filter.OrganizationId.NotIn != null)
            //    {
            //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
            //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
            //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => filter.OrganizationId.NotIn.Contains(o.Id)).ToList();
            //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
            //        List<long> Ids = Branches.Select(o => o.Id).ToList();
            //        query = query.Where(q => !Ids.Contains(q.OrganizationId));
            //    }

            //}
           
            
            if (filter.CurrencyId != null && filter.CurrencyId.HasValue)
                query = query.Where(q => q.CurrencyId.HasValue).Where(q => q.CurrencyId, filter.CurrencyId);
            if (filter.CompanyStatusId != null && filter.CompanyStatusId.HasValue)
                query = query.Where(q => q.CompanyStatusId.HasValue).Where(q => q.CompanyStatusId, filter.CompanyStatusId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CompanyDAO> OrFilter(IQueryable<CompanyDAO> query, CompanyFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CompanyDAO> initQuery = query.Where(q => false);
            foreach (CompanyFilter CompanyFilter in filter.OrFilter)
            {
                IQueryable<CompanyDAO> queryable = query;
                if (CompanyFilter.Id != null && CompanyFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CompanyFilter.Id);
                if (CompanyFilter.Name != null && CompanyFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CompanyFilter.Name);
                if (CompanyFilter.Phone != null && CompanyFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, CompanyFilter.Phone);
                if (CompanyFilter.FAX != null && CompanyFilter.FAX.HasValue)
                    queryable = queryable.Where(q => q.FAX, CompanyFilter.FAX);
                if (CompanyFilter.PhoneOther != null && CompanyFilter.PhoneOther.HasValue)
                    queryable = queryable.Where(q => q.PhoneOther, CompanyFilter.PhoneOther);
                if (CompanyFilter.Email != null && CompanyFilter.Email.HasValue)
                    queryable = queryable.Where(q => q.Email, CompanyFilter.Email);
                if (CompanyFilter.EmailOther != null && CompanyFilter.EmailOther.HasValue)
                    queryable = queryable.Where(q => q.EmailOther, CompanyFilter.EmailOther);
                if (CompanyFilter.ZIPCode != null && CompanyFilter.ZIPCode.HasValue)
                    queryable = queryable.Where(q => q.ZIPCode, CompanyFilter.ZIPCode);
                if (CompanyFilter.Revenue != null && CompanyFilter.Revenue.HasValue)
                    queryable = queryable.Where(q => q.Revenue.HasValue).Where(q => q.Revenue, CompanyFilter.Revenue);
                if (CompanyFilter.Website != null && CompanyFilter.Website.HasValue)
                    queryable = queryable.Where(q => q.Website, CompanyFilter.Website);
                if (CompanyFilter.Address != null && CompanyFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, CompanyFilter.Address);
                if (CompanyFilter.NationId != null && CompanyFilter.NationId.HasValue)
                    queryable = queryable.Where(q => q.NationId.HasValue).Where(q => q.NationId, CompanyFilter.NationId);
                if (CompanyFilter.ProvinceId != null && CompanyFilter.ProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, CompanyFilter.ProvinceId);
                if (CompanyFilter.DistrictId != null && CompanyFilter.DistrictId.HasValue)
                    queryable = queryable.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, CompanyFilter.DistrictId);
                if (CompanyFilter.NumberOfEmployee != null && CompanyFilter.NumberOfEmployee.HasValue)
                    queryable = queryable.Where(q => q.NumberOfEmployee.HasValue).Where(q => q.NumberOfEmployee, CompanyFilter.NumberOfEmployee);
                if (CompanyFilter.CustomerLeadId != null && CompanyFilter.CustomerLeadId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadId.HasValue).Where(q => q.CustomerLeadId, CompanyFilter.CustomerLeadId);
                if (CompanyFilter.ParentId != null && CompanyFilter.ParentId.HasValue)
                    queryable = queryable.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, CompanyFilter.ParentId);
                if (CompanyFilter.Path != null && CompanyFilter.Path.HasValue)
                    queryable = queryable.Where(q => q.Path, CompanyFilter.Path);
                if (CompanyFilter.Level != null && CompanyFilter.Level.HasValue)
                    queryable = queryable.Where(q => q.Level.HasValue).Where(q => q.Level, CompanyFilter.Level);
                if (CompanyFilter.ProfessionId != null && CompanyFilter.ProfessionId.HasValue)
                    queryable = queryable.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, CompanyFilter.ProfessionId);
                if (CompanyFilter.AppUserId != null && CompanyFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, CompanyFilter.AppUserId);
                if (CompanyFilter.CreatorId != null && CompanyFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CompanyFilter.CreatorId);
               
                //if (CompanyFilter.OrganizationId != null)
                //{
                //    if (CompanyFilter.OrganizationId.Equal != null)
                //    {
                //        OrganizationDAO OrganizationDAO = DataContext.Organization
                //            .Where(o => o.Id == CompanyFilter.OrganizationId.Equal.Value).FirstOrDefault();
                //        queryable = queryable.Where(q => q.Organization.Path.StartsWith(OrganizationDAO.Path));
                //    }
                //    if (CompanyFilter.OrganizationId.NotEqual != null)
                //    {
                //        OrganizationDAO OrganizationDAO = DataContext.Organization
                //            .Where(o => o.Id == CompanyFilter.OrganizationId.NotEqual.Value).FirstOrDefault();
                //        queryable = queryable.Where(q => !q.Organization.Path.StartsWith(OrganizationDAO.Path));
                //    }
                //    if (CompanyFilter.OrganizationId.In != null)
                //    {
                //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => CompanyFilter.OrganizationId.In.Contains(o.Id)).ToList();
                //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                //        List<long> Ids = Branches.Select(o => o.Id).ToList();
                //        queryable = queryable.Where(q => Ids.Contains(q.OrganizationId));
                //    }
                //    if (CompanyFilter.OrganizationId.NotIn != null)
                //    {
                //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => CompanyFilter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                //        List<long> Ids = Branches.Select(o => o.Id).ToList();
                //        queryable = queryable.Where(q => !Ids.Contains(q.OrganizationId));
                //    }
                //}
                
                if (CompanyFilter.CurrencyId != null && CompanyFilter.CurrencyId.HasValue)
                    queryable = queryable.Where(q => q.CurrencyId.HasValue).Where(q => q.CurrencyId, CompanyFilter.CurrencyId);
                if (CompanyFilter.CompanyStatusId != null && CompanyFilter.CompanyStatusId.HasValue)
                    queryable = queryable.Where(q => q.CompanyStatusId.HasValue).Where(q => q.CompanyStatusId, CompanyFilter.CompanyStatusId);
                if (CompanyFilter.Description != null && CompanyFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CompanyFilter.Description);
                if (CompanyFilter.RowId != null && CompanyFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CompanyFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CompanyDAO> DynamicOrder(IQueryable<CompanyDAO> query, CompanyFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CompanyOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CompanyOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CompanyOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case CompanyOrder.FAX:
                            query = query.OrderBy(q => q.FAX);
                            break;
                        case CompanyOrder.PhoneOther:
                            query = query.OrderBy(q => q.PhoneOther);
                            break;
                        case CompanyOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CompanyOrder.EmailOther:
                            query = query.OrderBy(q => q.EmailOther);
                            break;
                        case CompanyOrder.ZIPCode:
                            query = query.OrderBy(q => q.ZIPCode);
                            break;
                        case CompanyOrder.Revenue:
                            query = query.OrderBy(q => q.Revenue);
                            break;
                        case CompanyOrder.Website:
                            query = query.OrderBy(q => q.Website);
                            break;
                        case CompanyOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CompanyOrder.Nation:
                            query = query.OrderBy(q => q.NationId);
                            break;
                        case CompanyOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case CompanyOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case CompanyOrder.NumberOfEmployee:
                            query = query.OrderBy(q => q.NumberOfEmployee);
                            break;
                        case CompanyOrder.RefuseReciveEmail:
                            query = query.OrderBy(q => q.RefuseReciveEmail);
                            break;
                        case CompanyOrder.RefuseReciveSMS:
                            query = query.OrderBy(q => q.RefuseReciveSMS);
                            break;
                        case CompanyOrder.CustomerLead:
                            query = query.OrderBy(q => q.CustomerLeadId);
                            break;
                        case CompanyOrder.Parent:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        case CompanyOrder.Path:
                            query = query.OrderBy(q => q.Path);
                            break;
                        case CompanyOrder.Level:
                            query = query.OrderBy(q => q.Level);
                            break;
                        case CompanyOrder.Profession:
                            query = query.OrderBy(q => q.ProfessionId);
                            break;
                        case CompanyOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case CompanyOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CompanyOrder.Currency:
                            query = query.OrderBy(q => q.CurrencyId);
                            break;
                        case CompanyOrder.CompanyStatus:
                            query = query.OrderBy(q => q.CompanyStatusId);
                            break;
                        case CompanyOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CompanyOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CompanyOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CompanyOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CompanyOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case CompanyOrder.FAX:
                            query = query.OrderByDescending(q => q.FAX);
                            break;
                        case CompanyOrder.PhoneOther:
                            query = query.OrderByDescending(q => q.PhoneOther);
                            break;
                        case CompanyOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CompanyOrder.EmailOther:
                            query = query.OrderByDescending(q => q.EmailOther);
                            break;
                        case CompanyOrder.ZIPCode:
                            query = query.OrderByDescending(q => q.ZIPCode);
                            break;
                        case CompanyOrder.Revenue:
                            query = query.OrderByDescending(q => q.Revenue);
                            break;
                        case CompanyOrder.Website:
                            query = query.OrderByDescending(q => q.Website);
                            break;
                        case CompanyOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CompanyOrder.Nation:
                            query = query.OrderByDescending(q => q.NationId);
                            break;
                        case CompanyOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case CompanyOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case CompanyOrder.NumberOfEmployee:
                            query = query.OrderByDescending(q => q.NumberOfEmployee);
                            break;
                        case CompanyOrder.RefuseReciveEmail:
                            query = query.OrderByDescending(q => q.RefuseReciveEmail);
                            break;
                        case CompanyOrder.RefuseReciveSMS:
                            query = query.OrderByDescending(q => q.RefuseReciveSMS);
                            break;
                        case CompanyOrder.CustomerLead:
                            query = query.OrderByDescending(q => q.CustomerLeadId);
                            break;
                        case CompanyOrder.Parent:
                            query = query.OrderByDescending(q => q.ParentId);
                            break;
                        case CompanyOrder.Path:
                            query = query.OrderByDescending(q => q.Path);
                            break;
                        case CompanyOrder.Level:
                            query = query.OrderByDescending(q => q.Level);
                            break;
                        case CompanyOrder.Profession:
                            query = query.OrderByDescending(q => q.ProfessionId);
                            break;
                        case CompanyOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case CompanyOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CompanyOrder.Currency:
                            query = query.OrderByDescending(q => q.CurrencyId);
                            break;
                        case CompanyOrder.CompanyStatus:
                            query = query.OrderByDescending(q => q.CompanyStatusId);
                            break;
                        case CompanyOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CompanyOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Company>> DynamicSelect(IQueryable<CompanyDAO> query, CompanyFilter filter)
        {
            List<Company> Companies = await query.Select(q => new Company()
            {
                Id = filter.Selects.Contains(CompanySelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(CompanySelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(CompanySelect.Phone) ? q.Phone : default(string),
                FAX = filter.Selects.Contains(CompanySelect.FAX) ? q.FAX : default(string),
                PhoneOther = filter.Selects.Contains(CompanySelect.PhoneOther) ? q.PhoneOther : default(string),
                Email = filter.Selects.Contains(CompanySelect.Email) ? q.Email : default(string),
                EmailOther = filter.Selects.Contains(CompanySelect.EmailOther) ? q.EmailOther : default(string),
                ZIPCode = filter.Selects.Contains(CompanySelect.ZIPCode) ? q.ZIPCode : default(string),
                Revenue = filter.Selects.Contains(CompanySelect.Revenue) ? q.Revenue : default(decimal?),
                Website = filter.Selects.Contains(CompanySelect.Website) ? q.Website : default(string),
                Address = filter.Selects.Contains(CompanySelect.Address) ? q.Address : default(string),
                NationId = filter.Selects.Contains(CompanySelect.Nation) ? q.NationId : default(long?),
                ProvinceId = filter.Selects.Contains(CompanySelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(CompanySelect.District) ? q.DistrictId : default(long?),
                NumberOfEmployee = filter.Selects.Contains(CompanySelect.NumberOfEmployee) ? q.NumberOfEmployee : default(long?),
                RefuseReciveEmail = filter.Selects.Contains(CompanySelect.RefuseReciveEmail) ? q.RefuseReciveEmail : default(bool?),
                RefuseReciveSMS = filter.Selects.Contains(CompanySelect.RefuseReciveSMS) ? q.RefuseReciveSMS : default(bool?),
                CustomerLeadId = filter.Selects.Contains(CompanySelect.CustomerLead) ? q.CustomerLeadId : default(long?),
                ParentId = filter.Selects.Contains(CompanySelect.Parent) ? q.ParentId : default(long?),
                Path = filter.Selects.Contains(CompanySelect.Path) ? q.Path : default(string),
                Level = filter.Selects.Contains(CompanySelect.Level) ? q.Level : default(long?),
                ProfessionId = filter.Selects.Contains(CompanySelect.Profession) ? q.ProfessionId : default(long?),
                AppUserId = filter.Selects.Contains(CompanySelect.AppUser) ? q.AppUserId : default(long?),
                CreatorId = filter.Selects.Contains(CompanySelect.Creator) ? q.CreatorId : default(long),
                //OrganizationId = filter.Selects.Contains(CompanySelect.Organization) ? q.OrganizationId : default(long),
                CurrencyId = filter.Selects.Contains(CompanySelect.Currency) ? q.CurrencyId : default(long?),
                CompanyStatusId = filter.Selects.Contains(CompanySelect.CompanyStatus) ? q.CompanyStatusId : default(long?),
                Description = filter.Selects.Contains(CompanySelect.Description) ? q.Description : default(string),
                RowId = filter.Selects.Contains(CompanySelect.Row) ? q.RowId : default(Guid),
                AppUser = filter.Selects.Contains(CompanySelect.AppUser) && q.AppUser != null ? new AppUser
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
                CompanyStatus = filter.Selects.Contains(CompanySelect.CompanyStatus) && q.CompanyStatus != null ? new CompanyStatus
                {
                    Id = q.CompanyStatus.Id,
                    Code = q.CompanyStatus.Code,
                    Name = q.CompanyStatus.Name,
                } : null,
                Creator = filter.Selects.Contains(CompanySelect.Creator) && q.Creator != null ? new AppUser
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
                Currency = filter.Selects.Contains(CompanySelect.Currency) && q.Currency != null ? new Currency
                {
                    Id = q.Currency.Id,
                    Code = q.Currency.Code,
                    Name = q.Currency.Name,
                } : null,
                CustomerLead = filter.Selects.Contains(CompanySelect.CustomerLead) && q.CustomerLead != null ? new CustomerLead
                {
                    Id = q.CustomerLead.Id,
                    Name = q.CustomerLead.Name,
                    CompanyName = q.CustomerLead.CompanyName,
                    TelePhone = q.CustomerLead.TelePhone,
                    Phone = q.CustomerLead.Phone,
                    Fax = q.CustomerLead.Fax,
                    Email = q.CustomerLead.Email,
                    SecondEmail = q.CustomerLead.SecondEmail,
                    Website = q.CustomerLead.Website,
                    CustomerLeadSourceId = q.CustomerLead.CustomerLeadSourceId,
                    CustomerLeadLevelId = q.CustomerLead.CustomerLeadLevelId,
                    CompanyId = q.CustomerLead.CompanyId,
                    CampaignId = q.CustomerLead.CampaignId,
                    ProfessionId = q.CustomerLead.ProfessionId,
                    Revenue = q.CustomerLead.Revenue,
                    EmployeeQuantity = q.CustomerLead.EmployeeQuantity,
                    Address = q.CustomerLead.Address,
                    NationId = q.CustomerLead.NationId,
                    ProvinceId = q.CustomerLead.ProvinceId,
                    DistrictId = q.CustomerLead.DistrictId,
                    CustomerLeadStatusId = q.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = q.CustomerLead.BusinessRegistrationCode,
                    SexId = q.CustomerLead.SexId,
                    RefuseReciveSMS = q.CustomerLead.RefuseReciveSMS,
                    RefuseReciveEmail = q.CustomerLead.RefuseReciveEmail,
                    Description = q.CustomerLead.Description,
                    AppUserId = q.CustomerLead.AppUserId,
                    CreatorId = q.CustomerLead.CreatorId,
                    ZipCode = q.CustomerLead.ZipCode,
                    CurrencyId = q.CustomerLead.CurrencyId,
                    RowId = q.CustomerLead.RowId,
                } : null,
                District = filter.Selects.Contains(CompanySelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Code = q.District.Code,
                    Name = q.District.Name,
                    Priority = q.District.Priority,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                    RowId = q.District.RowId,
                    Used = q.District.Used,
                } : null,
                Nation = filter.Selects.Contains(CompanySelect.Nation) && q.Nation != null ? new Nation
                {
                    Id = q.Nation.Id,
                    Code = q.Nation.Code,
                    Name = q.Nation.Name,
                    Priority = q.Nation.Priority,
                    StatusId = q.Nation.StatusId,
                    Used = q.Nation.Used,
                    RowId = q.Nation.RowId,
                } : null,
                //Organization = filter.Selects.Contains(CompanySelect.Organization) && q.Organization != null ? new Organization
                //{
                //    Id = q.Organization.Id,
                //    Code = q.Organization.Code,
                //    Name = q.Organization.Name,
                //    Address = q.Organization.Address,
                //    Phone = q.Organization.Phone,
                //    Path = q.Organization.Path,
                //    ParentId = q.Organization.ParentId,
                //    Email = q.Organization.Email,
                //    StatusId = q.Organization.StatusId,
                //    Level = q.Organization.Level
                //} : null,
                Parent = filter.Selects.Contains(CompanySelect.Parent) && q.Parent != null ? new Company
                {
                    Id = q.Parent.Id,
                    Name = q.Parent.Name,
                    Phone = q.Parent.Phone,
                    FAX = q.Parent.FAX,
                    PhoneOther = q.Parent.PhoneOther,
                    Email = q.Parent.Email,
                    EmailOther = q.Parent.EmailOther,
                    ZIPCode = q.Parent.ZIPCode,
                    Revenue = q.Parent.Revenue,
                    Website = q.Parent.Website,
                    Address = q.Parent.Address,
                    NationId = q.Parent.NationId,
                    ProvinceId = q.Parent.ProvinceId,
                    DistrictId = q.Parent.DistrictId,
                    NumberOfEmployee = q.Parent.NumberOfEmployee,
                    RefuseReciveEmail = q.Parent.RefuseReciveEmail,
                    RefuseReciveSMS = q.Parent.RefuseReciveSMS,
                    CustomerLeadId = q.Parent.CustomerLeadId,
                    ParentId = q.Parent.ParentId,
                    Path = q.Parent.Path,
                    Level = q.Parent.Level,
                    ProfessionId = q.Parent.ProfessionId,
                    AppUserId = q.Parent.AppUserId,
                    CreatorId = q.Parent.CreatorId,
                    CurrencyId = q.Parent.CurrencyId,
                    CompanyStatusId = q.Parent.CompanyStatusId,
                    Description = q.Parent.Description,
                    RowId = q.Parent.RowId,
                } : null,
                Profession = filter.Selects.Contains(CompanySelect.Profession) && q.Profession != null ? new Profession
                {
                    Id = q.Profession.Id,
                    Code = q.Profession.Code,
                    Name = q.Profession.Name,
                    StatusId = q.Profession.StatusId,
                    RowId = q.Profession.RowId,
                    Used = q.Profession.Used,
                } : null,
                Province = filter.Selects.Contains(CompanySelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    RowId = q.Province.RowId,
                    Used = q.Province.Used,
                } : null,
            }).ToListAsync();
            return Companies;
        }
        public async Task<int> Count(CompanyFilter filter)
        {
            IQueryable<CompanyDAO> Companys = DataContext.Company.AsNoTracking();
            Companys = DynamicFilter(Companys, filter);
            return await Companys.CountAsync();
        }

        public async Task<List<Company>> List(CompanyFilter filter)
        {
            if (filter == null) return new List<Company>();
            IQueryable<CompanyDAO> CompanyDAOs = DataContext.Company.AsNoTracking();
            CompanyDAOs = DynamicFilter(CompanyDAOs, filter);
            CompanyDAOs = DynamicOrder(CompanyDAOs, filter);
            List<Company> Companys = await DynamicSelect(CompanyDAOs, filter);
            return Companys;
        }

        public async Task<Company> Get(long Id)
        {
            Company Company = await DataContext.Company.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new Company()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                FAX = x.FAX,
                PhoneOther = x.PhoneOther,
                Email = x.Email,
                EmailOther = x.EmailOther,
                ZIPCode = x.ZIPCode,
                Revenue = x.Revenue,
                Website = x.Website,
                Address = x.Address,
                NationId = x.NationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                NumberOfEmployee = x.NumberOfEmployee,
                RefuseReciveEmail = x.RefuseReciveEmail,
                RefuseReciveSMS = x.RefuseReciveSMS,
                CustomerLeadId = x.CustomerLeadId,
                ParentId = x.ParentId,
                Path = x.Path,
                Level = x.Level,
                ProfessionId = x.ProfessionId,
                AppUserId = x.AppUserId,
                CreatorId = x.CreatorId,
                //OrganizationId = x.OrganizationId,
                CurrencyId = x.CurrencyId,
                CompanyStatusId = x.CompanyStatusId,
                Description = x.Description,
                RowId = x.RowId,
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
                CompanyStatus = x.CompanyStatus == null ? null : new CompanyStatus
                {
                    Id = x.CompanyStatus.Id,
                    Code = x.CompanyStatus.Code,
                    Name = x.CompanyStatus.Name,
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
                CustomerLead = x.CustomerLead == null ? null : new CustomerLead
                {
                    Id = x.CustomerLead.Id,
                    Name = x.CustomerLead.Name,
                    CompanyName = x.CustomerLead.CompanyName,
                    TelePhone = x.CustomerLead.TelePhone,
                    Phone = x.CustomerLead.Phone,
                    Fax = x.CustomerLead.Fax,
                    Email = x.CustomerLead.Email,
                    SecondEmail = x.CustomerLead.SecondEmail,
                    Website = x.CustomerLead.Website,
                    CustomerLeadSourceId = x.CustomerLead.CustomerLeadSourceId,
                    CustomerLeadLevelId = x.CustomerLead.CustomerLeadLevelId,
                    CompanyId = x.CustomerLead.CompanyId,
                    CampaignId = x.CustomerLead.CampaignId,
                    ProfessionId = x.CustomerLead.ProfessionId,
                    Revenue = x.CustomerLead.Revenue,
                    EmployeeQuantity = x.CustomerLead.EmployeeQuantity,
                    Address = x.CustomerLead.Address,
                    NationId = x.CustomerLead.NationId,
                    ProvinceId = x.CustomerLead.ProvinceId,
                    DistrictId = x.CustomerLead.DistrictId,
                    CustomerLeadStatusId = x.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = x.CustomerLead.BusinessRegistrationCode,
                    SexId = x.CustomerLead.SexId,
                    RefuseReciveSMS = x.CustomerLead.RefuseReciveSMS,
                    RefuseReciveEmail = x.CustomerLead.RefuseReciveEmail,
                    Description = x.CustomerLead.Description,
                    AppUserId = x.CustomerLead.AppUserId,
                    CreatorId = x.CustomerLead.CreatorId,
                    ZipCode = x.CustomerLead.ZipCode,
                    CurrencyId = x.CustomerLead.CurrencyId,
                    RowId = x.CustomerLead.RowId,
                },
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
                },
                Nation = x.Nation == null ? null : new Nation
                {
                    Id = x.Nation.Id,
                    Code = x.Nation.Code,
                    Name = x.Nation.Name,
                    Priority = x.Nation.Priority,
                    StatusId = x.Nation.StatusId,
                    Used = x.Nation.Used,
                    RowId = x.Nation.RowId,
                },
                //Organization = x.Organization == null ? null : new Organization
                //{
                //    Id = x.Organization.Id,
                //    Code = x.Organization.Code,
                //    Name = x.Organization.Name,
                //    Address = x.Organization.Address,
                //    Phone = x.Organization.Phone,
                //    Path = x.Organization.Path,
                //    ParentId = x.Organization.ParentId,
                //    Email = x.Organization.Email,
                //    StatusId = x.Organization.StatusId,
                //    Level = x.Organization.Level
                //},
                Parent = x.Parent == null ? null : new Company
                {
                    Id = x.Parent.Id,
                    Name = x.Parent.Name,
                    Phone = x.Parent.Phone,
                    FAX = x.Parent.FAX,
                    PhoneOther = x.Parent.PhoneOther,
                    Email = x.Parent.Email,
                    EmailOther = x.Parent.EmailOther,
                    ZIPCode = x.Parent.ZIPCode,
                    Revenue = x.Parent.Revenue,
                    Website = x.Parent.Website,
                    Address = x.Parent.Address,
                    NationId = x.Parent.NationId,
                    ProvinceId = x.Parent.ProvinceId,
                    DistrictId = x.Parent.DistrictId,
                    NumberOfEmployee = x.Parent.NumberOfEmployee,
                    RefuseReciveEmail = x.Parent.RefuseReciveEmail,
                    RefuseReciveSMS = x.Parent.RefuseReciveSMS,
                    CustomerLeadId = x.Parent.CustomerLeadId,
                    ParentId = x.Parent.ParentId,
                    Path = x.Parent.Path,
                    Level = x.Parent.Level,
                    ProfessionId = x.Parent.ProfessionId,
                    AppUserId = x.Parent.AppUserId,
                    CreatorId = x.Parent.CreatorId,
                    CurrencyId = x.Parent.CurrencyId,
                    CompanyStatusId = x.Parent.CompanyStatusId,
                    Description = x.Parent.Description,
                    RowId = x.Parent.RowId,
                },
                Profession = x.Profession == null ? null : new Profession
                {
                    Id = x.Profession.Id,
                    Code = x.Profession.Code,
                    Name = x.Profession.Name,
                    StatusId = x.Profession.StatusId,
                    RowId = x.Profession.RowId,
                    Used = x.Profession.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
            }).FirstOrDefaultAsync();

            if (Company == null)
                return null;

            Company.CompanyFileGroupings = await DataContext.CompanyFileGrouping
                .Where(x => x.CompanyId == Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new CompanyFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    CompanyId = x.CompanyId,
                    RowId = x.RowId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Creator = x.Creator == null ? null : new AppUser
                    {
                        Id = x.Creator.Id,
                        Username = x.Creator.Username,
                        DisplayName = x.Creator.DisplayName,
                    },
                    FileType = x.FileType == null ? null : new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    }
                }).ToListAsync();
            var CompanyFileGroupingIds = Company.CompanyFileGroupings.Select(x => x.Id).ToList();

            var CompanyFileMappings = await DataContext.CompanyFileMapping.Where(x => CompanyFileGroupingIds.Contains(x.CompanyFileGroupingId))
                .Select(x => new CompanyFileMapping
                {
                    CompanyFileGroupingId = x.CompanyFileGroupingId,
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

            foreach (CompanyFileGrouping CompanyFileGrouping in Company.CompanyFileGroupings)
            {
                CompanyFileGrouping.CompanyFileMappings = CompanyFileMappings.Where(x => x.CompanyFileGroupingId == CompanyFileGrouping.Id).ToList();
            }
            return Company;
        }
        public async Task<bool> Create(Company Company)
        {
            CompanyDAO CompanyDAO = new CompanyDAO();
            CompanyDAO.Id = Company.Id;
            CompanyDAO.Name = Company.Name;
            CompanyDAO.Phone = Company.Phone;
            CompanyDAO.FAX = Company.FAX;
            CompanyDAO.PhoneOther = Company.PhoneOther;
            CompanyDAO.Email = Company.Email;
            CompanyDAO.EmailOther = Company.EmailOther;
            CompanyDAO.ZIPCode = Company.ZIPCode;
            CompanyDAO.Revenue = Company.Revenue;
            CompanyDAO.Website = Company.Website;
            CompanyDAO.Address = Company.Address;
            CompanyDAO.NationId = Company.NationId;
            CompanyDAO.ProvinceId = Company.ProvinceId;
            CompanyDAO.DistrictId = Company.DistrictId;
            CompanyDAO.NumberOfEmployee = Company.NumberOfEmployee;
            CompanyDAO.RefuseReciveEmail = Company.RefuseReciveEmail;
            CompanyDAO.RefuseReciveSMS = Company.RefuseReciveSMS;
            CompanyDAO.CustomerLeadId = Company.CustomerLeadId;
            CompanyDAO.ParentId = Company.ParentId;
            CompanyDAO.Path = Company.Path;
            CompanyDAO.Level = Company.Level;
            CompanyDAO.ProfessionId = Company.ProfessionId;
            CompanyDAO.AppUserId = Company.AppUserId;
            CompanyDAO.CreatorId = Company.CreatorId;
            //CompanyDAO.OrganizationId = Company.OrganizationId;
            CompanyDAO.CurrencyId = Company.CurrencyId;
            CompanyDAO.CompanyStatusId = Company.CompanyStatusId;
            CompanyDAO.Description = Company.Description;
            CompanyDAO.RowId = Company.RowId;
            CompanyDAO.Path = "";
            CompanyDAO.Level = 1;
            CompanyDAO.CreatedAt = StaticParams.DateTimeNow;
            CompanyDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Company.Add(CompanyDAO);
            await DataContext.SaveChangesAsync();
            Company.Id = CompanyDAO.Id;
            await SaveReference(Company);
            await BuildPath();
            return true;
        }

        public async Task<bool> Update(Company Company)
        {
            CompanyDAO CompanyDAO = DataContext.Company.Where(x => x.Id == Company.Id).FirstOrDefault();
            if (CompanyDAO == null)
                return false;
            CompanyDAO.Id = Company.Id;
            CompanyDAO.Name = Company.Name;
            CompanyDAO.Phone = Company.Phone;
            CompanyDAO.FAX = Company.FAX;
            CompanyDAO.PhoneOther = Company.PhoneOther;
            CompanyDAO.Email = Company.Email;
            CompanyDAO.EmailOther = Company.EmailOther;
            CompanyDAO.ZIPCode = Company.ZIPCode;
            CompanyDAO.Revenue = Company.Revenue;
            CompanyDAO.Website = Company.Website;
            CompanyDAO.Address = Company.Address;
            CompanyDAO.NationId = Company.NationId;
            CompanyDAO.ProvinceId = Company.ProvinceId;
            CompanyDAO.DistrictId = Company.DistrictId;
            CompanyDAO.NumberOfEmployee = Company.NumberOfEmployee;
            CompanyDAO.RefuseReciveEmail = Company.RefuseReciveEmail;
            CompanyDAO.RefuseReciveSMS = Company.RefuseReciveSMS;
            CompanyDAO.CustomerLeadId = Company.CustomerLeadId;
            CompanyDAO.ParentId = Company.ParentId;
            CompanyDAO.Path = Company.Path;
            CompanyDAO.Level = Company.Level;
            CompanyDAO.ProfessionId = Company.ProfessionId;
            CompanyDAO.AppUserId = Company.AppUserId;
            CompanyDAO.CreatorId = Company.CreatorId;
            CompanyDAO.CurrencyId = Company.CurrencyId;
            CompanyDAO.CompanyStatusId = Company.CompanyStatusId;
            CompanyDAO.Description = Company.Description;
            CompanyDAO.RowId = Company.RowId;
            CompanyDAO.Path = "";
            CompanyDAO.Level = 1;
            CompanyDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Company);
            await BuildPath();
            return true;
        }

        public async Task<bool> Delete(Company Company)
        {
            CompanyDAO CompanyDAO = await DataContext.Company.Where(x => x.Id == Company.Id).FirstOrDefaultAsync();
            await DataContext.Company.Where(x => x.Path.StartsWith(CompanyDAO.Id + ".")).UpdateFromQueryAsync(x => new CompanyDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            await DataContext.Company.Where(x => x.Id == Company.Id).UpdateFromQueryAsync(x => new CompanyDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            await BuildPath();
            return true;
        }

        public async Task<bool> BulkMerge(List<Company> Companies)
        {
            List<CompanyDAO> CompanyDAOs = new List<CompanyDAO>();
            foreach (Company Company in Companies)
            {
                CompanyDAO CompanyDAO = new CompanyDAO();
                CompanyDAO.Id = Company.Id;
                CompanyDAO.Name = Company.Name;
                CompanyDAO.Phone = Company.Phone;
                CompanyDAO.FAX = Company.FAX;
                CompanyDAO.PhoneOther = Company.PhoneOther;
                CompanyDAO.Email = Company.Email;
                CompanyDAO.EmailOther = Company.EmailOther;
                CompanyDAO.ZIPCode = Company.ZIPCode;
                CompanyDAO.Revenue = Company.Revenue;
                CompanyDAO.Website = Company.Website;
                CompanyDAO.Address = Company.Address;
                CompanyDAO.NationId = Company.NationId;
                CompanyDAO.ProvinceId = Company.ProvinceId;
                CompanyDAO.DistrictId = Company.DistrictId;
                CompanyDAO.NumberOfEmployee = Company.NumberOfEmployee;
                CompanyDAO.RefuseReciveEmail = Company.RefuseReciveEmail;
                CompanyDAO.RefuseReciveSMS = Company.RefuseReciveSMS;
                CompanyDAO.CustomerLeadId = Company.CustomerLeadId;
                CompanyDAO.ParentId = Company.ParentId;
                CompanyDAO.Path = Company.Path;
                CompanyDAO.Level = Company.Level;
                CompanyDAO.ProfessionId = Company.ProfessionId;
                CompanyDAO.AppUserId = Company.AppUserId;
                CompanyDAO.CreatorId = Company.CreatorId;
                //CompanyDAO.OrganizationId = Company.OrganizationId;
                CompanyDAO.CurrencyId = Company.CurrencyId;
                CompanyDAO.CompanyStatusId = Company.CompanyStatusId;
                CompanyDAO.Description = Company.Description;
                CompanyDAO.RowId = Company.RowId;
                CompanyDAO.CreatedAt = StaticParams.DateTimeNow;
                CompanyDAO.UpdatedAt = StaticParams.DateTimeNow;
                CompanyDAOs.Add(CompanyDAO);
            }
            await DataContext.BulkMergeAsync(CompanyDAOs);
            await BuildPath();
            return true;
        }

        public async Task<bool> BulkDelete(List<Company> Companies)
        {
            List<long> Ids = Companies.Select(x => x.Id).ToList();
            await DataContext.Company
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CompanyDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            await BuildPath();
            return true;
        }

        private async Task SaveReference(Company Company)
        {
            List<CompanyFileGroupingDAO> CompanyFileGroupingDAOs = await DataContext.CompanyFileGrouping.Where(x => x.CompanyId == Company.Id).ToListAsync();
            CompanyFileGroupingDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            await DataContext.CompanyFileMapping.Where(x => x.CompanyFileGrouping.CompanyId == Company.Id).DeleteFromQueryAsync();
            List<CompanyFileMappingDAO> CompanyFileMappingDAOs = new List<CompanyFileMappingDAO>();
            if (Company.CompanyFileGroupings != null)
            {
                foreach (var CompanyFileGrouping in Company.CompanyFileGroupings)
                {
                    CompanyFileGroupingDAO CompanyFileGroupingDAO = CompanyFileGroupingDAOs.Where(x => x.Id == CompanyFileGrouping.Id && x.Id != 0).FirstOrDefault();
                    if (CompanyFileGroupingDAO == null)
                    {
                        CompanyFileGroupingDAO = new CompanyFileGroupingDAO
                        {
                            CreatorId = CompanyFileGrouping.CreatorId,
                            CompanyId = Company.Id,
                            Description = CompanyFileGrouping.Description,
                            Title = CompanyFileGrouping.Title,
                            FileTypeId = CompanyFileGrouping.FileTypeId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow,
                            RowId = Guid.NewGuid()
                        };
                        CompanyFileGroupingDAOs.Add(CompanyFileGroupingDAO);
                        CompanyFileGrouping.RowId = CompanyFileGroupingDAO.RowId;
                    }
                    else
                    {
                        CompanyFileGroupingDAO.Description = CompanyFileGrouping.Description;
                        CompanyFileGroupingDAO.Title = CompanyFileGrouping.Title;
                        CompanyFileGroupingDAO.FileTypeId = CompanyFileGrouping.FileTypeId;
                        CompanyFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                        CompanyFileGroupingDAO.DeletedAt = null;
                        CompanyFileGrouping.RowId = CompanyFileGroupingDAO.RowId;
                    }
                }
            }
            await DataContext.BulkMergeAsync(CompanyFileGroupingDAOs);

            if (Company.CompanyFileGroupings != null)
            {
                foreach (var CompanyFileGrouping in Company.CompanyFileGroupings)
                {
                    CompanyFileGrouping.Id = CompanyFileGroupingDAOs.Where(x => x.RowId == CompanyFileGrouping.RowId).Select(x => x.Id).FirstOrDefault();
                    if (CompanyFileGrouping.CompanyFileMappings != null)
                    {
                        foreach (var CompanyFileMapping in CompanyFileGrouping.CompanyFileMappings)
                        {
                            CompanyFileMappingDAO CompanyFileMappingDAO = new CompanyFileMappingDAO
                            {
                                FileId = CompanyFileMapping.FileId,
                                CompanyFileGroupingId = CompanyFileGrouping.Id,
                            };
                            CompanyFileMappingDAOs.Add(CompanyFileMappingDAO);
                        }
                    }
                }
            }
            await DataContext.BulkMergeAsync(CompanyFileMappingDAOs);
        }

        private async Task BuildPath()
        {
            List<CompanyDAO> CompanyDAOs = await DataContext.Company
                .Where(x => x.DeletedAt == null)
                .AsNoTracking().ToListAsync();
            Queue<CompanyDAO> queue = new Queue<CompanyDAO>();
            CompanyDAOs.ForEach(x =>
            {
                if (!x.ParentId.HasValue)
                {
                    x.Path = x.Id + ".";
                    x.Level = 1;
                    queue.Enqueue(x);
                }
            });
            while (queue.Count > 0)
            {
                CompanyDAO Parent = queue.Dequeue();
                foreach (CompanyDAO CompanyDAO in CompanyDAOs)
                {
                    if (CompanyDAO.ParentId == Parent.Id)
                    {
                        CompanyDAO.Path = Parent.Path + CompanyDAO.Id + ".";
                        CompanyDAO.Level = Parent.Level + 1;
                        queue.Enqueue(CompanyDAO);
                    }
                }
            }
            await DataContext.BulkMergeAsync(CompanyDAOs);
        }

        private async Task AuditLogProperty(Company Company)
        {
            var modifiedEntities = DataContext.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified
                || p.State == EntityState.Added
                || p.State == EntityState.Deleted
                || p.State == EntityState.Modified
                || p.State == EntityState.Detached)
                .ToList();


            foreach (var change in modifiedEntities)
            {
                var type = change.Entity.GetType();

                var EntityDisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Select(x => ((DisplayNameAttribute)x).DisplayName)
                .DefaultIfEmpty(type.Name)
                .First();

                if (change.State == EntityState.Modified)
                {
                    foreach (var prop in change.Entity.GetType().GetTypeInfo().DeclaredProperties)
                    {
                        if (!prop.GetGetMethod().IsVirtual)
                        {
                            var currentValue = change.Property(prop.Name).CurrentValue;
                            var originalValue = change.Property(prop.Name).OriginalValue;
                            if (!Equals(originalValue, currentValue))
                            {
                                var attributes = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                                if (attributes != null && attributes.Length > 0)
                                {
                                    AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                                    var displayName = (DisplayNameAttribute)attributes[0];
                                    AuditLogPropertyDAO.OldValue = originalValue == null ? "" : originalValue.ToString();
                                    AuditLogPropertyDAO.NewValue = currentValue.ToString();
                                    if (prop.Name.Equals("ProvinceId"))
                                    {
                                        Province ProvinceNew = await DataContext.Province.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Province()
                                        {
                                            Id = x.Id,
                                            Code = x.Code,
                                            Name = x.Name,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            Province ProvinceOld = await DataContext.Province.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Province()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = ProvinceOld.Name;
                                        }

                                        AuditLogPropertyDAO.NewValue = ProvinceNew.Name;
                                    }
                                    else if (prop.Name.Equals("DistrictId"))
                                    {
                                        District DistrictNew = await DataContext.District.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new District()
                                        {
                                            Id = x.Id,
                                            Code = x.Code,
                                            Name = x.Name,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            District DistrictOld = await DataContext.District.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new District()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = DistrictOld.Name;
                                        }

                                        AuditLogPropertyDAO.NewValue = DistrictNew.Name;
                                    }
                                    else if (prop.Name.Equals("AppUserAssignedId"))
                                    {
                                        AppUser AppUserNew = await DataContext.AppUser.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new AppUser()
                                        {
                                            Id = x.Id,
                                            DisplayName = x.DisplayName,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            AppUser AppUserOld = await DataContext.AppUser.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new AppUser()
                                            {
                                                Id = x.Id,
                                                DisplayName = x.DisplayName,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = AppUserOld.DisplayName;
                                        }

                                        AuditLogPropertyDAO.NewValue = AppUserNew.DisplayName;
                                    }
                                    else if (prop.Name.Equals("ProfessionId"))
                                    {
                                        Profession ProfessionNew = await DataContext.Profession.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Profession()
                                        {
                                            Id = x.Id,
                                            Code = x.Code,
                                            Name = x.Name,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            Profession ProfessionOld = await DataContext.Profession.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Profession()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.OldValue = ProfessionOld.Name;
                                        }

                                        AuditLogPropertyDAO.NewValue = ProfessionNew.Name;

                                    }
                                    else if (prop.Name.Equals("LeadSourceId"))
                                    {
                                        CustomerLeadSource CustomerLeadSourceNew = await DataContext.CustomerLeadSource.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new CustomerLeadSource()
                                        {
                                            Id = x.Id,
                                            Code = x.Code,
                                            Name = x.Name,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            CustomerLeadSource CustomerLeadSourceOld = await DataContext.CustomerLeadSource.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new CustomerLeadSource()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = CustomerLeadSourceOld.Name;
                                        }

                                        AuditLogPropertyDAO.NewValue = CustomerLeadSourceNew.Name;
                                    }
                                    else if (prop.Name.Equals("NationId"))
                                    {
                                        Nation NationNew = await DataContext.Nation.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Nation()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            Nation NationOld = await DataContext.Nation.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Nation()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = NationOld.Name;
                                        }

                                        AuditLogPropertyDAO.NewValue = NationNew.Name;
                                    }
                                    else if (prop.Name.Equals("CurrencyId"))
                                    {
                                        Currency New = await DataContext.Currency.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Currency()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                        }).FirstOrDefaultAsync();
                                        if (originalValue != null)
                                        {
                                            Currency Old = await DataContext.Currency.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Currency()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = Old.Name;
                                        }

                                        AuditLogPropertyDAO.NewValue = New.Name;
                                    }
                                    AuditLogPropertyDAO.Property = displayName.DisplayName;
                                    AuditLogPropertyDAO.ActionName = AuditLogPropertyActionEnum.EDIT.Name;
                                    AuditLogPropertyDAO.Time = StaticParams.DateTimeNow;
                                    AuditLogPropertyDAO.ClassName = EntityDisplayName;
                                    AuditLogPropertyDAO.AppUserId = 2;
                                    DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
                                    await DataContext.SaveChangesAsync();
                                    //CompanyAuditLogPropertyMappingDAO CompanyAuditLogPropertyMappingDAO = new CompanyAuditLogPropertyMappingDAO();
                                    //CompanyAuditLogPropertyMappingDAO.CompanyId = Company.Id;
                                    //CompanyAuditLogPropertyMappingDAO.AuditLogPropertyId = AuditLogPropertyDAO.Id;
                                    //DataContext.CompanyAuditLogPropertyMapping.Add(CompanyAuditLogPropertyMappingDAO);
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}
