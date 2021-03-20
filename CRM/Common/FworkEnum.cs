using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common
{
    public class FworkEnum
    {
        public static string APP_CODE = "crm-143a1ca7";
        public static string URI_CHECK_PERMISION = "https://dev.fpt.work/api/v1/iam/authorization/check-permission/" + APP_CODE;
        public static string URI_AUTHORIZATION = "https://dev.fpt.work/api/v1/iam/authorization/" + APP_CODE;
        public static string PrefixCookie = "Bearer ";
    }
}
