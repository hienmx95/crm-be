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

namespace CRM.Services.MCustomerSalesOrderPromotion
{
    public interface ICustomerSalesOrderPromotionService :  IServiceScoped
    {
        Task<int> Count(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter);
        Task<List<CustomerSalesOrderPromotion>> List(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter);
        Task<CustomerSalesOrderPromotion> Get(long Id);
        Task<CustomerSalesOrderPromotion> Create(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<CustomerSalesOrderPromotion> Update(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<CustomerSalesOrderPromotion> Delete(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<List<CustomerSalesOrderPromotion>> BulkDelete(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions);
        Task<List<CustomerSalesOrderPromotion>> Import(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions);
        Task<CustomerSalesOrderPromotionFilter> ToFilter(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter);
    }

    public class CustomerSalesOrderPromotionService : BaseService, ICustomerSalesOrderPromotionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerSalesOrderPromotionValidator CustomerSalesOrderPromotionValidator;

        public CustomerSalesOrderPromotionService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerSalesOrderPromotionValidator CustomerSalesOrderPromotionValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerSalesOrderPromotionValidator = CustomerSalesOrderPromotionValidator;
        }
        public async Task<int> Count(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter)
        {
            try
            {
                int result = await UOW.CustomerSalesOrderPromotionRepository.Count(CustomerSalesOrderPromotionFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return 0;
        }

        public async Task<List<CustomerSalesOrderPromotion>> List(CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter)
        {
            try
            {
                List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions = await UOW.CustomerSalesOrderPromotionRepository.List(CustomerSalesOrderPromotionFilter);
                return CustomerSalesOrderPromotions;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return null;
        }
        
        public async Task<CustomerSalesOrderPromotion> Get(long Id)
        {
            CustomerSalesOrderPromotion CustomerSalesOrderPromotion = await UOW.CustomerSalesOrderPromotionRepository.Get(Id);
            if (CustomerSalesOrderPromotion == null)
                return null;
            return CustomerSalesOrderPromotion;
        }
        public async Task<CustomerSalesOrderPromotion> Create(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            if (!await CustomerSalesOrderPromotionValidator.Create(CustomerSalesOrderPromotion))
                return CustomerSalesOrderPromotion;

            try
            {
                await UOW.CustomerSalesOrderPromotionRepository.Create(CustomerSalesOrderPromotion);
                CustomerSalesOrderPromotion = await UOW.CustomerSalesOrderPromotionRepository.Get(CustomerSalesOrderPromotion.Id);
                await Logging.CreateAuditLog(CustomerSalesOrderPromotion, new { }, nameof(CustomerSalesOrderPromotionService));
                return CustomerSalesOrderPromotion;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return null;
        }

        public async Task<CustomerSalesOrderPromotion> Update(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            if (!await CustomerSalesOrderPromotionValidator.Update(CustomerSalesOrderPromotion))
                return CustomerSalesOrderPromotion;
            try
            {
                var oldData = await UOW.CustomerSalesOrderPromotionRepository.Get(CustomerSalesOrderPromotion.Id);

                await UOW.CustomerSalesOrderPromotionRepository.Update(CustomerSalesOrderPromotion);

                CustomerSalesOrderPromotion = await UOW.CustomerSalesOrderPromotionRepository.Get(CustomerSalesOrderPromotion.Id);
                await Logging.CreateAuditLog(CustomerSalesOrderPromotion, oldData, nameof(CustomerSalesOrderPromotionService));
                return CustomerSalesOrderPromotion;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return null;
        }

        public async Task<CustomerSalesOrderPromotion> Delete(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            if (!await CustomerSalesOrderPromotionValidator.Delete(CustomerSalesOrderPromotion))
                return CustomerSalesOrderPromotion;

            try
            {
                await UOW.CustomerSalesOrderPromotionRepository.Delete(CustomerSalesOrderPromotion);
                await Logging.CreateAuditLog(new { }, CustomerSalesOrderPromotion, nameof(CustomerSalesOrderPromotionService));
                return CustomerSalesOrderPromotion;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return null;
        }

        public async Task<List<CustomerSalesOrderPromotion>> BulkDelete(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions)
        {
            if (!await CustomerSalesOrderPromotionValidator.BulkDelete(CustomerSalesOrderPromotions))
                return CustomerSalesOrderPromotions;

            try
            {
                await UOW.CustomerSalesOrderPromotionRepository.BulkDelete(CustomerSalesOrderPromotions);
                await Logging.CreateAuditLog(new { }, CustomerSalesOrderPromotions, nameof(CustomerSalesOrderPromotionService));
                return CustomerSalesOrderPromotions;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return null;

        }
        
        public async Task<List<CustomerSalesOrderPromotion>> Import(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions)
        {
            if (!await CustomerSalesOrderPromotionValidator.Import(CustomerSalesOrderPromotions))
                return CustomerSalesOrderPromotions;
            try
            {
                await UOW.CustomerSalesOrderPromotionRepository.BulkMerge(CustomerSalesOrderPromotions);

                await Logging.CreateAuditLog(CustomerSalesOrderPromotions, new { }, nameof(CustomerSalesOrderPromotionService));
                return CustomerSalesOrderPromotions;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderPromotionService));
            }
            return null;
        }     
        
        public async Task<CustomerSalesOrderPromotionFilter> ToFilter(CustomerSalesOrderPromotionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerSalesOrderPromotionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerSalesOrderPromotionFilter subFilter = new CustomerSalesOrderPromotionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerSalesOrderId))
                        subFilter.CustomerSalesOrderId = FilterBuilder.Merge(subFilter.CustomerSalesOrderId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ItemId))
                        subFilter.ItemId = FilterBuilder.Merge(subFilter.ItemId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnitOfMeasureId))
                        subFilter.UnitOfMeasureId = FilterBuilder.Merge(subFilter.UnitOfMeasureId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Quantity))
                        subFilter.Quantity = FilterBuilder.Merge(subFilter.Quantity, FilterPermissionDefinition.LongFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RequestedQuantity))
                        subFilter.RequestedQuantity = FilterBuilder.Merge(subFilter.RequestedQuantity, FilterPermissionDefinition.LongFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PrimaryUnitOfMeasureId))
                        subFilter.PrimaryUnitOfMeasureId = FilterBuilder.Merge(subFilter.PrimaryUnitOfMeasureId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Factor))
                        subFilter.Factor = FilterBuilder.Merge(subFilter.Factor, FilterPermissionDefinition.LongFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Note))
                        subFilter.Note = FilterBuilder.Merge(subFilter.Note, FilterPermissionDefinition.StringFilter);
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
