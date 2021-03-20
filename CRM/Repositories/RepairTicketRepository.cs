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
    public interface IRepairTicketRepository
    {
        Task<int> Count(RepairTicketFilter RepairTicketFilter);
        Task<List<RepairTicket>> List(RepairTicketFilter RepairTicketFilter);
        Task<List<RepairTicket>> List(List<long> Ids);
        Task<RepairTicket> Get(long Id);
        Task<bool> Create(RepairTicket RepairTicket);
        Task<bool> Update(RepairTicket RepairTicket);
        Task<bool> Delete(RepairTicket RepairTicket);
        Task<bool> BulkMerge(List<RepairTicket> RepairTickets);
        Task<bool> BulkDelete(List<RepairTicket> RepairTickets);
    }
    public class RepairTicketRepository : IRepairTicketRepository
    {
        private DataContext DataContext;
        public RepairTicketRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<RepairTicketDAO> DynamicFilter(IQueryable<RepairTicketDAO> query, RepairTicketFilter filter)
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
            if (filter.DeviceSerial != null && filter.DeviceSerial.HasValue)
                query = query.Where(q => q.DeviceSerial, filter.DeviceSerial);
            if (filter.OrderId != null && filter.OrderId.HasValue)
                query = query.Where(q => q.OrderId, filter.OrderId);
            if (filter.OrderCategoryId != null && filter.OrderCategoryId.HasValue)
                query = query.Where(q => q.OrderCategoryId, filter.OrderCategoryId);
            if (filter.RepairDueDate != null && filter.RepairDueDate.HasValue)
                query = query.Where(q => q.RepairDueDate == null).Union(query.Where(q => q.RepairDueDate.HasValue).Where(q => q.RepairDueDate, filter.RepairDueDate));
            if (filter.ItemId != null && filter.ItemId.HasValue)
                query = query.Where(q => q.ItemId.HasValue).Where(q => q.ItemId, filter.ItemId);
            if (filter.RejectReason != null && filter.RejectReason.HasValue)
                query = query.Where(q => q.RejectReason, filter.RejectReason);
            if (filter.DeviceState != null && filter.DeviceState.HasValue)
                query = query.Where(q => q.DeviceState, filter.DeviceState);
            if (filter.RepairStatusId != null && filter.RepairStatusId.HasValue)
                query = query.Where(q => q.RepairStatusId.HasValue).Where(q => q.RepairStatusId, filter.RepairStatusId);
            if (filter.RepairAddess != null && filter.RepairAddess.HasValue)
                query = query.Where(q => q.RepairAddess, filter.RepairAddess);
            if (filter.ReceiveUser != null && filter.ReceiveUser.HasValue)
                query = query.Where(q => q.ReceiveUser, filter.ReceiveUser);
            if (filter.ReceiveDate != null && filter.ReceiveDate.HasValue)
                query = query.Where(q => q.ReceiveDate == null).Union(query.Where(q => q.ReceiveDate.HasValue).Where(q => q.ReceiveDate, filter.ReceiveDate));
            if (filter.RepairDate != null && filter.RepairDate.HasValue)
                query = query.Where(q => q.RepairDate == null).Union(query.Where(q => q.RepairDate.HasValue).Where(q => q.RepairDate, filter.RepairDate));
            if (filter.ReturnDate != null && filter.ReturnDate.HasValue)
                query = query.Where(q => q.ReturnDate == null).Union(query.Where(q => q.ReturnDate.HasValue).Where(q => q.ReturnDate, filter.ReturnDate));
            if (filter.RepairSolution != null && filter.RepairSolution.HasValue)
                query = query.Where(q => q.RepairSolution, filter.RepairSolution);
            if (filter.Note != null && filter.Note.HasValue)
                query = query.Where(q => q.Note, filter.Note);
            if (filter.RepairCost != null && filter.RepairCost.HasValue)
                query = query.Where(q => q.RepairCost.HasValue).Where(q => q.RepairCost, filter.RepairCost);
            if (filter.PaymentStatusId != null && filter.PaymentStatusId.HasValue)
                query = query.Where(q => q.PaymentStatusId.HasValue).Where(q => q.PaymentStatusId, filter.PaymentStatusId);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<RepairTicketDAO> OrFilter(IQueryable<RepairTicketDAO> query, RepairTicketFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<RepairTicketDAO> initQuery = query.Where(q => false);
            foreach (RepairTicketFilter RepairTicketFilter in filter.OrFilter)
            {
                IQueryable<RepairTicketDAO> queryable = query;
                if (RepairTicketFilter.Id != null && RepairTicketFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, RepairTicketFilter.Id);
                if (RepairTicketFilter.Code != null && RepairTicketFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, RepairTicketFilter.Code);
                if (RepairTicketFilter.DeviceSerial != null && RepairTicketFilter.DeviceSerial.HasValue)
                    queryable = queryable.Where(q => q.DeviceSerial, RepairTicketFilter.DeviceSerial);
                if (RepairTicketFilter.OrderId != null && RepairTicketFilter.OrderId.HasValue)
                    queryable = queryable.Where(q => q.OrderId, RepairTicketFilter.OrderId);
                if (RepairTicketFilter.OrderCategoryId != null && RepairTicketFilter.OrderCategoryId.HasValue)
                    queryable = queryable.Where(q => q.OrderCategoryId, RepairTicketFilter.OrderCategoryId);
                if (RepairTicketFilter.RepairDueDate != null && RepairTicketFilter.RepairDueDate.HasValue)
                    queryable = queryable.Where(q => q.RepairDueDate.HasValue).Where(q => q.RepairDueDate, RepairTicketFilter.RepairDueDate);
                if (RepairTicketFilter.ItemId != null && RepairTicketFilter.ItemId.HasValue)
                    queryable = queryable.Where(q => q.ItemId.HasValue).Where(q => q.ItemId, RepairTicketFilter.ItemId);
                if (RepairTicketFilter.RejectReason != null && RepairTicketFilter.RejectReason.HasValue)
                    queryable = queryable.Where(q => q.RejectReason, RepairTicketFilter.RejectReason);
                if (RepairTicketFilter.DeviceState != null && RepairTicketFilter.DeviceState.HasValue)
                    queryable = queryable.Where(q => q.DeviceState, RepairTicketFilter.DeviceState);
                if (RepairTicketFilter.RepairStatusId != null && RepairTicketFilter.RepairStatusId.HasValue)
                    queryable = queryable.Where(q => q.RepairStatusId.HasValue).Where(q => q.RepairStatusId, RepairTicketFilter.RepairStatusId);
                if (RepairTicketFilter.RepairAddess != null && RepairTicketFilter.RepairAddess.HasValue)
                    queryable = queryable.Where(q => q.RepairAddess, RepairTicketFilter.RepairAddess);
                if (RepairTicketFilter.ReceiveUser != null && RepairTicketFilter.ReceiveUser.HasValue)
                    queryable = queryable.Where(q => q.ReceiveUser, RepairTicketFilter.ReceiveUser);
                if (RepairTicketFilter.ReceiveDate != null && RepairTicketFilter.ReceiveDate.HasValue)
                    queryable = queryable.Where(q => q.ReceiveDate.HasValue).Where(q => q.ReceiveDate, RepairTicketFilter.ReceiveDate);
                if (RepairTicketFilter.RepairDate != null && RepairTicketFilter.RepairDate.HasValue)
                    queryable = queryable.Where(q => q.RepairDate.HasValue).Where(q => q.RepairDate, RepairTicketFilter.RepairDate);
                if (RepairTicketFilter.ReturnDate != null && RepairTicketFilter.ReturnDate.HasValue)
                    queryable = queryable.Where(q => q.ReturnDate.HasValue).Where(q => q.ReturnDate, RepairTicketFilter.ReturnDate);
                if (RepairTicketFilter.RepairSolution != null && RepairTicketFilter.RepairSolution.HasValue)
                    queryable = queryable.Where(q => q.RepairSolution, RepairTicketFilter.RepairSolution);
                if (RepairTicketFilter.Note != null && RepairTicketFilter.Note.HasValue)
                    queryable = queryable.Where(q => q.Note, RepairTicketFilter.Note);
                if (RepairTicketFilter.RepairCost != null && RepairTicketFilter.RepairCost.HasValue)
                    queryable = queryable.Where(q => q.RepairCost.HasValue).Where(q => q.RepairCost, RepairTicketFilter.RepairCost);
                if (RepairTicketFilter.PaymentStatusId != null && RepairTicketFilter.PaymentStatusId.HasValue)
                    queryable = queryable.Where(q => q.PaymentStatusId.HasValue).Where(q => q.PaymentStatusId, RepairTicketFilter.PaymentStatusId);
                if (RepairTicketFilter.CustomerId != null && RepairTicketFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, RepairTicketFilter.CustomerId);
                if (RepairTicketFilter.CreatorId != null && RepairTicketFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, RepairTicketFilter.CreatorId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<RepairTicketDAO> DynamicOrder(IQueryable<RepairTicketDAO> query, RepairTicketFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case RepairTicketOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case RepairTicketOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case RepairTicketOrder.DeviceSerial:
                            query = query.OrderBy(q => q.DeviceSerial);
                            break;
                        case RepairTicketOrder.Order:
                            query = query.OrderBy(q => q.OrderId);
                            break;
                        case RepairTicketOrder.OrderCategory:
                            query = query.OrderBy(q => q.OrderCategoryId);
                            break;
                        case RepairTicketOrder.RepairDueDate:
                            query = query.OrderBy(q => q.RepairDueDate);
                            break;
                        case RepairTicketOrder.Item:
                            query = query.OrderBy(q => q.ItemId);
                            break;
                        case RepairTicketOrder.IsRejectRepair:
                            query = query.OrderBy(q => q.IsRejectRepair);
                            break;
                        case RepairTicketOrder.RejectReason:
                            query = query.OrderBy(q => q.RejectReason);
                            break;
                        case RepairTicketOrder.DeviceState:
                            query = query.OrderBy(q => q.DeviceState);
                            break;
                        case RepairTicketOrder.RepairStatus:
                            query = query.OrderBy(q => q.RepairStatusId);
                            break;
                        case RepairTicketOrder.RepairAddess:
                            query = query.OrderBy(q => q.RepairAddess);
                            break;
                        case RepairTicketOrder.ReceiveUser:
                            query = query.OrderBy(q => q.ReceiveUser);
                            break;
                        case RepairTicketOrder.ReceiveDate:
                            query = query.OrderBy(q => q.ReceiveDate);
                            break;
                        case RepairTicketOrder.RepairDate:
                            query = query.OrderBy(q => q.RepairDate);
                            break;
                        case RepairTicketOrder.ReturnDate:
                            query = query.OrderBy(q => q.ReturnDate);
                            break;
                        case RepairTicketOrder.RepairSolution:
                            query = query.OrderBy(q => q.RepairSolution);
                            break;
                        case RepairTicketOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        case RepairTicketOrder.RepairCost:
                            query = query.OrderBy(q => q.RepairCost);
                            break;
                        case RepairTicketOrder.PaymentStatus:
                            query = query.OrderBy(q => q.PaymentStatusId);
                            break;
                        case RepairTicketOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case RepairTicketOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case RepairTicketOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case RepairTicketOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case RepairTicketOrder.DeviceSerial:
                            query = query.OrderByDescending(q => q.DeviceSerial);
                            break;
                        case RepairTicketOrder.Order:
                            query = query.OrderByDescending(q => q.OrderId);
                            break;
                        case RepairTicketOrder.OrderCategory:
                            query = query.OrderByDescending(q => q.OrderCategoryId);
                            break;
                        case RepairTicketOrder.RepairDueDate:
                            query = query.OrderByDescending(q => q.RepairDueDate);
                            break;
                        case RepairTicketOrder.Item:
                            query = query.OrderByDescending(q => q.ItemId);
                            break;
                        case RepairTicketOrder.IsRejectRepair:
                            query = query.OrderByDescending(q => q.IsRejectRepair);
                            break;
                        case RepairTicketOrder.RejectReason:
                            query = query.OrderByDescending(q => q.RejectReason);
                            break;
                        case RepairTicketOrder.DeviceState:
                            query = query.OrderByDescending(q => q.DeviceState);
                            break;
                        case RepairTicketOrder.RepairStatus:
                            query = query.OrderByDescending(q => q.RepairStatusId);
                            break;
                        case RepairTicketOrder.RepairAddess:
                            query = query.OrderByDescending(q => q.RepairAddess);
                            break;
                        case RepairTicketOrder.ReceiveUser:
                            query = query.OrderByDescending(q => q.ReceiveUser);
                            break;
                        case RepairTicketOrder.ReceiveDate:
                            query = query.OrderByDescending(q => q.ReceiveDate);
                            break;
                        case RepairTicketOrder.RepairDate:
                            query = query.OrderByDescending(q => q.RepairDate);
                            break;
                        case RepairTicketOrder.ReturnDate:
                            query = query.OrderByDescending(q => q.ReturnDate);
                            break;
                        case RepairTicketOrder.RepairSolution:
                            query = query.OrderByDescending(q => q.RepairSolution);
                            break;
                        case RepairTicketOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
                            break;
                        case RepairTicketOrder.RepairCost:
                            query = query.OrderByDescending(q => q.RepairCost);
                            break;
                        case RepairTicketOrder.PaymentStatus:
                            query = query.OrderByDescending(q => q.PaymentStatusId);
                            break;
                        case RepairTicketOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case RepairTicketOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<RepairTicket>> DynamicSelect(IQueryable<RepairTicketDAO> query, RepairTicketFilter filter)
        {
            List<RepairTicket> RepairTickets = await query.Select(q => new RepairTicket()
            {
                Id = filter.Selects.Contains(RepairTicketSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(RepairTicketSelect.Code) ? q.Code : default(string),
                DeviceSerial = filter.Selects.Contains(RepairTicketSelect.DeviceSerial) ? q.DeviceSerial : default(string),
                OrderId = filter.Selects.Contains(RepairTicketSelect.Order) ? q.OrderId : default(long),
                OrderCategoryId = filter.Selects.Contains(RepairTicketSelect.OrderCategory) ? q.OrderCategoryId : default(long),
                RepairDueDate = filter.Selects.Contains(RepairTicketSelect.RepairDueDate) ? q.RepairDueDate : default(DateTime?),
                ItemId = filter.Selects.Contains(RepairTicketSelect.Item) ? q.ItemId : default(long?),
                IsRejectRepair = filter.Selects.Contains(RepairTicketSelect.IsRejectRepair) ? q.IsRejectRepair : default(bool?),
                RejectReason = filter.Selects.Contains(RepairTicketSelect.RejectReason) ? q.RejectReason : default(string),
                DeviceState = filter.Selects.Contains(RepairTicketSelect.DeviceState) ? q.DeviceState : default(string),
                RepairStatusId = filter.Selects.Contains(RepairTicketSelect.RepairStatus) ? q.RepairStatusId : default(long?),
                RepairAddess = filter.Selects.Contains(RepairTicketSelect.RepairAddess) ? q.RepairAddess : default(string),
                ReceiveUser = filter.Selects.Contains(RepairTicketSelect.ReceiveUser) ? q.ReceiveUser : default(string),
                ReceiveDate = filter.Selects.Contains(RepairTicketSelect.ReceiveDate) ? q.ReceiveDate : default(DateTime?),
                RepairDate = filter.Selects.Contains(RepairTicketSelect.RepairDate) ? q.RepairDate : default(DateTime?),
                ReturnDate = filter.Selects.Contains(RepairTicketSelect.ReturnDate) ? q.ReturnDate : default(DateTime?),
                RepairSolution = filter.Selects.Contains(RepairTicketSelect.RepairSolution) ? q.RepairSolution : default(string),
                Note = filter.Selects.Contains(RepairTicketSelect.Note) ? q.Note : default(string),
                RepairCost = filter.Selects.Contains(RepairTicketSelect.RepairCost) ? q.RepairCost : default(decimal?),
                PaymentStatusId = filter.Selects.Contains(RepairTicketSelect.PaymentStatus) ? q.PaymentStatusId : default(long?),
                CustomerId = filter.Selects.Contains(RepairTicketSelect.Customer) ? q.CustomerId : default(long),
                CreatorId = filter.Selects.Contains(RepairTicketSelect.Creator) ? q.CreatorId : default(long),
                Creator = filter.Selects.Contains(RepairTicketSelect.Creator) && q.Creator != null ? new AppUser
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
                Customer = filter.Selects.Contains(RepairTicketSelect.Customer) && q.Customer != null ? new Customer
                {
                    Id = q.Customer.Id,
                    Code = q.Customer.Code,
                    Name = q.Customer.Name,
                    Phone = q.Customer.Phone,
                    Email = q.Customer.Email,
                    Address = q.Customer.Address,
                    StatusId = q.Customer.StatusId,
                    NationId = q.Customer.NationId,
                    ProvinceId = q.Customer.ProvinceId,
                    DistrictId = q.Customer.DistrictId,
                    WardId = q.Customer.WardId,
                    ProfessionId = q.Customer.ProfessionId,
                    Used = q.Customer.Used,
                } : null,
                Item = filter.Selects.Contains(RepairTicketSelect.Item) && q.Item != null ? new Item
                {
                    Id = q.Item.Id,
                    ProductId = q.Item.ProductId,
                    Code = q.Item.Code,
                    Name = q.Item.Name,
                    ScanCode = q.Item.ScanCode,
                    SalePrice = q.Item.SalePrice,
                    RetailPrice = q.Item.RetailPrice,
                    StatusId = q.Item.StatusId,
                    Used = q.Item.Used,
                    RowId = q.Item.RowId,
                } : null,
                OrderCategory = filter.Selects.Contains(RepairTicketSelect.OrderCategory) && q.OrderCategory != null ? new OrderCategory
                {
                    Id = q.OrderCategory.Id,
                    Code = q.OrderCategory.Code,
                    Name = q.OrderCategory.Name,
                } : null,
                PaymentStatus = filter.Selects.Contains(RepairTicketSelect.PaymentStatus) && q.PaymentStatus != null ? new PaymentStatus
                {
                    Id = q.PaymentStatus.Id,
                    Code = q.PaymentStatus.Code,
                    Name = q.PaymentStatus.Name,
                } : null,
                RepairStatus = filter.Selects.Contains(RepairTicketSelect.RepairStatus) && q.RepairStatus != null ? new RepairStatus
                {
                    Id = q.RepairStatus.Id,
                    Name = q.RepairStatus.Name,
                    Code = q.RepairStatus.Code,
                } : null,
            }).ToListAsync();
            return RepairTickets;
        }

        public async Task<int> Count(RepairTicketFilter filter)
        {
            IQueryable<RepairTicketDAO> RepairTickets = DataContext.RepairTicket.AsNoTracking();
            RepairTickets = DynamicFilter(RepairTickets, filter);
            return await RepairTickets.CountAsync();
        }

        public async Task<List<RepairTicket>> List(RepairTicketFilter filter)
        {
            if (filter == null) return new List<RepairTicket>();
            IQueryable<RepairTicketDAO> RepairTicketDAOs = DataContext.RepairTicket.AsNoTracking();
            RepairTicketDAOs = DynamicFilter(RepairTicketDAOs, filter);
            RepairTicketDAOs = DynamicOrder(RepairTicketDAOs, filter);
            List<RepairTicket> RepairTickets = await DynamicSelect(RepairTicketDAOs, filter);
            return RepairTickets;
        }

        public async Task<List<RepairTicket>> List(List<long> Ids)
        {
            List<RepairTicket> RepairTickets = await DataContext.RepairTicket.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new RepairTicket()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                DeviceSerial = x.DeviceSerial,
                OrderId = x.OrderId,
                OrderCategoryId = x.OrderCategoryId,
                RepairDueDate = x.RepairDueDate,
                ItemId = x.ItemId,
                IsRejectRepair = x.IsRejectRepair,
                RejectReason = x.RejectReason,
                DeviceState = x.DeviceState,
                RepairStatusId = x.RepairStatusId,
                RepairAddess = x.RepairAddess,
                ReceiveUser = x.ReceiveUser,
                ReceiveDate = x.ReceiveDate,
                RepairDate = x.RepairDate,
                ReturnDate = x.ReturnDate,
                RepairSolution = x.RepairSolution,
                Note = x.Note,
                RepairCost = x.RepairCost,
                PaymentStatusId = x.PaymentStatusId,
                CustomerId = x.CustomerId,
                CreatorId = x.CreatorId,
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
                    Email = x.Customer.Email,
                    Address = x.Customer.Address,
                    StatusId = x.Customer.StatusId,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    ProfessionId = x.Customer.ProfessionId,
                    Used = x.Customer.Used,
                },
                Item = x.Item == null ? null : new Item
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
                OrderCategory = x.OrderCategory == null ? null : new OrderCategory
                {
                    Id = x.OrderCategory.Id,
                    Code = x.OrderCategory.Code,
                    Name = x.OrderCategory.Name,
                },
                PaymentStatus = x.PaymentStatus == null ? null : new PaymentStatus
                {
                    Id = x.PaymentStatus.Id,
                    Code = x.PaymentStatus.Code,
                    Name = x.PaymentStatus.Name,
                },
                RepairStatus = x.RepairStatus == null ? null : new RepairStatus
                {
                    Id = x.RepairStatus.Id,
                    Name = x.RepairStatus.Name,
                    Code = x.RepairStatus.Code,
                },
            }).ToListAsync();
            

            return RepairTickets;
        }

        public async Task<RepairTicket> Get(long Id)
        {
            RepairTicket RepairTicket = await DataContext.RepairTicket.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new RepairTicket()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                DeviceSerial = x.DeviceSerial,
                OrderId = x.OrderId,
                OrderCategoryId = x.OrderCategoryId,
                RepairDueDate = x.RepairDueDate,
                ItemId = x.ItemId,
                IsRejectRepair = x.IsRejectRepair,
                RejectReason = x.RejectReason,
                DeviceState = x.DeviceState,
                RepairStatusId = x.RepairStatusId,
                RepairAddess = x.RepairAddess,
                ReceiveUser = x.ReceiveUser,
                ReceiveDate = x.ReceiveDate,
                RepairDate = x.RepairDate,
                ReturnDate = x.ReturnDate,
                RepairSolution = x.RepairSolution,
                Note = x.Note,
                RepairCost = x.RepairCost,
                PaymentStatusId = x.PaymentStatusId,
                CustomerId = x.CustomerId,
                CreatorId = x.CreatorId,
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
                    Email = x.Customer.Email,
                    Address = x.Customer.Address,
                    StatusId = x.Customer.StatusId,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    ProfessionId = x.Customer.ProfessionId,
                    Used = x.Customer.Used,
                },
                Item = x.Item == null ? null : new Item
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
                OrderCategory = x.OrderCategory == null ? null : new OrderCategory
                {
                    Id = x.OrderCategory.Id,
                    Code = x.OrderCategory.Code,
                    Name = x.OrderCategory.Name,
                },
                PaymentStatus = x.PaymentStatus == null ? null : new PaymentStatus
                {
                    Id = x.PaymentStatus.Id,
                    Code = x.PaymentStatus.Code,
                    Name = x.PaymentStatus.Name,
                },
                RepairStatus = x.RepairStatus == null ? null : new RepairStatus
                {
                    Id = x.RepairStatus.Id,
                    Name = x.RepairStatus.Name,
                    Code = x.RepairStatus.Code,
                },
            }).FirstOrDefaultAsync();

            if (RepairTicket == null)
                return null;

            return RepairTicket;
        }
        public async Task<bool> Create(RepairTicket RepairTicket)
        {
            RepairTicketDAO RepairTicketDAO = new RepairTicketDAO();
            RepairTicketDAO.Id = RepairTicket.Id;
            RepairTicketDAO.Code = RepairTicket.Code;
            RepairTicketDAO.DeviceSerial = RepairTicket.DeviceSerial;
            RepairTicketDAO.OrderId = RepairTicket.OrderId;
            RepairTicketDAO.OrderCategoryId = RepairTicket.OrderCategoryId;
            RepairTicketDAO.RepairDueDate = RepairTicket.RepairDueDate;
            RepairTicketDAO.ItemId = RepairTicket.ItemId;
            RepairTicketDAO.IsRejectRepair = RepairTicket.IsRejectRepair;
            RepairTicketDAO.RejectReason = RepairTicket.RejectReason;
            RepairTicketDAO.DeviceState = RepairTicket.DeviceState;
            RepairTicketDAO.RepairStatusId = RepairTicket.RepairStatusId;
            RepairTicketDAO.RepairAddess = RepairTicket.RepairAddess;
            RepairTicketDAO.ReceiveUser = RepairTicket.ReceiveUser;
            RepairTicketDAO.ReceiveDate = RepairTicket.ReceiveDate;
            RepairTicketDAO.RepairDate = RepairTicket.RepairDate;
            RepairTicketDAO.ReturnDate = RepairTicket.ReturnDate;
            RepairTicketDAO.RepairSolution = RepairTicket.RepairSolution;
            RepairTicketDAO.Note = RepairTicket.Note;
            RepairTicketDAO.RepairCost = RepairTicket.RepairCost;
            RepairTicketDAO.PaymentStatusId = RepairTicket.PaymentStatusId;
            RepairTicketDAO.CustomerId = RepairTicket.CustomerId;
            RepairTicketDAO.CreatorId = RepairTicket.CreatorId;
            RepairTicketDAO.CreatedAt = StaticParams.DateTimeNow;
            RepairTicketDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.RepairTicket.Add(RepairTicketDAO);
            await DataContext.SaveChangesAsync();
            RepairTicket.Id = RepairTicketDAO.Id;
            await SaveReference(RepairTicket);
            return true;
        }

        public async Task<bool> Update(RepairTicket RepairTicket)
        {
            RepairTicketDAO RepairTicketDAO = DataContext.RepairTicket.Where(x => x.Id == RepairTicket.Id).FirstOrDefault();
            if (RepairTicketDAO == null)
                return false;
            RepairTicketDAO.Id = RepairTicket.Id;
            RepairTicketDAO.Code = RepairTicket.Code;
            RepairTicketDAO.DeviceSerial = RepairTicket.DeviceSerial;
            RepairTicketDAO.OrderId = RepairTicket.OrderId;
            RepairTicketDAO.OrderCategoryId = RepairTicket.OrderCategoryId;
            RepairTicketDAO.RepairDueDate = RepairTicket.RepairDueDate;
            RepairTicketDAO.ItemId = RepairTicket.ItemId;
            RepairTicketDAO.IsRejectRepair = RepairTicket.IsRejectRepair;
            RepairTicketDAO.RejectReason = RepairTicket.RejectReason;
            RepairTicketDAO.DeviceState = RepairTicket.DeviceState;
            RepairTicketDAO.RepairStatusId = RepairTicket.RepairStatusId;
            RepairTicketDAO.RepairAddess = RepairTicket.RepairAddess;
            RepairTicketDAO.ReceiveUser = RepairTicket.ReceiveUser;
            RepairTicketDAO.ReceiveDate = RepairTicket.ReceiveDate;
            RepairTicketDAO.RepairDate = RepairTicket.RepairDate;
            RepairTicketDAO.ReturnDate = RepairTicket.ReturnDate;
            RepairTicketDAO.RepairSolution = RepairTicket.RepairSolution;
            RepairTicketDAO.Note = RepairTicket.Note;
            RepairTicketDAO.RepairCost = RepairTicket.RepairCost;
            RepairTicketDAO.PaymentStatusId = RepairTicket.PaymentStatusId;
            RepairTicketDAO.CustomerId = RepairTicket.CustomerId;
            RepairTicketDAO.CreatorId = RepairTicket.CreatorId;
            RepairTicketDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(RepairTicket);
            return true;
        }

        public async Task<bool> Delete(RepairTicket RepairTicket)
        {
            await DataContext.RepairTicket.Where(x => x.Id == RepairTicket.Id).UpdateFromQueryAsync(x => new RepairTicketDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<RepairTicket> RepairTickets)
        {
            List<RepairTicketDAO> RepairTicketDAOs = new List<RepairTicketDAO>();
            foreach (RepairTicket RepairTicket in RepairTickets)
            {
                RepairTicketDAO RepairTicketDAO = new RepairTicketDAO();
                RepairTicketDAO.Id = RepairTicket.Id;
                RepairTicketDAO.Code = RepairTicket.Code;
                RepairTicketDAO.DeviceSerial = RepairTicket.DeviceSerial;
                RepairTicketDAO.OrderId = RepairTicket.OrderId;
                RepairTicketDAO.OrderCategoryId = RepairTicket.OrderCategoryId;
                RepairTicketDAO.RepairDueDate = RepairTicket.RepairDueDate;
                RepairTicketDAO.ItemId = RepairTicket.ItemId;
                RepairTicketDAO.IsRejectRepair = RepairTicket.IsRejectRepair;
                RepairTicketDAO.RejectReason = RepairTicket.RejectReason;
                RepairTicketDAO.DeviceState = RepairTicket.DeviceState;
                RepairTicketDAO.RepairStatusId = RepairTicket.RepairStatusId;
                RepairTicketDAO.RepairAddess = RepairTicket.RepairAddess;
                RepairTicketDAO.ReceiveUser = RepairTicket.ReceiveUser;
                RepairTicketDAO.ReceiveDate = RepairTicket.ReceiveDate;
                RepairTicketDAO.RepairDate = RepairTicket.RepairDate;
                RepairTicketDAO.ReturnDate = RepairTicket.ReturnDate;
                RepairTicketDAO.RepairSolution = RepairTicket.RepairSolution;
                RepairTicketDAO.Note = RepairTicket.Note;
                RepairTicketDAO.RepairCost = RepairTicket.RepairCost;
                RepairTicketDAO.PaymentStatusId = RepairTicket.PaymentStatusId;
                RepairTicketDAO.CustomerId = RepairTicket.CustomerId;
                RepairTicketDAO.CreatorId = RepairTicket.CreatorId;
                RepairTicketDAO.CreatedAt = StaticParams.DateTimeNow;
                RepairTicketDAO.UpdatedAt = StaticParams.DateTimeNow;
                RepairTicketDAOs.Add(RepairTicketDAO);
            }
            await DataContext.BulkMergeAsync(RepairTicketDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<RepairTicket> RepairTickets)
        {
            List<long> Ids = RepairTickets.Select(x => x.Id).ToList();
            await DataContext.RepairTicket
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new RepairTicketDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(RepairTicket RepairTicket)
        {
        }
        
    }
}
