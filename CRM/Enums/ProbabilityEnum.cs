using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class ProbabilityEnum
    {
        public static GenericEnum ZERO_PERCENT = new GenericEnum { Id = 1, Code = "ZERO_PERCENT", Name = "0%" };
        public static GenericEnum TEN_PERCENT = new GenericEnum { Id = 2, Code = "TEN_PERCENT", Name = "10%" };
        public static GenericEnum TWENTY_PERCEN = new GenericEnum { Id = 3, Code = "TWENTY_PERCEN", Name = "20%" };
        public static GenericEnum THIRTY_PERCENT = new GenericEnum { Id = 4, Code = "THIRTY_PERCENT", Name = "30%" };
        public static GenericEnum FORTY_PERCENT = new GenericEnum { Id = 5, Code = "FORTY_PERCENT", Name = "40%" };
        public static GenericEnum FIFTY_PERCENT = new GenericEnum { Id = 6, Code = "FIFTY_PERCENT", Name = "50%" };
        public static GenericEnum SIXTY_PERCENT = new GenericEnum { Id = 7, Code = "SIXTY_PERCENT", Name = "60%" };
        public static GenericEnum SEVENTY_PERCENT = new GenericEnum { Id = 8, Code = "SEVENTY_PERCENT", Name = "70%" };
        public static GenericEnum EIGHTY_PERCENT = new GenericEnum { Id = 9, Code = "EIGHTY_PERCENT", Name = "80%" };
        public static GenericEnum NINETY_PERCENT = new GenericEnum { Id = 10, Code = "NINETY_PERCENT", Name = "90%" };
        public static GenericEnum ONE_HUNDRED_PERCENT = new GenericEnum { Id = 11, Code = "ONE_HUNDRED_PERCENT", Name = "100%" };

        public static List<GenericEnum> ProbabilityEnumList = new List<GenericEnum>()
        {
            ZERO_PERCENT, TEN_PERCENT, TWENTY_PERCEN, THIRTY_PERCENT, ONE_HUNDRED_PERCENT,
            FIFTY_PERCENT, SIXTY_PERCENT, SEVENTY_PERCENT, EIGHTY_PERCENT, NINETY_PERCENT, FORTY_PERCENT
        };
    }
}
