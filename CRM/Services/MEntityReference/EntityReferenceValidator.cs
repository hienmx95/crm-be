using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MEntityReference
{
    public interface IEntityReferenceValidator : IServiceScoped
    {
        Task<bool> Create(EntityReference EntityReference);
        Task<bool> Update(EntityReference EntityReference);
        Task<bool> Delete(EntityReference EntityReference);
        Task<bool> BulkDelete(List<EntityReference> EntityReferences);
        Task<bool> Import(List<EntityReference> EntityReferences);
    }

    public class EntityReferenceValidator : IEntityReferenceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public EntityReferenceValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(EntityReference EntityReference)
        {
            EntityReferenceFilter EntityReferenceFilter = new EntityReferenceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = EntityReference.Id },
                Selects = EntityReferenceSelect.Id
            };

            int count = await UOW.EntityReferenceRepository.Count(EntityReferenceFilter);
            if (count == 0)
                EntityReference.AddError(nameof(EntityReferenceValidator), nameof(EntityReference.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(EntityReference EntityReference)
        {
            return EntityReference.IsValidated;
        }

        public async Task<bool> Update(EntityReference EntityReference)
        {
            if (await ValidateId(EntityReference))
            {
            }
            return EntityReference.IsValidated;
        }

        public async Task<bool> Delete(EntityReference EntityReference)
        {
            if (await ValidateId(EntityReference))
            {
            }
            return EntityReference.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<EntityReference> EntityReferences)
        {
            foreach (EntityReference EntityReference in EntityReferences)
            {
                await Delete(EntityReference);
            }
            return EntityReferences.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<EntityReference> EntityReferences)
        {
            return true;
        }
    }
}
