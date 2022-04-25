using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Entities
{
    public  class Transaction : EntityBase
    {
        public decimal Sum { get; set; }
        
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

        public int IncomeItemId { get; set; }
        public IncomeItem IncomeItem { get; set; }
    }
}
