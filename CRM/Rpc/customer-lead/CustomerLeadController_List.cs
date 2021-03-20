using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MCustomerLead;
using CRM.Services.MCustomerLeadLevel;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerLeadStatus;
using CRM.Services.MDistrict;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MAppUser;
using CRM.Enums;

namespace CRM.Rpc.customer_lead
{
    public partial class CustomerLeadController : RpcController
    {
        [Route(CustomerLeadRoute.CountActivity), HttpPost]
        public async Task<ActionResult<int>> CountActivity([FromBody] CustomerLead_CustomerLeadActivityFilterDTO CustomerLead_CustomerLeadActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivityFilter CustomerLeadActivityFilter = ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadActivityFilterDTO);
            CustomerLeadActivityFilter = await CustomerLeadActivityService.ToFilter(CustomerLeadActivityFilter);
            int count = await CustomerLeadActivityService.Count(CustomerLeadActivityFilter);
            return count;
        }

        [Route(CustomerLeadRoute.ListActivity), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadActivityDTO>>> ListActivity([FromBody] CustomerLead_CustomerLeadActivityFilterDTO CustomerLead_CustomerLeadActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivityFilter CustomerLeadActivityFilter = ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadActivityFilterDTO);
            CustomerLeadActivityFilter = await CustomerLeadActivityService.ToFilter(CustomerLeadActivityFilter);
            List<CustomerLeadActivity> CustomerLeadActivities = await CustomerLeadActivityService.List(CustomerLeadActivityFilter);
            List<CustomerLead_CustomerLeadActivityDTO> CustomerLead_CustomerLeadActivityDTOs = CustomerLeadActivities
                .Select(c => new CustomerLead_CustomerLeadActivityDTO(c)).ToList();
            return CustomerLead_CustomerLeadActivityDTOs;
        }

        [Route(CustomerLeadRoute.GetActivity), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadActivityDTO>> GetActivity([FromBody] CustomerLead_CustomerLeadActivityDTO CustomerLead_CustomerLeadActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivity CustomerLeadActivity = await CustomerLeadActivityService.Get(CustomerLead_CustomerLeadActivityDTO.Id);
            return new CustomerLead_CustomerLeadActivityDTO(CustomerLeadActivity);
        }

        private CustomerLeadActivityFilter ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadActivityFilterDTO CustomerLead_CustomerLeadActivityFilterDTO)
        {
            CustomerLeadActivityFilter CustomerLeadActivityFilter = new CustomerLeadActivityFilter();
            CustomerLeadActivityFilter.Selects = CustomerLeadActivitySelect.ALL;
            CustomerLeadActivityFilter.Skip = CustomerLead_CustomerLeadActivityFilterDTO.Skip;
            CustomerLeadActivityFilter.Take = CustomerLead_CustomerLeadActivityFilterDTO.Take;
            CustomerLeadActivityFilter.OrderBy = CustomerLead_CustomerLeadActivityFilterDTO.OrderBy;
            CustomerLeadActivityFilter.OrderType = CustomerLead_CustomerLeadActivityFilterDTO.OrderType;

            CustomerLeadActivityFilter.Id = CustomerLead_CustomerLeadActivityFilterDTO.Id;
            CustomerLeadActivityFilter.Title = CustomerLead_CustomerLeadActivityFilterDTO.Title;
            CustomerLeadActivityFilter.FromDate = CustomerLead_CustomerLeadActivityFilterDTO.FromDate;
            CustomerLeadActivityFilter.ToDate = CustomerLead_CustomerLeadActivityFilterDTO.ToDate;
            CustomerLeadActivityFilter.ActivityTypeId = CustomerLead_CustomerLeadActivityFilterDTO.ActivityTypeId;
            CustomerLeadActivityFilter.ActivityPriorityId = CustomerLead_CustomerLeadActivityFilterDTO.ActivityPriorityId;
            CustomerLeadActivityFilter.Description = CustomerLead_CustomerLeadActivityFilterDTO.Description;
            CustomerLeadActivityFilter.Address = CustomerLead_CustomerLeadActivityFilterDTO.Address;
            CustomerLeadActivityFilter.CustomerLeadId = CustomerLead_CustomerLeadActivityFilterDTO.CustomerLeadId;
            CustomerLeadActivityFilter.AppUserId = CustomerLead_CustomerLeadActivityFilterDTO.AppUserId;
            CustomerLeadActivityFilter.ActivityStatusId = CustomerLead_CustomerLeadActivityFilterDTO.ActivityStatusId;
            CustomerLeadActivityFilter.CreatedAt = CustomerLead_CustomerLeadActivityFilterDTO.CreatedAt;
            CustomerLeadActivityFilter.UpdatedAt = CustomerLead_CustomerLeadActivityFilterDTO.UpdatedAt;
            return CustomerLeadActivityFilter;
        }

