using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MPaymentStatus
{
    public interface IPaymentStatusValidator : IServiceScoped
    {
        Task<bool> Create(PaymentStatus PaymentStatus);
        Task<bool> Update(PaymentStatus PaymentStatus);
        Task<bool> Delete(PaymentStatus PaymentStatus);
        Task<bool> BulkDelete(List<PaymentStatus> PaymentStatuses);
        Task<bool> Import(List<PaymentStatus> PaymentStatuses);
    }

    public class PaymentStatusValidator : IPaymentStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public PaymentStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(PaymentStatus PaymentStatus)
        {
            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = PaymentStatus.Id },
                Selects = PaymentStatusSelect.Id
            };

            int count = await UOW.PaymentStatusRepository.Count(PaymentStatusFilter);
            if (count == 0)
                PaymentStatus.AddError(nameof(PaymentStatusValidator), nameof(PaymentStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(PaymentStatus PaymentStatus)
        {
            return PaymentStatus.IsValidated;
        }

        public async Task<bool> Update(PaymentStatus PaymentStatus)
        {
            if (await ValidateId(PaymentStatus))
            {
            }
            return PaymentStatus.IsValidated;
        }

        public async Task<bool> Delete(PaymentStatus PaymentStatus)
        {
            if (await ValidateId(PaymentStatus))
            {
            }
            return PaymentStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<PaymentStatus> PaymentStatuses)
        {
            foreach (PaymentStatus PaymentStatus in PaymentStatuses)
            {
                await Delete(PaymentStatus);
            }
            return PaymentStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<PaymentStatus> PaymentStatuses)
        {
            return true;
        }
    }
}
