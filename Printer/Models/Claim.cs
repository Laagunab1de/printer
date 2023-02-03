using System;
using System.Collections.Generic;

namespace Printer.Models
{
    public partial class Claim
    {
        public int Id { get; set; }
        public int Idcabinet { get; set; }//
        public int IdcalimType { get; set; }//
        public int IdclaimDevices { get; set; }//
        public string NameOfMatherial { get; set; } = null!;
        public int AmountOfMatherial { get; set; }
        public int Cost { get; set; }

        public virtual Cabinet IdcabinetNavigation { get; set; } = null!;
        public virtual ClaimType IdcalimTypeNavigation { get; set; } = null!;
        public virtual Device IdclaimDevicesNavigation { get; set; } = null!;
    }
}
