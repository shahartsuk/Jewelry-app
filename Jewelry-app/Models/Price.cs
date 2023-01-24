using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jewelry_app.Models
{
    public class Price
    {
        public Price() { Start = DateTime.Now;
            End = DateTime.Now.AddYears(1);
        }
        [Key]
        public int ID { get; set; }
        public Item item { get; set; }
        [Display(Name ="תאריך ושעת התחלה")]
        public DateTime Start { get; set; }= DateTime.Now;
        [Display(Name = "תאריך התחלה"),DataType(DataType.Date),NotMapped]
        public DateTime StartDate { get { return Start; }set { Start = new DateTime(value.Year,value.Month,value.Day,Start.Hour, Start.Minute,0); } }
        [Display(Name = "שעת התחלה"), DataType(DataType.Time), NotMapped]
        public DateTime StartTime { get {return Start; } set { Start = new DateTime(Start.Year,Start.Month,Start.Day,value.Hour,value.Minute,0); } }


        [Display(Name = "תאריך ושעת סיום")]
        public DateTime End { get; set; } = DateTime.Now;
        [Display(Name = "תאריך סיום"), DataType(DataType.Date), NotMapped]
        public DateTime EndDate { get { return End; } set { End = new DateTime(value.Year, value.Month, value.Day, End.Hour, End.Minute, 0); } }
        [Display(Name = "שעת סיום"), DataType(DataType.Time), NotMapped]
        public DateTime EndTime { get { return End; } set { End = new DateTime(End.Year, End.Month, End.Day, value.Hour, value.Minute, 0); } }
        [Display(Name = "מחיר")]
        public decimal MyPrice { get; set; }

    }
}
