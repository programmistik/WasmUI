using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WasmUI.Shared;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Threading;
using System.Text;
using System.Net.Http;
using IdentityModel.Client;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using WasmUI.Server.Services;
using System.Text.RegularExpressions;
using ImageMagick;
using Serilog;

namespace WasmUI.Server.Controllers
{
    public partial class UploadController : Controller
    {
        
        [HttpPost("upload/{id}")]
        public async Task<string> Post(IFormFile file, string id)
        {
            try
            {
                if (file != null)
                {
                     var ext = Path.GetExtension(file.FileName);
                    var blobName = Guid.NewGuid() + ext;
                    var connectionString =// "DefaultEndpointsProtocol=https;AccountName=programmistik83;AccountKey=01u8KvcjtTdd4+IJdslghzF38Gqd8x8VU7DuA85zazffuDmaQCobdA42tocy2X7dRd/m+rYt4nwOcJ4OFFSO7Q==;EndpointSuffix=core.windows.net";
                        Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
                    string container = "posts";


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

                    await UpdateService.UpdatePostAsync(id, ur);

                    // resize
                    using (MagickImage image = new MagickImage(ur))
                    {
                        // MagickGeometry size = new MagickGeometry(100, 100);
                        // This will resize the image to a fixed size without maintaining the aspect ratio.
                        // Normally an image will be resized to fit inside the specified size.
                        var size = new Percentage(50);

                        image.Resize(size);

                        // Save the result
                        using (var output = new MemoryStream())
                        {
                            await image.WriteAsync(output);
                            output.Position = 0;
                            string container2 = "resizedposts";
                            var client2 = new BlobClient(connectionString, container2, blobName);
                            var res2 = await client2.UploadAsync(output, options, cancellationToken: default);
                        }
                    }


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
