using System.Collections.Generic;

namespace GoodsPlan.Infrastructure.Modules
{
    public interface IModuleConfigurationManager
    {
        IEnumerable<ModuleInfo> GetModules();
    }
}