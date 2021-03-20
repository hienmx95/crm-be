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
    public interface IProductGroupingRepository
    {
        Task<int> Count(ProductGroupingFilter ProductGroupingFilter);
        Task<List<ProductGrouping>> List(ProductGroupingFilter ProductGroupingFilter);
        Task<ProductGrouping> Get(long Id);
    }
    public class ProductGroupingRepository : IProductGroupingRepository
    {
        private DataContext DataContext;
        public ProductGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ProductGroupingDAO> DynamicFilter(IQueryable<ProductGroupingDAO> query, ProductGroupingFilter filter)
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
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.ParentId != null)
                query = query.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, filter.ParentId);
            if (filter.Path != null)
                query = query.Where(q => q.Path, filter.Path);
            if (filter.Level != null)
                query = query.Where(q => q.Level, filter.Level);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<ProductGroupingDAO> OrFilter(IQueryable<ProductGroupingDAO> query, ProductGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ProductGroupingDAO> initQuery = query.Where(q => false);
            foreach (ProductGroupingFilter ProductGroupingFilter in filter.OrFilter)
            {
                IQueryable<ProductGroupingDAO> queryable = query;
                if (ProductGroupingFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, ProductGroupingFilter.Id);
                if (ProductGroupingFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, ProductGroupingFilter.Code);
                if (ProductGroupingFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, ProductGroupingFilter.Name);
                if (ProductGroupingFilter.Description != null)
                    queryable = queryable.Where(q => q.Description, ProductGroupingFilter.Description);
                if (ProductGroupingFilter.ParentId != null)
                    queryable = queryable.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, ProductGroupingFilter.ParentId);
                if (ProductGroupingFilter.Path != null)
                    queryable = queryable.Where(q => q.Path, ProductGroupingFilter.Path);
                if (ProductGroupingFilter.Level != null)
                    queryable = queryable.Where(q => q.Level, ProductGroupingFilter.Level);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ProductGroupingDAO> DynamicOrder(IQueryable<ProductGroupingDAO> query, ProductGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ProductGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProductGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProductGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProductGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ProductGroupingOrder.Parent:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        case ProductGroupingOrder.Path:
                            query = query.OrderBy(q => q.Path);
                            break;
                        case ProductGroupingOrder.Level:
                            query = query.OrderBy(q => q.Level);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ProductGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProductGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProductGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProductGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ProductGroupingOrder.Parent:
                            query = query.OrderByDescending(q => q.ParentId);
                            break;
                        case ProductGroupingOrder.Path:
                            query = query.OrderByDescending(q => q.Path);
                            break;
                        case ProductGroupingOrder.Level:
                            query = query.OrderByDescending(q => q.Level);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ProductGrouping>> DynamicSelect(IQueryable<ProductGroupingDAO> query, ProductGroupingFilter filter)
        {
            List<ProductGrouping> ProductGroupings = await query.Select(q => new ProductGrouping()
            {
                Id = filter.Selects.Contains(ProductGroupingSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProductGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProductGroupingSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(ProductGroupingSelect.Description) ? q.Description : default(string),
                ParentId = filter.Selects.Contains(ProductGroupingSelect.Parent) ? q.ParentId : default(long?),
                Path = filter.Selects.Contains(ProductGroupingSelect.Path) ? q.Path : default(string),
                Level = filter.Selects.Contains(ProductGroupingSelect.Level) ? q.Level : default(long),
                Parent = filter.Selects.Contains(ProductGroupingSelect.Parent) && q.Parent != null ? new ProductGrouping
                {
                    Id = q.Parent.Id,
                    Code = q.Parent.Code,
                    Name = q.Parent.Name,
                    Description = q.Parent.Description,
                    ParentId = q.Parent.ParentId,
                    Path = q.Parent.Path,
                    Level = q.Parent.Level,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return ProductGroupings;
        }

        public async Task<int> Count(ProductGroupingFilter filter)
        {
            IQueryable<ProductGroupingDAO> ProductGroupings = DataContext.ProductGrouping.AsNoTracking();
            ProductGroupings = DynamicFilter(ProductGroupings, filter);
            return await ProductGroupings.CountAsync();
        }

        public async Task<List<ProductGrouping>> List(ProductGroupingFilter filter)
        {
            if (filter == null) return new List<ProductGrouping>();
            IQueryable<ProductGroupingDAO> ProductGroupingDAOs = DataContext.ProductGrouping.AsNoTracking();
            ProductGroupingDAOs = DynamicFilter(ProductGroupingDAOs, filter);
            ProductGroupingDAOs = DynamicOrder(ProductGroupingDAOs, filter);
            List<ProductGrouping> ProductGroupings = await DynamicSelect(ProductGroupingDAOs, filter);
            return ProductGroupings;
        }

        public async Task<ProductGrouping> Get(long Id)
        {
            ProductGrouping ProductGrouping = await DataContext.ProductGrouping.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new ProductGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                ParentId = x.ParentId,
                Path = x.Path,
                Level = x.Level,
                Parent = x.Parent == null ? null : new ProductGrouping
                {
                    Id = x.Parent.Id,
                    Code = x.Parent.Code,
                    Name = x.Parent.Name,
                    Description = x.Parent.Description,
                    ParentId = x.Parent.ParentId,
                    Path = x.Parent.Path,
                    Level = x.Parent.Level,
                },
            }).FirstOrDefaultAsync();

            if (ProductGrouping == null)
                return null;

            return ProductGrouping;
        }
    }
}
