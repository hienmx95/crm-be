using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;
using CRM.Enums;

namespace CRM.Services.MRepairTicket
{
    public interface IRepairTicketService :  IServiceScoped
    {
        Task<int> Count(RepairTicketFilter RepairTicketFilter);
        Task<List<RepairTicket>> List(RepairTicketFilter RepairTicketFilter);
        Task<List<Item>> ListItem(ItemFilter ItemFilter, IdFilter OrderCategoryId, IdFilter OrderId);
        Task<RepairTicket> Get(long Id);
        Task<RepairTicket> Create(RepairTicket RepairTicket);
        Task<RepairTicket> Update(RepairTicket RepairTicket);
        Task<RepairTicket> Delete(RepairTicket RepairTicket);
        Task<List<RepairTicket>> BulkDelete(List<RepairTicket> RepairTickets);
        Task<List<RepairTicket>> Import(List<RepairTicket> RepairTickets);
        Task<RepairTicketFilter> ToFilter(RepairTicketFilter RepairTicketFilter);
    }

    public class RepairTicketService : BaseService, IRepairTicketService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IRepairTicketValidator RepairTicketValidator;

        public RepairTicketService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IRepairTicketValidator RepairTicketValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.RepairTicketValidator = RepairTicketValidator;
        }
        public async Task<int> Count(RepairTicketFilter RepairTicketFilter)
        {
            try
            {
                int result = await UOW.RepairTicketRepository.Count(RepairTicketFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return 0;
        }

        public async Task<List<RepairTicket>> List(RepairTicketFilter RepairTicketFilter)
        {
            try
            {
                List<RepairTicket> RepairTickets = await UOW.RepairTicketRepository.List(RepairTicketFilter);

                var RetailIds = RepairTickets.Where(x => x.OrderCategoryId == OrderCategoryEnum.ORDER_CUSTOMER.Id).Select(x => x.OrderId).ToList();
                var DirectIds = RepairTickets.Where(x => x.OrderCategoryId == OrderCategoryEnum.ORDER_DIRECT.Id).Select(x => x.OrderId).ToList();

                List<CustomerSalesOrder> CustomerSalesOrders = await UOW.CustomerSalesOrderRepository.List(new CustomerSalesOrderFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Selects = CustomerSalesOrderSelect.Id | CustomerSalesOrderSelect.Code,
                    Id = new IdFilter { In = RetailIds }
                });

                List<DirectSalesOrder> DirectSalesOrders = await UOW.DirectSalesOrderRepository.List(new DirectSalesOrderFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Selects = DirectSalesOrderSelect.Id | DirectSalesOrderSelect.Code,
                    Id = new IdFilter { In = DirectIds }
                });


                foreach (var RepairTicket in RepairTickets)
                {
                    if (RepairTicket.OrderCategoryId == OrderCategoryEnum.ORDER_CUSTOMER.Id)
                    {
                        RepairTicket.CustomerSalesOrder = CustomerSalesOrders.Where(x => x.Id == RepairTicket.OrderId).FirstOrDefault();
                    }
                    if (RepairTicket.OrderCategoryId == OrderCategoryEnum.ORDER_DIRECT.Id)
                    {
                        RepairTicket.DirectSalesOrder = DirectSalesOrders.Where(x => x.Id == RepairTicket.OrderId).FirstOrDefault();
                    }
                }

                return RepairTickets;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return null;
        }

        public async Task<List<Item>> ListItem(ItemFilter ItemFilter, IdFilter OrderCategoryId, IdFilter OrderId)
        {
            if(OrderCategoryId == null || OrderCategoryId.Equal.HasValue == false)
                return new List<Item>();
            if (OrderId.HasValue == false || OrderId.Equal.HasValue == false)
                return new List<Item>();
            
            if (OrderCategoryId.Equal == OrderCategoryEnum.ORDER_CUSTOMER.Id)
            {
                var CustomerSalesOrder = await UOW.CustomerSalesOrderRepository.Get(OrderId.Equal.Value);
                if(CustomerSalesOrder == null || (CustomerSalesOrder.CustomerSalesOrderContents == null && CustomerSalesOrder.CustomerSalesOrderPromotions == null))
                    return new List<Item>();
                var ItemIds = new List<long>();
                var Ids1 = CustomerSalesOrder.CustomerSalesOrderContents?.Select(x => x.ItemId).ToList();
                var Ids2 = CustomerSalesOrder.CustomerSalesOrderPromotions?.Select(x => x.ItemId).ToList();
                ItemIds.AddRange(Ids1);
                ItemIds.AddRange(Ids2);
                ItemIds = ItemIds.Distinct().ToList();

                ItemFilter.Id = new IdFilter { In = ItemIds };
                return await UOW.ItemRepository.List(ItemFilter);
            }
            if (OrderCategoryId.Equal == OrderCategoryEnum.ORDER_DIRECT.Id)
            {
                var DirectSalesOrder = await UOW.DirectSalesOrderRepository.Get(OrderId.Equal.Value);
                if (DirectSalesOrder == null || (DirectSalesOrder.DirectSalesOrderContents == null && DirectSalesOrder.DirectSalesOrderPromotions == null))
                    return new List<Item>();
                var ItemIds = new List<long>();
                var Ids1 = DirectSalesOrder.DirectSalesOrderContents?.Select(x => x.ItemId).ToList();
                var Ids2 = DirectSalesOrder.DirectSalesOrderPromotions?.Select(x => x.ItemId).ToList();
                ItemIds.AddRange(Ids1);
                ItemIds.AddRange(Ids2);
                ItemIds = ItemIds.Distinct().ToList();

                ItemFilter.Id = new IdFilter { In = ItemIds };
                return await UOW.ItemRepository.List(ItemFilter);
            }
            return new List<Item>();
        }

        public async Task<RepairTicket> Get(long Id)
        {
            RepairTicket RepairTicket = await UOW.RepairTicketRepository.Get(Id);
            if (RepairTicket == null)
                return null;

            if (RepairTicket.OrderCategoryId == OrderCategoryEnum.ORDER_CUSTOMER.Id)
            {
                RepairTicket.CustomerSalesOrder = await UOW.CustomerSalesOrderRepository.Get(RepairTicket.OrderId);
            }
            if (RepairTicket.OrderCategoryId == OrderCategoryEnum.ORDER_DIRECT.Id)
            {
                RepairTicket.DirectSalesOrder = await UOW.DirectSalesOrderRepository.Get(RepairTicket.OrderId);
            }
            return RepairTicket;
        }
        public async Task<RepairTicket> Create(RepairTicket RepairTicket)
        {
            if (!await RepairTicketValidator.Create(RepairTicket))
                return RepairTicket;

            try
            {
                RepairTicket.CreatorId = CurrentContext.UserId;
                await UOW.RepairTicketRepository.Create(RepairTicket);
                RepairTicket = await UOW.RepairTicketRepository.Get(RepairTicket.Id);
                await Logging.CreateAuditLog(RepairTicket, new { }, nameof(RepairTicketService));
                return RepairTicket;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return null;
        }

        public async Task<RepairTicket> Update(RepairTicket RepairTicket)
        {
            if (!await RepairTicketValidator.Update(RepairTicket))
                return RepairTicket;
            try
            {
                var oldData = await UOW.RepairTicketRepository.Get(RepairTicket.Id);

                await UOW.RepairTicketRepository.Update(RepairTicket);

                RepairTicket = await UOW.RepairTicketRepository.Get(RepairTicket.Id);
                await Logging.CreateAuditLog(RepairTicket, oldData, nameof(RepairTicketService));
                return RepairTicket;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return null;
        }

        public async Task<RepairTicket> Delete(RepairTicket RepairTicket)
        {
            if (!await RepairTicketValidator.Delete(RepairTicket))
                return RepairTicket;

            try
            {
                await UOW.RepairTicketRepository.Delete(RepairTicket);
                await Logging.CreateAuditLog(new { }, RepairTicket, nameof(RepairTicketService));
                return RepairTicket;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return null;
        }

        public async Task<List<RepairTicket>> BulkDelete(List<RepairTicket> RepairTickets)
        {
            if (!await RepairTicketValidator.BulkDelete(RepairTickets))
                return RepairTickets;

            try
            {
                await UOW.RepairTicketRepository.BulkDelete(RepairTickets);
                await Logging.CreateAuditLog(new { }, RepairTickets, nameof(RepairTicketService));
                return RepairTickets;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return null;

        }
        
        public async Task<List<RepairTicket>> Import(List<RepairTicket> RepairTickets)
        {
            if (!await RepairTicketValidator.Import(RepairTickets))
                return RepairTickets;
            try
            {
                await UOW.RepairTicketRepository.BulkMerge(RepairTickets);

                await Logging.CreateAuditLog(RepairTickets, new { }, nameof(RepairTicketService));
                return RepairTickets;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RepairTicketService));
            }
            return null;
        }     
        
        public async Task<RepairTicketFilter> ToFilter(RepairTicketFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<RepairTicketFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                RepairTicketFilter subFilter = new RepairTicketFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderId))
                        subFilter.OrderId = FilterBuilder.Merge(subFilter.OrderId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderCategoryId))
                        subFilter.OrderCategoryId = FilterBuilder.Merge(subFilter.OrderCategoryId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RepairDueDate))
                        subFilter.RepairDueDate = FilterBuilder.Merge(subFilter.RepairDueDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ItemId))
                        subFilter.ItemId = FilterBuilder.Merge(subFilter.ItemId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RejectReason))
                        subFilter.RejectReason = FilterBuilder.Merge(subFilter.RejectReason, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeviceState))
                        subFilter.DeviceState = FilterBuilder.Merge(subFilter.DeviceState, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RepairStatusId))
                        subFilter.RepairStatusId = FilterBuilder.Merge(subFilter.RepairStatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RepairAddess))
                        subFilter.RepairAddess = FilterBuilder.Merge(subFilter.RepairAddess, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ReceiveUser))
                        subFilter.ReceiveUser = FilterBuilder.Merge(subFilter.ReceiveUser, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ReceiveDate))
                        subFilter.ReceiveDate = FilterBuilder.Merge(subFilter.ReceiveDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RepairDate))
                        subFilter.RepairDate = FilterBuilder.Merge(subFilter.RepairDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ReturnDate))
                        subFilter.ReturnDate = FilterBuilder.Merge(subFilter.ReturnDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RepairSolution))
                        subFilter.RepairSolution = FilterBuilder.Merge(subFilter.RepairSolution, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Note))
                        subFilter.Note = FilterBuilder.Merge(subFilter.Note, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RepairCost))
                        subFilter.RepairCost = FilterBuilder.Merge(subFilter.RepairCost, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PaymentStatusId))
                        subFilter.PaymentStatusId = FilterBuilder.Merge(subFilter.PaymentStatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CreatorId))
                        subFilter.CreatorId = FilterBuilder.Merge(subFilter.CreatorId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
