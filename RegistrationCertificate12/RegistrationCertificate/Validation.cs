using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegistrationCertificate
{
    public static class Validation
    {
        private static string _patternRegistrationNumber = @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$";
        private static string _patternVinCode = @"^(?=.*\d|=.*[A-Z])(?=.*[A-Z])[A-Z0-9]{17}$";

        public static int IdIsValid(int id)
        {
            if (id < 0)
                throw new BadModelException("Id can not be negative");
            return id;
        }

        public static string RegistrationNumberIsValid(string registrationNumber)
        {
            if (!Regex.IsMatch(registrationNumber, _patternRegistrationNumber))
                throw new BadModelException("Bad format registration number. Must be AA0000AA");
            return registrationNumber;
        }

        public static DateTime DateOfRegistrationIsValid(DateTime dateOfRegistration)
        {
            if (dateOfRegistration.Year < 1950 || dateOfRegistration >= DateTime.Now)
                throw new BadModelException($"Date of registration must be between 01/01/1950 and {DateTime.Now.ToString("MM/dd/yyyy")}");
            return dateOfRegistration;
        }

        public static string VinCodeIsValid(string vinCode)
        {
            if (!Regex.IsMatch(vinCode, _patternVinCode))
                throw new BadModelException("Bad format VIN code. Must be 17 characters long and contain only letters and numbers");
            return vinCode;
        }

        public static string CarIsValid(string car)
        {
            if (Char.IsLower(car[0]))
                throw new BadModelException("The car make must start with a capital letter");
            return car;
        }

        public static int YearOfManufactureIsValid(int yearOfManufacture)
        {
            if (yearOfManufacture < 1950 || yearOfManufacture > DateTime.Now.Year)
                throw new BadModelException($"Year of manufacture must be between 1950 and {DateTime.Now.Year}");
            return yearOfManufacture;
        }
    }
}
