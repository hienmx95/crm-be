using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MStore
{
    public interface IStoreValidator : IServiceScoped
    {
        Task<bool> AddToCustomer(List<Store> Stores);
    }

    public class StoreValidator : IStoreValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            RepresentCannotBeNull,
            PhoneNumberEmpty,
            PhoneNumberOverLength,
            CompanyNameEmpty,
            BusinessCapitalEmpty,
            BusinessTypeEmpty,
            ATMEmpty,
            BusinessLicenseEmpty,
            BankNameEmpty,
            AgentContractNumberEmpty,
            DistributionAreaEmpty,
            RegionalPopulationEmpty,
            DistributionAcreageEmpty,
            UrbanizationLevelEmpty,
            NumberOfPointsOfSaleEmpty,
            NumberOfKeyCustomerEmpty,
            MarketCharacteristicsEmpty,
            StoreAcreageEmpty,
            AbilityToPayEmpty,
            StoreMeansOfDeliveryCannotBeNull,
            StoreAssetsCannotBeNull,
            DeliveryTimeCannotBeNull,
            RelationshipCustomerTypeCannotBeNull,
            InfulenceLevelMarketCannotBeNull,
            MarketPriceCannotBeNull,
            PersonnelCannotBeNull,
            WarrantyServiceCannotBeNull,
            CoverageCapacityCannotBeNull,
            StoreConsultingServiceCannotBeNull,
            AnotherStrongPointEmpty,
            CooperativeAttitudeCannotBeNull,
            ImproveQualityServingCannotBeNull,
            StoreUsed
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public StoreValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Store Store)
        {
            StoreFilter StoreFilter = new StoreFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Store.Id },
                Selects = StoreSelect.Id
            };

            int count = await UOW.StoreRepository.Count(StoreFilter);
            if (count == 0)
                Store.AddError(nameof(StoreValidator), nameof(Store.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        //#region Validate StoreExtend

        //#region Số điện thoại khác/zalo
        //public async Task<bool> ValidatePhoneNumber(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.PhoneOther))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.PhoneOther), ErrorCode.PhoneNumberEmpty);
        //    }
        //    else if (Store.StoreExtend.PhoneOther.Length > 20)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.PhoneOther), ErrorCode.PhoneNumberOverLength);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Tên khách hàng/Công ty
        //public async Task<bool> ValidateCompanyName(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.CompanyName))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.CompanyName), ErrorCode.CompanyNameEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Vốn kinh doanh
        //public async Task<bool> ValidateBusinessCapital(Store Store)
        //{
        //    if (Store.StoreExtend.BusinessCapital == null)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.BusinessCapital), ErrorCode.BusinessCapitalEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Loại hình doanh nghiệp
        //public async Task<bool> ValidateBusinessType(Store Store)
        //{
        //    if (Store.StoreExtend.BusinessTypeId == null || Store.StoreExtend.BusinessTypeId == 0)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.BusinessType), ErrorCode.BusinessTypeEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Số tài khoản
        //public async Task<bool> ValidateATM(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.BankAccountNumber))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.BankAccountNumber), ErrorCode.ATMEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Giấy phép kinh doanh
        //public async Task<bool> ValidateBusinessLicense(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.BusinessLicense))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.BusinessLicense), ErrorCode.BusinessLicenseEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Ngân hàng
        //public async Task<bool> ValidateBankName(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.BankName))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.BankName), ErrorCode.BankNameEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Hợp đồng đại lý
        //public async Task<bool> ValidateAgentContractNumber(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.AgentContractNumber))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.AgentContractNumber), ErrorCode.AgentContractNumberEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Người đại diện
        //private async Task<bool> ValidateRepresent(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.StoreRepresents != null)
        //    {
        //        foreach (var represent in Store.StoreRepresents)
        //        {
        //            if (represent.Name != null
        //                || represent.DateOfBirth != null
        //                || represent.Phone != null
        //                || represent.PositionId != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.StoreRepresents), ErrorCode.RepresentCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Khu vực phân phối
        //public async Task<bool> ValidateDistributionArea(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.DistributionArea))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.DistributionArea), ErrorCode.DistributionAreaEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Dân số khu vực
        //public async Task<bool> ValidateRegionalPopulation(Store Store)
        //{
        //    if (Store.StoreExtend.RegionalPopulation == null)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.RegionalPopulation), ErrorCode.RegionalPopulationEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Diện tích khu vực
        //public async Task<bool> ValidateDistributionAcreage(Store Store)
        //{
        //    if (Store.StoreExtend.DistributionAcreage == null)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.DistributionAcreage), ErrorCode.DistributionAcreageEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Mức đô thị hóa
        //public async Task<bool> ValidateUrbanizationLevel(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.UrbanizationLevel))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.UrbanizationLevel), ErrorCode.UrbanizationLevelEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Số lượng điểm bán
        //public async Task<bool> ValidateNumberOfPointsOfSale(Store Store)
        //{
        //    if (Store.StoreExtend.NumberOfPointsOfSale == null)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.NumberOfPointsOfSale), ErrorCode.NumberOfPointsOfSaleEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Số lượng khách hàng trọng điểm
        //public async Task<bool> ValidateNumberOfKeyCustomer(Store Store)
        //{
        //    if (Store.StoreExtend.NumberOfKeyCustomer == null)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.NumberOfKeyCustomer), ErrorCode.NumberOfKeyCustomerEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Đặc điểm thị trường
        //public async Task<bool> ValidateMarketCharacteristics(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.MarketCharacteristics))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.MarketCharacteristics), ErrorCode.MarketCharacteristicsEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Diện tích cửa hàng
        //public async Task<bool> ValidateStoreAcreage(Store Store)
        //{
        //    if (Store.StoreExtend.StoreAcreage == null)
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.StoreAcreage), ErrorCode.StoreAcreageEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Khả năng thanh toán đúng hạn
        //public async Task<bool> ValidateAbilityToPay(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.AbilityToPay))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.AbilityToPay), ErrorCode.AbilityToPayEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Nhân sự
        //private async Task<bool> ValidatePersonnel(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.StorePersonnels != null)
        //    {
        //        foreach (var represent in Store.StorePersonnels)
        //        {
        //            if (represent.Name != null
        //                || represent.Quantity != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.StorePersonnels), ErrorCode.PersonnelCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Phương tiện giao hàng
        //private async Task<bool> ValidateStoreMeansOfDelivery(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.StoreMeansOfDeliveries != null)
        //    {
        //        foreach (var represent in Store.StoreMeansOfDeliveries)
        //        {
        //            if (represent.Name != null
        //                || represent.Quantity != null
        //                || represent.Owned != null
        //                || represent.Rent != null
        //                || represent.StoreId != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.StoreMeansOfDeliveries), ErrorCode.StoreMeansOfDeliveryCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Tài sản
        //private async Task<bool> ValidateStoreAssets(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.StoreAssetses != null)
        //    {
        //        foreach (var item in Store.StoreAssetses)
        //        {
        //            if (item.Name != null
        //                || item.Quantity != null
        //                || item.Owned != null
        //                || item.Rent != null
        //                || item.StoreId != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.StoreAssetses), ErrorCode.StoreAssetsCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Thời gian giao hàng
        //private async Task<bool> ValidateDeliveryTime(Store Store)
        //{
        //    if (Store.StoreDeliveryTimeMappings == null || !Store.StoreDeliveryTimeMappings.Any())
        //    {
        //        Store.AddError(nameof(StoreValidator), nameof(Store.StoreDeliveryTimeMappings), ErrorCode.DeliveryTimeCannotBeNull);
        //    }

        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Mối quan hệ khách hàng
        //private async Task<bool> ValidateRelationshipCustomerType(Store Store)
        //{
        //    if (Store.StoreRelationshipCustomerMappings == null || !Store.StoreRelationshipCustomerMappings.Any())
        //    {
        //        Store.AddError(nameof(StoreValidator), nameof(Store.StoreRelationshipCustomerMappings), ErrorCode.RelationshipCustomerTypeCannotBeNull);
        //    }

        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Mức độ ảnh hưởng 
        //private async Task<bool> ValidateInfulenceLevelMarket(Store Store)
        //{
        //    if (Store.StoreInfulenceLevelMarketMappings == null || !Store.StoreInfulenceLevelMarketMappings.Any())
        //    {
        //        Store.AddError(nameof(StoreValidator), nameof(Store.StoreInfulenceLevelMarketMappings), ErrorCode.InfulenceLevelMarketCannotBeNull);
        //    }

        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Giá bán thị trường
        //private async Task<bool> ValidateMarketPrice(Store Store)
        //{
        //    if (Store.StoreMarketPriceMappings == null || !Store.StoreMarketPriceMappings.Any())
        //    {
        //        Store.AddError(nameof(StoreValidator), nameof(Store.StoreMarketPriceMappings), ErrorCode.MarketPriceCannotBeNull);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Dịch vụ bảo hành
        //private async Task<bool> ValidateWarrantyService(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.StoreWarrantyServices != null)
        //    {
        //        foreach (var item in Store.StoreWarrantyServices)
        //        {
        //            if (item.Name != null
        //                || item.Detail != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.StoreWarrantyServices), ErrorCode.WarrantyServiceCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Khả năng bao phủ thị trường
        //private async Task<bool> ValidateStoreCoverageCapacity(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.StoreCoverageCapacities != null)
        //    {
        //        foreach (var item in Store.StoreCoverageCapacities)
        //        {
        //            if (item.Name != null
        //                || item.Detail != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.StoreCoverageCapacities), ErrorCode.CoverageCapacityCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Khả năng tư vấn dịch vụ
        //private async Task<bool> ValidateStoreConsultingService(Store Store)
        //{
        //    if (Store.StoreConsultingServiceMappings == null || !Store.StoreConsultingServiceMappings.Any())
        //    {
        //        Store.AddError(nameof(StoreValidator), nameof(Store.StoreConsultingServiceMappings), ErrorCode.StoreConsultingServiceCannotBeNull);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Điểm mạnh khác
        //public async Task<bool> ValidateAnotherStrongPoint(Store Store)
        //{
        //    if (string.IsNullOrWhiteSpace(Store.StoreExtend.AnotherStrongPoint))
        //    {
        //        Store.StoreExtend.AddError(nameof(StoreValidator), nameof(Store.StoreExtend.AnotherStrongPoint), ErrorCode.AnotherStrongPointEmpty);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Thái độ hợp tác
        //private async Task<bool> ValidateCooperativeAttitude(Store Store)
        //{
        //    if (Store.StoreCooperativeAttitudeMappings == null || !Store.StoreCooperativeAttitudeMappings.Any())
        //    {
        //        Store.AddError(nameof(StoreValidator), nameof(Store.StoreCooperativeAttitudeMappings), ErrorCode.CooperativeAttitudeCannotBeNull);
        //    }
        //    return Store.IsValidated;
        //}
        //#endregion

        //#region Nâng cao chất lượng dịch vụ
        //private async Task<bool> ValidateImproveQualityServing(Store Store)
        //{
        //    bool flag = false;
        //    if (Store.ImproveQualityServings != null)
        //    {
        //        foreach (var item in Store.ImproveQualityServings)
        //        {
        //            if (item.Name != null
        //                || item.Detail != null
        //                )
        //                flag = true;
        //        }
        //    }
        //    if (!flag) Store.AddError(nameof(StoreValidator), nameof(Store.ImproveQualityServings), ErrorCode.ImproveQualityServingCannotBeNull);
        //    return Store.IsValidated;
        //}
        //#endregion


        //#endregion


        public async Task<bool> AddToCustomer(List<Store> Stores)
        {
            var CustomerId = Stores.Select(x => x.CustomerId).FirstOrDefault();
            if(CustomerId == 0)
            {
                return false;
            }
            var Ids = Stores.Select(x => x.Id).ToList();
            StoreFilter StoreFilter = new StoreFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StoreSelect.Id | StoreSelect.Customer,
                Id = new IdFilter { In = Ids }
            };
            var oldDatas = await UOW.StoreRepository.List(StoreFilter);
            Dictionary<long, Store> dict = oldDatas.ToDictionary(x => x.Id, y => y);
            foreach (var Store in Stores)
            {
                var old = dict[Store.Id];
                if(old == null)
                {
                    Store.AddError(nameof(StoreValidator), nameof(Store.Id), ErrorCode.IdNotExisted);
                }
                else
                {
                    if (old.CustomerId.HasValue)
                    {
                        Store.AddError(nameof(StoreValidator), nameof(Store.Id), ErrorCode.StoreUsed);
                    }
                }
            }
            return true;
        }
    }
}
public class StoreExtendValidator
{
    public enum ErrorCode
    {
        IdNotExisted,
        RepresentCannotBeNull,
        PhoneNumberEmpty,
        PhoneNumberOverLength,
        CompanyNameEmpty,
        BusinessCapitalEmpty,
        BusinessTypeEmpty,
        ATMEmpty,
        BusinessLicenseEmpty,
        BankNameEmpty,
        AgentContractNumberEmpty,
        DistributionAreaEmpty,
        RegionalPopulationEmpty,
        DistributionAcreageEmpty,
        UrbanizationLevelEmpty,
        NumberOfPointsOfSaleEmpty,
        NumberOfKeyCustomerEmpty,
        MarketCharacteristicsEmpty,
        StoreAcreageEmpty,
        AbilityToPayEmpty,
        StoreMeansOfDeliveryCannotBeNull,
        StoreAssetsCannotBeNull,
        DeliveryTimeCannotBeNull,
        RelationshipCustomerTypeCannotBeNull,
        InfulenceLevelMarketCannotBeNull,
        MarketPriceCannotBeNull,
        PersonnelCannotBeNull,
        WarrantyServiceCannotBeNull,
        CoverageCapacityCannotBeNull,
        StoreConsultingServiceCannotBeNull,
        AnotherStrongPointEmpty,
        CooperativeAttitudeCannotBeNull,
        ImproveQualityServingCannotBeNull,
    }

}