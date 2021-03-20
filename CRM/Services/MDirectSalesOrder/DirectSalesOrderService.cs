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

namespace CRM.Services.MDirectSalesOrder
{
    public interface IDirectSalesOrderService : IServiceScoped
    {
        Task<int> Count(DirectSalesOrderFilter DirectSalesOrderFilter);
        Task<List<DirectSalesOrder>> List(DirectSalesOrderFilter DirectSalesOrderFilter);
        Task<DirectSalesOrder> Get(long Id);
        Task<DirectSalesOrderFilter> ToFilter(DirectSalesOrderFilter DirectSalesOrderFilter);
    }

    public class DirectSalesOrderService : BaseService, IDirectSalesOrderService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IDirectSalesOrderValidator DirectSalesOrderValidator;
        private IOrganizationService OrganizationService;

        public DirectSalesOrderService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IDirectSalesOrderValidator DirectSalesOrderValidator,
            IOrganizationService OrganizationService
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.DirectSalesOrderValidator = DirectSalesOrderValidator;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(DirectSalesOrderFilter DirectSalesOrderFilter)
        {
            try
            {
                int result = await UOW.DirectSalesOrderRepository.Count(DirectSalesOrderFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(DirectSalesOrderService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(DirectSalesOrderService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<DirectSalesOrder>> List(DirectSalesOrderFilter DirectSalesOrderFilter)
        {
            try
            {
                List<DirectSalesOrder> DirectSalesOrders = await UOW.DirectSalesOrderRepository.List(DirectSalesOrderFilter);
                return DirectSalesOrders;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(DirectSalesOrderService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(DirectSalesOrderService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<DirectSalesOrder> Get(long Id)
        {
            DirectSalesOrder DirectSalesOrder = await UOW.DirectSalesOrderRepository.Get(Id);
            if (DirectSalesOrder == null)
                return null;
            return DirectSalesOrder;
        }

        public async Task<DirectSalesOrderFilter> ToFilter(DirectSalesOrderFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<DirectSalesOrderFilter>();
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
                DirectSalesOrderFilter subFilter = new DirectSalesOrderFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                    {
                        var organizationIds = FilterOrganization(Organizations, FilterPermissionDefinition.IdFilter);
                        IdFilter IdFilter = new IdFilter { In = organizationIds };
                        subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, IdFilter);
                    }
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                            if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                            subFilter.UserId.Equal = CurrentContext.UserId;
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                            if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                            subFilter.UserId.NotEqual = CurrentContext.UserId;
                        }
                    }

                }
            }
            return filter;
        }
    }
}
