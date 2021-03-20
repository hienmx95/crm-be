using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreExtendDTO : DataDTO
    {

        public long StoreId { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string Fax { get; set; }

        public decimal BusinessCapital { get; set; }

        public long BusinessTypeId { get; set; }

        public string ATM { get; set; }

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

        public string AbilityToPay { get; set; }

        public long? RewardInYear { get; set; }

        public string AbilityRaisingCapital { get; set; }

        public string AbilityLimitedCapital { get; set; }

        public bool? DivideEachPart { get; set; }

        public bool? DivideHuman { get; set; }

        public string AnotherStrongPoint { get; set; }

        public string ReadyCoordinate { get; set; }

        public string Invest { get; set; }
        public decimal WareHouseAcreage { get; set; }
        public long CurrencyId { get; set; }
        public Store_CurrencyDTO Currency { get; set; }


        public Store_StoreExtendDTO() { }
        public Store_StoreExtendDTO(StoreExtend StoreExtend)
        {

            this.StoreId = StoreExtend.StoreId;

            this.PhoneNumber = StoreExtend.PhoneOther;

            this.CompanyName = StoreExtend.CompanyName;

            this.Fax = StoreExtend.Fax;

            this.BusinessCapital = StoreExtend.BusinessCapital;

            this.BusinessTypeId = StoreExtend.BusinessTypeId;

            this.ATM = StoreExtend.BankAccountNumber;

            this.BusinessLicense = StoreExtend.BusinessLicense;

            this.BankName = StoreExtend.BankName;

            this.DateOfBusinessLicense = StoreExtend.DateOfBusinessLicense;

            this.AgentContractNumber = StoreExtend.AgentContractNumber;

            this.DateOfAgentContractNumber = StoreExtend.DateOfAgentContractNumber;

            this.DistributionArea = StoreExtend.DistributionArea;

            this.RegionalPopulation = StoreExtend.RegionalPopulation;

            this.DistributionAcreage = StoreExtend.DistributionAcreage;

            this.UrbanizationLevel = StoreExtend.UrbanizationLevel;

            this.NumberOfPointsOfSale = StoreExtend.NumberOfPointsOfSale;

            this.NumberOfKeyCustomer = StoreExtend.NumberOfKeyCustomer;

            this.MarketCharacteristics = StoreExtend.MarketCharacteristics;

            this.StoreAcreage = StoreExtend.StoreAcreage;

            this.AbilityToPay = StoreExtend.AbilityToPay;

            this.RewardInYear = StoreExtend.RewardInYear;

            this.AbilityRaisingCapital = StoreExtend.AbilityRaisingCapital;

            this.AbilityLimitedCapital = StoreExtend.AbilityLimitedCapital;

            this.DivideEachPart = StoreExtend.DivideEachPart;

            this.DivideHuman = StoreExtend.DivideHuman;

            this.AnotherStrongPoint = StoreExtend.AnotherStrongPoint;

            this.ReadyCoordinate = StoreExtend.ReadyCoordinate;
            this.WareHouseAcreage = StoreExtend.WareHouseAcreage;

            this.Invest = StoreExtend.Invest;

            this.CurrencyId = StoreExtend.CurrencyId;

            this.Currency = StoreExtend.Currency == null ? null : new Store_CurrencyDTO(StoreExtend.Currency);

            this.Errors = StoreExtend.Errors;
        }
    }

    public class Store_StoreExtendFilterDTO : FilterDTO
    {

        public IdFilter StoreId { get; set; }

        public StringFilter PhoneNumber { get; set; }

        public StringFilter CompanyName { get; set; }

        public StringFilter Fax { get; set; }

        public StringFilter BusinessCapital { get; set; }

        public IdFilter BusinessTypeId { get; set; }

        public StringFilter ATM { get; set; }

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

        public StoreExtendOrder OrderBy { get; set; }
    }
}