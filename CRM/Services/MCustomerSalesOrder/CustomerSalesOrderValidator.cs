using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MCustomerSalesOrder
{
    public interface ICustomerSalesOrderValidator : IServiceScoped
    {
        Task<bool> Create(CustomerSalesOrder CustomerSalesOrder);
        Task<bool> Update(CustomerSalesOrder CustomerSalesOrder);
        Task<bool> Delete(CustomerSalesOrder CustomerSalesOrder);
        Task<bool> BulkDelete(List<CustomerSalesOrder> CustomerSalesOrders);
        Task<bool> Import(List<CustomerSalesOrder> CustomerSalesOrders);
    }

    public class CustomerSalesOrderValidator : ICustomerSalesOrderValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CustomerTypeEmpty,
            CustomerNotExisted,
            CustomerEmpty,
            SalesEmployeeNotExisted,
            SalesEmployeeEmpty,
            OrderDateEmpty,
            EditedPriceStatusNotExisted,
            PriceOutOfRange,
            UnitOfMeasureEmpty,
            UnitOfMeasureNotExisted,
            QuantityEmpty,
            ItemNotExisted,
            QuantityInvalid,
            ContentEmpty,
            DeliveryDateInvalid,
            OrganizationInvalid,
            CustomerInvalid,
            ItemInvalid
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerSalesOrderValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerSalesOrder CustomerSalesOrder)
        {
            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerSalesOrder.Id },
                Selects = CustomerSalesOrderSelect.Id
            };

            int count = await UOW.CustomerSalesOrderRepository.Count(CustomerSalesOrderFilter);
            if (count == 0)
                CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateCustomerType(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.CustomerTypeId == 0)
                CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.CustomerType), ErrorCode.CustomerTypeEmpty);

            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidateCustomer(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.CustomerId == 0)
                CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.Customer), ErrorCode.CustomerEmpty);
            else
            {
                CustomerFilter CustomerFilter = new CustomerFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { In = new List<long> { CustomerSalesOrder.CustomerId } },
                    CustomerTypeId = new IdFilter { Equal = CustomerSalesOrder .CustomerTypeId },
                    StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id },
                    Selects = CustomerSelect.Id
                };

                int count = await UOW.CustomerRepository.Count(CustomerFilter);
                if (count == 0)
                    CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.Customer), ErrorCode.CustomerNotExisted);
            }

            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidateEmployee(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.SalesEmployeeId == 0)
                CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.SalesEmployee), ErrorCode.SalesEmployeeEmpty);
            else
            {
                AppUserFilter AppUserFilter = new AppUserFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = CustomerSalesOrder.SalesEmployeeId },
                    StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id },
                    Selects = AppUserSelect.Id | AppUserSelect.Organization
                };

                var count = await UOW.AppUserRepository.Count(AppUserFilter);
                if (count == 0)
                    CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.SalesEmployee), ErrorCode.SalesEmployeeNotExisted);
            }

            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidateOrderDate(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.OrderDate == default(DateTime))
                CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.OrderDate), ErrorCode.OrderDateEmpty);
            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidateDeliveryDate(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.DeliveryDate.HasValue)
            {
                if (CustomerSalesOrder.DeliveryDate.Value < CustomerSalesOrder.OrderDate)
                {
                    CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.DeliveryDate), ErrorCode.DeliveryDateInvalid);
                }
            }
            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidateContent(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.CustomerSalesOrderContents != null && CustomerSalesOrder.CustomerSalesOrderContents.Any())
            {
                var ItemIds = CustomerSalesOrder.CustomerSalesOrderContents.Select(x => x.ItemId).ToList();
                var ProductIds = CustomerSalesOrder.CustomerSalesOrderContents.Select(x => x.Item.ProductId).ToList();
                List<Item> Items = await UOW.ItemRepository.List(new ItemFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Id = new IdFilter { In = ItemIds },
                    Selects = ItemSelect.Id | ItemSelect.SalePrice | ItemSelect.ProductId
                });

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

                foreach (var CustomerSalesOrderContent in CustomerSalesOrder.CustomerSalesOrderContents)
                {
                    if (CustomerSalesOrderContent.UnitOfMeasureId == 0)
                        CustomerSalesOrderContent.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderContent.UnitOfMeasure), ErrorCode.UnitOfMeasureEmpty);
                    else
                    {
                        var Item = Items.Where(x => x.Id == CustomerSalesOrderContent.ItemId).FirstOrDefault();
                        if (Item == null)
                        {
                            CustomerSalesOrderContent.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderContent.Item), ErrorCode.ItemNotExisted);
                        }
                        else
                        {
                            var Product = Products.Where(x => Item.ProductId == x.Id).FirstOrDefault();
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
                            if (UOM == null)
                                CustomerSalesOrderContent.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderContent.UnitOfMeasure), ErrorCode.UnitOfMeasureNotExisted);
                            //else
                            //{
                            //    if(CustomerSalesOrder.EditedPriceStatusId == EditedPriceStatusEnum.ACTIVE.Id)
                            //    {
                            //        if(CustomerSalesOrderContent.SalePrice < (Item.SalePrice * UOM.Factor.Value) * 0.9m
                            //            || CustomerSalesOrderContent.SalePrice > (Item.SalePrice * UOM.Factor.Value) * 1.1m)
                            //            CustomerSalesOrderContent.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderContent.SalePrice), ErrorCode.PriceOutOfRange);
                            //    }
                            //}
                        }

                        if (CustomerSalesOrderContent.Quantity <= 0)
                            CustomerSalesOrderContent.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderContent.Quantity), ErrorCode.QuantityEmpty);

                    }

                }
            }
            else
            {
                if (CustomerSalesOrder.CustomerSalesOrderPromotions == null || !CustomerSalesOrder.CustomerSalesOrderPromotions.Any())
                {
                    CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.Id), ErrorCode.ContentEmpty);
                }
            }

            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidatePromotion(CustomerSalesOrder CustomerSalesOrder)
        {
            if (CustomerSalesOrder.CustomerSalesOrderPromotions != null)
            {
                await ValidateItem(CustomerSalesOrder);
                //validate đơn vị tính sản phẩm khuyến mãi
                var Ids = CustomerSalesOrder.CustomerSalesOrderPromotions.Select(x => x.UnitOfMeasureId).ToList();
                var UnitOfMeasureFilter = new UnitOfMeasureFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Id = new IdFilter { In = Ids },
                    StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id },
                    Selects = UnitOfMeasureSelect.Id
                };

                var listIdsInDB = (await UOW.UnitOfMeasureRepository.List(UnitOfMeasureFilter)).Select(x => x.Id);
                var listIdsNotExisted = Ids.Except(listIdsInDB);

                foreach (var CustomerSalesOrderPromotion in CustomerSalesOrder.CustomerSalesOrderPromotions)
                {
                    if (listIdsNotExisted.Contains(CustomerSalesOrderPromotion.UnitOfMeasureId))
                        CustomerSalesOrderPromotion.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderPromotion.UnitOfMeasure), ErrorCode.UnitOfMeasureEmpty);
                    //validate số lượng
                    if (CustomerSalesOrderPromotion.Quantity <= 0)
                        CustomerSalesOrderPromotion.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderPromotion.Quantity), ErrorCode.QuantityEmpty);
                }

            }
            return CustomerSalesOrder.IsValidated;
        }


        private async Task<bool> ValidateItem(CustomerSalesOrder CustomerSalesOrder)
        {

            if (CustomerSalesOrder.CustomerSalesOrderPromotions != null)
            {
                var Ids = CustomerSalesOrder.CustomerSalesOrderPromotions.Select(x => x.ItemId).ToList();
                var ItemFilter = new ItemFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Id = new IdFilter { In = Ids },
                    StatusId = new IdFilter { Equal = Enums.StatusEnum.ACTIVE.Id },
                    Selects = ItemSelect.Id
                };

                var listIdsInDB = (await UOW.ItemRepository.List(ItemFilter)).Select(x => x.Id);
                var listIdsNotExisted = Ids.Except(listIdsInDB);
                foreach (var CustomerSalesOrderPromotion in CustomerSalesOrder.CustomerSalesOrderPromotions)
                {
                    if (listIdsNotExisted.Contains(CustomerSalesOrderPromotion.ItemId))
                        CustomerSalesOrderPromotion.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrderPromotion.Item), ErrorCode.ItemNotExisted);
                }
            }
            return CustomerSalesOrder.IsValidated;
        }

        private async Task<bool> ValidateEditedPrice(CustomerSalesOrder CustomerSalesOrder)
        {
            if (EditedPriceStatusEnum.ACTIVE.Id != CustomerSalesOrder.EditedPriceStatusId && EditedPriceStatusEnum.INACTIVE.Id != CustomerSalesOrder.EditedPriceStatusId)
                CustomerSalesOrder.AddError(nameof(CustomerSalesOrderValidator), nameof(CustomerSalesOrder.EditedPriceStatus), ErrorCode.EditedPriceStatusNotExisted);
            return CustomerSalesOrder.IsValidated;
        }

        public async Task<bool>Create(CustomerSalesOrder CustomerSalesOrder)
        {
            await ValidateCustomerType(CustomerSalesOrder);
            await ValidateCustomer(CustomerSalesOrder);
            await ValidateEmployee(CustomerSalesOrder);
            await ValidateOrderDate(CustomerSalesOrder);
            await ValidateDeliveryDate(CustomerSalesOrder);
            await ValidateContent(CustomerSalesOrder);
            await ValidatePromotion(CustomerSalesOrder);
            await ValidateItem(CustomerSalesOrder);
            await ValidateEditedPrice(CustomerSalesOrder);
            return CustomerSalesOrder.IsValidated;
        }

        public async Task<bool> Update(CustomerSalesOrder CustomerSalesOrder)
        {
            if (await ValidateId(CustomerSalesOrder))
            {
                await ValidateCustomerType(CustomerSalesOrder);
                await ValidateCustomer(CustomerSalesOrder);
                await ValidateEmployee(CustomerSalesOrder);
                await ValidateOrderDate(CustomerSalesOrder);
                await ValidateDeliveryDate(CustomerSalesOrder);
                await ValidateContent(CustomerSalesOrder);
                await ValidatePromotion(CustomerSalesOrder);
                await ValidateItem(CustomerSalesOrder);
                await ValidateEditedPrice(CustomerSalesOrder);
            }
            return CustomerSalesOrder.IsValidated;
        }

        public async Task<bool> Delete(CustomerSalesOrder CustomerSalesOrder)
        {
            if (await ValidateId(CustomerSalesOrder))
            {
            }
            return CustomerSalesOrder.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerSalesOrder> CustomerSalesOrders)
        {
            foreach (CustomerSalesOrder CustomerSalesOrder in CustomerSalesOrders)
            {
                await Delete(CustomerSalesOrder);
            }
            return CustomerSalesOrders.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerSalesOrder> CustomerSalesOrders)
        {
            return true;
        }
    }
}
