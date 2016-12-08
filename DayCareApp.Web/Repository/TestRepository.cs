using DayCareApp.Web.DataContext;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Repository
{
    public class TestRepository
    {
        DayCareAppDB dbContext = new DayCareAppDB();

        
        public int testDBConnection()
        {
            int result = -1;

            using (dbContext)
            {
                var inputParam = new SqlParameter("@InputParam", 1);

                var resultList = dbContext.Database.SqlQuery<int>("TestConnection @InputParam", inputParam).ToList();
                if (resultList.Count < 0)
                {
                    result = resultList[0];
                }
            }
            
            return result;
        }
    }
}