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

namespace CRM.Services.MSLAAlertFRTPhone
{
    public interface ISLAAlertFRTPhoneService :  IServiceScoped
    {
        Task<int> Count(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter);
        Task<List<SLAAlertFRTPhone>> List(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter);
        Task<SLAAlertFRTPhone> Get(long Id);
        Task<SLAAlertFRTPhone> Create(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<SLAAlertFRTPhone> Update(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<SLAAlertFRTPhone> Delete(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<List<SLAAlertFRTPhone>> BulkDelete(List<SLAAlertFRTPhone> SLAAlertFRTPhones);
        Task<List<SLAAlertFRTPhone>> Import(List<SLAAlertFRTPhone> SLAAlertFRTPhones);
        SLAAlertFRTPhoneFilter ToFilter(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter);
    }

    public class SLAAlertFRTPhoneService : BaseService, ISLAAlertFRTPhoneService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertFRTPhoneValidator SLAAlertFRTPhoneValidator;

        public SLAAlertFRTPhoneService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertFRTPhoneValidator SLAAlertFRTPhoneValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertFRTPhoneValidator = SLAAlertFRTPhoneValidator;
        }
        public async Task<int> Count(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter)
        {
            try
            {
                int result = await UOW.SLAAlertFRTPhoneRepository.Count(SLAAlertFRTPhoneFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRTPhone>> List(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter)
        {
            try
            {
                List<SLAAlertFRTPhone> SLAAlertFRTPhones = await UOW.SLAAlertFRTPhoneRepository.List(SLAAlertFRTPhoneFilter);
                return SLAAlertFRTPhones;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertFRTPhone> Get(long Id)
        {
            SLAAlertFRTPhone SLAAlertFRTPhone = await UOW.SLAAlertFRTPhoneRepository.Get(Id);
            if (SLAAlertFRTPhone == null)
                return null;
            return SLAAlertFRTPhone;
        }
       
        public async Task<SLAAlertFRTPhone> Create(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            if (!await SLAAlertFRTPhoneValidator.Create(SLAAlertFRTPhone))
                return SLAAlertFRTPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTPhoneRepository.Create(SLAAlertFRTPhone);
                await UOW.Commit();
                SLAAlertFRTPhone = await UOW.SLAAlertFRTPhoneRepository.Get(SLAAlertFRTPhone.Id);
                await Logging.CreateAuditLog(SLAAlertFRTPhone, new { }, nameof(SLAAlertFRTPhoneService));
                return SLAAlertFRTPhone;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRTPhone> Update(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            if (!await SLAAlertFRTPhoneValidator.Update(SLAAlertFRTPhone))
                return SLAAlertFRTPhone;
            try
            {
                var oldData = await UOW.SLAAlertFRTPhoneRepository.Get(SLAAlertFRTPhone.Id);

                await UOW.Begin();
                await UOW.SLAAlertFRTPhoneRepository.Update(SLAAlertFRTPhone);
                await UOW.Commit();

                SLAAlertFRTPhone = await UOW.SLAAlertFRTPhoneRepository.Get(SLAAlertFRTPhone.Id);
                await Logging.CreateAuditLog(SLAAlertFRTPhone, oldData, nameof(SLAAlertFRTPhoneService));
                return SLAAlertFRTPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRTPhone> Delete(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            if (!await SLAAlertFRTPhoneValidator.Delete(SLAAlertFRTPhone))
                return SLAAlertFRTPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTPhoneRepository.Delete(SLAAlertFRTPhone);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTPhone, nameof(SLAAlertFRTPhoneService));
                return SLAAlertFRTPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRTPhone>> BulkDelete(List<SLAAlertFRTPhone> SLAAlertFRTPhones)
        {
            if (!await SLAAlertFRTPhoneValidator.BulkDelete(SLAAlertFRTPhones))
                return SLAAlertFRTPhones;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTPhoneRepository.BulkDelete(SLAAlertFRTPhones);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTPhones, nameof(SLAAlertFRTPhoneService));
                return SLAAlertFRTPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertFRTPhone>> Import(List<SLAAlertFRTPhone> SLAAlertFRTPhones)
        {
            if (!await SLAAlertFRTPhoneValidator.Import(SLAAlertFRTPhones))
                return SLAAlertFRTPhones;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTPhoneRepository.BulkMerge(SLAAlertFRTPhones);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertFRTPhones, new { }, nameof(SLAAlertFRTPhoneService));
                return SLAAlertFRTPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertFRTPhoneFilter ToFilter(SLAAlertFRTPhoneFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertFRTPhoneFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertFRTPhoneFilter subFilter = new SLAAlertFRTPhoneFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAAlertFRTId))
                        subFilter.SLAAlertFRTId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
