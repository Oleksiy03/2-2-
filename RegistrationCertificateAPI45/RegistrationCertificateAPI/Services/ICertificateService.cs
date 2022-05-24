using RegistrationCertificateAPI.Data;
using RegistrationCertificateAPI.Models;
using RegistrationCertificateAPI.Responses;

namespace RegistrationCertificateAPI.Services
{
    public interface ICertificateService
    {
        public Task<List<RegistrationCertificateModel>> GetAllCertificates(User user, string sort_by, string sort_type, string search_value);

        public RegistrationCertificateModel GetCertificateById(User user, int id);

        public Response AddCertificate(User user, RegistrationCertificateModel certificate);

        public Response UpdateCertificate(User user, int id, RegistrationCertificateModel certificate);

        public RegistrationCertificateModel DeleteCertificate(User user, int id);
    }
}
