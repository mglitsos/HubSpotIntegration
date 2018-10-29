using CsvHelper;
using HubSpotIntegration.Models;
using HubSpotIntegration.Services;
using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HubSpotIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUrl = ConfigurationManager.AppSettings["baseUrl"];  //"https://api.hubapi.com/contacts/v1";
            var baseFilePath = ConfigurationManager.AppSettings["baseFilePath"];
            var ftpHost = ConfigurationManager.AppSettings["ftpHost"];
            var ftpPort = int.Parse(ConfigurationManager.AppSettings["ftpPort"]);
            var ftpUserName = ConfigurationManager.AppSettings["ftpUserName"];
            var ftpPassword = ConfigurationManager.AppSettings["ftpPassword"];

            Console.WriteLine($"Initializing...{Environment.NewLine}");
            Console.WriteLine($"baseUrl: {baseUrl}");
            Console.WriteLine($"baseFilePath: {baseUrl}");
            Console.WriteLine($"ftpHost: {baseUrl}");
            Console.WriteLine($"ftpPort: {baseUrl}");
            Console.WriteLine($"ftpUserName: {baseUrl}");
            Console.WriteLine($"ftpPassword: {ftpPassword}");
            Console.WriteLine($"{Environment.NewLine}");

            Console.Write($"hAPIKey: ");
            var hapiKey = Console.ReadLine();

            Console.Write($"listId: ");
            int? listId = int.Parse(Console.ReadLine());

            Console.WriteLine($"{Environment.NewLine}");
            Console.WriteLine($"Running...");
            Console.WriteLine($"{Environment.NewLine}");

            var hsService = new HubSpotClientService(baseUrl, hapiKey);

            string filePath = null;

            if (listId != null)
            {
                var guid = Guid.NewGuid();
                filePath = Environment.ExpandEnvironmentVariables($"{baseFilePath}contactList_{listId}_{guid}.csv");
                using (var textWriter = File.CreateText(filePath))
                {
                    
                    using (var csv = new CsvWriter(textWriter))
                    {
                        using(var console = new CsvWriter(Console.Out))
                        {
                            var contactList = hsService.GetContactList((int)listId);
                            csv.WriteHeader<ContactList>();
                            console.WriteHeader<ContactList>();
                            csv.WriteRecord(contactList);
                            console.WriteRecord(contactList);
                        }
                    }
                }
            }
            else
            {
                var guid = Guid.NewGuid();
                filePath = Environment.ExpandEnvironmentVariables($"{baseFilePath}contactLists_all_{guid}.csv");
                using (var textWriter = File.CreateText(filePath))
                {
                    using (var csv = new CsvWriter(textWriter))
                    {
                        using (var console = new CsvWriter(Console.Out))
                        {
                            var contactLists = hsService.GetAllContactLists();
                            csv.WriteHeader<ContactList>();
                            console.WriteHeader<ContactList>();
                            foreach (var contactList in contactLists)
                            {
                                csv.WriteRecord(contactList);
                                console.WriteRecord(contactList);
                            }
                        }
                    }
                }
            }

            //try
            //{
            //    using (var ftpClient = new SftpClient(ftpHost, ftpPort, ftpUserName, ftpPassword))
            //    {
            //        using (FileStream fs = new FileStream(filePath, FileMode.Open))
            //        {
            //            ftpClient.UploadFile(fs, Path.GetFileName(filePath));
            //        }
            //    }
            //} catch(Exception e)
            //{
            //    Console.WriteLine($"An error occurred with attempting to transport file to {ftpHost};");
            //    Console.WriteLine($"Exception: {e}");
            //}

            Console.WriteLine($"{Environment.NewLine}");
            Console.WriteLine("DONE...");
            Console.ReadLine();
        }
    }
}
