namespace Application.Models
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public decimal Rates { get; set; }
        public DateTime Date { get; set; }
    }
}
