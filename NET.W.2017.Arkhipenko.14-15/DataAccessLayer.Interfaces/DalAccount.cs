using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public class DalAccount
    {        
        public Type AccountType { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
        public int Points { get; set; }
        public int BonusValue { get; set; }
    }
}
