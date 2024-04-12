namespace Intex2024.Data
{
    public class CartSubmissionViewModel
    {
        public CustomerViewModel? Customer { get; set; }
        public OrderViewModel? Order { get; set; }
    }

    public class CustomerViewModel
    {
        public string? CountryOfResidence { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
    }

    public class OrderViewModel
    {
        public string? DayOfWeek { get; set; }
        public int? Time { get; set; }
        public string? EntryMode { get; set; }
        public decimal? Amount { get; set; }
        public string? TypeOfTransaction { get; set; }
        public string? CountryOfTransaction { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Bank { get; set; }
        public string? TypeOfCard { get; set; }
    }
}
