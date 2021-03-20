using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MTicketPriority
{
    public interface ITicketPriorityValidator : IServiceScoped
    {
        Task<bool> Create(TicketPriority TicketPriority);
        Task<bool> Update(TicketPriority TicketPriority);
        Task<bool> Delete(TicketPriority TicketPriority);
        Task<bool> BulkDelete(List<TicketPriority> TicketPriorities);
        Task<bool> Import(List<TicketPriority> TicketPriorities);
    }

    public class TicketPriorityValidator : ITicketPriorityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            ColorCodeEmpty,
            ColorCodeOverLength,
            StatusNotExisted
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketPriorityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketPriority TicketPriority)
        {
            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketPriority.Id },
                Selects = TicketPrioritySelect.Id
            };

            int count = await UOW.TicketPriorityRepository.Count(TicketPriorityFilter);
            if (count == 0)
                TicketPriority.AddError(nameof(TicketPriorityValidator), nameof(TicketPriority.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateName(TicketPriority TicketPriority)
        {
            if (string.IsNullOrWhiteSpace(TicketPriority.Name))
            {
                TicketPriority.AddError(nameof(TicketPriorityValidator), nameof(TicketPriority.Name), ErrorCode.NameEmpty);
            }
            else if (TicketPriority.Name.Length > 255)
            {
                TicketPriority.AddError(nameof(TicketPriorityValidator), nameof(TicketPriority.Name), ErrorCode.NameOverLength);
            }
            return TicketPriority.IsValidated;
        }

        public async Task<bool> ValidateColorCode(TicketPriority TicketPriority)
        {
            if (string.IsNullOrWhiteSpace(TicketPriority.ColorCode))
            {
                TicketPriority.AddError(nameof(TicketPriorityValidator), nameof(TicketPriority.ColorCode), ErrorCode.ColorCodeEmpty);
            }
            else if (TicketPriority.ColorCode.Length > 20)
            {
                TicketPriority.AddError(nameof(TicketPriorityValidator), nameof(TicketPriority.ColorCode), ErrorCode.ColorCodeOverLength);
            }
            return TicketPriority.IsValidated;
        }

        public async Task<bool> ValidateStatus(TicketPriority TicketPriority)
        {
            if (StatusEnum.ACTIVE.Id != TicketPriority.StatusId && StatusEnum.INACTIVE.Id != TicketPriority.StatusId)
                TicketPriority.AddError(nameof(TicketPriorityValidator), nameof(TicketPriority.Status), ErrorCode.StatusNotExisted);
            return TicketPriority.IsValidated;
        }

        public async Task<bool>Create(TicketPriority TicketPriority)
        {
            await ValidateName(TicketPriority);
            await ValidateColorCode(TicketPriority);
            await ValidateStatus(TicketPriority);
            return TicketPriority.IsValidated;
        }

        public async Task<bool> Update(TicketPriority TicketPriority)
        {
            if (await ValidateId(TicketPriority))
            {
                await ValidateName(TicketPriority);
                await ValidateColorCode(TicketPriority);
                await ValidateStatus(TicketPriority);
            }
            return TicketPriority.IsValidated;
        }

        public async Task<bool> Delete(TicketPriority TicketPriority)
        {
            if (await ValidateId(TicketPriority))
            {
            }
            return TicketPriority.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketPriority> TicketPriorities)
        {
            foreach (TicketPriority TicketPriority in TicketPriorities)
            {
                await Delete(TicketPriority);
            }
            return TicketPriorities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketPriority> TicketPriorities)
        {
            return true;
        }
    }
}
