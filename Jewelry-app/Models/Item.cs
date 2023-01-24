using System.ComponentModel.DataAnnotations;

namespace Jewelry_app.Models
{
    public class Item
    {
        public Item() 
        {
            Images =new List<Image>();
            Prices=new List<Price>();
        }
        [Key]
        public int ID { get; set; }
        [Display(Name = "שם פריט")]
        public string Name { get; set; }
        [Display(Name = "תיאור")]
        public string Description { get; set; }
        public Group Group { get; set; }
        public List<Image> Images { get; set; }
        public List<Price> Prices { get; set; }
        //פונקציה של הוספת תמונה
        public void AddImage(IFormFile file)
        {
            if (file == null) return;
            Images.Add(new Image { item=this,setImage=file});
        }
        //פונקציה של הוספת מחיר
        public Price AddPrice(decimal myPrice)
        {
            return AddPrice(new Price { MyPrice = myPrice });
        }
        public Price AddPrice(decimal myPrice,DateTime end)
        {
            return AddPrice(new Price { MyPrice = myPrice,End=end });
        }
        public Price AddPrice(decimal myPrice, DateTime start, DateTime end)
        {
            return AddPrice(new Price { MyPrice = myPrice ,Start=start,End=end});
        }
        public Price AddPrice(Price price)
        {
            Prices.Add(price);
            return price;
        }

    }
}
