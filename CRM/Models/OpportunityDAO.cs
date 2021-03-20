using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityDAO
    {
        public OpportunityDAO()
        {
            Contracts = new HashSet<ContractDAO>();
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
            OpportunityActivities = new HashSet<OpportunityActivityDAO>();
            OpportunityCallLogMappings = new HashSet<OpportunityCallLogMappingDAO>();
            OpportunityContactMappings = new HashSet<OpportunityContactMappingDAO>();
            OpportunityEmails = new HashSet<OpportunityEmailDAO>();
            OpportunityFileGroupings = new HashSet<OpportunityFileGroupingDAO>();
            OpportunityItemMappings = new HashSet<OpportunityItemMappingDAO>();
            OrderQuotes = new HashSet<OrderQuoteDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public long? CustomerLeadId { get; set; }
        public DateTime ClosingDate { get; set; }
        public long? SaleStageId { get; set; }
        public long ProbabilityId { get; set; }
        public long? PotentialResultId { get; set; }
        public long? LeadSourceId { get; set; }
        public long AppUserId { get; set; }
        public long? CurrencyId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? ForecastAmount { get; set; }
        public string Description { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public long? OpportunityResultTypeId { get; set; }
        public long CreatorId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual CustomerLeadDAO CustomerLead { get; set; }
        public virtual CustomerLeadSourceDAO LeadSource { get; set; }
        public virtual OpportunityResultTypeDAO OpportunityResultType { get; set; }
        public virtual PotentialResultDAO PotentialResult { get; set; }
        public virtual ProbabilityDAO Probability { get; set; }
        public virtual SaleStageDAO SaleStage { get; set; }
        public virtual ICollection<ContractDAO> Contracts { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
        public virtual ICollection<OpportunityActivityDAO> OpportunityActivities { get; set; }
        public virtual ICollection<OpportunityCallLogMappingDAO> OpportunityCallLogMappings { get; set; }
        public virtual ICollection<OpportunityContactMappingDAO> OpportunityContactMappings { get; set; }
        public virtual ICollection<OpportunityEmailDAO> OpportunityEmails { get; set; }
        public virtual ICollection<OpportunityFileGroupingDAO> OpportunityFileGroupings { get; set; }
        public virtual ICollection<OpportunityItemMappingDAO> OpportunityItemMappings { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuotes { get; set; }
    }
}
