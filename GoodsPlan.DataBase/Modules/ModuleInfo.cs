using System;
using System.Reflection;

namespace GoodsPlan.DataBase
{
    public class ModuleInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsBundledWithHost { get; set; }

        public Assembly Assembly { get; set; }
    }
}
