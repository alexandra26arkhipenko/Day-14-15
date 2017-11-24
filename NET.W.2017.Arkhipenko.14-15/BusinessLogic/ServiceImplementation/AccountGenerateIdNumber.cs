using System;
using BusinessLogic.Interfaces.Interfaces;

namespace BusinessLogic.ServiceImplementation
{
    public class AccountGenerateIdNumber: IAccountGenerateIdNumber
    {
        /// <summary>
        /// Generate unique Id number
        /// </summary>
        /// <returns>unique Id number</returns>
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
