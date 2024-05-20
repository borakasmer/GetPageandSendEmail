namespace GetPageandSendEmail.Service
{
    public interface ICoreService
    {
        public string GetPdfFromUrl(string url);
        public bool SendMailWebReport(string url);
    }
}
