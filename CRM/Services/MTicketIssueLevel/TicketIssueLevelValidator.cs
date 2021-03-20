using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MTicketIssueLevel
{
    public interface ITicketIssueLevelValidator : IServiceScoped
    {
        Task<bool> Create(TicketIssueLevel TicketIssueLevel);
        Task<bool> Update(TicketIssueLevel TicketIssueLevel);
        Task<bool> Delete(TicketIssueLevel TicketIssueLevel);
        Task<bool> BulkDelete(List<TicketIssueLevel> TicketIssueLevels);
        Task<bool> Import(List<TicketIssueLevel> TicketIssueLevels);
    }

    public class TicketIssueLevelValidator : ITicketIssueLevelValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            StatusNotExisted,
            TicketGroupEmpty,
            SLAEmpty,
            FirstResponseTimeEmpty,
            ResolveTimeWrong,
            ResolveTimeEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketIssueLevelValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketIssueLevel TicketIssueLevel)
        {
            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketIssueLevel.Id },
                Selects = TicketIssueLevelSelect.Id
            };

            int count = await UOW.TicketIssueLevelRepository.Count(TicketIssueLevelFilter);
            if (count == 0)
                TicketIssueLevel.AddError(nameof(TicketIssueLevelValidator), nameof(TicketIssueLevel.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateName(TicketIssueLevel TicketIssueLevel)
        {
            if (string.IsNullOrWhiteSpace(TicketIssueLevel.Name))
            {
                TicketIssueLevel.AddError(nameof(TicketIssueLevelValidator), nameof(TicketIssueLevel.Name), ErrorCode.NameEmpty);
            }
            else if (TicketIssueLevel.Name.Length > 255)
            {
                TicketIssueLevel.AddError(nameof(TicketIssueLevelValidator), nameof(TicketIssueLevel.Name), ErrorCode.NameOverLength);
            }
            return TicketIssueLevel.IsValidated;
        }

        public async Task<bool> ValidateSLA(TicketIssueLevel TicketIssueLevel)
        {
            if (TicketIssueLevel.SLA <= 0)
            {
                TicketIssueLevel.AddError(nameof(TicketIssueLevelValidator), nameof(TicketIssueLevel.SLA), ErrorCode.SLAEmpty);
            }
          
            return TicketIssueLevel.IsValidated;
        }

        public async Task<bool> ValidateTicketGroup(TicketIssueLevel TicketIssueLevel)
        {
            if (TicketIssueLevel.TicketGroup == null)
            {
                TicketIssueLevel.AddError(nameof(TicketIssueLevelValidator), nameof(TicketIssueLevel.TicketGroup), ErrorCode.TicketGroupEmpty);
            }
            return TicketIssueLevel.IsValidated;
        }

        public async Task<bool> ValidateStatus(TicketIssueLevel TicketIssueLevel)
        {
            if (StatusEnum.ACTIVE.Id != TicketIssueLevel.StatusId && StatusEnum.INACTIVE.Id != TicketIssueLevel.StatusId)
                TicketIssueLevel.AddError(nameof(TicketIssueLevelValidator), nameof(TicketIssueLevel.Status), ErrorCode.StatusNotExisted);
            return TicketIssueLevel.IsValidated;
        }

        public async Task<bool> ValidateSLAPolicy(TicketIssueLevel TicketIssueLevel)
        {
            if (TicketIssueLevel.SLAPolicies != null && TicketIssueLevel.SLAPolicies.Any())
            {
                foreach (var SLAPolicy in TicketIssueLevel.SLAPolicies)
                {
                    if (SLAPolicy.FirstResponseTime == null)
                    {
                        SLAPolicy.AddError(nameof(TicketIssueLevelValidator), nameof(SLAPolicy.FirstResponseTime), ErrorCode.FirstResponseTimeEmpty);
                    }
                    if (SLAPolicy.ResolveTime == null || SLAPolicy.ResolveTime <= 0)
                    {
                        SLAPolicy.AddError(nameof(TicketIssueLevelValidator), nameof(SLAPolicy.ResolveTime), ErrorCode.ResolveTimeEmpty);
                    }
                    else
                    {
                        if (ConvertSLATimeToMenute(SLAPolicy.ResolveTime.Value, SLAPolicy.ResolveUnit) < ConvertSLATimeToMenute(SLAPolicy.FirstResponseTime.Value, SLAPolicy.FirstResponseUnit))
                        {
                            SLAPolicy.AddError(nameof(TicketIssueLevelValidator), nameof(SLAPolicy.ResolveTime), ErrorCode.ResolveTimeWrong);
                        }
                    }
                }
            }
            return TicketIssueLevel.IsValidated;
        }
        public long ConvertSLATimeToMenute(long Time, SLATimeUnit SLATimeUnit)
        {
            if (SLATimeUnit.Id == SLATimeUnitEnum.HOURS.Id)
                return (Time * 60);
            else if (SLATimeUnit.Id == SLATimeUnitEnum.DAY.Id)
                return (Time * 24 * 60);
            else
                return Time;
        }
        public async Task<bool>Create(TicketIssueLevel TicketIssueLevel)
        {
            await ValidateName(TicketIssueLevel);
            await ValidateSLAPolicy(TicketIssueLevel);
            await ValidateStatus(TicketIssueLevel);
            await ValidateTicketGroup(TicketIssueLevel);
            return TicketIssueLevel.IsValidated;
        }

        public async Task<bool> Update(TicketIssueLevel TicketIssueLevel)
        {
            if (await ValidateId(TicketIssueLevel))
            {
                await ValidateName(TicketIssueLevel);
                await ValidateSLAPolicy(TicketIssueLevel);
                await ValidateStatus(TicketIssueLevel);
                await ValidateTicketGroup(TicketIssueLevel);
            }
            return TicketIssueLevel.IsValidated;
        }

        public async Task<bool> Delete(TicketIssueLevel TicketIssueLevel)
        {
            if (await ValidateId(TicketIssueLevel))
            {
            }
            return TicketIssueLevel.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketIssueLevel> TicketIssueLevels)
        {
            foreach (TicketIssueLevel TicketIssueLevel in TicketIssueLevels)
            {
                await Delete(TicketIssueLevel);
            }
            return TicketIssueLevels.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketIssueLevel> TicketIssueLevels)
        {
            return true;
        }
    }
}
