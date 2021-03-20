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
using CRM.Services.MKnowledgeArticle;
using CRM.Services.MAppUser;
using CRM.Services.MKnowledgeGroup;
using CRM.Enums;

namespace CRM.Rpc.knowledge_article
{
    public partial class KnowledgeArticleController : RpcController
    {
        [Route(KnowledgeArticleRoute.FilterListAppUser), HttpPost]
        public async Task<List<KnowledgeArticle_AppUserDTO>> FilterListAppUser([FromBody] KnowledgeArticle_AppUserFilterDTO KnowledgeArticle_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = KnowledgeArticle_AppUserFilterDTO.Id;
            AppUserFilter.Username = KnowledgeArticle_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = KnowledgeArticle_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = KnowledgeArticle_AppUserFilterDTO.Address;
            AppUserFilter.Email = KnowledgeArticle_AppUserFilterDTO.Email;
            AppUserFilter.Phone = KnowledgeArticle_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = KnowledgeArticle_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = KnowledgeArticle_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = KnowledgeArticle_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = KnowledgeArticle_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = KnowledgeArticle_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = KnowledgeArticle_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = KnowledgeArticle_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = KnowledgeArticle_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = KnowledgeArticle_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = KnowledgeArticle_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<KnowledgeArticle_AppUserDTO> KnowledgeArticle_AppUserDTOs = AppUsers
                .Select(x => new KnowledgeArticle_AppUserDTO(x)).ToList();
            return KnowledgeArticle_AppUserDTOs;
        }
        [Route(KnowledgeArticleRoute.FilterListKnowledgeGroup), HttpPost]
        public async Task<List<KnowledgeArticle_KnowledgeGroupDTO>> FilterListKnowledgeGroup([FromBody] KnowledgeArticle_KnowledgeGroupFilterDTO KnowledgeArticle_KnowledgeGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter();
            KnowledgeGroupFilter.Skip = 0;
            KnowledgeGroupFilter.Take = 20;
            KnowledgeGroupFilter.OrderBy = KnowledgeGroupOrder.Id;
            KnowledgeGroupFilter.OrderType = OrderType.ASC;
            KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.ALL;
            KnowledgeGroupFilter.Id = KnowledgeArticle_KnowledgeGroupFilterDTO.Id;
            KnowledgeGroupFilter.Name = KnowledgeArticle_KnowledgeGroupFilterDTO.Name;
            KnowledgeGroupFilter.Code = KnowledgeArticle_KnowledgeGroupFilterDTO.Code;
            KnowledgeGroupFilter.StatusId = KnowledgeArticle_KnowledgeGroupFilterDTO.StatusId;
            KnowledgeGroupFilter.DisplayOrder = KnowledgeArticle_KnowledgeGroupFilterDTO.DisplayOrder;
            KnowledgeGroupFilter.Description = KnowledgeArticle_KnowledgeGroupFilterDTO.Description;

            List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);
            List<KnowledgeArticle_KnowledgeGroupDTO> KnowledgeArticle_KnowledgeGroupDTOs = KnowledgeGroups
                .Select(x => new KnowledgeArticle_KnowledgeGroupDTO(x)).ToList();
            return KnowledgeArticle_KnowledgeGroupDTOs;
        }

        [Route(KnowledgeArticleRoute.SingleListAppUser), HttpPost]
        public async Task<List<KnowledgeArticle_AppUserDTO>> SingleListAppUser([FromBody] KnowledgeArticle_AppUserFilterDTO KnowledgeArticle_AppUserFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = KnowledgeArticle_AppUserFilterDTO.Id;
            AppUserFilter.Username = KnowledgeArticle_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = KnowledgeArticle_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = KnowledgeArticle_AppUserFilterDTO.Address;
            AppUserFilter.Email = KnowledgeArticle_AppUserFilterDTO.Email;
            AppUserFilter.Phone = KnowledgeArticle_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = KnowledgeArticle_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = KnowledgeArticle_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = KnowledgeArticle_AppUserFilterDTO.Avatar;
            AppUserFilter.PositionId = KnowledgeArticle_AppUserFilterDTO.PositionId;
            AppUserFilter.Department = KnowledgeArticle_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = KnowledgeArticle_AppUserFilterDTO.OrganizationId;
            AppUserFilter.ProvinceId = KnowledgeArticle_AppUserFilterDTO.ProvinceId;
            AppUserFilter.Longitude = KnowledgeArticle_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = KnowledgeArticle_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = KnowledgeArticle_AppUserFilterDTO.StatusId;

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<KnowledgeArticle_AppUserDTO> KnowledgeArticle_AppUserDTOs = AppUsers
                .Select(x => new KnowledgeArticle_AppUserDTO(x)).ToList();
            return KnowledgeArticle_AppUserDTOs;
        }
        [Route(KnowledgeArticleRoute.SingleListKnowledgeGroup), HttpPost]
        public async Task<List<KnowledgeArticle_KnowledgeGroupDTO>> SingleListKnowledgeGroup([FromBody] KnowledgeArticle_KnowledgeGroupFilterDTO KnowledgeArticle_KnowledgeGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter();
            KnowledgeGroupFilter.Skip = 0;
            KnowledgeGroupFilter.Take = 20;
            KnowledgeGroupFilter.OrderBy = KnowledgeGroupOrder.Id;
            KnowledgeGroupFilter.OrderType = OrderType.ASC;
            KnowledgeGroupFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };
            KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.ALL;
            KnowledgeGroupFilter.Id = KnowledgeArticle_KnowledgeGroupFilterDTO.Id;
            KnowledgeGroupFilter.Name = KnowledgeArticle_KnowledgeGroupFilterDTO.Name;
            KnowledgeGroupFilter.Code = KnowledgeArticle_KnowledgeGroupFilterDTO.Code; 
            KnowledgeGroupFilter.DisplayOrder = KnowledgeArticle_KnowledgeGroupFilterDTO.DisplayOrder;
            KnowledgeGroupFilter.Description = KnowledgeArticle_KnowledgeGroupFilterDTO.Description;

