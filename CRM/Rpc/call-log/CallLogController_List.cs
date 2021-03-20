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
using CRM.Services.MCallLog;
using CRM.Services.MCallType;
using CRM.Services.MCustomer;
using CRM.Services.MCallEmotion;
using CRM.Enums;

namespace CRM.Rpc.call_log
{
    public partial class CallLogController : RpcController
    {
        [Route(CallLogRoute.FilterListCallType), HttpPost]
        public async Task<ActionResult<List<CallLog_CallTypeDTO>>> FilterListCallType([FromBody] CallLog_CallTypeFilterDTO CallLog_CallTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallTypeFilter CallTypeFilter = new CallTypeFilter();
            CallTypeFilter.Skip = 0;
            CallTypeFilter.Take = 20;
            CallTypeFilter.OrderBy = CallTypeOrder.Id;
            CallTypeFilter.OrderType = OrderType.ASC;
            CallTypeFilter.Selects = CallTypeSelect.ALL;
            CallTypeFilter.Id = CallLog_CallTypeFilterDTO.Id;
            CallTypeFilter.Code = CallLog_CallTypeFilterDTO.Code;
            CallTypeFilter.Name = CallLog_CallTypeFilterDTO.Name;
            CallTypeFilter.ColorCode = CallLog_CallTypeFilterDTO.ColorCode;
            CallTypeFilter.StatusId = CallLog_CallTypeFilterDTO.StatusId;

            List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);
            List<CallLog_CallTypeDTO> CallLog_CallTypeDTOs = CallTypes
                .Select(x => new CallLog_CallTypeDTO(x)).ToList();
            return CallLog_CallTypeDTOs;
        }
        [Route(CallLogRoute.FilterListCallCategory), HttpPost]
        public async Task<ActionResult<List<CallLog_CallCategoryDTO>>> FilterListCallCategory([FromBody] CallLog_CallCategoryFilterDTO CallLog_CallCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallCategoryFilter CallCategoryFilter = new CallCategoryFilter();
            CallCategoryFilter.Skip = 0;
            CallCategoryFilter.Take = int.MaxValue;
            CallCategoryFilter.Take = 20;
            CallCategoryFilter.OrderBy = CallCategoryOrder.Id;
            CallCategoryFilter.OrderType = OrderType.ASC;
            CallCategoryFilter.Selects = CallCategorySelect.ALL;

            List<CallCategory> CallCategories = await CallCategoryService.List(CallCategoryFilter);
            List<CallLog_CallCategoryDTO> CallLog_CallCategoryDTOs = CallCategories
                .Select(x => new CallLog_CallCategoryDTO(x)).ToList();
            return CallLog_CallCategoryDTOs;
        }
        [Route(CallLogRoute.FilterListCallStatus), HttpPost]
        public async Task<ActionResult<List<CallLog_CallStatusDTO>>> FilterListCallStatus([FromBody] CallLog_CallStatusFilterDTO CallLog_CallStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallStatusFilter CallStatusFilter = new CallStatusFilter();
            CallStatusFilter.Skip = 0;
            CallStatusFilter.Take = int.MaxValue;
            CallStatusFilter.Take = 20;
            CallStatusFilter.OrderBy = CallStatusOrder.Id;
            CallStatusFilter.OrderType = OrderType.ASC;
            CallStatusFilter.Selects = CallStatusSelect.ALL;

            List<CallStatus> CallStatuses = await CallStatusService.List(CallStatusFilter);
            List<CallLog_CallStatusDTO> CallLog_CallStatusDTOs = CallStatuses
                .Select(x => new CallLog_CallStatusDTO(x)).ToList();
            return CallLog_CallStatusDTOs;
        }
        [Route(CallLogRoute.FilterListCustomer), HttpPost]
        public async Task<ActionResult<List<CallLog_CustomerDTO>>> FilterListCustomer([FromBody] CallLog_CustomerFilterDTO CallLog_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = CallLog_CustomerFilterDTO.Id;
            CustomerFilter.Code = CallLog_CustomerFilterDTO.Code;
            CustomerFilter.StatusId = CallLog_CustomerFilterDTO.StatusId;
            CustomerFilter.Phone = CallLog_CustomerFilterDTO.Phone;
            CustomerFilter.Address = CallLog_CustomerFilterDTO.Address;
            CustomerFilter.Name = CallLog_CustomerFilterDTO.Name;
            CustomerFilter.Email = CallLog_CustomerFilterDTO.Email;


            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<CallLog_CustomerDTO> CallLog_CustomerDTOs = Customers
                .Select(x => new CallLog_CustomerDTO(x)).ToList();
            return CallLog_CustomerDTOs;
        }