        [Route(CustomerLeadRoute.CountCallLog), HttpPost]
        public async Task<ActionResult<int>> CountCallLog([FromBody] CustomerLead_CallLogFilterDTO CustomerLead_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertCallLog(CustomerLead_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            int count = await CallLogService.Count(CallLogFilter);
            return count;
        }

        [Route(CustomerLeadRoute.ListCallLog), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CallLogDTO>>> ListCallLog([FromBody] CustomerLead_CallLogFilterDTO CustomerLead_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertCallLog(CustomerLead_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<CustomerLead_CallLogDTO> CustomerLead_CallLogDTOs = CallLogs
                .Select(c => new CustomerLead_CallLogDTO(c)).ToList();
            return CustomerLead_CallLogDTOs;
        }

        [Route(CustomerLeadRoute.GetCallLog), HttpPost]
        public async Task<ActionResult<CustomerLead_CallLogDTO>> GetCallLog([FromBody] CustomerLead_CallLogDTO CustomerLead_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = await CallLogService.Get(CustomerLead_CallLogDTO.Id);
            return new CustomerLead_CallLogDTO(CallLog);
        }

        private CallLogFilter ConvertCallLog(CustomerLead_CallLogFilterDTO CustomerLead_CallLogFilterDTO)
        {
            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Skip = CustomerLead_CallLogFilterDTO.Skip;
            CallLogFilter.Take = CustomerLead_CallLogFilterDTO.Take;
            CallLogFilter.OrderBy = CustomerLead_CallLogFilterDTO.OrderBy;
            CallLogFilter.OrderType = CustomerLead_CallLogFilterDTO.OrderType;

            CallLogFilter.Id = CustomerLead_CallLogFilterDTO.Id;
            CallLogFilter.EntityReferenceId = CustomerLead_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.CallTypeId = CustomerLead_CallLogFilterDTO.CallTypeId;
            CallLogFilter.CallEmotionId = CustomerLead_CallLogFilterDTO.CallEmotionId;
            CallLogFilter.AppUserId = CustomerLead_CallLogFilterDTO.AppUserId;
            CallLogFilter.Title = CustomerLead_CallLogFilterDTO.Title;
            CallLogFilter.Content = CustomerLead_CallLogFilterDTO.Content;
            CallLogFilter.Phone = CustomerLead_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = CustomerLead_CallLogFilterDTO.CallTime;
            CallLogFilter.CreatedAt = CustomerLead_CallLogFilterDTO.CreatedAt;
            CallLogFilter.UpdatedAt = CustomerLead_CallLogFilterDTO.UpdatedAt;
            return CallLogFilter;
        }

        #region Item
        [Route(CustomerLeadRoute.CountItem), HttpPost]
        public async Task<ActionResult<long>> CountItem([FromBody] CustomerLead_ItemFilterDTO CustomerLead_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Id = CustomerLead_ItemFilterDTO.Id;
            ItemFilter.ProductId = CustomerLead_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = CustomerLead_ItemFilterDTO.ProductTypeId;
            ItemFilter.ProductGroupingId = CustomerLead_ItemFilterDTO.ProductGroupingId;
            ItemFilter.Code = CustomerLead_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerLead_ItemFilterDTO.Name;
            ItemFilter.ScanCode = CustomerLead_ItemFilterDTO.ScanCode;
            ItemFilter.OtherName = CustomerLead_ItemFilterDTO.OtherName;
            ItemFilter.SalePrice = CustomerLead_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = CustomerLead_ItemFilterDTO.RetailPrice;
            ItemFilter.Search = CustomerLead_ItemFilterDTO.Search;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            return await ItemService.Count(ItemFilter);
        }

        [Route(CustomerLeadRoute.ListItem), HttpPost]
        public async Task<ActionResult<List<CustomerLead_ItemDTO>>> ListItem([FromBody] CustomerLead_ItemFilterDTO CustomerLead_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = CustomerLead_ItemFilterDTO.Skip;
            ItemFilter.Take = CustomerLead_ItemFilterDTO.Take;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = CustomerLead_ItemFilterDTO.Id;
            ItemFilter.ProductId = CustomerLead_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = CustomerLead_ItemFilterDTO.ProductTypeId;
            ItemFilter.ProductGroupingId = CustomerLead_ItemFilterDTO.ProductGroupingId;
            ItemFilter.Code = CustomerLead_ItemFilterDTO.Code;
            ItemFilter.Name = CustomerLead_ItemFilterDTO.Name;
            ItemFilter.ScanCode = CustomerLead_ItemFilterDTO.ScanCode;
            ItemFilter.OtherName = CustomerLead_ItemFilterDTO.OtherName;
            ItemFilter.SalePrice = CustomerLead_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = CustomerLead_ItemFilterDTO.RetailPrice;
            ItemFilter.Search = CustomerLead_ItemFilterDTO.Search;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<CustomerLead_ItemDTO> CustomerLead_ItemDTOs = Items
                .Select(x => new CustomerLead_ItemDTO(x)).ToList();
            return CustomerLead_ItemDTOs;
        }
        #endregion

        #region Email
        [Route(CustomerLeadRoute.GetCustomerLeadEmail), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadEmailDTO>> GetCustomerLeadEmail([FromBody] CustomerLead_CustomerLeadEmailDTO CustomerLead_CustomerLeadEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadEmail CustomerLeadEmail = await CustomerLeadEmailService.Get(CustomerLead_CustomerLeadEmailDTO.Id);
            return new CustomerLead_CustomerLeadEmailDTO(CustomerLeadEmail);
        }

        [Route(CustomerLeadRoute.CountCustomerLeadEmail), HttpPost]
        public async Task<ActionResult<long>> CountCustomerLeadEmail([FromBody] CustomerLead_CustomerLeadEmailFilterDTO CustomerLead_CustomerLeadEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerLeadEmailFilter CustomerLeadEmailFilter = new CustomerLeadEmailFilter();
            CustomerLeadEmailFilter.Id = CustomerLead_CustomerLeadEmailFilterDTO.Id;
            CustomerLeadEmailFilter.Title = CustomerLead_CustomerLeadEmailFilterDTO.Title;
            CustomerLeadEmailFilter.Content = CustomerLead_CustomerLeadEmailFilterDTO.Content;
            CustomerLeadEmailFilter.CreatorId = CustomerLead_CustomerLeadEmailFilterDTO.CreatorId;
            CustomerLeadEmailFilter.CreatedAt = CustomerLead_CustomerLeadEmailFilterDTO.CreatedAt;
            CustomerLeadEmailFilter.CustomerLeadId = CustomerLead_CustomerLeadEmailFilterDTO.CustomerLeadId;
            CustomerLeadEmailFilter.EmailStatusId = CustomerLead_CustomerLeadEmailFilterDTO.EmailStatusId;
            CustomerLeadEmailFilter.Reciepient = CustomerLead_CustomerLeadEmailFilterDTO.Reciepient;

            return await CustomerLeadEmailService.Count(CustomerLeadEmailFilter);
        }

        [Route(CustomerLeadRoute.ListCustomerLeadEmail), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadEmailDTO>>> ListCustomerLeadEmail([FromBody] CustomerLead_CustomerLeadEmailFilterDTO CustomerLead_CustomerLeadEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerLeadEmailFilter CustomerLeadEmailFilter = new CustomerLeadEmailFilter();
            CustomerLeadEmailFilter.Skip = CustomerLead_CustomerLeadEmailFilterDTO.Skip;
            CustomerLeadEmailFilter.Take = CustomerLead_CustomerLeadEmailFilterDTO.Take;
            CustomerLeadEmailFilter.OrderBy = CustomerLeadEmailOrder.Id;
            CustomerLeadEmailFilter.OrderType = OrderType.ASC;
            CustomerLeadEmailFilter.Selects = CustomerLeadEmailSelect.ALL;
            CustomerLeadEmailFilter.Id = CustomerLead_CustomerLeadEmailFilterDTO.Id;
            CustomerLeadEmailFilter.Title = CustomerLead_CustomerLeadEmailFilterDTO.Title;
            CustomerLeadEmailFilter.Content = CustomerLead_CustomerLeadEmailFilterDTO.Content;
            CustomerLeadEmailFilter.CreatorId = CustomerLead_CustomerLeadEmailFilterDTO.CreatorId;
            CustomerLeadEmailFilter.CreatedAt = CustomerLead_CustomerLeadEmailFilterDTO.CreatedAt;
            CustomerLeadEmailFilter.CustomerLeadId = CustomerLead_CustomerLeadEmailFilterDTO.CustomerLeadId;
            CustomerLeadEmailFilter.EmailStatusId = CustomerLead_CustomerLeadEmailFilterDTO.EmailStatusId;
            CustomerLeadEmailFilter.Reciepient = CustomerLead_CustomerLeadEmailFilterDTO.Reciepient;

            List<CustomerLeadEmail> CustomerLeadEmails = await CustomerLeadEmailService.List(CustomerLeadEmailFilter);
            List<CustomerLead_CustomerLeadEmailDTO> CustomerLead_CustomerLeadEmailDTOs = CustomerLeadEmails
                .Select(x => new CustomerLead_CustomerLeadEmailDTO(x)).ToList();
            return CustomerLead_CustomerLeadEmailDTOs;
        }
        #endregion
    }
}


