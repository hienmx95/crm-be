using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using Microsoft.AspNetCore.Mvc;
using CRM.Entities;

namespace CRM.Rpc.reports.staff_report
{
    public partial class StaffReportController : RpcController
    {
        [Route(StaffReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<StaffReport_AppUserDTO>>> FilterListAppUser([FromBody] StaffReport_AppUserFilterDTO StaffReport_AppUserFilterDTO)
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
            AppUserFilter.Id = StaffReport_AppUserFilterDTO.Id;
            AppUserFilter.Username = StaffReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = StaffReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = StaffReport_AppUserFilterDTO.Address;
            AppUserFilter.Email = StaffReport_AppUserFilterDTO.Email;
            AppUserFilter.Phone = StaffReport_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = StaffReport_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = StaffReport_AppUserFilterDTO.Birthday;
            AppUserFilter.PositionId = StaffReport_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = StaffReport_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = StaffReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.StatusId = StaffReport_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<StaffReport_AppUserDTO> StaffReport_AppUserDTOs = AppUsers
                .Select(x => new StaffReport_AppUserDTO(x)).ToList();
            return StaffReport_AppUserDTOs;
        }
        [Route(StaffReportRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<StaffReport_OrganizationDTO>>> FilterListOrganization([FromBody] StaffReport_OrganizationFilterDTO StaffReport_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 20;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = StaffReport_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = StaffReport_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = StaffReport_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = StaffReport_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = StaffReport_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = StaffReport_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = StaffReport_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = StaffReport_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = StaffReport_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = StaffReport_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<StaffReport_OrganizationDTO> StaffReport_OrganizationDTOs = Organizations
                .Select(x => new StaffReport_OrganizationDTO(x)).ToList();
            return StaffReport_OrganizationDTOs;
        }

    }
}
