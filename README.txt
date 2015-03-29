Huerate - Mobile System for Feedback Collection
Author: Jakub Kadlubiec, xkadlu00

NuGet packages are not included in the archive! It is needed to allow automatic download of the packages before build.

How to test (no cloud involved):
- Set project Huerate.WebUI as a starting project
web.config:
- Set key ContentFilesOnBlob to false, so it will not try to look for media files on the blob storage
- Set HuerateContext connection string to the valid database. It will create schema automatically.


How to deploy to cloud:
- Set the same options as above, but now in the ServiceConfiguration.Cloud.cscfg in the Huerate.AzureWebUI project
- Publish project to the Azure cloud service