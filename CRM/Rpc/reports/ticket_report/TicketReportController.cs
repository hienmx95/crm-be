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
using System.Dynamic;
using CRM.Entities;
using CRM.Services.MOpportunity;
using CRM.Services.MAppUser;
using CRM.Services.MTicket;
using CRM.Services.MTicketType;
using CRM.Services.MTicketGroup;
using CRM.Services.MTicketStatus;
using CRM.Services.MSLAStatus;
using CRM.Services.MTicketPriority;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerType;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.reports.ticket_report
{
    public partial class TicketReportController : RpcController
    {
        private ITicketService TicketService;
        private ITicketTypeService TicketTypeService;
        private IAppUserService AppUserService;
        private IOrganizationService OrganizationService;
        private ITicketGroupService TicketGroupService;
        private ITicketStatusService TicketStatusService;
        private ISLAStatusService SLAStatusService;
        private ITicketPriorityService TicketPriorityService;
        private ICustomerService CustomerService;
        private ICustomerTypeService CustomerTypeService;
        private ICurrentContext CurrentContext;


        public TicketReportController(
            ITicketService TicketService,
            IAppUserService AppUserService,
            IOrganizationService OrganizationService,
            ITicketTypeService TicketTypeService,
            ITicketGroupService TicketGroupService,
            ITicketStatusService TicketStatusService,
            ISLAStatusService SLAStatusService,
            ITicketPriorityService TicketPriorityService,
            ICustomerService CustomerService,
            ICustomerTypeService CustomerTypeService,
            ICurrentContext CurrentContext,
            IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.TicketService = TicketService;
            this.AppUserService = AppUserService;
            this.OrganizationService = OrganizationService;
            this.TicketTypeService = TicketTypeService;
            this.TicketGroupService = TicketGroupService;
            this.TicketStatusService = TicketStatusService;
            this.SLAStatusService = SLAStatusService;
            this.TicketPriorityService = TicketPriorityService;
            this.CustomerService = CustomerService;
            this.CustomerTypeService = CustomerTypeService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketReportRoute.List), HttpPost]
        public async Task<ActionResult<TicketReport_TicketReportDTO>> List([FromBody] TicketReport_TicketReportFilterDTO TicketReport_TicketReportFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            TicketFilter TicketFilter = ConvertFilterDTOToFilterEntity(TicketReport_TicketReportFilterDTO);
            //Lấy danh sách Tickets
            var Tickets = await TicketService.List(TicketFilter);

            List<TicketReport_TicketDTO> TicketDTOs = Tickets.Select(p => new TicketReport_TicketDTO(p)).ToList();
            TicketReport_TicketReportDTO TicketReport_TicketReportDTO = new TicketReport_TicketReportDTO();
            TicketReport_TicketReportDTO.Tickets = TicketDTOs;
            return TicketReport_TicketReportDTO;
        }
        [Route(TicketReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketReport_TicketReportFilterDTO TicketReport_TicketReportFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            TicketFilter TicketFilter = ConvertFilterDTOToFilterEntity(TicketReport_TicketReportFilterDTO);
            return await TicketService.Count(TicketFilter);
        }

        [Route(TicketReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] TicketReport_TicketReportFilterDTO TicketReport_TicketReportFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);


            //Lấy danh sách Tickets
            TicketFilter TicketFilter = ConvertFilterDTOToFilterEntity(TicketReport_TicketReportFilterDTO);
            var Tickets = await TicketService.List(TicketFilter);

            List<TicketReport_TicketDTO> TicketDTOs = Tickets.Select(p => new TicketReport_TicketDTO(p)).ToList();
            TicketReport_TicketReportDTO TicketReport_TicketReportDTO = new TicketReport_TicketReportDTO();
            var STT = 1;
            foreach (var item in TicketDTOs)
            {
                item.STT = STT;
                STT++;
            }
            TicketReport_TicketReportDTO.Tickets = TicketDTOs;
            var maxDate = TicketDTOs.Max(p => p.CreatedAt);
            var minDate = TicketDTOs.Min(p => p.CreatedAt);

            DateTime Start = TicketReport_TicketReportFilterDTO.CreatedAt?.GreaterEqual == null ?
                minDate :
                TicketReport_TicketReportFilterDTO.CreatedAt.GreaterEqual.Value;

            DateTime End = TicketReport_TicketReportFilterDTO.CreatedAt?.LessEqual == null ?
                maxDate :
                TicketReport_TicketReportFilterDTO.CreatedAt.LessEqual.Value;

            string path = "Templates/TicketReport.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            Data.Start = Start.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.End = End.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.Tickets = TicketReport_TicketReportDTO.Tickets;
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "BaoCaoTicket.xlsx");
        }

        private TicketFilter ConvertFilterDTOToFilterEntity(TicketReport_TicketReportFilterDTO TicketReport_TicketReportFilterDTO)
        {
            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Skip = TicketReport_TicketReportFilterDTO.Skip;
            TicketFilter.Take = TicketReport_TicketReportFilterDTO.Take;
            TicketFilter.OrderType = TicketReport_TicketReportFilterDTO.OrderType;

            TicketFilter.TicketNumber = TicketReport_TicketReportFilterDTO.TicketNumber;
            TicketFilter.TicketTypeId = TicketReport_TicketReportFilterDTO.TicketTypeId;
            TicketFilter.TicketGroupId = TicketReport_TicketReportFilterDTO.TicketGroupId;
            TicketFilter.TicketStatusId = TicketReport_TicketReportFilterDTO.TicketStatusId;
            TicketFilter.SLAStatusId = TicketReport_TicketReportFilterDTO.SLAStatusId;
            TicketFilter.TicketPriorityId = TicketReport_TicketReportFilterDTO.TicketPriorityId;
            TicketFilter.CustomerTypeId = TicketReport_TicketReportFilterDTO.CustomerTypeId;
            TicketFilter.CustomerId = TicketReport_TicketReportFilterDTO.CustomerId;
            TicketFilter.UserId = TicketReport_TicketReportFilterDTO.UserId;
            TicketFilter.CreatedAt = TicketReport_TicketReportFilterDTO.CreatedAt;

            return TicketFilter;
        }


    }
}

