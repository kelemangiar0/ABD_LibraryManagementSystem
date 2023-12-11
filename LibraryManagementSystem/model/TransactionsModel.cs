using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    // TransactionsModel.cs
    public class TransactionsModel
    {
        public int TransactionID { get; set; }
        public int BookID { get; set; }
        public int UserID_from { get; set; }
        public int UserID_to { get; set; }
        public DateTime? Date_transaction { get; set; }
        public DateTime? Date_penalty { get; set; }
    }

}
