using HubSpotIntegration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpotIntegration.Services
{
    public class HubSpotClientService
    {
        protected readonly string HapiKey;
        protected readonly string BaseUrl;
        public HubSpotClientService(string baseUrl, string hapiKey)
        {
            BaseUrl = baseUrl;
            HapiKey = hapiKey;
        }
        public ContactList GetContactList(int listId)
        {
            using (var client = new HttpClient())
            {
                var contactListRaw = client.GetStringAsync($"{BaseUrl}/lists/{listId}?hapikey={HapiKey}").Result;

                return JsonConvert.DeserializeObject<ContactList>(contactListRaw);
            }
        }

        public IEnumerable<ContactList> GetAllContactLists()
        {
            using (var client = new HttpClient())
            {

                var offset = 0;
                var count = 250;
                var hasMore = false;

                do
                {
                    var contactListRaw = client.GetStringAsync($"https://api.hubapi.com/contacts/v1/lists?count={count}&hapikey={HapiKey}&offset={offset}").Result;

                    ContactListResponse response = JsonConvert.DeserializeObject<ContactListResponse>(contactListRaw);

                    offset = response.offset;
                    hasMore = response.hasmore;

                    foreach (var c in response.lists)
                    {
                        yield return c;
                        //Console.WriteLine($"name: {c.name}");
                    }

                    if (hasMore)
                        Thread.Sleep(1000);

                } while (hasMore);
            }
        }
    }
}
