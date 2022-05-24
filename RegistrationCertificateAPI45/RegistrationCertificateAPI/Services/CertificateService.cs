using Microsoft.AspNetCore.Mvc;
using RegistrationCertificateAPI.Data;
using RegistrationCertificateAPI.Models;
using RegistrationCertificateAPI.Responses;

namespace RegistrationCertificateAPI.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly CertificateContextDB context;

        public CertificateService(CertificateContextDB context)
        {
            this.context = context;
        }

        public Task<List<RegistrationCertificateModel>> GetAllCertificates(User user, string sort_by, string sort_type, string search_value)
        {
            var certificates = context.Certificates.ToList().Where(c => c?.User?.UserName == user.UserName).ToList();

            if(sort_by != null)
            {
                var property =
                    typeof(RegistrationCertificate)
                    .GetProperties()
                    .FirstOrDefault(prop => prop.Name.ToLower() == sort_by?.ToLower());

                if(property != null)
                {
                    if(sort_type?.ToLower() == "desc")
                    {
                        certificates = certificates.OrderByDescending(c => property.GetValue(c)).ToList();
                    }
                    else
                    {
                        certificates = certificates.OrderBy(c => property.GetValue(c)).ToList();
                    }
                }
            }

            if(search_value != null)
            {
                var properties = typeof(RegistrationCertificate).GetProperties();
                var searchedCertificates = new List<RegistrationCertificate>();

                foreach(RegistrationCertificate certificate in certificates)
                {
                    foreach(var property in properties)
                    {
                        if (property.GetValue(certificate).ToString().ToLower().Contains(search_value.ToLower()))
                        {
                            searchedCertificates.Add(certificate);
                            break;
                        }
                    }
                }

                return Mapper(searchedCertificates);
            }

            return Mapper(certificates);
        }

        public RegistrationCertificateModel GetCertificateById(User user, int id)
        {
            RegistrationCertificate certificate = context.Certificates.Find(id);

            if(certificate != null && certificate.User.UserName == user.UserName)
            {
                return Mapper(certificate);
            }

            return null;
        }

        public Response AddCertificate(User user, RegistrationCertificateModel model)
        {
            List<ErrorResponse> errors = CertificateValidation.Validate(
                model.RegistrationNumber, model.DateOfRegistration, model.VINCode, model.Car, model.YearOfManufacture);

            if (errors.Count > 0)
                return new Response { Errors = errors };

            RegistrationCertificate certificate = new RegistrationCertificate()
            {
                RegistrationNumber = model.RegistrationNumber,
                DateOfRegistration = model.DateOfRegistration,
                VINCode = model.VINCode,
                Car = model.Car,
                YearOfManufacture = model.YearOfManufacture,
                User = user
            };

            context.Certificates.Add(certificate);
            context.SaveChanges();

            return new Response { Certificate = Mapper(certificate) };
        }

        public Response UpdateCertificate(User user, int id, RegistrationCertificateModel model)
        {
            List<ErrorResponse> errors = CertificateValidation.Validate(
                model.RegistrationNumber, model.DateOfRegistration, model.VINCode, model.Car, model.YearOfManufacture);

            if(errors.Count > 0)
                return new Response { Errors = errors };

            RegistrationCertificate certificate = context.Certificates.Find(id);

            if (certificate is null || certificate.UserId != user.Id)
                return null;

            certificate.RegistrationNumber = model.RegistrationNumber;
            certificate.DateOfRegistration = model.DateOfRegistration;
            certificate.VINCode = model.VINCode;
            certificate.Car = model.Car;
            certificate.YearOfManufacture = model.YearOfManufacture;

            context.Certificates.Update(certificate);
            context.SaveChanges();

            return new Response { Certificate = Mapper(certificate) };
        }

        public RegistrationCertificateModel DeleteCertificate(User user, int id)
        {
            RegistrationCertificate certificate = context.Certificates.Find(id);

            if (certificate is null || certificate.UserId != user.Id)
                return null;

            context.Certificates.Remove(certificate);
            context.SaveChanges();

            return Mapper(certificate);
        }

        private RegistrationCertificateModel Mapper(RegistrationCertificate certificate)
        {
            if (certificate is null)
                return null;

            RegistrationCertificateModel model = new RegistrationCertificateModel
            {
                Id = certificate.Id,
                RegistrationNumber = certificate.RegistrationNumber,
                DateOfRegistration = certificate.DateOfRegistration,
                VINCode = certificate.VINCode,
                Car = certificate.Car,
                YearOfManufacture = certificate.YearOfManufacture
            };

            return model;
        }

        private async Task<List<RegistrationCertificateModel>> Mapper(List<RegistrationCertificate> certificates)
        {
            List<RegistrationCertificateModel> models = new List<RegistrationCertificateModel>();

            foreach (RegistrationCertificate certificate in certificates)
            {
                if (certificate != null)
                {
                    models.Add(Mapper(certificate));
                }
            }

            return models;
        }
    }
}
