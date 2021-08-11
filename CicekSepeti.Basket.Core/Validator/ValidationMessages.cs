using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Basket.Core.Validator
{
    public class ValidationMessages
    {
        public static string NotEmpty(string fieldName)
        {
            return $"{fieldName} boş bırakılamaz";
        }

        public static string NotValid(string fieldName)
        {
            return $"{fieldName} değeri yanlış yada hatalıdır";
        }

        public static string MustBeGreater(string fieldName, int num)
        {
            return $"{fieldName}, {num} değerinden büyük olmalıdır";
        }
    }
}
