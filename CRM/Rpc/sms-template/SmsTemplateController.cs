using CRM.Common;
using CRM.Entities;
using CRM.Models;
using CRM.Services.MSmsTemplate;
using CRM.Services.MStatus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.sms_template
{
    public partial class SmsTemplateController : RpcController
    {
        private IStatusService StatusService;
        private ISmsTemplateService SmsTemplateService;
        private ICurrentContext CurrentContext;
        public SmsTemplateController(
            IStatusService StatusService,
            ISmsTemplateService SmsTemplateService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.SmsTemplateService = SmsTemplateService;
            this.CurrentContext = CurrentContext;
        }

        [Route(SmsTemplateRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] SmsTemplate_SmsTemplateFilterDTO SmsTemplate_SmsTemplateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SmsTemplateFilter SmsTemplateFilter = ConvertFilterDTOToFilterEntity(SmsTemplate_SmsTemplateFilterDTO);
            SmsTemplateFilter = SmsTemplateService.ToFilter(SmsTemplateFilter);
            int count = await SmsTemplateService.Count(SmsTemplateFilter);
            return count;
        }

        [Route(SmsTemplateRoute.List), HttpPost]
        public async Task<ActionResult<List<SmsTemplate_SmsTemplateDTO>>> List([FromBody] SmsTemplate_SmsTemplateFilterDTO SmsTemplate_SmsTemplateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SmsTemplateFilter SmsTemplateFilter = ConvertFilterDTOToFilterEntity(SmsTemplate_SmsTemplateFilterDTO);
            SmsTemplateFilter = SmsTemplateService.ToFilter(SmsTemplateFilter);
            List<SmsTemplate> SmsTemplates = await SmsTemplateService.List(SmsTemplateFilter);
            List<SmsTemplate_SmsTemplateDTO> SmsTemplate_SmsTemplateDTOs = SmsTemplates
                .Select(c => new SmsTemplate_SmsTemplateDTO(c)).ToList();
            return SmsTemplate_SmsTemplateDTOs;
        }

        [Route(SmsTemplateRoute.Get), HttpPost]
        public async Task<ActionResult<SmsTemplate_SmsTemplateDTO>> Get([FromBody] SmsTemplate_SmsTemplateDTO SmsTemplate_SmsTemplateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(SmsTemplate_SmsTemplateDTO.Id))
                return Forbid();

            SmsTemplate SmsTemplate = await SmsTemplateService.Get(SmsTemplate_SmsTemplateDTO.Id);
            return new SmsTemplate_SmsTemplateDTO(SmsTemplate);
        }

        [Route(SmsTemplateRoute.Create), HttpPost]
        public async Task<ActionResult<SmsTemplate_SmsTemplateDTO>> Create([FromBody] SmsTemplate_SmsTemplateDTO SmsTemplate_SmsTemplateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(SmsTemplate_SmsTemplateDTO.Id))
                return Forbid();

            SmsTemplate SmsTemplate = ConvertDTOToEntity(SmsTemplate_SmsTemplateDTO);
            SmsTemplate = await SmsTemplateService.Create(SmsTemplate);
            SmsTemplate_SmsTemplateDTO = new SmsTemplate_SmsTemplateDTO(SmsTemplate);
            if (SmsTemplate.IsValidated)
                return SmsTemplate_SmsTemplateDTO;
            else
                return BadRequest(SmsTemplate_SmsTemplateDTO);
        }

        [Route(SmsTemplateRoute.Update), HttpPost]
        public async Task<ActionResult<SmsTemplate_SmsTemplateDTO>> Update([FromBody] SmsTemplate_SmsTemplateDTO SmsTemplate_SmsTemplateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(SmsTemplate_SmsTemplateDTO.Id))
                return Forbid();

            SmsTemplate SmsTemplate = ConvertDTOToEntity(SmsTemplate_SmsTemplateDTO);
            SmsTemplate = await SmsTemplateService.Update(SmsTemplate);
            SmsTemplate_SmsTemplateDTO = new SmsTemplate_SmsTemplateDTO(SmsTemplate);
            if (SmsTemplate.IsValidated)
                return SmsTemplate_SmsTemplateDTO;
            else
                return BadRequest(SmsTemplate_SmsTemplateDTO);
        }

        [Route(SmsTemplateRoute.Delete), HttpPost]
        public async Task<ActionResult<SmsTemplate_SmsTemplateDTO>> Delete([FromBody] SmsTemplate_SmsTemplateDTO SmsTemplate_SmsTemplateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(SmsTemplate_SmsTemplateDTO.Id))
                return Forbid();

            SmsTemplate SmsTemplate = ConvertDTOToEntity(SmsTemplate_SmsTemplateDTO);
            SmsTemplate = await SmsTemplateService.Delete(SmsTemplate);
            SmsTemplate_SmsTemplateDTO = new SmsTemplate_SmsTemplateDTO(SmsTemplate);
            if (SmsTemplate.IsValidated)
                return SmsTemplate_SmsTemplateDTO;
            else
                return BadRequest(SmsTemplate_SmsTemplateDTO);
        }

        [Route(SmsTemplateRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter();
            SmsTemplateFilter = SmsTemplateService.ToFilter(SmsTemplateFilter);
            SmsTemplateFilter.Id = new IdFilter { In = Ids };
            SmsTemplateFilter.Selects = SmsTemplateSelect.Id;
            SmsTemplateFilter.Skip = 0;
            SmsTemplateFilter.Take = int.MaxValue;

            List<SmsTemplate> SmsTemplates = await SmsTemplateService.List(SmsTemplateFilter);
            SmsTemplates = await SmsTemplateService.BulkDelete(SmsTemplates);
            if (SmsTemplates.Any(x => !x.IsValidated))
                return BadRequest(SmsTemplates.Where(x => !x.IsValidated));
            return true;
        }

        [Route(SmsTemplateRoute.Import), HttpPost]
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
            List<SmsTemplate> SmsTemplates = new List<SmsTemplate>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(SmsTemplates);
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

                    SmsTemplate SmsTemplate = new SmsTemplate();
                    SmsTemplate.Code = CodeValue;
                    SmsTemplate.Name = NameValue;
                    SmsTemplate.Content = ContentValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    SmsTemplate.StatusId = Status == null ? 0 : Status.Id;
                    SmsTemplate.Status = Status;

                    SmsTemplates.Add(SmsTemplate);
                }
            }
            SmsTemplates = await SmsTemplateService.Import(SmsTemplates);
            if (SmsTemplates.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < SmsTemplates.Count; i++)
                {
                    SmsTemplate SmsTemplate = SmsTemplates[i];
                    if (!SmsTemplate.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (SmsTemplate.Errors.ContainsKey(nameof(SmsTemplate.Id)))
                            Error += SmsTemplate.Errors[nameof(SmsTemplate.Id)];
                        if (SmsTemplate.Errors.ContainsKey(nameof(SmsTemplate.Code)))
                            Error += SmsTemplate.Errors[nameof(SmsTemplate.Code)];
                        if (SmsTemplate.Errors.ContainsKey(nameof(SmsTemplate.Name)))
                            Error += SmsTemplate.Errors[nameof(SmsTemplate.Name)];
                        if (SmsTemplate.Errors.ContainsKey(nameof(SmsTemplate.Content)))
                            Error += SmsTemplate.Errors[nameof(SmsTemplate.Content)];
                        if (SmsTemplate.Errors.ContainsKey(nameof(SmsTemplate.StatusId)))
                            Error += SmsTemplate.Errors[nameof(SmsTemplate.StatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(SmsTemplateRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] SmsTemplate_SmsTemplateFilterDTO SmsTemplate_SmsTemplateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region SmsTemplate
                var SmsTemplateFilter = ConvertFilterDTOToFilterEntity(SmsTemplate_SmsTemplateFilterDTO);
                SmsTemplateFilter.Skip = 0;
                SmsTemplateFilter.Take = int.MaxValue;
                SmsTemplateFilter = SmsTemplateService.ToFilter(SmsTemplateFilter);
                List<SmsTemplate> SmsTemplates = await SmsTemplateService.List(SmsTemplateFilter);

                var SmsTemplateHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                        "Content",
                        "StatusId",
                    }
                };
                List<object[]> SmsTemplateData = new List<object[]>();
                for (int i = 0; i < SmsTemplates.Count; i++)
                {
                    var SmsTemplate = SmsTemplates[i];
                    SmsTemplateData.Add(new Object[]
                    {
                        SmsTemplate.Id,
                        SmsTemplate.Code,
                        SmsTemplate.Name,
                        SmsTemplate.Content,
                        SmsTemplate.StatusId,
                    });
                }
                excel.GenerateWorksheet("SmsTemplate", SmsTemplateHeaders, SmsTemplateData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "SmsTemplate.xlsx");
        }

        [Route(SmsTemplateRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] SmsTemplate_SmsTemplateFilterDTO SmsTemplate_SmsTemplateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region SmsTemplate
                var SmsTemplateHeaders = new List<string[]>()
                {
                    new string[] {
                        "Số thứ tự",
                        "Mã *",
                        "Tiêu đề *",
                        "Nội dung",
                        "Trạng thái",
                    }
                };
                List<object[]> SmsTemplateData = new List<object[]>();
                excel.GenerateWorksheet("Mẫu tin nhắn - File template tải mẫu", SmsTemplateHeaders, SmsTemplateData);
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
                        "Mã",
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
                excel.GenerateWorksheet("Trạng thái tin nhắn", StatusHeaders, StatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "SmsTemplate.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter();
            SmsTemplateFilter = SmsTemplateService.ToFilter(SmsTemplateFilter);
            if (Id == 0)
            {

            }
            else
            {
                SmsTemplateFilter.Id = new IdFilter { Equal = Id };
                int count = await SmsTemplateService.Count(SmsTemplateFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private SmsTemplate ConvertDTOToEntity(SmsTemplate_SmsTemplateDTO SmsTemplate_SmsTemplateDTO)
        {
            SmsTemplate SmsTemplate = new SmsTemplate();
            SmsTemplate.Id = SmsTemplate_SmsTemplateDTO.Id;
            SmsTemplate.Code = SmsTemplate_SmsTemplateDTO.Code;
            SmsTemplate.Name = SmsTemplate_SmsTemplateDTO.Name;
            SmsTemplate.Content = SmsTemplate_SmsTemplateDTO.Content;
            SmsTemplate.StatusId = SmsTemplate_SmsTemplateDTO.StatusId;
            SmsTemplate.Status = SmsTemplate_SmsTemplateDTO.Status == null ? null : new Status
            {
                Id = SmsTemplate_SmsTemplateDTO.Status.Id,
                Code = SmsTemplate_SmsTemplateDTO.Status.Code,
                Name = SmsTemplate_SmsTemplateDTO.Status.Name,
            };
            SmsTemplate.BaseLanguage = CurrentContext.Language;
            return SmsTemplate;
        }

        private SmsTemplateFilter ConvertFilterDTOToFilterEntity(SmsTemplate_SmsTemplateFilterDTO SmsTemplate_SmsTemplateFilterDTO)
        {
            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter();
            SmsTemplateFilter.Selects = SmsTemplateSelect.ALL;
            SmsTemplateFilter.Skip = SmsTemplate_SmsTemplateFilterDTO.Skip;
            SmsTemplateFilter.Take = SmsTemplate_SmsTemplateFilterDTO.Take;
            SmsTemplateFilter.OrderBy = SmsTemplate_SmsTemplateFilterDTO.OrderBy;
            SmsTemplateFilter.OrderType = SmsTemplate_SmsTemplateFilterDTO.OrderType;

            SmsTemplateFilter.Id = SmsTemplate_SmsTemplateFilterDTO.Id;
            SmsTemplateFilter.Code = SmsTemplate_SmsTemplateFilterDTO.Code;
            SmsTemplateFilter.Name = SmsTemplate_SmsTemplateFilterDTO.Name;
            SmsTemplateFilter.Content = SmsTemplate_SmsTemplateFilterDTO.Content;
            SmsTemplateFilter.StatusId = SmsTemplate_SmsTemplateFilterDTO.StatusId;
            SmsTemplateFilter.CreatedAt = SmsTemplate_SmsTemplateFilterDTO.CreatedAt;
            SmsTemplateFilter.UpdatedAt = SmsTemplate_SmsTemplateFilterDTO.UpdatedAt;
            return SmsTemplateFilter;
        }
    }
}

