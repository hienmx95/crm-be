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

namespace CRM.Services.MCooperativeAttitude
{
    public interface ICooperativeAttitudeService :  IServiceScoped
    {
        Task<int> Count(CooperativeAttitudeFilter CooperativeAttitudeFilter);
        Task<List<CooperativeAttitude>> List(CooperativeAttitudeFilter CooperativeAttitudeFilter);
        Task<CooperativeAttitude> Get(long Id);
    }

    public class CooperativeAttitudeService : BaseService, ICooperativeAttitudeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CooperativeAttitudeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CooperativeAttitudeFilter CooperativeAttitudeFilter)
        {
            try
            {
                int result = await UOW.CooperativeAttitudeRepository.Count(CooperativeAttitudeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CooperativeAttitudeService));
            }
            return 0;
        }

        public async Task<List<CooperativeAttitude>> List(CooperativeAttitudeFilter CooperativeAttitudeFilter)
        {
            try
            {
                List<CooperativeAttitude> CooperativeAttitudes = await UOW.CooperativeAttitudeRepository.List(CooperativeAttitudeFilter);
                return CooperativeAttitudes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CooperativeAttitudeService));
            }
            return null;
        }
        
        public async Task<CooperativeAttitude> Get(long Id)
        {
            CooperativeAttitude CooperativeAttitude = await UOW.CooperativeAttitudeRepository.Get(Id);
            if (CooperativeAttitude == null)
                return null;
            return CooperativeAttitude;
        }
    }
}
