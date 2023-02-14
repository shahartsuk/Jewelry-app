using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Jewelry_app.Models;
using Jewelry_app.ViewModels.Manager;

namespace Jewelry_app.Controllers
{
    public class ManagerController : Controller
    {
        //הצגת הקבוצות והפריטים שלה למנהל
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                Group group = DataLayer.Instance.GroupsAllIncludes.Find(g => g.ID == id);
                if (group != null)
                    return View(group);
            }
            Group group1 = DataLayer.Instance.GroupsAllIncludes.First();
            return View(group1);
        }
        //הוספת קבוצה
        public IActionResult Create(int? id)
        {
            List<Group> groups = DataLayer.Instance.Groups.ToList();

            if (id != null)
            {
                Group parent = groups.Find(g => g.ID == id);
                if (parent != null) return View(new VMCreateGroup { Groups = groups, Parent = parent, ParentID = id.Value });
            }

            return View(new VMCreateGroup { Groups = groups,Parent = groups.First(),ParentID = groups.First().ID });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VMCreateGroup VM)
        {
            Group Parent = DataLayer.Instance.Groups.FirstOrDefault(g => g.ID == VM.ParentID);
            if(Parent != null)
            {
                //הוספת הקבוצה החדשה להורה המאושר
                Parent.AddSubGroup(VM.Group);
                //הוספת התמונה לילד המאושר
                VM.Group.SetImage = VM.FileGroup;
                if(VM.Item.Name != null || VM.Item.ID !=0)
                {
                    VM.Group.AddItem(VM.Item).AddPrice(VM.Price);
                    VM.Item.AddImage(VM.FileItem);
                }
                DataLayer.Instance.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }
        public IActionResult DetailsItem(int? id)
        {
            //List<Item> items = DataLayer.Instance.Items.Include(i => i.Prices).Include(i => i.Images).ToList();
            if (id == null)
            {
                return View("Index");
            }
            Item item = DataLayer.Instance.Items.Include(i => i.Prices).Include(i => i.Images).ToList().Find(i => i.ID == id);
            if (item == null) return View("Index");
            return View(item);
        }
        public IActionResult EditItem(int? id)
        {
            //קבלת רשימת הפריטים מהמסד נתונים,ומציאת הפריט לפי הקוד
            Item item = DataLayer.Instance.Items.Include(i=>i.Prices).Include(i=>i.Images).ToList().Find(i => i.ID == id);
            //אם הקוד ריק חזרה לדף הבית
            if (id == null) return RedirectToAction("Index");
            //אם הקוד לא נכון והפריט ריק חזרה לדף הבית
            if (item == null) return RedirectToAction("Index");
            return View(new VMEditItem { Item = item, Price = item.Prices.FirstOrDefault()});
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditItem(VMEditItem VM)
        {
            //אם הפריט שהתקבל ריק חזרה לדף הבית
            if (VM == null) return RedirectToAction("Index");
            //מציאת הפריט מהמסד נתונים לפי הפריט שקיבלנו
            Item itemDB = DataLayer.Instance.Items.Include(i => i.Prices).Include(i => i.Images).ToList().Find(i=>i.ID == VM.Item.ID);
            //בדיקה אם קיבלנו את הפריט מהמסד נתונים
            if (itemDB == null) return RedirectToAction("Index");
            //עדכון הנתונים של הפריט
            itemDB.Name = VM.Item.Name;
            itemDB.Description = VM.Item.Description;
            if(VM.Price != null)
            {
                itemDB.AddPrice(VM.Price);
            }
            if(VM.File != null)
            {
                itemDB.AddImage(VM.File);
            }
            DataLayer.Instance.SaveChanges();
            //בשביל להוסיף תמונה או מחיר אני שולח את המודל של יצירת הפריט
            return View("Details", new VMEditItem { Item = itemDB });
        }
        
        public IActionResult AddItem(int? id)
        {
            List<Group> groups = DataLayer.Instance.Groups.ToList();

            if (id != null)
            {
                Group group = groups.Find(g => g.ID == id);
                if (group != null) return View(new VMCreateItem { Groups = groups, Group = group, GroupID = id.Value });
            }
            return RedirectToAction("Index","Home");
        }
        //פונקציה המציגה את פרטי התכשיט
        
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddItem(VMCreateItem VM)
        {
             Group group = DataLayer.Instance.Groups.FirstOrDefault(g => g.ID == VM.GroupID);
            if (VM.Item.Name != null)
            {
                VM.Group.AddItem(VM.Item).AddPrice(VM.Price);
                VM.Item.AddImage(VM.File);
                group.AddItem(VM.Item);
                DataLayer.Instance.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
//[HttpPost, ValidateAntiForgeryToken]
//public IActionResult addImage(VMCreateItem VM)
//{
//    //במידה ולא התקבל אובייקט\חזרה לדף הבית
//    if (VM == null) return RedirectToAction("Index");
//    //מציאת הפריט שאני רוצה להוסיף לו תמונה מהמסד נתונים
//    Item? item = DataLayer.Instance.Items.ToList().Find(i => i.ID == VM.Item.ID);
//    //בדיקה אם הפריט שקיבלתי הוא ריק
//    if(item == null) return RedirectToAction("Index");
//    if(VM.File != null)
//    {
//        item.AddImage(VM.File);
//        DataLayer.Instance.SaveChanges();
//    }
//    return View();
//}
//[HttpPost, ValidateAntiForgeryToken]
//public IActionResult addPrice(VMCreateItem VM)
//{
//    //במידה ולא התקבל אובייקט\חזרה לדף הבית
//    if (VM == null) return RedirectToAction("Index");
//    //מציאת הפריט שאני רוצה להוסיף לו תמונה מהמסד נתונים
//    Item? item = DataLayer.Instance.Items.ToList().Find(i => i.ID == VM.Item.ID);
//    //בדיקה אם הפריט שקיבלתי הוא ריק
//    if (item == null) return RedirectToAction("Index");
//    if (VM.Price != null)
//    {
//        item.AddPrice(VM.Price);
//        DataLayer.Instance.SaveChanges();
//    }
//    return View();
//}