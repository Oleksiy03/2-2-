using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RegistrationCertificate
{
    [Serializable]
    public class BadModelException : Exception
    {
        public BadModelException() { }

        public BadModelException(string message)
            : base(message) { }

        public BadModelException(string message, Exception innerException)
            : base(message, innerException) { }

        protected BadModelException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
