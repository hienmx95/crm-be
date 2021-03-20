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

namespace CRM.Services.MEditedPriceStatus
{
    public interface IEditedPriceStatusService :  IServiceScoped
    {
        Task<int> Count(EditedPriceStatusFilter EditedPriceStatusFilter);
        Task<List<EditedPriceStatus>> List(EditedPriceStatusFilter EditedPriceStatusFilter);
        Task<EditedPriceStatus> Get(long Id);
    }

    public class EditedPriceStatusService : BaseService, IEditedPriceStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public EditedPriceStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(EditedPriceStatusFilter EditedPriceStatusFilter)
        {
            try
            {
                int result = await UOW.EditedPriceStatusRepository.Count(EditedPriceStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EditedPriceStatusService));
            }
            return 0;
        }

        public async Task<List<EditedPriceStatus>> List(EditedPriceStatusFilter EditedPriceStatusFilter)
        {
            try
            {
                List<EditedPriceStatus> EditedPriceStatuss = await UOW.EditedPriceStatusRepository.List(EditedPriceStatusFilter);
                return EditedPriceStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(EditedPriceStatusService));
            }
            return null;
        }
        
        public async Task<EditedPriceStatus> Get(long Id)
        {
            EditedPriceStatus EditedPriceStatus = await UOW.EditedPriceStatusRepository.Get(Id);
            if (EditedPriceStatus == null)
                return null;
            return EditedPriceStatus;
        }
    }
}
