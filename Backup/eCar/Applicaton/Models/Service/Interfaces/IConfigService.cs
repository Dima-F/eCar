using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IConfigService
    {
        MyConfig Current { get; }
    }
}
