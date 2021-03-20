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

namespace CRM.Services.MSLAEscalationPhone
{
    public interface ISLAEscalationPhoneService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationPhoneFilter SLAEscalationPhoneFilter);
        Task<List<SLAEscalationPhone>> List(SLAEscalationPhoneFilter SLAEscalationPhoneFilter);
        Task<SLAEscalationPhone> Get(long Id);
        Task<SLAEscalationPhone> Create(SLAEscalationPhone SLAEscalationPhone);
        Task<SLAEscalationPhone> Update(SLAEscalationPhone SLAEscalationPhone);
        Task<SLAEscalationPhone> Delete(SLAEscalationPhone SLAEscalationPhone);
        Task<List<SLAEscalationPhone>> BulkDelete(List<SLAEscalationPhone> SLAEscalationPhones);
        Task<List<SLAEscalationPhone>> Import(List<SLAEscalationPhone> SLAEscalationPhones);
        SLAEscalationPhoneFilter ToFilter(SLAEscalationPhoneFilter SLAEscalationPhoneFilter);
    }

    public class SLAEscalationPhoneService : BaseService, ISLAEscalationPhoneService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationPhoneValidator SLAEscalationPhoneValidator;

        public SLAEscalationPhoneService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationPhoneValidator SLAEscalationPhoneValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationPhoneValidator = SLAEscalationPhoneValidator;
        }
        public async Task<int> Count(SLAEscalationPhoneFilter SLAEscalationPhoneFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationPhoneRepository.Count(SLAEscalationPhoneFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationPhone>> List(SLAEscalationPhoneFilter SLAEscalationPhoneFilter)
        {
            try
            {
                List<SLAEscalationPhone> SLAEscalationPhones = await UOW.SLAEscalationPhoneRepository.List(SLAEscalationPhoneFilter);
                return SLAEscalationPhones;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationPhone> Get(long Id)
        {
            SLAEscalationPhone SLAEscalationPhone = await UOW.SLAEscalationPhoneRepository.Get(Id);
            if (SLAEscalationPhone == null)
                return null;
            return SLAEscalationPhone;
        }
       
        public async Task<SLAEscalationPhone> Create(SLAEscalationPhone SLAEscalationPhone)
        {
            if (!await SLAEscalationPhoneValidator.Create(SLAEscalationPhone))
                return SLAEscalationPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationPhoneRepository.Create(SLAEscalationPhone);
                await UOW.Commit();
                SLAEscalationPhone = await UOW.SLAEscalationPhoneRepository.Get(SLAEscalationPhone.Id);
                await Logging.CreateAuditLog(SLAEscalationPhone, new { }, nameof(SLAEscalationPhoneService));
                return SLAEscalationPhone;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationPhone> Update(SLAEscalationPhone SLAEscalationPhone)
        {
            if (!await SLAEscalationPhoneValidator.Update(SLAEscalationPhone))
                return SLAEscalationPhone;
            try
            {
                var oldData = await UOW.SLAEscalationPhoneRepository.Get(SLAEscalationPhone.Id);

                await UOW.Begin();
                await UOW.SLAEscalationPhoneRepository.Update(SLAEscalationPhone);
                await UOW.Commit();

                SLAEscalationPhone = await UOW.SLAEscalationPhoneRepository.Get(SLAEscalationPhone.Id);
                await Logging.CreateAuditLog(SLAEscalationPhone, oldData, nameof(SLAEscalationPhoneService));
                return SLAEscalationPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationPhone> Delete(SLAEscalationPhone SLAEscalationPhone)
        {
            if (!await SLAEscalationPhoneValidator.Delete(SLAEscalationPhone))
                return SLAEscalationPhone;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationPhoneRepository.Delete(SLAEscalationPhone);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationPhone, nameof(SLAEscalationPhoneService));
                return SLAEscalationPhone;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationPhone>> BulkDelete(List<SLAEscalationPhone> SLAEscalationPhones)
        {
            if (!await SLAEscalationPhoneValidator.BulkDelete(SLAEscalationPhones))
                return SLAEscalationPhones;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationPhoneRepository.BulkDelete(SLAEscalationPhones);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationPhones, nameof(SLAEscalationPhoneService));
                return SLAEscalationPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationPhone>> Import(List<SLAEscalationPhone> SLAEscalationPhones)
        {
            if (!await SLAEscalationPhoneValidator.Import(SLAEscalationPhones))
                return SLAEscalationPhones;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationPhoneRepository.BulkMerge(SLAEscalationPhones);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationPhones, new { }, nameof(SLAEscalationPhoneService));
                return SLAEscalationPhones;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationPhoneService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationPhoneFilter ToFilter(SLAEscalationPhoneFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationPhoneFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationPhoneFilter subFilter = new SLAEscalationPhoneFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAEscalationId))
                        subFilter.SLAEscalationId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
