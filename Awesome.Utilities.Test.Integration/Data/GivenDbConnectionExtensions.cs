using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Awesome.Utilities.Test.Integration.Data
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDbConnectionExtensions
    {
        private IDbConnection connection;

        [SetUp]
        public void SetUp()
        {
            if (File.Exists("IntegrationTest.s3db"))
            {
                File.Delete("IntegrationTest.s3db");
            }
            this.connection = new SQLiteConnection("Data Source=IntegrationTest.s3db;Synchronous=Off;Version=3;New=True;Pooling=True;Max Pool Size=1;");
            this.connection.Open();
            
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE Tests (ID INT NOT NULL, Name NVARCHAR(10) NOT NULL);";
                command.ExecuteNonQuery();
            }
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Tests (ID, Name) VALUES (1, 'Test1');";
                command.ExecuteNonQuery();
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
            }
            SQLiteConnection.ClearAllPools();
        }

        [Test]
        public void When_using_execute_reader_Then_works()
        {
            int id = 0;
            string name = null;
            int count = 0;
            using (var reader = this.connection.ExecuteReader("SELECT ID, Name FROM Tests"))
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    name = reader.GetString(1);
                    count++;
                }
            }

            Assert.That(count, Is.EqualTo(1), "should only have one result");
            Assert.That(id, Is.EqualTo(1));
            Assert.That(name, Is.EqualTo("Test1"));
        }

        [Test]
        public void When_using_execute_scalar_Then_works()
        {
            int count = this.connection.ExecuteScalar<int>("SELECT COUNT(ID) FROM Tests WHERE ID = 1");

            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void When_using_execute_non_query_Then_works()
        {
            int count = this.connection.ExecuteNonQuery("UPDATE Tests SET Name = 'LOL' WHERE ID = 1");

            Assert.That(count, Is.EqualTo(1));
        }
    }
}
