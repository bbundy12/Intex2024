namespace Intex2024.Data
{
    public class CartSubmissionViewModel
    {   
        public CustomerViewModel Customer { get; set; }
        public OrderViewModel Order { get; set; }
        public CartViewModel Cart { get; set; }
    }

    public class CartViewModel
    {
        public int CartId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product p, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Product.ProductId == p.ProductId)
                .FirstOrDefault(); //find the first one that matches

            //has the item already been added to the cart?
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = p,
                    Quantity = quantity
                }); 
            }
            else
            {
                line.Quantity += quantity;
            }
            
        }

        public virtual void RemoveLine(Product p) =>
            Lines.RemoveAll(x => x.Product.ProductId == p.ProductId);
        public virtual void Clear() => Lines.Clear();
        public decimal CalculateTotal() =>
            Lines.Sum(x => x.Product.Price * x.Quantity);

        public class CartLine
        {
            public int CartLineId { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }  
    }

    public class CustomerViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryOfResidence { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }        
    }

    public class OrderViewModel
    {
        public string DayOfWeek { get; set; }
        public int Time { get; set; }
        public string EntryMode { get; set; }
        public decimal Amount { get; set; }
        public string TypeOfTransaction { get; set; }
        public string CountryOfTransaction { get; set; }
        public string ShippingAddress { get; set; }
        public string Bank { get; set; }
        public string TypeOfCard { get; set; }
        
    }
}
