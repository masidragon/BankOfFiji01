using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfFiji01.Models
{
    public interface ITransferRepository
    {
        IList<BankAccount> MyAccounts();

        IList<BankAccount> GetOtherAccounts(int SelectedAccount);
    }
}
