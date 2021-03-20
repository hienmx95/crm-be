using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerAgentDAO
    {
        public CustomerAgentDAO()
        {
            CustomerAgentCallLogMappings = new HashSet<CustomerAgentCallLogMappingDAO>();
            CustomerAgentContractMappings = new HashSet<CustomerAgentContractMappingDAO>();
            CustomerAgentEmails = new HashSet<CustomerAgentEmailDAO>();
            CustomerAgentRelationships = new HashSet<CustomerAgentRelationshipDAO>();
            CustomerAgentTicketMappings = new HashSet<CustomerAgentTicketMappingDAO>();
            Stores = new HashSet<StoreDAO>();
        }

        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneOther { get; set; }
        public string TaxCode { get; set; }
        public string Fax { get; set; }
        public decimal? BusinessCapital { get; set; }
        public long? CompanyId { get; set; }
        public long? BusinessTypeId { get; set; }
        public string BusinessAddress { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string BusinessLicense { get; set; }
        public string BankName { get; set; }
        public DateTime? BusinessLicenseSignDay { get; set; }
        public string ContractAgentNumber { get; set; }
        public DateTime? ContractAgentSignDay { get; set; }
        public long? CustomerTypeId { get; set; }
        public long? CustomerGroupId { get; set; }
        public long? CustomerResourceId { get; set; }
        public long? CustomerLevelId { get; set; }
        public long? AssignedAppUserId { get; set; }
        public long? StatusId { get; set; }
        public string AreaDistribution { get; set; }
        public string AreaPopulation { get; set; }
        public decimal? AreaAcreage { get; set; }
        public string LevelOfUrbanization { get; set; }
        public long? NumberPointOfSale { get; set; }
        public long? NumberCustomerKey { get; set; }
        public string MarketCharacteristic { get; set; }
        public decimal? AcreageStore { get; set; }
        public decimal? AcreageWarehouse { get; set; }
        public string AbilityToPay { get; set; }
        public long? NumberRewardOfYear { get; set; }
        public string AbilityCapitalMobilization { get; set; }
        public string AbilityLimitFinancial { get; set; }
        public bool? DivideEachPart { get; set; }
        public bool? DivisionPeople { get; set; }
        public string OtherStrongPoint { get; set; }
        public string DevelopMarket { get; set; }
        public string Invest { get; set; }
        public long? OrganizationId { get; set; }
        public long? CreatedById { get; set; }
        public decimal? AwardPoint { get; set; }
        public decimal? SubAwardPoint { get; set; }
        public bool? Used { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        public long? CurrencyId { get; set; }

        public virtual AppUserDAO AssignedAppUser { get; set; }
        public virtual BusinessTypeDAO BusinessType { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual AppUserDAO CreatedBy { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerGroupDAO CustomerGroup { get; set; }
        public virtual CustomerLevelDAO CustomerLevel { get; set; }
        public virtual CustomerResourceDAO CustomerResource { get; set; }
        public virtual CustomerTypeDAO CustomerType { get; set; }
        public virtual OrganizationDAO Organization { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CustomerAgentCallLogMappingDAO> CustomerAgentCallLogMappings { get; set; }
        public virtual ICollection<CustomerAgentContractMappingDAO> CustomerAgentContractMappings { get; set; }
        public virtual ICollection<CustomerAgentEmailDAO> CustomerAgentEmails { get; set; }
        public virtual ICollection<CustomerAgentRelationshipDAO> CustomerAgentRelationships { get; set; }
        public virtual ICollection<CustomerAgentTicketMappingDAO> CustomerAgentTicketMappings { get; set; }
        public virtual ICollection<StoreDAO> Stores { get; set; }
    }
}
