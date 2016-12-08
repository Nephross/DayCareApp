using DayCareApp.Web.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC = System.Data.SqlClient;

namespace DayCareApp.Tests.Misc
{
    [TestFixture]
    public class MiscTest
    {
               
     //   [Test]
        public void TestRepoConnection()
        {
            // Arrange
            TestRepository _Repo = new TestRepository();

            // Act
            int result = _Repo.testDBConnection();

            // Assert
            Assert.AreEqual(42, result, "TestConnection failed, as it did not return 42 from db.");
            }

        [Test]
        public void TestConnection()
        {
            // Arrange
            

            // Act

            using (var connection = new QC.SqlConnection(
                   "Server=tcp:keaprojectdb.database.windows.net,1433;Initial Catalog=dayCareAppDB;Persist Security Info=False;User ID=dayCareAppAdmin;Password=gihrdiw34klng346GW32W12gBWgwsvcjjuiwA4r9ug34;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                   ))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");

                    Console.WriteLine("Press any key to finish...");
                    Console.ReadKey(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not connect to DB");
                }
            }
           
            // Assert
            
        }
    

    
}
}
