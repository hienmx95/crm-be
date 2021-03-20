using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_StoreDTO : DataDTO
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
        public long StoreStatusId { get; set; }
        public long? CustomerId { get; set; }
        public Customer_AppUserDTO AppUser { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public Customer_DistrictDTO District { get; set; }
        public Customer_OrganizationDTO Organization { get; set; }
        public Customer_StoreDTO ParentStore { get; set; }
        public Customer_ProvinceDTO Province { get; set; }
        public Customer_StatusDTO Status { get; set; }
        public Customer_StoreGroupingDTO StoreGrouping { get; set; }
        public Customer_StoreStatusDTO StoreStatus { get; set; }
        public Customer_StoreTypeDTO StoreType { get; set; }
        public Customer_WardDTO Ward { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_StoreDTO() {}
        public Customer_StoreDTO(Store Store)
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
            this.RowId = Store.RowId;
            this.Used = Store.Used;
            this.StoreStatusId = Store.StoreStatusId;
            this.CustomerId = Store.CustomerId;
            this.AppUser = Store.AppUser == null ? null : new Customer_AppUserDTO(Store.AppUser);
            this.Customer = Store.Customer == null ? null : new Customer_CustomerDTO(Store.Customer);
            this.District = Store.District == null ? null : new Customer_DistrictDTO(Store.District);
            this.Organization = Store.Organization == null ? null : new Customer_OrganizationDTO(Store.Organization);
            this.ParentStore = Store.ParentStore == null ? null : new Customer_StoreDTO(Store.ParentStore);
            this.Province = Store.Province == null ? null : new Customer_ProvinceDTO(Store.Province);
            this.Status = Store.Status == null ? null : new Customer_StatusDTO(Store.Status);
            this.StoreGrouping = Store.StoreGrouping == null ? null : new Customer_StoreGroupingDTO(Store.StoreGrouping);
            this.StoreStatus = Store.StoreStatus == null ? null : new Customer_StoreStatusDTO(Store.StoreStatus);
            this.StoreType = Store.StoreType == null ? null : new Customer_StoreTypeDTO(Store.StoreType);
            this.Ward = Store.Ward == null ? null : new Customer_WardDTO(Store.Ward);
            this.CreatedAt = Store.CreatedAt;
            this.UpdatedAt = Store.UpdatedAt;
            this.Errors = Store.Errors;
        }
    }

    public class Customer_StoreFilterDTO : FilterDTO
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
        public IdFilter StoreStatusId { get; set; }
        public IdFilter CustomerId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public bool? isSelected { get; set; }
        public StoreOrder OrderBy { get; set; }
    }
}
