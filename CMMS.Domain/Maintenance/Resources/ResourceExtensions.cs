using System.Collections.Generic;
using System.Linq;

namespace CMMS.Domain.Maintenance.Resources
{
    internal static class ResourceExtensions
    {
        internal static IEnumerable<Resource> Descendants(this Resource node)
        {
            return node.Children.Concat(node.Children.SelectMany(n => n.Descendants()));
        }
    }
}
