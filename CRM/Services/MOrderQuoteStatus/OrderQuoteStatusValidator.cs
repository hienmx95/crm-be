using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MOrderQuoteStatus
{
    public interface IOrderQuoteStatusValidator : IServiceScoped
    {
        Task<bool> Create(OrderQuoteStatus OrderQuoteStatus);
        Task<bool> Update(OrderQuoteStatus OrderQuoteStatus);
        Task<bool> Delete(OrderQuoteStatus OrderQuoteStatus);
        Task<bool> BulkDelete(List<OrderQuoteStatus> OrderQuoteStatuses);
        Task<bool> Import(List<OrderQuoteStatus> OrderQuoteStatuses);
    }

    public class OrderQuoteStatusValidator : IOrderQuoteStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OrderQuoteStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OrderQuoteStatus OrderQuoteStatus)
        {
            OrderQuoteStatusFilter OrderQuoteStatusFilter = new OrderQuoteStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OrderQuoteStatus.Id },
                Selects = OrderQuoteStatusSelect.Id
            };

            int count = await UOW.OrderQuoteStatusRepository.Count(OrderQuoteStatusFilter);
            if (count == 0)
                OrderQuoteStatus.AddError(nameof(OrderQuoteStatusValidator), nameof(OrderQuoteStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(OrderQuoteStatus OrderQuoteStatus)
        {
            return OrderQuoteStatus.IsValidated;
        }

        public async Task<bool> Update(OrderQuoteStatus OrderQuoteStatus)
        {
            if (await ValidateId(OrderQuoteStatus))
            {
            }
            return OrderQuoteStatus.IsValidated;
        }

        public async Task<bool> Delete(OrderQuoteStatus OrderQuoteStatus)
        {
            if (await ValidateId(OrderQuoteStatus))
            {
            }
            return OrderQuoteStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OrderQuoteStatus> OrderQuoteStatuses)
        {
            foreach (OrderQuoteStatus OrderQuoteStatus in OrderQuoteStatuses)
            {
                await Delete(OrderQuoteStatus);
            }
            return OrderQuoteStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OrderQuoteStatus> OrderQuoteStatuses)
        {
            return true;
        }
    }
}
