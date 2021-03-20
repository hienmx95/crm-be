using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerSalesOrderDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CustomerTypeId { get; set; }
        public long CustomerId { get; set; }
        public long? OpportunityId { get; set; }
        public long? ContractId { get; set; }
        public long? OrderPaymentStatusId { get; set; }
        public long? RequestStateId { get; set; }
        public long EditedPriceStatusId { get; set; }
        public string ShippingName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long SalesEmployeeId { get; set; }
        public string Note { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public long? InvoiceWardId { get; set; }
        public string InvoiceZIPCode { get; set; }
        public string DeliveryAddress { get; set; }
        public long? DeliveryNationId { get; set; }
        public long? DeliveryProvinceId { get; set; }
        public long? DeliveryDistrictId { get; set; }
        public long? DeliveryWardId { get; set; }
        public string DeliveryZIPCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal TotalTaxOther { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public Guid RowId { get; set; }
        public Customer_ContractDTO Contract { get; set; }
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public Customer_CustomerTypeDTO CustomerType { get; set; }
        public Customer_DistrictDTO DeliveryDistrict { get; set; }
        public Customer_NationDTO DeliveryNation { get; set; }
        public Customer_ProvinceDTO DeliveryProvince { get; set; }
        public Customer_WardDTO DeliveryWard { get; set; }
        public Customer_EditedPriceStatusDTO EditedPriceStatus { get; set; }
        public Customer_DistrictDTO InvoiceDistrict { get; set; }
        public Customer_NationDTO InvoiceNation { get; set; }
        public Customer_ProvinceDTO InvoiceProvince { get; set; }
        public Customer_WardDTO InvoiceWard { get; set; }
        public Customer_OpportunityDTO Opportunity { get; set; }
        public Customer_OrderPaymentStatusDTO OrderPaymentStatus { get; set; }
        public Customer_OrganizationDTO Organization { get; set; }
        public Customer_RequestStateDTO RequestState { get; set; }
        public Customer_AppUserDTO SalesEmployee { get; set; }
        public List<Customer_CustomerSalesOrderContentDTO> CustomerSalesOrderContents { get; set; }
        public List<Customer_CustomerSalesOrderPromotionDTO> CustomerSalesOrderPromotions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_CustomerSalesOrderDTO() {}
        public Customer_CustomerSalesOrderDTO(CustomerSalesOrder CustomerSalesOrder)
        {
            this.Id = CustomerSalesOrder.Id;
            this.Code = CustomerSalesOrder.Code;
            this.CustomerTypeId = CustomerSalesOrder.CustomerTypeId;
            this.CustomerId = CustomerSalesOrder.CustomerId;
            this.OpportunityId = CustomerSalesOrder.OpportunityId;
            this.ContractId = CustomerSalesOrder.ContractId;
            this.OrderPaymentStatusId = CustomerSalesOrder.OrderPaymentStatusId;
            this.RequestStateId = CustomerSalesOrder.RequestStateId;
            this.EditedPriceStatusId = CustomerSalesOrder.EditedPriceStatusId;
            this.ShippingName = CustomerSalesOrder.ShippingName;
            this.OrderDate = CustomerSalesOrder.OrderDate;
            this.DeliveryDate = CustomerSalesOrder.DeliveryDate;
            this.SalesEmployeeId = CustomerSalesOrder.SalesEmployeeId;
            this.Note = CustomerSalesOrder.Note;
            this.InvoiceAddress = CustomerSalesOrder.InvoiceAddress;
            this.InvoiceNationId = CustomerSalesOrder.InvoiceNationId;
            this.InvoiceProvinceId = CustomerSalesOrder.InvoiceProvinceId;
            this.InvoiceDistrictId = CustomerSalesOrder.InvoiceDistrictId;
            this.InvoiceWardId = CustomerSalesOrder.InvoiceWardId;
            this.InvoiceZIPCode = CustomerSalesOrder.InvoiceZIPCode;
            this.DeliveryAddress = CustomerSalesOrder.DeliveryAddress;
            this.DeliveryNationId = CustomerSalesOrder.DeliveryNationId;
            this.DeliveryProvinceId = CustomerSalesOrder.DeliveryProvinceId;
            this.DeliveryDistrictId = CustomerSalesOrder.DeliveryDistrictId;
            this.DeliveryWardId = CustomerSalesOrder.DeliveryWardId;
            this.DeliveryZIPCode = CustomerSalesOrder.DeliveryZIPCode;
            this.SubTotal = CustomerSalesOrder.SubTotal;
            this.GeneralDiscountPercentage = CustomerSalesOrder.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = CustomerSalesOrder.GeneralDiscountAmount;
            this.TotalTaxOther = CustomerSalesOrder.TotalTaxOther;
            this.TotalTax = CustomerSalesOrder.TotalTax;
            this.Total = CustomerSalesOrder.Total;
            this.CreatorId = CustomerSalesOrder.CreatorId;
            this.OrganizationId = CustomerSalesOrder.OrganizationId;
            this.RowId = CustomerSalesOrder.RowId;
            this.Contract = CustomerSalesOrder.Contract == null ? null : new Customer_ContractDTO(CustomerSalesOrder.Contract);
            this.Creator = CustomerSalesOrder.Creator == null ? null : new Customer_AppUserDTO(CustomerSalesOrder.Creator);
            this.Customer = CustomerSalesOrder.Customer == null ? null : new Customer_CustomerDTO(CustomerSalesOrder.Customer);
            this.CustomerType = CustomerSalesOrder.CustomerType == null ? null : new Customer_CustomerTypeDTO(CustomerSalesOrder.CustomerType);
            this.DeliveryDistrict = CustomerSalesOrder.DeliveryDistrict == null ? null : new Customer_DistrictDTO(CustomerSalesOrder.DeliveryDistrict);
            this.DeliveryNation = CustomerSalesOrder.DeliveryNation == null ? null : new Customer_NationDTO(CustomerSalesOrder.DeliveryNation);
            this.DeliveryProvince = CustomerSalesOrder.DeliveryProvince == null ? null : new Customer_ProvinceDTO(CustomerSalesOrder.DeliveryProvince);
            this.DeliveryWard = CustomerSalesOrder.DeliveryWard == null ? null : new Customer_WardDTO(CustomerSalesOrder.DeliveryWard);
            this.EditedPriceStatus = CustomerSalesOrder.EditedPriceStatus == null ? null : new Customer_EditedPriceStatusDTO(CustomerSalesOrder.EditedPriceStatus);
            this.InvoiceDistrict = CustomerSalesOrder.InvoiceDistrict == null ? null : new Customer_DistrictDTO(CustomerSalesOrder.InvoiceDistrict);
            this.InvoiceNation = CustomerSalesOrder.InvoiceNation == null ? null : new Customer_NationDTO(CustomerSalesOrder.InvoiceNation);
            this.InvoiceProvince = CustomerSalesOrder.InvoiceProvince == null ? null : new Customer_ProvinceDTO(CustomerSalesOrder.InvoiceProvince);
            this.InvoiceWard = CustomerSalesOrder.InvoiceWard == null ? null : new Customer_WardDTO(CustomerSalesOrder.InvoiceWard);
            this.Opportunity = CustomerSalesOrder.Opportunity == null ? null : new Customer_OpportunityDTO(CustomerSalesOrder.Opportunity);
            this.OrderPaymentStatus = CustomerSalesOrder.OrderPaymentStatus == null ? null : new Customer_OrderPaymentStatusDTO(CustomerSalesOrder.OrderPaymentStatus);
            this.Organization = CustomerSalesOrder.Organization == null ? null : new Customer_OrganizationDTO(CustomerSalesOrder.Organization);
            this.RequestState = CustomerSalesOrder.RequestState == null ? null : new Customer_RequestStateDTO(CustomerSalesOrder.RequestState);
            this.SalesEmployee = CustomerSalesOrder.SalesEmployee == null ? null : new Customer_AppUserDTO(CustomerSalesOrder.SalesEmployee);
            this.CustomerSalesOrderContents = CustomerSalesOrder.CustomerSalesOrderContents?.Select(x => new Customer_CustomerSalesOrderContentDTO(x)).ToList();
            this.CustomerSalesOrderPromotions = CustomerSalesOrder.CustomerSalesOrderPromotions?.Select(x => new Customer_CustomerSalesOrderPromotionDTO(x)).ToList();
            this.CreatedAt = CustomerSalesOrder.CreatedAt;
            this.UpdatedAt = CustomerSalesOrder.UpdatedAt;
            this.Errors = CustomerSalesOrder.Errors;
        }
    }

    public class Customer_CustomerSalesOrderFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter ContractId { get; set; }
        public IdFilter OrderPaymentStatusId { get; set; }
        public IdFilter RequestStateId { get; set; }
        public IdFilter EditedPriceStatusId { get; set; }
        public StringFilter ShippingName { get; set; }
        public DateFilter OrderDate { get; set; }
        public DateFilter DeliveryDate { get; set; }
        public IdFilter SalesEmployeeId { get; set; }
        public StringFilter Note { get; set; }
        public StringFilter InvoiceAddress { get; set; }
        public IdFilter InvoiceNationId { get; set; }
        public IdFilter InvoiceProvinceId { get; set; }
        public IdFilter InvoiceDistrictId { get; set; }
        public IdFilter InvoiceWardId { get; set; }
        public StringFilter InvoiceZIPCode { get; set; }
        public StringFilter DeliveryAddress { get; set; }
        public IdFilter DeliveryNationId { get; set; }
        public IdFilter DeliveryProvinceId { get; set; }
        public IdFilter DeliveryDistrictId { get; set; }
        public IdFilter DeliveryWardId { get; set; }
        public StringFilter DeliveryZIPCode { get; set; }
        public DecimalFilter SubTotal { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DecimalFilter TotalTaxOther { get; set; }
        public DecimalFilter TotalTax { get; set; }
        public DecimalFilter Total { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerSalesOrderOrder OrderBy { get; set; }
    }
}
