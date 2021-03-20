using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string CodeDraft { get; set; }
        public string Name { get; set; }
        public string UnsignName { get; set; }
        public long? ParentStoreId { get; set; }
        public long OrganizationId { get; set; }
        public long StoreTypeId { get; set; }
        public long? StoreGroupingId { get; set; }
        public long? ResellerId { get; set; }
        public string Telephone { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? WardId { get; set; }
        public string Address { get; set; }
        public string UnsignAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal? DeliveryLatitude { get; set; }
        public decimal? DeliveryLongitude { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }
        public string TaxCode { get; set; }
        public string LegalEntity { get; set; }
        public long? AppUserId { get; set; }
        public long StatusId { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }
        public long? StoreScoutingId { get; set; }
        public long StoreStatusId { get; set; }
        public long? CustomerAgentId { get; set; }
        public Store_AppUserDTO AppUser { get; set; }
        public Store_DistrictDTO District { get; set; }
        public Store_OrganizationDTO Organization { get; set; }
        public Store_StoreDTO ParentStore { get; set; }
        public Store_ProvinceDTO Province { get; set; }
        public Store_StatusDTO Status { get; set; }
        public Store_StoreGroupingDTO StoreGrouping { get; set; }
        public Store_StoreStatusDTO StoreStatus { get; set; }
        public Store_StoreTypeDTO StoreType { get; set; }
        public Store_WardDTO Ward { get; set; }
        public Store_StoreExtendDTO StoreExtend { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //public List<Store_StoreRepresentDTO> StoreRepresents { get; set; }
        //public List<Store_StorePersonnelDTO> StorePersonnels { get; set; }
        //public List<Store_StoreMeansOfDeliveryDTO> StoreMeansOfDeliveries { get; set; }
        //public List<Store_StoreAssetsDTO> StoreAssetses { get; set; }
        //public List<Store_StoreDeliveryTimeMappingDTO> StoreDeliveryTimeMappings { get; set; }
        //public List<Store_StoreRelationshipCustomerMappingDTO> StoreRelationshipCustomerMappings { get; set; }
        //public List<Store_StoreInfulenceLevelMarketMappingDTO> StoreInfulenceLevelMarketMappings { get; set; }
        //public List<Store_StoreMarketPriceMappingDTO> StoreMarketPriceMappings { get; set; }
        //public List<Store_StoreWarrantyServiceDTO> StoreWarrantyServices { get; set; }
        //public List<Store_StoreConsultingServiceMappingDTO> StoreConsultingServiceMappings { get; set; }
        //public List<Store_StoreCooperativeAttitudeMappingDTO> StoreCooperativeAttitudeMappings { get; set; }
        //public List<Store_BusinessConcentrationLevelDTO> BusinessConcentrationLevels { get; set; }
        //public List<Store_ImproveQualityServingDTO> ImproveQualityServings { get; set; }
        //public List<Store_StoreCoverageCapacityDTO> StoreCoverageCapacities { get; set; }

        public Store_StoreDTO() { }
        public Store_StoreDTO(Store Store)
        {
            this.Id = Store.Id;
            this.Code = Store.Code;
            this.CodeDraft = Store.CodeDraft;
            this.Name = Store.Name;
            this.UnsignName = Store.UnsignName;
            this.ParentStoreId = Store.ParentStoreId;
            this.OrganizationId = Store.OrganizationId;
            this.StoreTypeId = Store.StoreTypeId;
            this.StoreGroupingId = Store.StoreGroupingId;
            this.Telephone = Store.Telephone;
            this.ProvinceId = Store.ProvinceId;
            this.DistrictId = Store.DistrictId;
            this.WardId = Store.WardId;
            this.Address = Store.Address;
            this.UnsignAddress = Store.UnsignAddress;
            this.DeliveryAddress = Store.DeliveryAddress;
            this.Latitude = Store.Latitude;
            this.Longitude = Store.Longitude;
            this.DeliveryLatitude = Store.DeliveryLatitude;
            this.DeliveryLongitude = Store.DeliveryLongitude;
            this.OwnerName = Store.OwnerName;
            this.OwnerPhone = Store.OwnerPhone;
            this.OwnerEmail = Store.OwnerEmail;
            this.TaxCode = Store.TaxCode;
            this.LegalEntity = Store.LegalEntity;
            this.AppUserId = Store.AppUserId;
            this.StatusId = Store.StatusId;
            this.Used = Store.Used;
            this.StoreStatusId = Store.StoreStatusId;
            this.AppUser = Store.AppUser == null ? null : new Store_AppUserDTO(Store.AppUser);
            this.District = Store.District == null ? null : new Store_DistrictDTO(Store.District);
            this.Organization = Store.Organization == null ? null : new Store_OrganizationDTO(Store.Organization);
            this.ParentStore = Store.ParentStore == null ? null : new Store_StoreDTO(Store.ParentStore);
            this.Province = Store.Province == null ? null : new Store_ProvinceDTO(Store.Province);
            this.Status = Store.Status == null ? null : new Store_StatusDTO(Store.Status);
            this.StoreGrouping = Store.StoreGrouping == null ? null : new Store_StoreGroupingDTO(Store.StoreGrouping);
            this.StoreStatus = Store.StoreStatus == null ? null : new Store_StoreStatusDTO(Store.StoreStatus);
            this.StoreType = Store.StoreType == null ? null : new Store_StoreTypeDTO(Store.StoreType);
            this.Ward = Store.Ward == null ? null : new Store_WardDTO(Store.Ward);
            this.StoreExtend = Store.StoreExtend == null ? null : new Store_StoreExtendDTO(Store.StoreExtend);
            this.CreatedAt = Store.CreatedAt;
            this.UpdatedAt = Store.UpdatedAt;
            this.Errors = Store.Errors;

            //this.StoreRepresents = Store.StoreRepresents == null ? null : Store.StoreRepresents.Select(p => new Store_StoreRepresentDTO(p)).ToList();
            //this.StorePersonnels = Store.StorePersonnels == null ? null : Store.StorePersonnels.Select(p => new Store_StorePersonnelDTO(p)).ToList();
            //this.StoreMeansOfDeliveries = Store.StoreMeansOfDeliveries == null ? null : Store.StoreMeansOfDeliveries.Select(p => new Store_StoreMeansOfDeliveryDTO(p)).ToList();
            //this.StoreAssetses = Store.StoreAssetses == null ? null : Store.StoreAssetses.Select(p => new Store_StoreAssetsDTO(p)).ToList();
            //this.StoreDeliveryTimeMappings = Store.StoreDeliveryTimeMappings == null ? null : Store.StoreDeliveryTimeMappings.Select(p => new Store_StoreDeliveryTimeMappingDTO(p)).ToList();
            //this.StoreRelationshipCustomerMappings = Store.StoreRelationshipCustomerMappings == null ? null : Store.StoreRelationshipCustomerMappings.Select(p => new Store_StoreRelationshipCustomerMappingDTO(p)).ToList();
            //this.StoreInfulenceLevelMarketMappings = Store.StoreInfulenceLevelMarketMappings == null ? null : Store.StoreInfulenceLevelMarketMappings.Select(p => new Store_StoreInfulenceLevelMarketMappingDTO(p)).ToList();
            //this.StoreMarketPriceMappings = Store.StoreMarketPriceMappings == null ? null : Store.StoreMarketPriceMappings.Select(p => new Store_StoreMarketPriceMappingDTO(p)).ToList();
            //this.StoreWarrantyServices = Store.StoreWarrantyServices == null ? null : Store.StoreWarrantyServices.Select(p => new Store_StoreWarrantyServiceDTO(p)).ToList();
            //this.StoreCoverageCapacities = Store.StoreCoverageCapacities == null ? null : Store.StoreCoverageCapacities.Select(p => new Store_StoreCoverageCapacityDTO(p)).ToList();
            //this.StoreConsultingServiceMappings = Store.StoreConsultingServiceMappings == null ? null : Store.StoreConsultingServiceMappings.Select(p => new Store_StoreConsultingServiceMappingDTO(p)).ToList();
            //this.StoreCooperativeAttitudeMappings = Store.StoreCooperativeAttitudeMappings == null ? null : Store.StoreCooperativeAttitudeMappings.Select(p => new Store_StoreCooperativeAttitudeMappingDTO(p)).ToList();
            //this.BusinessConcentrationLevels = Store.BusinessConcentrationLevels == null ? null : Store.BusinessConcentrationLevels.Select(p => new Store_BusinessConcentrationLevelDTO(p)).ToList();
            //this.ImproveQualityServings = Store.ImproveQualityServings == null ? null : Store.ImproveQualityServings.Select(p => new Store_ImproveQualityServingDTO(p)).ToList();
        }
    }

    public class Store_StoreFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter CodeDraft { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter UnsignName { get; set; }
        public IdFilter ParentStoreId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter StoreTypeId { get; set; }
        public IdFilter StoreGroupingId { get; set; }
        public IdFilter ResellerId { get; set; }
        public StringFilter Telephone { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter WardId { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter UnsignAddress { get; set; }
        public StringFilter DeliveryAddress { get; set; }
        public DecimalFilter Latitude { get; set; }
        public DecimalFilter Longitude { get; set; }
        public DecimalFilter DeliveryLatitude { get; set; }
        public DecimalFilter DeliveryLongitude { get; set; }
        public StringFilter OwnerName { get; set; }
        public StringFilter OwnerPhone { get; set; }
        public StringFilter OwnerEmail { get; set; }
        public StringFilter TaxCode { get; set; }
        public StringFilter LegalEntity { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter StatusId { get; set; }
        public GuidFilter RowId { get; set; }
        public IdFilter StoreScoutingId { get; set; }
        public IdFilter StoreStatusId { get; set; }
        public IdFilter CustomerId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public StoreOrder OrderBy { get; set; }
    }
}
