using System.Collections.Generic;

namespace GoodsPlan.DataBase.Modules
{
    public interface IModuleConfigurationManager
    {
        IEnumerable<ModuleInfo> GetModules();
    }
}