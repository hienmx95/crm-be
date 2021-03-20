using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MTicketStatus
{
    public interface ITicketStatusValidator : IServiceScoped
    {
        Task<bool> Create(TicketStatus TicketStatus);
        Task<bool> Update(TicketStatus TicketStatus);
        Task<bool> Delete(TicketStatus TicketStatus);
        Task<bool> BulkDelete(List<TicketStatus> TicketStatuses);
        Task<bool> Import(List<TicketStatus> TicketStatuses);
    }

    public class TicketStatusValidator : ITicketStatusValidator
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

        public TicketStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketStatus TicketStatus)
        {
            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketStatus.Id },
                Selects = TicketStatusSelect.Id
            };

            int count = await UOW.TicketStatusRepository.Count(TicketStatusFilter);
            if (count == 0)
                TicketStatus.AddError(nameof(TicketStatusValidator), nameof(TicketStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateName(TicketStatus TicketStatus)
        {
            if (string.IsNullOrWhiteSpace(TicketStatus.Name))
            {
                TicketStatus.AddError(nameof(TicketStatusValidator), nameof(TicketStatus.Name), ErrorCode.NameEmpty);
            }
            else if (TicketStatus.Name.Length > 255)
            {
                TicketStatus.AddError(nameof(TicketStatusValidator), nameof(TicketStatus.Name), ErrorCode.NameOverLength);
            }
            return TicketStatus.IsValidated;
        }

        public async Task<bool> ValidateColorCode(TicketStatus TicketStatus)
        {
            if (string.IsNullOrWhiteSpace(TicketStatus.ColorCode))
            {
                TicketStatus.AddError(nameof(TicketStatusValidator), nameof(TicketStatus.ColorCode), ErrorCode.ColorCodeEmpty);
            }
            else if (TicketStatus.ColorCode.Length > 20)
            {
                TicketStatus.AddError(nameof(TicketStatusValidator), nameof(TicketStatus.ColorCode), ErrorCode.ColorCodeOverLength);
            }
            return TicketStatus.IsValidated;
        }

        public async Task<bool> ValidateStatus(TicketStatus TicketStatus)
        {
            if (StatusEnum.ACTIVE.Id != TicketStatus.StatusId && StatusEnum.INACTIVE.Id != TicketStatus.StatusId)
                TicketStatus.AddError(nameof(TicketStatusValidator), nameof(TicketStatus.Status), ErrorCode.StatusNotExisted);
            return TicketStatus.IsValidated;
        }
        public async Task<bool>Create(TicketStatus TicketStatus)
        {
            await ValidateName(TicketStatus);
            await ValidateColorCode(TicketStatus);
            await ValidateStatus(TicketStatus);
            return TicketStatus.IsValidated;
        }

        public async Task<bool> Update(TicketStatus TicketStatus)
        {
            if (await ValidateId(TicketStatus))
            {
                await ValidateName(TicketStatus);
                await ValidateColorCode(TicketStatus);
                await ValidateStatus(TicketStatus);
            }
            return TicketStatus.IsValidated;
        }

        public async Task<bool> Delete(TicketStatus TicketStatus)
        {
            if (await ValidateId(TicketStatus))
            {
            }
            return TicketStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketStatus> TicketStatuses)
        {
            foreach (TicketStatus TicketStatus in TicketStatuses)
            {
                await Delete(TicketStatus);
            }
            return TicketStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketStatus> TicketStatuses)
        {
            return true;
        }
    }
}
