using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTicketOfUser
{
    public interface ITicketOfUserValidator : IServiceScoped
    {
        Task<bool> Create(TicketOfUser TicketOfUser);
        Task<bool> Update(TicketOfUser TicketOfUser);
        Task<bool> Delete(TicketOfUser TicketOfUser);
        Task<bool> BulkDelete(List<TicketOfUser> TicketOfUsers);
        Task<bool> Import(List<TicketOfUser> TicketOfUsers);
    }

    public class TicketOfUserValidator : ITicketOfUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketOfUserValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketOfUser TicketOfUser)
        {
            TicketOfUserFilter TicketOfUserFilter = new TicketOfUserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketOfUser.Id },
                Selects = TicketOfUserSelect.Id
            };

            int count = await UOW.TicketOfUserRepository.Count(TicketOfUserFilter);
            if (count == 0)
                TicketOfUser.AddError(nameof(TicketOfUserValidator), nameof(TicketOfUser.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(TicketOfUser TicketOfUser)
        {
            return TicketOfUser.IsValidated;
        }

        public async Task<bool> Update(TicketOfUser TicketOfUser)
        {
            if (await ValidateId(TicketOfUser))
            {
            }
            return TicketOfUser.IsValidated;
        }

        public async Task<bool> Delete(TicketOfUser TicketOfUser)
        {
            if (await ValidateId(TicketOfUser))
            {
            }
            return TicketOfUser.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketOfUser> TicketOfUsers)
        {
            foreach (TicketOfUser TicketOfUser in TicketOfUsers)
            {
                await Delete(TicketOfUser);
            }
            return TicketOfUsers.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketOfUser> TicketOfUsers)
        {
            return true;
        }
    }
}
