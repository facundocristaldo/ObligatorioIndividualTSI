using System;
using System.Runtime.Serialization;

namespace BusinessLogicLayer
{
    [Serializable]
    internal class EmployeeIdNotFoundException : Exception
    {
        private int idEmployee;

        public EmployeeIdNotFoundException()
        {
        }

        public EmployeeIdNotFoundException(int idEmployee)
        {
            this.idEmployee = idEmployee;
        }

        public EmployeeIdNotFoundException(string message) : base(message)
        {
        }

        public EmployeeIdNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmployeeIdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}