
namespace MichalNajwerLab2.Models
{
    public class Pizza
    {
        public Pizza(int Id, string Name, double Price, string Description, string Image)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
            this.Image = Image;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
