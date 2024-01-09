using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Model
{
    public class TransactionsModel
    {
        public int TransactionID { get; set; }
        public int BookID { get; set; }
        public int UserID_from { get; set; }
        public int UserID_to { get; set; }
        public DateTime? Date_transaction { get; set; }
        public DateTime? Date_penalty { get; set; }
        public string Bookname { get; set; }
        public string Username_from {  get; set; }
        public string Username_to { get; set; }
    }

}
