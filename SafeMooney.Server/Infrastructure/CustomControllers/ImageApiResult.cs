using System;
using System.IO;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SafeMooney.Shared.Models;

namespace SafeMooney.Server.Infrastructure.CustomControllers
{
    public class ImageApiResult : IHttpActionResult
    {
        private Stream _bitmap;
        private int _contentLength;
        //REVIEW:Кандидат на константу :)
        private static String _contentType = "image/jpeg";

        public ImageApiResult(UserImage img)
        {
            if (img == null)
                throw new ArgumentNullException("img is NULL");

            _bitmap = new MemoryStream(img.Data);
            _contentLength = img.Data.Length;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage();
            result.Content = new StreamContent(_bitmap);
            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(_contentType);

            result.Content.Headers.ContentLength = _contentLength;
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue(System.Net.Mime.DispositionTypeNames.Inline);

            //REVIEW: Проверить бы на исключения
            return Task.FromResult<HttpResponseMessage>(result);
        }
    }
}