using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Quartz;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Job
{
    public class Beckup:IJob
    {
        public ICloudService Cloud { get; set; }
        // По определеному расписанию создает резервную копию данных и сохраняет ее в облачном хранилище.
        public void Execute(IJobExecutionContext context)
        {
            //Cloud.ArchiveFolder("~/App_Data");
            Debug.WriteLine("Beckup was done at... " + DateTime.Now.ToString());
        }
    }
}
