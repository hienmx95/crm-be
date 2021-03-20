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
    public interface IDirectSalesOrderRepository
    {
        Task<int> Count(DirectSalesOrderFilter DirectSalesOrderFilter);
        Task<List<DirectSalesOrder>> List(DirectSalesOrderFilter DirectSalesOrderFilter);
        Task<DirectSalesOrder> Get(long Id);
    }
    public class DirectSalesOrderRepository : IDirectSalesOrderRepository
    {
        private DataContext DataContext;
        public DirectSalesOrderRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<DirectSalesOrderDAO> DynamicFilter(IQueryable<DirectSalesOrderDAO> query, DirectSalesOrderFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.OrganizationId != null)
            {
                if (filter.OrganizationId.Equal != null)
                {
                    OrganizationDAO OrganizationDAO = DataContext.Organization
                        .Where(o => o.Id == filter.OrganizationId.Equal.Value).FirstOrDefault();
                    query = query.Where(q => q.Organization.Path.StartsWith(OrganizationDAO.Path));
                }
                if (filter.OrganizationId.NotEqual != null)
                {
                    OrganizationDAO OrganizationDAO = DataContext.Organization
                        .Where(o => o.Id == filter.OrganizationId.NotEqual.Value).FirstOrDefault();
                    query = query.Where(q => !q.Organization.Path.StartsWith(OrganizationDAO.Path));
                }
                if (filter.OrganizationId.In != null)
                {
                    List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                        .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                    List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => filter.OrganizationId.In.Contains(o.Id)).ToList();
                    List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                    List<long> Ids = Branches.Select(o => o.Id).ToList();
                    query = query.Where(q => Ids.Contains(q.OrganizationId));
                }
                if (filter.OrganizationId.NotIn != null)
                {
                    List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                        .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                    List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => filter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                    List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                    List<long> Ids = Branches.Select(o => o.Id).ToList();
                    query = query.Where(q => !Ids.Contains(q.OrganizationId));
                }
            }
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.BuyerStoreId != null)
                query = query.Where(q => q.BuyerStoreId, filter.BuyerStoreId);
            if (filter.PhoneNumber != null)
                query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            if (filter.StoreAddress != null)
                query = query.Where(q => q.StoreAddress, filter.StoreAddress);
            if (filter.DeliveryAddress != null)
                query = query.Where(q => q.DeliveryAddress, filter.DeliveryAddress);
            if (filter.AppUserId != null)
                query = query.Where(q => q.SaleEmployeeId, filter.AppUserId);
            if (filter.OrderDate != null)
                query = query.Where(q => q.OrderDate, filter.OrderDate);
            if (filter.DeliveryDate != null)
                query = query.Where(q => q.DeliveryDate, filter.DeliveryDate);
            if (filter.EditedPriceStatusId != null)
                query = query.Where(q => q.EditedPriceStatusId, filter.EditedPriceStatusId);
            if (filter.Note != null)
                query = query.Where(q => q.Note, filter.Note);
            if (filter.RequestStateId != null)
                query = query.Where(q => q.RequestStateId, filter.RequestStateId);
            if (filter.SubTotal != null)
                query = query.Where(q => q.SubTotal, filter.SubTotal);
            if (filter.GeneralDiscountPercentage != null)
                query = query.Where(q => q.GeneralDiscountPercentage, filter.GeneralDiscountPercentage);
            if (filter.GeneralDiscountAmount != null)
                query = query.Where(q => q.GeneralDiscountAmount, filter.GeneralDiscountAmount);
            if (filter.PromotionCode != null && filter.PromotionCode.HasValue)
                query = query.Where(q => q.PromotionCode, filter.PromotionCode);
            if (filter.TotalTaxAmount != null)
                query = query.Where(q => q.TotalTaxAmount, filter.TotalTaxAmount);
            if (filter.Total != null)
                query = query.Where(q => q.Total, filter.Total);
            if (filter.StoreStatusId != null)
                query = query.Where(q => q.BuyerStore.StoreStatusId, filter.StoreStatusId);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
            {
                var StoreIds = DataContext.Store.Where(x => x.CustomerId, filter.CustomerId).Select(x => x.Id).ToList();
                query = query.Where(x => StoreIds.Contains(x.BuyerStoreId));
            }
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
            {
                var CustomerIds = DataContext.Customer.Where(x => x.CompanyId, filter.CompanyId).Select(x => x.Id).ToList();
                var StoreIds = DataContext.Store.Where(x => x.CustomerId.HasValue && CustomerIds.Contains(x.CustomerId.Value)).Select(x => x.Id).ToList();
                query = query.Where(x => StoreIds.Contains(x.BuyerStoreId));
            }
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<DirectSalesOrderDAO> OrFilter(IQueryable<DirectSalesOrderDAO> query, DirectSalesOrderFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<DirectSalesOrderDAO> initQuery = query.Where(q => false);
            foreach (DirectSalesOrderFilter DirectSalesOrderFilter in filter.OrFilter)
            {
                IQueryable<DirectSalesOrderDAO> queryable = query;
                if (DirectSalesOrderFilter.UserId != null)
                {
                    if (DirectSalesOrderFilter.UserId.Equal != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == DirectSalesOrderFilter.UserId.Equal).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (DirectSalesOrderFilter.UserId.NotEqual != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == DirectSalesOrderFilter.UserId.NotEqual).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id != AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => !q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                }
                if (DirectSalesOrderFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.CreatorId, DirectSalesOrderFilter.AppUserId);

                if (DirectSalesOrderFilter.OrganizationId != null)
                {
                    if (DirectSalesOrderFilter.OrganizationId.Equal != null)
                    {
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == DirectSalesOrderFilter.OrganizationId.Equal.Value).FirstOrDefault();
                        queryable = queryable.Where(q => q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (DirectSalesOrderFilter.OrganizationId.NotEqual != null)
                    {
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == DirectSalesOrderFilter.OrganizationId.NotEqual.Value).FirstOrDefault();
                        queryable = queryable.Where(q => !q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (DirectSalesOrderFilter.OrganizationId.In != null)
                    {
                        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => DirectSalesOrderFilter.OrganizationId.In.Contains(o.Id)).ToList();
                        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                        List<long> Ids = Branches.Select(o => o.Id).ToList();
                        queryable = queryable.Where(q => Ids.Contains(q.Creator.OrganizationId));
                    }
                    if (DirectSalesOrderFilter.OrganizationId.NotIn != null)
                    {
                        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => DirectSalesOrderFilter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                        List<long> Ids = Branches.Select(o => o.Id).ToList();
                        queryable = queryable.Where(q => !Ids.Contains(q.Creator.OrganizationId));
                    }
                }

                //if (DirectSalesOrderFilter.OrganizationId != null)
                //{
                //    if (DirectSalesOrderFilter.OrganizationId.Equal != null)
                //    {
                //        OrganizationDAO OrganizationDAO = DataContext.Organization
                //            .Where(o => o.Id == DirectSalesOrderFilter.OrganizationId.Equal.Value).FirstOrDefault();
                //        queryable = queryable.Where(q => q.Organization.Path.StartsWith(OrganizationDAO.Path));
                //    }
                //    if (DirectSalesOrderFilter.OrganizationId.NotEqual != null)
                //    {
                //        OrganizationDAO OrganizationDAO = DataContext.Organization
                //            .Where(o => o.Id == DirectSalesOrderFilter.OrganizationId.NotEqual.Value).FirstOrDefault();
                //        queryable = queryable.Where(q => !q.Organization.Path.StartsWith(OrganizationDAO.Path));
                //    }
                //    if (DirectSalesOrderFilter.OrganizationId.In != null)
                //    {
                //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => DirectSalesOrderFilter.OrganizationId.In.Contains(o.Id)).ToList();
                //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                //        List<long> Ids = Branches.Select(o => o.Id).ToList();
                //        queryable = queryable.Where(q => Ids.Contains(q.OrganizationId));
                //    }
                //    if (DirectSalesOrderFilter.OrganizationId.NotIn != null)
                //    {
                //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => DirectSalesOrderFilter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                //        List<long> Ids = Branches.Select(o => o.Id).ToList();
                //        queryable = queryable.Where(q => !Ids.Contains(q.OrganizationId));
                //    }
                //}

                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<DirectSalesOrderDAO> DynamicOrder(IQueryable<DirectSalesOrderDAO> query, DirectSalesOrderFilter filter)
        {
            query = query.Distinct();
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case DirectSalesOrderOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DirectSalesOrderOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case DirectSalesOrderOrder.Organization:
                            query = query.OrderBy(q => q.OrganizationId);
                            break;
                        case DirectSalesOrderOrder.BuyerStore:
                            query = query.OrderBy(q => q.BuyerStoreId);
                            break;
                        case DirectSalesOrderOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case DirectSalesOrderOrder.StoreAddress:
                            query = query.OrderBy(q => q.StoreAddress);
                            break;
                        case DirectSalesOrderOrder.DeliveryAddress:
                            query = query.OrderBy(q => q.DeliveryAddress);
                            break;
                        case DirectSalesOrderOrder.SaleEmployee:
                            query = query.OrderBy(q => q.SaleEmployeeId);
                            break;
                        case DirectSalesOrderOrder.OrderDate:
                            query = query.OrderBy(q => q.OrderDate);
                            break;
                        case DirectSalesOrderOrder.DeliveryDate:
                            query = query.OrderBy(q => q.DeliveryDate);
                            break;
                        case DirectSalesOrderOrder.EditedPriceStatus:
                            query = query.OrderBy(q => q.EditedPriceStatusId);
                            break;
                        case DirectSalesOrderOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        case DirectSalesOrderOrder.RequestState:
                            query = query.OrderBy(q => q.RequestStateId);
                            break;
                        case DirectSalesOrderOrder.SubTotal:
                            query = query.OrderBy(q => q.SubTotal);
                            break;
                        case DirectSalesOrderOrder.GeneralDiscountPercentage:
                            query = query.OrderBy(q => q.GeneralDiscountPercentage);
                            break;
                        case DirectSalesOrderOrder.GeneralDiscountAmount:
                            query = query.OrderBy(q => q.GeneralDiscountAmount);
                            break;
                        case DirectSalesOrderOrder.TotalTaxAmount:
                            query = query.OrderBy(q => q.TotalTaxAmount);
                            break;
                        case DirectSalesOrderOrder.Total:
                            query = query.OrderBy(q => q.Total);
                            break;
                        case DirectSalesOrderOrder.CreatedAt:
                            query = query.OrderBy(q => q.CreatedAt);
                            break;
                        case DirectSalesOrderOrder.UpdatedAt:
                            query = query.OrderBy(q => q.UpdatedAt);
                            break;
                        default:
                            query = query.OrderBy(q => q.UpdatedAt);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case DirectSalesOrderOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DirectSalesOrderOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case DirectSalesOrderOrder.Organization:
                            query = query.OrderByDescending(q => q.OrganizationId);
                            break;
                        case DirectSalesOrderOrder.BuyerStore:
                            query = query.OrderByDescending(q => q.BuyerStoreId);
                            break;
                        case DirectSalesOrderOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                        case DirectSalesOrderOrder.StoreAddress:
                            query = query.OrderByDescending(q => q.StoreAddress);
                            break;
                        case DirectSalesOrderOrder.DeliveryAddress:
                            query = query.OrderByDescending(q => q.DeliveryAddress);
                            break;
                        case DirectSalesOrderOrder.SaleEmployee:
                            query = query.OrderByDescending(q => q.SaleEmployeeId);
                            break;
                        case DirectSalesOrderOrder.OrderDate:
                            query = query.OrderByDescending(q => q.OrderDate);
                            break;
                        case DirectSalesOrderOrder.DeliveryDate:
                            query = query.OrderByDescending(q => q.DeliveryDate);
                            break;
                        case DirectSalesOrderOrder.EditedPriceStatus:
                            query = query.OrderByDescending(q => q.EditedPriceStatusId);
                            break;
                        case DirectSalesOrderOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
                            break;
                        case DirectSalesOrderOrder.RequestState:
                            query = query.OrderByDescending(q => q.RequestStateId);
                            break;
                        case DirectSalesOrderOrder.SubTotal:
                            query = query.OrderByDescending(q => q.SubTotal);
                            break;
                        case DirectSalesOrderOrder.GeneralDiscountPercentage:
                            query = query.OrderByDescending(q => q.GeneralDiscountPercentage);
                            break;
                        case DirectSalesOrderOrder.GeneralDiscountAmount:
                            query = query.OrderByDescending(q => q.GeneralDiscountAmount);
                            break;
                        case DirectSalesOrderOrder.TotalTaxAmount:
                            query = query.OrderByDescending(q => q.TotalTaxAmount);
                            break;
                        case DirectSalesOrderOrder.Total:
                            query = query.OrderByDescending(q => q.Total);
                            break;
                        case DirectSalesOrderOrder.CreatedAt:
                            query = query.OrderByDescending(q => q.CreatedAt);
                            break;
                        case DirectSalesOrderOrder.UpdatedAt:
                            query = query.OrderByDescending(q => q.UpdatedAt);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.UpdatedAt);
                            break;
                    }
                    break;
                default:
                    query = query.OrderByDescending(q => q.UpdatedAt);
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<DirectSalesOrder>> DynamicSelect(IQueryable<DirectSalesOrderDAO> query, DirectSalesOrderFilter filter)
        {
            List<DirectSalesOrder> DirectSalesOrders = await query.Select(q => new DirectSalesOrder()
            {
                Id = filter.Selects.Contains(DirectSalesOrderSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(DirectSalesOrderSelect.Code) ? q.Code : default(string),
                OrganizationId = filter.Selects.Contains(DirectSalesOrderSelect.Organization) ? q.OrganizationId : default(long),
                BuyerStoreId = filter.Selects.Contains(DirectSalesOrderSelect.BuyerStore) ? q.BuyerStoreId : default(long),
                PhoneNumber = filter.Selects.Contains(DirectSalesOrderSelect.PhoneNumber) ? q.PhoneNumber : default(string),
                StoreAddress = filter.Selects.Contains(DirectSalesOrderSelect.StoreAddress) ? q.StoreAddress : default(string),
                DeliveryAddress = filter.Selects.Contains(DirectSalesOrderSelect.DeliveryAddress) ? q.DeliveryAddress : default(string),
                SaleEmployeeId = filter.Selects.Contains(DirectSalesOrderSelect.SaleEmployee) ? q.SaleEmployeeId : default(long),
                OrderDate = filter.Selects.Contains(DirectSalesOrderSelect.OrderDate) ? q.OrderDate : default(DateTime),
                DeliveryDate = filter.Selects.Contains(DirectSalesOrderSelect.DeliveryDate) ? q.DeliveryDate : default(DateTime?),
                EditedPriceStatusId = filter.Selects.Contains(DirectSalesOrderSelect.EditedPriceStatus) ? q.EditedPriceStatusId : default(long),
                Note = filter.Selects.Contains(DirectSalesOrderSelect.Note) ? q.Note : default(string),
                RequestStateId = filter.Selects.Contains(DirectSalesOrderSelect.RequestState) ? q.RequestStateId : default(long),
                SubTotal = filter.Selects.Contains(DirectSalesOrderSelect.SubTotal) ? q.SubTotal : default(decimal),
                GeneralDiscountPercentage = filter.Selects.Contains(DirectSalesOrderSelect.GeneralDiscountPercentage) ? q.GeneralDiscountPercentage : default(decimal?),
                GeneralDiscountAmount = filter.Selects.Contains(DirectSalesOrderSelect.GeneralDiscountAmount) ? q.GeneralDiscountAmount : default(decimal?),
                PromotionCode = filter.Selects.Contains(DirectSalesOrderSelect.PromotionCode) ? q.PromotionCode : default(string),
                PromotionValue = filter.Selects.Contains(DirectSalesOrderSelect.PromotionValue) ? q.PromotionValue : default(decimal?),
                TotalTaxAmount = filter.Selects.Contains(DirectSalesOrderSelect.TotalTaxAmount) ? q.TotalTaxAmount : default(decimal),
                TotalAfterTax = filter.Selects.Contains(DirectSalesOrderSelect.TotalAfterTax) ? q.TotalAfterTax : default(decimal),
                Total = filter.Selects.Contains(DirectSalesOrderSelect.Total) ? q.Total : default(decimal),
                BuyerStore = filter.Selects.Contains(DirectSalesOrderSelect.BuyerStore) && q.BuyerStore != null ? new Store
                {
                    Id = q.BuyerStore.Id,
                    Code = q.BuyerStore.Code,
                    CodeDraft = q.BuyerStore.CodeDraft,
                    Name = q.BuyerStore.Name,
                    ParentStoreId = q.BuyerStore.ParentStoreId,
                    OrganizationId = q.BuyerStore.OrganizationId,
                    StoreTypeId = q.BuyerStore.StoreTypeId,
                    StoreGroupingId = q.BuyerStore.StoreGroupingId,
                    Telephone = q.BuyerStore.Telephone,
                    ProvinceId = q.BuyerStore.ProvinceId,
                    DistrictId = q.BuyerStore.DistrictId,
                    WardId = q.BuyerStore.WardId,
                    Address = q.BuyerStore.Address,
                    DeliveryAddress = q.BuyerStore.DeliveryAddress,
                    Latitude = q.BuyerStore.Latitude,
                    Longitude = q.BuyerStore.Longitude,
                    DeliveryLatitude = q.BuyerStore.DeliveryLatitude,
                    DeliveryLongitude = q.BuyerStore.DeliveryLongitude,
                    OwnerName = q.BuyerStore.OwnerName,
                    OwnerPhone = q.BuyerStore.OwnerPhone,
                    OwnerEmail = q.BuyerStore.OwnerEmail,
                    TaxCode = q.BuyerStore.TaxCode,
                    LegalEntity = q.BuyerStore.LegalEntity,
                    StatusId = q.BuyerStore.StatusId,
                } : null,
                EditedPriceStatus = filter.Selects.Contains(DirectSalesOrderSelect.EditedPriceStatus) && q.EditedPriceStatus != null ? new EditedPriceStatus
                {
                    Id = q.EditedPriceStatus.Id,
                    Code = q.EditedPriceStatus.Code,
                    Name = q.EditedPriceStatus.Name,
                } : null,
                Organization = filter.Selects.Contains(DirectSalesOrderSelect.Organization) && q.Organization != null ? new Organization
                {
                    Id = q.Organization.Id,
                    Code = q.Organization.Code,
                    Name = q.Organization.Name,
                    Address = q.Organization.Address,
                    Phone = q.Organization.Phone,
                    Path = q.Organization.Path,
                    ParentId = q.Organization.ParentId,
                    Email = q.Organization.Email,
                    StatusId = q.Organization.StatusId,
                    Level = q.Organization.Level
                } : null,
                RequestState = filter.Selects.Contains(DirectSalesOrderSelect.RequestState) && q.RequestState != null ? new RequestState
                {
                    Id = q.RequestState.Id,
                    Code = q.RequestState.Code,
                    Name = q.RequestState.Name,
                } : null,
                SaleEmployee = filter.Selects.Contains(DirectSalesOrderSelect.SaleEmployee) && q.SaleEmployee != null ? new AppUser
                {
                    Id = q.SaleEmployee.Id,
                    Username = q.SaleEmployee.Username,
                    DisplayName = q.SaleEmployee.DisplayName,
                    Address = q.SaleEmployee.Address,
                    Email = q.SaleEmployee.Email,
                    Phone = q.SaleEmployee.Phone,
                } : null,
                RowId = q.RowId
            }).ToListAsync();
            return DirectSalesOrders;
        }

        public async Task<int> Count(DirectSalesOrderFilter filter)
        {
            IQueryable<DirectSalesOrderDAO> DirectSalesOrderDAOs = DataContext.DirectSalesOrder.AsNoTracking();
            DirectSalesOrderDAOs = DynamicFilter(DirectSalesOrderDAOs, filter);
            DirectSalesOrderDAOs = OrFilter(DirectSalesOrderDAOs, filter);
            return await DirectSalesOrderDAOs.Distinct().CountAsync();
        }

        public async Task<List<DirectSalesOrder>> List(DirectSalesOrderFilter filter)
        {
            if (filter == null) return new List<DirectSalesOrder>();
            IQueryable<DirectSalesOrderDAO> DirectSalesOrderDAOs = DataContext.DirectSalesOrder.AsNoTracking();
            DirectSalesOrderDAOs = DynamicFilter(DirectSalesOrderDAOs, filter);
            DirectSalesOrderDAOs = OrFilter(DirectSalesOrderDAOs, filter);
            DirectSalesOrderDAOs = DynamicOrder(DirectSalesOrderDAOs, filter);
            List<DirectSalesOrder> DirectSalesOrders = await DynamicSelect(DirectSalesOrderDAOs, filter);
            return DirectSalesOrders;
        }

        public async Task<DirectSalesOrder> Get(long Id)
        {
            DirectSalesOrder DirectSalesOrder = await DataContext.DirectSalesOrder.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new DirectSalesOrder()
            {
                Id = x.Id,
                Code = x.Code,
                OrganizationId = x.OrganizationId,
                BuyerStoreId = x.BuyerStoreId,
                PhoneNumber = x.PhoneNumber,
                StoreAddress = x.StoreAddress,
                DeliveryAddress = x.DeliveryAddress,
                SaleEmployeeId = x.SaleEmployeeId,
                OrderDate = x.OrderDate,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeliveryDate = x.DeliveryDate,
                EditedPriceStatusId = x.EditedPriceStatusId,
                Note = x.Note,
                RequestStateId = x.RequestStateId,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                PromotionCode = x.PromotionCode,
                PromotionValue = x.PromotionValue,
                TotalTaxAmount = x.TotalTaxAmount,
                TotalAfterTax = x.TotalAfterTax,
                Total = x.Total,
                RowId = x.RowId,
                BuyerStore = x.BuyerStore == null ? null : new Store
                {
                    Id = x.BuyerStore.Id,
                    Code = x.BuyerStore.Code,
                    CodeDraft = x.BuyerStore.CodeDraft,
                    Name = x.BuyerStore.Name,
                    ParentStoreId = x.BuyerStore.ParentStoreId,
                    OrganizationId = x.BuyerStore.OrganizationId,
                    StoreTypeId = x.BuyerStore.StoreTypeId,
                    StoreGroupingId = x.BuyerStore.StoreGroupingId,
                    Telephone = x.BuyerStore.Telephone,
                    ProvinceId = x.BuyerStore.ProvinceId,
                    DistrictId = x.BuyerStore.DistrictId,
                    WardId = x.BuyerStore.WardId,
                    Address = x.BuyerStore.Address,
                    DeliveryAddress = x.BuyerStore.DeliveryAddress,
                    Latitude = x.BuyerStore.Latitude,
                    Longitude = x.BuyerStore.Longitude,
                    DeliveryLatitude = x.BuyerStore.DeliveryLatitude,
                    DeliveryLongitude = x.BuyerStore.DeliveryLongitude,
                    OwnerName = x.BuyerStore.OwnerName,
                    OwnerPhone = x.BuyerStore.OwnerPhone,
                    OwnerEmail = x.BuyerStore.OwnerEmail,
                    TaxCode = x.BuyerStore.TaxCode,
                    LegalEntity = x.BuyerStore.LegalEntity,
                    StatusId = x.BuyerStore.StatusId,
                },
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                Organization = x.Organization == null ? null : new Organization
                {
                    Id = x.Organization.Id,
                    Code = x.Organization.Code,
                    Name = x.Organization.Name,
                    Address = x.Organization.Address,
                    Phone = x.Organization.Phone,
                    Path = x.Organization.Path,
                    ParentId = x.Organization.ParentId,
                    Email = x.Organization.Email,
                    StatusId = x.Organization.StatusId,
                    Level = x.Organization.Level
                },
                RequestState = x.RequestState == null ? null : new RequestState
                {
                    Id = x.RequestState.Id,
                    Code = x.RequestState.Code,
                    Name = x.RequestState.Name,
                },
                SaleEmployee = x.SaleEmployee == null ? null : new AppUser
                {
                    Id = x.SaleEmployee.Id,
                    Username = x.SaleEmployee.Username,
                    DisplayName = x.SaleEmployee.DisplayName,
                    Address = x.SaleEmployee.Address,
                    Email = x.SaleEmployee.Email,
                    Phone = x.SaleEmployee.Phone,
                },
            }).FirstOrDefaultAsync();

            if (DirectSalesOrder == null)
                return null;

            RequestWorkflowDefinitionMappingDAO RequestWorkflowDefinitionMappingDAO = await DataContext.RequestWorkflowDefinitionMapping
              .Where(x => DirectSalesOrder.RowId == x.RequestId)
              .Include(x => x.RequestState)
              .AsNoTracking()
              .FirstOrDefaultAsync();
            if (RequestWorkflowDefinitionMappingDAO != null)
            {
                DirectSalesOrder.RequestStateId = RequestWorkflowDefinitionMappingDAO.RequestStateId;
                DirectSalesOrder.RequestState = new RequestState
                {
                    Id = RequestWorkflowDefinitionMappingDAO.RequestState.Id,
                    Code = RequestWorkflowDefinitionMappingDAO.RequestState.Code,
                    Name = RequestWorkflowDefinitionMappingDAO.RequestState.Name,
                };
            }

            DirectSalesOrder.DirectSalesOrderContents = await DataContext.DirectSalesOrderContent.AsNoTracking()
                .Where(x => x.DirectSalesOrderId == DirectSalesOrder.Id)
                .Select(x => new DirectSalesOrderContent
                {
                    Id = x.Id,
                    DirectSalesOrderId = x.DirectSalesOrderId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    EditedPriceStatusId = x.EditedPriceStatusId,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    Amount = x.Amount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    Factor = x.Factor,
                    EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = new Item
                    {
                        Id = x.Item.Id,
                        Code = x.Item.Code,
                        Name = x.Item.Name,
                        ProductId = x.Item.ProductId,
                        RetailPrice = x.Item.RetailPrice,
                        SalePrice = x.Item.SalePrice,
                        ScanCode = x.Item.ScanCode,
                        StatusId = x.Item.StatusId,
                        Product = new Product
                        {
                            Id = x.Item.Product.Id,
                            Code = x.Item.Product.Code,
                            Name = x.Item.Product.Name,
                            TaxTypeId = x.Item.Product.TaxTypeId,
                            UnitOfMeasureId = x.Item.Product.UnitOfMeasureId,
                            UnitOfMeasureGroupingId = x.Item.Product.UnitOfMeasureGroupingId,
                            TaxType = new TaxType
                            {
                                Id = x.Item.Product.TaxType.Id,
                                Code = x.Item.Product.TaxType.Code,
                                Name = x.Item.Product.TaxType.Name,
                                StatusId = x.Item.Product.TaxType.StatusId,
                                Percentage = x.Item.Product.TaxType.Percentage,
                            },
                            UnitOfMeasure = new UnitOfMeasure
                            {
                                Id = x.Item.Product.UnitOfMeasure.Id,
                                Code = x.Item.Product.UnitOfMeasure.Code,
                                Name = x.Item.Product.UnitOfMeasure.Name,
                                Description = x.Item.Product.UnitOfMeasure.Description,
                                StatusId = x.Item.Product.UnitOfMeasure.StatusId,
                            },
                            UnitOfMeasureGrouping = new UnitOfMeasureGrouping
                            {
                                Id = x.Item.Product.UnitOfMeasureGrouping.Id,
                                Code = x.Item.Product.UnitOfMeasureGrouping.Code,
                                Name = x.Item.Product.UnitOfMeasureGrouping.Name,
                                Description = x.Item.Product.UnitOfMeasureGrouping.Description,
                                StatusId = x.Item.Product.UnitOfMeasureGrouping.StatusId,
                                UnitOfMeasureId = x.Item.Product.UnitOfMeasureGrouping.UnitOfMeasureId
                            }
                        }
                    },
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                    },
                }).ToListAsync();
            DirectSalesOrder.DirectSalesOrderPromotions = await DataContext.DirectSalesOrderPromotion.AsNoTracking()
                .Where(x => x.DirectSalesOrderId == DirectSalesOrder.Id)
                .Select(x => new DirectSalesOrderPromotion
                {
                    Id = x.Id,
                    DirectSalesOrderId = x.DirectSalesOrderId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    RequestedQuantity = x.RequestedQuantity,
                    Note = x.Note,
                    Factor = x.Factor,
                    Item = new Item
                    {
                        Id = x.Item.Id,
                        Code = x.Item.Code,
                        Name = x.Item.Name,
                        ProductId = x.Item.ProductId,
                        RetailPrice = x.Item.RetailPrice,
                        SalePrice = x.Item.SalePrice,
                        ScanCode = x.Item.ScanCode,
                        StatusId = x.Item.StatusId,
                        Product = new Product
                        {
                            Id = x.Item.Product.Id,
                            Code = x.Item.Product.Code,
                            Name = x.Item.Product.Name,
                            TaxTypeId = x.Item.Product.TaxTypeId,
                            UnitOfMeasureId = x.Item.Product.UnitOfMeasureId,
                            UnitOfMeasureGroupingId = x.Item.Product.UnitOfMeasureGroupingId,
                            TaxType = new TaxType
                            {
                                Id = x.Item.Product.TaxType.Id,
                                Code = x.Item.Product.TaxType.Code,
                                Name = x.Item.Product.TaxType.Name,
                                StatusId = x.Item.Product.TaxType.StatusId,
                                Percentage = x.Item.Product.TaxType.Percentage,
                            },
                            UnitOfMeasure = new UnitOfMeasure
                            {
                                Id = x.Item.Product.UnitOfMeasure.Id,
                                Code = x.Item.Product.UnitOfMeasure.Code,
                                Name = x.Item.Product.UnitOfMeasure.Name,
                                Description = x.Item.Product.UnitOfMeasure.Description,
                                StatusId = x.Item.Product.UnitOfMeasure.StatusId,
                            },
                            UnitOfMeasureGrouping = new UnitOfMeasureGrouping
                            {
                                Id = x.Item.Product.UnitOfMeasureGrouping.Id,
                                Code = x.Item.Product.UnitOfMeasureGrouping.Code,
                                Name = x.Item.Product.UnitOfMeasureGrouping.Name,
                                Description = x.Item.Product.UnitOfMeasureGrouping.Description,
                                StatusId = x.Item.Product.UnitOfMeasureGrouping.StatusId,
                                UnitOfMeasureId = x.Item.Product.UnitOfMeasureGrouping.UnitOfMeasureId
                            }
                        }
                    },
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                    },
                }).ToListAsync();

            decimal GeneralDiscountAmount = DirectSalesOrder.GeneralDiscountAmount.HasValue ? DirectSalesOrder.GeneralDiscountAmount.Value : 0;
            decimal DiscountAmount = DirectSalesOrder.DirectSalesOrderContents != null ?
                DirectSalesOrder.DirectSalesOrderContents
                .Select(x => x.DiscountAmount.GetValueOrDefault(0))
                .Sum() : 0;
            DirectSalesOrder.TotalDiscountAmount = GeneralDiscountAmount + DiscountAmount;
            DirectSalesOrder.TotalRequestedQuantity = DirectSalesOrder.DirectSalesOrderContents != null ?
                DirectSalesOrder.DirectSalesOrderContents
                .Select(x => x.RequestedQuantity)
                .Sum() : 0;
            return DirectSalesOrder;
        }
    }
}
