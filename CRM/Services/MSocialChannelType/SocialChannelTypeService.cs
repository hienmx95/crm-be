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

namespace CRM.Services.MSocialChannelType
{
    public interface ISocialChannelTypeService :  IServiceScoped
    {
        Task<int> Count(SocialChannelTypeFilter SocialChannelTypeFilter);
        Task<List<SocialChannelType>> List(SocialChannelTypeFilter SocialChannelTypeFilter);
        Task<SocialChannelType> Get(long Id);
    }

    public class SocialChannelTypeService : BaseService, ISocialChannelTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public SocialChannelTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(SocialChannelTypeFilter SocialChannelTypeFilter)
        {
            try
            {
                int result = await UOW.SocialChannelTypeRepository.Count(SocialChannelTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SocialChannelTypeService));
            }
            return 0;
        }

        public async Task<List<SocialChannelType>> List(SocialChannelTypeFilter SocialChannelTypeFilter)
        {
            try
            {
                List<SocialChannelType> SocialChannelTypes = await UOW.SocialChannelTypeRepository.List(SocialChannelTypeFilter);
                return SocialChannelTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(SocialChannelTypeService));
            }
            return null;
        }
        
        public async Task<SocialChannelType> Get(long Id)
        {
            SocialChannelType SocialChannelType = await UOW.SocialChannelTypeRepository.Get(Id);
            if (SocialChannelType == null)
                return null;
            return SocialChannelType;
        }
    }
}
