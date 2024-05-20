using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace GetPageandSendEmail.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {

        public ReportController()
        {

        }

        [HttpGet(Name = "GetReport")]
        public bool Get()
        {
            /*try
            {
                //string filePath = GetPdfFromUrl("https://www.borakasmer.com/hakkimda/");                
                string filePath = GetPdfFromUrl("https://www.borakasmer.com/hakkimda/");
                var client = new SmtpClient("smtpclient", 587)
                {
                    Credentials = new NetworkCredential("username", "password"),
                };
                //add pdf attachment filepath to client
                using (MailMessage mail = new MailMessage())
                {
                    mail.Attachments.Add(new Attachment(filePath));
                    mail.Subject = "About Bora Kamser ®borakasmer.com";
                    mail.Body = "Who is Bora Kasmer ? And what is he doing ? ®borakasmer.com";
                    //mail.To.Add("bora.kasmer78@gmail.com, hrndlpyrz@gmail.com");
                    mail.To.Add("bora.kasmer78@gmail.com");
                    mail.From = new MailAddress("bora@borakasmer.com");
                    client.Send(mail);
                }
                //client.Send("bora@borakasmer.com", "bora.kasmer78@gmail.com", "test", "testbody");
                //file delete from path
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return false;
            }*/
            return true;
        }
       
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet(Name = "GetPdfFromUrl")]
        public string GetPdfFromUrl(string url)
        {
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            converter.Options.MaxPageLoadTime = 120;
            converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;

            converter.Options.JavaScriptEnabled = true;
            converter.Options.LoginOptions.DelayAfter = 5000; // 5 seconds wait for page loading..

            SelectPdf.PdfDocument doc = converter.ConvertUrl(url);            
            string guid = Guid.NewGuid().ToString();
            doc.Save($"C:\\TestPdf\\{guid}.pdf");
            doc.Close();
            return $"C:\\TestPdf\\{guid}.pdf";
        }        
    }
}
