namespace RegistrationCertificateAPI.Data
{
    public class RegistrationCertificate
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string VINCode { get; set; }
        public string Car { get; set; }
        public int YearOfManufacture { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
