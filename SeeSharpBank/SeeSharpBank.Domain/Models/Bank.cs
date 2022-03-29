using System.Collections.Generic;

namespace SeeSharpBank.Domain
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}