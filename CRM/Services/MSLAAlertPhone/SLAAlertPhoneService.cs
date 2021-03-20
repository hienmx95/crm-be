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

namespace CRM.Services.MSLAAlertPhone
{
    public interface ISLAAlertPhoneService :  IServiceScoped
    {
        Task<int> Count(SLAAlertPhoneFilter SLAAlertPhoneFilter);
        Task<List<SLAAlertPhone>> List(SLAAlertPhoneFilter SLAAlertPhoneFilter);
        Task<SLAAlertPhone> Get(long Id);
        Task<SLAAlertPhone> Create(SLAAlertPhone SLAAlertPhone);
        Task<SLAAlertPhone> Update(SLAAlertPhone SLAAlertPhone);
        Task<SLAAlertPhone> Delete(SLAAlertPhone SLAAlertPhone);
        Task<List<SLAAlertPhone>> BulkDelete(List<SLAAlertPhone> SLAAlertPhones);
        Task<List<SLAAlertPhone>> Import(List<SLAAlertPhone> SLAAlertPhones);
        SLAAlertPhoneFilter ToFilter(SLAAlertPhoneFilter SLAAlertPhoneFilter);
    }

    public class SLAAlertPhoneService : BaseService, ISLAAlertPhoneService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertPhoneValidator SLAAlertPhoneValidator;

        public SLAAlertPhoneService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertPhoneValidator SLAAlertPhoneValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertPhoneValidator = SLAAlertPhoneValidator;
        }
        public async Task<int> Count(SLAAlertPhoneFilter SLAAlertPhoneFilter)
        {
            try
            {
                int result = await UOW.SLAAlertPhoneRepository.Count(SLAAlertPhoneFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertPhone>> List(SLAAlertPhoneFilter SLAAlertPhoneFilter)
        {
            try
            {
                List<SLAAlertPhone> SLAAlertPhones = await UOW.SLAAlertPhoneRepository.List(SLAAlertPhoneFilter);
                return SLAAlertPhones;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertPhone> Get(long Id)
        {
            SLAAlertPhone SLAAlertPhone = await UOW.SLAAlertPhoneRepository.Get(Id);
            if (SLAAlertPhone == null)
                return null;
            return SLAAlertPhone;
        }
       
        public async Task<SLAAlertPhone> Create(SLAAlertPhone SLAAlertPhone)
        {
            if (!await SLAAlertPhoneValidator.Create(SLAAlertPhone))
                return SLAAlertPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertPhoneRepository.Create(SLAAlertPhone);
                await UOW.Commit();
                SLAAlertPhone = await UOW.SLAAlertPhoneRepository.Get(SLAAlertPhone.Id);
                await Logging.CreateAuditLog(SLAAlertPhone, new { }, nameof(SLAAlertPhoneService));
                return SLAAlertPhone;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertPhone> Update(SLAAlertPhone SLAAlertPhone)
        {
            if (!await SLAAlertPhoneValidator.Update(SLAAlertPhone))
                return SLAAlertPhone;
            try
            {
                var oldData = await UOW.SLAAlertPhoneRepository.Get(SLAAlertPhone.Id);

                await UOW.Begin();
                await UOW.SLAAlertPhoneRepository.Update(SLAAlertPhone);
                await UOW.Commit();

                SLAAlertPhone = await UOW.SLAAlertPhoneRepository.Get(SLAAlertPhone.Id);
                await Logging.CreateAuditLog(SLAAlertPhone, oldData, nameof(SLAAlertPhoneService));
                return SLAAlertPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertPhone> Delete(SLAAlertPhone SLAAlertPhone)
        {
            if (!await SLAAlertPhoneValidator.Delete(SLAAlertPhone))
                return SLAAlertPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertPhoneRepository.Delete(SLAAlertPhone);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertPhone, nameof(SLAAlertPhoneService));
                return SLAAlertPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertPhone>> BulkDelete(List<SLAAlertPhone> SLAAlertPhones)
        {
            if (!await SLAAlertPhoneValidator.BulkDelete(SLAAlertPhones))
                return SLAAlertPhones;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertPhoneRepository.BulkDelete(SLAAlertPhones);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertPhones, nameof(SLAAlertPhoneService));
                return SLAAlertPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertPhone>> Import(List<SLAAlertPhone> SLAAlertPhones)
        {
            if (!await SLAAlertPhoneValidator.Import(SLAAlertPhones))
                return SLAAlertPhones;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertPhoneRepository.BulkMerge(SLAAlertPhones);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertPhones, new { }, nameof(SLAAlertPhoneService));
                return SLAAlertPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertPhoneFilter ToFilter(SLAAlertPhoneFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertPhoneFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertPhoneFilter subFilter = new SLAAlertPhoneFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAAlertId))
                        subFilter.SLAAlertId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
