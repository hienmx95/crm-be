using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;
using CRM.Enums;

namespace CRM.Services.MCustomerSalesOrder
{
    public interface ICustomerSalesOrderService :  IServiceScoped
    {
        Task<int> Count(CustomerSalesOrderFilter CustomerSalesOrderFilter);
        Task<List<CustomerSalesOrder>> List(CustomerSalesOrderFilter CustomerSalesOrderFilter);
        Task<CustomerSalesOrder> Get(long Id);
        Task<CustomerSalesOrder> Create(CustomerSalesOrder CustomerSalesOrder);
        Task<CustomerSalesOrder> Update(CustomerSalesOrder CustomerSalesOrder);
        Task<CustomerSalesOrder> Delete(CustomerSalesOrder CustomerSalesOrder);
        Task<List<CustomerSalesOrder>> BulkDelete(List<CustomerSalesOrder> CustomerSalesOrders);
        Task<List<CustomerSalesOrder>> Import(List<CustomerSalesOrder> CustomerSalesOrders);
        Task<CustomerSalesOrderFilter> ToFilter(CustomerSalesOrderFilter CustomerSalesOrderFilter);
    }

    public class CustomerSalesOrderService : BaseService, ICustomerSalesOrderService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerSalesOrderValidator CustomerSalesOrderValidator;

