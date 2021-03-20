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
using System.Reflection;
using CRM.Enums;

namespace CRM.Repositories
{
    public interface IAuditLogPropertyRepository
    {
        Task<int> Count(AuditLogPropertyFilter AuditLogPropertyFilter);
        Task<List<AuditLogProperty>> List(AuditLogPropertyFilter AuditLogPropertyFilter);
        Task<bool> ChangeTracker();
        Task<AuditLogProperty> Get(long Id);
        Task<bool> Create(AuditLogProperty AuditLogProperty);
        Task<bool> Update(AuditLogProperty AuditLogProperty);
        Task<bool> Delete(AuditLogProperty AuditLogProperty);
        Task<bool> BulkMerge(List<AuditLogProperty> AuditLogProperties);
        Task<bool> BulkDelete(List<AuditLogProperty> AuditLogProperties);
    }
    public class AuditLogPropertyRepository : IAuditLogPropertyRepository
    {
        private DataContext DataContext;
        public AuditLogPropertyRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<AuditLogPropertyDAO> DynamicFilter(IQueryable<AuditLogPropertyDAO> query, AuditLogPropertyFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            if (filter.Property != null)
                query = query.Where(q => q.Property, filter.Property);
            if (filter.OldValue != null)
                query = query.Where(q => q.OldValue, filter.OldValue);
            if (filter.NewValue != null)
                query = query.Where(q => q.NewValue, filter.NewValue);
            if (filter.ClassName != null)
                query = query.Where(q => q.ClassName, filter.ClassName);
            if (filter.ActionName != null)
                query = query.Where(q => q.ActionName, filter.ActionName);
            if (filter.Time != null)
                query = query.Where(q => q.Time == null).Union(query.Where(q => q.Time.HasValue).Where(q => q.Time, filter.Time));
            ////Tìm kiếm theo CustomerLeadId
            //if (filter.CustomerLeadId.Equal.HasValue)
            //{
            //    query = from q in query
            //            join ar in DataContext.CustomerLeadAuditLogPropertyMapping on q.Id equals ar.AuditLogPropertyId
            //            where ar.CustomerLeadId == filter.CustomerLeadId.Equal
            //            select q;
            //}
            ////Tìm kiếm theo CompanyId

            //if (filter.CompanyId.Equal.HasValue)
            //{
            //    query = from q in query
            //            join ar in DataContext.CompanyAuditLogPropertyMapping on q.Id equals ar.AuditLogPropertyId
            //            where ar.CompanyId == filter.CompanyId.Equal
            //            select q;
            //}
            //Tìm kiếm theo ContactId
            //if (filter.ContactId.Equal.HasValue)
            //{
            //    query = from q in query
            //            join ar in DataContext.ContactAuditLogPropertyMapping on q.Id equals ar.AuditLogPropertyId
            //            where ar.ContactId == filter.ContactId.Equal
            //            select q;
            //}

            ////Tìm kiếm theo OpportunityId
            //if (filter.OpportunityId.Equal.HasValue)
            //{
            //    query = from q in query
            //            join ar in DataContext.OpportunityAuditLogPropertyMapping on q.Id equals ar.AuditLogPropertyId
            //            where ar.OpportunityId == filter.OpportunityId.Equal
            //            select q;
            //}
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<AuditLogPropertyDAO> OrFilter(IQueryable<AuditLogPropertyDAO> query, AuditLogPropertyFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<AuditLogPropertyDAO> initQuery = query.Where(q => false);
            foreach (AuditLogPropertyFilter AuditLogPropertyFilter in filter.OrFilter)
            {
                IQueryable<AuditLogPropertyDAO> queryable = query;
                if (AuditLogPropertyFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, AuditLogPropertyFilter.Id);
                if (AuditLogPropertyFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, AuditLogPropertyFilter.AppUserId);
                if (AuditLogPropertyFilter.Property != null)
                    queryable = queryable.Where(q => q.Property, AuditLogPropertyFilter.Property);
                if (AuditLogPropertyFilter.OldValue != null)
                    queryable = queryable.Where(q => q.OldValue, AuditLogPropertyFilter.OldValue);
                if (AuditLogPropertyFilter.NewValue != null)
                    queryable = queryable.Where(q => q.NewValue, AuditLogPropertyFilter.NewValue);
                if (AuditLogPropertyFilter.ClassName != null)
                    queryable = queryable.Where(q => q.ClassName, AuditLogPropertyFilter.ClassName);
                if (AuditLogPropertyFilter.ActionName != null)
                    queryable = queryable.Where(q => q.ActionName, AuditLogPropertyFilter.ActionName);
                if (AuditLogPropertyFilter.Time != null)
                    queryable = queryable.Where(q => q.Time.HasValue).Where(q => q.Time, AuditLogPropertyFilter.Time);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<AuditLogPropertyDAO> DynamicOrder(IQueryable<AuditLogPropertyDAO> query, AuditLogPropertyFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case AuditLogPropertyOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case AuditLogPropertyOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case AuditLogPropertyOrder.Property:
                            query = query.OrderBy(q => q.Property);
                            break;
                        case AuditLogPropertyOrder.OldValue:
                            query = query.OrderBy(q => q.OldValue);
                            break;
                        case AuditLogPropertyOrder.NewValue:
                            query = query.OrderBy(q => q.NewValue);
                            break;
                        case AuditLogPropertyOrder.ClassName:
                            query = query.OrderBy(q => q.ClassName);
                            break;
                        case AuditLogPropertyOrder.ActionName:
                            query = query.OrderBy(q => q.ActionName);
                            break;
                        case AuditLogPropertyOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case AuditLogPropertyOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case AuditLogPropertyOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case AuditLogPropertyOrder.Property:
                            query = query.OrderByDescending(q => q.Property);
                            break;
                        case AuditLogPropertyOrder.OldValue:
                            query = query.OrderByDescending(q => q.OldValue);
                            break;
                        case AuditLogPropertyOrder.NewValue:
                            query = query.OrderByDescending(q => q.NewValue);
                            break;
                        case AuditLogPropertyOrder.ClassName:
                            query = query.OrderByDescending(q => q.ClassName);
                            break;
                        case AuditLogPropertyOrder.ActionName:
                            query = query.OrderByDescending(q => q.ActionName);
                            break;
                        case AuditLogPropertyOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<AuditLogProperty>> DynamicSelect(IQueryable<AuditLogPropertyDAO> query, AuditLogPropertyFilter filter)
        {
            List<AuditLogProperty> AuditLogProperties = await query.Select(q => new AuditLogProperty()
            {
                Id = filter.Selects.Contains(AuditLogPropertySelect.Id) ? q.Id : default(long),
                AppUserId = filter.Selects.Contains(AuditLogPropertySelect.AppUser) ? q.AppUserId : default(long?),
                Property = filter.Selects.Contains(AuditLogPropertySelect.Property) ? q.Property : default(string),
                OldValue = filter.Selects.Contains(AuditLogPropertySelect.OldValue) ? q.OldValue : default(string),
                NewValue = filter.Selects.Contains(AuditLogPropertySelect.NewValue) ? q.NewValue : default(string),
                ClassName = filter.Selects.Contains(AuditLogPropertySelect.ClassName) ? q.ClassName : default(string),
                ActionName = filter.Selects.Contains(AuditLogPropertySelect.ActionName) ? q.ActionName : default(string),
                Time = filter.Selects.Contains(AuditLogPropertySelect.Time) ? q.Time : default(DateTime?),
                AppUser = filter.Selects.Contains(AuditLogPropertySelect.AppUser) && q.AppUser != null ? new AppUser
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
                } : null,
            }).ToListAsync();
            return AuditLogProperties;
        }

        public async Task<int> Count(AuditLogPropertyFilter filter)
        {
            IQueryable<AuditLogPropertyDAO> AuditLogProperties = DataContext.AuditLogProperty.AsNoTracking();
            AuditLogProperties = DynamicFilter(AuditLogProperties, filter);
            return await AuditLogProperties.CountAsync();
        }

        public async Task<List<AuditLogProperty>> List(AuditLogPropertyFilter filter)
        {
            if (filter == null) return new List<AuditLogProperty>();
            IQueryable<AuditLogPropertyDAO> AuditLogPropertyDAOs = DataContext.AuditLogProperty.AsNoTracking();
            AuditLogPropertyDAOs = DynamicFilter(AuditLogPropertyDAOs, filter);
            AuditLogPropertyDAOs = DynamicOrder(AuditLogPropertyDAOs, filter);
            List<AuditLogProperty> AuditLogProperties = await DynamicSelect(AuditLogPropertyDAOs, filter);
            return AuditLogProperties;
        }

        public async Task<bool> ChangeTracker()
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
                var type = change.GetType();
                var entityName = type.Name;

                var EntityDisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Select(x => ((DisplayNameAttribute)x).DisplayName)
                .DefaultIfEmpty(type.Name)
                .First();

                if (change.State == EntityState.Added)
                {
                    // Log Added
                }
                else if (change.State == EntityState.Modified)
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

                                    var displayName = (DisplayNameAttribute)attributes[0];
                                    AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                                    AuditLogPropertyDAO.Property = displayName.ToString();
                                    AuditLogPropertyDAO.ActionName = AuditLogPropertyActionEnum.EDIT.Name;
                                    AuditLogPropertyDAO.OldValue = originalValue.ToString();
                                    AuditLogPropertyDAO.NewValue = currentValue.ToString();
                                    AuditLogPropertyDAO.Time = StaticParams.DateTimeNow;
                                    AuditLogPropertyDAO.ClassName = EntityDisplayName;
                                    AuditLogPropertyDAO.AppUserId = 2;
                                    DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
                                    await DataContext.SaveChangesAsync();
                                    AuditLogPropertyDAO.Id = AuditLogPropertyDAO.Id;
                                    await SaveReference(AuditLogPropertyDAO);
                                }
                            }
                        }

                    }
                }
                else if (change.State == EntityState.Deleted)
                {
                    AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                    AuditLogPropertyDAO.ActionName = AuditLogPropertyActionEnum.CREATE.Name;
                    AuditLogPropertyDAO.Time = StaticParams.DateTimeNow;
                    AuditLogPropertyDAO.ClassName = EntityDisplayName;
                    AuditLogPropertyDAO.AppUserId = 2;
                    DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
                    await DataContext.SaveChangesAsync();
                    AuditLogPropertyDAO.Id = AuditLogPropertyDAO.Id;
                    await SaveReference(AuditLogPropertyDAO);
                }
            }
            return true;
        }

        public async Task<AuditLogProperty> Get(long Id)
        {
            AuditLogProperty AuditLogProperty = await DataContext.AuditLogProperty.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new AuditLogProperty()
            {
                Id = x.Id,
                AppUserId = x.AppUserId,
                Property = x.Property,
                OldValue = x.OldValue,
                NewValue = x.NewValue,
                ClassName = x.ClassName,
                ActionName = x.ActionName,
                Time = x.Time,
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
                },
            }).FirstOrDefaultAsync();

            if (AuditLogProperty == null)
                return null;

            return AuditLogProperty;
        }
        public async Task<bool> Create(AuditLogProperty AuditLogProperty)
        {

            AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
            AuditLogPropertyDAO.Id = AuditLogProperty.Id;
            AuditLogPropertyDAO.AppUserId = AuditLogProperty.AppUserId;
            AuditLogPropertyDAO.Property = AuditLogProperty.Property;
            AuditLogPropertyDAO.OldValue = AuditLogProperty.OldValue;
            AuditLogPropertyDAO.NewValue = AuditLogProperty.NewValue;
            AuditLogPropertyDAO.ClassName = AuditLogProperty.ClassName;
            AuditLogPropertyDAO.ActionName = AuditLogProperty.ActionName;
            AuditLogPropertyDAO.Time = AuditLogProperty.Time;
            DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
            await DataContext.SaveChangesAsync();
            AuditLogPropertyDAO.Id = AuditLogPropertyDAO.Id;
            await SaveReference(AuditLogPropertyDAO);
            return true;
        }

        public async Task<bool> Update(AuditLogProperty AuditLogProperty)
        {
            AuditLogPropertyDAO AuditLogPropertyDAO = DataContext.AuditLogProperty.Where(x => x.Id == AuditLogProperty.Id).FirstOrDefault();
            if (AuditLogPropertyDAO == null)
                return false;
            AuditLogPropertyDAO.Id = AuditLogProperty.Id;
            AuditLogPropertyDAO.AppUserId = AuditLogProperty.AppUserId;
            AuditLogPropertyDAO.Property = AuditLogProperty.Property;
            AuditLogPropertyDAO.OldValue = AuditLogProperty.OldValue;
            AuditLogPropertyDAO.NewValue = AuditLogProperty.NewValue;
            AuditLogPropertyDAO.ClassName = AuditLogProperty.ClassName;
            AuditLogPropertyDAO.ActionName = AuditLogProperty.ActionName;
            AuditLogPropertyDAO.Time = AuditLogProperty.Time;
            await DataContext.SaveChangesAsync();
            await SaveReference(AuditLogPropertyDAO);
            return true;
        }

        public async Task<bool> Delete(AuditLogProperty AuditLogProperty)
        {
            await DataContext.AuditLogProperty.Where(x => x.Id == AuditLogProperty.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<AuditLogProperty> AuditLogProperties)
        {
            List<AuditLogPropertyDAO> AuditLogPropertyDAOs = new List<AuditLogPropertyDAO>();
            foreach (AuditLogProperty AuditLogProperty in AuditLogProperties)
            {
                AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                AuditLogPropertyDAO.Id = AuditLogProperty.Id;
                AuditLogPropertyDAO.AppUserId = AuditLogProperty.AppUserId;
                AuditLogPropertyDAO.Property = AuditLogProperty.Property;
                AuditLogPropertyDAO.OldValue = AuditLogProperty.OldValue;
                AuditLogPropertyDAO.NewValue = AuditLogProperty.NewValue;
                AuditLogPropertyDAO.ClassName = AuditLogProperty.ClassName;
                AuditLogPropertyDAO.ActionName = AuditLogProperty.ActionName;
                AuditLogPropertyDAO.Time = AuditLogProperty.Time;
                AuditLogPropertyDAOs.Add(AuditLogPropertyDAO);
            }
            await DataContext.BulkMergeAsync(AuditLogPropertyDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<AuditLogProperty> AuditLogProperties)
        {
            List<long> Ids = AuditLogProperties.Select(x => x.Id).ToList();
            await DataContext.AuditLogProperty
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(AuditLogPropertyDAO AuditLogPropertyDAO)
        {
        }
    }
}
