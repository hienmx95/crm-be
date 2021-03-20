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
using CRM.Services.MPosition;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.position
{
    public partial class PositionController : RpcController
    {
        private IStatusService StatusService;
        private IPositionService PositionService;
        private ICurrentContext CurrentContext;
        public PositionController(
            IStatusService StatusService,
            IPositionService PositionService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.PositionService = PositionService;
            this.CurrentContext = CurrentContext;
        }

        [Route(PositionRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Position_PositionFilterDTO Position_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PositionFilter PositionFilter = ConvertFilterDTOToFilterEntity(Position_PositionFilterDTO);
            PositionFilter = await PositionService.ToFilter(PositionFilter);
            int count = await PositionService.Count(PositionFilter);
            return count;
        }

        [Route(PositionRoute.List), HttpPost]
        public async Task<ActionResult<List<Position_PositionDTO>>> List([FromBody] Position_PositionFilterDTO Position_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PositionFilter PositionFilter = ConvertFilterDTOToFilterEntity(Position_PositionFilterDTO);
            PositionFilter = await PositionService.ToFilter(PositionFilter);
            List<Position> Positions = await PositionService.List(PositionFilter);
            List<Position_PositionDTO> Position_PositionDTOs = Positions
                .Select(c => new Position_PositionDTO(c)).ToList();
            return Position_PositionDTOs;
        }

        [Route(PositionRoute.Get), HttpPost]
        public async Task<ActionResult<Position_PositionDTO>> Get([FromBody]Position_PositionDTO Position_PositionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Position_PositionDTO.Id))
                return Forbid();

            Position Position = await PositionService.Get(Position_PositionDTO.Id);
            return new Position_PositionDTO(Position);
        }

        [Route(PositionRoute.Create), HttpPost]
        public async Task<ActionResult<Position_PositionDTO>> Create([FromBody] Position_PositionDTO Position_PositionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Position_PositionDTO.Id))
                return Forbid();

            Position Position = ConvertDTOToEntity(Position_PositionDTO);
            Position = await PositionService.Create(Position);
            Position_PositionDTO = new Position_PositionDTO(Position);
            if (Position.IsValidated)
                return Position_PositionDTO;
            else
                return BadRequest(Position_PositionDTO);
        }

        [Route(PositionRoute.Update), HttpPost]
        public async Task<ActionResult<Position_PositionDTO>> Update([FromBody] Position_PositionDTO Position_PositionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Position_PositionDTO.Id))
                return Forbid();

            Position Position = ConvertDTOToEntity(Position_PositionDTO);
            Position = await PositionService.Update(Position);
            Position_PositionDTO = new Position_PositionDTO(Position);
            if (Position.IsValidated)
                return Position_PositionDTO;
            else
                return BadRequest(Position_PositionDTO);
        }

        [Route(PositionRoute.Delete), HttpPost]
        public async Task<ActionResult<Position_PositionDTO>> Delete([FromBody] Position_PositionDTO Position_PositionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Position_PositionDTO.Id))
                return Forbid();

            Position Position = ConvertDTOToEntity(Position_PositionDTO);
            Position = await PositionService.Delete(Position);
            Position_PositionDTO = new Position_PositionDTO(Position);
            if (Position.IsValidated)
                return Position_PositionDTO;
            else
                return BadRequest(Position_PositionDTO);
        }
        
        [Route(PositionRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter = await PositionService.ToFilter(PositionFilter);
            PositionFilter.Id = new IdFilter { In = Ids };
            PositionFilter.Selects = PositionSelect.Id;
            PositionFilter.Skip = 0;
            PositionFilter.Take = int.MaxValue;

            List<Position> Positions = await PositionService.List(PositionFilter);
            Positions = await PositionService.BulkDelete(Positions);
            if (Positions.Any(x => !x.IsValidated))
                return BadRequest(Positions.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(PositionRoute.Import), HttpPost]
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
            List<Position> Positions = new List<Position>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Positions);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int RowIdColumn = 4 + StartColumn;
                int UsedColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    Position Position = new Position();
                    Position.Code = CodeValue;
                    Position.Name = NameValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    Position.StatusId = Status == null ? 0 : Status.Id;
                    Position.Status = Status;
                    
                    Positions.Add(Position);
                }
            }
            Positions = await PositionService.Import(Positions);
            if (Positions.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Positions.Count; i++)
                {
                    Position Position = Positions[i];
                    if (!Position.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Position.Errors.ContainsKey(nameof(Position.Id)))
                            Error += Position.Errors[nameof(Position.Id)];
                        if (Position.Errors.ContainsKey(nameof(Position.Code)))
                            Error += Position.Errors[nameof(Position.Code)];
                        if (Position.Errors.ContainsKey(nameof(Position.Name)))
                            Error += Position.Errors[nameof(Position.Name)];
                        if (Position.Errors.ContainsKey(nameof(Position.StatusId)))
                            Error += Position.Errors[nameof(Position.StatusId)];
                        if (Position.Errors.ContainsKey(nameof(Position.RowId)))
                            Error += Position.Errors[nameof(Position.RowId)];
                        if (Position.Errors.ContainsKey(nameof(Position.Used)))
                            Error += Position.Errors[nameof(Position.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(PositionRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Position_PositionFilterDTO Position_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region Position
                var PositionFilter = ConvertFilterDTOToFilterEntity(Position_PositionFilterDTO);
                PositionFilter.Skip = 0;
                PositionFilter.Take = int.MaxValue;
                PositionFilter = await PositionService.ToFilter(PositionFilter);
                List<Position> Positions = await PositionService.List(PositionFilter);

                var PositionHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> PositionData = new List<object[]>();
                for (int i = 0; i < Positions.Count; i++)
                {
                    var Position = Positions[i];
                    PositionData.Add(new Object[]
                    {
                        Position.Id,
                        Position.Code,
                        Position.Name,
                        Position.StatusId,
                        Position.RowId,
                        Position.Used,
                    });
                }
                excel.GenerateWorksheet("Position", PositionHeaders, PositionData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "Position.xlsx");
        }

        [Route(PositionRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] Position_PositionFilterDTO Position_PositionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/Position_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "Position.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter = await PositionService.ToFilter(PositionFilter);
            if (Id == 0)
            {

            }
            else
            {
                PositionFilter.Id = new IdFilter { Equal = Id };
                int count = await PositionService.Count(PositionFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Position ConvertDTOToEntity(Position_PositionDTO Position_PositionDTO)
        {
            Position Position = new Position();
            Position.Id = Position_PositionDTO.Id;
            Position.Code = Position_PositionDTO.Code;
            Position.Name = Position_PositionDTO.Name;
            Position.StatusId = Position_PositionDTO.StatusId;
            Position.RowId = Position_PositionDTO.RowId;
            Position.Used = Position_PositionDTO.Used;
            Position.Status = Position_PositionDTO.Status == null ? null : new Status
            {
                Id = Position_PositionDTO.Status.Id,
                Code = Position_PositionDTO.Status.Code,
                Name = Position_PositionDTO.Status.Name,
            };
            Position.BaseLanguage = CurrentContext.Language;
            return Position;
        }

        private PositionFilter ConvertFilterDTOToFilterEntity(Position_PositionFilterDTO Position_PositionFilterDTO)
        {
            PositionFilter PositionFilter = new PositionFilter();
            PositionFilter.Selects = PositionSelect.ALL;
            PositionFilter.Skip = Position_PositionFilterDTO.Skip;
            PositionFilter.Take = Position_PositionFilterDTO.Take;
            PositionFilter.OrderBy = Position_PositionFilterDTO.OrderBy;
            PositionFilter.OrderType = Position_PositionFilterDTO.OrderType;

            PositionFilter.Id = Position_PositionFilterDTO.Id;
            PositionFilter.Code = Position_PositionFilterDTO.Code;
            PositionFilter.Name = Position_PositionFilterDTO.Name;
            PositionFilter.StatusId = Position_PositionFilterDTO.StatusId;
            PositionFilter.CreatedAt = Position_PositionFilterDTO.CreatedAt;
            PositionFilter.UpdatedAt = Position_PositionFilterDTO.UpdatedAt;
            return PositionFilter;
        }
    }
}

