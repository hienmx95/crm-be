using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSaleStage
{
    public interface ISaleStageValidator : IServiceScoped
    {
        Task<bool> Create(SaleStage SaleStage);
        Task<bool> Update(SaleStage SaleStage);
        Task<bool> Delete(SaleStage SaleStage);
        Task<bool> BulkDelete(List<SaleStage> SaleStages);
        Task<bool> Import(List<SaleStage> SaleStages);
    }

    public class SaleStageValidator : ISaleStageValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SaleStageValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SaleStage SaleStage)
        {
            SaleStageFilter SaleStageFilter = new SaleStageFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SaleStage.Id },
                Selects = SaleStageSelect.Id
            };

            int count = await UOW.SaleStageRepository.Count(SaleStageFilter);
            if (count == 0)
                SaleStage.AddError(nameof(SaleStageValidator), nameof(SaleStage.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SaleStage SaleStage)
        {
            return SaleStage.IsValidated;
        }

        public async Task<bool> Update(SaleStage SaleStage)
        {
            if (await ValidateId(SaleStage))
            {
            }
            return SaleStage.IsValidated;
        }

        public async Task<bool> Delete(SaleStage SaleStage)
        {
            if (await ValidateId(SaleStage))
            {
            }
            return SaleStage.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SaleStage> SaleStages)
        {
            foreach (SaleStage SaleStage in SaleStages)
            {
                await Delete(SaleStage);
            }
            return SaleStages.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SaleStage> SaleStages)
        {
            return true;
        }
    }
}
