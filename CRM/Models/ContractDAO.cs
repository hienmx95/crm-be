using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContractDAO
    {
        public ContractDAO()
        {
            ContractContactMappings = new HashSet<ContractContactMappingDAO>();
            ContractFileGroupings = new HashSet<ContractFileGroupingDAO>();
            ContractItemDetails = new HashSet<ContractItemDetailDAO>();
            ContractPaymentHistories = new HashSet<ContractPaymentHistoryDAO>();
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public long? OpportunityId { get; set; }
        public long CustomerId { get; set; }
        public long ContractTypeId { get; set; }
        public decimal TotalValue { get; set; }
        public long CurrencyId { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long AppUserId { get; set; }
        public string DeliveryUnit { get; set; }
        public long ContractStatusId { get; set; }
        public long PaymentStatusId { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public string InvoiceZipCode { get; set; }
        public string ReceiveAddress { get; set; }
        public long? ReceiveNationId { get; set; }
        public long? ReceiveProvinceId { get; set; }
        public long? ReceiveDistrictId { get; set; }
        public string ReceiveZipCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TotalTaxAmountOther { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public decimal Total { get; set; }
        public string TermAndCondition { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual ContractStatusDAO ContractStatus { get; set; }
        public virtual ContractTypeDAO ContractType { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual CustomerDAO Customer { get; set; }
        public virtual DistrictDAO InvoiceDistrict { get; set; }
        public virtual NationDAO InvoiceNation { get; set; }
        public virtual ProvinceDAO InvoiceProvince { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
        public virtual OrganizationDAO Organization { get; set; }
        public virtual PaymentStatusDAO PaymentStatus { get; set; }
        public virtual DistrictDAO ReceiveDistrict { get; set; }
        public virtual NationDAO ReceiveNation { get; set; }
        public virtual ProvinceDAO ReceiveProvince { get; set; }
        public virtual ICollection<ContractContactMappingDAO> ContractContactMappings { get; set; }
        public virtual ICollection<ContractFileGroupingDAO> ContractFileGroupings { get; set; }
        public virtual ICollection<ContractItemDetailDAO> ContractItemDetails { get; set; }
        public virtual ICollection<ContractPaymentHistoryDAO> ContractPaymentHistories { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
    }
}
