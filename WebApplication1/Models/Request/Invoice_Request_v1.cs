namespace WebApplication1.Models.Request
{
    public class Invoice_Request_v1
    {
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
    }
}
