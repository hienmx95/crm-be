using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MOrderQuoteContent
{
    public interface IOrderQuoteContentValidator : IServiceScoped
    {
        Task<bool> Create(OrderQuoteContent OrderQuoteContent);
        Task<bool> Update(OrderQuoteContent OrderQuoteContent);
        Task<bool> Delete(OrderQuoteContent OrderQuoteContent);
        Task<bool> BulkDelete(List<OrderQuoteContent> OrderQuoteContents);
        Task<bool> Import(List<OrderQuoteContent> OrderQuoteContents);
    }

    public class OrderQuoteContentValidator : IOrderQuoteContentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OrderQuoteContentValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OrderQuoteContent OrderQuoteContent)
        {
            OrderQuoteContentFilter OrderQuoteContentFilter = new OrderQuoteContentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OrderQuoteContent.Id },
                Selects = OrderQuoteContentSelect.Id
            };

            int count = await UOW.OrderQuoteContentRepository.Count(OrderQuoteContentFilter);
            if (count == 0)
                OrderQuoteContent.AddError(nameof(OrderQuoteContentValidator), nameof(OrderQuoteContent.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(OrderQuoteContent OrderQuoteContent)
        {
            return OrderQuoteContent.IsValidated;
        }

        public async Task<bool> Update(OrderQuoteContent OrderQuoteContent)
        {
            if (await ValidateId(OrderQuoteContent))
            {
            }
            return OrderQuoteContent.IsValidated;
        }

        public async Task<bool> Delete(OrderQuoteContent OrderQuoteContent)
        {
            if (await ValidateId(OrderQuoteContent))
            {
            }
            return OrderQuoteContent.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OrderQuoteContent> OrderQuoteContents)
        {
            foreach (OrderQuoteContent OrderQuoteContent in OrderQuoteContents)
            {
                await Delete(OrderQuoteContent);
            }
            return OrderQuoteContents.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OrderQuoteContent> OrderQuoteContents)
        {
            return true;
        }
    }
}
