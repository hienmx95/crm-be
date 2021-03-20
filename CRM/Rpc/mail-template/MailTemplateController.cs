using CRM.Common;
using CRM.Entities;
using CRM.Models;
using CRM.Services.MMailTemplate;
using CRM.Services.MStatus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.mail_template
{
    public partial class MailTemplateController : RpcController
    {
        private IStatusService StatusService;
        private IMailTemplateService MailTemplateService;
        private ICurrentContext CurrentContext;
        public MailTemplateController(
            IStatusService StatusService,
            IMailTemplateService MailTemplateService,
            ICurrentContext CurrentContext
      ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.MailTemplateService = MailTemplateService;
            this.CurrentContext = CurrentContext;
        }

        [Route(MailTemplateRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] MailTemplate_MailTemplateFilterDTO MailTemplate_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = ConvertFilterDTOToFilterEntity(MailTemplate_MailTemplateFilterDTO);
            MailTemplateFilter = MailTemplateService.ToFilter(MailTemplateFilter);
            int count = await MailTemplateService.Count(MailTemplateFilter);
            return count;
        }

        [Route(MailTemplateRoute.List), HttpPost]
        public async Task<ActionResult<List<MailTemplate_MailTemplateDTO>>> List([FromBody] MailTemplate_MailTemplateFilterDTO MailTemplate_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = ConvertFilterDTOToFilterEntity(MailTemplate_MailTemplateFilterDTO);
            MailTemplateFilter = MailTemplateService.ToFilter(MailTemplateFilter);
            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            List<MailTemplate_MailTemplateDTO> MailTemplate_MailTemplateDTOs = MailTemplates
                .Select(c => new MailTemplate_MailTemplateDTO(c)).ToList();
            return MailTemplate_MailTemplateDTOs;
        }

        [Route(MailTemplateRoute.Get), HttpPost]
        public async Task<ActionResult<MailTemplate_MailTemplateDTO>> Get([FromBody]MailTemplate_MailTemplateDTO MailTemplate_MailTemplateDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(MailTemplate_MailTemplateDTO.Id))
                return Forbid();

            MailTemplate MailTemplate = await MailTemplateService.Get(MailTemplate_MailTemplateDTO.Id);
            return new MailTemplate_MailTemplateDTO(MailTemplate);
        }

        [Route(MailTemplateRoute.Create), HttpPost]
        public async Task<ActionResult<MailTemplate_MailTemplateDTO>> Create([FromBody] MailTemplate_MailTemplateDTO MailTemplate_MailTemplateDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(MailTemplate_MailTemplateDTO.Id))
                return Forbid();

            MailTemplate MailTemplate = ConvertDTOToEntity(MailTemplate_MailTemplateDTO);
            MailTemplate = await MailTemplateService.Create(MailTemplate);
            MailTemplate_MailTemplateDTO = new MailTemplate_MailTemplateDTO(MailTemplate);
            if (MailTemplate.IsValidated)
                return MailTemplate_MailTemplateDTO;
            else
                return BadRequest(MailTemplate_MailTemplateDTO);
        }

        [Route(MailTemplateRoute.Update), HttpPost]
        public async Task<ActionResult<MailTemplate_MailTemplateDTO>> Update([FromBody] MailTemplate_MailTemplateDTO MailTemplate_MailTemplateDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(MailTemplate_MailTemplateDTO.Id))
                return Forbid();

            MailTemplate MailTemplate = ConvertDTOToEntity(MailTemplate_MailTemplateDTO);
            MailTemplate = await MailTemplateService.Update(MailTemplate);
            MailTemplate_MailTemplateDTO = new MailTemplate_MailTemplateDTO(MailTemplate);
            if (MailTemplate.IsValidated)
                return MailTemplate_MailTemplateDTO;
            else
                return BadRequest(MailTemplate_MailTemplateDTO);
        }

        [Route(MailTemplateRoute.Delete), HttpPost]
        public async Task<ActionResult<MailTemplate_MailTemplateDTO>> Delete([FromBody] MailTemplate_MailTemplateDTO MailTemplate_MailTemplateDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(MailTemplate_MailTemplateDTO.Id))
                return Forbid();

            MailTemplate MailTemplate = ConvertDTOToEntity(MailTemplate_MailTemplateDTO);
            MailTemplate = await MailTemplateService.Delete(MailTemplate);
            MailTemplate_MailTemplateDTO = new MailTemplate_MailTemplateDTO(MailTemplate);
            if (MailTemplate.IsValidated)
                return MailTemplate_MailTemplateDTO;
            else
                return BadRequest(MailTemplate_MailTemplateDTO);
        }
        
        [Route(MailTemplateRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter = MailTemplateService.ToFilter(MailTemplateFilter);
            MailTemplateFilter.Id = new IdFilter { In = Ids };
            MailTemplateFilter.Selects = MailTemplateSelect.Id;
            MailTemplateFilter.Skip = 0;
            MailTemplateFilter.Take = int.MaxValue;

            List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);
            MailTemplates = await MailTemplateService.BulkDelete(MailTemplates);
            if (MailTemplates.Any(x => !x.IsValidated))
                return BadRequest(MailTemplates.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(MailTemplateRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<MailTemplate> MailTemplates = new List<MailTemplate>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(MailTemplates);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int ContentColumn = 3 + StartColumn;
                int StatusIdColumn = 4 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string ContentValue = worksheet.Cells[i + StartRow, ContentColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    
                    MailTemplate MailTemplate = new MailTemplate();
                    MailTemplate.Code = CodeValue;
                    MailTemplate.Name = NameValue;
                    MailTemplate.Content = ContentValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    MailTemplate.StatusId = Status == null ? 0 : Status.Id;
                    MailTemplate.Status = Status;
                    
                    MailTemplates.Add(MailTemplate);
                }
            }
            MailTemplates = await MailTemplateService.Import(MailTemplates);
            if (MailTemplates.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < MailTemplates.Count; i++)
                {
                    MailTemplate MailTemplate = MailTemplates[i];
                    if (!MailTemplate.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (MailTemplate.Errors.ContainsKey(nameof(MailTemplate.Id)))
                            Error += MailTemplate.Errors[nameof(MailTemplate.Id)];
                        if (MailTemplate.Errors.ContainsKey(nameof(MailTemplate.Code)))
                            Error += MailTemplate.Errors[nameof(MailTemplate.Code)];
                        if (MailTemplate.Errors.ContainsKey(nameof(MailTemplate.Name)))
                            Error += MailTemplate.Errors[nameof(MailTemplate.Name)];
                        if (MailTemplate.Errors.ContainsKey(nameof(MailTemplate.Content)))
                            Error += MailTemplate.Errors[nameof(MailTemplate.Content)];
                        if (MailTemplate.Errors.ContainsKey(nameof(MailTemplate.StatusId)))
                            Error += MailTemplate.Errors[nameof(MailTemplate.StatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(MailTemplateRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] MailTemplate_MailTemplateFilterDTO MailTemplate_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region MailTemplate
                var MailTemplateFilter = ConvertFilterDTOToFilterEntity(MailTemplate_MailTemplateFilterDTO);
                MailTemplateFilter.Skip = 0;
                MailTemplateFilter.Take = int.MaxValue;
                MailTemplateFilter = MailTemplateService.ToFilter(MailTemplateFilter);
                List<MailTemplate> MailTemplates = await MailTemplateService.List(MailTemplateFilter);

                var MailTemplateHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Content",
                        "StatusId",
                    }
                };
                List<object[]> MailTemplateData = new List<object[]>();
                for (int i = 0; i < MailTemplates.Count; i++)
                {
                    var MailTemplate = MailTemplates[i];
                    MailTemplateData.Add(new Object[]
                    {
                        MailTemplate.Id,
                        MailTemplate.Code,
                        MailTemplate.Name,
                        MailTemplate.Content,
                        MailTemplate.StatusId,
                    });
                }
                excel.GenerateWorksheet("MailTemplate", MailTemplateHeaders, MailTemplateData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "MailTemplate.xlsx");
        }

        [Route(MailTemplateRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] MailTemplate_MailTemplateFilterDTO MailTemplate_MailTemplateFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region MailTemplate
                var MailTemplateHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Content",
                        "StatusId",
                    }
                };
                List<object[]> MailTemplateData = new List<object[]>();
                excel.GenerateWorksheet("MailTemplate", MailTemplateHeaders, MailTemplateData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "MailTemplate.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter = MailTemplateService.ToFilter(MailTemplateFilter);
            if (Id == 0)
            {

            }
            else
            {
                MailTemplateFilter.Id = new IdFilter { Equal = Id };
                int count = await MailTemplateService.Count(MailTemplateFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private MailTemplate ConvertDTOToEntity(MailTemplate_MailTemplateDTO MailTemplate_MailTemplateDTO)
        {
            MailTemplate MailTemplate = new MailTemplate();
            MailTemplate.Id = MailTemplate_MailTemplateDTO.Id;
            MailTemplate.Code = MailTemplate_MailTemplateDTO.Code;
            MailTemplate.Name = MailTemplate_MailTemplateDTO.Name;
            MailTemplate.Content = MailTemplate_MailTemplateDTO.Content;
            MailTemplate.StatusId = MailTemplate_MailTemplateDTO.StatusId;
            MailTemplate.Status = MailTemplate_MailTemplateDTO.Status == null ? null : new Status
            {
                Id = MailTemplate_MailTemplateDTO.Status.Id,
                Code = MailTemplate_MailTemplateDTO.Status.Code,
                Name = MailTemplate_MailTemplateDTO.Status.Name,
            };
            MailTemplate.BaseLanguage = CurrentContext.Language;
            return MailTemplate;
        }

        private MailTemplateFilter ConvertFilterDTOToFilterEntity(MailTemplate_MailTemplateFilterDTO MailTemplate_MailTemplateFilterDTO)
        {
            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter();
            MailTemplateFilter.Selects = MailTemplateSelect.ALL;
            MailTemplateFilter.Skip = MailTemplate_MailTemplateFilterDTO.Skip;
            MailTemplateFilter.Take = MailTemplate_MailTemplateFilterDTO.Take;
            MailTemplateFilter.OrderBy = MailTemplate_MailTemplateFilterDTO.OrderBy;
            MailTemplateFilter.OrderType = MailTemplate_MailTemplateFilterDTO.OrderType;

            MailTemplateFilter.Id = MailTemplate_MailTemplateFilterDTO.Id;
            MailTemplateFilter.Code = MailTemplate_MailTemplateFilterDTO.Code;
            MailTemplateFilter.Name = MailTemplate_MailTemplateFilterDTO.Name;
            MailTemplateFilter.Content = MailTemplate_MailTemplateFilterDTO.Content;
            MailTemplateFilter.StatusId = MailTemplate_MailTemplateFilterDTO.StatusId;
            MailTemplateFilter.CreatedAt = MailTemplate_MailTemplateFilterDTO.CreatedAt;
            MailTemplateFilter.UpdatedAt = MailTemplate_MailTemplateFilterDTO.UpdatedAt;
            return MailTemplateFilter;
        }
    }
}

