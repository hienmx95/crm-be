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

namespace CRM.Services.MConsultingService
{
    public interface IConsultingServiceService :  IServiceScoped
    {
        Task<int> Count(ConsultingServiceFilter ConsultingServiceFilter);
        Task<List<ConsultingService>> List(ConsultingServiceFilter ConsultingServiceFilter);
        Task<ConsultingService> Get(long Id);
    }

    public class ConsultingServiceService : BaseService, IConsultingServiceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ConsultingServiceService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ConsultingServiceFilter ConsultingServiceFilter)
        {
            try
            {
                int result = await UOW.ConsultingServiceRepository.Count(ConsultingServiceFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ConsultingServiceService));
            }
            return 0;
        }

        public async Task<List<ConsultingService>> List(ConsultingServiceFilter ConsultingServiceFilter)
        {
            try
            {
                List<ConsultingService> ConsultingServices = await UOW.ConsultingServiceRepository.List(ConsultingServiceFilter);
                return ConsultingServices;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ConsultingServiceService));
            }
            return null;
        }
        
        public async Task<ConsultingService> Get(long Id)
        {
            ConsultingService ConsultingService = await UOW.ConsultingServiceRepository.Get(Id);
            if (ConsultingService == null)
                return null;
            return ConsultingService;
        }
    }
}
