using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;

namespace CRM.Services.MRequestState
{
    public interface IRequestStateService :  IServiceScoped
    {
        Task<int> Count(RequestStateFilter RequestStateFilter);
        Task<List<RequestState>> List(RequestStateFilter RequestStateFilter);
    }

    public class RequestStateService : BaseService, IRequestStateService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IRequestStateValidator RequestStateValidator;

        public RequestStateService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IRequestStateValidator RequestStateValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.RequestStateValidator = RequestStateValidator;
        }
        public async Task<int> Count(RequestStateFilter RequestStateFilter)
        {
            try
            {
                int result = await UOW.RequestStateRepository.Count(RequestStateFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RequestStateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RequestStateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<RequestState>> List(RequestStateFilter RequestStateFilter)
        {
            try
            {
                List<RequestState> RequestStates = await UOW.RequestStateRepository.List(RequestStateFilter);
                return RequestStates;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(RequestStateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(RequestStateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
    }
}
