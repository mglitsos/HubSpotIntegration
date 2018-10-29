using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubSpotIntegration.Models
{
    public class ContactList
    {
        public int parentId { get; set; }
        public bool dynamic { get; set; }
        public MetaData metaData { get; set; }
        public string name { get; set; }
        public int portalId { get; set; }
        public long createdAt { get; set; }
        public int listId { get; set; }
        public long updatedAt { get; set; }
        public bool deleteable { get; set; }
    }
}
