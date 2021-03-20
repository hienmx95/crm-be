using CRM.Common;
using CRM.Entities;
using CRM.Models;
using CRM.Services.MAppUser;
using CRM.Services.MBusinessType;
using CRM.Services.MConsultingService;
using CRM.Services.MCooperativeAttitude;
using CRM.Services.MCurrency;
using CRM.Services.MDistrict;
using CRM.Services.MInfulenceLevelMarket;
using CRM.Services.MMarketPrice;
using CRM.Services.MOrganization;
using CRM.Services.MPosition;
using CRM.Services.MProvince;
using CRM.Services.MRelationshipCustomerType;
using CRM.Services.MStatus;
using CRM.Services.MStore;
using CRM.Services.MStoreDeliveryTime;
using CRM.Services.MStoreGrouping;
using CRM.Services.MStoreStatus;
using CRM.Services.MStoreType;
using CRM.Services.MWard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.store
{
    public partial class StoreController : RpcController
    {
        private IAppUserService AppUserService;
        private IDistrictService DistrictService;
        private IOrganizationService OrganizationService;
        private IProvinceService ProvinceService;
        private IStatusService StatusService;
        private IStoreGroupingService StoreGroupingService;
        private IStoreStatusService StoreStatusService;
        private IStoreTypeService StoreTypeService;
        private IWardService WardService;
        private IStoreService StoreService;
        private IPositionService PositionService;
        private IBusinessTypeService BusinessTypeService;
        private IStoreDeliveryTimeService StoreDeliveryTimeService;
        private IInfulenceLevelMarketService InfulenceLevelMarketService;
        private IRelationshipCustomerTypeService RelationshipCustomerTypeService;
        private IMarketPriceService MarketPriceService;
        private IConsultingServiceService ConsultingServiceService;
        private ICooperativeAttitudeService CooperativeAttitudeService;
        private ICurrencyService CurrencyService;
        private ICurrentContext CurrentContext;
        public StoreController(
            IAppUserService AppUserService,
            IDistrictService DistrictService,
            IOrganizationService OrganizationService,
            IProvinceService ProvinceService,
            IStatusService StatusService,
            IStoreGroupingService StoreGroupingService,
            IStoreStatusService StoreStatusService,
            IStoreTypeService StoreTypeService,
            IWardService WardService,
            IStoreService StoreService,
            IBusinessTypeService BusinessTypeService,
            IPositionService PositionService,
            IStoreDeliveryTimeService StoreDeliveryTimeService,
            IRelationshipCustomerTypeService RelationshipCustomerTypeService,
            IInfulenceLevelMarketService InfulenceLevelMarketService,
            IMarketPriceService MarketPriceService,
            IConsultingServiceService ConsultingServiceService,
            ICooperativeAttitudeService CooperativeAttitudeService,
            ICurrencyService CurrencyService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.AppUserService = AppUserService;
            this.DistrictService = DistrictService;
            this.OrganizationService = OrganizationService;
            this.ProvinceService = ProvinceService;
            this.StatusService = StatusService;
            this.StoreGroupingService = StoreGroupingService;
            this.StoreStatusService = StoreStatusService;
            this.StoreTypeService = StoreTypeService;
            this.WardService = WardService;
            this.StoreService = StoreService;
            this.BusinessTypeService = BusinessTypeService;
            this.PositionService = PositionService;
            this.StoreDeliveryTimeService = StoreDeliveryTimeService;
            this.RelationshipCustomerTypeService = RelationshipCustomerTypeService;
            this.InfulenceLevelMarketService = InfulenceLevelMarketService;
            this.MarketPriceService = MarketPriceService;
            this.ConsultingServiceService = ConsultingServiceService;
            this.CooperativeAttitudeService = CooperativeAttitudeService;
            this.CurrencyService = CurrencyService;
            this.CurrentContext = CurrentContext;
        }

        [Route(StoreRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Store_StoreFilterDTO Store_StoreFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = ConvertFilterDTOToFilterEntity(Store_StoreFilterDTO);
            StoreFilter = StoreService.ToFilter(StoreFilter);
            int count = await StoreService.Count(StoreFilter);
            return count;
        }

        [Route(StoreRoute.List), HttpPost]
        public async Task<ActionResult<List<Store_StoreDTO>>> List([FromBody] Store_StoreFilterDTO Store_StoreFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = ConvertFilterDTOToFilterEntity(Store_StoreFilterDTO);
            StoreFilter = StoreService.ToFilter(StoreFilter);
            List<Store> Stores = await StoreService.List(StoreFilter);
            List<Store_StoreDTO> Store_StoreDTOs = Stores
                .Select(c => new Store_StoreDTO(c)).ToList();
            return Store_StoreDTOs;
        }

        [Route(StoreRoute.Get), HttpPost]
        public async Task<ActionResult<Store_StoreDTO>> Get([FromBody] Store_StoreDTO Store_StoreDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Store_StoreDTO.Id))
                return Forbid();

            Store Store = await StoreService.Get(Store_StoreDTO.Id);
            return new Store_StoreDTO(Store);
        }

        private async Task<bool> HasPermission(long Id)
        {
            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter = StoreService.ToFilter(StoreFilter);
            if (Id == 0)
            {

            }
            else
            {
                StoreFilter.Id = new IdFilter { Equal = Id };
                int count = await StoreService.Count(StoreFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Store ConvertDTOToEntity(Store_StoreDTO Store_StoreDTO)
        {
            Store Store = new Store();
            Store.Id = Store_StoreDTO.Id;
            Store.Code = Store_StoreDTO.Code;
            Store.CodeDraft = Store_StoreDTO.CodeDraft;
            Store.Name = Store_StoreDTO.Name;
            Store.UnsignName = Store_StoreDTO.UnsignName;
            Store.ParentStoreId = Store_StoreDTO.ParentStoreId;
            Store.OrganizationId = Store_StoreDTO.OrganizationId;
            Store.StoreTypeId = Store_StoreDTO.StoreTypeId;
            Store.StoreGroupingId = Store_StoreDTO.StoreGroupingId;
            Store.Telephone = Store_StoreDTO.Telephone;
            Store.ProvinceId = Store_StoreDTO.ProvinceId;
            Store.DistrictId = Store_StoreDTO.DistrictId;
            Store.WardId = Store_StoreDTO.WardId;
            Store.Address = Store_StoreDTO.Address;
            Store.UnsignAddress = Store_StoreDTO.UnsignAddress;
            Store.DeliveryAddress = Store_StoreDTO.DeliveryAddress;
            Store.Latitude = Store_StoreDTO.Latitude;
            Store.Longitude = Store_StoreDTO.Longitude;
            Store.DeliveryLatitude = Store_StoreDTO.DeliveryLatitude;
            Store.DeliveryLongitude = Store_StoreDTO.DeliveryLongitude;
            Store.OwnerName = Store_StoreDTO.OwnerName;
            Store.OwnerPhone = Store_StoreDTO.OwnerPhone;
            Store.OwnerEmail = Store_StoreDTO.OwnerEmail;
            Store.TaxCode = Store_StoreDTO.TaxCode;
            Store.LegalEntity = Store_StoreDTO.LegalEntity;
            Store.AppUserId = Store_StoreDTO.AppUserId;
            Store.StatusId = Store_StoreDTO.StatusId;
            Store.Used = Store_StoreDTO.Used;
            Store.StoreStatusId = Store_StoreDTO.StoreStatusId;
            Store.AppUser = Store_StoreDTO.AppUser == null ? null : new AppUser
            {
                Id = Store_StoreDTO.AppUser.Id,
                Username = Store_StoreDTO.AppUser.Username,
                DisplayName = Store_StoreDTO.AppUser.DisplayName,
                Address = Store_StoreDTO.AppUser.Address,
                Email = Store_StoreDTO.AppUser.Email,
                Phone = Store_StoreDTO.AppUser.Phone,
                SexId = Store_StoreDTO.AppUser.SexId,
                Birthday = Store_StoreDTO.AppUser.Birthday,
                Avatar = Store_StoreDTO.AppUser.Avatar,
                PositionId = Store_StoreDTO.AppUser.PositionId,
                Department = Store_StoreDTO.AppUser.Department,
                OrganizationId = Store_StoreDTO.AppUser.OrganizationId,
                ProvinceId = Store_StoreDTO.AppUser.ProvinceId,
                Longitude = Store_StoreDTO.AppUser.Longitude,
                Latitude = Store_StoreDTO.AppUser.Latitude,
                StatusId = Store_StoreDTO.AppUser.StatusId,
            };
            Store.District = Store_StoreDTO.District == null ? null : new District
            {
                Id = Store_StoreDTO.District.Id,
                Code = Store_StoreDTO.District.Code,
                Name = Store_StoreDTO.District.Name,
                Priority = Store_StoreDTO.District.Priority,
                ProvinceId = Store_StoreDTO.District.ProvinceId,
                StatusId = Store_StoreDTO.District.StatusId,
            };
            Store.Organization = Store_StoreDTO.Organization == null ? null : new Organization
            {
                Id = Store_StoreDTO.Organization.Id,
                Code = Store_StoreDTO.Organization.Code,
                Name = Store_StoreDTO.Organization.Name,
                ParentId = Store_StoreDTO.Organization.ParentId,
                Path = Store_StoreDTO.Organization.Path,
                Level = Store_StoreDTO.Organization.Level,
                StatusId = Store_StoreDTO.Organization.StatusId,
                Phone = Store_StoreDTO.Organization.Phone,
                Email = Store_StoreDTO.Organization.Email,
                Address = Store_StoreDTO.Organization.Address,
            };
            Store.ParentStore = Store_StoreDTO.ParentStore == null ? null : new Store
            {
                Id = Store_StoreDTO.ParentStore.Id,
                Code = Store_StoreDTO.ParentStore.Code,
                CodeDraft = Store_StoreDTO.ParentStore.CodeDraft,
                Name = Store_StoreDTO.ParentStore.Name,
                UnsignName = Store_StoreDTO.ParentStore.UnsignName,
                ParentStoreId = Store_StoreDTO.ParentStore.ParentStoreId,
                OrganizationId = Store_StoreDTO.ParentStore.OrganizationId,
                StoreTypeId = Store_StoreDTO.ParentStore.StoreTypeId,
                StoreGroupingId = Store_StoreDTO.ParentStore.StoreGroupingId,
                Telephone = Store_StoreDTO.ParentStore.Telephone,
                ProvinceId = Store_StoreDTO.ParentStore.ProvinceId,
                DistrictId = Store_StoreDTO.ParentStore.DistrictId,
                WardId = Store_StoreDTO.ParentStore.WardId,
                Address = Store_StoreDTO.ParentStore.Address,
                UnsignAddress = Store_StoreDTO.ParentStore.UnsignAddress,
                DeliveryAddress = Store_StoreDTO.ParentStore.DeliveryAddress,
                Latitude = Store_StoreDTO.ParentStore.Latitude,
                Longitude = Store_StoreDTO.ParentStore.Longitude,
                DeliveryLatitude = Store_StoreDTO.ParentStore.DeliveryLatitude,
                DeliveryLongitude = Store_StoreDTO.ParentStore.DeliveryLongitude,
                OwnerName = Store_StoreDTO.ParentStore.OwnerName,
                OwnerPhone = Store_StoreDTO.ParentStore.OwnerPhone,
                OwnerEmail = Store_StoreDTO.ParentStore.OwnerEmail,
                TaxCode = Store_StoreDTO.ParentStore.TaxCode,
                LegalEntity = Store_StoreDTO.ParentStore.LegalEntity,
                AppUserId = Store_StoreDTO.ParentStore.AppUserId,
                StatusId = Store_StoreDTO.ParentStore.StatusId,
                Used = Store_StoreDTO.ParentStore.Used,
                StoreStatusId = Store_StoreDTO.ParentStore.StoreStatusId,
            };
            Store.Province = Store_StoreDTO.Province == null ? null : new Province
            {
                Id = Store_StoreDTO.Province.Id,
                Code = Store_StoreDTO.Province.Code,
                Name = Store_StoreDTO.Province.Name,
                Priority = Store_StoreDTO.Province.Priority,
                StatusId = Store_StoreDTO.Province.StatusId,
            };
            Store.Status = Store_StoreDTO.Status == null ? null : new Status
            {
                Id = Store_StoreDTO.Status.Id,
                Code = Store_StoreDTO.Status.Code,
                Name = Store_StoreDTO.Status.Name,
            };
            Store.StoreGrouping = Store_StoreDTO.StoreGrouping == null ? null : new StoreGrouping
            {
                Id = Store_StoreDTO.StoreGrouping.Id,
                Code = Store_StoreDTO.StoreGrouping.Code,
                Name = Store_StoreDTO.StoreGrouping.Name,
                ParentId = Store_StoreDTO.StoreGrouping.ParentId,
                Path = Store_StoreDTO.StoreGrouping.Path,
                Level = Store_StoreDTO.StoreGrouping.Level,
                StatusId = Store_StoreDTO.StoreGrouping.StatusId,
            };
            Store.StoreStatus = Store_StoreDTO.StoreStatus == null ? null : new StoreStatus
            {
                Id = Store_StoreDTO.StoreStatus.Id,
                Code = Store_StoreDTO.StoreStatus.Code,
                Name = Store_StoreDTO.StoreStatus.Name,
            };
            Store.StoreType = Store_StoreDTO.StoreType == null ? null : new StoreType
            {
                Id = Store_StoreDTO.StoreType.Id,
                Code = Store_StoreDTO.StoreType.Code,
                Name = Store_StoreDTO.StoreType.Name,
                ColorId = Store_StoreDTO.StoreType.ColorId,
                StatusId = Store_StoreDTO.StoreType.StatusId,
                Used = Store_StoreDTO.StoreType.Used,
            };
            Store.Ward = Store_StoreDTO.Ward == null ? null : new Ward
            {
                Id = Store_StoreDTO.Ward.Id,
                Code = Store_StoreDTO.Ward.Code,
                Name = Store_StoreDTO.Ward.Name,
                Priority = Store_StoreDTO.Ward.Priority,
                DistrictId = Store_StoreDTO.Ward.DistrictId,
                StatusId = Store_StoreDTO.Ward.StatusId,
            };
            Store.StoreExtend = Store_StoreDTO.StoreExtend == null ? new StoreExtend() : new StoreExtend
            {
                StoreId = Store_StoreDTO.StoreExtend.StoreId,
                PhoneOther = Store_StoreDTO.StoreExtend.PhoneNumber,
                CompanyName = Store_StoreDTO.StoreExtend.CompanyName,
                Fax = Store_StoreDTO.StoreExtend.Fax,
                BusinessCapital = Store_StoreDTO.StoreExtend.BusinessCapital,
                BusinessTypeId = Store_StoreDTO.StoreExtend.BusinessTypeId,
                BankAccountNumber = Store_StoreDTO.StoreExtend.ATM,
                BusinessLicense = Store_StoreDTO.StoreExtend.BusinessLicense,
                BankName = Store_StoreDTO.StoreExtend.BankName,
                DateOfBusinessLicense = Store_StoreDTO.StoreExtend.DateOfBusinessLicense,
                AgentContractNumber = Store_StoreDTO.StoreExtend.AgentContractNumber,
                DateOfAgentContractNumber = Store_StoreDTO.StoreExtend.DateOfAgentContractNumber,
                DistributionArea = Store_StoreDTO.StoreExtend.DistributionArea,
                RegionalPopulation = Store_StoreDTO.StoreExtend.RegionalPopulation,
                DistributionAcreage = Store_StoreDTO.StoreExtend.DistributionAcreage,
                UrbanizationLevel = Store_StoreDTO.StoreExtend.UrbanizationLevel,
                NumberOfPointsOfSale = Store_StoreDTO.StoreExtend.NumberOfPointsOfSale,
                NumberOfKeyCustomer = Store_StoreDTO.StoreExtend.NumberOfKeyCustomer,
                MarketCharacteristics = Store_StoreDTO.StoreExtend.MarketCharacteristics,
                StoreAcreage = Store_StoreDTO.StoreExtend.StoreAcreage,
                AbilityToPay = Store_StoreDTO.StoreExtend.AbilityToPay,
                RewardInYear = Store_StoreDTO.StoreExtend.RewardInYear,
                AbilityRaisingCapital = Store_StoreDTO.StoreExtend.AbilityRaisingCapital,
                AbilityLimitedCapital = Store_StoreDTO.StoreExtend.AbilityLimitedCapital,
                DivideEachPart = Store_StoreDTO.StoreExtend.DivideEachPart,
                DivideHuman = Store_StoreDTO.StoreExtend.DivideHuman,
                AnotherStrongPoint = Store_StoreDTO.StoreExtend.AnotherStrongPoint,
                ReadyCoordinate = Store_StoreDTO.StoreExtend.ReadyCoordinate,
                Invest = Store_StoreDTO.StoreExtend.Invest,
                WareHouseAcreage = Store_StoreDTO.StoreExtend.WareHouseAcreage,
                CurrencyId = Store_StoreDTO.StoreExtend.CurrencyId,
            };
            //#region StoreRepresent
            //Store.StoreRepresents = Store_StoreDTO.StoreRepresents == null ? null : Store_StoreDTO.StoreRepresents.Select(p => new StoreRepresent
            //{
            //    Name = p.Name,
            //    DateOfBirth = p.DateOfBirth,
            //    Phone = p.Phone,
            //    Email = p.Email,
            //    PositionId = p.PositionId,
            //}).ToList();

            //#endregion
            //#region ImproveQualityServing
            //Store.ImproveQualityServings = Store_StoreDTO.ImproveQualityServings == null ? null : Store_StoreDTO.ImproveQualityServings.Select(p => new ImproveQualityServing
            //{
            //    Name = p.Name,
            //    Detail = p.Detail, 
            //}).ToList();

            //#endregion
            //#region StoreAssetses
            //Store.StoreAssetses = Store_StoreDTO.StoreAssetses == null ? null : Store_StoreDTO.StoreAssetses.Select(p => new StoreAssets
            //{
            //    Name = p.Name,
            //    Quantity = p.Quantity,
            //    Owned = p.Owned,
            //    Rent = p.Rent,
            //}).ToList();

            //#endregion
            //#region StoreConsultingServiceMappings
            //Store.StoreConsultingServiceMappings = Store_StoreDTO.StoreConsultingServiceMappings == null ? null : Store_StoreDTO.StoreConsultingServiceMappings.Select(p => new StoreConsultingServiceMapping
            //{
            //    ConsultingServiceId = p.ConsultingServiceId, 
            //}).ToList();

            //#endregion
            //#region storeCooperativeAttitudeMappings
            //Store.StoreCooperativeAttitudeMappings = Store_StoreDTO.StoreCooperativeAttitudeMappings == null ? null : Store_StoreDTO.StoreCooperativeAttitudeMappings.Select(p => new StoreCooperativeAttitudeMapping
            //{
            //    CooperativeAttitudeId = p.CooperativeAttitudeId,
            //}).ToList();

            //#endregion
            //#region StoreCoverageCapacities
            //Store.StoreCoverageCapacities = Store_StoreDTO.StoreCoverageCapacities == null ? null : Store_StoreDTO.StoreCoverageCapacities.Select(p => new StoreCoverageCapacity
            //{
            //    Name = p.Name,
            //    Detail = p.Detail,
            //}).ToList();

            //#endregion
            //#region StoreDeliveryTimeMappings
            //Store.StoreDeliveryTimeMappings = Store_StoreDTO.StoreDeliveryTimeMappings == null ? null : Store_StoreDTO.StoreDeliveryTimeMappings.Select(p => new StoreDeliveryTimeMapping
            //{
            //    StoreDeliveryTimeId = p.StoreDeliveryTimeId,
            //}).ToList();

            //#endregion
            //#region StoreInfulenceLevelMarketMappings
            //Store.StoreInfulenceLevelMarketMappings = Store_StoreDTO.StoreInfulenceLevelMarketMappings == null ? null : Store_StoreDTO.StoreInfulenceLevelMarketMappings.Select(p => new StoreInfulenceLevelMarketMapping
            //{
            //    InfulenceLevelMarketId = p.InfulenceLevelMarketId,
            //}).ToList();

            //#endregion
            //#region StoreMarketPriceMappings
            //Store.StoreMarketPriceMappings = Store_StoreDTO.StoreMarketPriceMappings == null ? null : Store_StoreDTO.StoreMarketPriceMappings.Select(p => new StoreMarketPriceMapping
            //{
            //    MarketPriceId = p.MarketPriceId,
            //}).ToList();

            //#endregion
            //#region StoreMeansOfDeliveries
            //Store.StoreMeansOfDeliveries = Store_StoreDTO.StoreMeansOfDeliveries == null ? null : Store_StoreDTO.StoreMeansOfDeliveries.Select(p => new StoreMeansOfDelivery
            //{
            //    Name = p.Name,
            //    Quantity = p.Quantity,
            //    Owned = p.Owned,
            //    Rent = p.Rent,
            //}).ToList();

            //#endregion
            //#region StorePersonnels
            //Store.StorePersonnels = Store_StoreDTO.StorePersonnels == null ? null : Store_StoreDTO.StorePersonnels.Select(p => new StorePersonnel
            //{
            //    Name = p.Name,
            //    Quantity = p.Quantity, 
            //}).ToList();

            //#endregion
            //#region StoreRelationshipCustomerMappings
            //Store.StoreRelationshipCustomerMappings = Store_StoreDTO.StoreRelationshipCustomerMappings == null ? null : Store_StoreDTO.StoreRelationshipCustomerMappings.Select(p => new StoreRelationshipCustomerMapping
            //{
            //    RelationshipCustomerTypeId = p.RelationshipCustomerTypeId,
            //}).ToList();

            //#endregion
            //#region StoreWarrantyServices
            //Store.StoreWarrantyServices = Store_StoreDTO.StoreWarrantyServices == null ? null : Store_StoreDTO.StoreWarrantyServices.Select(p => new StoreWarrantyService
            //{
            //    Name = p.Name,
            //    Detail = p.Detail,
            //}).ToList();

            //#endregion
            //#region BusinessConcentrationLevels
            //Store.BusinessConcentrationLevels = Store_StoreDTO.BusinessConcentrationLevels == null ? null : Store_StoreDTO.BusinessConcentrationLevels.Select(p => new BusinessConcentrationLevel
            //{
            //    Name = p.Name,
            //    Manufacturer = p.Manufacturer,
            //    Branch = p.Branch,
            //    RevenueInYear = p.RevenueInYear,
            //    MarketingStaff = p.MarketingStaff,
            //}).ToList();

            //#endregion
            Store.BaseLanguage = CurrentContext.Language;
            return Store;
        }

        private StoreFilter ConvertFilterDTOToFilterEntity(Store_StoreFilterDTO Store_StoreFilterDTO)
        {
            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Selects = StoreSelect.ALL;
            StoreFilter.Skip = Store_StoreFilterDTO.Skip;
            StoreFilter.Take = Store_StoreFilterDTO.Take;
            StoreFilter.OrderBy = Store_StoreFilterDTO.OrderBy;
            StoreFilter.OrderType = Store_StoreFilterDTO.OrderType;

            StoreFilter.Id = Store_StoreFilterDTO.Id;
            StoreFilter.Code = Store_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Store_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Store_StoreFilterDTO.Name;
            StoreFilter.UnsignName = Store_StoreFilterDTO.UnsignName;
            StoreFilter.ParentStoreId = Store_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Store_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Store_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Store_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Store_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Store_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Store_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Store_StoreFilterDTO.WardId;
            StoreFilter.Address = Store_StoreFilterDTO.Address;
            StoreFilter.UnsignAddress = Store_StoreFilterDTO.UnsignAddress;
            StoreFilter.DeliveryAddress = Store_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Store_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Store_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Store_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Store_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Store_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Store_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Store_StoreFilterDTO.OwnerEmail;
            StoreFilter.TaxCode = Store_StoreFilterDTO.TaxCode;
            StoreFilter.LegalEntity = Store_StoreFilterDTO.LegalEntity;
            StoreFilter.AppUserId = Store_StoreFilterDTO.AppUserId;
            StoreFilter.StatusId = Store_StoreFilterDTO.StatusId;
            StoreFilter.StoreStatusId = Store_StoreFilterDTO.StoreStatusId;
            StoreFilter.CustomerId = Store_StoreFilterDTO.CustomerId;
            StoreFilter.CreatedAt = Store_StoreFilterDTO.CreatedAt;
            StoreFilter.UpdatedAt = Store_StoreFilterDTO.UpdatedAt;
            return StoreFilter;
        }
    }
}

