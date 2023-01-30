using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "תיאור")]
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
        public Item AddItem(string name,string description,decimal price)
        {
            Item item = new Item { Name = name,Description=description,Group=this };
            item.AddPrice(price);
            return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price, DateTime start, DateTime end)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price, start, end);
           return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price, IFormFile image)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price);
            item.AddImage(image);
            return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price, List<IFormFile> images)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price);
            foreach(IFormFile image in images)
            {
                item.AddImage(image);
            }
            return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price, List<IFormFile> images,DateTime start,DateTime end)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price, start, end);
            foreach (IFormFile image in images)
            {
                item.AddImage(image);
            }
            return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price, IFormFile image, DateTime start, DateTime end)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price, start, end);
            item.AddImage(image);
            return AddItem(item);
        }
        public Item AddItem(Item item)
        {
            item.Group= this;
           Items.Add(item);
            return item;
        }
        // פונקציה של הוספת תת קבוצה
        public Group AddSubGroup(string name)
        {
            Group group = new Group
            {
                Name = name,
                Parent = this
            };
            return AddSubGroup(group);
        }
        public Group AddSubGroup(string name,string description)
        {
            Group group = new Group
            {
                Name = name,
                Description = description,
                Parent = this
            };
            return AddSubGroup(group);
        }
        public Group AddSubGroup(string name, string description,IFormFile image)
        {
            Group group = new Group { Name = name,Description = description,SetImage = image,
            Parent = this};
            return AddSubGroup(group);

        }
        public Group AddSubGroup(Group subGroup)
        {
            subGroup.Parent= this;
            SubGroups.Add(subGroup);
            return subGroup;
        }
        //הצגת פריטים כלל תתי הקבוצות
        [NotMapped]
        public List<Item> AllItems { get 
            { 
                return GetItems(this);
            } }
        //פונקציה ראשונה המקבלת קבוצה ובונה רשימה ריקה עבור הפריטים
        private List<Item> GetItems(Group group)
        {
            List <Item> items = new List <Item>();
            //שליחה לפונקציה הפינימית את הרשימה הריקה עם הקבוצה הראשית
             GetItems(group,items);
            return items;
        }
        //קבלה בפונקציה של קבוצה עם רשימת הפריטים 
        private void GetItems(Group group, List<Item> items)
        {
            //בדיקה האם יש תתי קבוצות לקבוצה הנוכחית
            if (group.SubGroups.Count > 0)
                //ריצה על תתי הקבוצות
                foreach (Group group1 in group.SubGroups)
                {
                    //שליחה של כל תת הקבוצה שוב לפונקציה ביחד עם הרשימה המתמלאת
                    GetItems(group1, items);
                }
            //בדיקה האם יש פריטים בקבוצה הנוכחית ואם יש הוא אוגר אותם לרשימה אחת גדולה 
            if(group.Items.Count >0)
                foreach (Item item in group.Items)
                {
                    items.Add(item);
                }
        }
    }
}


