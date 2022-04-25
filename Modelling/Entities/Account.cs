using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Entities
{
    public class Account : EntityBase
    {
        public ICollection<IncomeItem> Incomes { get; set; }

        public decimal Fortune { get; set; }
    }
}
