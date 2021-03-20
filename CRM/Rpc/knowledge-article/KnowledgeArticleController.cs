using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MKnowledgeArticle;
using CRM.Services.MAppUser;
using CRM.Services.MKnowledgeGroup;
using CRM.Services.MStatus;
using CRM.Services.MOrganization;
using CRM.Services.MProduct;
using CRM.Services.MKMSStatus;
using CRM.Models;

namespace CRM.Rpc.knowledge_article
{
    public partial class KnowledgeArticleController : RpcController
    {
        private IAppUserService AppUserService;
        private IKnowledgeGroupService KnowledgeGroupService;
        private IStatusService StatusService;
        private IKnowledgeArticleService KnowledgeArticleService;
        private IOrganizationService OrganizationService;
        private IItemService ItemService;
        private IKMSStatusService KMSStatusService;
        private ICurrentContext CurrentContext;
        public KnowledgeArticleController(
            IAppUserService AppUserService,
            IKnowledgeGroupService KnowledgeGroupService,
            IStatusService StatusService,
            IKnowledgeArticleService KnowledgeArticleService,
            IOrganizationService OrganizationService,
            IItemService ItemService,
            IKMSStatusService KMSStatusService,
            ICurrentContext CurrentContext
      ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.AppUserService = AppUserService;
            this.KnowledgeGroupService = KnowledgeGroupService;
            this.StatusService = StatusService;
            this.KnowledgeArticleService = KnowledgeArticleService;
            this.OrganizationService = OrganizationService;
            this.ItemService = ItemService;
            this.KMSStatusService = KMSStatusService;
            this.CurrentContext = CurrentContext;
        }

        [Route(KnowledgeArticleRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] KnowledgeArticle_KnowledgeArticleFilterDTO KnowledgeArticle_KnowledgeArticleFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeArticleFilter KnowledgeArticleFilter = ConvertFilterDTOToFilterEntity(KnowledgeArticle_KnowledgeArticleFilterDTO);
            KnowledgeArticleFilter = await KnowledgeArticleService.ToFilter(KnowledgeArticleFilter);
            int count = await KnowledgeArticleService.Count(KnowledgeArticleFilter);
            return count;
        }

        [Route(KnowledgeArticleRoute.List), HttpPost]
        public async Task<ActionResult<List<KnowledgeArticle_KnowledgeArticleDTO>>> List([FromBody] KnowledgeArticle_KnowledgeArticleFilterDTO KnowledgeArticle_KnowledgeArticleFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeArticleFilter KnowledgeArticleFilter = ConvertFilterDTOToFilterEntity(KnowledgeArticle_KnowledgeArticleFilterDTO);
            KnowledgeArticleFilter = await KnowledgeArticleService.ToFilter(KnowledgeArticleFilter);
            List<KnowledgeArticle> KnowledgeArticles = await KnowledgeArticleService.List(KnowledgeArticleFilter);
            List<KnowledgeArticle_KnowledgeArticleDTO> KnowledgeArticle_KnowledgeArticleDTOs = KnowledgeArticles
                .Select(c => new KnowledgeArticle_KnowledgeArticleDTO(c)).ToList();
            return KnowledgeArticle_KnowledgeArticleDTOs;
        }

        [Route(KnowledgeArticleRoute.Get), HttpPost]
        public async Task<ActionResult<KnowledgeArticle_KnowledgeArticleDTO>> Get([FromBody] KnowledgeArticle_KnowledgeArticleDTO KnowledgeArticle_KnowledgeArticleDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(KnowledgeArticle_KnowledgeArticleDTO.Id))
                return Forbid();

            KnowledgeArticle KnowledgeArticle = await KnowledgeArticleService.Get(KnowledgeArticle_KnowledgeArticleDTO.Id);
            return new KnowledgeArticle_KnowledgeArticleDTO(KnowledgeArticle);
        }

        [Route(KnowledgeArticleRoute.Create), HttpPost]
        public async Task<ActionResult<KnowledgeArticle_KnowledgeArticleDTO>> Create([FromBody] KnowledgeArticle_KnowledgeArticleDTO KnowledgeArticle_KnowledgeArticleDTO)
        {
            if (UnAuthorization) return Forbid();
            KnowledgeArticle_KnowledgeArticleDTO.CreatorId = CurrentContext.UserId;
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(KnowledgeArticle_KnowledgeArticleDTO.Id))
                return Forbid();

