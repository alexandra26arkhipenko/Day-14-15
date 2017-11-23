using System;
using BusinessLogic.Interfaces.Interfaces;

namespace BusinessLogic.ServiceImplementation
{
    class AccountGenerateIdNumber: IAccountGenerateIdNumber
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
