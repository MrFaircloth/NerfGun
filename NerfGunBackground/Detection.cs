using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerfGunBackground
{
    class Detection
    {
        public String TargetDetected { get; set; }
        public DateTime TimeDetected { get; set; }
        public String SystemResponse { get; set; }

        public Detection()
        {
            this.TimeDetected = DateTime.Now;
        }

        public Detection(String SystemResponse)
        {
            this.SystemResponse = SystemResponse;
            this.TimeDetected = DateTime.Now;
        }

        public Detection(String TargetDetected, String SystemResponse)
        {
            this.TargetDetected = TargetDetected;
            this.TimeDetected = DateTime.Now;
            this.SystemResponse = SystemResponse;
        }


    }
}
