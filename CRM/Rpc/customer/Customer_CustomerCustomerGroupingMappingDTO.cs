using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerCustomerGroupingMappingDTO : DataDTO
    {
        public long CustomerId { get; set; }
        public long CustomerGroupingId { get; set; }
        public Customer_CustomerGroupingDTO CustomerGrouping { get; set; }   

        public Customer_CustomerCustomerGroupingMappingDTO() {}
        public Customer_CustomerCustomerGroupingMappingDTO(CustomerCustomerGroupingMapping CustomerCustomerGroupingMapping)
        {
            this.CustomerId = CustomerCustomerGroupingMapping.CustomerId;
            this.CustomerGroupingId = CustomerCustomerGroupingMapping.CustomerGroupingId;
            this.CustomerGrouping = CustomerCustomerGroupingMapping.CustomerGrouping == null ? null : new Customer_CustomerGroupingDTO(CustomerCustomerGroupingMapping.CustomerGrouping);
            this.Errors = CustomerCustomerGroupingMapping.Errors;
        }
    }

    public class Customer_CustomerCustomerGroupingMappingFilterDTO : FilterDTO
    {
        
        public IdFilter CustomerId { get; set; }
        
        public IdFilter CustomerGroupingId { get; set; }
        
        public CustomerCustomerGroupingMappingOrder OrderBy { get; set; }
    }
}