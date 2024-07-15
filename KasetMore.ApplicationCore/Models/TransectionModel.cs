using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasetMore.ApplicationCore.Models
{
    public class TransectionItemModel
    {
        public int ProductId { get; set; }
        public string Unit { get; set; } = null!;
        public string SellerEmail { get; set; } = null!;
        public int Amount { get; set; }
        public double Price { get; set; }
    }
    public class TransectionModel
    {
        public string BuyerEmail { get; set; } = null!;
        public List<TransectionItemModel> items { get; set; }
    }
    public class TransectionItemInsert
    {
        public int ProductId { get; set; }
        public int TransectionId { get; set; }
        public string Unit { get; set; } = null!;
        public string SellerEmail { get; set; } = null!;

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}
