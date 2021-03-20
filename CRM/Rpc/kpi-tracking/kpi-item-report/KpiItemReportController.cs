using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Models;
using CRM.Services.MAppUser;
using CRM.Services.MKpiPeriod;
using CRM.Services.MKpiYear;
using CRM.Services.MOrganization;
using CRM.Services.MProduct;
using CRM.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CRM.Rpc.kpi_tracking.kpi_item_report
{
    public class KpiItemReportController : RpcController
    {
        private DataContext DataContext;
        private IOrganizationService OrganizationService;
        private IAppUserService AppUserService;
        private IKpiYearService KpiYearService;
        private IKpiPeriodService KpiPeriodService;
        private IItemService ItemService;
        private ICurrentContext CurrentContext;
        public KpiItemReportController(DataContext DataContext,
            IOrganizationService OrganizationService,
            IAppUserService AppUserService,
            IKpiYearService KpiYearService,
            IKpiPeriodService KpiPeriodService,
            IItemService ItemService,
            ICurrentContext CurrentContext,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.DataContext = DataContext;
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.KpiPeriodService = KpiPeriodService;
            this.KpiYearService = KpiYearService;
            this.ItemService = ItemService;
            this.CurrentContext = CurrentContext;
        }

        [Route(KpiItemReportRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<KpiItemReport_AppUserDTO>>> FilterListAppUser([FromBody] KpiItemReport_AppUserFilterDTO KpiItemReport_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.Id | AppUserSelect.Username | AppUserSelect.DisplayName;
            AppUserFilter.Id = KpiItemReport_AppUserFilterDTO.Id;
            AppUserFilter.OrganizationId = KpiItemReport_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Username = KpiItemReport_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = KpiItemReport_AppUserFilterDTO.DisplayName;
            AppUserFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            //if (AppUserFilter.Id == null) AppUserFilter.Id = new IdFilter();
            //AppUserFilter.Id.In = await FilterAppUser(AppUserService, OrganizationService, CurrentContext);

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<KpiItemReport_AppUserDTO> KpiItemReport_AppUserDTOs = AppUsers
                .Select(x => new KpiItemReport_AppUserDTO(x)).ToList();
            return KpiItemReport_AppUserDTOs;
        }

        [Route(KpiItemReportRoute.FilterListOrganization), HttpPost]
        public async Task<ActionResult<List<KpiItemReport_OrganizationDTO>>> FilterListOrganization([FromBody] KpiItemReport_OrganizationFilterDTO KpiItemReport_OrganizationFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrganizationFilter OrganizationFilter = new OrganizationFilter();
            OrganizationFilter.Skip = 0;
            OrganizationFilter.Take = int.MaxValue;
            OrganizationFilter.OrderBy = OrganizationOrder.Id;
            OrganizationFilter.OrderType = OrderType.ASC;
            OrganizationFilter.Selects = OrganizationSelect.ALL;
            OrganizationFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            if (OrganizationFilter.Id == null) OrganizationFilter.Id = new IdFilter();
            OrganizationFilter.Id.In = await FilterOrganization(OrganizationService, CurrentContext);

            List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);
            List<KpiItemReport_OrganizationDTO> KpiItemReport_OrganizationDTOs = Organizations
                .Select(x => new KpiItemReport_OrganizationDTO(x)).ToList();
            return KpiItemReport_OrganizationDTOs;
        }

        [Route(KpiItemReportRoute.FilterListItem), HttpPost]
        public async Task<ActionResult<List<KpiItemReport_ItemDTO>>> FilterListItem([FromBody] KpiItemReport_ItemFilterDTO KpiItemReport_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = int.MaxValue;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<KpiItemReport_ItemDTO> KpiItemReport_ItemDTOs = Items
                .Select(x => new KpiItemReport_ItemDTO(x)).ToList();
            return KpiItemReport_ItemDTOs;
        }

        [Route(KpiItemReportRoute.FilterListKpiPeriod), HttpPost]
        public async Task<ActionResult<List<KpiItemReport_KpiPeriodDTO>>> FilterListKpiPeriod([FromBody] KpiItemReport_KpiPeriodFilterDTO KpiItemReport_KpiPeriodFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KpiPeriodFilter KpiPeriodFilter = new KpiPeriodFilter();
            KpiPeriodFilter.Skip = 0;
            KpiPeriodFilter.Take = 20;
            KpiPeriodFilter.OrderBy = KpiPeriodOrder.Id;
            KpiPeriodFilter.OrderType = OrderType.ASC;
            KpiPeriodFilter.Selects = KpiPeriodSelect.ALL;
            KpiPeriodFilter.Id = KpiItemReport_KpiPeriodFilterDTO.Id;
            KpiPeriodFilter.Code = KpiItemReport_KpiPeriodFilterDTO.Code;
            KpiPeriodFilter.Name = KpiItemReport_KpiPeriodFilterDTO.Name;

            List<KpiPeriod> KpiPeriods = await KpiPeriodService.List(KpiPeriodFilter);
            List<KpiItemReport_KpiPeriodDTO> KpiItemReport_KpiPeriodDTOs = KpiPeriods
                .Select(x => new KpiItemReport_KpiPeriodDTO(x)).ToList();
            return KpiItemReport_KpiPeriodDTOs;
        }

        [Route(KpiItemReportRoute.FilterListKpiYear), HttpPost]
        public async Task<ActionResult<List<KpiItemReport_KpiYearDTO>>> FilterListKpiYear([FromBody] KpiItemReport_KpiYearFilterDTO KpiItemReport_KpiYearFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            KpiYearFilter KpiYearFilter = new KpiYearFilter();
            KpiYearFilter.Skip = 0;
            KpiYearFilter.Take = 20;
            KpiYearFilter.OrderBy = KpiYearOrder.Id;
            KpiYearFilter.OrderType = OrderType.ASC;
            KpiYearFilter.Selects = KpiYearSelect.ALL;
            KpiYearFilter.Id = KpiItemReport_KpiYearFilterDTO.Id;
            KpiYearFilter.Code = KpiItemReport_KpiYearFilterDTO.Code;
            KpiYearFilter.Name = KpiItemReport_KpiYearFilterDTO.Name;

            List<KpiYear> KpiYears = await KpiYearService.List(KpiYearFilter);
            List<KpiItemReport_KpiYearDTO> KpiItemReport_KpiYearDTOs = KpiYears
                .Select(x => new KpiItemReport_KpiYearDTO(x)).ToList();
            return KpiItemReport_KpiYearDTOs;
        }

        [Route(KpiItemReportRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] KpiItemReport_KpiItemReportFilterDTO KpiItemReport_KpiItemReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState); // to do kpi year and period
            DateTime StartDate, EndDate;
            long? SaleEmployeeId = KpiItemReport_KpiItemReportFilterDTO.AppUserId?.Equal;
            long? ItemId = KpiItemReport_KpiItemReportFilterDTO.ItemId?.Equal;
            if (KpiItemReport_KpiItemReportFilterDTO.KpiPeriodId?.Equal.HasValue == false ||
                KpiItemReport_KpiItemReportFilterDTO.KpiYearId?.Equal.HasValue == false)
                return 0;
            long? KpiPeriodId = KpiItemReport_KpiItemReportFilterDTO.KpiPeriodId?.Equal.Value;
            long? KpiYearId = KpiItemReport_KpiItemReportFilterDTO.KpiYearId?.Equal.Value;
            (StartDate, EndDate) = DateTimeConvert(KpiPeriodId.Value, KpiYearId.Value);

            List<long> AppUserIds, OrganizationIds;
            (AppUserIds, OrganizationIds) = await FilterOrganizationAndUser(KpiItemReport_KpiItemReportFilterDTO.OrganizationId,
                AppUserService, OrganizationService, CurrentContext, DataContext);

            var query = from ki in DataContext.KpiItem
                        join kic in DataContext.KpiItemContent on ki.Id equals kic.KpiItemId
                        join i in DataContext.Item on kic.ItemId equals i.Id
                        where OrganizationIds.Contains(ki.OrganizationId) &&
                        AppUserIds.Contains(ki.EmployeeId) &&
                        (SaleEmployeeId == null || ki.Id == SaleEmployeeId.Value) &&
                        (ItemId == null || i.Id == ItemId.Value) &&
                        (ki.KpiYearId == KpiYearId) &&
                        ki.DeletedAt == null &&
                        ki.StatusId == StatusEnum.ACTIVE.Id
                        select new
                        {
                            SaleEmployeeId = ki.EmployeeId,
                            ItemId = i.Id,
                        };
            return await query.Distinct().CountAsync();
        }

        [Route(KpiItemReportRoute.List), HttpPost]
        public async Task<ActionResult<List<KpiItemReport_KpiItemReportDTO>>> List([FromBody] KpiItemReport_KpiItemReportFilterDTO KpiItemReport_KpiItemReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (KpiItemReport_KpiItemReportFilterDTO.KpiPeriodId?.Equal.HasValue == false)
                return BadRequest(new { message = "Chưa chọn kì KPI" });
            if (KpiItemReport_KpiItemReportFilterDTO.KpiYearId?.Equal.HasValue == false)
                return BadRequest(new { message = "Chưa chọn năm KPI" });

            DateTime StartDate, EndDate;
            long? SaleEmployeeId = KpiItemReport_KpiItemReportFilterDTO.AppUserId?.Equal;
            long? ItemId = KpiItemReport_KpiItemReportFilterDTO.ItemId?.Equal;
            long? KpiPeriodId = KpiItemReport_KpiItemReportFilterDTO.KpiPeriodId?.Equal.Value;
            long? KpiYearId = KpiItemReport_KpiItemReportFilterDTO.KpiYearId?.Equal.Value;
            (StartDate, EndDate) = DateTimeConvert(KpiPeriodId.Value, KpiYearId.Value);

            List<long> AppUserIds, OrganizationIds;
            (AppUserIds, OrganizationIds) = await FilterOrganizationAndUser(KpiItemReport_KpiItemReportFilterDTO.OrganizationId,
                AppUserService, OrganizationService, CurrentContext, DataContext);

            var query = from ki in DataContext.KpiItem
                        join au in DataContext.AppUser on ki.EmployeeId equals au.Id
                        join kic in DataContext.KpiItemContent on ki.Id equals kic.KpiItemId
                        join i in DataContext.Item on kic.ItemId equals i.Id
                        where OrganizationIds.Contains(ki.OrganizationId) &&
                        AppUserIds.Contains(au.Id) &&
                        (SaleEmployeeId.HasValue == false || ki.EmployeeId == SaleEmployeeId.Value) &&
                        (ItemId.HasValue == false || i.Id == ItemId.Value) &&
                        (ki.KpiPeriodId == KpiPeriodId.Value) &&
                        (ki.KpiYearId == KpiYearId.Value) &&
                        ki.DeletedAt == null &&
                        ki.StatusId == StatusEnum.ACTIVE.Id
                        select new
                        {
                            SaleEmployeeId = au.Id,
                            Username = au.Username,
                            DisplayName = au.DisplayName,
                            OrganizationId = au.OrganizationId,
                            ItemId = i.Id,
                            ItemCode = i.Code,
                            ItemName = i.Name,
                        };

            var ItemContents = await query.Distinct()
                .OrderBy(q => q.OrganizationId).ThenBy(x => x.DisplayName)
                .Skip(KpiItemReport_KpiItemReportFilterDTO.Skip)
                .Take(KpiItemReport_KpiItemReportFilterDTO.Take)
                .ToListAsync();

            List<long> SaleEmployeeIds = ItemContents.Select(x => x.SaleEmployeeId).Distinct().ToList();


            List<KpiItemReport_KpiItemReportDTO> KpiItemReport_KpiItemReportDTOs = new List<KpiItemReport_KpiItemReportDTO>();
            foreach (var EmployeeId in SaleEmployeeIds)
            {
                KpiItemReport_KpiItemReportDTO KpiItemReport_KpiItemReportDTO = new KpiItemReport_KpiItemReportDTO()
                {
                    SaleEmployeeId = EmployeeId,
                    DisplayName = ItemContents.Where(x => x.SaleEmployeeId == EmployeeId).Select(x => x.DisplayName).FirstOrDefault(),
                    Username = ItemContents.Where(x => x.SaleEmployeeId == EmployeeId).Select(x => x.Username).FirstOrDefault(),
                    ItemContents = ItemContents.Where(x => x.SaleEmployeeId == EmployeeId).Select(x => new KpiItemReport_KpiItemContentDTO
                    {
                        ItemId = x.ItemId,
                        SaleEmployeeId = EmployeeId,
                        ItemName = x.ItemName,
                        ItemCode = x.ItemCode,
                    })
                    .Where(x => x.SaleEmployeeId == EmployeeId)
                    .ToList()
                };
                KpiItemReport_KpiItemReportDTOs.Add(KpiItemReport_KpiItemReportDTO);
            }

            // lay du lieu bang mapping
            var query_detail = from km in DataContext.KpiItemContentKpiCriteriaItemMapping
                               join kc in DataContext.KpiItemContent on km.KpiItemContentId equals kc.Id
                               join k in DataContext.KpiItem on kc.KpiItemId equals k.Id
                               join i in DataContext.Item on kc.ItemId equals i.Id
                               where (SaleEmployeeIds.Contains(k.EmployeeId) &&
                                      k.KpiYearId == KpiYearId &&
                                      k.KpiPeriodId == KpiPeriodId &&
                                      (ItemId == null || i.Id == ItemId)) &&
                                      km.Value.HasValue &&
                                      k.DeletedAt == null &&
                                      k.StatusId == StatusEnum.ACTIVE.Id
                               select new
                               {
                                   SaleEmployeeId = k.EmployeeId,
                                   KpiCriteriaItemId = km.KpiCriteriaItemId,
                                   Value = km.Value.Value,
                                   ItemId = i.Id,
                               };

            List<KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTO>
                KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs = (await query_detail.Distinct()
                .ToListAsync())
                .Select(x => new KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTO
                {
                    SaleEmployeeId = x.SaleEmployeeId,
                    KpiCriteriaItemId = x.KpiCriteriaItemId,
                    Value = x.Value,
                    ItemId = x.ItemId,
                })
                .ToList();


            //#region Danh sách đơn hàng
            //#region Đơn hàng lẻ 

            //var CustomerSalesOrderDAOs = await DataContext.CustomerSalesOrder
            //    .Where(x => SaleEmployeeIds.Contains(x.AppUserAssignedId) &&
            //    x.OrderDate >= StartDate && x.OrderDate <= EndDate
            //    )
            //    .Select(x => new OrderRetailDAO
            //    {
            //        Id = x.Id,
            //        Code = x.Code,
            //        OrderRetailTypeId = x.OrderRetailTypeId,
            //        CustomerRetailId = x.CustomerRetailId,
            //        OrderRetailSoureId = x.OrderRetailSoureId,
            //        CustomerPhone = x.CustomerPhone,
            //        CustomerAddress = x.CustomerAddress,
            //        Company = x.Company,
            //        TaxCode = x.TaxCode,
            //        CompanyAddress = x.CompanyAddress,
            //        DeliveryAddress = x.DeliveryAddress,
            //        ZipCode = x.ZipCode,
            //        RequestStateId = x.RequestStateId,
            //        OrderPaymentStatusId = x.OrderPaymentStatusId,
            //        AppUserAssignedId = x.AppUserAssignedId,
            //        Note = x.Note,
            //        OrderDate = x.OrderDate,
            //        DeliveryDate = x.DeliveryDate,
            //        EditedPriceStatusId = x.EditedPriceStatusId,
            //        SubTotal = x.SubTotal,
            //        GeneralDiscountPercentage = x.GeneralDiscountPercentage,
            //        GeneralDiscountAmount = x.GeneralDiscountAmount,
            //        TotalTaxAmount = x.TotalTaxAmount,
            //        TotalTaxAmountOther = x.TotalTaxAmountOther,
            //        Total = x.Total,
            //        OrganizationId = x.OrganizationId,
            //        OrderRetailContents = x.OrderRetailContents == null ? null : x.OrderRetailContents.Select(p => new OrderRetailContentDAO
            //        {
            //            Id = p.Id,
            //            OrderRetailId = p.OrderRetailId,
            //            ItemId = p.ItemId,
            //            UnitOfMeasureId = p.UnitOfMeasureId,
            //            Quantity = p.Quantity,
            //            PrimaryUnitOfMeasureId = p.PrimaryUnitOfMeasureId,
            //            RequestedQuantity = p.RequestedQuantity,
            //            PrimaryPrice = p.PrimaryPrice,
            //            SalePrice = p.SalePrice,
            //            EditedPriceStatusId = p.EditedPriceStatusId,
            //            DiscountPercentage = p.DiscountPercentage,
            //            DiscountAmount = p.DiscountAmount,
            //            GeneralDiscountPercentage = p.GeneralDiscountPercentage,
            //            GeneralDiscountAmount = p.GeneralDiscountAmount,
            //            TaxPercentage = p.TaxPercentage,
            //            TaxAmount = p.TaxAmount,
            //            TaxPercentageOther = p.TaxPercentageOther,
            //            TaxAmountOther = p.TaxAmountOther,
            //            Amount = p.Amount,
            //            Factor = p.Factor,
            //            Item = p.Item == null ? null : new ItemDAO
            //            {
            //                Id = p.Item.Id,
            //                Code = p.Item.Code,
            //                Name = p.Item.Name,
            //                ProductId = p.Item.ProductId,
            //                RetailPrice = p.Item.RetailPrice,
            //                SalePrice = p.Item.SalePrice,
            //                ScanCode = p.Item.ScanCode,
            //                StatusId = p.Item.StatusId,
            //                Product = p.Item.Product == null ? null : new ProductDAO
            //                {
            //                    Id = p.Item.Product.Id,
            //                    Code = p.Item.Product.Code,
            //                    Name = p.Item.Product.Name,
            //                    TaxTypeId = p.Item.Product.TaxTypeId,
            //                    UnitOfMeasureId = p.Item.Product.UnitOfMeasureId,
            //                    UnitOfMeasureGroupingId = p.Item.Product.UnitOfMeasureGroupingId,
            //                }
            //            },
            //            OrderRetail = p.OrderRetail == null ? null : new OrderRetailDAO
            //            {
            //                Id = p.OrderRetail.Id,
            //                Code = p.OrderRetail.Code,
            //                OrderRetailTypeId = p.OrderRetail.OrderRetailTypeId,
            //                CustomerRetailId = p.OrderRetail.CustomerRetailId,
            //                OrderRetailSoureId = p.OrderRetail.OrderRetailSoureId,
            //                CustomerPhone = p.OrderRetail.CustomerPhone,
            //                CustomerAddress = p.OrderRetail.CustomerAddress,
            //                Company = p.OrderRetail.Company,
            //                TaxCode = p.OrderRetail.TaxCode,
            //                CompanyAddress = p.OrderRetail.CompanyAddress,
            //                DeliveryAddress = p.OrderRetail.DeliveryAddress,
            //                ZipCode = p.OrderRetail.ZipCode,
            //                RequestStateId = p.OrderRetail.RequestStateId,
            //                OrderPaymentStatusId = p.OrderRetail.OrderPaymentStatusId,
            //                AppUserAssignedId = p.OrderRetail.AppUserAssignedId,
            //                Note = p.OrderRetail.Note,
            //                OrderDate = p.OrderRetail.OrderDate,
            //                DeliveryDate = p.OrderRetail.DeliveryDate,
            //                EditedPriceStatusId = p.OrderRetail.EditedPriceStatusId,
            //                SubTotal = p.OrderRetail.SubTotal,
            //                GeneralDiscountPercentage = p.OrderRetail.GeneralDiscountPercentage,
            //                GeneralDiscountAmount = p.OrderRetail.GeneralDiscountAmount,
            //                TotalTaxAmount = p.OrderRetail.TotalTaxAmount,
            //                TotalTaxAmountOther = p.OrderRetail.TotalTaxAmountOther,
            //                Total = p.OrderRetail.Total,
            //                OrganizationId = p.OrderRetail.OrganizationId,
            //            },
            //        }).ToList()
            //    }).ToListAsync();
            //#endregion
            //#region Đơn hàng đại lý
            //var OrderAgentDAOs = await DataContext.DirectSalesOrder
            //    .Where(p => SaleEmployeeIds.Contains(p.SaleEmployeeId) &&
            //    p.OrderDate >= StartDate && p.OrderDate <= EndDate
            //    )
            //    .Select(x => new DirectSalesOrderDAO
            //    {
            //        Id = x.Id,
            //        Code = x.Code,
            //        OrganizationId = x.OrganizationId,
            //        BuyerStoreId = x.BuyerStoreId,
            //        PhoneNumber = x.PhoneNumber,
            //        StoreAddress = x.StoreAddress,
            //        DeliveryAddress = x.DeliveryAddress,
            //        SaleEmployeeId = x.SaleEmployeeId,
            //        OrderDate = x.OrderDate,
            //        CreatedAt = x.CreatedAt,
            //        UpdatedAt = x.UpdatedAt,
            //        DeliveryDate = x.DeliveryDate,
            //        EditedPriceStatusId = x.EditedPriceStatusId,
            //        Note = x.Note,
            //        RequestStateId = x.RequestStateId,
            //        SubTotal = x.SubTotal,
            //        GeneralDiscountPercentage = x.GeneralDiscountPercentage,
            //        GeneralDiscountAmount = x.GeneralDiscountAmount,
            //        PromotionCode = x.PromotionCode,
            //        PromotionValue = x.PromotionValue,
            //        TotalTaxAmount = x.TotalTaxAmount,
            //        TotalAfterTax = x.TotalAfterTax,
            //        Total = x.Total,
            //        RowId = x.RowId,
            //        DirectSalesOrderContents = x.DirectSalesOrderContents == null ? null : x.DirectSalesOrderContents.Select(p => new DirectSalesOrderContentDAO
            //        {
            //            Id = p.Id,
            //            DirectSalesOrderId = p.DirectSalesOrderId,
            //            ItemId = p.ItemId,
            //            UnitOfMeasureId = p.UnitOfMeasureId,
            //            Quantity = p.Quantity,
            //            PrimaryUnitOfMeasureId = p.PrimaryUnitOfMeasureId,
            //            RequestedQuantity = p.RequestedQuantity,
            //            PrimaryPrice = p.PrimaryPrice,
            //            SalePrice = p.SalePrice,
            //            EditedPriceStatusId = p.EditedPriceStatusId,
            //            DiscountPercentage = p.DiscountPercentage,
            //            DiscountAmount = p.DiscountAmount,
            //            GeneralDiscountPercentage = p.GeneralDiscountPercentage,
            //            GeneralDiscountAmount = p.GeneralDiscountAmount,
            //            Amount = p.Amount,
            //            TaxPercentage = p.TaxPercentage,
            //            TaxAmount = p.TaxAmount,
            //            Factor = p.Factor,
            //            Item = new ItemDAO
            //            {
            //                Id = p.Item.Id,
            //                Code = p.Item.Code,
            //                Name = p.Item.Name,
            //                ProductId = p.Item.ProductId,
            //                RetailPrice = p.Item.RetailPrice,
            //                SalePrice = p.Item.SalePrice,
            //                ScanCode = p.Item.ScanCode,
            //                StatusId = p.Item.StatusId,
            //                Product = new ProductDAO
            //                {
            //                    Id = p.Item.Product.Id,
            //                    Code = p.Item.Product.Code,
            //                    Name = p.Item.Product.Name,
            //                    TaxTypeId = p.Item.Product.TaxTypeId,
            //                    UnitOfMeasureId = p.Item.Product.UnitOfMeasureId,
            //                    UnitOfMeasureGroupingId = p.Item.Product.UnitOfMeasureGroupingId,

            //                }
            //            },

            //        }).ToList(),
            //    }).ToListAsync();
            //#endregion
            //#region Đơn hàng dự án
            //var OrderProjectDAOs = await DataContext.OrderProject
            //    .Where(p => SaleEmployeeIds.Contains(p.AppUserAssignedId) &&
            //    p.OrderDate >= StartDate && p.OrderDate <= EndDate
            //    ).Select(x => new OrderProject
            //    {
            //        CreatedAt = x.CreatedAt,
            //        UpdatedAt = x.UpdatedAt,
            //        Id = x.Id,
            //        Code = x.Code,
            //        OrderProjectTypeId = x.OrderProjectTypeId,
            //        Transportation = x.Transportation,
            //        CompanyId = x.CompanyId,
            //        DeliveryDate = x.DeliveryDate,
            //        OpportunityId = x.OpportunityId,
            //        OrderDate = x.OrderDate,
            //        CustomerProjectId = x.CustomerProjectId,
            //        AppUserAssignedId = x.AppUserAssignedId,
            //        ContractId = x.ContractId,
            //        Note = x.Note,
            //        OrderQuoteId = x.OrderQuoteId,
            //        OrderPaymentStatusId = x.OrderPaymentStatusId,
            //        RequestStateId = x.RequestStateId,
            //        EditedPriceStatusId = x.EditedPriceStatusId,
            //        InvoiceAddress = x.InvoiceAddress,
            //        InvoiceProvinceId = x.InvoiceProvinceId,
            //        InvoiceDistrictId = x.InvoiceDistrictId,
            //        InvoiceNationId = x.InvoiceNationId,
            //        InvoiceZIPCode = x.InvoiceZIPCode,
            //        DeliveryAddress = x.DeliveryAddress,
            //        DeliveryNationId = x.DeliveryNationId,
            //        DeliveryProvinceId = x.DeliveryProvinceId,
            //        DeliveryDistrictId = x.DeliveryDistrictId,
            //        DeliveryZIPCode = x.DeliveryZIPCode,
            //        SubTotal = x.SubTotal,
            //        GeneralDiscountPercentage = x.GeneralDiscountPercentage,
            //        GeneralDiscountAmount = x.GeneralDiscountAmount,
            //        TotalTaxAmount = x.TotalTaxAmount,
            //        TotalTaxAmountOther = x.TotalTaxAmountOther,
            //        Total = x.Total,
            //        OrganizationId = x.OrganizationId,
            //        OrderProjectContents = x.OrderProjectContents == null ? null : x.OrderProjectContents.Select(p => new OrderProjectContent
            //        {
            //            Id = p.Id,
            //            OrderProjectId = p.OrderProjectId,
            //            ItemId = p.ItemId,
            //            UnitOfMeasureId = p.UnitOfMeasureId,
            //            Quantity = p.Quantity,
            //            PrimaryUnitOfMeasureId = p.PrimaryUnitOfMeasureId,
            //            RequestedQuantity = p.RequestedQuantity,
            //            PrimaryPrice = p.PrimaryPrice,
            //            SalePrice = p.SalePrice,
            //            EditedPriceStatusId = p.EditedPriceStatusId,
            //            DiscountPercentage = p.DiscountPercentage,
            //            DiscountAmount = p.DiscountAmount,
            //            GeneralDiscountPercentage = p.GeneralDiscountPercentage,
            //            GeneralDiscountAmount = p.GeneralDiscountAmount,
            //            TaxPercentage = p.TaxPercentage,
            //            TaxAmount = p.TaxAmount,
            //            TaxPercentageOther = p.TaxPercentageOther,
            //            TaxAmountOther = p.TaxAmountOther,
            //            Amount = p.Amount,
            //            Factor = p.Factor,
            //            Item = p.Item == null ? null : new Item
            //            {
            //                Id = p.Item.Id,
            //                Code = p.Item.Code,
            //                Name = p.Item.Name,
            //                ProductId = p.Item.ProductId,
            //                RetailPrice = p.Item.RetailPrice,
            //                SalePrice = p.Item.SalePrice,
            //                ScanCode = p.Item.ScanCode,
            //                StatusId = p.Item.StatusId,
            //                Product = p.Item.Product == null ? null : new Product
            //                {
            //                    Id = p.Item.Product.Id,
            //                    Code = p.Item.Product.Code,
            //                    Name = p.Item.Product.Name,
            //                    TaxTypeId = p.Item.Product.TaxTypeId,
            //                    UnitOfMeasureId = p.Item.Product.UnitOfMeasureId,
            //                    UnitOfMeasureGroupingId = p.Item.Product.UnitOfMeasureGroupingId,

            //                }
            //            },
            //            OrderProject = p.OrderProject == null ? null : new OrderProject
            //            {
            //                Id = p.OrderProject.Id,
            //                Code = p.OrderProject.Code,
            //                OrderProjectTypeId = p.OrderProject.OrderProjectTypeId,
            //                Transportation = p.OrderProject.Transportation,
            //                CompanyId = p.OrderProject.CompanyId,
            //                DeliveryDate = p.OrderProject.DeliveryDate,
            //                OpportunityId = p.OrderProject.OpportunityId,
            //                OrderDate = p.OrderProject.OrderDate,
            //                CustomerProjectId = p.OrderProject.CustomerProjectId,
            //                AppUserAssignedId = p.OrderProject.AppUserAssignedId,
            //                ContractId = p.OrderProject.ContractId,
            //                Note = p.OrderProject.Note,
            //                OrderQuoteId = p.OrderProject.OrderQuoteId,
            //                OrderPaymentStatusId = p.OrderProject.OrderPaymentStatusId,
            //                RequestStateId = p.OrderProject.RequestStateId,
            //                EditedPriceStatusId = p.OrderProject.EditedPriceStatusId,
            //                InvoiceAddress = p.OrderProject.InvoiceAddress,
            //                InvoiceProvinceId = p.OrderProject.InvoiceProvinceId,
            //                InvoiceDistrictId = p.OrderProject.InvoiceDistrictId,
            //                InvoiceNationId = p.OrderProject.InvoiceNationId,
            //                InvoiceZIPCode = p.OrderProject.InvoiceZIPCode,
            //                DeliveryAddress = p.OrderProject.DeliveryAddress,
            //                DeliveryNationId = p.OrderProject.DeliveryNationId,
            //                DeliveryProvinceId = p.OrderProject.DeliveryProvinceId,
            //                DeliveryDistrictId = p.OrderProject.DeliveryDistrictId,
            //                DeliveryZIPCode = p.OrderProject.DeliveryZIPCode,
            //                SubTotal = p.OrderProject.SubTotal,
            //                GeneralDiscountPercentage = p.OrderProject.GeneralDiscountPercentage,
            //                GeneralDiscountAmount = p.OrderProject.GeneralDiscountAmount,
            //                TotalTaxAmount = p.OrderProject.TotalTaxAmount,
            //                TotalTaxAmountOther = p.OrderProject.TotalTaxAmountOther,
            //                Total = p.OrderProject.Total,
            //                OrganizationId = p.OrderProject.OrganizationId,
            //            },
            //        }).ToList()
            //    })
            //    .ToListAsync();
            //#endregion

            #region Danh sách hợp đồng
            var ContractDAOs = await DataContext.Contract
                .Where(x => !x.DeletedAt.HasValue && SaleEmployeeIds.Contains(x.AppUserId) &&
                x.ValidityDate >= StartDate && x.ValidityDate <= EndDate).Select(x => new ContractDAO
                {
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    TotalValue = x.TotalValue,
                    ValidityDate = x.ValidityDate,
                    ExpirationDate = x.ExpirationDate,
                    DeliveryUnit = x.DeliveryUnit,
                    InvoiceAddress = x.InvoiceAddress,
                    InvoiceZipCode = x.InvoiceZipCode,
                    ReceiveAddress = x.ReceiveAddress,
                    ReceiveZipCode = x.ReceiveZipCode,
                    TermAndCondition = x.TermAndCondition,
                    InvoiceNationId = x.InvoiceNationId,
                    InvoiceProvinceId = x.InvoiceProvinceId,
                    InvoiceDistrictId = x.InvoiceDistrictId,
                    ReceiveNationId = x.ReceiveNationId,
                    ReceiveProvinceId = x.ReceiveProvinceId,
                    ReceiveDistrictId = x.ReceiveDistrictId,
                    ContractTypeId = x.ContractTypeId,
                    CompanyId = x.CompanyId,
                    OpportunityId = x.OpportunityId,
                    OrganizationId = x.OrganizationId,
                    AppUserId = x.AppUserId,
                    ContractStatusId = x.ContractStatusId,
                    CreatorId = x.CreatorId,
                    CustomerId = x.CustomerId,
                    CurrencyId = x.CurrencyId,
                    PaymentStatusId = x.PaymentStatusId,
                    SubTotal = x.SubTotal,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TotalTaxAmountOther = x.TotalTaxAmountOther,
                    TotalTaxAmount = x.TotalTaxAmount,
                    Total = x.Total,
                    ContractItemDetails = x.ContractItemDetails == null ? null : x.ContractItemDetails.Select(p => new ContractItemDetailDAO
                    {
                        ContractId = p.ContractId,
                        ItemId = p.ItemId,
                        UnitOfMeasureId = p.UnitOfMeasureId,
                        Quantity = p.Quantity,
                        RequestQuantity = p.RequestQuantity,
                        PrimaryPrice = p.PrimaryPrice,
                        SalePrice = p.SalePrice,
                        DiscountPercentage = p.DiscountPercentage,
                        DiscountAmount = p.DiscountAmount,
                        GeneralDiscountPercentage = p.GeneralDiscountPercentage,
                        GeneralDiscountAmount = p.GeneralDiscountAmount,
                        TaxPercentage = p.TaxPercentage,
                        TaxAmount = p.TaxAmount,
                        TaxPercentageOther = p.TaxPercentageOther,
                        TaxAmountOther = p.TaxAmountOther,
                        TotalPrice = p.TotalPrice,
                        Factor = p.Factor,
                        Item = new ItemDAO
                        {
                            Id = p.Item.Id,
                            Name = p.Item.Name,
                            Code = p.Item.Code,
                            ScanCode = p.Item.ScanCode,
                            SalePrice = p.Item.SalePrice,
                            RetailPrice = p.Item.RetailPrice,
                            StatusId = p.Item.StatusId,
                            ProductId = p.Item.ProductId,
                            Product = new ProductDAO
                            {
                                CreatedAt = p.Item.Product.CreatedAt,
                                UpdatedAt = p.Item.Product.UpdatedAt,
                                Id = p.Item.Product.Id,
                                Code = p.Item.Product.Code,
                                Name = p.Item.Product.Name,
                                Description = p.Item.Product.Description,
                                ScanCode = p.Item.Product.ScanCode,
                                ERPCode = p.Item.Product.ERPCode,
                                ProductTypeId = p.Item.Product.ProductTypeId,
                                BrandId = p.Item.Product.BrandId,
                                UnitOfMeasureId = p.Item.Product.UnitOfMeasureId,
                                UnitOfMeasureGroupingId = p.Item.Product.UnitOfMeasureGroupingId,
                                SalePrice = p.Item.Product.SalePrice,
                                RetailPrice = p.Item.Product.RetailPrice,
                                TaxTypeId = p.Item.Product.TaxTypeId,
                                StatusId = p.Item.Product.StatusId,
                                OtherName = p.Item.Product.OtherName,
                                TechnicalName = p.Item.Product.TechnicalName,
                                Note = p.Item.Product.Note,
                                IsNew = p.Item.Product.IsNew,
                                UsedVariationId = p.Item.Product.UsedVariationId,
                                Used = p.Item.Product.Used,
                            }
                        },
                    }).ToList()
                })
                .ToListAsync();

            #endregion

            foreach (var Employee in KpiItemReport_KpiItemReportDTOs)
            {
                foreach (var ItemContent in Employee.ItemContents)
                {

                    #region Sản lượng theo đơn hàng
                    //kế hoạch
                    ItemContent.OrderOutputPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.ORDER_OUTPUT.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    if (ItemContent.OrderOutputPlanned == null)
                    {
                        ItemContent.OrderOutput = null;
                    }
                    else
                    {
                        //ItemContent.OrderOutput = 0;
                        //foreach (var order in CustomerSalesOrderDAOs)
                        //{
                        //    foreach (var content in order.OrderRetailContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.OrderOutput += content.RequestedQuantity;
                        //        }
                        //    }
                        //}
                        //foreach (var order in OrderAgentDAOs)
                        //{
                        //    foreach (var content in order.DirectSalesOrderContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.OrderOutput += content.RequestedQuantity;
                        //        }
                        //    }
                        //}
                        //foreach (var order in OrderProjectDAOs)
                        //{
                        //    foreach (var content in order.OrderProjectContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.OrderOutput += content.RequestedQuantity;
                        //        }
                        //    }
                        //}
                        //foreach (var order in OrderExportDAOs)
                        //{
                        //    foreach (var content in order.OrderExportContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.OrderOutput += content.RequestedQuantity;
                        //        }
                        //    }
                        //}

                        //tỉ lệ
                        ItemContent.OrderOutputRatio = ItemContent.OrderOutputPlanned == 0 || ItemContent.OrderOutputPlanned == null ?
                            null : (decimal?)
                            Math.Round((ItemContent.OrderOutput.Value / ItemContent.OrderOutputPlanned.Value) * 100, 2);
                    }
                    #endregion

                    #region Doanh số
                    //kế hoạch
                    ItemContent.SalesPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.SALES.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    if (ItemContent.SalesPlanned == null)
                    {
                        ItemContent.Sales = null;
                    }
                    else
                    {
                        ItemContent.Sales = 0;
                        //foreach (var order in CustomerSalesOrderDAOs)
                        //{
                        //    foreach (var content in order.OrderRetailContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.Sales += content.Amount;
                        //        }
                        //    }
                        //}
                        //foreach (var order in OrderAgentDAOs)
                        //{
                        //    foreach (var content in order.DirectSalesOrderContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.Sales += content.Amount;
                        //        }
                        //    }
                        //}
                        //foreach (var order in OrderProjectDAOs)
                        //{
                        //    foreach (var content in order.OrderProjectContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.Sales += content.Amount;
                        //        }
                        //    }
                        //}
                        //foreach (var order in OrderExportDAOs)
                        //{
                        //    foreach (var content in order.OrderExportContents)
                        //    {
                        //        if (content.ItemId == ItemContent.ItemId)
                        //        {
                        //            ItemContent.Sales += content.Amount;
                        //        }
                        //    }
                        //}

                        //tỉ lệ
                        ItemContent.SalesRatio = ItemContent.SalesPlanned == 0 || ItemContent.SalesPlanned == null ?
                            null : (decimal?)
                            Math.Round((ItemContent.Sales.Value / ItemContent.SalesPlanned.Value) * 100, 2);
                    }
                    #endregion

                    #region Số đơn hàng
                    //kế hoạch
                    ItemContent.SalesOrderPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.ORDER_NUMBER.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    ItemContent.SalesOrderIds = new HashSet<long>();
                    //foreach (var order in CustomerSalesOrderDAOs)
                    //{
                    //    foreach (var content in order.OrderRetailContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.SalesOrderIds.Add(content.OrderRetailId);
                    //    }
                    //}
                    //foreach (var order in OrderAgentDAOs)
                    //{
                    //    foreach (var content in order.DirectSalesOrderContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.SalesOrderIds.Add(content.DirectSalesOrderId);
                    //    }
                    //}
                    //foreach (var order in OrderProjectDAOs)
                    //{
                    //    foreach (var content in order.OrderProjectContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.SalesOrderIds.Add(content.OrderProjectId);
                    //    }
                    //}
                    //foreach (var order in OrderExportDAOs)
                    //{
                    //    foreach (var content in order.OrderExportContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.SalesOrderIds.Add(content.OrderExportId);
                    //    }
                    //}
                    //tỉ lệ
                    ItemContent.SalesOrderRatio = ItemContent.SalesOrderPlanned == null || ItemContent.SalesOrderPlanned == 0 ?
                        null : (decimal?)
                        Math.Round((ItemContent.SalesOrder.Value / ItemContent.SalesOrderPlanned.Value) * 100, 2);
                    #endregion

                    #region Số khách hàng
                    //kế hoạch
                    ItemContent.NumberOfCustomerPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.NUMBER_OF_CUSTOMER.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    ItemContent.NumberOfCustomerIds = new HashSet<long>();
                    //foreach (var order in CustomerSalesOrderDAOs)
                    //{
                    //    foreach (var content in order.OrderRetailContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.NumberOfCustomerIds.Add(content.OrderRetail.CustomerRetailId);
                    //    }
                    //}
                    //foreach (var order in OrderAgentDAOs)
                    //{
                    //    foreach (var content in order.DirectSalesOrderContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //        {
                    //            var customerAgentIds = DataContext.Customer.Select(p => p.CustomerId);
                    //            // Lấy danh sách customerAgent có số điện thoại nằm trong customer
                    //            var customer = DataContext.Customer.Where(p => p.Phone == content.DirectSalesOrder.PhoneNumber).FirstOrDefault();
                    //            if (DataContext.Customer.Any(p => p.Id == customer.Id))
                    //            {
                    //                ItemContent.NumberOfCustomerIds.Add(customer.Id);
                    //            }
                    //        }
                    //    }
                    //}
                    //foreach (var order in OrderProjectDAOs)
                    //{
                    //    foreach (var content in order.OrderProjectContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.NumberOfCustomerIds.Add(content.OrderProject.CustomerProjectId);
                    //    }
                    //}
                    //foreach (var order in OrderExportDAOs)
                    //{
                    //    foreach (var content in order.OrderExportContents)
                    //    {
                    //        if (content.ItemId == ItemContent.ItemId)
                    //            ItemContent.NumberOfCustomerIds.Add(content.OrderExport.CustomerExportId);
                    //    }
                    //}
                    //tỉ lệ
                    ItemContent.NumberOfCustomerRatio = ItemContent.NumberOfCustomerPlanned == null || ItemContent.NumberOfCustomerPlanned == 0 ?
                        null : (decimal?)
                        Math.Round((ItemContent.NumberOfCustomer.Value / ItemContent.NumberOfCustomerPlanned.Value) * 100, 2);
                    #endregion

                    #region Sản lượng theo hợp đồng
                    //kế hoạch
                    ItemContent.CountItemInContractPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.COUNT_ITEM_IN_CONTRACT.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    if (ItemContent.CountItemInContractPlanned == null)
                    {
                        ItemContent.CountItemInContract = null;
                    }
                    else
                    {
                        ItemContent.CountItemInContract = 0;
                        foreach (var contract in ContractDAOs)
                        {
                            foreach (var contractItemDetail in contract.ContractItemDetails)
                            {
                                if (contractItemDetail.ItemId == ItemContent.ItemId)
                                {
                                    ItemContent.CountItemInContract += contractItemDetail.Quantity;
                                }
                            }
                        }
                        //tỉ lệ
                        ItemContent.CountItemInContractRatio = ItemContent.CountItemInContractPlanned == 0 || ItemContent.CountItemInContractPlanned == null ?
                            null : (decimal?)
                            Math.Round((ItemContent.CountItemInContract.Value / ItemContent.CountItemInContractPlanned.Value) * 100, 2);
                    }
                    #endregion

                    #region Số lượng hợp đồng
                    //kế hoạch
                    ItemContent.CountContractPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.COUNT_CONTRACT.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    if (ItemContent.CountContractPlanned == null)
                    {
                        ItemContent.CountContract = null;
                    }
                    else
                    {
                        ItemContent.CountContract = ContractDAOs.Count();
                        //tỉ lệ
                        ItemContent.CountContractRatio = ItemContent.CountContractPlanned == 0 || ItemContent.CountContractPlanned == null ?
                            null : (decimal?)
                            Math.Round((ItemContent.CountContract.Value / ItemContent.CountContractPlanned.Value) * 100, 2);
                    }
                    #endregion

                    #region Doanh số theo hợp đồng
                    //kế hoạch
                    ItemContent.ReveuneContractPlanned = KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTOs
                            .Where(x => x.SaleEmployeeId == ItemContent.SaleEmployeeId &&
                            x.ItemId == ItemContent.ItemId &&
                            x.KpiCriteriaItemId == KpiCriteriaItemEnum.REVEUNE_CONTRACT.Id)
                            .Select(x => x.Value).FirstOrDefault();
                    //thực hiện
                    if (ItemContent.ReveuneContractPlanned == null)
                    {
                        ItemContent.ReveuneContract = null;
                    }
                    else
                    {
                        ItemContent.ReveuneContract = ContractDAOs.Sum(p => p.TotalValue);
                        //tỉ lệ
                        ItemContent.ReveuneContractRatio = ItemContent.ReveuneContractPlanned == 0 || ItemContent.ReveuneContractPlanned == null ?
                            null : (decimal?)
                            Math.Round((ItemContent.ReveuneContract.Value / ItemContent.ReveuneContractPlanned.Value) * 100, 2);
                    }
                    #endregion

                }
            };
            KpiItemReport_KpiItemReportDTOs = KpiItemReport_KpiItemReportDTOs.Where(x => x.ItemContents.Any()).ToList();
            return KpiItemReport_KpiItemReportDTOs;
        }

        [Route(KpiItemReportRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] KpiItemReport_KpiItemReportFilterDTO KpiItemReport_KpiItemReportFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (KpiItemReport_KpiItemReportFilterDTO.KpiPeriodId?.Equal.HasValue == false)
                return BadRequest(new { message = "Chưa chọn kì KPI" });
            if (KpiItemReport_KpiItemReportFilterDTO.KpiYearId?.Equal.HasValue == false)
                return BadRequest(new { message = "Chưa chọn năm KPI" });

            var KpiPeriod = KpiPeriodEnum.KpiPeriodEnumList.Where(x => x.Id == KpiItemReport_KpiItemReportFilterDTO.KpiPeriodId.Equal.Value).FirstOrDefault();
            var KpiYear = KpiYearEnum.KpiYearEnumList.Where(x => x.Id == KpiItemReport_KpiItemReportFilterDTO.KpiYearId.Equal.Value).FirstOrDefault();

            KpiItemReport_KpiItemReportFilterDTO.Skip = 0;
            KpiItemReport_KpiItemReportFilterDTO.Take = int.MaxValue;
            List<KpiItemReport_KpiItemReportDTO> KpiItemReport_KpiItemReportDTOs = (await List(KpiItemReport_KpiItemReportFilterDTO)).Value;

            long stt = 1;
            foreach (KpiItemReport_KpiItemReportDTO KpiItemReport_KpiItemReportDTO in KpiItemReport_KpiItemReportDTOs)
            {
                foreach (var ItemContent in KpiItemReport_KpiItemReportDTO.ItemContents)
                {
                    ItemContent.STT = stt;
                    stt++;
                }
            }

            List<KpiItemReport_ExportDTO> KpiItemReport_ExportDTOs = KpiItemReport_KpiItemReportDTOs?.Select(x => new KpiItemReport_ExportDTO(x)).ToList();
            string path = "Templates/Kpi_Item_Report.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            Data.KpiPeriod = KpiPeriod.Name;
            Data.KpiYear = KpiYear.Name;
            Data.KpiItemReports = KpiItemReport_ExportDTOs;
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };

            return File(output.ToArray(), "application/octet-stream", "KpiItemReport.xlsx");
        }


        private Tuple<DateTime, DateTime> DateTimeConvert(long KpiPeriodId, long KpiYearId)
        {
            DateTime startDate = StaticParams.DateTimeNow;
            DateTime endDate = StaticParams.DateTimeNow;
            if (KpiPeriodId <= Enums.KpiPeriodEnum.PERIOD_MONTH12.Id)
            {
                startDate = new DateTime((int)KpiYearId, (int)(KpiPeriodId % 100), 1);
                endDate = startDate.AddMonths(1).AddSeconds(-1);
            }
            else
            {
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER01.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 1, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER02.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 4, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER03.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 7, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_QUATER04.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 10, 1);
                    endDate = startDate.AddMonths(3).AddSeconds(-1);
                }
                if (KpiPeriodId == Enums.KpiPeriodEnum.PERIOD_YEAR01.Id)
                {
                    startDate = new DateTime((int)KpiYearId, 1, 1);
                    endDate = startDate.AddYears(1).AddSeconds(-1);
                }
            }

            return Tuple.Create(startDate, endDate);
        }
    }
}
