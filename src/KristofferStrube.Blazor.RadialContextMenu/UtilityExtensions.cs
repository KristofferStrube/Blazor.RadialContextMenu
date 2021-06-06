using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KristofferStrube.Blazor.RadialContextMenu
{
    internal static class UtilityExtensions
    {
        public static string AsString(this double d) => d.ToString(CultureInfo.InvariantCulture);
    }
}
