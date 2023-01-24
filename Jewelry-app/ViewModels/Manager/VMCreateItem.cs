using Jewelry_app.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Jewelry_app.ViewModels.Manager
{
    public class VMCreateItem
    {
        public VMCreateItem()
        {
            Price = new Price();
            Item = new Item();
            Groups = new List<Group>();
            Group = new Group();
        }
        public List<Group> Groups { get; set; }
        public Group Group { get; set; }
        public Item Item { get; set; }
        public Price Price { get; set; }
        [Display(Name = "הוספת תמונה לפריט")]
        public IFormFile File { get; set; }
        public int GroupID { get; set; }
    }
}
