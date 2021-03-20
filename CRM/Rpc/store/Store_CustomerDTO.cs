using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_CustomerDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? StatusId { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? WardId { get; set; }
        public long? ImageId { get; set; }
        public long CreatorId { get; set; }
        public long? OrganizationId { get; set; }
        public long? ProfessionId { get; set; }
        public bool? Used { get; set; }
        //public Store_DistrictDTO District { get; set; }
        //public Store_NationDTO Nation { get; set; }
        //public Store_ProvinceDTO Province { get; set; }
        //public Store_StatusDTO Status { get; set; }
        //public Store_WardDTO Ward { get; set; }
        //public Store_ImageDTO Image { get; set; }
        //public Store_CustomerAgentDTO CustomerAgent { get; set; }
        //public Store_CustomerExportDTO CustomerExport { get; set; }
        //public Store_CustomerProjectDTO CustomerProject { get; set; }
        //public Store_CustomerRetailDTO CustomerRetail { get; set; }
        //public Store_ProfessionDTO Profession { get; set; }
        //public Store_AppUserDTO Creator { get; set; }
        //public Store_OrganizationDTO Organization { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        //public List<Store_CustomerCustomerTypeMappingDTO> CustomerCustomerTypeMappings { get; set; }
        public Store_CustomerDTO() { }
        public Store_CustomerDTO(Customer Customer)
        {
            this.Id = Customer.Id;
            this.Code = Customer.Code;
            this.Name = Customer.Name;
            this.Phone = Customer.Phone;
            this.Email = Customer.Email;
            this.Address = Customer.Address;
            this.StatusId = Customer.StatusId;
            this.NationId = Customer.NationId;
            this.ProvinceId = Customer.ProvinceId;
            this.DistrictId = Customer.DistrictId;
            this.WardId = Customer.WardId;
            this.ProfessionId = Customer.ProfessionId;
            this.Used = Customer.Used;
            //this.District = Customer.District == null ? null : new Store_DistrictDTO(Customer.District);
            //this.Nation = Customer.Nation == null ? null : new Store_NationDTO(Customer.Nation);
            //this.Province = Customer.Province == null ? null : new Store_ProvinceDTO(Customer.Province);
            //this.Status = Customer.Status == null ? null : new Store_StatusDTO(Customer.Status);
            //this.Ward = Customer.Ward == null ? null : new Store_WardDTO(Customer.Ward);
            //this.CustomerAgent = Customer.CustomerAgent == null ? null : new Store_CustomerAgentDTO(Customer.CustomerAgent);
            //this.CustomerExport = Customer.CustomerExport == null ? null : new Store_CustomerExportDTO(Customer.CustomerExport);
            //this.CustomerProject = Customer.CustomerProject == null ? null : new Store_CustomerProjectDTO(Customer.CustomerProject);
            //this.CustomerRetail = Customer.CustomerRetail == null ? null : new Store_CustomerRetailDTO(Customer.CustomerRetail);
            //this.Image = Customer.Image == null ? null : new Store_ImageDTO(Customer.Image);
            this.CreatedAt = Customer.CreatedAt;
            this.UpdatedAt = Customer.UpdatedAt;
            //this.Profession = Customer.Profession == null ? null : new Store_ProfessionDTO(Customer.Profession);
            //this.Creator = Customer.Creator == null ? null : new Store_AppUserDTO(Customer.Creator);
            //this.Organization = Customer.Organization == null ? null : new Store_OrganizationDTO(Customer.Organization);

            //this.CustomerCustomerTypeMappings = Customer.CustomerCustomerTypeMappings == null ? null : Customer.CustomerCustomerTypeMappings.Select(p => new Store_CustomerCustomerTypeMappingDTO(p)).ToList();
            this.Errors = Customer.Errors;
        }
    }

    public class Store_CustomerFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter IdentificationNumber { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter WardId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerOrder OrderBy { get; set; }
    }
}
