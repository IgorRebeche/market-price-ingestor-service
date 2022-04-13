using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppConfiguration
    {
        public Configuration[]? configurations;

        public Configuration? GetConfiguration(string name)
        {
            return configurations is not null ? configurations.Where(x => x.Name == name).First() : null;
        }

    }
}
