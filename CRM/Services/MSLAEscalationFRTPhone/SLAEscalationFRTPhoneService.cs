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

namespace CRM.Services.MSLAEscalationFRTPhone
{
    public interface ISLAEscalationFRTPhoneService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter);
        Task<List<SLAEscalationFRTPhone>> List(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter);
        Task<SLAEscalationFRTPhone> Get(long Id);
        Task<SLAEscalationFRTPhone> Create(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<SLAEscalationFRTPhone> Update(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<SLAEscalationFRTPhone> Delete(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<List<SLAEscalationFRTPhone>> BulkDelete(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones);
        Task<List<SLAEscalationFRTPhone>> Import(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones);
        SLAEscalationFRTPhoneFilter ToFilter(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter);
    }

    public class SLAEscalationFRTPhoneService : BaseService, ISLAEscalationFRTPhoneService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationFRTPhoneValidator SLAEscalationFRTPhoneValidator;

        public SLAEscalationFRTPhoneService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationFRTPhoneValidator SLAEscalationFRTPhoneValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationFRTPhoneValidator = SLAEscalationFRTPhoneValidator;
        }
        public async Task<int> Count(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationFRTPhoneRepository.Count(SLAEscalationFRTPhoneFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRTPhone>> List(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter)
        {
            try
            {
                List<SLAEscalationFRTPhone> SLAEscalationFRTPhones = await UOW.SLAEscalationFRTPhoneRepository.List(SLAEscalationFRTPhoneFilter);
                return SLAEscalationFRTPhones;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationFRTPhone> Get(long Id)
        {
            SLAEscalationFRTPhone SLAEscalationFRTPhone = await UOW.SLAEscalationFRTPhoneRepository.Get(Id);
            if (SLAEscalationFRTPhone == null)
                return null;
            return SLAEscalationFRTPhone;
        }
       
        public async Task<SLAEscalationFRTPhone> Create(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            if (!await SLAEscalationFRTPhoneValidator.Create(SLAEscalationFRTPhone))
                return SLAEscalationFRTPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTPhoneRepository.Create(SLAEscalationFRTPhone);
                await UOW.Commit();
                SLAEscalationFRTPhone = await UOW.SLAEscalationFRTPhoneRepository.Get(SLAEscalationFRTPhone.Id);
                await Logging.CreateAuditLog(SLAEscalationFRTPhone, new { }, nameof(SLAEscalationFRTPhoneService));
                return SLAEscalationFRTPhone;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRTPhone> Update(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            if (!await SLAEscalationFRTPhoneValidator.Update(SLAEscalationFRTPhone))
                return SLAEscalationFRTPhone;
            try
            {
                var oldData = await UOW.SLAEscalationFRTPhoneRepository.Get(SLAEscalationFRTPhone.Id);

                await UOW.Begin();
                await UOW.SLAEscalationFRTPhoneRepository.Update(SLAEscalationFRTPhone);
                await UOW.Commit();

                SLAEscalationFRTPhone = await UOW.SLAEscalationFRTPhoneRepository.Get(SLAEscalationFRTPhone.Id);
                await Logging.CreateAuditLog(SLAEscalationFRTPhone, oldData, nameof(SLAEscalationFRTPhoneService));
                return SLAEscalationFRTPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRTPhone> Delete(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            if (!await SLAEscalationFRTPhoneValidator.Delete(SLAEscalationFRTPhone))
                return SLAEscalationFRTPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTPhoneRepository.Delete(SLAEscalationFRTPhone);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTPhone, nameof(SLAEscalationFRTPhoneService));
                return SLAEscalationFRTPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRTPhone>> BulkDelete(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones)
        {
            if (!await SLAEscalationFRTPhoneValidator.BulkDelete(SLAEscalationFRTPhones))
                return SLAEscalationFRTPhones;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTPhoneRepository.BulkDelete(SLAEscalationFRTPhones);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTPhones, nameof(SLAEscalationFRTPhoneService));
                return SLAEscalationFRTPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationFRTPhone>> Import(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones)
        {
            if (!await SLAEscalationFRTPhoneValidator.Import(SLAEscalationFRTPhones))
                return SLAEscalationFRTPhones;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTPhoneRepository.BulkMerge(SLAEscalationFRTPhones);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationFRTPhones, new { }, nameof(SLAEscalationFRTPhoneService));
                return SLAEscalationFRTPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationFRTPhoneFilter ToFilter(SLAEscalationFRTPhoneFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationFRTPhoneFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationFRTPhoneFilter subFilter = new SLAEscalationFRTPhoneFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAEscalationFRTId))
                        subFilter.SLAEscalationFRTId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
