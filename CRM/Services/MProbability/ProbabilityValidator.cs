using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MProbability
{
    public interface IProbabilityValidator : IServiceScoped
    {
        Task<bool> Create(Probability Probability);
        Task<bool> Update(Probability Probability);
        Task<bool> Delete(Probability Probability);
        Task<bool> BulkDelete(List<Probability> Probabilities);
        Task<bool> Import(List<Probability> Probabilities);
    }

    public class ProbabilityValidator : IProbabilityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ProbabilityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Probability Probability)
        {
            ProbabilityFilter ProbabilityFilter = new ProbabilityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Probability.Id },
                Selects = ProbabilitySelect.Id
            };

            int count = await UOW.ProbabilityRepository.Count(ProbabilityFilter);
            if (count == 0)
                Probability.AddError(nameof(ProbabilityValidator), nameof(Probability.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(Probability Probability)
        {
            return Probability.IsValidated;
        }

        public async Task<bool> Update(Probability Probability)
        {
            if (await ValidateId(Probability))
            {
            }
            return Probability.IsValidated;
        }

        public async Task<bool> Delete(Probability Probability)
        {
            if (await ValidateId(Probability))
            {
            }
            return Probability.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Probability> Probabilities)
        {
            foreach (Probability Probability in Probabilities)
            {
                await Delete(Probability);
            }
            return Probabilities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Probability> Probabilities)
        {
            return true;
        }
    }
}
