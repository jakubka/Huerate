/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Huerate.BlobUploader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var storageConnectionString = ConfigurationManager.ConnectionStrings["AzureStorageConnectionString"].ConnectionString;
            var contentRoot = Path.GetFullPath(ConfigurationManager.AppSettings["ContentRootPath"]);
            var containerName = ConfigurationManager.AppSettings["ContainerName"];

            var account = CloudStorageAccount.Parse(storageConnectionString);

            var blobClient = account.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions()
                                         {
                                             PublicAccess = BlobContainerPublicAccessType.Blob,
                                         });

            string rootDirectoryName = new DirectoryInfo(contentRoot).Name;

            var sw = Stopwatch.StartNew();


            // application/javascript
            //text/css
            // image/png
            // image/gif

            Parallel.ForEach(Directory.EnumerateFiles(contentRoot, "*.*", SearchOption.AllDirectories),
                             filePath =>
                                 {
                                     string relativePath = filePath.Substring(contentRoot.Length - rootDirectoryName.Length).Replace('\\', '/');

                                     var blockBlobReference = container.GetBlockBlobReference(relativePath);

                                     using (var fileStream = File.OpenRead(filePath))
                                     {
                                         Console.WriteLine("START " + relativePath);
                                         blockBlobReference.UploadFromStream(fileStream);
                                         Console.WriteLine("END " + relativePath);
                                     }

                                     blockBlobReference.Properties.ContentType = MimeMapping.GetMimeMapping(Path.GetFileName(filePath));

                                     blockBlobReference.SetProperties();
                                 });

            Console.WriteLine(sw.Elapsed);

            Console.ReadLine();
        }
    }
}