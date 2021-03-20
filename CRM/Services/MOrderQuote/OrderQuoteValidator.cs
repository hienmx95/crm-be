using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MOrderQuote
{
    public interface IOrderQuoteValidator : IServiceScoped
    {
        Task<bool> Create(OrderQuote OrderQuote);
        Task<bool> Update(OrderQuote OrderQuote);
        Task<bool> Delete(OrderQuote OrderQuote);
        Task<bool> BulkDelete(List<OrderQuote> OrderQuotes);
        Task<bool> Import(List<OrderQuote> OrderQuotes);
    }

    public class OrderQuoteValidator : IOrderQuoteValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            SubjectEmpty,
            SubjectOverLength,
            CompanyEmpty,
            ContactEmpty,
            UserEmpty,
            OrderQuoteStatusEmpty,
            QuotestageEmpty,
            QuotestageWrong,
            ContentEmpty,
            UnitOfMeasureEmpty,
            QuantityEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OrderQuoteValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OrderQuote OrderQuote)
        {
            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OrderQuote.Id },
                Selects = OrderQuoteSelect.Id
            };

            int count = await UOW.OrderQuoteRepository.Count(OrderQuoteFilter);
            if (count == 0)
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateSubject(OrderQuote OrderQuote)
        {
            if (string.IsNullOrWhiteSpace(OrderQuote.Subject))
            {
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.Subject), ErrorCode.SubjectEmpty);
            }
            else if (OrderQuote.Subject.Length > 255)
            {
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.Subject), ErrorCode.SubjectOverLength);
            }
            return OrderQuote.IsValidated;
        }

        public async Task<bool> ValidateCompanyOrContact(OrderQuote OrderQuote)
        {
            if (OrderQuote.Company == null && OrderQuote.Contact == null)
            {
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.Company), ErrorCode.CompanyEmpty);
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.Contact), ErrorCode.ContactEmpty);
            }
            return OrderQuote.IsValidated;
        }

        public async Task<bool> ValidateUser(OrderQuote OrderQuote)
        {
            if (OrderQuote.AppUser == null)
            {
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.AppUser), ErrorCode.UserEmpty);
            }
            return OrderQuote.IsValidated;
        }

        public async Task<bool> ValidateOrderQuoteStatus(OrderQuote OrderQuote)
        {
            if (OrderQuote.OrderQuoteStatus == null)
            {
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.OrderQuoteStatus), ErrorCode.OrderQuoteStatusEmpty);
            }
            return OrderQuote.IsValidated;
        }

        private async Task<bool> ValidateQuotestage(OrderQuote OrderQuote)
        {
            

            return OrderQuote.IsValidated;
        }

        private async Task<bool> ValidateContent(OrderQuote OrderQuote)
        {
            if (OrderQuote.OrderQuoteContents != null && OrderQuote.OrderQuoteContents.Any())
            {
                foreach (var OrderQuoteContent in OrderQuote.OrderQuoteContents)
                {
                    if (OrderQuoteContent.UnitOfMeasureId == 0)
                        OrderQuoteContent.AddError(nameof(OrderQuoteValidator), nameof(OrderQuoteContent.UnitOfMeasure), ErrorCode.UnitOfMeasureEmpty);
                    else
                    {
                        if (OrderQuoteContent.Quantity <= 0)
                            OrderQuoteContent.AddError(nameof(OrderQuoteValidator), nameof(OrderQuoteContent.Quantity), ErrorCode.QuantityEmpty);
                    }

                }
            }
            else
            {
                
                OrderQuote.AddError(nameof(OrderQuoteValidator), nameof(OrderQuote.Id), ErrorCode.ContentEmpty);
                
            }

            return OrderQuote.IsValidated;
        }

        public async Task<bool>Create(OrderQuote OrderQuote)
        {
            await ValidateSubject(OrderQuote);
            await ValidateUser(OrderQuote);
            await ValidateCompanyOrContact(OrderQuote);
            await ValidateQuotestage(OrderQuote);
            await ValidateContent(OrderQuote);
            return OrderQuote.IsValidated;
        }

        public async Task<bool> Update(OrderQuote OrderQuote)
        {
            if (await ValidateId(OrderQuote))
            {
                await ValidateSubject(OrderQuote);
                await ValidateUser(OrderQuote);
                await ValidateOrderQuoteStatus(OrderQuote);
                await ValidateCompanyOrContact(OrderQuote);
                await ValidateQuotestage(OrderQuote);
                await ValidateContent(OrderQuote);
            }
            return OrderQuote.IsValidated;
        }

        public async Task<bool> Delete(OrderQuote OrderQuote)
        {
            if (await ValidateId(OrderQuote))
            {
            }
            return OrderQuote.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OrderQuote> OrderQuotes)
        {
            foreach (OrderQuote OrderQuote in OrderQuotes)
            {
                await Delete(OrderQuote);
            }
            return OrderQuotes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OrderQuote> OrderQuotes)
        {
            return true;
        }
    }
}
