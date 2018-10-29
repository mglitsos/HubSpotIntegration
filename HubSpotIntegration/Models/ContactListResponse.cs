using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpotIntegration.Models
{
    public class ContactListResponse
    {
        public IEnumerable<ContactList> lists { get; set; }
        public int offset { get; set; }
        [JsonProperty(PropertyName = "has-more")]
        public bool hasmore { get; set; }
    }
}
