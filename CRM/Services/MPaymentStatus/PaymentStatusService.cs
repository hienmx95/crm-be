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

namespace CRM.Services.MPaymentStatus
{
    public interface IPaymentStatusService :  IServiceScoped
    {
        Task<int> Count(PaymentStatusFilter PaymentStatusFilter);
        Task<List<PaymentStatus>> List(PaymentStatusFilter PaymentStatusFilter);
        Task<PaymentStatus> Get(long Id);
    }

    public class PaymentStatusService : BaseService, IPaymentStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public PaymentStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(PaymentStatusFilter PaymentStatusFilter)
        {
            try
            {
                int result = await UOW.PaymentStatusRepository.Count(PaymentStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PaymentStatusService));
            }
            return 0;
        }

        public async Task<List<PaymentStatus>> List(PaymentStatusFilter PaymentStatusFilter)
        {
            try
            {
                List<PaymentStatus> PaymentStatuss = await UOW.PaymentStatusRepository.List(PaymentStatusFilter);
                return PaymentStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(PaymentStatusService));
            }
            return null;
        }
        
        public async Task<PaymentStatus> Get(long Id)
        {
            PaymentStatus PaymentStatus = await UOW.PaymentStatusRepository.Get(Id);
            if (PaymentStatus == null)
                return null;
            return PaymentStatus;
        }
    }
}
