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

namespace CRM.Services.MEntityReference
{
    public interface IEntityReferenceService :  IServiceScoped
    {
        Task<int> Count(EntityReferenceFilter EntityReferenceFilter);
        Task<List<EntityReference>> List(EntityReferenceFilter EntityReferenceFilter);
        Task<EntityReference> Get(long Id);
    }

    public class EntityReferenceService : BaseService, IEntityReferenceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public EntityReferenceService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(EntityReferenceFilter EntityReferenceFilter)
        {
            try
            {
                int result = await UOW.EntityReferenceRepository.Count(EntityReferenceFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EntityReferenceService));
            }
            return 0;
        }

        public async Task<List<EntityReference>> List(EntityReferenceFilter EntityReferenceFilter)
        {
            try
            {
                List<EntityReference> EntityReferences = await UOW.EntityReferenceRepository.List(EntityReferenceFilter);
                return EntityReferences;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EntityReferenceService));
            }
            return null;
        }
        
        public async Task<EntityReference> Get(long Id)
        {
            EntityReference EntityReference = await UOW.EntityReferenceRepository.Get(Id);
            if (EntityReference == null)
                return null;
            return EntityReference;
        }
    }
}
