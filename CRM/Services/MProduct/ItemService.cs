using CRM.Common;
using CRM.Entities;
using CRM.Repositories;
using CRM.Services.MImage;
using CRM.Helpers;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.MProduct
{
    public interface IItemService : IServiceScoped
    {
        Task<int> Count(ItemFilter ItemFilter);
        Task<List<Item>> List(ItemFilter ItemFilter);
        Task<Item> Get(long Id);
        ItemFilter ToFilter(ItemFilter ItemFilter);
    }

    public class ItemService : BaseService, IItemService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IImageService ImageService;

        public ItemService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IImageService ImageService
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ImageService = ImageService;
        }
        public async Task<int> Count(ItemFilter ItemFilter)
        {
            try
            {
                int result = await UOW.ItemRepository.Count(ItemFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ItemService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ItemService));
                    throw new MessageException(ex.InnerException);
                };
            }
        }

        public async Task<List<Item>> List(ItemFilter ItemFilter)
        {
            try
            {
                List<Item> Items = await UOW.ItemRepository.List(ItemFilter);

                var Ids = Items.Select(x => x.Id).ToList();
                InventoryFilter InventoryFilter = new InventoryFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    ItemId = new IdFilter { In = Ids },
                    Selects = InventorySelect.SaleStock | InventorySelect.Item
                };

                var inventories = await UOW.InventoryRepository.List(InventoryFilter);
                var list = inventories.GroupBy(x => x.ItemId).Select(x => new { ItemId = x.Key, SaleStock = x.Sum(s => s.SaleStock) }).ToList();

                foreach (var item in Items)
                {
                    item.SaleStock = list.Where(i => i.ItemId == item.Id).Select(i => i.SaleStock).FirstOrDefault();
                    item.HasInventory = item.SaleStock > 0;
                }
                return Items;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ItemService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ItemService));
                    throw new MessageException(ex.InnerException);
                };
            }
        }
        public async Task<Item> Get(long Id)
        {
            var appUser = await UOW.AppUserRepository.Get(CurrentContext.UserId);
            Item Item = await UOW.ItemRepository.Get(Id);
            if (Item == null)
                return null;
            return Item;
        }

        public ItemFilter ToFilter(ItemFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ItemFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ItemFilter subFilter = new ItemFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ProductTypeId))
                        subFilter.ProductTypeId = FilterPermissionDefinition.IdFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ProductGroupingId))
                        subFilter.ProductGroupingId = FilterPermissionDefinition.IdFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SalePrice))
                        subFilter.SalePrice = FilterPermissionDefinition.DecimalFilter;
                }
            }
            return filter;
        }
    }
}
