using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderRetailDAO
    {
        public OrderRetailDAO()
        {
            OrderRetailContents = new HashSet<OrderRetailContentDAO>();
            OrderRetailPaymentHistories = new HashSet<OrderRetailPaymentHistoryDAO>();
            OrderRetailPromotions = new HashSet<OrderRetailPromotionDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long? OrderRetailTypeId { get; set; }
        public long CustomerRetailId { get; set; }
        public long? OrderRetailSoureId { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string Company { get; set; }
        public string TaxCode { get; set; }
        public string CompanyAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ZipCode { get; set; }
        public long RequestStateId { get; set; }
        public long OrderPaymentStatusId { get; set; }
        public long AppUserAssignedId { get; set; }
        public string Note { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long EditedPriceStatusId { get; set; }
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
        public virtual AppUserDAO CreatedBy { get; set; }
        public virtual CustomerRetailDAO CustomerRetail { get; set; }
        public virtual EditedPriceStatusDAO EditedPriceStatus { get; set; }
        public virtual OrderPaymentStatusDAO OrderPaymentStatus { get; set; }
        public virtual OrderRetailSourceDAO OrderRetailSoure { get; set; }
        public virtual OrderRetailTypeDAO OrderRetailType { get; set; }
        public virtual OrganizationDAO Organization { get; set; }
        public virtual RequestStateDAO RequestState { get; set; }
        public virtual ICollection<OrderRetailContentDAO> OrderRetailContents { get; set; }
        public virtual ICollection<OrderRetailPaymentHistoryDAO> OrderRetailPaymentHistories { get; set; }
        public virtual ICollection<OrderRetailPromotionDAO> OrderRetailPromotions { get; set; }
    }
}
