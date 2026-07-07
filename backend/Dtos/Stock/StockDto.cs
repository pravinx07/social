namespace backend.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }

        public decimal LastDividend { get; set; }

        // without ? This string is NEVER allowed to be null.
        // with ? it means this string can be null
        public string? Industry { get; set; }
        public string? MarketCap { get; set; }

    }
}

/*
C# me data type 2 cAategories me divide hote hai 
1.Value Types(int decimal, bool , DateTime);
ye wo data type hai jo kbhi null nahi ho skte (unless aap ? lagao)
aagr user ne json me price ki value hi nahi bheji to wo default values de deta hai jaisw ont decimal 0 ho jata hai 
and bool => false ho jata hai 

2.Reference Types(string, classes)
Ye wo data type hai jo null ho skte hai 
aagr user ne json me industry ki value hi nahi bheji to wo null ho jata hai 
*/