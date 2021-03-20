using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderProjectDAO
    {
        public OrderProjectDAO()
        {
            OrderProjectContactMappings = new HashSet<OrderProjectContactMappingDAO>();
            OrderProjectContents = new HashSet<OrderProjectContentDAO>();
            OrderProjectPaymentHistories = new HashSet<OrderProjectPaymentHistoryDAO>();
            OrderProjectPromotions = new HashSet<OrderProjectPromotionDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long? OrderProjectTypeId { get; set; }
        public string Transportation { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? OpportunityId { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerProjectId { get; set; }
        public long AppUserAssignedId { get; set; }
        public long? ContractId { get; set; }
        public string Note { get; set; }
        public long? OrderQuoteId { get; set; }
        public long RequestStateId { get; set; }
        public long OrderPaymentStatusId { get; set; }
        public long EditedPriceStatusId { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public long? InvoiceNationId { get; set; }
        public string InvoiceZIPCode { get; set; }
        public string DeliveryAddress { get; set; }
        public long? DeliveryNationId { get; set; }
        public long? DeliveryProvinceId { get; set; }
        public long? DeliveryDistrictId { get; set; }
        public string DeliveryZIPCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalTaxAmountOther { get; set; }
        public decimal Total { get; set; }
        public long OrganizationId { get; set; }
        public Guid RowId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? CreatedById { get; set; }

        public virtual AppUserDAO AppUserAssigned { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual ContractDAO Contract { get; set; }
        public virtual AppUserDAO CreatedBy { get; set; }
        public virtual CustomerProjectDAO CustomerProject { get; set; }
        public virtual DistrictDAO DeliveryDistrict { get; set; }
        public virtual NationDAO DeliveryNation { get; set; }
        public virtual ProvinceDAO DeliveryProvince { get; set; }
        public virtual EditedPriceStatusDAO EditedPriceStatus { get; set; }
        public virtual DistrictDAO InvoiceDistrict { get; set; }
        public virtual NationDAO InvoiceNation { get; set; }
        public virtual ProvinceDAO InvoiceProvince { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
        public virtual OrderPaymentStatusDAO OrderPaymentStatus { get; set; }
        public virtual OrderProjectTypeDAO OrderProjectType { get; set; }
        public virtual OrderQuoteDAO OrderQuote { get; set; }
        public virtual OrganizationDAO Organization { get; set; }
        public virtual RequestStateDAO RequestState { get; set; }
        public virtual ICollection<OrderProjectContactMappingDAO> OrderProjectContactMappings { get; set; }
        public virtual ICollection<OrderProjectContentDAO> OrderProjectContents { get; set; }
        public virtual ICollection<OrderProjectPaymentHistoryDAO> OrderProjectPaymentHistories { get; set; }
        public virtual ICollection<OrderProjectPromotionDAO> OrderProjectPromotions { get; set; }
    }
}
