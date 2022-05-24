using RegistrationCertificateAPI.Responses;
using System.Text.RegularExpressions;

namespace RegistrationCertificateAPI
{
    public static class CertificateValidation
    {
        private static string _patternRegistrationNumber = @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$";
        private static string _patternVinCode = @"^(?=.*\d|=.*[A-Z])(?=.*[A-Z])[A-Z0-9]{17}$";

        public static List<ErrorResponse>
            Validate(string registrationNumber, DateTime dateOfRegistration, string vinCode, string car, int yearOfManufacture)
        {
            List<ErrorResponse> errors = new List<ErrorResponse>();

            if (!Regex.IsMatch(registrationNumber, _patternRegistrationNumber))
            {
                errors.Add(new ErrorResponse(
                    "RegistrationNumber", "Bad format registration number. Must be AA0000AA"));
            }

            if(dateOfRegistration.Year < 1950 || dateOfRegistration >= DateTime.Now)
            {
                errors.Add(new ErrorResponse(
                    "DateOfRegistration", $"Date of registration must be between 01/011950 and {DateTime.Now.ToString("MM/dd/yyyy")}"));
            }

            if(!Regex.IsMatch(vinCode, _patternVinCode))
            {
                errors.Add(new ErrorResponse(
                    "VinCode", "Bad format VIN code. Must be 17 characters long and contain only letters and numbers"));
            }

            if (Char.IsLower(car[0]))
            {
                errors.Add(new ErrorResponse(
                    "Car", "The name of the machine must start with a capital letter"));
            }

            if(yearOfManufacture < 1950 || yearOfManufacture > DateTime.Now.Year)
            {
                errors.Add(new ErrorResponse(
                    "YearOfManufacture", $"Year of manufacture must be between 1950 and {DateTime.Now.Year}"));
            }

            return errors;
        }
    }
}
