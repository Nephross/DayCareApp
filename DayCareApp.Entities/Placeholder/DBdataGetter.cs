using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayCareApp.Entities.Placeholder
{
    class DBdataGetter
    {

        public static void StartConnection()
        {
            // testing if this shit works
            DBinitializer db = new DBinitializer();


            db.DbCommand("INSERT INTO dbo.Child (ID, Name, Parent, Institution, Country, Age, CurrentlyCheckedIn, ImagePath) " +
                                    "Values ('2', 'Frank', 'Senior', 'Elefanten', 'Danmark', '11', 'YES', 'randomurl2')");

            db.DbReadOrderData();

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
        
    }
}
