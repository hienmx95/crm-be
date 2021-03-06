using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MRequestState
{
    public interface IRequestStateValidator : IServiceScoped
    {
        Task<bool> Create(RequestState RequestState);
        Task<bool> Update(RequestState RequestState);
        Task<bool> Delete(RequestState RequestState);
        Task<bool> BulkDelete(List<RequestState> RequestStates);
        Task<bool> Import(List<RequestState> RequestStates);
    }

    public class RequestStateValidator : IRequestStateValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public RequestStateValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(RequestState RequestState)
        {
            RequestStateFilter RequestStateFilter = new RequestStateFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = RequestState.Id },
                Selects = RequestStateSelect.Id
            };

            int count = await UOW.RequestStateRepository.Count(RequestStateFilter);
            if (count == 0)
                RequestState.AddError(nameof(RequestStateValidator), nameof(RequestState.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(RequestState RequestState)
        {
            return RequestState.IsValidated;
        }

        public async Task<bool> Update(RequestState RequestState)
        {
            if (await ValidateId(RequestState))
            {
            }
            return RequestState.IsValidated;
        }

        public async Task<bool> Delete(RequestState RequestState)
        {
            if (await ValidateId(RequestState))
            {
            }
            return RequestState.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<RequestState> RequestStates)
        {
            foreach (RequestState RequestState in RequestStates)
            {
                await Delete(RequestState);
            }
            return RequestStates.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<RequestState> RequestStates)
        {
            return true;
        }
    }
}
