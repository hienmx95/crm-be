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

namespace CRM.Services.MOrderQuoteContent
{
    public interface IOrderQuoteContentService :  IServiceScoped
    {
        Task<int> Count(OrderQuoteContentFilter OrderQuoteContentFilter);
        Task<List<OrderQuoteContent>> List(OrderQuoteContentFilter OrderQuoteContentFilter);
        Task<OrderQuoteContent> Get(long Id);
        Task<OrderQuoteContent> Create(OrderQuoteContent OrderQuoteContent);
        Task<OrderQuoteContent> Update(OrderQuoteContent OrderQuoteContent);
        Task<OrderQuoteContent> Delete(OrderQuoteContent OrderQuoteContent);
        Task<List<OrderQuoteContent>> BulkDelete(List<OrderQuoteContent> OrderQuoteContents);
        Task<List<OrderQuoteContent>> Import(List<OrderQuoteContent> OrderQuoteContents);
        OrderQuoteContentFilter ToFilter(OrderQuoteContentFilter OrderQuoteContentFilter);
    }

    public class OrderQuoteContentService : BaseService, IOrderQuoteContentService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IOrderQuoteContentValidator OrderQuoteContentValidator;

        public OrderQuoteContentService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrderQuoteContentValidator OrderQuoteContentValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OrderQuoteContentValidator = OrderQuoteContentValidator;
        }
        public async Task<int> Count(OrderQuoteContentFilter OrderQuoteContentFilter)
        {
            try
            {
                int result = await UOW.OrderQuoteContentRepository.Count(OrderQuoteContentFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<OrderQuoteContent>> List(OrderQuoteContentFilter OrderQuoteContentFilter)
        {
            try
            {
                List<OrderQuoteContent> OrderQuoteContents = await UOW.OrderQuoteContentRepository.List(OrderQuoteContentFilter);
                return OrderQuoteContents;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<OrderQuoteContent> Get(long Id)
        {
            OrderQuoteContent OrderQuoteContent = await UOW.OrderQuoteContentRepository.Get(Id);
            if (OrderQuoteContent == null)
                return null;
            return OrderQuoteContent;
        }
       
        public async Task<OrderQuoteContent> Create(OrderQuoteContent OrderQuoteContent)
        {
            if (!await OrderQuoteContentValidator.Create(OrderQuoteContent))
                return OrderQuoteContent;

            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteContentRepository.Create(OrderQuoteContent);
                await UOW.Commit();
                OrderQuoteContent = await UOW.OrderQuoteContentRepository.Get(OrderQuoteContent.Id);
                await Logging.CreateAuditLog(OrderQuoteContent, new { }, nameof(OrderQuoteContentService));
                return OrderQuoteContent;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<OrderQuoteContent> Update(OrderQuoteContent OrderQuoteContent)
        {
            if (!await OrderQuoteContentValidator.Update(OrderQuoteContent))
                return OrderQuoteContent;
            try
            {
                var oldData = await UOW.OrderQuoteContentRepository.Get(OrderQuoteContent.Id);

                await UOW.Begin();
                await UOW.OrderQuoteContentRepository.Update(OrderQuoteContent);
                await UOW.Commit();

                OrderQuoteContent = await UOW.OrderQuoteContentRepository.Get(OrderQuoteContent.Id);
                await Logging.CreateAuditLog(OrderQuoteContent, oldData, nameof(OrderQuoteContentService));
                return OrderQuoteContent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<OrderQuoteContent> Delete(OrderQuoteContent OrderQuoteContent)
        {
            if (!await OrderQuoteContentValidator.Delete(OrderQuoteContent))
                return OrderQuoteContent;

            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteContentRepository.Delete(OrderQuoteContent);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, OrderQuoteContent, nameof(OrderQuoteContentService));
                return OrderQuoteContent;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<OrderQuoteContent>> BulkDelete(List<OrderQuoteContent> OrderQuoteContents)
        {
            if (!await OrderQuoteContentValidator.BulkDelete(OrderQuoteContents))
                return OrderQuoteContents;

            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteContentRepository.BulkDelete(OrderQuoteContents);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, OrderQuoteContents, nameof(OrderQuoteContentService));
                return OrderQuoteContents;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<OrderQuoteContent>> Import(List<OrderQuoteContent> OrderQuoteContents)
        {
            if (!await OrderQuoteContentValidator.Import(OrderQuoteContents))
                return OrderQuoteContents;
            try
            {
                await UOW.Begin();
                await UOW.OrderQuoteContentRepository.BulkMerge(OrderQuoteContents);
                await UOW.Commit();

                await Logging.CreateAuditLog(OrderQuoteContents, new { }, nameof(OrderQuoteContentService));
                return OrderQuoteContents;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(OrderQuoteContentService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(OrderQuoteContentService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public OrderQuoteContentFilter ToFilter(OrderQuoteContentFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<OrderQuoteContentFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                OrderQuoteContentFilter subFilter = new OrderQuoteContentFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderQuoteId))
                        subFilter.OrderQuoteId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.ItemId))
                        subFilter.ItemId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnitOfMeasureId))
                        subFilter.UnitOfMeasureId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Quantity))
                        
                        subFilter.Quantity = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PrimaryUnitOfMeasureId))
                        subFilter.PrimaryUnitOfMeasureId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.RequestedQuantity))
                        
                        subFilter.RequestedQuantity = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PrimaryPrice))
                        
                        
                        subFilter.PrimaryPrice = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SalePrice))
                        
                        
                        subFilter.SalePrice = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DiscountPercentage))
                        
                        
                        subFilter.DiscountPercentage = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DiscountAmount))
                        
                        
                        subFilter.DiscountAmount = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.GeneralDiscountPercentage))
                        
                        
                        subFilter.GeneralDiscountPercentage = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.GeneralDiscountAmount))
                        
                        
                        subFilter.GeneralDiscountAmount = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TaxPercentage))
                        
                        
                        subFilter.TaxPercentage = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TaxAmount))
                        
                        
                        subFilter.TaxAmount = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Amount))
                        
                        
                        subFilter.Amount = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Factor))
                        
                        subFilter.Factor = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                }
            }
            return filter;
        }
    }
}
