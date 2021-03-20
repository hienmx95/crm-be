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
using CRM.Services.MOrganization;

namespace CRM.Services.MOrderQuote
{
    public interface IOrderQuoteService :  IServiceScoped
    {
        Task<int> Count(OrderQuoteFilter OrderQuoteFilter);
        Task<List<OrderQuote>> List(OrderQuoteFilter OrderQuoteFilter);
        Task<OrderQuote> Get(long Id);
        Task<OrderQuote> Create(OrderQuote OrderQuote);
        Task<OrderQuote> Update(OrderQuote OrderQuote);
        Task<OrderQuote> Delete(OrderQuote OrderQuote);
        Task<List<OrderQuote>> BulkDelete(List<OrderQuote> OrderQuotes);
        Task<List<OrderQuote>> Import(List<OrderQuote> OrderQuotes);
        Task<OrderQuoteFilter> ToFilter(OrderQuoteFilter OrderQuoteFilter);
    }

    public class OrderQuoteService : BaseService, IOrderQuoteService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IOrderQuoteValidator OrderQuoteValidator;
        private IOrganizationService OrganizationService;

        public OrderQuoteService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            IOrderQuoteValidator OrderQuoteValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OrderQuoteValidator = OrderQuoteValidator;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(OrderQuoteFilter OrderQuoteFilter)
        {
            try
            {
                int result = await UOW.OrderQuoteRepository.Count(OrderQuoteFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<OrderQuote>> List(OrderQuoteFilter OrderQuoteFilter)
        {
            try
            {
                List<OrderQuote> OrderQuotes = await UOW.OrderQuoteRepository.List(OrderQuoteFilter);
                return OrderQuotes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<OrderQuote> Get(long Id)
        {
            OrderQuote OrderQuote = await UOW.OrderQuoteRepository.Get(Id);
            if (OrderQuote == null)
                return null;
            return OrderQuote;
        }
       
        public async Task<OrderQuote> Create(OrderQuote OrderQuote)
        {
            if (!await OrderQuoteValidator.Create(OrderQuote))
                return OrderQuote;

            try
            {
                await UOW.Begin();
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                OrderQuote.CreatorId = Creator.Id;
                OrderQuote.OrganizationId = Creator.OrganizationId;
                OrderQuote.OrderQuoteStatusId = OrderQuoteStatusEnum.NEW.Id;
                await UOW.OrderQuoteRepository.Create(OrderQuote);
                await UOW.OrderQuoteRepository.Update(OrderQuote);
                await UOW.Commit();
                OrderQuote = await UOW.OrderQuoteRepository.Get(OrderQuote.Id);
                await Logging.CreateAuditLog(OrderQuote, new { }, nameof(OrderQuoteService));
                return OrderQuote;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<OrderQuote> Update(OrderQuote OrderQuote)
        {
            if (!await OrderQuoteValidator.Update(OrderQuote))
                return OrderQuote;
            try
            {
                var oldData = await UOW.OrderQuoteRepository.Get(OrderQuote.Id);

                await UOW.Begin();
                await UOW.OrderQuoteRepository.Update(OrderQuote);
                await UOW.Commit();

                OrderQuote = await UOW.OrderQuoteRepository.Get(OrderQuote.Id);
                await Logging.CreateAuditLog(OrderQuote, oldData, nameof(OrderQuoteService));
                return OrderQuote;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<OrderQuote> Delete(OrderQuote OrderQuote)
        {
            if (!await OrderQuoteValidator.Delete(OrderQuote))
                return OrderQuote;

            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteRepository.Delete(OrderQuote);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, OrderQuote, nameof(OrderQuoteService));
                return OrderQuote;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<OrderQuote>> BulkDelete(List<OrderQuote> OrderQuotes)
        {
            if (!await OrderQuoteValidator.BulkDelete(OrderQuotes))
                return OrderQuotes;

            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteRepository.BulkDelete(OrderQuotes);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, OrderQuotes, nameof(OrderQuoteService));
                return OrderQuotes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<OrderQuote>> Import(List<OrderQuote> OrderQuotes)
        {
            if (!await OrderQuoteValidator.Import(OrderQuotes))
                return OrderQuotes;
            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteRepository.BulkMerge(OrderQuotes);
                await UOW.Commit();

                await Logging.CreateAuditLog(OrderQuotes, new { }, nameof(OrderQuoteService));
                return OrderQuotes;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public async Task<OrderQuoteFilter> ToFilter(OrderQuoteFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<OrderQuoteFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });
            foreach (var currentFilter in CurrentContext.Filters)
            {
                OrderQuoteFilter subFilter = new OrderQuoteFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                            if (subFilter.AppUserId == null) subFilter.AppUserId = new IdFilter { };
                            subFilter.AppUserId.Equal = CurrentContext.UserId;
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                            if (subFilter.AppUserId == null) subFilter.AppUserId = new IdFilter { };
                            subFilter.AppUserId.NotEqual = CurrentContext.UserId;
                        }
                    }
                }
            }
            return filter;
        }
    }
}
