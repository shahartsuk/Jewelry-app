using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Jewelry_app.Models
{
    public class Image
    {
        [Key] 
        public int ID { get; set; }
        [Required] public Item item { get; set; }

        [Display(Name = "תמונה")]
        public byte[] MyImage { get; set; }

        //תכונת הוספה של תמונה
        public IFormFile setImage
        {
            set
            {
                if (value == null) return;
                //יצירת מקום בזכרון המכיל קובץ
                MemoryStream stream = new MemoryStream();
                //העתקת הקובץ מהמשתמש למקום שנוצר זכרון
                value.CopyTo(stream);
                //הפיכת הזכרון למערך כדי שיוכל להיכנס למסד נתונים
                MyImage = stream.ToArray();

            }
        }
    }
}
