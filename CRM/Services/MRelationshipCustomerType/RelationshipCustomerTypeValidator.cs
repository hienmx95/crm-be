using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MRelationshipCustomerType
{
    public interface IRelationshipCustomerTypeValidator : IServiceScoped
    {
        Task<bool> Create(RelationshipCustomerType RelationshipCustomerType);
        Task<bool> Update(RelationshipCustomerType RelationshipCustomerType);
        Task<bool> Delete(RelationshipCustomerType RelationshipCustomerType);
        Task<bool> BulkDelete(List<RelationshipCustomerType> RelationshipCustomerTypes);
        Task<bool> Import(List<RelationshipCustomerType> RelationshipCustomerTypes);
    }

    public class RelationshipCustomerTypeValidator : IRelationshipCustomerTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public RelationshipCustomerTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(RelationshipCustomerType RelationshipCustomerType)
        {
            RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter = new RelationshipCustomerTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = RelationshipCustomerType.Id },
                Selects = RelationshipCustomerTypeSelect.Id
            };

            int count = await UOW.RelationshipCustomerTypeRepository.Count(RelationshipCustomerTypeFilter);
            if (count == 0)
                RelationshipCustomerType.AddError(nameof(RelationshipCustomerTypeValidator), nameof(RelationshipCustomerType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(RelationshipCustomerType RelationshipCustomerType)
        {
            return RelationshipCustomerType.IsValidated;
        }

        public async Task<bool> Update(RelationshipCustomerType RelationshipCustomerType)
        {
            if (await ValidateId(RelationshipCustomerType))
            {
            }
            return RelationshipCustomerType.IsValidated;
        }

        public async Task<bool> Delete(RelationshipCustomerType RelationshipCustomerType)
        {
            if (await ValidateId(RelationshipCustomerType))
            {
            }
            return RelationshipCustomerType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<RelationshipCustomerType> RelationshipCustomerTypes)
        {
            foreach (RelationshipCustomerType RelationshipCustomerType in RelationshipCustomerTypes)
            {
                await Delete(RelationshipCustomerType);
            }
            return RelationshipCustomerTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<RelationshipCustomerType> RelationshipCustomerTypes)
        {
            return true;
        }
    }
}
