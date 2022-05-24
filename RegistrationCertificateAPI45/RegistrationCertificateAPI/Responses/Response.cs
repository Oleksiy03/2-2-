using RegistrationCertificateAPI.Models;

namespace RegistrationCertificateAPI.Responses
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public RegistrationCertificateModel? Certificate { get; set; }
        public List<ErrorResponse>? Errors { get; set; }
    }
}
