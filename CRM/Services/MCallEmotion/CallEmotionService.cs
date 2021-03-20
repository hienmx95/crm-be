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

namespace CRM.Services.MCallEmotion
{
    public interface ICallEmotionService :  IServiceScoped
    {
        Task<int> Count(CallEmotionFilter CallEmotionFilter);
        Task<List<CallEmotion>> List(CallEmotionFilter CallEmotionFilter);
        Task<CallEmotion> Get(long Id);
        Task<CallEmotion> Create(CallEmotion CallEmotion);
        Task<CallEmotion> Update(CallEmotion CallEmotion);
        Task<CallEmotion> Delete(CallEmotion CallEmotion);
        Task<List<CallEmotion>> BulkDelete(List<CallEmotion> CallEmotions);
        Task<List<CallEmotion>> Import(List<CallEmotion> CallEmotions);
        Task<CallEmotionFilter> ToFilter(CallEmotionFilter CallEmotionFilter);
    }

    public class CallEmotionService : BaseService, ICallEmotionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICallEmotionValidator CallEmotionValidator;

        public CallEmotionService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICallEmotionValidator CallEmotionValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CallEmotionValidator = CallEmotionValidator;
        }
        public async Task<int> Count(CallEmotionFilter CallEmotionFilter)
        {
            try
            {
                int result = await UOW.CallEmotionRepository.Count(CallEmotionFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return 0;
        }

        public async Task<List<CallEmotion>> List(CallEmotionFilter CallEmotionFilter)
        {
            try
            {
                List<CallEmotion> CallEmotions = await UOW.CallEmotionRepository.List(CallEmotionFilter);
                return CallEmotions;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return null;
        }
        
        public async Task<CallEmotion> Get(long Id)
        {
            CallEmotion CallEmotion = await UOW.CallEmotionRepository.Get(Id);
            if (CallEmotion == null)
                return null;
            return CallEmotion;
        }
        public async Task<CallEmotion> Create(CallEmotion CallEmotion)
        {
            if (!await CallEmotionValidator.Create(CallEmotion))
                return CallEmotion;

            try
            {
                await UOW.CallEmotionRepository.Create(CallEmotion);
                CallEmotion = await UOW.CallEmotionRepository.Get(CallEmotion.Id);
                await Logging.CreateAuditLog(CallEmotion, new { }, nameof(CallEmotionService));
                return CallEmotion;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return null;
        }

        public async Task<CallEmotion> Update(CallEmotion CallEmotion)
        {
            if (!await CallEmotionValidator.Update(CallEmotion))
                return CallEmotion;
            try
            {
                var oldData = await UOW.CallEmotionRepository.Get(CallEmotion.Id);

                await UOW.CallEmotionRepository.Update(CallEmotion);

                CallEmotion = await UOW.CallEmotionRepository.Get(CallEmotion.Id);
                await Logging.CreateAuditLog(CallEmotion, oldData, nameof(CallEmotionService));
                return CallEmotion;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return null;
        }

        public async Task<CallEmotion> Delete(CallEmotion CallEmotion)
        {
            if (!await CallEmotionValidator.Delete(CallEmotion))
                return CallEmotion;

            try
            {
                await UOW.CallEmotionRepository.Delete(CallEmotion);
                await Logging.CreateAuditLog(new { }, CallEmotion, nameof(CallEmotionService));
                return CallEmotion;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return null;
        }

        public async Task<List<CallEmotion>> BulkDelete(List<CallEmotion> CallEmotions)
        {
            if (!await CallEmotionValidator.BulkDelete(CallEmotions))
                return CallEmotions;

            try
            {
                await UOW.CallEmotionRepository.BulkDelete(CallEmotions);
                await Logging.CreateAuditLog(new { }, CallEmotions, nameof(CallEmotionService));
                return CallEmotions;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return null;

        }
        
        public async Task<List<CallEmotion>> Import(List<CallEmotion> CallEmotions)
        {
            if (!await CallEmotionValidator.Import(CallEmotions))
                return CallEmotions;
            try
            {
                await UOW.CallEmotionRepository.BulkMerge(CallEmotions);

                await Logging.CreateAuditLog(CallEmotions, new { }, nameof(CallEmotionService));
                return CallEmotions;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CallEmotionService));
            }
            return null;
        }     
        
        public async Task<CallEmotionFilter> ToFilter(CallEmotionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CallEmotionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CallEmotionFilter subFilter = new CallEmotionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterBuilder.Merge(subFilter.Name, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterBuilder.Merge(subFilter.StatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        subFilter.Description = FilterBuilder.Merge(subFilter.Description, FilterPermissionDefinition.StringFilter);
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
