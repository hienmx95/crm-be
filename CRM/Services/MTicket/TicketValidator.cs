using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTicket
{
    public interface ITicketValidator : IServiceScoped
    {
        Task<bool> Create(Ticket Ticket);
        Task<bool> Update(Ticket Ticket);
        Task<bool> Delete(Ticket Ticket);
        Task<bool> BulkDelete(List<Ticket> Tickets);
        Task<bool> Import(List<Ticket> Tickets);
    }

    public class TicketValidator : ITicketValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            CustomerEmpty,
            TicketIssueLevelEmpty,
            TicketPriorityEmpty,
            TicketSourceEmpty,
            ContentEmpty,
            CustomerTypeEmpty,
            TicketResolveTypeEmpty,
            ResolveContentEmpty,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Ticket Ticket)
        {
            TicketFilter TicketFilter = new TicketFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Ticket.Id },
                Selects = TicketSelect.Id
            };

            int count = await UOW.TicketRepository.Count(TicketFilter);
            if (count == 0)
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateCustomer(Ticket Ticket)
        {
            if (Ticket.Customer == null)
            {
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.Customer), ErrorCode.CustomerEmpty);
            }
            return Ticket.IsValidated;
        }

        public async Task<bool> ValidateTicketIssueLevel(Ticket Ticket)
        {
            if (Ticket.TicketIssueLevel == null)
            {
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.TicketIssueLevel), ErrorCode.TicketIssueLevelEmpty);
            }
            return Ticket.IsValidated;
        }

        public async Task<bool> ValidateTicketPriority(Ticket Ticket)
        {
            if (Ticket.TicketPriority == null)
            {
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.TicketPriority), ErrorCode.TicketPriorityEmpty);
            }
            return Ticket.IsValidated;
        }

        public async Task<bool> ValidateTicketSource(Ticket Ticket)
        {
            if (Ticket.TicketSource == null)
            {
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.TicketSource), ErrorCode.TicketSourceEmpty);
            }
            return Ticket.IsValidated;
        }
        public async Task<bool> ValidateCustomerType(Ticket Ticket)
        {
            if (Ticket.CustomerType == null)
            {
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.CustomerType), ErrorCode.CustomerTypeEmpty);
            }
            return Ticket.IsValidated;
        }

        public async Task<bool> ValidateContent(Ticket Ticket)
        {
            if (string.IsNullOrWhiteSpace(Ticket.Content))
            {
                Ticket.AddError(nameof(TicketValidator), nameof(Ticket.Content), ErrorCode.ContentEmpty);
            }
            return Ticket.IsValidated;
        }

        private async Task<bool> ValidateResolve(Ticket Ticket)
        {
            if (Ticket.IsClose.HasValue && Ticket.IsClose.Value)
            {
                if(Ticket.TicketResolveTypeId.HasValue == false)
                {
                    Ticket.AddError(nameof(TicketValidator), nameof(Ticket.TicketResolveType), ErrorCode.TicketResolveTypeEmpty);
                }

                if (string.IsNullOrWhiteSpace(Ticket.ResolveContent))
                {
                    Ticket.AddError(nameof(TicketValidator), nameof(Ticket.ResolveContent), ErrorCode.ResolveContentEmpty);
                }
            }
            return Ticket.IsValidated;
        }

        public async Task<bool>Create(Ticket Ticket)
        {
            await ValidateCustomer(Ticket);
            await ValidateTicketIssueLevel(Ticket);
            await ValidateTicketPriority(Ticket);
            await ValidateTicketSource(Ticket);
            await ValidateContent(Ticket);
            await ValidateCustomerType(Ticket);
            return Ticket.IsValidated;
        }

        public async Task<bool> Update(Ticket Ticket)
        {
            if (await ValidateId(Ticket))
            {
                await ValidateResolve(Ticket);
            }
            return Ticket.IsValidated;
        }

        public async Task<bool> Delete(Ticket Ticket)
        {
            if (await ValidateId(Ticket))
            {
            }
            return Ticket.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Ticket> Tickets)
        {
            foreach (Ticket Ticket in Tickets)
            {
                await Delete(Ticket);
            }
            return Tickets.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Ticket> Tickets)
        {
            return true;
        }
    }
}
