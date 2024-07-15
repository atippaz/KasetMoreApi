using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasetMore.Data.Models
{
    public partial class TransectionItems
    {
        public int TransactionItemId { get; set; }
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        public string Unit { get; set; } = null!;
        public string SellerEmail { get; set; } = null!;


        public int Amount { get; set; }

        public double Price { get; set; }
    }
}
