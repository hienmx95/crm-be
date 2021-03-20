using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MOpportunity
{
    public interface IOpportunityValidator : IServiceScoped
    {
        Task<bool> Create(Opportunity Opportunity);
        Task<bool> Update(Opportunity Opportunity);
        Task<bool> Delete(Opportunity Opportunity);
        Task<bool> BulkDelete(List<Opportunity> Opportunities);
        Task<bool> Import(List<Opportunity> Opportunities);
    }

    public class OpportunityValidator : IOpportunityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            SaleStageEmpty,
            AppUserEmpty,
            ProbabilityEmpty,
            AppUserNotExisted,
            CompanyNotExisted,
            CompanyEmpty,
            ClosingDateEmpty,
            ClosingDateWrong,
            OpportunityResultDescriptionEmpty,
            OpportunityResultTypeEmpty,
            PotentialResultEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OpportunityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Opportunity Opportunity)
        {
            OpportunityFilter OpportunityFilter = new OpportunityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Opportunity.Id },
                Selects = OpportunitySelect.Id
            };

            int count = await UOW.OpportunityRepository.Count(OpportunityFilter);
            if (count == 0)
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateName(Opportunity Opportunity)
        {
            if (string.IsNullOrWhiteSpace(Opportunity.Name))
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.Name), ErrorCode.NameEmpty);
            }
            else if (Opportunity.Name.Length > 500)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.Name), ErrorCode.NameOverLength);
            }
            return Opportunity.IsValidated;
        }
        public async Task<bool> ValidateSaleStage(Opportunity Opportunity)
        {
            if (Opportunity.SaleStageId == Enums.SaleStageEnum.CLOSED.Id && !Opportunity.PotentialResultId.HasValue)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.PotentialResult), ErrorCode.PotentialResultEmpty);
            }
            return Opportunity.IsValidated;
        }


        public async Task<bool> ValidateAppUser(Opportunity Opportunity)
        {
            if (Opportunity.AppUserId == 0)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.AppUser), ErrorCode.AppUserEmpty);
            }
            else
            {
                AppUserFilter AppUserFilter = new AppUserFilter
                {
                    Id = new IdFilter { Equal = Opportunity.AppUserId }
                };

                var count = await UOW.AppUserRepository.Count(AppUserFilter);
                if (count == 0)
                    Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.AppUser), ErrorCode.AppUserNotExisted);
            }
            return Opportunity.IsValidated;
        }
        public async Task<bool> ValidateCompany(Opportunity Opportunity)
        {
            if (Opportunity.CompanyId.HasValue)
            {
                CompanyFilter CompanyFilter = new CompanyFilter
                {
                    Id = new IdFilter { Equal = Opportunity.CompanyId }
                };

                var count = await UOW.CompanyRepository.Count(CompanyFilter);
                if (count == 0)
                    Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.Company), ErrorCode.CompanyNotExisted);
            }
            else
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.Company), ErrorCode.CompanyEmpty);
            }
            return Opportunity.IsValidated;
        }

        public async Task<bool> ValidateProbability(Opportunity Opportunity)
        {
            if (Opportunity.Probability == null)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.Probability), ErrorCode.ProbabilityEmpty);
            }
            return Opportunity.IsValidated;
        }

        private async Task<bool> ValidateClosingDate(Opportunity Opportunity)
        {

            if (Opportunity.ClosingDate == default(DateTime))
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.ClosingDate), ErrorCode.ClosingDateEmpty);
            }
            if (Opportunity.ClosingDate.Date < StaticParams.DateTimeNow.Date)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.ClosingDate), ErrorCode.ClosingDateWrong);
            }
            return Opportunity.IsValidated;
        }
        public async Task<bool> ValidateResultType(Opportunity Opportunity)
        {
            if (Opportunity.OpportunityResultTypeId == Enums.OpportunityResultTypeEnum.KHAC.Id)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.OpportunityResultType), ErrorCode.OpportunityResultDescriptionEmpty);
            }
            if (Opportunity.PotentialResultId == Enums.PotentialResultEnum.FAILED.Id && !Opportunity.OpportunityResultTypeId.HasValue)
            {
                Opportunity.AddError(nameof(OpportunityValidator), nameof(Opportunity.OpportunityResultType), ErrorCode.OpportunityResultTypeEmpty);
            }
            return Opportunity.IsValidated;
        }
        public async Task<bool> Create(Opportunity Opportunity)
        {
            await ValidateName(Opportunity);
            await ValidateSaleStage(Opportunity);
            await ValidateAppUser(Opportunity);
            await ValidateProbability(Opportunity);
            await ValidateClosingDate(Opportunity);
            //await ValidateCompany(Opportunity);
            await ValidateResultType(Opportunity);
            return Opportunity.IsValidated;
        }

        public async Task<bool> Update(Opportunity Opportunity)
        {
            if (await ValidateId(Opportunity))
            {
                await ValidateName(Opportunity);
                await ValidateSaleStage(Opportunity);
                await ValidateAppUser(Opportunity);
                await ValidateProbability(Opportunity);
                await ValidateClosingDate(Opportunity);
                await ValidateResultType(Opportunity);
                //await ValidateCompany(Opportunity);
            }
            return Opportunity.IsValidated;
        }

        public async Task<bool> Delete(Opportunity Opportunity)
        {
            if (await ValidateId(Opportunity))
            {
            }
            return Opportunity.IsValidated;
        }

        public async Task<bool> BulkDelete(List<Opportunity> Opportunities)
        {
            foreach (Opportunity Opportunity in Opportunities)
            {
                await Delete(Opportunity);
            }
            return Opportunities.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<Opportunity> Opportunities)
        {
            return true;
        }
    }
}
