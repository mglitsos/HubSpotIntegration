using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpotIntegration.Models
{
    public class MetaData
    {
        public string processing { get; set; }
        public int size { get; set; }
        public string error { get; set; }
        public long lastProcessingStateChangeAt { get; set; }
        public long lastSizeChangeAt { get; set; }
    }
}
