using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WasmUI.Server.Services;

namespace WasmUI.Server.Controllers
{
    public partial class AvaController : Controller
    {
        [HttpPost("uploadava/{AppUserId}")]
        public async Task<string> Ava(IFormFile file, string AppUserId)
        {
            try
            {
                if (file != null)
                {
                    var ext = Path.GetExtension(file.FileName);
                    var blobName = Guid.NewGuid() + ext;
                    var connectionString =
                        Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
                    string container = "profiles";


                    var client = new BlobClient(connectionString, container, blobName);
                    var headers = new BlobHttpHeaders
                    {
                        ContentType = file.ContentType
                    };

                    var options = new BlobUploadOptions
                    {
                        HttpHeaders = headers
                    };

                    var res = await client.UploadAsync(file.OpenReadStream(), options, cancellationToken: default);

                    var ur = client.Uri.AbsoluteUri;

                    await UpdateService.UpdateProfileAsync(AppUserId, ur);

                    return ur;

                }
                // Put your code here
                return "";
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return "";
            }
        }
    }
}
