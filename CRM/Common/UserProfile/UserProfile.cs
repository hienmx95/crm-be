using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common.UserProfile
{
    public class UserProfile
    {
        public bool success { get; set; }
        public UserInformation data { get; set; }
    }
    public class UserInformation
    {
        public string _id { get; set; }
        public bool is2FAEnabled { get; set; }
        public string email { get; set; }
        public DateTime changePasswordDate { get; set; }
        public Company company { get; set; }
        //public Company companies { get; set; }
        public Profile profile { get; set; }
        public Setting setting { get; set; }
    }
    public class Company
    {
        public string _id { get; set; }
        public List<Service> service { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string phone { get; set; }
        public string size { get; set; }
        public string represent { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public string __v { get; set; }
        public string isPlan { get; set; }

    }
    public class Service
    {
        public string code { get; set; }
        public bool status { get; set; }
    }
    public class Profile
    {
        public string _id { get; set; }
        //public microsoftAuthentication microsoftAuthentication { get; set; }
        public string microsoftMail { get; set; }
        public string status { get; set; }
        public string verifyEmailStatus { get; set; }
        public string code { get; set; }
        public string user { get; set; }
        public string company { get; set; }
        public string organization { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public string avatar { get; set; }
    }
    public class Setting
    {
        public string lng { get; set; }
        public string timeFormat { get; set; }
        public DateTime dateFormat { get; set; }
        public string timeZone { get; set; }
        public string firstDayOfWeek { get; set; }
        public bool notification { get; set; }
        public bool darkMode { get; set; }
        public bool is2FAEnabled { get; set; }
        public string _id { get; set; }
        public string owner { get; set; }
        public string companyId { get; set; }
        public int __v { get; set; }
    }
}
