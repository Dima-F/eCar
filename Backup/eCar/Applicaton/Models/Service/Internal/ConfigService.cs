using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class ConfigService:IConfigService
    {
        private readonly MyConfig _myConfig;
        public ConfigService()
        {
            _myConfig = (MyConfig) WebConfigurationManager.OpenWebConfiguration("/").GetSection("myConfig");
        }
        public MyConfig Current
        {
            get { return _myConfig; }
        }
    }
}