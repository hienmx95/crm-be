using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MContract;
using CRM.Services.MCompany;
using CRM.Services.MAppUser;
using CRM.Services.MContact;
using CRM.Services.MContractStatus;
using CRM.Services.MContractType;
using CRM.Services.MCurrency;
using CRM.Services.MCustomer;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProvince;
using CRM.Services.MOpportunity;
using CRM.Services.MOrganization;
using CRM.Services.MPaymentStatus;
using CRM.Enums;

namespace CRM.Rpc.contract
{
    public partial class ContractController : RpcController
    {
        #region Item
        [Route(ContractRoute.ListItem), HttpPost]
        public async Task<ActionResult<List<Contract_ItemDTO>>> ListItem([FromBody] Contract_ItemFilterDTO Contract_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = Contract_ItemFilterDTO.Skip;
            ItemFilter.Take = Contract_ItemFilterDTO.Take;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = Contract_ItemFilterDTO.Id;
            ItemFilter.Code = Contract_ItemFilterDTO.Code;
            ItemFilter.Name = Contract_ItemFilterDTO.Name;
            ItemFilter.ProductId = Contract_ItemFilterDTO.ProductId;
            ItemFilter.RetailPrice = Contract_ItemFilterDTO.RetailPrice;
            ItemFilter.SalePrice = Contract_ItemFilterDTO.SalePrice;
            ItemFilter.ScanCode = Contract_ItemFilterDTO.ScanCode;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            ItemFilter = ItemService.ToFilter(ItemFilter);

            List<Item> Items = await ContractService.ListItem(ItemFilter);
            List<Contract_ItemDTO> Contract_ItemDTOs = Items
                .Select(x => new Contract_ItemDTO(x)).ToList();
            return Contract_ItemDTOs;
        }
        [Route(ContractRoute.CountItem), HttpPost]
        public async Task<ActionResult<long>> CountItem([FromBody] Contract_ItemFilterDTO Contract_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Id = Contract_ItemFilterDTO.Id;
            ItemFilter.Code = Contract_ItemFilterDTO.Code;
            ItemFilter.Name = Contract_ItemFilterDTO.Name;
            ItemFilter.ProductId = Contract_ItemFilterDTO.ProductId;
            ItemFilter.RetailPrice = Contract_ItemFilterDTO.RetailPrice;
            ItemFilter.SalePrice = Contract_ItemFilterDTO.SalePrice;
            ItemFilter.ScanCode = Contract_ItemFilterDTO.ScanCode;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            ItemFilter = ItemService.ToFilter(ItemFilter);

            return await ItemService.Count(ItemFilter);
        }
        #endregion

        #region Contact
        [Route(ContractRoute.CountContact), HttpPost]
        public async Task<ActionResult<int>> CountContact([FromBody] Contract_ContactFilterDTO Contract_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterContact(Contract_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            int count = await ContactService.Count(ContactFilter);
            return count;
        }

        [Route(ContractRoute.ListContact), HttpPost]
        public async Task<ActionResult<List<Contract_ContactDTO>>> ListContact([FromBody] Contract_ContactFilterDTO Contract_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterContact(Contract_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Contract_ContactDTO> Contract_ContactDTOs = Contacts
                .Select(c => new Contract_ContactDTO(c)).ToList();
            return Contract_ContactDTOs;
        }

        private ContactFilter ConvertFilterContact(Contract_ContactFilterDTO Contract_ContactFilterDTO)
        {
            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Skip = Contract_ContactFilterDTO.Skip;
            ContactFilter.Take = Contract_ContactFilterDTO.Take;
            ContactFilter.OrderBy = Contract_ContactFilterDTO.OrderBy;
            ContactFilter.OrderType = Contract_ContactFilterDTO.OrderType;

            ContactFilter.Id = Contract_ContactFilterDTO.Id;
            ContactFilter.Name = Contract_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Contract_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Contract_ContactFilterDTO.CompanyId;
            ContactFilter.ProvinceId = Contract_ContactFilterDTO.ProvinceId;
            ContactFilter.DistrictId = Contract_ContactFilterDTO.DistrictId;
            ContactFilter.NationId = Contract_ContactFilterDTO.NationId;
            ContactFilter.CustomerLeadId = Contract_ContactFilterDTO.CustomerLeadId;
            ContactFilter.ImageId = Contract_ContactFilterDTO.ImageId;
            ContactFilter.Description = Contract_ContactFilterDTO.Description;
            ContactFilter.Address = Contract_ContactFilterDTO.Address;
            ContactFilter.EmailOther = Contract_ContactFilterDTO.EmailOther;
            ContactFilter.DateOfBirth = Contract_ContactFilterDTO.DateOfBirth;
            ContactFilter.Phone = Contract_ContactFilterDTO.Phone;
            ContactFilter.PhoneHome = Contract_ContactFilterDTO.PhoneHome;
            ContactFilter.FAX = Contract_ContactFilterDTO.FAX;
            ContactFilter.Email = Contract_ContactFilterDTO.Email;
            ContactFilter.ZIPCode = Contract_ContactFilterDTO.ZIPCode;
            ContactFilter.SexId = Contract_ContactFilterDTO.SexId;
            ContactFilter.AppUserId = Contract_ContactFilterDTO.AppUserId;
            ContactFilter.PositionId = Contract_ContactFilterDTO.PositionId;
            ContactFilter.Department = Contract_ContactFilterDTO.Department;
            ContactFilter.ContactStatusId = Contract_ContactFilterDTO.ContactStatusId;
            return ContactFilter;
        }
        #endregion
    }

}