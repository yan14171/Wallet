using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Entities
{
    public class IncomeItem : EntityBase
    {
        public string Name { get; set; }
        public Account Account { get; set; }
        public int AccountId{ get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
