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
using System.Dynamic;
using CRM.Entities;
using CRM.Services.MCustomerLevel;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.customer_level
{
    public partial class CustomerLevelController : RpcController
    {
        private IStatusService StatusService;
        private ICustomerLevelService CustomerLevelService;
        private ICurrentContext CurrentContext;
        public CustomerLevelController(
            IStatusService StatusService,
            ICustomerLevelService CustomerLevelService,
            ICurrentContext CurrentContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.CustomerLevelService = CustomerLevelService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerLevelRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerLevel_CustomerLevelFilterDTO CustomerLevel_CustomerLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLevelFilter CustomerLevelFilter = ConvertFilterDTOToFilterEntity(CustomerLevel_CustomerLevelFilterDTO);
            CustomerLevelFilter = await CustomerLevelService.ToFilter(CustomerLevelFilter);
            int count = await CustomerLevelService.Count(CustomerLevelFilter);
            return count;
        }

        [Route(CustomerLevelRoute.List), HttpPost]
        public async Task<ActionResult<List<CustomerLevel_CustomerLevelDTO>>> List([FromBody] CustomerLevel_CustomerLevelFilterDTO CustomerLevel_CustomerLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLevelFilter CustomerLevelFilter = ConvertFilterDTOToFilterEntity(CustomerLevel_CustomerLevelFilterDTO);
            CustomerLevelFilter = await CustomerLevelService.ToFilter(CustomerLevelFilter);
            List<CustomerLevel> CustomerLevels = await CustomerLevelService.List(CustomerLevelFilter);
            List<CustomerLevel_CustomerLevelDTO> CustomerLevel_CustomerLevelDTOs = CustomerLevels
                .Select(c => new CustomerLevel_CustomerLevelDTO(c)).ToList();
            return CustomerLevel_CustomerLevelDTOs;
        }

        [Route(CustomerLevelRoute.Get), HttpPost]
        public async Task<ActionResult<CustomerLevel_CustomerLevelDTO>> Get([FromBody]CustomerLevel_CustomerLevelDTO CustomerLevel_CustomerLevelDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLevel_CustomerLevelDTO.Id))
                return Forbid();

            CustomerLevel CustomerLevel = await CustomerLevelService.Get(CustomerLevel_CustomerLevelDTO.Id);
            return new CustomerLevel_CustomerLevelDTO(CustomerLevel);
        }

        [Route(CustomerLevelRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerLevel_CustomerLevelDTO>> Create([FromBody] CustomerLevel_CustomerLevelDTO CustomerLevel_CustomerLevelDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerLevel_CustomerLevelDTO.Id))
                return Forbid();

            CustomerLevel CustomerLevel = ConvertDTOToEntity(CustomerLevel_CustomerLevelDTO);
            CustomerLevel = await CustomerLevelService.Create(CustomerLevel);
            CustomerLevel_CustomerLevelDTO = new CustomerLevel_CustomerLevelDTO(CustomerLevel);
            if (CustomerLevel.IsValidated)
                return CustomerLevel_CustomerLevelDTO;
            else
                return BadRequest(CustomerLevel_CustomerLevelDTO);
        }

        [Route(CustomerLevelRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerLevel_CustomerLevelDTO>> Update([FromBody] CustomerLevel_CustomerLevelDTO CustomerLevel_CustomerLevelDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerLevel_CustomerLevelDTO.Id))
                return Forbid();

            CustomerLevel CustomerLevel = ConvertDTOToEntity(CustomerLevel_CustomerLevelDTO);
            CustomerLevel = await CustomerLevelService.Update(CustomerLevel);
            CustomerLevel_CustomerLevelDTO = new CustomerLevel_CustomerLevelDTO(CustomerLevel);
            if (CustomerLevel.IsValidated)
                return CustomerLevel_CustomerLevelDTO;
            else
                return BadRequest(CustomerLevel_CustomerLevelDTO);
        }

        [Route(CustomerLevelRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerLevel_CustomerLevelDTO>> Delete([FromBody] CustomerLevel_CustomerLevelDTO CustomerLevel_CustomerLevelDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLevel_CustomerLevelDTO.Id))
                return Forbid();

            CustomerLevel CustomerLevel = ConvertDTOToEntity(CustomerLevel_CustomerLevelDTO);
            CustomerLevel = await CustomerLevelService.Delete(CustomerLevel);
            CustomerLevel_CustomerLevelDTO = new CustomerLevel_CustomerLevelDTO(CustomerLevel);
            if (CustomerLevel.IsValidated)
                return CustomerLevel_CustomerLevelDTO;
            else
                return BadRequest(CustomerLevel_CustomerLevelDTO);
        }
        
        [Route(CustomerLevelRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLevelFilter CustomerLevelFilter = new CustomerLevelFilter();
            CustomerLevelFilter = await CustomerLevelService.ToFilter(CustomerLevelFilter);
            CustomerLevelFilter.Id = new IdFilter { In = Ids };
            CustomerLevelFilter.Selects = CustomerLevelSelect.Id;
            CustomerLevelFilter.Skip = 0;
            CustomerLevelFilter.Take = int.MaxValue;

            List<CustomerLevel> CustomerLevels = await CustomerLevelService.List(CustomerLevelFilter);
            CustomerLevels = await CustomerLevelService.BulkDelete(CustomerLevels);
            if (CustomerLevels.Any(x => !x.IsValidated))
                return BadRequest(CustomerLevels.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(CustomerLevelRoute.Import), HttpPost]
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
            List<CustomerLevel> CustomerLevels = new List<CustomerLevel>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CustomerLevels);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int ColorColumn = 3 + StartColumn;
                int PointFromColumn = 4 + StartColumn;
                int PointToColumn = 5 + StartColumn;
                int StatusIdColumn = 6 + StartColumn;
                int DescriptionColumn = 7 + StartColumn;
                int UsedColumn = 11 + StartColumn;
                int RowIdColumn = 12 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string ColorValue = worksheet.Cells[i + StartRow, ColorColumn].Value?.ToString();
                    string PointFromValue = worksheet.Cells[i + StartRow, PointFromColumn].Value?.ToString();
                    string PointToValue = worksheet.Cells[i + StartRow, PointToColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();
                    
                    CustomerLevel CustomerLevel = new CustomerLevel();
                    CustomerLevel.Code = CodeValue;
                    CustomerLevel.Name = NameValue;
                    CustomerLevel.Color = ColorValue;
                    CustomerLevel.PointFrom = long.TryParse(PointFromValue, out long PointFrom) ? PointFrom : 0;
                    CustomerLevel.PointTo = long.TryParse(PointToValue, out long PointTo) ? PointTo : 0;
                    CustomerLevel.Description = DescriptionValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    CustomerLevel.StatusId = Status == null ? 0 : Status.Id;
                    CustomerLevel.Status = Status;
                    
                    CustomerLevels.Add(CustomerLevel);
                }
            }
            CustomerLevels = await CustomerLevelService.Import(CustomerLevels);
            if (CustomerLevels.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CustomerLevels.Count; i++)
                {
                    CustomerLevel CustomerLevel = CustomerLevels[i];
                    if (!CustomerLevel.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.Id)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.Id)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.Code)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.Code)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.Name)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.Name)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.Color)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.Color)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.PointFrom)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.PointFrom)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.PointTo)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.PointTo)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.StatusId)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.StatusId)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.Description)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.Description)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.Used)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.Used)];
                        if (CustomerLevel.Errors.ContainsKey(nameof(CustomerLevel.RowId)))
                            Error += CustomerLevel.Errors[nameof(CustomerLevel.RowId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(CustomerLevelRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerLevel_CustomerLevelFilterDTO CustomerLevel_CustomerLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerLevel
                var CustomerLevelFilter = ConvertFilterDTOToFilterEntity(CustomerLevel_CustomerLevelFilterDTO);
                CustomerLevelFilter.Skip = 0;
                CustomerLevelFilter.Take = int.MaxValue;
                CustomerLevelFilter = await CustomerLevelService.ToFilter(CustomerLevelFilter);
                List<CustomerLevel> CustomerLevels = await CustomerLevelService.List(CustomerLevelFilter);

                var CustomerLevelHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Color",
                        "PointFrom",
                        "PointTo",
                        "StatusId",
                        "Description",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> CustomerLevelData = new List<object[]>();
                for (int i = 0; i < CustomerLevels.Count; i++)
                {
                    var CustomerLevel = CustomerLevels[i];
                    CustomerLevelData.Add(new Object[]
                    {
                        CustomerLevel.Id,
                        CustomerLevel.Code,
                        CustomerLevel.Name,
                        CustomerLevel.Color,
                        CustomerLevel.PointFrom,
                        CustomerLevel.PointTo,
                        CustomerLevel.StatusId,
                        CustomerLevel.Description,
                        CustomerLevel.Used,
                        CustomerLevel.RowId,
                    });
                }
                excel.GenerateWorksheet("CustomerLevel", CustomerLevelHeaders, CustomerLevelData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerLevel.xlsx");
        }

        [Route(CustomerLevelRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CustomerLevel_CustomerLevelFilterDTO CustomerLevel_CustomerLevelFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/CustomerLevel_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "CustomerLevel.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerLevelFilter CustomerLevelFilter = new CustomerLevelFilter();
            CustomerLevelFilter = await CustomerLevelService.ToFilter(CustomerLevelFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerLevelFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerLevelService.Count(CustomerLevelFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CustomerLevel ConvertDTOToEntity(CustomerLevel_CustomerLevelDTO CustomerLevel_CustomerLevelDTO)
        {
            CustomerLevel CustomerLevel = new CustomerLevel();
            CustomerLevel.Id = CustomerLevel_CustomerLevelDTO.Id;
            CustomerLevel.Code = CustomerLevel_CustomerLevelDTO.Code;
            CustomerLevel.Name = CustomerLevel_CustomerLevelDTO.Name;
            CustomerLevel.Color = CustomerLevel_CustomerLevelDTO.Color;
            CustomerLevel.PointFrom = CustomerLevel_CustomerLevelDTO.PointFrom;
            CustomerLevel.PointTo = CustomerLevel_CustomerLevelDTO.PointTo;
            CustomerLevel.StatusId = CustomerLevel_CustomerLevelDTO.StatusId;
            CustomerLevel.Description = CustomerLevel_CustomerLevelDTO.Description;
            CustomerLevel.Used = CustomerLevel_CustomerLevelDTO.Used;
            CustomerLevel.RowId = CustomerLevel_CustomerLevelDTO.RowId;
            CustomerLevel.Status = CustomerLevel_CustomerLevelDTO.Status == null ? null : new Status
            {
                Id = CustomerLevel_CustomerLevelDTO.Status.Id,
                Code = CustomerLevel_CustomerLevelDTO.Status.Code,
                Name = CustomerLevel_CustomerLevelDTO.Status.Name,
            };
            CustomerLevel.BaseLanguage = CurrentContext.Language;
            return CustomerLevel;
        }

        private CustomerLevelFilter ConvertFilterDTOToFilterEntity(CustomerLevel_CustomerLevelFilterDTO CustomerLevel_CustomerLevelFilterDTO)
        {
            CustomerLevelFilter CustomerLevelFilter = new CustomerLevelFilter();
            CustomerLevelFilter.Selects = CustomerLevelSelect.ALL;
            CustomerLevelFilter.Skip = CustomerLevel_CustomerLevelFilterDTO.Skip;
            CustomerLevelFilter.Take = CustomerLevel_CustomerLevelFilterDTO.Take;
            CustomerLevelFilter.OrderBy = CustomerLevel_CustomerLevelFilterDTO.OrderBy;
            CustomerLevelFilter.OrderType = CustomerLevel_CustomerLevelFilterDTO.OrderType;

            CustomerLevelFilter.Id = CustomerLevel_CustomerLevelFilterDTO.Id;
            CustomerLevelFilter.Code = CustomerLevel_CustomerLevelFilterDTO.Code;
            CustomerLevelFilter.Name = CustomerLevel_CustomerLevelFilterDTO.Name;
            CustomerLevelFilter.Color = CustomerLevel_CustomerLevelFilterDTO.Color;
            CustomerLevelFilter.PointFrom = CustomerLevel_CustomerLevelFilterDTO.PointFrom;
            CustomerLevelFilter.PointTo = CustomerLevel_CustomerLevelFilterDTO.PointTo;
            CustomerLevelFilter.StatusId = CustomerLevel_CustomerLevelFilterDTO.StatusId;
            CustomerLevelFilter.Description = CustomerLevel_CustomerLevelFilterDTO.Description;
            CustomerLevelFilter.RowId = CustomerLevel_CustomerLevelFilterDTO.RowId;
            CustomerLevelFilter.CreatedAt = CustomerLevel_CustomerLevelFilterDTO.CreatedAt;
            CustomerLevelFilter.UpdatedAt = CustomerLevel_CustomerLevelFilterDTO.UpdatedAt;
            return CustomerLevelFilter;
        }
    }
}

