using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Desafio.s2.Domain.Extensions
{
    public static class IFormFileExtension
    {
        public static string Base64Image(this IFormFile formFile)
        {
            return ConverterImagemPagaBase64(formFile);
        }

        private static string ConverterImagemPagaBase64(IFormFile file)
        {
            if (ImagemVazia(file)) return null;

            var base64Image = "";
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                base64Image = Convert.ToBase64String(fileBytes);
            }

            const string base64ImagePrefix = "data:image/png;base64,";

            return base64ImagePrefix + base64Image;
        }

        private static bool ImagemVazia(IFormFile file)
        {
            return file == null || file.Length == 0;
        }
    }
}