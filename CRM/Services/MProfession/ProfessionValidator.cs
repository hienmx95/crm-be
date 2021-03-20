using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MProfession
{
    public interface IProfessionValidator : IServiceScoped
    {
        Task<bool> Create(Profession Profession);
        Task<bool> Update(Profession Profession);
        Task<bool> Delete(Profession Profession);
        Task<bool> BulkDelete(List<Profession> Professions);
        Task<bool> Import(List<Profession> Professions);
    }

    public class ProfessionValidator : IProfessionValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ProfessionValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Profession Profession)
        {
            ProfessionFilter ProfessionFilter = new ProfessionFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Profession.Id },
                Selects = ProfessionSelect.Id
            };

            int count = await UOW.ProfessionRepository.Count(ProfessionFilter);
            if (count == 0)
                Profession.AddError(nameof(ProfessionValidator), nameof(Profession.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(Profession Profession)
        {
            return Profession.IsValidated;
        }

        public async Task<bool> Update(Profession Profession)
        {
            if (await ValidateId(Profession))
            {
            }
            return Profession.IsValidated;
        }

        public async Task<bool> Delete(Profession Profession)
        {
            if (await ValidateId(Profession))
            {
            }
            return Profession.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Profession> Professions)
        {
            foreach (Profession Profession in Professions)
            {
                await Delete(Profession);
            }
            return Professions.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Profession> Professions)
        {
            return true;
        }
    }
}
