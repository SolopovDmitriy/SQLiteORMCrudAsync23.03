using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*SQLiteConnector.CreateDatabaseSource("test.db");
                SQLiteConnector.Connection.Open();
                string createStudentTableQuery = "CREATE TABLE IF NOT EXISTS students (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, fio VARCHAR(128) NOT NULL, age INTEGER )";
                    SQLiteCommand sQLiteCommand = new SQLiteCommand(createStudentTableQuery, SQLiteConnector.Connection);
                sQLiteCommand.ExecuteNonQuery();
                SQLiteConnector.Connection.Close();*/

                string pathTofile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database", "test.db");
                SQLiteDBEngine dBEngine = new SQLiteDBEngine(pathTofile, SQLIteMode.EXISTS);


                //Console.WriteLine(dBEngine["student"].Name);

                SQLiteTable Students = dBEngine["student"];

                foreach (SQLiteColumn col in Students.HeadRowInfo)
                {
                    Console.WriteLine(col);
                }
                Console.WriteLine("Данные таблицы");
                foreach (var col in Students.BodyRows)
                {
                    Console.WriteLine($"ID: {col.Key}");
                    foreach (var item in col.Value)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }

                Students.Print();

                /*long lastIndex = Students.BodyRows.Count + 1;

                               List<string> newRow = new List<string>();
                                   newRow.Add("Вася");
                                   newRow.Add(15.ToString());
                               Students.BodyRows.Add(lastIndex, newRow);*/


                Students.AddOneRow(new List<string> { "Маклауд", 45.ToString() });
                List<KeyValuePair<string, string>> searchPattern = new List<KeyValuePair<string, string>>();
                searchPattern.Add(new KeyValuePair<string, string>("age", "35"));
                searchPattern.Add(new KeyValuePair<string, string>("fio", "Руслан"));
                searchPattern.Add(new KeyValuePair<string, string>("Id", 3.ToString()));


                //-------------------------------------------------------------------------------------------------------------------------start version 2

                // version 2  PK is not first column 
                //Dictionary<string, string> searchPattern = new Dictionary<string, string>();
                //searchPattern.Add("age", "35");
                //searchPattern.Add("fio", "Руслан");
                //searchPattern.Add("id", "3");



                //-------------------------------------------------------------------------------------------------------------------------stop version 2
                KeyValuePair<long, List<string>> ?findedStudent = Students.GetOneRow(searchPattern);
                //if (findedStudent.Value != null)
                //{
                //    Console.WriteLine("findedStudent: " + String.Join(" -- ", findedStudent.Value));
                //}
                //else
                //{
                //    Console.WriteLine("not found ");
                //}

                Students.Print();
                Students.UpdateOneRow(4, new List<string> { "Peter", "33" });
                Students.Print();

                //Students.AddOneRow(new List<string> { "Маклауд1", 45.ToString() });
                List<KeyValuePair<string, string>> createPattern = new List<KeyValuePair<string, string>>();
                createPattern.Add(new KeyValuePair<string, string>("age", "36"));
                createPattern.Add(new KeyValuePair<string, string>("fio", "Руслан1"));
                createPattern.Add(new KeyValuePair<string, string>("Id", 5.ToString()));
               

                //delete
                //Students.DeleteOneRow(16);
                //Console.WriteLine("name of table is " + Students.Name);

                // async  version1
                // Students.Create(new List<string> { "Peter", "33"}) ;   // sql query  = "insert into student('fio',age) values ('Peter',33)" - 
                //сохраняем запрос для синхронизации в будущем

                //Console.WriteLine("{0, -10} --- {1} --- {2}", "hello", "ok", "errro")


                // async  version1
                // Students.Create(new List<string> { "Peter", "33"}) ;   // sql query  = "insert into student('fio',age) values ('Peter',33)" - 
                //сохраняем запрос для синхронизации в будущем

                //Console.WriteLine("{0, -10} --- {1} --- {2}", "hello", "ok", "errro");



                /*           Students.CreateRow(long Id, List<string> newData)*/




                Console.WriteLine("Данные таблицы");
                foreach (var col in Students.BodyRows)
                {
                    Console.WriteLine($"ID: {col.Key}");
                    foreach (var item in col.Value)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }



                /*foreach (var item in dBEngine.Tables)
                {
                    Console.WriteLine(item);
                }
                dBEngine.getTableFields("student");*/

                //string[] str = { };
                /*SQLiteColumn sQLiteColumn = new SQLiteColumn(str);
                //sQLiteColumn.GetDateType("VARCHAR(128)");
                Console.WriteLine(sQLiteColumn.DataType);
                */

                /* SQLiteRow sQLiteRow = new SQLiteRow(3);
                 foreach (var item in sQLiteRow)
                 {
                     Console.WriteLine(item);
                 }*/
                //Console.WriteLine(sQLiteRow[0]);
                //Console.WriteLine(sQLiteRow["id"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
        
    }
}
