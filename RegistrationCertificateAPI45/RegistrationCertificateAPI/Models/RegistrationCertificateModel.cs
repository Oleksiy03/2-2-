namespace RegistrationCertificateAPI.Models
{
    public class RegistrationCertificateModel
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string VINCode { get; set; }
        public string Car { get; set; }
        public int YearOfManufacture { get; set; }
    }
}
