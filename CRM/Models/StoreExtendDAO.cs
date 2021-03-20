using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreExtendDAO
    {
        public long StoreId { get; set; }
        public string PhoneOther { get; set; }
        public string Fax { get; set; }
        public decimal BusinessCapital { get; set; }
        public long CurrencyId { get; set; }
        public long BusinessTypeId { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BusinessLicense { get; set; }
        public DateTime? DateOfBusinessLicense { get; set; }
        public string AgentContractNumber { get; set; }
        public DateTime? DateOfAgentContractNumber { get; set; }
        public string DistributionArea { get; set; }
        public long RegionalPopulation { get; set; }
        public decimal DistributionAcreage { get; set; }
        public string UrbanizationLevel { get; set; }
        public long NumberOfPointsOfSale { get; set; }
        public long NumberOfKeyCustomer { get; set; }
        public string MarketCharacteristics { get; set; }
        public decimal StoreAcreage { get; set; }
        public decimal WareHouseAcreage { get; set; }
        public string AbilityToPay { get; set; }
        public long? RewardInYear { get; set; }
        public string AbilityRaisingCapital { get; set; }
        public string AbilityLimitedCapital { get; set; }
        public bool? DivideEachPart { get; set; }
        public bool? DivideHuman { get; set; }
        public string AnotherStrongPoint { get; set; }
        public string ReadyCoordinate { get; set; }
        public string Invest { get; set; }

        public virtual BusinessTypeDAO BusinessType { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
