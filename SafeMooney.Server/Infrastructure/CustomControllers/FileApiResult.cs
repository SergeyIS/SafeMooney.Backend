using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SafeMooney.Server.Infrastructure.CustomControllers
{
    public class FileApiResult : IHttpActionResult
    {
        public string FileName { get; set; }

        public Stream Content { get; set; }

        public string ContentType { get; set; }

        public long? ContentLength { get; set; }

        public string DispositionType { get; set; }

        public FileApiResult(Stream content, String filename, String contentType)
        {
            Content = content;
            FileName = filename;
            ContentType = contentType;
        }

        public FileApiResult(Stream content, String filename)
        {
            Content = content;
            FileName = filename;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(Content)
                };

                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue(DispositionType ?? System.Net.Mime.DispositionTypeNames.Attachment)
                    {
                        FileNameStar = FileName
                    };

                result.Content.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(ContentType ?? System.Net.Mime.MediaTypeNames.Image.Jpeg);

                if (ContentLength.HasValue)
                    result.Content.Headers.ContentLength = ContentLength;

                return Task.FromResult(result);
            }
            catch
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}