            KnowledgeArticle KnowledgeArticle = ConvertDTOToEntity(KnowledgeArticle_KnowledgeArticleDTO);
            KnowledgeArticle = await KnowledgeArticleService.Create(KnowledgeArticle);
            KnowledgeArticle_KnowledgeArticleDTO = new KnowledgeArticle_KnowledgeArticleDTO(KnowledgeArticle);
            if (KnowledgeArticle.IsValidated)
                return KnowledgeArticle_KnowledgeArticleDTO;
            else
                return BadRequest(KnowledgeArticle_KnowledgeArticleDTO);
        }

        [Route(KnowledgeArticleRoute.Update), HttpPost]
        public async Task<ActionResult<KnowledgeArticle_KnowledgeArticleDTO>> Update([FromBody] KnowledgeArticle_KnowledgeArticleDTO KnowledgeArticle_KnowledgeArticleDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(KnowledgeArticle_KnowledgeArticleDTO.Id))
                return Forbid();

            KnowledgeArticle KnowledgeArticle = ConvertDTOToEntity(KnowledgeArticle_KnowledgeArticleDTO);
            KnowledgeArticle = await KnowledgeArticleService.Update(KnowledgeArticle);
            KnowledgeArticle_KnowledgeArticleDTO = new KnowledgeArticle_KnowledgeArticleDTO(KnowledgeArticle);
            if (KnowledgeArticle.IsValidated)
                return KnowledgeArticle_KnowledgeArticleDTO;
            else
                return BadRequest(KnowledgeArticle_KnowledgeArticleDTO);
        }

        [Route(KnowledgeArticleRoute.Delete), HttpPost]
        public async Task<ActionResult<KnowledgeArticle_KnowledgeArticleDTO>> Delete([FromBody] KnowledgeArticle_KnowledgeArticleDTO KnowledgeArticle_KnowledgeArticleDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(KnowledgeArticle_KnowledgeArticleDTO.Id))
                return Forbid();

            KnowledgeArticle KnowledgeArticle = ConvertDTOToEntity(KnowledgeArticle_KnowledgeArticleDTO);
            KnowledgeArticle = await KnowledgeArticleService.Delete(KnowledgeArticle);
            KnowledgeArticle_KnowledgeArticleDTO = new KnowledgeArticle_KnowledgeArticleDTO(KnowledgeArticle);
            if (KnowledgeArticle.IsValidated)
                return KnowledgeArticle_KnowledgeArticleDTO;
            else
                return BadRequest(KnowledgeArticle_KnowledgeArticleDTO);
        }

        [Route(KnowledgeArticleRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeArticleFilter KnowledgeArticleFilter = new KnowledgeArticleFilter();
            KnowledgeArticleFilter = await KnowledgeArticleService.ToFilter(KnowledgeArticleFilter);
            KnowledgeArticleFilter.Id = new IdFilter { In = Ids };
            KnowledgeArticleFilter.Selects = KnowledgeArticleSelect.Id;
            KnowledgeArticleFilter.Skip = 0;
            KnowledgeArticleFilter.Take = int.MaxValue;

            List<KnowledgeArticle> KnowledgeArticles = await KnowledgeArticleService.List(KnowledgeArticleFilter);
            KnowledgeArticles = await KnowledgeArticleService.BulkDelete(KnowledgeArticles);
            if (KnowledgeArticles.Any(x => !x.IsValidated))
                return BadRequest(KnowledgeArticles.Where(x => !x.IsValidated));
            return true;
        }

        [Route(KnowledgeArticleRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            AppUserFilter CreatorFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Creators = await AppUserService.List(CreatorFilter);
            KnowledgeGroupFilter GroupFilter = new KnowledgeGroupFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = KnowledgeGroupSelect.ALL
            };
            List<KnowledgeGroup> Groups = await KnowledgeGroupService.List(GroupFilter);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<KnowledgeArticle> KnowledgeArticles = new List<KnowledgeArticle>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(KnowledgeArticles);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int TitleColumn = 1 + StartColumn;
                int DetailColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int GroupIdColumn = 4 + StartColumn;
                int CreatorIdColumn = 5 + StartColumn;
                int DisplayOrderColumn = 6 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string TitleValue = worksheet.Cells[i + StartRow, TitleColumn].Value?.ToString();
                    string DetailValue = worksheet.Cells[i + StartRow, DetailColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string GroupIdValue = worksheet.Cells[i + StartRow, GroupIdColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();
                    string DisplayOrderValue = worksheet.Cells[i + StartRow, DisplayOrderColumn].Value?.ToString();

                    KnowledgeArticle KnowledgeArticle = new KnowledgeArticle();
                    KnowledgeArticle.Title = TitleValue;
                    KnowledgeArticle.Detail = DetailValue;
                    KnowledgeArticle.DisplayOrder = long.TryParse(DisplayOrderValue, out long DisplayOrder) ? DisplayOrder : 0;
                    AppUser Creator = Creators.Where(x => x.Id.ToString() == CreatorIdValue).FirstOrDefault();
                    KnowledgeArticle.CreatorId = Creator == null ? 0 : Creator.Id;
                    KnowledgeArticle.Creator = Creator;
                    KnowledgeGroup Group = Groups.Where(x => x.Id.ToString() == GroupIdValue).FirstOrDefault();
                    KnowledgeArticle.GroupId = Group == null ? 0 : Group.Id;
                    KnowledgeArticle.Group = Group;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    KnowledgeArticle.StatusId = Status == null ? 0 : Status.Id;
                    KnowledgeArticle.Status = Status;

                    KnowledgeArticles.Add(KnowledgeArticle);
                }
            }
            KnowledgeArticles = await KnowledgeArticleService.Import(KnowledgeArticles);
            if (KnowledgeArticles.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < KnowledgeArticles.Count; i++)
                {
                    KnowledgeArticle KnowledgeArticle = KnowledgeArticles[i];
                    if (!KnowledgeArticle.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.Id)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.Id)];
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.Title)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.Title)];
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.Detail)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.Detail)];
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.StatusId)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.StatusId)];
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.GroupId)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.GroupId)];
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.CreatorId)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.CreatorId)];
                        if (KnowledgeArticle.Errors.ContainsKey(nameof(KnowledgeArticle.DisplayOrder)))
                            Error += KnowledgeArticle.Errors[nameof(KnowledgeArticle.DisplayOrder)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(KnowledgeArticleRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] KnowledgeArticle_KnowledgeArticleFilterDTO KnowledgeArticle_KnowledgeArticleFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region KnowledgeArticle
                var KnowledgeArticleFilter = ConvertFilterDTOToFilterEntity(KnowledgeArticle_KnowledgeArticleFilterDTO);
                KnowledgeArticleFilter.Skip = 0;
                KnowledgeArticleFilter.Take = int.MaxValue;
                KnowledgeArticleFilter = await KnowledgeArticleService.ToFilter(KnowledgeArticleFilter);
                List<KnowledgeArticle> KnowledgeArticles = await KnowledgeArticleService.List(KnowledgeArticleFilter);

                var KnowledgeArticleHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "Detail",
                        "StatusId",
                        "GroupId",
                        "CreatorId",
                        "DisplayOrder",
                    }
                };
                List<object[]> KnowledgeArticleData = new List<object[]>();
                for (int i = 0; i < KnowledgeArticles.Count; i++)
                {
                    var KnowledgeArticle = KnowledgeArticles[i];
                    KnowledgeArticleData.Add(new Object[]
                    {
                        KnowledgeArticle.Id,
                        KnowledgeArticle.Title,
                        KnowledgeArticle.Detail,
                        KnowledgeArticle.StatusId,
                        KnowledgeArticle.GroupId,
                        KnowledgeArticle.CreatorId,
                        KnowledgeArticle.DisplayOrder,
                    });
                }
                excel.GenerateWorksheet("KnowledgeArticle", KnowledgeArticleHeaders, KnowledgeArticleData);
                #endregion

                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region KnowledgeGroup
                var KnowledgeGroupFilter = new KnowledgeGroupFilter();
                KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.ALL;
                KnowledgeGroupFilter.OrderBy = KnowledgeGroupOrder.Id;
                KnowledgeGroupFilter.OrderType = OrderType.ASC;
                KnowledgeGroupFilter.Skip = 0;
                KnowledgeGroupFilter.Take = int.MaxValue;
                List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);

                var KnowledgeGroupHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "Code",
                        "StatusId",
                        "DisplayOrder",
                        "Description",
                    }
                };
                List<object[]> KnowledgeGroupData = new List<object[]>();
                for (int i = 0; i < KnowledgeGroups.Count; i++)
                {
                    var KnowledgeGroup = KnowledgeGroups[i];
                    KnowledgeGroupData.Add(new Object[]
                    {
                        KnowledgeGroup.Id,
                        KnowledgeGroup.Name,
                        KnowledgeGroup.Code,
                        KnowledgeGroup.StatusId,
                        KnowledgeGroup.DisplayOrder,
                        KnowledgeGroup.Description,
                    });
                }
                excel.GenerateWorksheet("KnowledgeGroup", KnowledgeGroupHeaders, KnowledgeGroupData);
                #endregion
                #region Status
                var StatusFilter = new StatusFilter();
                StatusFilter.Selects = StatusSelect.ALL;
                StatusFilter.OrderBy = StatusOrder.Id;
                StatusFilter.OrderType = OrderType.ASC;
                StatusFilter.Skip = 0;
                StatusFilter.Take = int.MaxValue;
                List<Status> Statuses = await StatusService.List(StatusFilter);

                var StatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> StatusData = new List<object[]>();
                for (int i = 0; i < Statuses.Count; i++)
                {
                    var Status = Statuses[i];
                    StatusData.Add(new Object[]
                    {
                        Status.Id,
                        Status.Code,
                        Status.Name,
                    });
                }
                excel.GenerateWorksheet("Status", StatusHeaders, StatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "KnowledgeArticle.xlsx");
        }

        [Route(KnowledgeArticleRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] KnowledgeArticle_KnowledgeArticleFilterDTO KnowledgeArticle_KnowledgeArticleFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region KnowledgeArticle
                var KnowledgeArticleHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "Detail",
                        "StatusId",
                        "GroupId",
                        "CreatorId",
                        "DisplayOrder",
                    }
                };
                List<object[]> KnowledgeArticleData = new List<object[]>();
                excel.GenerateWorksheet("KnowledgeArticle", KnowledgeArticleHeaders, KnowledgeArticleData);
                #endregion

                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region KnowledgeGroup
                var KnowledgeGroupFilter = new KnowledgeGroupFilter();
                KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.ALL;
                KnowledgeGroupFilter.OrderBy = KnowledgeGroupOrder.Id;
                KnowledgeGroupFilter.OrderType = OrderType.ASC;
                KnowledgeGroupFilter.Skip = 0;
                KnowledgeGroupFilter.Take = int.MaxValue;
                List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);

                var KnowledgeGroupHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "Code",
                        "StatusId",
                        "DisplayOrder",
                        "Description",
                    }
                };
                List<object[]> KnowledgeGroupData = new List<object[]>();
                for (int i = 0; i < KnowledgeGroups.Count; i++)
                {
                    var KnowledgeGroup = KnowledgeGroups[i];
                    KnowledgeGroupData.Add(new Object[]
                    {
                        KnowledgeGroup.Id,
                        KnowledgeGroup.Name,
                        KnowledgeGroup.Code,
                        KnowledgeGroup.StatusId,
                        KnowledgeGroup.DisplayOrder,
                        KnowledgeGroup.Description,
                    });
                }
                excel.GenerateWorksheet("KnowledgeGroup", KnowledgeGroupHeaders, KnowledgeGroupData);
                #endregion
                #region Status
                var StatusFilter = new StatusFilter();
                StatusFilter.Selects = StatusSelect.ALL;
                StatusFilter.OrderBy = StatusOrder.Id;
                StatusFilter.OrderType = OrderType.ASC;
                StatusFilter.Skip = 0;
                StatusFilter.Take = int.MaxValue;
                List<Status> Statuses = await StatusService.List(StatusFilter);

                var StatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> StatusData = new List<object[]>();
                for (int i = 0; i < Statuses.Count; i++)
                {
                    var Status = Statuses[i];
                    StatusData.Add(new Object[]
                    {
                        Status.Id,
                        Status.Code,
                        Status.Name,
                    });
                }
                excel.GenerateWorksheet("Status", StatusHeaders, StatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "KnowledgeArticle.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            KnowledgeArticleFilter KnowledgeArticleFilter = new KnowledgeArticleFilter();
            KnowledgeArticleFilter = await KnowledgeArticleService.ToFilter(KnowledgeArticleFilter);
            if (Id == 0)
            {

            }
            else
            {
                KnowledgeArticleFilter.Id = new IdFilter { Equal = Id };
                int count = await KnowledgeArticleService.Count(KnowledgeArticleFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private KnowledgeArticle ConvertDTOToEntity(KnowledgeArticle_KnowledgeArticleDTO KnowledgeArticle_KnowledgeArticleDTO)
        {
            KnowledgeArticle KnowledgeArticle = new KnowledgeArticle();
            KnowledgeArticle.Id = KnowledgeArticle_KnowledgeArticleDTO.Id;
            KnowledgeArticle.Title = KnowledgeArticle_KnowledgeArticleDTO.Title;
            KnowledgeArticle.Detail = KnowledgeArticle_KnowledgeArticleDTO.Detail;
            KnowledgeArticle.StatusId = KnowledgeArticle_KnowledgeArticleDTO.StatusId;
            KnowledgeArticle.GroupId = KnowledgeArticle_KnowledgeArticleDTO.GroupId;
            KnowledgeArticle.CreatorId = KnowledgeArticle_KnowledgeArticleDTO.CreatorId;
            KnowledgeArticle.DisplayOrder = KnowledgeArticle_KnowledgeArticleDTO.DisplayOrder;
            KnowledgeArticle.FromDate = KnowledgeArticle_KnowledgeArticleDTO.FromDate;
            KnowledgeArticle.ToDate = KnowledgeArticle_KnowledgeArticleDTO.ToDate;
            KnowledgeArticle.KMSStatusId = KnowledgeArticle_KnowledgeArticleDTO.KMSStatusId;
            KnowledgeArticle.ItemId = KnowledgeArticle_KnowledgeArticleDTO.ItemId;
            KnowledgeArticle.Creator = KnowledgeArticle_KnowledgeArticleDTO.Creator == null ? null : new AppUser
            {
                Id = KnowledgeArticle_KnowledgeArticleDTO.Creator.Id,
                Username = KnowledgeArticle_KnowledgeArticleDTO.Creator.Username,
                DisplayName = KnowledgeArticle_KnowledgeArticleDTO.Creator.DisplayName,
                Address = KnowledgeArticle_KnowledgeArticleDTO.Creator.Address,
                Email = KnowledgeArticle_KnowledgeArticleDTO.Creator.Email,
                Phone = KnowledgeArticle_KnowledgeArticleDTO.Creator.Phone,
                SexId = KnowledgeArticle_KnowledgeArticleDTO.Creator.SexId,
                Birthday = KnowledgeArticle_KnowledgeArticleDTO.Creator.Birthday,
                Avatar = KnowledgeArticle_KnowledgeArticleDTO.Creator.Avatar,
                PositionId = KnowledgeArticle_KnowledgeArticleDTO.Creator.PositionId,
                Department = KnowledgeArticle_KnowledgeArticleDTO.Creator.Department,
                OrganizationId = KnowledgeArticle_KnowledgeArticleDTO.Creator.OrganizationId,
                ProvinceId = KnowledgeArticle_KnowledgeArticleDTO.Creator.ProvinceId,
                Longitude = KnowledgeArticle_KnowledgeArticleDTO.Creator.Longitude,
                Latitude = KnowledgeArticle_KnowledgeArticleDTO.Creator.Latitude,
                StatusId = KnowledgeArticle_KnowledgeArticleDTO.Creator.StatusId,
            };
            KnowledgeArticle.Group = KnowledgeArticle_KnowledgeArticleDTO.Group == null ? null : new KnowledgeGroup
            {
                Id = KnowledgeArticle_KnowledgeArticleDTO.Group.Id,
                Name = KnowledgeArticle_KnowledgeArticleDTO.Group.Name,
                Code = KnowledgeArticle_KnowledgeArticleDTO.Group.Code,
                StatusId = KnowledgeArticle_KnowledgeArticleDTO.Group.StatusId,
                DisplayOrder = KnowledgeArticle_KnowledgeArticleDTO.Group.DisplayOrder,
                Description = KnowledgeArticle_KnowledgeArticleDTO.Group.Description,
            };
            KnowledgeArticle.Status = KnowledgeArticle_KnowledgeArticleDTO.Status == null ? null : new Status
            {
                Id = KnowledgeArticle_KnowledgeArticleDTO.Status.Id,
                Code = KnowledgeArticle_KnowledgeArticleDTO.Status.Code,
                Name = KnowledgeArticle_KnowledgeArticleDTO.Status.Name,
            };
            KnowledgeArticle.KnowledgeArticleOrganizationMappings = KnowledgeArticle_KnowledgeArticleDTO.KnowledgeArticleOrganizationMappings == null ? null : KnowledgeArticle_KnowledgeArticleDTO.KnowledgeArticleOrganizationMappings.Select(p => new KnowledgeArticleOrganizationMapping
            {
                OrganizationId = p.OrganizationId,
                KnowledgeArticleId = KnowledgeArticle_KnowledgeArticleDTO.Id
            }).ToList();
            KnowledgeArticle.KnowledgeArticleKeywords = KnowledgeArticle_KnowledgeArticleDTO.KnowledgeArticleKeywords == null ? null : KnowledgeArticle_KnowledgeArticleDTO.KnowledgeArticleKeywords.Select(p => new KnowledgeArticleKeyword
            {
                Name = p.Name,
            }).ToList();
            KnowledgeArticle.BaseLanguage = CurrentContext.Language;
            return KnowledgeArticle;
        }

        private KnowledgeArticleFilter ConvertFilterDTOToFilterEntity(KnowledgeArticle_KnowledgeArticleFilterDTO KnowledgeArticle_KnowledgeArticleFilterDTO)
        {
            KnowledgeArticleFilter KnowledgeArticleFilter = new KnowledgeArticleFilter();
            KnowledgeArticleFilter.Selects = KnowledgeArticleSelect.ALL;
            KnowledgeArticleFilter.Skip = KnowledgeArticle_KnowledgeArticleFilterDTO.Skip;
            KnowledgeArticleFilter.Take = KnowledgeArticle_KnowledgeArticleFilterDTO.Take;
            KnowledgeArticleFilter.OrderBy = KnowledgeArticle_KnowledgeArticleFilterDTO.OrderBy;
            KnowledgeArticleFilter.OrderType = KnowledgeArticle_KnowledgeArticleFilterDTO.OrderType;

            KnowledgeArticleFilter.Id = KnowledgeArticle_KnowledgeArticleFilterDTO.Id;
            KnowledgeArticleFilter.Title = KnowledgeArticle_KnowledgeArticleFilterDTO.Title;
            KnowledgeArticleFilter.Detail = KnowledgeArticle_KnowledgeArticleFilterDTO.Detail;
            KnowledgeArticleFilter.StatusId = KnowledgeArticle_KnowledgeArticleFilterDTO.StatusId;
            KnowledgeArticleFilter.GroupId = KnowledgeArticle_KnowledgeArticleFilterDTO.GroupId;
            KnowledgeArticleFilter.CreatorId = KnowledgeArticle_KnowledgeArticleFilterDTO.CreatorId;
            KnowledgeArticleFilter.DisplayOrder = KnowledgeArticle_KnowledgeArticleFilterDTO.DisplayOrder;
            KnowledgeArticleFilter.CreatedAt = KnowledgeArticle_KnowledgeArticleFilterDTO.CreatedAt;
            KnowledgeArticleFilter.UpdatedAt = KnowledgeArticle_KnowledgeArticleFilterDTO.UpdatedAt;

            KnowledgeArticleFilter.FromDate = KnowledgeArticle_KnowledgeArticleFilterDTO.FromDate;
            KnowledgeArticleFilter.ToDate = KnowledgeArticle_KnowledgeArticleFilterDTO.ToDate;
            KnowledgeArticleFilter.ItemId = KnowledgeArticle_KnowledgeArticleFilterDTO.ItemId;
            KnowledgeArticleFilter.KMSStatusId = KnowledgeArticle_KnowledgeArticleFilterDTO.KMSStatusId;
            KnowledgeArticleFilter.OrganizationId = KnowledgeArticle_KnowledgeArticleFilterDTO.OrganizationId;
            return KnowledgeArticleFilter;
        }



    }
}

