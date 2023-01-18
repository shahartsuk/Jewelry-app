using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Jewelry_app.Models
{
    public class Group
    {
        public Group()
        {
            SubGroups = new List<Group>();
            Items = new List<Item>();
        }
        [Key]
        public int ID { get; set; }
        [Required, Display(Name = "שם קבוצה")]
        public string Name { get; set; }
        [Required, Display(Name = "תיאור")]
        public string Description { get; set; }
        [Display(Name = "תמונה")]
        public byte[] Image { get; set; }
        public Group Parent { get; set; }
        public List<Group> SubGroups { get; set; }
        public List<Item> Items { get; set; }
        public IFormFile SetImage { set {
                if (value == null) return;
                //יצירת מקום בזכרון המכיל קובץ
                MemoryStream stream = new MemoryStream();
                //העתקת הקובץ מהמשתמש למקום שנוצר זכרון
                value.CopyTo(stream);
                //הופך את הזיכרון של הסטרים למערך של ביטים
                Image=stream.ToArray();
            } }

        // פונקציה של הוספת פריט
        public void AddItem(string name,string description,decimal price)
        {
            Item item = new Item { Name = name,Description=description };
            item.AddPrice(price);
            AddItem(item);
        }
        public void AddItem(string name, string description, decimal price, IFormFile image)
        {
            Item item = new Item { Name = name, Description = description};
            item.AddPrice(price);
            item.AddImage(image);
            AddItem(item);
        }
        public void AddItem(string name, string description, decimal price, List<IFormFile> images)
        {
            Item item = new Item { Name = name, Description = description };
            item.AddPrice(price);
            foreach(IFormFile image in images)
            {
                item.AddImage(image);
            }
            AddItem(item);
        }
        public void AddItem(string name, string description, decimal price, List<IFormFile> images,DateTime start,DateTime end)
        {
            Item item = new Item { Name = name, Description = description };
            item.AddPrice(price, start, end);
            foreach (IFormFile image in images)
            {
                item.AddImage(image);
            }
            AddItem(item);
        }
        public void AddItem(string name, string description, decimal price, IFormFile image, DateTime start, DateTime end)
        {
            Item item = new Item { Name = name, Description = description };
            item.AddPrice(price, start, end);
            item.AddImage(image);
            AddItem(item);
        }
        public void AddItem(Item item)
        {
           Items.Add(item);
        }
        // פונקציה של הוספת תת קבוצה
        public void AddSubGroup(string name)
        {
            Group group = new Group
            {
                Name = name,
                Parent = this
            };
            AddSubGroup(group);
        }
        public void AddSubGroup(string name,string description)
        {
            Group group = new Group
            {
                Name = name,
                Description = description,
                Parent = this
            };
            AddSubGroup(group);
        }
        public void AddSubGroup(string name, string description,IFormFile image)
        {
            Group group = new Group { Name = name,Description = description,SetImage = image,
            Parent = this};
            AddSubGroup(group);

        }
        public void AddSubGroup(Group subGroup)
        {
            SubGroups.Add(subGroup);
        }

    }
}


