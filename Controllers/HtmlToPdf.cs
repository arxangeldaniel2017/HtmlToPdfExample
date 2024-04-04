using HtmlRendererCore.PdfSharp;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using HiQPdf;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.Model;

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
            var htmlPath = Path.Combine(appPath, "AppData\\SalmaAlamNaylor.html");
            var pdfPath = Path.Combine(appPath, "AppData\\SalmaAlamNaylor.pdf");

            var html = System.IO.File.ReadAllText(htmlPath);

            if(System.IO.File.Exists(pdfPath))
                System.IO.File.Delete(pdfPath);

            // Syncfusion - good but expensive
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            var document = htmlConverter.Convert(htmlPath);
            FileStream fileStream = new FileStream(pdfPath, FileMode.CreateNew, FileAccess.ReadWrite);
            document.Save(fileStream);
            document.Close(true);

            return true;
        }
    }
}