        [Route(CallLogRoute.FilterListCallEmotion), HttpPost]
        public async Task<ActionResult<List<CallLog_CallEmotionDTO>>> FilterListCallEmotion([FromBody] CallLog_CallEmotionFilterDTO CallLog_CallEmotionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallEmotionFilter CallEmotionFilter = new CallEmotionFilter();
            CallEmotionFilter.Skip = 0;
            CallEmotionFilter.Take = 20;
            CallEmotionFilter.OrderBy = CallEmotionOrder.Id;
            CallEmotionFilter.OrderType = OrderType.ASC;
            CallEmotionFilter.Selects = CallEmotionSelect.ALL;
            CallEmotionFilter.Id = CallLog_CallEmotionFilterDTO.Id;
            CallEmotionFilter.Name = CallLog_CallEmotionFilterDTO.Name;
            CallEmotionFilter.Code = CallLog_CallEmotionFilterDTO.Code;
            CallEmotionFilter.StatusId = CallLog_CallEmotionFilterDTO.StatusId;
            CallEmotionFilter.Description = CallLog_CallEmotionFilterDTO.Description;

            List<CallEmotion> CallEmotions = await CallEmotionService.List(CallEmotionFilter);
            List<CallLog_CallEmotionDTO> CallLog_CallEmotionDTOs = CallEmotions
                .Select(x => new CallLog_CallEmotionDTO(x)).ToList();
            return CallLog_CallEmotionDTOs;
        }


