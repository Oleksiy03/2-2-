using System;
using System.Collections.Generic;
using System.Text;

namespace RegistrationCertificate
{
    public class Certificate : BaseClass
    {
        private string _registrationNumber;
        private DateTime _dateOfRegistration;
        private string _vinCode;
        private string _car;
        private int _yearOfManufacture;

        public string RegistrationNumber { get => _registrationNumber; set => _registrationNumber = Validation.RegistrationNumberIsValid(value); }
        public DateTime DateOfRegistration { get => _dateOfRegistration; set => _dateOfRegistration = Validation.DateOfRegistrationIsValid(value); }
        public string VINCode { get => _vinCode; set => _vinCode = Validation.VinCodeIsValid(value); }
        public string Car { get => _car; set => _car = Validation.CarIsValid(value); }
        public int YearOfManufacture { get => _yearOfManufacture; set => _yearOfManufacture = Validation.YearOfManufactureIsValid(value); }

        public override string ToString()
        {
            var properties = typeof(Certificate).GetProperties();

            string certificate = String.Empty;

            foreach (var property in properties)
                certificate += $"{property.Name}: {property.GetValue(this)}\n";

            return certificate;
        }
    }
}
