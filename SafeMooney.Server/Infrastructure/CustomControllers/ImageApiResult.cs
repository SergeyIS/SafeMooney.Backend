using SafeMooney.Shared.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SafeMooney.Server.Infrastructure.CustomControllers
{
    public class ImageApiResult : IHttpActionResult
    {
        private const String _contentType = "image/jpeg";
        private Stream _bitmap;
        private int _contentLength;

        public ImageApiResult(UserImage img)
        {
            if (img == null || img.Data == null)
                throw new ArgumentNullException("img or img.Data is NULL");

            _bitmap = new MemoryStream(img.Data);
            _contentLength = img.Data.Length;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = new HttpResponseMessage();
                result.Content = new StreamContent(_bitmap);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(_contentType);
                result.Content.Headers.ContentLength = _contentLength;
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Inline);

                return Task.FromResult(result);
            }
            catch
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}