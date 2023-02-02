using System;
using System.IO;
using Testes.PdfCSharp.ConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using Testes.PdfCSharp.ConsoleApp.Infra.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Testes.PdfCSharp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            string pdfFilePath = $@"{basePath}\File.pdf";

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            var settings = config.GetSection("settings").Get<Settings>();
            var connectionString = config.GetConnectionString("EnviaDetran");

            var images = new SqlRepository(connectionString).GetImagesFromDB();
            var base64BD = Convert.ToBase64String(images[0]);

            if (OperatingSystem.IsWindows())
            {
                var bytes = GetByAppSettings(settings);
                using(var stream = new MemoryStream(bytes))
                using(var doc = PdfReader.Open(stream, PdfDocumentOpenMode.Import))
                {
                    doc.AddPage();
                    doc.Save(pdfFilePath);
                }

                using (var doc = new PdfDocument())
                {
                    for (var i = 0; i < images.Count; i++)
                    {
                        var jpgFilePath = $@"{basePath}\File_{i}.jpg";
                        var byteArray = images[i];
                        using Image image = Image.FromStream(new MemoryStream(byteArray));
                        image.Save(jpgFilePath, ImageFormat.Jpeg);

                        var page = doc.AddPage();
                        var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                        var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                        var font = new PdfSharp.Drawing.XFont("Arial", 14);

                        //graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile(jpgFilePath), 250, 300);

                        graphics.DrawImage(PdfSharp.Drawing.XImage.FromStream(new MemoryStream(byteArray)), 0, 0);
                    }

                    doc.Save(pdfFilePath);
                }   
            }

            //using var writer = new BinaryWriter(File.OpenWrite($@"{basePath}\File.jpg"));
            //writer.Write(byteArray);

            Console.WriteLine("Hello World!");
        }

        private static byte[] GetByAppSettings(Settings settings) => Convert.FromBase64String(settings.ImageBytes);
    }
}
