using System.Data.Entity;

namespace Jewelry_app.Models
{
    public class DataLayer:DbContext
    {
        private static DataLayer _Instance;
        private DataLayer() : base("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Jewelry_app;Data Source=LAPTOP-GJS6JBCP\\SQLEXPRESS")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataLayer>());
            if (Groups.Count() == 0) Seed();
        }
        public static DataLayer Instance { get { if (_Instance == null) { _Instance = new DataLayer(); } return _Instance; } }
        private void Seed()
        {
            //קבוצה ראשונית, ברירת מחדל
            Group group = new Group { Name = "חנות תכשיטים" };
            Groups.Add(group);

            //מחיר ראשוני, ברירת מחדל
            Price price = new Price { MyPrice= 5000, End =DateTime.Now.AddYears(20)};
            Prices.Add(price);

            SaveChanges();
        }

        // מאפיין המחזיר את הקבוצות עם כל המידע עליהם
        public List<Group> GroupsAllIncludes { get 
            {
                //אני לוקח את המוצרים מהמסד נתונים ושאני עושה אינקלוד למוצרים הוא יודע שזה המוצרים שלקחתי כולל התמונות והמחירים
                List<Item> items = Instance.Items.Include(i=>i.Prices).Include(i=>i.Images).Include(i=>i.Group).ToList();
                return Instance.Groups.Include(g => g.SubGroups).Include(g => g.Items).ToList();
            } }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
