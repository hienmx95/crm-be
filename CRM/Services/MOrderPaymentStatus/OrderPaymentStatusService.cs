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

namespace CRM.Services.MOrderPaymentStatus
{
    public interface IOrderPaymentStatusService :  IServiceScoped
    {
        Task<int> Count(OrderPaymentStatusFilter OrderPaymentStatusFilter);
        Task<List<OrderPaymentStatus>> List(OrderPaymentStatusFilter OrderPaymentStatusFilter);
        Task<OrderPaymentStatus> Get(long Id);
    }

    public class OrderPaymentStatusService : BaseService, IOrderPaymentStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public OrderPaymentStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(OrderPaymentStatusFilter OrderPaymentStatusFilter)
        {
            try
            {
                int result = await UOW.OrderPaymentStatusRepository.Count(OrderPaymentStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OrderPaymentStatusService));
            }
            return 0;
        }

        public async Task<List<OrderPaymentStatus>> List(OrderPaymentStatusFilter OrderPaymentStatusFilter)
        {
            try
            {
                List<OrderPaymentStatus> OrderPaymentStatuss = await UOW.OrderPaymentStatusRepository.List(OrderPaymentStatusFilter);
                return OrderPaymentStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OrderPaymentStatusService));
            }
            return null;
        }
        
        public async Task<OrderPaymentStatus> Get(long Id)
        {
            OrderPaymentStatus OrderPaymentStatus = await UOW.OrderPaymentStatusRepository.Get(Id);
            if (OrderPaymentStatus == null)
                return null;
            return OrderPaymentStatus;
        }
    }
}
