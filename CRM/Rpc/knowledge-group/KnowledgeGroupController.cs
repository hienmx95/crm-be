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
using CRM.Services.MKnowledgeGroup;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.knowledge_group
{
    public partial class KnowledgeGroupController : RpcController
    {
        private IStatusService StatusService;
        private IKnowledgeGroupService KnowledgeGroupService;
        private ICurrentContext CurrentContext;
        public KnowledgeGroupController(
            IStatusService StatusService,
            IKnowledgeGroupService KnowledgeGroupService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.KnowledgeGroupService = KnowledgeGroupService;
            this.CurrentContext = CurrentContext;
        }

        [Route(KnowledgeGroupRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] KnowledgeGroup_KnowledgeGroupFilterDTO KnowledgeGroup_KnowledgeGroupFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeGroupFilter KnowledgeGroupFilter = ConvertFilterDTOToFilterEntity(KnowledgeGroup_KnowledgeGroupFilterDTO);
            KnowledgeGroupFilter = KnowledgeGroupService.ToFilter(KnowledgeGroupFilter);
            int count = await KnowledgeGroupService.Count(KnowledgeGroupFilter);
            return count;
        }

        [Route(KnowledgeGroupRoute.List), HttpPost]
        public async Task<ActionResult<List<KnowledgeGroup_KnowledgeGroupDTO>>> List([FromBody] KnowledgeGroup_KnowledgeGroupFilterDTO KnowledgeGroup_KnowledgeGroupFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeGroupFilter KnowledgeGroupFilter = ConvertFilterDTOToFilterEntity(KnowledgeGroup_KnowledgeGroupFilterDTO);
            KnowledgeGroupFilter = KnowledgeGroupService.ToFilter(KnowledgeGroupFilter);
            List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);
            List<KnowledgeGroup_KnowledgeGroupDTO> KnowledgeGroup_KnowledgeGroupDTOs = KnowledgeGroups
                .Select(c => new KnowledgeGroup_KnowledgeGroupDTO(c)).ToList();
            return KnowledgeGroup_KnowledgeGroupDTOs;
        }

        [Route(KnowledgeGroupRoute.Get), HttpPost]
        public async Task<ActionResult<KnowledgeGroup_KnowledgeGroupDTO>> Get([FromBody]KnowledgeGroup_KnowledgeGroupDTO KnowledgeGroup_KnowledgeGroupDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(KnowledgeGroup_KnowledgeGroupDTO.Id))
                return Forbid();

            KnowledgeGroup KnowledgeGroup = await KnowledgeGroupService.Get(KnowledgeGroup_KnowledgeGroupDTO.Id);
            return new KnowledgeGroup_KnowledgeGroupDTO(KnowledgeGroup);
        }

        [Route(KnowledgeGroupRoute.Create), HttpPost]
        public async Task<ActionResult<KnowledgeGroup_KnowledgeGroupDTO>> Create([FromBody] KnowledgeGroup_KnowledgeGroupDTO KnowledgeGroup_KnowledgeGroupDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(KnowledgeGroup_KnowledgeGroupDTO.Id))
                return Forbid();

            KnowledgeGroup KnowledgeGroup = ConvertDTOToEntity(KnowledgeGroup_KnowledgeGroupDTO);
            KnowledgeGroup = await KnowledgeGroupService.Create(KnowledgeGroup);
            KnowledgeGroup_KnowledgeGroupDTO = new KnowledgeGroup_KnowledgeGroupDTO(KnowledgeGroup);
            if (KnowledgeGroup.IsValidated)
                return KnowledgeGroup_KnowledgeGroupDTO;
            else
                return BadRequest(KnowledgeGroup_KnowledgeGroupDTO);
        }

        [Route(KnowledgeGroupRoute.Update), HttpPost]
        public async Task<ActionResult<KnowledgeGroup_KnowledgeGroupDTO>> Update([FromBody] KnowledgeGroup_KnowledgeGroupDTO KnowledgeGroup_KnowledgeGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(KnowledgeGroup_KnowledgeGroupDTO.Id))
                return Forbid();

            KnowledgeGroup KnowledgeGroup = ConvertDTOToEntity(KnowledgeGroup_KnowledgeGroupDTO);
            KnowledgeGroup = await KnowledgeGroupService.Update(KnowledgeGroup);
            KnowledgeGroup_KnowledgeGroupDTO = new KnowledgeGroup_KnowledgeGroupDTO(KnowledgeGroup);
            if (KnowledgeGroup.IsValidated)
                return KnowledgeGroup_KnowledgeGroupDTO;
            else
                return BadRequest(KnowledgeGroup_KnowledgeGroupDTO);
        }

        [Route(KnowledgeGroupRoute.Delete), HttpPost]
        public async Task<ActionResult<KnowledgeGroup_KnowledgeGroupDTO>> Delete([FromBody] KnowledgeGroup_KnowledgeGroupDTO KnowledgeGroup_KnowledgeGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(KnowledgeGroup_KnowledgeGroupDTO.Id))
                return Forbid();

            KnowledgeGroup KnowledgeGroup = ConvertDTOToEntity(KnowledgeGroup_KnowledgeGroupDTO);
            KnowledgeGroup = await KnowledgeGroupService.Delete(KnowledgeGroup);
            KnowledgeGroup_KnowledgeGroupDTO = new KnowledgeGroup_KnowledgeGroupDTO(KnowledgeGroup);
            if (KnowledgeGroup.IsValidated)
                return KnowledgeGroup_KnowledgeGroupDTO;
            else
                return BadRequest(KnowledgeGroup_KnowledgeGroupDTO);
        }
        
        [Route(KnowledgeGroupRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter();
            KnowledgeGroupFilter = KnowledgeGroupService.ToFilter(KnowledgeGroupFilter);
            KnowledgeGroupFilter.Id = new IdFilter { In = Ids };
            KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.Id;
            KnowledgeGroupFilter.Skip = 0;
            KnowledgeGroupFilter.Take = int.MaxValue;

            List<KnowledgeGroup> KnowledgeGroups = await KnowledgeGroupService.List(KnowledgeGroupFilter);
            KnowledgeGroups = await KnowledgeGroupService.BulkDelete(KnowledgeGroups);
            if (KnowledgeGroups.Any(x => !x.IsValidated))
                return BadRequest(KnowledgeGroups.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(KnowledgeGroupRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<KnowledgeGroup> KnowledgeGroups = new List<KnowledgeGroup>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(KnowledgeGroups);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int CodeColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int DisplayOrderColumn = 4 + StartColumn;
                int DescriptionColumn = 5 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string DisplayOrderValue = worksheet.Cells[i + StartRow, DisplayOrderColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    
                    KnowledgeGroup KnowledgeGroup = new KnowledgeGroup();
                    KnowledgeGroup.Name = NameValue;
                    KnowledgeGroup.Code = CodeValue;
                    KnowledgeGroup.DisplayOrder = long.TryParse(DisplayOrderValue, out long DisplayOrder) ? DisplayOrder : 0;
                    KnowledgeGroup.Description = DescriptionValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    KnowledgeGroup.StatusId = Status == null ? 0 : Status.Id;
                    KnowledgeGroup.Status = Status;
                    
                    KnowledgeGroups.Add(KnowledgeGroup);
                }
            }
            KnowledgeGroups = await KnowledgeGroupService.Import(KnowledgeGroups);
            if (KnowledgeGroups.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < KnowledgeGroups.Count; i++)
                {
                    KnowledgeGroup KnowledgeGroup = KnowledgeGroups[i];
                    if (!KnowledgeGroup.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (KnowledgeGroup.Errors.ContainsKey(nameof(KnowledgeGroup.Id)))
                            Error += KnowledgeGroup.Errors[nameof(KnowledgeGroup.Id)];
                        if (KnowledgeGroup.Errors.ContainsKey(nameof(KnowledgeGroup.Name)))
                            Error += KnowledgeGroup.Errors[nameof(KnowledgeGroup.Name)];
                        if (KnowledgeGroup.Errors.ContainsKey(nameof(KnowledgeGroup.Code)))
                            Error += KnowledgeGroup.Errors[nameof(KnowledgeGroup.Code)];
                        if (KnowledgeGroup.Errors.ContainsKey(nameof(KnowledgeGroup.StatusId)))
                            Error += KnowledgeGroup.Errors[nameof(KnowledgeGroup.StatusId)];
                        if (KnowledgeGroup.Errors.ContainsKey(nameof(KnowledgeGroup.DisplayOrder)))
                            Error += KnowledgeGroup.Errors[nameof(KnowledgeGroup.DisplayOrder)];
                        if (KnowledgeGroup.Errors.ContainsKey(nameof(KnowledgeGroup.Description)))
                            Error += KnowledgeGroup.Errors[nameof(KnowledgeGroup.Description)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(KnowledgeGroupRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] KnowledgeGroup_KnowledgeGroupFilterDTO KnowledgeGroup_KnowledgeGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region KnowledgeGroup
                var KnowledgeGroupFilter = ConvertFilterDTOToFilterEntity(KnowledgeGroup_KnowledgeGroupFilterDTO);
                KnowledgeGroupFilter.Skip = 0;
                KnowledgeGroupFilter.Take = int.MaxValue;
                KnowledgeGroupFilter = KnowledgeGroupService.ToFilter(KnowledgeGroupFilter);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "KnowledgeGroup.xlsx");
        }

        [Route(KnowledgeGroupRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] KnowledgeGroup_KnowledgeGroupFilterDTO KnowledgeGroup_KnowledgeGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region KnowledgeGroup
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
            return File(memoryStream.ToArray(), "application/octet-stream", "KnowledgeGroup.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter();
            KnowledgeGroupFilter = KnowledgeGroupService.ToFilter(KnowledgeGroupFilter);
            if (Id == 0)
            {

            }
            else
            {
                KnowledgeGroupFilter.Id = new IdFilter { Equal = Id };
                int count = await KnowledgeGroupService.Count(KnowledgeGroupFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private KnowledgeGroup ConvertDTOToEntity(KnowledgeGroup_KnowledgeGroupDTO KnowledgeGroup_KnowledgeGroupDTO)
        {
            KnowledgeGroup KnowledgeGroup = new KnowledgeGroup();
            KnowledgeGroup.Id = KnowledgeGroup_KnowledgeGroupDTO.Id;
            KnowledgeGroup.Name = KnowledgeGroup_KnowledgeGroupDTO.Name;
            KnowledgeGroup.Code = KnowledgeGroup_KnowledgeGroupDTO.Code;
            KnowledgeGroup.StatusId = KnowledgeGroup_KnowledgeGroupDTO.StatusId;
            KnowledgeGroup.DisplayOrder = KnowledgeGroup_KnowledgeGroupDTO.DisplayOrder;
            KnowledgeGroup.Description = KnowledgeGroup_KnowledgeGroupDTO.Description;
            KnowledgeGroup.Status = KnowledgeGroup_KnowledgeGroupDTO.Status == null ? null : new Status
            {
                Id = KnowledgeGroup_KnowledgeGroupDTO.Status.Id,
                Code = KnowledgeGroup_KnowledgeGroupDTO.Status.Code,
                Name = KnowledgeGroup_KnowledgeGroupDTO.Status.Name,
            };
            KnowledgeGroup.BaseLanguage = CurrentContext.Language;
            return KnowledgeGroup;
        }

        private KnowledgeGroupFilter ConvertFilterDTOToFilterEntity(KnowledgeGroup_KnowledgeGroupFilterDTO KnowledgeGroup_KnowledgeGroupFilterDTO)
        {
            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter();
            KnowledgeGroupFilter.Selects = KnowledgeGroupSelect.ALL;
            KnowledgeGroupFilter.Skip = KnowledgeGroup_KnowledgeGroupFilterDTO.Skip;
            KnowledgeGroupFilter.Take = KnowledgeGroup_KnowledgeGroupFilterDTO.Take;
            KnowledgeGroupFilter.OrderBy = KnowledgeGroup_KnowledgeGroupFilterDTO.OrderBy;
            KnowledgeGroupFilter.OrderType = KnowledgeGroup_KnowledgeGroupFilterDTO.OrderType;

            KnowledgeGroupFilter.Id = KnowledgeGroup_KnowledgeGroupFilterDTO.Id;
            KnowledgeGroupFilter.Name = KnowledgeGroup_KnowledgeGroupFilterDTO.Name;
            KnowledgeGroupFilter.Code = KnowledgeGroup_KnowledgeGroupFilterDTO.Code;
            KnowledgeGroupFilter.StatusId = KnowledgeGroup_KnowledgeGroupFilterDTO.StatusId;
            KnowledgeGroupFilter.DisplayOrder = KnowledgeGroup_KnowledgeGroupFilterDTO.DisplayOrder;
            KnowledgeGroupFilter.Description = KnowledgeGroup_KnowledgeGroupFilterDTO.Description;
            KnowledgeGroupFilter.CreatedAt = KnowledgeGroup_KnowledgeGroupFilterDTO.CreatedAt;
            KnowledgeGroupFilter.UpdatedAt = KnowledgeGroup_KnowledgeGroupFilterDTO.UpdatedAt;
            return KnowledgeGroupFilter;
        }
    }
}

