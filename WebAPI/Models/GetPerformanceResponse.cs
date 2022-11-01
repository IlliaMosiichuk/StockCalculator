namespace WebAPI.Models
{
    public class GetPerformanceResponse
    {
        public IEnumerable<GetPerformanceResponseItem> Items { get; set; }
    }

    public class GetPerformanceResponseItem
    {
        public string Date { get; set; }

        public string SymbolPerformance { get; set; }

        public string SpyPerformance { get; set; }
    }
}
