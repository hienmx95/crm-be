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

namespace CRM.Services.MColor
{
    public interface IColorService :  IServiceScoped
    {
        Task<int> Count(ColorFilter ColorFilter);
        Task<List<Color>> List(ColorFilter ColorFilter);
        Task<Color> Get(long Id);
    }

    public class ColorService : BaseService, IColorService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public ColorService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(ColorFilter ColorFilter)
        {
            try
            {
                int result = await UOW.ColorRepository.Count(ColorFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ColorService));
            }
            return 0;
        }

        public async Task<List<Color>> List(ColorFilter ColorFilter)
        {
            try
            {
                List<Color> Colors = await UOW.ColorRepository.List(ColorFilter);
                return Colors;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ColorService));
            }
            return null;
        }
        
        public async Task<Color> Get(long Id)
        {
            Color Color = await UOW.ColorRepository.Get(Id);
            if (Color == null)
                return null;
            return Color;
        }
    }
}