        [Route(CallLogRoute.SingleListCallType), HttpPost]
        public async Task<ActionResult<List<CallLog_CallTypeDTO>>> SingleListCallType([FromBody] CallLog_CallTypeFilterDTO CallLog_CallTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallTypeFilter CallTypeFilter = new CallTypeFilter();
            CallTypeFilter.Skip = 0;
            CallTypeFilter.Take = 20;
            CallTypeFilter.OrderBy = CallTypeOrder.Id;
            CallTypeFilter.OrderType = OrderType.ASC;
            CallTypeFilter.Selects = CallTypeSelect.ALL;
            CallTypeFilter.Id = CallLog_CallTypeFilterDTO.Id;
            CallTypeFilter.Code = CallLog_CallTypeFilterDTO.Code;
            CallTypeFilter.Name = CallLog_CallTypeFilterDTO.Name;
            CallTypeFilter.ColorCode = CallLog_CallTypeFilterDTO.ColorCode;
            CallTypeFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);
            List<CallLog_CallTypeDTO> CallLog_CallTypeDTOs = CallTypes
                .Select(x => new CallLog_CallTypeDTO(x)).ToList();
            return CallLog_CallTypeDTOs;
        }
        [Route(CallLogRoute.SingleListCallCategory), HttpPost]
        public async Task<ActionResult<List<CallLog_CallCategoryDTO>>> SingleListCallCategory([FromBody] CallLog_CallCategoryFilterDTO CallLog_CallCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallCategoryFilter CallCategoryFilter = new CallCategoryFilter();
            CallCategoryFilter.Skip = 0;
            CallCategoryFilter.Take = int.MaxValue;
            CallCategoryFilter.Take = 20;
            CallCategoryFilter.OrderBy = CallCategoryOrder.Id;
            CallCategoryFilter.OrderType = OrderType.ASC;
            CallCategoryFilter.Selects = CallCategorySelect.ALL;

            List<CallCategory> CallCategories = await CallCategoryService.List(CallCategoryFilter);
            List<CallLog_CallCategoryDTO> CallLog_CallCategoryDTOs = CallCategories
                .Select(x => new CallLog_CallCategoryDTO(x)).ToList();
            return CallLog_CallCategoryDTOs;
        }
        [Route(CallLogRoute.SingleListCallStatus), HttpPost]
        public async Task<ActionResult<List<CallLog_CallStatusDTO>>> SingleListCallStatus([FromBody] CallLog_CallStatusFilterDTO CallLog_CallStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallStatusFilter CallStatusFilter = new CallStatusFilter();
            CallStatusFilter.Skip = 0;
            CallStatusFilter.Take = int.MaxValue;
            CallStatusFilter.Take = 20;
            CallStatusFilter.OrderBy = CallStatusOrder.Id;
            CallStatusFilter.OrderType = OrderType.ASC;
            CallStatusFilter.Selects = CallStatusSelect.ALL;

            List<CallStatus> CallStatuses = await CallStatusService.List(CallStatusFilter);
            List<CallLog_CallStatusDTO> CallLog_CallStatusDTOs = CallStatuses
                .Select(x => new CallLog_CallStatusDTO(x)).ToList();
            return CallLog_CallStatusDTOs;
        }
        [Route(CallLogRoute.SingleListCustomer), HttpPost]
        public async Task<ActionResult<List<CallLog_CustomerDTO>>> SingleListCustomer([FromBody] CallLog_CustomerFilterDTO CallLog_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = CallLog_CustomerFilterDTO.Id;
            CustomerFilter.Code = CallLog_CustomerFilterDTO.Code;
            CustomerFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CustomerFilter.Phone = CallLog_CustomerFilterDTO.Phone;
            CustomerFilter.Address = CallLog_CustomerFilterDTO.Address;
            CustomerFilter.Name = CallLog_CustomerFilterDTO.Name;
            CustomerFilter.Email = CallLog_CustomerFilterDTO.Email;


            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<CallLog_CustomerDTO> CallLog_CustomerDTOs = Customers
                .Select(x => new CallLog_CustomerDTO(x)).ToList();
            return CallLog_CustomerDTOs;
        }

        [Route(CallLogRoute.SingleListCallEmotion), HttpPost]
        public async Task<ActionResult<List<CallLog_CallEmotionDTO>>> SingleListCallEmotion([FromBody] CallLog_CallEmotionFilterDTO CallLog_CallEmotionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallEmotionFilter CallEmotionFilter = new CallEmotionFilter();
            CallEmotionFilter.Skip = 0;
            CallEmotionFilter.Take = 20;
            CallEmotionFilter.OrderBy = CallEmotionOrder.Id;
            CallEmotionFilter.OrderType = OrderType.ASC;
            CallEmotionFilter.Selects = CallEmotionSelect.ALL;
            CallEmotionFilter.Id = CallLog_CallEmotionFilterDTO.Id;
            CallEmotionFilter.Name = CallLog_CallEmotionFilterDTO.Name;
            CallEmotionFilter.Code = CallLog_CallEmotionFilterDTO.Code;
            CallEmotionFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            CallEmotionFilter.Description = CallLog_CallEmotionFilterDTO.Description;

            List<CallEmotion> CallEmotions = await CallEmotionService.List(CallEmotionFilter);
            List<CallLog_CallEmotionDTO> CallLog_CallEmotionDTOs = CallEmotions
                .Select(x => new CallLog_CallEmotionDTO(x)).ToList();
            return CallLog_CallEmotionDTOs;
        }
        [Route(CallLogRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<CallLog_AppUserDTO>>> SingleListAppUser([FromBody] CallLog_AppUserFilterDTO CallLog_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = CallLog_AppUserFilterDTO.Id;
            AppUserFilter.Username = CallLog_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = CallLog_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = CallLog_AppUserFilterDTO.Address;
            AppUserFilter.Email = CallLog_AppUserFilterDTO.Email;
            AppUserFilter.Phone = CallLog_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = CallLog_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = CallLog_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = CallLog_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = CallLog_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = CallLog_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = CallLog_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = CallLog_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = CallLog_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = CallLog_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<CallLog_AppUserDTO> CallLog_AppUserDTOs = AppUsers
                .Select(x => new CallLog_AppUserDTO(x)).ToList();
            return CallLog_AppUserDTOs;
        }
        [Route(CallLogRoute.SingleListEntityReference), HttpPost]
        public async Task<ActionResult<List<CallLog_EntityReferenceDTO>>> SingleListEntityReference([FromBody] CallLog_EntityReferenceFilterDTO CallLog_EntityReferenceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EntityReferenceFilter EntityReferenceFilter = new EntityReferenceFilter();
            EntityReferenceFilter.Skip = 0;
            EntityReferenceFilter.Take = 20;
            EntityReferenceFilter.OrderBy = EntityReferenceOrder.Id;
            EntityReferenceFilter.OrderType = OrderType.ASC;
            EntityReferenceFilter.Selects = EntityReferenceSelect.ALL;
            EntityReferenceFilter.Id = CallLog_EntityReferenceFilterDTO.Id;
            EntityReferenceFilter.Code = CallLog_EntityReferenceFilterDTO.Code;
            EntityReferenceFilter.Name = CallLog_EntityReferenceFilterDTO.Name;

            List<EntityReference> EntityReferences = await EntityReferenceService.List(EntityReferenceFilter);
            List<CallLog_EntityReferenceDTO> CallLog_EntityReferenceDTOs = EntityReferences
                .Select(x => new CallLog_EntityReferenceDTO(x)).ToList();
            return CallLog_EntityReferenceDTOs;
        }
    }
}

