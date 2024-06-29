using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;

namespace GeneratePdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        private readonly ILogger<GenerateController> _logger;
        public GenerateController(ILogger<GenerateController> logger)
        {
            _logger = logger;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GeneratePdf()
        {
            var invoiceFactory = new InvoiceFactory();

            var invoice = invoiceFactory.Create();

            var html = await RazorTemplateEngine.RenderAsync("Temps/Invoice.cshtml", invoice);

            var rendered = new ChromePdfRenderer();

            using var pdf = rendered.RenderHtmlAsPdf(html);

            pdf.SaveAs($"wwwroot/invoices/test{invoice.Number}.pdf");
             
            return File(pdf.BinaryData, "application/pdf", $"test-{invoice.Number}.pdf");
        }
    }
}
