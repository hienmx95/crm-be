using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCooperativeAttitude
{
    public interface ICooperativeAttitudeValidator : IServiceScoped
    {
        Task<bool> Create(CooperativeAttitude CooperativeAttitude);
        Task<bool> Update(CooperativeAttitude CooperativeAttitude);
        Task<bool> Delete(CooperativeAttitude CooperativeAttitude);
        Task<bool> BulkDelete(List<CooperativeAttitude> CooperativeAttitudes);
        Task<bool> Import(List<CooperativeAttitude> CooperativeAttitudes);
    }

    public class CooperativeAttitudeValidator : ICooperativeAttitudeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CooperativeAttitudeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CooperativeAttitude CooperativeAttitude)
        {
            CooperativeAttitudeFilter CooperativeAttitudeFilter = new CooperativeAttitudeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CooperativeAttitude.Id },
                Selects = CooperativeAttitudeSelect.Id
            };

            int count = await UOW.CooperativeAttitudeRepository.Count(CooperativeAttitudeFilter);
            if (count == 0)
                CooperativeAttitude.AddError(nameof(CooperativeAttitudeValidator), nameof(CooperativeAttitude.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CooperativeAttitude CooperativeAttitude)
        {
            return CooperativeAttitude.IsValidated;
        }

        public async Task<bool> Update(CooperativeAttitude CooperativeAttitude)
        {
            if (await ValidateId(CooperativeAttitude))
            {
            }
            return CooperativeAttitude.IsValidated;
        }

        public async Task<bool> Delete(CooperativeAttitude CooperativeAttitude)
        {
            if (await ValidateId(CooperativeAttitude))
            {
            }
            return CooperativeAttitude.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CooperativeAttitude> CooperativeAttitudes)
        {
            foreach (CooperativeAttitude CooperativeAttitude in CooperativeAttitudes)
            {
                await Delete(CooperativeAttitude);
            }
            return CooperativeAttitudes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CooperativeAttitude> CooperativeAttitudes)
        {
            return true;
        }
    }
}
