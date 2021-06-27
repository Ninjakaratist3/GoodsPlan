using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace GoodsPlan.Infrastructure.Web
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var moduleViewLocations = new string[]
                {
                    $"/Areas/Views/{{1}}/{{0}}.cshtml"
                };

            viewLocations = moduleViewLocations.Concat(viewLocations);

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context) { }
    }
}
