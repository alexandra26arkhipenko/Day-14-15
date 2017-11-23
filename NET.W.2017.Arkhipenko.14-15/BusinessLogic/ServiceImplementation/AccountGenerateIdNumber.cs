using System;
using BusinessLogic.Interfaces.Interfaces;

namespace BusinessLogic.ServiceImplementation
{
    public class AccountGenerateIdNumber: IAccountGenerateIdNumber
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
