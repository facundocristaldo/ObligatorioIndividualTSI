using System;
using System.Runtime.Serialization;

namespace BusinessLogicLayer
{
    [Serializable]
    internal class EmployeeNotPartTimeException : Exception
    {
        private int idEmployee;

        public EmployeeNotPartTimeException()
        {
        }

        public EmployeeNotPartTimeException(int idEmployee)
        {
            this.idEmployee = idEmployee;
        }

        public EmployeeNotPartTimeException(string message) : base(message)
        {
        }

        public EmployeeNotPartTimeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmployeeNotPartTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}