using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MOrderPaymentStatus
{
    public interface IOrderPaymentStatusValidator : IServiceScoped
    {
        Task<bool> Create(OrderPaymentStatus OrderPaymentStatus);
        Task<bool> Update(OrderPaymentStatus OrderPaymentStatus);
        Task<bool> Delete(OrderPaymentStatus OrderPaymentStatus);
        Task<bool> BulkDelete(List<OrderPaymentStatus> OrderPaymentStatuses);
        Task<bool> Import(List<OrderPaymentStatus> OrderPaymentStatuses);
    }

    public class OrderPaymentStatusValidator : IOrderPaymentStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OrderPaymentStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OrderPaymentStatus OrderPaymentStatus)
        {
            OrderPaymentStatusFilter OrderPaymentStatusFilter = new OrderPaymentStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OrderPaymentStatus.Id },
                Selects = OrderPaymentStatusSelect.Id
            };

            int count = await UOW.OrderPaymentStatusRepository.Count(OrderPaymentStatusFilter);
            if (count == 0)
                OrderPaymentStatus.AddError(nameof(OrderPaymentStatusValidator), nameof(OrderPaymentStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(OrderPaymentStatus OrderPaymentStatus)
        {
            return OrderPaymentStatus.IsValidated;
        }

        public async Task<bool> Update(OrderPaymentStatus OrderPaymentStatus)
        {
            if (await ValidateId(OrderPaymentStatus))
            {
            }
            return OrderPaymentStatus.IsValidated;
        }

        public async Task<bool> Delete(OrderPaymentStatus OrderPaymentStatus)
        {
            if (await ValidateId(OrderPaymentStatus))
            {
            }
            return OrderPaymentStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OrderPaymentStatus> OrderPaymentStatuses)
        {
            foreach (OrderPaymentStatus OrderPaymentStatus in OrderPaymentStatuses)
            {
                await Delete(OrderPaymentStatus);
            }
            return OrderPaymentStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OrderPaymentStatus> OrderPaymentStatuses)
        {
            return true;
        }
    }
}
