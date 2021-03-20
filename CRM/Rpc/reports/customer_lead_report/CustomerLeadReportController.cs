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
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerLeadStatus;
using CRM.Services.MCustomerLead;
using CRM.Services.MAppUser;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.reports.customer_lead_report
{
    public partial class CustomerLeadReportController : RpcController
    {
        private IOrganizationService OrganizationService;
        private ICustomerLeadStatusService CustomerLeadStatusService;
        private ICustomerLeadSourceService CustomerLeadSourceService;
        private ICustomerLeadService CustomerLeadService;
        private IAppUserService AppUserService;
        private ICurrentContext CurrentContext;


        public CustomerLeadReportController(
            IOrganizationService OrganizationService,
            ICustomerLeadSourceService CustomerLeadSourceService,
            ICustomerLeadStatusService CustomerLeadStatusService,
            ICustomerLeadService CustomerLeadService,
            IAppUserService AppUserService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.OrganizationService = OrganizationService;
            this.CustomerLeadSourceService = CustomerLeadSourceService;
            this.CustomerLeadStatusService = CustomerLeadStatusService;
            this.CustomerLeadService = CustomerLeadService;
            this.AppUserService = AppUserService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerLeadReportRoute.List), HttpPost]
        public async Task<ActionResult<CustomerLeadReport_CustomerLeadReportDTO>> List([FromBody] CustomerLeadReport_CustomerLeadReportFilterDTO CustomerLeadReport_CustomerLeadReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerLeadFilter CustomerLeadFilter = ConvertFilterDTOToFilterEntity(CustomerLeadReport_CustomerLeadReportFilterDTO); 
            //Lấy danh sách CustomerLead
            var CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);

            List<CustomerLeadReport_CustomerLeadDTO> CustomerLeadDTOs = CustomerLeads.Select(p => new CustomerLeadReport_CustomerLeadDTO(p)).ToList();
            CustomerLeadReport_CustomerLeadReportDTO CustomerLeadReport_CustomerLeadReportDTO = new CustomerLeadReport_CustomerLeadReportDTO();
            CustomerLeadReport_CustomerLeadReportDTO.CustomerLeads = CustomerLeadDTOs;
            return CustomerLeadReport_CustomerLeadReportDTO;
        }
        [Route(CustomerLeadReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerLeadReport_CustomerLeadReportFilterDTO CustomerLeadReport_CustomerLeadReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadFilter CustomerLeadFilter = ConvertFilterDTOToFilterEntity(CustomerLeadReport_CustomerLeadReportFilterDTO);
            return await CustomerLeadService.Count(CustomerLeadFilter);
        }

        [Route(CustomerLeadReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerLeadReport_CustomerLeadReportFilterDTO CustomerLeadReport_CustomerLeadReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            

            CustomerLeadFilter CustomerLeadFilter = ConvertFilterDTOToFilterEntity(CustomerLeadReport_CustomerLeadReportFilterDTO);
            //Lấy danh sách CustomerLead
            var CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);
            List<CustomerLeadReport_CustomerLeadDTO> CustomerLeadDTOs = CustomerLeads.Select(p => new CustomerLeadReport_CustomerLeadDTO(p)).ToList();
            CustomerLeadReport_CustomerLeadReportDTO CustomerLeadReport_CustomerLeadReportDTO = new CustomerLeadReport_CustomerLeadReportDTO();
            var STT = 1;
            foreach (var item in CustomerLeadDTOs)
            {
                item.STT = STT;
                STT++;
            }
            var maxDate = CustomerLeadDTOs.Max(p => p.CreatedAt);
            var minDate = CustomerLeadDTOs.Min(p => p.CreatedAt);
            DateTime Start = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadTime?.GreaterEqual == null ?
                minDate :
                CustomerLeadReport_CustomerLeadReportFilterDTO.LeadTime.GreaterEqual.Value;

            DateTime End = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadTime?.LessEqual == null ?
                maxDate :
                CustomerLeadReport_CustomerLeadReportFilterDTO.LeadTime.LessEqual.Value;

            CustomerLeadReport_CustomerLeadReportDTO.CustomerLeads = CustomerLeadDTOs;

            string path = "Templates/CustomerLeadReport.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            Data.Start = Start.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.End = End.AddHours(CurrentContext.TimeZone).ToString("dd-MM-yyyy");
            Data.CustomerLeads = CustomerLeadReport_CustomerLeadReportDTO.CustomerLeads;
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };

            return File(output.ToArray(), "application/octet-stream", "BaoCaoLead.xlsx");
        }

        private CustomerLeadFilter ConvertFilterDTOToFilterEntity(CustomerLeadReport_CustomerLeadReportFilterDTO CustomerLeadReport_CustomerLeadReportFilterDTO)
        {
            CustomerLeadFilter CustomerLeadFilter = new CustomerLeadFilter();
            CustomerLeadFilter.Selects = CustomerLeadSelect.ALL;
            CustomerLeadFilter.Skip = CustomerLeadReport_CustomerLeadReportFilterDTO.Skip;
            CustomerLeadFilter.Take = CustomerLeadReport_CustomerLeadReportFilterDTO.Take;
            CustomerLeadFilter.OrderType = CustomerLeadReport_CustomerLeadReportFilterDTO.OrderType;

            CustomerLeadFilter.Name = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadName;
            CustomerLeadFilter.Phone = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadPhoneNumber;
            CustomerLeadFilter.Email = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadEmail;
            CustomerLeadFilter.CustomerLeadStatusId = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadStatusId;
            CustomerLeadFilter.CustomerLeadSourceId = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadSourceId;
            CustomerLeadFilter.AppUserId = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadStaffId;
            CustomerLeadFilter.CreatedAt = CustomerLeadReport_CustomerLeadReportFilterDTO.LeadTime;

            return CustomerLeadFilter;
        }


    }
}

