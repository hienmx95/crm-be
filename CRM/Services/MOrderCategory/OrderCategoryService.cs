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

namespace CRM.Services.MOrderCategory
{
    public interface IOrderCategoryService :  IServiceScoped
    {
        Task<int> Count(OrderCategoryFilter OrderCategoryFilter);
        Task<List<OrderCategory>> List(OrderCategoryFilter OrderCategoryFilter);
        Task<OrderCategory> Get(long Id);
    }

    public class OrderCategoryService : BaseService, IOrderCategoryService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public OrderCategoryService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(OrderCategoryFilter OrderCategoryFilter)
        {
            try
            {
                int result = await UOW.OrderCategoryRepository.Count(OrderCategoryFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OrderCategoryService));
            }
            return 0;
        }

        public async Task<List<OrderCategory>> List(OrderCategoryFilter OrderCategoryFilter)
        {
            try
            {
                List<OrderCategory> OrderCategorys = await UOW.OrderCategoryRepository.List(OrderCategoryFilter);
                return OrderCategorys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OrderCategoryService));
            }
            return null;
        }
        
        public async Task<OrderCategory> Get(long Id)
        {
            OrderCategory OrderCategory = await UOW.OrderCategoryRepository.Get(Id);
            if (OrderCategory == null)
                return null;
            return OrderCategory;
        }
    }
}
