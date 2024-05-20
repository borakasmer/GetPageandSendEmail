using System.Net.Mail;
using System.Net;

namespace GetPageandSendEmail.Service
{
    public class CoreService : ICoreService
    {
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

        public bool SendMailWebReport(string url)
        {
            try
            {                          
                string filePath = GetPdfFromUrl(url);
                var client = new SmtpClient("smtp client", 587)
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
                //file delete from path
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
