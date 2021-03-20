using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using Microsoft.AspNetCore.Mvc;
using CRM.Entities;

namespace CRM.Rpc.reports.customer_report
{
    public partial class CustomerReportController 
    {
        [Route(CustomerReportRoute.FilterListCustomerType), HttpPost]
        public async Task<ActionResult<List<CustomerReport_CustomerTypeDTO>>> FilterListCustomerType([FromBody] CustomerReport_CustomerTypeFilterDTO CustomerReport_CustomerTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter();
            CustomerTypeFilter.Skip = 0;
            CustomerTypeFilter.Take = 20;
            CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
            CustomerTypeFilter.OrderType = OrderType.ASC;
            CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
            CustomerTypeFilter.Id = CustomerReport_CustomerTypeFilterDTO.Id;
            CustomerTypeFilter.Name = CustomerReport_CustomerTypeFilterDTO.Name;
            CustomerTypeFilter.Code = CustomerReport_CustomerTypeFilterDTO.Code;

            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            List<CustomerReport_CustomerTypeDTO> CustomerReport_CustomerTypeDTOs = CustomerTypes
                .Select(x => new CustomerReport_CustomerTypeDTO(x)).ToList();
            return CustomerReport_CustomerTypeDTOs;
        }
    }
}
