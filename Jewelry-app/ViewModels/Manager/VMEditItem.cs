using Jewelry_app.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Jewelry_app.ViewModels.Manager
{
    public class VMEditItem
    {
        public VMEditItem()
        {
            Price = new Price();
            Item = new Item();
        }
        public Item Item { get; set; }
        public Price Price { get; set; }
        [Display(Name = "הוספת תמונה לפריט")]
        public IFormFile File { get; set; }
    }
}
