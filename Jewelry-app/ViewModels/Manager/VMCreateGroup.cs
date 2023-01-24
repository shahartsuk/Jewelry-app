using Jewelry_app.Models;
using System.ComponentModel.DataAnnotations;

namespace Jewelry_app.ViewModels.Manager
{
    public class VMCreateGroup
    {
        public VMCreateGroup() {
            Group = new Group();
            Groups = new List<Group>();
            Item=new Item();
            Price= new Price();
        }
        public List<Group> Groups { get; set; }
        public Group Parent { get; set; }
        public int ParentID { get; set; }
        public Group Group { get; set; }

        [Display(Name = "הוספת תמונה לקבוצה")]
        public IFormFile FileGroup { get; set; }
        [Display(Name = "הוספת תמונה לפריט")]
        public IFormFile FileItem { get; set; }
        public Item Item { get; set; }
        public Price Price { get; set; }
    }
}
