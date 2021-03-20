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
using CRM.Services.MCustomerFeedback;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerFeedbackType;
using CRM.Services.MStatus;

namespace CRM.Rpc.customer_feedback
{
    public partial class CustomerFeedbackController : RpcController
    {
        [Route(CustomerFeedbackRoute.FilterListCustomer), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_CustomerDTO>>> FilterListCustomer([FromBody] CustomerFeedback_CustomerFilterDTO CustomerFeedback_CustomerFilterDTO)
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
            CustomerFilter.Id = CustomerFeedback_CustomerFilterDTO.Id;
            CustomerFilter.Code = CustomerFeedback_CustomerFilterDTO.Code;
            CustomerFilter.Name = CustomerFeedback_CustomerFilterDTO.Name;
            CustomerFilter.Phone = CustomerFeedback_CustomerFilterDTO.Phone;
            CustomerFilter.Address = CustomerFeedback_CustomerFilterDTO.Address;
            CustomerFilter.NationId = CustomerFeedback_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = CustomerFeedback_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = CustomerFeedback_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = CustomerFeedback_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = CustomerFeedback_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = CustomerFeedback_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = CustomerFeedback_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = CustomerFeedback_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = CustomerFeedback_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = CustomerFeedback_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = CustomerFeedback_CustomerFilterDTO.StatusId;
            CustomerFilter.CompanyId = CustomerFeedback_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = CustomerFeedback_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = CustomerFeedback_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = CustomerFeedback_CustomerFilterDTO.Fax;
            CustomerFilter.Website = CustomerFeedback_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = CustomerFeedback_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = CustomerFeedback_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = CustomerFeedback_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = CustomerFeedback_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = CustomerFeedback_CustomerFilterDTO.Descreption;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<CustomerFeedback_CustomerDTO> CustomerFeedback_CustomerDTOs = Customers
                .Select(x => new CustomerFeedback_CustomerDTO(x)).ToList();
            return CustomerFeedback_CustomerDTOs;
        }
        [Route(CustomerFeedbackRoute.FilterListCustomerFeedbackType), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_CustomerFeedbackTypeDTO>>> FilterListCustomerFeedbackType([FromBody] CustomerFeedback_CustomerFeedbackTypeFilterDTO CustomerFeedback_CustomerFeedbackTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter = new CustomerFeedbackTypeFilter();
            CustomerFeedbackTypeFilter.Skip = 0;
            CustomerFeedbackTypeFilter.Take = int.MaxValue;
            CustomerFeedbackTypeFilter.Take = 20;
            CustomerFeedbackTypeFilter.OrderBy = CustomerFeedbackTypeOrder.Id;
            CustomerFeedbackTypeFilter.OrderType = OrderType.ASC;
            CustomerFeedbackTypeFilter.Selects = CustomerFeedbackTypeSelect.ALL;

            List<CustomerFeedbackType> CustomerFeedbackTypes = await CustomerFeedbackTypeService.List(CustomerFeedbackTypeFilter);
            List<CustomerFeedback_CustomerFeedbackTypeDTO> CustomerFeedback_CustomerFeedbackTypeDTOs = CustomerFeedbackTypes
                .Select(x => new CustomerFeedback_CustomerFeedbackTypeDTO(x)).ToList();
            return CustomerFeedback_CustomerFeedbackTypeDTOs;
        }
        [Route(CustomerFeedbackRoute.FilterListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_StatusDTO>>> FilterListStatus([FromBody] CustomerFeedback_StatusFilterDTO CustomerFeedback_StatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<CustomerFeedback_StatusDTO> CustomerFeedback_StatusDTOs = Statuses
                .Select(x => new CustomerFeedback_StatusDTO(x)).ToList();
            return CustomerFeedback_StatusDTOs;
        }

        [Route(CustomerFeedbackRoute.SingleListCustomer), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_CustomerDTO>>> SingleListCustomer([FromBody] CustomerFeedback_CustomerFilterDTO CustomerFeedback_CustomerFilterDTO)
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
            CustomerFilter.Id = CustomerFeedback_CustomerFilterDTO.Id;
            CustomerFilter.Code = CustomerFeedback_CustomerFilterDTO.Code;
            CustomerFilter.Name = CustomerFeedback_CustomerFilterDTO.Name;
            CustomerFilter.Phone = CustomerFeedback_CustomerFilterDTO.Phone;
            CustomerFilter.Address = CustomerFeedback_CustomerFilterDTO.Address;
            CustomerFilter.NationId = CustomerFeedback_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = CustomerFeedback_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = CustomerFeedback_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = CustomerFeedback_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = CustomerFeedback_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = CustomerFeedback_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = CustomerFeedback_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = CustomerFeedback_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = CustomerFeedback_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = CustomerFeedback_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = CustomerFeedback_CustomerFilterDTO.StatusId;
            CustomerFilter.CompanyId = CustomerFeedback_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = CustomerFeedback_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = CustomerFeedback_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = CustomerFeedback_CustomerFilterDTO.Fax;
            CustomerFilter.Website = CustomerFeedback_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = CustomerFeedback_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = CustomerFeedback_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = CustomerFeedback_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = CustomerFeedback_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = CustomerFeedback_CustomerFilterDTO.Descreption;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<CustomerFeedback_CustomerDTO> CustomerFeedback_CustomerDTOs = Customers
                .Select(x => new CustomerFeedback_CustomerDTO(x)).ToList();
            return CustomerFeedback_CustomerDTOs;
        }
        [Route(CustomerFeedbackRoute.SingleListCustomerFeedbackType), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_CustomerFeedbackTypeDTO>>> SingleListCustomerFeedbackType([FromBody] CustomerFeedback_CustomerFeedbackTypeFilterDTO CustomerFeedback_CustomerFeedbackTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter = new CustomerFeedbackTypeFilter();
            CustomerFeedbackTypeFilter.Skip = 0;
            CustomerFeedbackTypeFilter.Take = int.MaxValue;
            CustomerFeedbackTypeFilter.Take = 20;
            CustomerFeedbackTypeFilter.OrderBy = CustomerFeedbackTypeOrder.Id;
            CustomerFeedbackTypeFilter.OrderType = OrderType.ASC;
            CustomerFeedbackTypeFilter.Selects = CustomerFeedbackTypeSelect.ALL;

            List<CustomerFeedbackType> CustomerFeedbackTypes = await CustomerFeedbackTypeService.List(CustomerFeedbackTypeFilter);
            List<CustomerFeedback_CustomerFeedbackTypeDTO> CustomerFeedback_CustomerFeedbackTypeDTOs = CustomerFeedbackTypes
                .Select(x => new CustomerFeedback_CustomerFeedbackTypeDTO(x)).ToList();
            return CustomerFeedback_CustomerFeedbackTypeDTOs;
        }
        [Route(CustomerFeedbackRoute.SingleListStatus), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_StatusDTO>>> SingleListStatus([FromBody] CustomerFeedback_StatusFilterDTO CustomerFeedback_StatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<CustomerFeedback_StatusDTO> CustomerFeedback_StatusDTOs = Statuses
                .Select(x => new CustomerFeedback_StatusDTO(x)).ToList();
            return CustomerFeedback_StatusDTOs;
        }

    }
}

