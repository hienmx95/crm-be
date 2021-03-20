using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class StoreExtend : DataEntity, IEquatable<StoreExtend>
    {
        public long StoreId { get; set; }
        public string PhoneOther { get; set; }
        public string CompanyName { get; set; }
        public string Fax { get; set; }
        public decimal BusinessCapital { get; set; }
        public long BusinessTypeId { get; set; }
        public string BankAccountNumber { get; set; }
        public string BusinessLicense { get; set; }
        public string BankName { get; set; }
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
        public long CurrencyId { get; set; }
        public BusinessType BusinessType { get; set; }
        public Currency Currency { get; set; }
        public Store Store { get; set; }

        public bool Equals(StoreExtend other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StoreExtendFilter : FilterEntity
    {
        public IdFilter StoreId { get; set; }
        public StringFilter PhoneOther { get; set; }
        public StringFilter CompanyName { get; set; }
        public StringFilter Fax { get; set; }
        public StringFilter BusinessCapital { get; set; }
        public IdFilter BusinessTypeId { get; set; }
        public StringFilter BankAccountNumber { get; set; }
        public StringFilter BusinessLicense { get; set; }
        public StringFilter BankName { get; set; }
        public DateFilter DateOfBusinessLicense { get; set; }
        public StringFilter AgentContractNumber { get; set; }
        public DateFilter DateOfAgentContractNumber { get; set; }
        public StringFilter DistributionArea { get; set; }
        public LongFilter RegionalPopulation { get; set; }
        public DecimalFilter DistributionAcreage { get; set; }
        public StringFilter UrbanizationLevel { get; set; }
        public LongFilter NumberOfPointsOfSale { get; set; }
        public LongFilter NumberOfKeyCustomer { get; set; }
        public StringFilter MarketCharacteristics { get; set; }
        public DecimalFilter StoreAcreage { get; set; }
        public StringFilter AbilityToPay { get; set; }
        public LongFilter RewardInYear { get; set; }
        public StringFilter AbilityRaisingCapital { get; set; }
        public StringFilter AbilityLimitedCapital { get; set; }
        public StringFilter AnotherStrongPoint { get; set; }
        public StringFilter ReadyCoordinate { get; set; }
        public StringFilter Invest { get; set; }
        public List<StoreExtendFilter> OrFilter { get; set; }
        public StoreExtendOrder OrderBy { get; set; }
        public StoreExtendSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StoreExtendOrder
    {
        Store = 0,
        PhoneOther = 1,
        CompanyName = 2,
        Fax = 3,
        BusinessCapital = 4,
        BusinessType = 5,
        BankAccountNumber = 6,
        BusinessLicense = 7,
        BankName = 8,
        DateOfBusinessLicense = 9,
        AgentContractNumber = 10,
        DateOfAgentContractNumber = 11,
        DistributionArea = 12,
        RegionalPopulation = 13,
        DistributionAcreage = 14,
        UrbanizationLevel = 15,
        NumberOfPointsOfSale = 16,
        NumberOfKeyCustomer = 17,
        MarketCharacteristics = 18,
        StoreAcreage = 19,
        AbilityToPay = 20,
        RewardInYear = 21,
        AbilityRaisingCapital = 22,
        AbilityLimitedCapital = 23,
        DivideEachPart = 24,
        DivideHuman = 25,
        AnotherStrongPoint = 26,
        ReadyCoordinate = 27,
        Invest = 28,
    }

    [Flags]
    public enum StoreExtendSelect : long
    {
        ALL = E.ALL,
        Store = E._0,
        PhoneOther = E._1,
        CompanyName = E._2,
        Fax = E._3,
        BusinessCapital = E._4,
        BusinessType = E._5,
        BankAccountNumber = E._6,
        BusinessLicense = E._7,
        BankName = E._8,
        DateOfBusinessLicense = E._9,
        AgentContractNumber = E._10,
        DateOfAgentContractNumber = E._11,
        DistributionArea = E._12,
        RegionalPopulation = E._13,
        DistributionAcreage = E._14,
        UrbanizationLevel = E._15,
        NumberOfPointsOfSale = E._16,
        NumberOfKeyCustomer = E._17,
        MarketCharacteristics = E._18,
        StoreAcreage = E._19,
        AbilityToPay = E._20,
        RewardInYear = E._21,
        AbilityRaisingCapital = E._22,
        AbilityLimitedCapital = E._23,
        DivideEachPart = E._24,
        DivideHuman = E._25,
        AnotherStrongPoint = E._26,
        ReadyCoordinate = E._27,
        Invest = E._28,
    }
}