            List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);
            List<KnowledgeArticle_KnowledgeGroupDTO> KnowledgeArticle_KnowledgeGroupDTOs = KnowledgeGroups
                .Select(x => new KnowledgeArticle_KnowledgeGroupDTO(x)).ToList();
            return KnowledgeArticle_KnowledgeGroupDTOs;
        }
        [Route(KnowledgeArticleRoute.SingleListStatus), HttpPost]
        public async Task<List<KnowledgeArticle_StatusDTO>> SingleListStatus([FromBody] KnowledgeArticle_StatusFilterDTO KnowledgeArticle_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<KnowledgeArticle_StatusDTO> KnowledgeArticle_StatusDTOs = Statuses
                .Select(x => new KnowledgeArticle_StatusDTO(x)).ToList();
            return KnowledgeArticle_StatusDTOs;
        }

        [Route(KnowledgeArticleRoute.FilterListStatus), HttpPost]
        public async Task<List<KnowledgeArticle_StatusDTO>> FilterListStatus([FromBody] KnowledgeArticle_StatusFilterDTO KnowledgeArticle_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<KnowledgeArticle_StatusDTO> KnowledgeArticle_StatusDTOs = Statuses
                .Select(x => new KnowledgeArticle_StatusDTO(x)).ToList();
            return KnowledgeArticle_StatusDTOs;
        }

        [Route(KnowledgeArticleRoute.FilterListOrganization), HttpPost]
        public async Task<List<KnowledgeArticle_OrganizationDTO>> FilterListOrganization([FromBody] KnowledgeArticle_OrganizationFilterDTO KnowledgeArticle_OrganizationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 20;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = KnowledgeArticle_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = KnowledgeArticle_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = KnowledgeArticle_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = KnowledgeArticle_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = KnowledgeArticle_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = KnowledgeArticle_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = KnowledgeArticle_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = KnowledgeArticle_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = KnowledgeArticle_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = KnowledgeArticle_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<KnowledgeArticle_OrganizationDTO> KnowledgeArticle_OrganizationDTOs = Organizations
                .Select(x => new KnowledgeArticle_OrganizationDTO(x)).ToList();
            return KnowledgeArticle_OrganizationDTOs;
        }
        [Route(KnowledgeArticleRoute.SingleListOrganization), HttpPost]
        public async Task<List<KnowledgeArticle_OrganizationDTO>> SingleListOrganization([FromBody] KnowledgeArticle_OrganizationFilterDTO KnowledgeArticle_OrganizationFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = 9999;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.Id = KnowledgeArticle_OrganizationFilterDTO.Id;
            OrganizationFilter.Code = KnowledgeArticle_OrganizationFilterDTO.Code;
            OrganizationFilter.Name = KnowledgeArticle_OrganizationFilterDTO.Name;
            OrganizationFilter.ParentId = KnowledgeArticle_OrganizationFilterDTO.ParentId;
            OrganizationFilter.Path = KnowledgeArticle_OrganizationFilterDTO.Path;
            OrganizationFilter.Level = KnowledgeArticle_OrganizationFilterDTO.Level;
            OrganizationFilter.StatusId = KnowledgeArticle_OrganizationFilterDTO.StatusId;
            OrganizationFilter.Phone = KnowledgeArticle_OrganizationFilterDTO.Phone;
            OrganizationFilter.Email = KnowledgeArticle_OrganizationFilterDTO.Email;
            OrganizationFilter.Address = KnowledgeArticle_OrganizationFilterDTO.Address;

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<KnowledgeArticle_OrganizationDTO> KnowledgeArticle_OrganizationDTOs = Organizations
                .Select(x => new KnowledgeArticle_OrganizationDTO(x)).ToList();
            return KnowledgeArticle_OrganizationDTOs;
        }

        [Route(KnowledgeArticleRoute.FilterListItem), HttpPost]
        public async Task<List<KnowledgeArticle_ItemDTO>> FilterListItem([FromBody] KnowledgeArticle_ItemFilterDTO KnowledgeArticle_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = KnowledgeArticle_ItemFilterDTO.Id;
            ItemFilter.ProductId = KnowledgeArticle_ItemFilterDTO.ProductId;
            ItemFilter.Code = KnowledgeArticle_ItemFilterDTO.Code;
            ItemFilter.Name = KnowledgeArticle_ItemFilterDTO.Name;
            ItemFilter.ScanCode = KnowledgeArticle_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = KnowledgeArticle_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = KnowledgeArticle_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = KnowledgeArticle_ItemFilterDTO.StatusId;

            List<Item> Items = await ItemService.List(ItemFilter);
            List<KnowledgeArticle_ItemDTO> KnowledgeArticle_ItemDTOs = Items
                .Select(x => new KnowledgeArticle_ItemDTO(x)).ToList();
            return KnowledgeArticle_ItemDTOs;
        }
        [Route(KnowledgeArticleRoute.SingleListItem), HttpPost]
        public async Task<List<KnowledgeArticle_ItemDTO>> SingleListItem([FromBody] KnowledgeArticle_ItemFilterDTO KnowledgeArticle_ItemFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = KnowledgeArticle_ItemFilterDTO.Id;
            ItemFilter.ProductId = KnowledgeArticle_ItemFilterDTO.ProductId;
            ItemFilter.Code = KnowledgeArticle_ItemFilterDTO.Code;
            ItemFilter.Name = KnowledgeArticle_ItemFilterDTO.Name;
            ItemFilter.ScanCode = KnowledgeArticle_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = KnowledgeArticle_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = KnowledgeArticle_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = KnowledgeArticle_ItemFilterDTO.StatusId;

            List<Item> Items = await ItemService.List(ItemFilter);
            List<KnowledgeArticle_ItemDTO> KnowledgeArticle_ItemDTOs = Items
                .Select(x => new KnowledgeArticle_ItemDTO(x)).ToList();
            return KnowledgeArticle_ItemDTOs;
        }
        [Route(KnowledgeArticleRoute.SingleListKMSStatus), HttpPost]
        public async Task<List<KnowledgeArticle_KMSStatusDTO>> SingleListKMSStatus([FromBody] KnowledgeArticle_KMSStatusFilterDTO KnowledgeArticle_KMSStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KMSStatusFilter KMSStatusFilter = new KMSStatusFilter();
            KMSStatusFilter.Skip = 0;
            KMSStatusFilter.Take = int.MaxValue;
            KMSStatusFilter.Take = 20;
            KMSStatusFilter.OrderBy = KMSStatusOrder.Id;
            KMSStatusFilter.OrderType = OrderType.ASC;
            KMSStatusFilter.Selects = KMSStatusSelect.ALL;

            List<KMSStatus> KMSStatuses = await KMSStatusService.List(KMSStatusFilter);
            List<KnowledgeArticle_KMSStatusDTO> KnowledgeArticle_KMSStatusDTOs = KMSStatuses
                .Select(x => new KnowledgeArticle_KMSStatusDTO(x)).ToList();
            return KnowledgeArticle_KMSStatusDTOs;
        }

        [Route(KnowledgeArticleRoute.FilterListKMSStatus), HttpPost]
        public async Task<List<KnowledgeArticle_KMSStatusDTO>> FilterListKMSStatus([FromBody] KnowledgeArticle_KMSStatusFilterDTO KnowledgeArticle_KMSStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KMSStatusFilter KMSStatusFilter = new KMSStatusFilter();
            KMSStatusFilter.Skip = 0;
            KMSStatusFilter.Take = int.MaxValue;
            KMSStatusFilter.Take = 20;
            KMSStatusFilter.OrderBy = KMSStatusOrder.Id;
            KMSStatusFilter.OrderType = OrderType.ASC;
            KMSStatusFilter.Selects = KMSStatusSelect.ALL;

            List<KMSStatus> KMSStatuses = await KMSStatusService.List(KMSStatusFilter);
            List<KnowledgeArticle_KMSStatusDTO> KnowledgeArticle_KMSStatusDTOs = KMSStatuses
                .Select(x => new KnowledgeArticle_KMSStatusDTO(x)).ToList();
            return KnowledgeArticle_KMSStatusDTOs;
        }

    }
}

