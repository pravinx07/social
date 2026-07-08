namespace backend.Helpers
{
    public class QueryObject
    {
        // for filtering/searching
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        // for pagination 

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20; // default to 20 per page 

    }
}