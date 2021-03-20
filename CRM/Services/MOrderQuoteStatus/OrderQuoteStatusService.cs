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

namespace CRM.Services.MOrderQuoteStatus
{
    public interface IOrderQuoteStatusService :  IServiceScoped
    {
        Task<int> Count(OrderQuoteStatusFilter OrderQuoteStatusFilter);
        Task<List<OrderQuoteStatus>> List(OrderQuoteStatusFilter OrderQuoteStatusFilter);
        Task<OrderQuoteStatus> Get(long Id);
    }

    public class OrderQuoteStatusService : BaseService, IOrderQuoteStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public OrderQuoteStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(OrderQuoteStatusFilter OrderQuoteStatusFilter)
        {
            try
            {
                int result = await UOW.OrderQuoteStatusRepository.Count(OrderQuoteStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OrderQuoteStatusService));
            }
            return 0;
        }

        public async Task<List<OrderQuoteStatus>> List(OrderQuoteStatusFilter OrderQuoteStatusFilter)
        {
            try
            {
                List<OrderQuoteStatus> OrderQuoteStatuss = await UOW.OrderQuoteStatusRepository.List(OrderQuoteStatusFilter);
                return OrderQuoteStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OrderQuoteStatusService));
            }
            return null;
        }
        
        public async Task<OrderQuoteStatus> Get(long Id)
        {
            OrderQuoteStatus OrderQuoteStatus = await UOW.OrderQuoteStatusRepository.Get(Id);
            if (OrderQuoteStatus == null)
                return null;
            return OrderQuoteStatus;
        }
    }
}
