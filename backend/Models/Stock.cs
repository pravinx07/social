using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models{
    public class Stock{
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Change { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChangePercent { get; set; }

        public decimal LastDividend { get; set; }
        public string Industry { get; set; }
        public string MarketCap { get; set; }

        public List<Comment> Comments { get; set;} = new List<Comment>();
    }
}