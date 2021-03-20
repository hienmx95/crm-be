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

namespace CRM.Services.MRatingStatus
{
    public interface IRatingStatusService :  IServiceScoped
    {
        Task<int> Count(RatingStatusFilter RatingStatusFilter);
        Task<List<RatingStatus>> List(RatingStatusFilter RatingStatusFilter);
        Task<RatingStatus> Get(long Id);
    }

    public class RatingStatusService : BaseService, IRatingStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public RatingStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(RatingStatusFilter RatingStatusFilter)
        {
            try
            {
                int result = await UOW.RatingStatusRepository.Count(RatingStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RatingStatusService));
            }
            return 0;
        }

        public async Task<List<RatingStatus>> List(RatingStatusFilter RatingStatusFilter)
        {
            try
            {
                List<RatingStatus> RatingStatuss = await UOW.RatingStatusRepository.List(RatingStatusFilter);
                return RatingStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(RatingStatusService));
            }
            return null;
        }
        
        public async Task<RatingStatus> Get(long Id)
        {
            RatingStatus RatingStatus = await UOW.RatingStatusRepository.Get(Id);
            if (RatingStatus == null)
                return null;
            return RatingStatus;
        }
    }
}
