using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Regionals
{
    public static class Utilities
    {
        public static bool IsValid(this string phoneNumber, string region)
        {
            switch (region)
            {
                case Regions.IL:
                    {
                        if (!phoneNumber.StartsWith("05"))
                        {
                            return false;
                        }
                        return true;
                    }

                default:
                    break;
            }

            throw new Exception("PhoneNumber.IsValid failed due to missing Region.");
        }

        public static string NormalizePhoneText(this string phoneText)
        {
            return phoneText.Replace("-", "");
        }
    }

    public static class Regions
    {
        public const string IL = "IL";
        public const string US = "US";
    }
}