        public CustomerSalesOrderService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerSalesOrderValidator CustomerSalesOrderValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerSalesOrderValidator = CustomerSalesOrderValidator;
        }
        public async Task<int> Count(CustomerSalesOrderFilter CustomerSalesOrderFilter)
        {
            try
            {
                int result = await UOW.CustomerSalesOrderRepository.Count(CustomerSalesOrderFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return 0;
        }

        public async Task<List<CustomerSalesOrder>> List(CustomerSalesOrderFilter CustomerSalesOrderFilter)
        {
            try
            {
                List<CustomerSalesOrder> CustomerSalesOrders = await UOW.CustomerSalesOrderRepository.List(CustomerSalesOrderFilter);
                return CustomerSalesOrders;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return null;
        }

        public async Task<CustomerSalesOrder> Get(long Id)
        {
            CustomerSalesOrder CustomerSalesOrder = await UOW.CustomerSalesOrderRepository.Get(Id);
            if (CustomerSalesOrder == null)
                return null;
            if(CustomerSalesOrder.CustomerTypeId == CustomerTypeEnum.COMPANY.Id)
            {
                var CustomerPhones = await UOW.CustomerPhoneRepository.List(new CustomerPhoneFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Selects = CustomerPhoneSelect.ALL,
                    OrderBy = CustomerPhoneOrder.Id,
                    OrderType = OrderType.ASC,
                    CustomerId = new IdFilter { Equal = CustomerSalesOrder.CustomerId }
                });

                var CustomerEmails = await UOW.CustomerEmailRepository.List(new CustomerEmailFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Selects = CustomerEmailSelect.ALL,
                    OrderBy = CustomerEmailOrder.Id,
                    OrderType = OrderType.ASC,
                    CustomerId = new IdFilter { Equal = CustomerSalesOrder.CustomerId }
                });

                CustomerSalesOrder.Customer.Phone = CustomerPhones.Select(x => x.Phone).FirstOrDefault();
                CustomerSalesOrder.Customer.Email = CustomerEmails.Select(x => x.Email).FirstOrDefault();
            }
            return CustomerSalesOrder;
        }
        public async Task<CustomerSalesOrder> Create(CustomerSalesOrder CustomerSalesOrder)
        {
            if (!await CustomerSalesOrderValidator.Create(CustomerSalesOrder))
                return CustomerSalesOrder;

            try
            {
                var SalesEmployee = await UOW.AppUserRepository.Get(CustomerSalesOrder.SalesEmployeeId);
                CustomerSalesOrder.CreatorId = CurrentContext.UserId;
                CustomerSalesOrder.OrganizationId = SalesEmployee.OrganizationId;
                CustomerSalesOrder.RequestStateId = RequestStateEnum.NEW.Id;
                await Calculator(CustomerSalesOrder);
                await UOW.CustomerSalesOrderRepository.Create(CustomerSalesOrder);
                CustomerSalesOrder = await UOW.CustomerSalesOrderRepository.Get(CustomerSalesOrder.Id);
                await Logging.CreateAuditLog(CustomerSalesOrder, new { }, nameof(CustomerSalesOrderService));
                return CustomerSalesOrder;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return null;
        }

        public async Task<CustomerSalesOrder> Update(CustomerSalesOrder CustomerSalesOrder)
        {
            if (!await CustomerSalesOrderValidator.Update(CustomerSalesOrder))
                return CustomerSalesOrder;
            try
            {
                var oldData = await UOW.CustomerSalesOrderRepository.Get(CustomerSalesOrder.Id);

                await UOW.CustomerSalesOrderRepository.Update(CustomerSalesOrder);

                CustomerSalesOrder = await UOW.CustomerSalesOrderRepository.Get(CustomerSalesOrder.Id);
                await Logging.CreateAuditLog(CustomerSalesOrder, oldData, nameof(CustomerSalesOrderService));
                return CustomerSalesOrder;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return null;
        }

        public async Task<CustomerSalesOrder> Delete(CustomerSalesOrder CustomerSalesOrder)
        {
            if (!await CustomerSalesOrderValidator.Delete(CustomerSalesOrder))
                return CustomerSalesOrder;

            try
            {
                await UOW.CustomerSalesOrderRepository.Delete(CustomerSalesOrder);
                await Logging.CreateAuditLog(new { }, CustomerSalesOrder, nameof(CustomerSalesOrderService));
                return CustomerSalesOrder;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return null;
        }

        public async Task<List<CustomerSalesOrder>> BulkDelete(List<CustomerSalesOrder> CustomerSalesOrders)
        {
            if (!await CustomerSalesOrderValidator.BulkDelete(CustomerSalesOrders))
                return CustomerSalesOrders;

            try
            {
                await UOW.CustomerSalesOrderRepository.BulkDelete(CustomerSalesOrders);
                await Logging.CreateAuditLog(new { }, CustomerSalesOrders, nameof(CustomerSalesOrderService));
                return CustomerSalesOrders;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return null;

        }
        
        public async Task<List<CustomerSalesOrder>> Import(List<CustomerSalesOrder> CustomerSalesOrders)
        {
            if (!await CustomerSalesOrderValidator.Import(CustomerSalesOrders))
                return CustomerSalesOrders;
            try
            {
                await UOW.CustomerSalesOrderRepository.BulkMerge(CustomerSalesOrders);

                await Logging.CreateAuditLog(CustomerSalesOrders, new { }, nameof(CustomerSalesOrderService));
                return CustomerSalesOrders;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerSalesOrderService));
            }
            return null;
        }

        private async Task<CustomerSalesOrder> Calculator(CustomerSalesOrder CustomerSalesOrder)
        {
            var ProductIds = new List<long>();
            var ItemIds = new List<long>();
            if (CustomerSalesOrder.CustomerSalesOrderContents != null)
            {
                ProductIds.AddRange(CustomerSalesOrder.CustomerSalesOrderContents.Select(x => x.Item.ProductId).ToList());
                ItemIds.AddRange(CustomerSalesOrder.CustomerSalesOrderContents.Select(x => x.ItemId).ToList());
            }
            if (CustomerSalesOrder.CustomerSalesOrderPromotions != null)
            {
                ProductIds.AddRange(CustomerSalesOrder.CustomerSalesOrderPromotions.Select(x => x.Item.ProductId).ToList());
                ItemIds.AddRange(CustomerSalesOrder.CustomerSalesOrderPromotions.Select(x => x.ItemId).ToList());
            }
            ProductIds = ProductIds.Distinct().ToList();
            ItemIds = ItemIds.Distinct().ToList();

            ItemFilter ItemFilter = new ItemFilter
            {
                Skip = 0,
                Take = ItemIds.Count,
                Id = new IdFilter { In = ItemIds },
                Selects = ItemSelect.ALL,
            };
            var Items = await UOW.ItemRepository.List(ItemFilter);

            var Products = await UOW.ProductRepository.List(new ProductFilter
            {
                Id = new IdFilter { In = ProductIds },
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProductSelect.UnitOfMeasure | ProductSelect.UnitOfMeasureGrouping | ProductSelect.Id | ProductSelect.TaxType
            });

            var UOMGs = await UOW.UnitOfMeasureGroupingRepository.List(new UnitOfMeasureGroupingFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = UnitOfMeasureGroupingSelect.Id | UnitOfMeasureGroupingSelect.UnitOfMeasure | UnitOfMeasureGroupingSelect.UnitOfMeasureGroupingContents
            });

            //sản phẩm bán
            if (CustomerSalesOrder.CustomerSalesOrderContents != null)
            {
                foreach (var CustomerSalesOrderContent in CustomerSalesOrder.CustomerSalesOrderContents)
                {
                    var Item = Items.Where(x => x.Id == CustomerSalesOrderContent.ItemId).FirstOrDefault();
                    var Product = Products.Where(x => CustomerSalesOrderContent.Item.ProductId == x.Id).FirstOrDefault();
                    CustomerSalesOrderContent.PrimaryUnitOfMeasureId = Product.UnitOfMeasureId;

                    List<UnitOfMeasure> UnitOfMeasures = new List<UnitOfMeasure>();
                    if (Product.UnitOfMeasureGroupingId.HasValue)
                    {
                        var UOMG = UOMGs.Where(x => x.Id == Product.UnitOfMeasureGroupingId).FirstOrDefault();
                        UnitOfMeasures = UOMG.UnitOfMeasureGroupingContents.Select(x => new UnitOfMeasure
                        {
                            Id = x.UnitOfMeasure.Id,
                            Code = x.UnitOfMeasure.Code,
                            Name = x.UnitOfMeasure.Name,
                            Description = x.UnitOfMeasure.Description,
                            StatusId = x.UnitOfMeasure.StatusId,
                            Factor = x.Factor
                        }).ToList();
                    }

                    UnitOfMeasures.Add(new UnitOfMeasure
                    {
                        Id = Product.UnitOfMeasure.Id,
                        Code = Product.UnitOfMeasure.Code,
                        Name = Product.UnitOfMeasure.Name,
                        Description = Product.UnitOfMeasure.Description,
                        StatusId = Product.UnitOfMeasure.StatusId,
                        Factor = 1
                    });
                    var UOM = UnitOfMeasures.Where(x => CustomerSalesOrderContent.UnitOfMeasureId == x.Id).FirstOrDefault();
                    //CustomerSalesOrderContent.TaxPercentage = Product.TaxType.Percentage;
                    CustomerSalesOrderContent.RequestedQuantity = CustomerSalesOrderContent.Quantity * UOM.Factor.Value;

                    //Trường hợp không sửa giá, giá bán = giá bán cơ sở của sản phẩm * hệ số quy đổi của đơn vị tính
                    if (CustomerSalesOrder.EditedPriceStatusId == EditedPriceStatusEnum.INACTIVE.Id)
                    {
                        CustomerSalesOrderContent.PrimaryPrice = Item.SalePrice.GetValueOrDefault(0);
                        CustomerSalesOrderContent.SalePrice = CustomerSalesOrderContent.PrimaryPrice * UOM.Factor.Value;
                        CustomerSalesOrderContent.EditedPriceStatusId = EditedPriceStatusEnum.INACTIVE.Id;
                    }

                    if (CustomerSalesOrder.EditedPriceStatusId == EditedPriceStatusEnum.ACTIVE.Id)
                    {
                        CustomerSalesOrderContent.SalePrice = CustomerSalesOrderContent.PrimaryPrice * UOM.Factor.Value;
                        if (Item.SalePrice == CustomerSalesOrderContent.PrimaryPrice)
                            CustomerSalesOrderContent.EditedPriceStatusId = EditedPriceStatusEnum.INACTIVE.Id;
                        else
                            CustomerSalesOrderContent.EditedPriceStatusId = EditedPriceStatusEnum.ACTIVE.Id;
                    }

                    //giá tiền từng line trước chiết khấu
                    var SubAmount = CustomerSalesOrderContent.Quantity * CustomerSalesOrderContent.SalePrice;
                    if (CustomerSalesOrderContent.DiscountPercentage.HasValue)
                    {
                        CustomerSalesOrderContent.DiscountAmount = SubAmount * CustomerSalesOrderContent.DiscountPercentage.Value / 100;
                        CustomerSalesOrderContent.DiscountAmount = Math.Round(CustomerSalesOrderContent.DiscountAmount ?? 0, 0);
                        CustomerSalesOrderContent.Amount = SubAmount - CustomerSalesOrderContent.DiscountAmount.Value;
                    }
                    else
                    {
                        CustomerSalesOrderContent.Amount = SubAmount;
                    }
                }

                //tổng trước chiết khấu
                CustomerSalesOrder.SubTotal = CustomerSalesOrder.CustomerSalesOrderContents.Sum(x => x.Amount);

                //tính tổng chiết khấu theo % chiết khấu chung
                if (CustomerSalesOrder.GeneralDiscountPercentage.HasValue && CustomerSalesOrder.GeneralDiscountPercentage > 0)
                {
                    CustomerSalesOrder.GeneralDiscountAmount = CustomerSalesOrder.SubTotal * (CustomerSalesOrder.GeneralDiscountPercentage / 100);
                    CustomerSalesOrder.GeneralDiscountAmount = Math.Round(CustomerSalesOrder.GeneralDiscountAmount.Value, 0);
                }
                foreach (var CustomerSalesOrderContent in CustomerSalesOrder.CustomerSalesOrderContents)
                {
                    //phân bổ chiết khấu chung = tổng chiết khấu chung * (tổng từng line/tổng trc chiết khấu)
                    CustomerSalesOrderContent.GeneralDiscountPercentage = CustomerSalesOrderContent.Amount / CustomerSalesOrder.SubTotal * 100;
                    CustomerSalesOrderContent.GeneralDiscountAmount = CustomerSalesOrder.GeneralDiscountAmount * CustomerSalesOrderContent.GeneralDiscountPercentage / 100;
                    CustomerSalesOrderContent.GeneralDiscountAmount = Math.Round(CustomerSalesOrderContent.GeneralDiscountAmount ?? 0, 0);
                    //thuê từng line = (tổng từng line - chiết khấu phân bổ) * % thuế
                    CustomerSalesOrderContent.TaxAmount = (CustomerSalesOrderContent.Amount - (CustomerSalesOrderContent.GeneralDiscountAmount.HasValue ? CustomerSalesOrderContent.GeneralDiscountAmount.Value : 0)) * CustomerSalesOrderContent.TaxPercentage / 100;
                    CustomerSalesOrderContent.TaxAmount = Math.Round(CustomerSalesOrderContent.TaxAmount ?? 0, 0);
                }

                CustomerSalesOrder.TotalTax = CustomerSalesOrder.CustomerSalesOrderContents.Where(x => x.TaxAmount.HasValue).Sum(x => x.TaxAmount.Value);
                CustomerSalesOrder.TotalTax = Math.Round(CustomerSalesOrder.TotalTax, 0);
                CustomerSalesOrder.Total = CustomerSalesOrder.SubTotal - (CustomerSalesOrder.GeneralDiscountAmount.HasValue ? CustomerSalesOrder.GeneralDiscountAmount.Value : 0) + CustomerSalesOrder.TotalTax;
            }
            else
            {
                CustomerSalesOrder.SubTotal = 0;
                CustomerSalesOrder.GeneralDiscountPercentage = null;
                CustomerSalesOrder.GeneralDiscountAmount = null;
                CustomerSalesOrder.TotalTax = 0;
                CustomerSalesOrder.Total = 0;
            }

            //sản phẩm khuyến mãi
            if (CustomerSalesOrder.CustomerSalesOrderPromotions != null)
            {
                foreach (var CustomerSalesOrderPromotion in CustomerSalesOrder.CustomerSalesOrderPromotions)
                {
                    var Product = Products.Where(x => CustomerSalesOrderPromotion.Item.ProductId == x.Id).FirstOrDefault();
                    CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId = Product.UnitOfMeasureId;

                    List<UnitOfMeasure> UnitOfMeasures = new List<UnitOfMeasure>();
                    if (Product.UnitOfMeasureGroupingId.HasValue)
                    {
                        var UOMG = UOMGs.Where(x => x.Id == Product.UnitOfMeasureGroupingId).FirstOrDefault();
                        UnitOfMeasures = UOMG.UnitOfMeasureGroupingContents.Select(x => new UnitOfMeasure
                        {
                            Id = x.UnitOfMeasure.Id,
                            Code = x.UnitOfMeasure.Code,
                            Name = x.UnitOfMeasure.Name,
                            Description = x.UnitOfMeasure.Description,
                            StatusId = x.UnitOfMeasure.StatusId,
                            Factor = x.Factor
                        }).ToList();
                    }

                    UnitOfMeasures.Add(new UnitOfMeasure
                    {
                        Id = Product.UnitOfMeasure.Id,
                        Code = Product.UnitOfMeasure.Code,
                        Name = Product.UnitOfMeasure.Name,
                        Description = Product.UnitOfMeasure.Description,
                        StatusId = Product.UnitOfMeasure.StatusId,
                        Factor = 1
                    });
                    var UOM = UnitOfMeasures.Where(x => CustomerSalesOrderPromotion.UnitOfMeasureId == x.Id).FirstOrDefault();
                    CustomerSalesOrderPromotion.RequestedQuantity = CustomerSalesOrderPromotion.Quantity * UOM.Factor.Value;
                }
            }

            return CustomerSalesOrder;
        }

        public async Task<CustomerSalesOrderFilter> ToFilter(CustomerSalesOrderFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerSalesOrderFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerSalesOrderFilter subFilter = new CustomerSalesOrderFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerTypeId))
                        subFilter.CustomerTypeId = FilterBuilder.Merge(subFilter.CustomerTypeId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OpportunityId))
                        subFilter.OpportunityId = FilterBuilder.Merge(subFilter.OpportunityId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ContractId))
                        subFilter.ContractId = FilterBuilder.Merge(subFilter.ContractId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderPaymentStatusId))
                        subFilter.OrderPaymentStatusId = FilterBuilder.Merge(subFilter.OrderPaymentStatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.RequestStateId))
                        subFilter.RequestStateId = FilterBuilder.Merge(subFilter.RequestStateId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.EditedPriceStatusId))
                        subFilter.EditedPriceStatusId = FilterBuilder.Merge(subFilter.EditedPriceStatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ShippingName))
                        subFilter.ShippingName = FilterBuilder.Merge(subFilter.ShippingName, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderDate))
                        subFilter.OrderDate = FilterBuilder.Merge(subFilter.OrderDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryDate))
                        subFilter.DeliveryDate = FilterBuilder.Merge(subFilter.DeliveryDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SalesEmployeeId))
                        subFilter.SalesEmployeeId = FilterBuilder.Merge(subFilter.SalesEmployeeId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Note))
                        subFilter.Note = FilterBuilder.Merge(subFilter.Note, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.InvoiceAddress))
                        subFilter.InvoiceAddress = FilterBuilder.Merge(subFilter.InvoiceAddress, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.InvoiceNationId))
                        subFilter.InvoiceNationId = FilterBuilder.Merge(subFilter.InvoiceNationId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.InvoiceProvinceId))
                        subFilter.InvoiceProvinceId = FilterBuilder.Merge(subFilter.InvoiceProvinceId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.InvoiceDistrictId))
                        subFilter.InvoiceDistrictId = FilterBuilder.Merge(subFilter.InvoiceDistrictId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.InvoiceWardId))
                        subFilter.InvoiceWardId = FilterBuilder.Merge(subFilter.InvoiceWardId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.InvoiceZIPCode))
                        subFilter.InvoiceZIPCode = FilterBuilder.Merge(subFilter.InvoiceZIPCode, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryAddress))
                        subFilter.DeliveryAddress = FilterBuilder.Merge(subFilter.DeliveryAddress, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryNationId))
                        subFilter.DeliveryNationId = FilterBuilder.Merge(subFilter.DeliveryNationId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryProvinceId))
                        subFilter.DeliveryProvinceId = FilterBuilder.Merge(subFilter.DeliveryProvinceId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryDistrictId))
                        subFilter.DeliveryDistrictId = FilterBuilder.Merge(subFilter.DeliveryDistrictId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryWardId))
                        subFilter.DeliveryWardId = FilterBuilder.Merge(subFilter.DeliveryWardId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DeliveryZIPCode))
                        subFilter.DeliveryZIPCode = FilterBuilder.Merge(subFilter.DeliveryZIPCode, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SubTotal))
                        subFilter.SubTotal = FilterBuilder.Merge(subFilter.SubTotal, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.GeneralDiscountPercentage))
                        subFilter.GeneralDiscountPercentage = FilterBuilder.Merge(subFilter.GeneralDiscountPercentage, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.GeneralDiscountAmount))
                        subFilter.GeneralDiscountAmount = FilterBuilder.Merge(subFilter.GeneralDiscountAmount, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TotalTaxOther))
                        subFilter.TotalTaxOther = FilterBuilder.Merge(subFilter.TotalTaxOther, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TotalTax))
                        subFilter.TotalTax = FilterBuilder.Merge(subFilter.TotalTax, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Total))
                        subFilter.Total = FilterBuilder.Merge(subFilter.Total, FilterPermissionDefinition.DecimalFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CreatorId))
                        subFilter.CreatorId = FilterBuilder.Merge(subFilter.CreatorId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                        subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
