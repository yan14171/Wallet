using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Entities
{
    public class TransactionType : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
