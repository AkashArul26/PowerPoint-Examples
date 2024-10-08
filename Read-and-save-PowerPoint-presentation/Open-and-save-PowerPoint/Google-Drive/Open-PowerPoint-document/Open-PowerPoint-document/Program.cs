﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Open_PowerPoint_document
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserCredential credential;
            string[] Scopes = { DriveService.Scope.DriveReadonly };
            string ApplicationName = "YourAppName";
            // Step 1: Open Google Drive with credentials.
            using (var cretendialStream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(cretendialStream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Step 2: Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Step 3: Specify the file ID of the PowerPoint presentation you want to open.
            string fileId = "YOUR_FILE_ID"; // Replace with the actual file ID YOUR_FILE_ID.

            // Step 4: Download the PowerPoint presentation from Google Drive.
            var request = service.Files.Get(fileId);
            var stream = new MemoryStream();
            request.Download(stream);

            // Step 5: Save the PowerPoint presentation locally.
            using (FileStream fileStream = new FileStream("Output.pptx", FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }
            //Dispose the memory stream.
            stream.Dispose();
        }
    }
}
