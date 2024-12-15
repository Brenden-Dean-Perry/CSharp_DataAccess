using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace DataAccess.Enums
{
    enum AlphaVantageTimeInterval
    {
        [DescriptionAttribute("1min")]
        OneMin,
        [DescriptionAttribute("5min")]
        FiveMin,
        [DescriptionAttribute("15min")]
        FifteenMin,
        [DescriptionAttribute("30min")]
        ThirtyMin,
        [DescriptionAttribute("60min")]
        SixtyMin,
        [DescriptionAttribute("daily")]
        Daily,
        [DescriptionAttribute("weekly")]
        Weekly,
        [DescriptionAttribute("monthly")]
        Monthly,
        [DescriptionAttribute("quartly")]
        Quarterly,
        [DescriptionAttribute("annual")]
        Annual
    }
}
