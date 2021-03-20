using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MRepairTicket
{
    public interface IRepairTicketValidator : IServiceScoped
    {
        Task<bool> Create(RepairTicket RepairTicket);
        Task<bool> Update(RepairTicket RepairTicket);
        Task<bool> Delete(RepairTicket RepairTicket);
        Task<bool> BulkDelete(List<RepairTicket> RepairTickets);
        Task<bool> Import(List<RepairTicket> RepairTickets);
    }

    public class RepairTicketValidator : IRepairTicketValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CodeHasSpecialCharacter,
            CodeExisted,
            CodeOverLength,
            CodeEmpty,
            DeviceStateEmpty,
            DeviceStateOverLength,
            OrderCategoryEmpty,
            OrderCategoryNotExisted,
            OrderEmpty,
            CustomerEmpty,
            CustomerNotExisted,
            ItemEmpty,
            DeviceSerialEmpty,
            DeviceSerialOverLength
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public RepairTicketValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(RepairTicket RepairTicket)
        {
            RepairTicketFilter RepairTicketFilter = new RepairTicketFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = RepairTicket.Id },
                Selects = RepairTicketSelect.Id
            };

            int count = await UOW.RepairTicketRepository.Count(RepairTicketFilter);
            if (count == 0)
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateCode(RepairTicket RepairTicket)
        {
            if (string.IsNullOrWhiteSpace(RepairTicket.Code))
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = RepairTicket.Code;
                if (RepairTicket.Code.Contains(" ") || !FilterExtension.ChangeToEnglishChar(Code).Equals(RepairTicket.Code))
                {
                    RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Code), ErrorCode.CodeHasSpecialCharacter);
                }
                else
                {
                    if (RepairTicket.Code.Length > 255)
                    {
                        RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Code), ErrorCode.CodeOverLength);
                    }
                    else
                    {
                        RepairTicketFilter RepairTicketFilter = new RepairTicketFilter
                        {
                            Skip = 0,
                            Take = 10,
                            Id = new IdFilter { NotEqual = RepairTicket.Id },
                            Code = new StringFilter { Equal = RepairTicket.Code },
                            Selects = RepairTicketSelect.Code
                        };

                        int count = await UOW.RepairTicketRepository.Count(RepairTicketFilter);
                        if (count != 0)
                            RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Code), ErrorCode.CodeExisted);
                    }
                }
            }
            return RepairTicket.IsValidated;
        }

        private async Task<bool> ValidateDeviceSerial(RepairTicket RepairTicket)
        {
            if (string.IsNullOrWhiteSpace(RepairTicket.DeviceSerial))
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.DeviceSerial), ErrorCode.DeviceSerialEmpty);
            }
            else
            {
                if (RepairTicket.DeviceSerial.Length > 500)
                {
                    RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.DeviceSerial), ErrorCode.DeviceSerialOverLength);
                }
            }
            return RepairTicket.IsValidated;
        }

        private async Task<bool> ValidateDeviceState(RepairTicket RepairTicket)
        {
            if (string.IsNullOrWhiteSpace(RepairTicket.DeviceState))
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.DeviceState), ErrorCode.DeviceStateEmpty);
            }
            else
            {
                if (RepairTicket.DeviceState.Length > 500)
                {
                    RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.DeviceState), ErrorCode.DeviceStateOverLength);
                }
            }
            return RepairTicket.IsValidated;
        }

        private async Task<bool> ValidateOrderCategory(RepairTicket RepairTicket)
        {
            if (RepairTicket.OrderCategoryId == 0)
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.OrderCategoryId), ErrorCode.OrderCategoryEmpty);
            }
            else
            {
                var OrderCategoryIds = OrderCategoryEnum.OrderCategoryEnumList.Select(x => x.Id).ToList();
                if(!OrderCategoryIds.Contains(RepairTicket.OrderCategoryId))
                    RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.OrderCategoryId), ErrorCode.OrderCategoryNotExisted);
            }

            return RepairTicket.IsValidated;
        }

        private async Task<bool> ValidateOrder(RepairTicket RepairTicket)
        {
            if(RepairTicket.OrderId == 0)
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.OrderId), ErrorCode.OrderEmpty);
            }
            return RepairTicket.IsValidated;
        }

        private async Task<bool> ValidateItem(RepairTicket RepairTicket)
        {
            if (RepairTicket.ItemId == 0)
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Item), ErrorCode.ItemEmpty);
            }
            return RepairTicket.IsValidated;
        }

        private async Task<bool> ValidateCustomer(RepairTicket RepairTicket)
        {
            if(RepairTicket.CustomerId == 0)
            {
                RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Customer), ErrorCode.CustomerEmpty);
            }
            else
            {
                CustomerFilter CustomerFilter = new CustomerFilter
                {
                    Id = new IdFilter { Equal = RepairTicket.CustomerId },
                    StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id }
                };

                var count = await UOW.CustomerRepository.Count(CustomerFilter);
                if(count == 0)
                    RepairTicket.AddError(nameof(RepairTicketValidator), nameof(RepairTicket.Customer), ErrorCode.CustomerNotExisted);
            }
            return RepairTicket.IsValidated;
        }

        public async Task<bool> Create(RepairTicket RepairTicket)
        {
            await ValidateCode(RepairTicket);
            await ValidateDeviceSerial(RepairTicket);
            await ValidateDeviceState(RepairTicket);
            await ValidateOrderCategory(RepairTicket);
            await ValidateOrder(RepairTicket);
            await ValidateItem(RepairTicket);
            await ValidateCustomer(RepairTicket);
            return RepairTicket.IsValidated;
        }

        public async Task<bool> Update(RepairTicket RepairTicket)
        {
            if (await ValidateId(RepairTicket))
            {
                await ValidateCode(RepairTicket);
                await ValidateDeviceSerial(RepairTicket);
                await ValidateDeviceState(RepairTicket);
                await ValidateOrderCategory(RepairTicket);
                await ValidateOrder(RepairTicket);
                await ValidateItem(RepairTicket);
                await ValidateCustomer(RepairTicket);
            }
            return RepairTicket.IsValidated;
        }

        public async Task<bool> Delete(RepairTicket RepairTicket)
        {
            if (await ValidateId(RepairTicket))
            {
            }
            return RepairTicket.IsValidated;
        }

        public async Task<bool> BulkDelete(List<RepairTicket> RepairTickets)
        {
            foreach (RepairTicket RepairTicket in RepairTickets)
            {
                await Delete(RepairTicket);
            }
            return RepairTickets.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<RepairTicket> RepairTickets)
        {
            return true;
        }
    }
}
