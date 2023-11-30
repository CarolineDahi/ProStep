using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.SharedKernel.ExtensionMethods
{
    public class GlobalValue
    {
        public const int DefaultExpireTokenMinut = 2 * 24 * 60;
        public const string RouteApp = "App/[action]";
        public const string RouteDash = "Dash/[action]";
        public const string RouteBoth = "[action]";
    }
}
