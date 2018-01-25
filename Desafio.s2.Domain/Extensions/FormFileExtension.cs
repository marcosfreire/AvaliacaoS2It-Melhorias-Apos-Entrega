using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Desafio.s2.Domain.Extensions
{
    public static class FormFileExtension
    {
        private static List<string> _formatosValidos = new List<string> { "png", "jpg" };

        public static string Base64Image(this IFormFile formFile)
        {
            return ConverterImagemPagaBase64(formFile);
        }

        private static string ConverterImagemPagaBase64(IFormFile file)
        {
            if (ImagemVazia(file)) return null;

            if (ImagemFormatoInvalido(file)) throw new FormatException("Imagem em formato inválido");

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

        private static bool ImagemFormatoInvalido(IFormFile file)
        {
            return !_formatosValidos.Contains(Path.GetExtension(file.FileName));
        }

        private static bool ImagemVazia(IFormFile file)
        {
            return file == null || file.Length == 0;
        }
    }
}