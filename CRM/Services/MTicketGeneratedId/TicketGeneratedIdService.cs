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

namespace CRM.Services.MTicketGeneratedId
{
    public interface ITicketGeneratedIdService :  IServiceScoped
    {
        Task<int> Count(TicketGeneratedIdFilter TicketGeneratedIdFilter);
        Task<List<TicketGeneratedId>> List(TicketGeneratedIdFilter TicketGeneratedIdFilter);
        Task<TicketGeneratedId> GetNewTicketId();
        Task<TicketGeneratedId> UpdateUsed(long Id);
    }

    public class TicketGeneratedIdService : BaseService, ITicketGeneratedIdService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketGeneratedIdValidator TicketGeneratedIdValidator;

        public TicketGeneratedIdService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketGeneratedIdValidator TicketGeneratedIdValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketGeneratedIdValidator = TicketGeneratedIdValidator;
        }
        public async Task<int> Count(TicketGeneratedIdFilter TicketGeneratedIdFilter)
        {
            try
            {
                int result = await UOW.TicketGeneratedIdRepository.Count(TicketGeneratedIdFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketGeneratedId>> List(TicketGeneratedIdFilter TicketGeneratedIdFilter)
        {
            try
            {
                List<TicketGeneratedId> TicketGeneratedIds = await UOW.TicketGeneratedIdRepository.List(TicketGeneratedIdFilter);
                return TicketGeneratedIds;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketGeneratedId> GetNewTicketId()
        {
            try
            {
                TicketGeneratedId ticketUnused = await UOW.TicketGeneratedIdRepository.GetTicketGeneratedIdUnused();
                if (ticketUnused != null)
                {
                    return ticketUnused;
                }
                TicketGeneratedId TicketGeneratedId = new TicketGeneratedId();

                await UOW.Begin();
                await UOW.TicketGeneratedIdRepository.Create(TicketGeneratedId);
                await UOW.Commit();
                TicketGeneratedId = await UOW.TicketGeneratedIdRepository.Get(TicketGeneratedId.Id);
                await Logging.CreateAuditLog(TicketGeneratedId, new { }, nameof(TicketGeneratedIdService));
                return TicketGeneratedId;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketGeneratedId> UpdateUsed(long Id)
        {
            
            try
            {
                var oldData = await UOW.TicketGeneratedIdRepository.Get(Id);
                if (oldData != null)
                {
                    oldData.Used = true;
                }
                await UOW.Begin();
                await UOW.TicketGeneratedIdRepository.Update(oldData);
                await UOW.Commit();

                var TicketGeneratedId = await UOW.TicketGeneratedIdRepository.Get(Id);
                await Logging.CreateAuditLog(TicketGeneratedId, oldData, nameof(TicketGeneratedIdService));
                return TicketGeneratedId;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGeneratedIdService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
    }
}
