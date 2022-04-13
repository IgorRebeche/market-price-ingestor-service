using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IConfigurationRepository
    {
        public Task<AppConfiguration> GetAppConfiguration();
        public Task InsertConfiguration(Configuration configuration);
    }
}
