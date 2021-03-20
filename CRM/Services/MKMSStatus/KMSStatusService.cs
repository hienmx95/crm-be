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
using CRM.Enums;

namespace CRM.Services.MKMSStatus
{
    public interface IKMSStatusService :  IServiceScoped
    {
        Task<int> Count(KMSStatusFilter KMSStatusFilter);
        Task<List<KMSStatus>> List(KMSStatusFilter KMSStatusFilter);
        Task<KMSStatus> Get(long Id);
    }

    public class KMSStatusService : BaseService, IKMSStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public KMSStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(KMSStatusFilter KMSStatusFilter)
        {
            try
            {
                int result = await UOW.KMSStatusRepository.Count(KMSStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KMSStatusService));
            }
            return 0;
        }

        public async Task<List<KMSStatus>> List(KMSStatusFilter KMSStatusFilter)
        {
            try
            {
                List<KMSStatus> KMSStatuss = await UOW.KMSStatusRepository.List(KMSStatusFilter);
                return KMSStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(KMSStatusService));
            }
            return null;
        }
        
        public async Task<KMSStatus> Get(long Id)
        {
            KMSStatus KMSStatus = await UOW.KMSStatusRepository.Get(Id);
            if (KMSStatus == null)
                return null;
            return KMSStatus;
        }
    }
}
