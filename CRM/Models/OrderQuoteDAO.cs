using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderQuoteDAO
    {
        public OrderQuoteDAO()
        {
            OrderQuoteContents = new HashSet<OrderQuoteContentDAO>();
        }

        public long Id { get; set; }
        public string Subject { get; set; }
        public long CompanyId { get; set; }
        public long ContactId { get; set; }
        public long? OpportunityId { get; set; }
        public long EditedPriceStatusId { get; set; }
        public DateTime EndAt { get; set; }
        public long AppUserId { get; set; }
        public long OrderQuoteStatusId { get; set; }
        public string Note { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public string InvoiceZIPCode { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public string ZIPCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GeneralDiscountPercentage { get; set; }
        public decimal GeneralDiscountAmount { get; set; }
        public decimal? TotalTaxAmountOther { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal Total { get; set; }
        public long CreatorId { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual ContactDAO Contact { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual EditedPriceStatusDAO EditedPriceStatus { get; set; }
        public virtual DistrictDAO InvoiceDistrict { get; set; }
        public virtual NationDAO InvoiceNation { get; set; }
        public virtual ProvinceDAO InvoiceProvince { get; set; }
        public virtual NationDAO Nation { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
        public virtual OrderQuoteStatusDAO OrderQuoteStatus { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<OrderQuoteContentDAO> OrderQuoteContents { get; set; }
    }
}
