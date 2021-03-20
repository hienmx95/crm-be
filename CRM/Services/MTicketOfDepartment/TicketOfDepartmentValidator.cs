using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTicketOfDepartment
{
    public interface ITicketOfDepartmentValidator : IServiceScoped
    {
        Task<bool> Create(TicketOfDepartment TicketOfDepartment);
        Task<bool> Update(TicketOfDepartment TicketOfDepartment);
        Task<bool> Delete(TicketOfDepartment TicketOfDepartment);
        Task<bool> BulkDelete(List<TicketOfDepartment> TicketOfDepartments);
        Task<bool> Import(List<TicketOfDepartment> TicketOfDepartments);
    }

    public class TicketOfDepartmentValidator : ITicketOfDepartmentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketOfDepartmentValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketOfDepartment TicketOfDepartment)
        {
            TicketOfDepartmentFilter TicketOfDepartmentFilter = new TicketOfDepartmentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketOfDepartment.Id },
                Selects = TicketOfDepartmentSelect.Id
            };

            int count = await UOW.TicketOfDepartmentRepository.Count(TicketOfDepartmentFilter);
            if (count == 0)
                TicketOfDepartment.AddError(nameof(TicketOfDepartmentValidator), nameof(TicketOfDepartment.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(TicketOfDepartment TicketOfDepartment)
        {
            return TicketOfDepartment.IsValidated;
        }

        public async Task<bool> Update(TicketOfDepartment TicketOfDepartment)
        {
            if (await ValidateId(TicketOfDepartment))
            {
            }
            return TicketOfDepartment.IsValidated;
        }

        public async Task<bool> Delete(TicketOfDepartment TicketOfDepartment)
        {
            if (await ValidateId(TicketOfDepartment))
            {
            }
            return TicketOfDepartment.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketOfDepartment> TicketOfDepartments)
        {
            foreach (TicketOfDepartment TicketOfDepartment in TicketOfDepartments)
            {
                await Delete(TicketOfDepartment);
            }
            return TicketOfDepartments.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TicketOfDepartment> TicketOfDepartments)
        {
            return true;
        }
    }
}
