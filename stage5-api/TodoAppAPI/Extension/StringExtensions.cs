using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppAPI.Extension
{
    public static class StringExtensions
    {
        public static byte[] SHA1Hash(this string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                return hash;
            }
        }
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        /*
        public static UserRole GetUserRole(this string roleDescription)
        {
            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();

            foreach (var role in roles)
            {
                string description = role.GetDescription();

                if (String.Equals(roleDescription, description, StringComparison.CurrentCultureIgnoreCase))
                    return role;
            }

            throw new ArgumentException("User role description does not exist!");
        }
        public static bool IsValidCharacterlength(this string value, int minCharLength, int maxCharLenth)
        {
            return (value.Length >= minCharLength && value.Length <= maxCharLenth);
        }
        */
    }
}
