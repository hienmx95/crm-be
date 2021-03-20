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
    public interface IProductRepository
    {
        Task<int> Count(ProductFilter ProductFilter);
        Task<List<Product>> List(ProductFilter ProductFilter);
        Task<Product> Get(long Id);
    }
    public class ProductRepository : IProductRepository
    {
        private DataContext DataContext;
        public ProductRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ProductDAO> DynamicFilter(IQueryable<ProductDAO> query, ProductFilter filter)
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
            if (filter.ScanCode != null)
                query = query.Where(q => q.ScanCode, filter.ScanCode);
            if (filter.ERPCode != null)
                query = query.Where(q => q.ERPCode, filter.ERPCode);
            if (filter.CategoryId != null)
                query = query.Where(q => q.CategoryId, filter.CategoryId);
            if (filter.ProductTypeId != null)
                query = query.Where(q => q.ProductTypeId, filter.ProductTypeId);
            if (filter.BrandId != null)
                query = query.Where(q => q.BrandId, filter.BrandId);
            if (filter.UnitOfMeasureId != null)
                query = query.Where(q => q.UnitOfMeasureId, filter.UnitOfMeasureId);
            if (filter.UnitOfMeasureGroupingId != null)
                query = query.Where(q => q.UnitOfMeasureGroupingId, filter.UnitOfMeasureGroupingId);
            if (filter.SalePrice != null)
                query = query.Where(q => q.SalePrice, filter.SalePrice);
            if (filter.RetailPrice != null)
                query = query.Where(q => q.RetailPrice, filter.RetailPrice);
            if (filter.TaxTypeId != null)
                query = query.Where(q => q.TaxTypeId, filter.TaxTypeId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.OtherName != null)
                query = query.Where(q => q.OtherName, filter.OtherName);
            if (filter.TechnicalName != null)
                query = query.Where(q => q.TechnicalName, filter.TechnicalName);
            if (filter.Note != null)
                query = query.Where(q => q.Note, filter.Note);
            if (filter.UsedVariationId != null)
                query = query.Where(q => q.UsedVariationId, filter.UsedVariationId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<ProductDAO> OrFilter(IQueryable<ProductDAO> query, ProductFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ProductDAO> initQuery = query.Where(q => false);
            foreach (ProductFilter ProductFilter in filter.OrFilter)
            {
                IQueryable<ProductDAO> queryable = query;
                if (ProductFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, ProductFilter.Id);
                if (ProductFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, ProductFilter.Code);
                if (ProductFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, ProductFilter.Name);
                if (ProductFilter.Description != null)
                    queryable = queryable.Where(q => q.Description, ProductFilter.Description);
                if (ProductFilter.ScanCode != null)
                    queryable = queryable.Where(q => q.ScanCode, ProductFilter.ScanCode);
                if (ProductFilter.ERPCode != null)
                    queryable = queryable.Where(q => q.ERPCode, ProductFilter.ERPCode);
                if (ProductFilter.ProductTypeId != null)
                    queryable = queryable.Where(q => q.ProductTypeId, ProductFilter.ProductTypeId);
                if (ProductFilter.BrandId != null)
                    queryable = queryable.Where(q => q.BrandId.HasValue).Where(q => q.BrandId, ProductFilter.BrandId);
                if (ProductFilter.UnitOfMeasureId != null)
                    queryable = queryable.Where(q => q.UnitOfMeasureId, ProductFilter.UnitOfMeasureId);
                if (ProductFilter.UnitOfMeasureGroupingId != null)
                    queryable = queryable.Where(q => q.UnitOfMeasureGroupingId.HasValue).Where(q => q.UnitOfMeasureGroupingId, ProductFilter.UnitOfMeasureGroupingId);
                if (ProductFilter.SalePrice != null)
                    queryable = queryable.Where(q => q.SalePrice, ProductFilter.SalePrice);
                if (ProductFilter.RetailPrice != null)
                    queryable = queryable.Where(q => q.RetailPrice.HasValue).Where(q => q.RetailPrice, ProductFilter.RetailPrice);
                if (ProductFilter.TaxTypeId != null)
                    queryable = queryable.Where(q => q.TaxTypeId, ProductFilter.TaxTypeId);
                if (ProductFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, ProductFilter.StatusId);
                if (ProductFilter.OtherName != null)
                    queryable = queryable.Where(q => q.OtherName, ProductFilter.OtherName);
                if (ProductFilter.TechnicalName != null)
                    queryable = queryable.Where(q => q.TechnicalName, ProductFilter.TechnicalName);
                if (ProductFilter.Note != null)
                    queryable = queryable.Where(q => q.Note, ProductFilter.Note);
                if (ProductFilter.UsedVariationId != null)
                    queryable = queryable.Where(q => q.UsedVariationId, ProductFilter.UsedVariationId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ProductDAO> DynamicOrder(IQueryable<ProductDAO> query, ProductFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ProductOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProductOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProductOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProductOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ProductOrder.ScanCode:
                            query = query.OrderBy(q => q.ScanCode);
                            break;
                        case ProductOrder.ERPCode:
                            query = query.OrderBy(q => q.ERPCode);
                            break;
                        case ProductOrder.ProductType:
                            query = query.OrderBy(q => q.ProductTypeId);
                            break;
                        case ProductOrder.Brand:
                            query = query.OrderBy(q => q.BrandId);
                            break;
                        case ProductOrder.UnitOfMeasure:
                            query = query.OrderBy(q => q.UnitOfMeasureId);
                            break;
                        case ProductOrder.UnitOfMeasureGrouping:
                            query = query.OrderBy(q => q.UnitOfMeasureGroupingId);
                            break;
                        case ProductOrder.SalePrice:
                            query = query.OrderBy(q => q.SalePrice);
                            break;
                        case ProductOrder.RetailPrice:
                            query = query.OrderBy(q => q.RetailPrice);
                            break;
                        case ProductOrder.TaxType:
                            query = query.OrderBy(q => q.TaxTypeId);
                            break;
                        case ProductOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case ProductOrder.OtherName:
                            query = query.OrderBy(q => q.OtherName);
                            break;
                        case ProductOrder.TechnicalName:
                            query = query.OrderBy(q => q.TechnicalName);
                            break;
                        case ProductOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        case ProductOrder.IsNew:
                            query = query.OrderBy(q => q.IsNew);
                            break;
                        case ProductOrder.UsedVariation:
                            query = query.OrderBy(q => q.UsedVariationId);
                            break;
                        case ProductOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ProductOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProductOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProductOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProductOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ProductOrder.ScanCode:
                            query = query.OrderByDescending(q => q.ScanCode);
                            break;
                        case ProductOrder.ERPCode:
                            query = query.OrderByDescending(q => q.ERPCode);
                            break;
                        case ProductOrder.ProductType:
                            query = query.OrderByDescending(q => q.ProductTypeId);
                            break;
                        case ProductOrder.Brand:
                            query = query.OrderByDescending(q => q.BrandId);
                            break;
                        case ProductOrder.UnitOfMeasure:
                            query = query.OrderByDescending(q => q.UnitOfMeasureId);
                            break;
                        case ProductOrder.UnitOfMeasureGrouping:
                            query = query.OrderByDescending(q => q.UnitOfMeasureGroupingId);
                            break;
                        case ProductOrder.SalePrice:
                            query = query.OrderByDescending(q => q.SalePrice);
                            break;
                        case ProductOrder.RetailPrice:
                            query = query.OrderByDescending(q => q.RetailPrice);
                            break;
                        case ProductOrder.TaxType:
                            query = query.OrderByDescending(q => q.TaxTypeId);
                            break;
                        case ProductOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case ProductOrder.OtherName:
                            query = query.OrderByDescending(q => q.OtherName);
                            break;
                        case ProductOrder.TechnicalName:
                            query = query.OrderByDescending(q => q.TechnicalName);
                            break;
                        case ProductOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
                            break;
                        case ProductOrder.IsNew:
                            query = query.OrderByDescending(q => q.IsNew);
                            break;
                        case ProductOrder.UsedVariation:
                            query = query.OrderByDescending(q => q.UsedVariationId);
                            break;
                        case ProductOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Product>> DynamicSelect(IQueryable<ProductDAO> query, ProductFilter filter)
        {
            List<Product> Products = await query.Select(q => new Product()
            {
                Id = filter.Selects.Contains(ProductSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProductSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProductSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(ProductSelect.Description) ? q.Description : default(string),
                ScanCode = filter.Selects.Contains(ProductSelect.ScanCode) ? q.ScanCode : default(string),
                ERPCode = filter.Selects.Contains(ProductSelect.ERPCode) ? q.ERPCode : default(string),
                CategoryId = filter.Selects.Contains(ProductSelect.Category) ? q.CategoryId : default(long),
                ProductTypeId = filter.Selects.Contains(ProductSelect.ProductType) ? q.ProductTypeId : default(long),
                BrandId = filter.Selects.Contains(ProductSelect.Brand) ? q.BrandId : default(long?),
                UnitOfMeasureId = filter.Selects.Contains(ProductSelect.UnitOfMeasure) ? q.UnitOfMeasureId : default(long),
                UnitOfMeasureGroupingId = filter.Selects.Contains(ProductSelect.UnitOfMeasureGrouping) ? q.UnitOfMeasureGroupingId : default(long?),
                SalePrice = filter.Selects.Contains(ProductSelect.SalePrice) ? q.SalePrice : default(decimal),
                RetailPrice = filter.Selects.Contains(ProductSelect.RetailPrice) ? q.RetailPrice : default(decimal?),
                TaxTypeId = filter.Selects.Contains(ProductSelect.TaxType) ? q.TaxTypeId : default(long),
                StatusId = filter.Selects.Contains(ProductSelect.Status) ? q.StatusId : default(long),
                OtherName = filter.Selects.Contains(ProductSelect.OtherName) ? q.OtherName : default(string),
                TechnicalName = filter.Selects.Contains(ProductSelect.TechnicalName) ? q.TechnicalName : default(string),
                Note = filter.Selects.Contains(ProductSelect.Note) ? q.Note : default(string),
                IsNew = filter.Selects.Contains(ProductSelect.IsNew) ? q.IsNew : default(bool),
                UsedVariationId = filter.Selects.Contains(ProductSelect.UsedVariation) ? q.UsedVariationId : default(long),
                Used = filter.Selects.Contains(ProductSelect.Used) ? q.Used : default(bool),
                Brand = filter.Selects.Contains(ProductSelect.Brand) && q.Brand != null ? new Brand
                {
                    Id = q.Brand.Id,
                    Code = q.Brand.Code,
                    Name = q.Brand.Name,
                    StatusId = q.Brand.StatusId,
                    Description = q.Brand.Description,
                    Used = q.Brand.Used,
                } : null,
                Category = filter.Selects.Contains(ProductSelect.Category) && q.Category != null ? new Category
                {
                    Id = q.Category.Id,
                    Code = q.Category.Code,
                    Name = q.Category.Name,
                    Path = q.Category.Path,
                    ParentId = q.Category.ParentId,
                    StatusId = q.Category.StatusId,
                    Level = q.Category.Level
                } : null,
                ProductType = filter.Selects.Contains(ProductSelect.ProductType) && q.ProductType != null ? new ProductType
                {
                    Id = q.ProductType.Id,
                    Code = q.ProductType.Code,
                    Name = q.ProductType.Name,
                    Description = q.ProductType.Description,
                    StatusId = q.ProductType.StatusId,
                    Used = q.ProductType.Used,
                } : null,
                TaxType = filter.Selects.Contains(ProductSelect.TaxType) && q.TaxType != null ? new TaxType
                {
                    Id = q.TaxType.Id,
                    Code = q.TaxType.Code,
                    Name = q.TaxType.Name,
                    Percentage = q.TaxType.Percentage,
                    StatusId = q.TaxType.StatusId,
                    Used = q.TaxType.Used,
                } : null,
                UnitOfMeasure = filter.Selects.Contains(ProductSelect.UnitOfMeasure) && q.UnitOfMeasure != null ? new UnitOfMeasure
                {
                    Id = q.UnitOfMeasure.Id,
                    Code = q.UnitOfMeasure.Code,
                    Name = q.UnitOfMeasure.Name,
                    Description = q.UnitOfMeasure.Description,
                    StatusId = q.UnitOfMeasure.StatusId,
                    Used = q.UnitOfMeasure.Used,
                } : null,
                UnitOfMeasureGrouping = filter.Selects.Contains(ProductSelect.UnitOfMeasureGrouping) && q.UnitOfMeasureGrouping != null ? new UnitOfMeasureGrouping
                {
                    Id = q.UnitOfMeasureGrouping.Id,
                    Code = q.UnitOfMeasureGrouping.Code,
                    Name = q.UnitOfMeasureGrouping.Name,
                    Description = q.UnitOfMeasureGrouping.Description,
                    UnitOfMeasureId = q.UnitOfMeasureGrouping.UnitOfMeasureId,
                    StatusId = q.UnitOfMeasureGrouping.StatusId,
                    Used = q.UnitOfMeasureGrouping.Used,
                } : null,
                ProductProductGroupingMappings = filter.Selects.Contains(ProductSelect.ProductProductGroupingMapping) && q.ProductProductGroupingMappings != null ?
                q.ProductProductGroupingMappings.Select(p => new ProductProductGroupingMapping
                {
                    ProductId = p.ProductId,
                    ProductGroupingId = p.ProductGroupingId,
                    ProductGrouping = new ProductGrouping
                    {
                        Id = p.ProductGrouping.Id,
                        Code = p.ProductGrouping.Code,
                        Name = p.ProductGrouping.Name,
                        ParentId = p.ProductGrouping.ParentId,
                        Path = p.ProductGrouping.Path,
                        Description = p.ProductGrouping.Description,
                    },
                }).ToList() : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return Products;
        }

        public async Task<int> Count(ProductFilter filter)
        {
            IQueryable<ProductDAO> Products = DataContext.Product.AsNoTracking();
            Products = DynamicFilter(Products, filter);
            return await Products.CountAsync();
        }

        public async Task<List<Product>> List(ProductFilter filter)
        {
            if (filter == null) return new List<Product>();
            IQueryable<ProductDAO> ProductDAOs = DataContext.Product.AsNoTracking();
            ProductDAOs = DynamicFilter(ProductDAOs, filter);
            ProductDAOs = DynamicOrder(ProductDAOs, filter);
            List<Product> Products = await DynamicSelect(ProductDAOs, filter);
            return Products;
        }

        public async Task<Product> Get(long Id)
        {
            Product Product = await DataContext.Product.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new Product()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                ScanCode = x.ScanCode,
                ERPCode = x.ERPCode,
                CategoryId = x.CategoryId,
                ProductTypeId = x.ProductTypeId,
                BrandId = x.BrandId,
                UnitOfMeasureId = x.UnitOfMeasureId,
                UnitOfMeasureGroupingId = x.UnitOfMeasureGroupingId,
                SalePrice = x.SalePrice,
                RetailPrice = x.RetailPrice,
                TaxTypeId = x.TaxTypeId,
                StatusId = x.StatusId,
                OtherName = x.OtherName,
                TechnicalName = x.TechnicalName,
                Note = x.Note,
                IsNew = x.IsNew,
                UsedVariationId = x.UsedVariationId,
                Used = x.Used,
                Brand = x.Brand == null ? null : new Brand
                {
                    Id = x.Brand.Id,
                    Code = x.Brand.Code,
                    Name = x.Brand.Name,
                    StatusId = x.Brand.StatusId,
                    Description = x.Brand.Description,
                    Used = x.Brand.Used,
                },
                Category = x.Category == null ? null : new Category
                {
                    Id = x.Category.Id,
                    Code = x.Category.Code,
                    Name = x.Category.Name,
                    Path = x.Category.Path,
                    ParentId = x.Category.ParentId,
                    StatusId = x.Category.StatusId,
                    Level = x.Category.Level
                },
                ProductType = x.ProductType == null ? null : new ProductType
                {
                    Id = x.ProductType.Id,
                    Code = x.ProductType.Code,
                    Name = x.ProductType.Name,
                    Description = x.ProductType.Description,
                    StatusId = x.ProductType.StatusId,
                    Used = x.ProductType.Used,
                },
                TaxType = x.TaxType == null ? null : new TaxType
                {
                    Id = x.TaxType.Id,
                    Code = x.TaxType.Code,
                    Name = x.TaxType.Name,
                    Percentage = x.TaxType.Percentage,
                    StatusId = x.TaxType.StatusId,
                    Used = x.TaxType.Used,
                },
                UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                {
                    Id = x.UnitOfMeasure.Id,
                    Code = x.UnitOfMeasure.Code,
                    Name = x.UnitOfMeasure.Name,
                    Description = x.UnitOfMeasure.Description,
                    StatusId = x.UnitOfMeasure.StatusId,
                    Used = x.UnitOfMeasure.Used,
                },
                UnitOfMeasureGrouping = x.UnitOfMeasureGrouping == null ? null : new UnitOfMeasureGrouping
                {
                    Id = x.UnitOfMeasureGrouping.Id,
                    Code = x.UnitOfMeasureGrouping.Code,
                    Name = x.UnitOfMeasureGrouping.Name,
                    Description = x.UnitOfMeasureGrouping.Description,
                    UnitOfMeasureId = x.UnitOfMeasureGrouping.UnitOfMeasureId,
                    StatusId = x.UnitOfMeasureGrouping.StatusId,
                    Used = x.UnitOfMeasureGrouping.Used,
                },
            }).FirstOrDefaultAsync();

            if (Product == null)
                return null;
            if (Product.UnitOfMeasureGrouping != null)
            {
                Product.UnitOfMeasureGrouping.UnitOfMeasureGroupingContents = await DataContext.UnitOfMeasureGroupingContent
                    .Where(uomgc => uomgc.UnitOfMeasureGroupingId == Product.UnitOfMeasureGroupingId.Value)
                    .Select(uomgc => new UnitOfMeasureGroupingContent
                    {
                        Id = uomgc.Id,
                        Factor = uomgc.Factor,
                        UnitOfMeasureId = uomgc.UnitOfMeasureId,
                        UnitOfMeasure = new UnitOfMeasure
                        {
                            Id = uomgc.UnitOfMeasure.Id,
                            Code = uomgc.UnitOfMeasure.Code,
                            Name = uomgc.UnitOfMeasure.Name,
                            Description = uomgc.UnitOfMeasure.Description,
                            StatusId = uomgc.UnitOfMeasure.StatusId,
                        }
                    }).ToListAsync();
            }
            
            Product.ProductImageMappings = await DataContext.ProductImageMapping.AsNoTracking()
                .Where(x => x.ProductId == Product.Id)
                .Select(x => new ProductImageMapping
                {
                    ProductId = x.ProductId,
                    ImageId = x.ImageId,
                    Image = new Image
                    {
                        Id = x.Image.Id,
                        Name = x.Image.Name,
                        Url = x.Image.Url,
                    },
                }).ToListAsync();

            Product.ProductProductGroupingMappings = await DataContext.ProductProductGroupingMapping.AsNoTracking()
                .Where(x => x.ProductId == Product.Id)
                .Where(x => x.ProductGrouping.DeletedAt == null)
                .Select(x => new ProductProductGroupingMapping
                {
                    ProductId = x.ProductId,
                    ProductGroupingId = x.ProductGroupingId,
                    ProductGrouping = new ProductGrouping
                    {
                        Id = x.ProductGrouping.Id,
                        Code = x.ProductGrouping.Code,
                        Name = x.ProductGrouping.Name,
                        ParentId = x.ProductGrouping.ParentId,
                        Path = x.ProductGrouping.Path,
                        Description = x.ProductGrouping.Description,
                    },
                }).ToListAsync();

            if (Product == null)
                return null;

            return Product;
        }
    }
}
