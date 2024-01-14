﻿using HtmlRendererCore.PdfSharp;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using HiQPdf;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace HtmlToPdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlToPdf : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private IHostingEnvironment _env;

        public HtmlToPdf(ILogger<WeatherForecastController> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        [HttpGet(Name = "HtmlToPdf")]
        public bool Get()
        {
            var appPath = _env.ContentRootPath;
            var htmlPath = Path.Combine(appPath, "AppData\\HtmlToPdf\\DesignDevelopmentGloutir.html");
            var pdfPath = Path.Combine(appPath, "AppData\\HtmlToPdf\\DesignDevelopmentGloutir3.pdf");

            var html = System.IO.File.ReadAllText(htmlPath);

            // PdfSharp - bad
            //var pdf = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);
            //pdf.Save(pdfPath);

            // Syncfusion - good but expensive
            //HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            //PdfDocument document = htmlConverter.Convert(htmlPath);
            //FileStream fileStream = new FileStream(pdfPath, FileMode.CreateNew, FileAccess.ReadWrite);
            //document.Save(fileStream);
            //document.Close(true);


            var converter = new HiQPdf.HtmlToPdf();
            var basePath = "";
            converter.ConvertHtmlToFile(html, basePath, pdfPath);
            
            return true;
        }
    }
}