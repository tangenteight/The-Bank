using System.Collections.Generic;

namespace SeeSharpBank.Domain
{
    // Quick Interface to abstract getting banks for this exercise
    public interface IBankService
    {
        
        Bank GetBankById(int id);
    }
}
