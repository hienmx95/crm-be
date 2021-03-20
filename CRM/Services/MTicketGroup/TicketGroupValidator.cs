using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MTicketGroup
{
    public interface ITicketGroupValidator : IServiceScoped
    {
        Task<bool> Create(TicketGroup TicketGroup);
        Task<bool> Update(TicketGroup TicketGroup);
        Task<bool> Delete(TicketGroup TicketGroup);
        Task<bool> BulkDelete(List<TicketGroup> TicketGroups);
        Task<bool> Import(List<TicketGroup> TicketGroups);
    }

    public class TicketGroupValidator : ITicketGroupValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            StatusNotExisted,
            TicketTypeEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketGroupValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketGroup TicketGroup)
        {
            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketGroup.Id },
                Selects = TicketGroupSelect.Id
            };

            int count = await UOW.TicketGroupRepository.Count(TicketGroupFilter);
            if (count == 0)
                TicketGroup.AddError(nameof(TicketGroupValidator), nameof(TicketGroup.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateName(TicketGroup TicketGroup)
        {
            if (string.IsNullOrWhiteSpace(TicketGroup.Name))
            {
                TicketGroup.AddError(nameof(TicketGroupValidator), nameof(TicketGroup.Name), ErrorCode.NameEmpty);
            }
            else if (TicketGroup.Name.Length > 255)
            {
                TicketGroup.AddError(nameof(TicketGroupValidator), nameof(TicketGroup.Name), ErrorCode.NameOverLength);
            }
            return TicketGroup.IsValidated;
        }

        public async Task<bool> ValidateTicketType(TicketGroup TicketGroup)
        {
            if (TicketGroup.TicketType == null)
            {
                TicketGroup.AddError(nameof(TicketGroupValidator), nameof(TicketGroup.TicketType), ErrorCode.TicketTypeEmpty);
            }
            return TicketGroup.IsValidated;
        }

        public async Task<bool> ValidateStatus(TicketGroup TicketGroup)
        {
            if (StatusEnum.ACTIVE.Id != TicketGroup.StatusId && StatusEnum.INACTIVE.Id != TicketGroup.StatusId)
                TicketGroup.AddError(nameof(TicketGroupValidator), nameof(TicketGroup.Status), ErrorCode.StatusNotExisted);
            return TicketGroup.IsValidated;
        }

        public async Task<bool>Create(TicketGroup TicketGroup)
        {
            await ValidateName(TicketGroup);
            await ValidateTicketType(TicketGroup);
            await ValidateStatus(TicketGroup);
            return TicketGroup.IsValidated;
        }

        public async Task<bool> Update(TicketGroup TicketGroup)
        {
            if (await ValidateId(TicketGroup))
            {
                await ValidateName(TicketGroup);
                await ValidateTicketType(TicketGroup);
                await ValidateStatus(TicketGroup);
            }
            return TicketGroup.IsValidated;
        }

        public async Task<bool> Delete(TicketGroup TicketGroup)
        {
            if (await ValidateId(TicketGroup))
            {
            }
            return TicketGroup.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketGroup> TicketGroups)
        {
            foreach (TicketGroup TicketGroup in TicketGroups)
            {
                await Delete(TicketGroup);
            }
            return TicketGroups.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketGroup> TicketGroups)
        {
            return true;
        }
    }
}
