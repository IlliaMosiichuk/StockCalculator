using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToPercents(this decimal val)
        {
            return val.ToString("P", CultureInfo.InvariantCulture);
        }
    }
}
