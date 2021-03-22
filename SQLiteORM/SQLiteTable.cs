using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class SQLiteTable
    {
        private string _name;
        private SQLiteRow _headRow;
        private SortedList<long, List<string>> _bodyRows;
        public SQLiteRow HeadRowInfo
        {
            get
            {
                return _headRow;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public SortedList<long, List<string>> BodyRows
        {
            get
            {
                return _bodyRows;
            }
        }
        public SQLiteTable(string tableName, SQLiteRow headRow, SortedList<long, List<string>> bodyRows)
        {
            _name = tableName; //валидация?
            _headRow = headRow;
            _bodyRows = bodyRows;
        }

        public void AddOneRow(List<string> row)
        {
            this.BodyRows.Add(_bodyRows.Count + 1, row);

            string query = $"INSERT INTO {_name} (";
            foreach (SQLiteColumn column in _headRow)
            {
                query += column.Name + ", ";
            }
            query = query.Substring(0, query.Length - 2);
            query += $") VALUES ( {_bodyRows.Count + 1}, ";

            foreach (string item in row)
            {
                query += "'" + item + "', ";
            }
            query = query.Substring(0, query.Length - 2);
            query += ")";

            SQLiteDBEngine.AsyncQuery.Add(query);
        }
     
        public KeyValuePair<long, List<string>> GetOneRow(long Id)
        {
            if (BodyRows.ContainsKey(Id))
            {
                return new KeyValuePair<long, List<string>>(Id, BodyRows[Id]);
            }
            else
            {
                throw new ArgumentException("Incorrect row Id");
            }
        }
        public KeyValuePair<long, List<string>>? GetOneRow(List<KeyValuePair<string, string>> searchPattern)
        {
            bool searchMatched;
            foreach (KeyValuePair<long, List<string>> oneRow in BodyRows)
            {
                searchMatched = true;
                foreach (KeyValuePair<string, string> pattern in searchPattern)
                {
                    int indexCol = HeadRowInfo[pattern.Key].Cid;

                    if (indexCol != 0)
                    {
                        if (oneRow.Value[indexCol - 1] != pattern.Value)
                        {
                            searchMatched = false;
                            break;
                        }
                    }
                    else
                    {
                        if (oneRow.Key != Int64.Parse(pattern.Value))
                        {
                            searchMatched = false;
                            break;
                        }
                    }
                }
                if (searchMatched)
                {
                    return oneRow;
                }
            }
            return null;
        }
        public void Print()
        {


            //Console.WriteLine("{0, -10} --- {1} --- {2}", "hello", "ok", "errro");


            Console.WriteLine("Данные таблицы");
            Console.Write("+");
            for (int i = 0; i < 2; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            foreach (SQLiteColumn col in HeadRowInfo)
            {
                if (col.Name == "id")
                {
                    Console.Write("|{0,-2}", col.Name);

                }

                else
                {
                    Console.Write("|{0,-20}", col.Name);
                }
            }
            Console.WriteLine("|");
            Console.Write("+");
            for (int i = 0; i < 2; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            // Console.WriteLine("-------------------------------------------");

            foreach (var col in BodyRows)
            {
                Console.Write("|{0,-2}", col.Key);
                foreach (var item in col.Value)
                {
                    Console.Write("|{0,-20}", item);
                }
                Console.WriteLine("|");
            }
            Console.Write("+");
            for (int i = 0; i < 2; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            for (int i = 0; i < 20; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }

        public bool UpdateOneRow(long Id, List<string> newData) // List<string> { "Олег",	"25"}; 
        {
            if (BodyRows.ContainsKey(Id))
            {
                BodyRows[Id] = newData;

                string queryUpdate = $"UPDATE {_name} SET ";//делаем текст запроса
                int i = 0;
                foreach (SQLiteColumn column in _headRow)//column - данные о столбце (cid, Name, IsPrimaryKey, .....), _headRow - все столбцы - columns
                {
                    if (!column.IsPrimaryKey)
                    {
                        queryUpdate += $"{column.Name} = '{newData[i]}',"; // {column.Name} = '{row[i]}',    -     fio = 'DFcz',
                        i++;
                    }

                }
                queryUpdate = queryUpdate.Substring(0, queryUpdate.Length - 1);
                queryUpdate += $" WHERE id = {Id} ";

                 SQLiteDBEngine.AsyncQuery.Add(queryUpdate);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteOneRow(long Id)
        {
            if (BodyRows.ContainsKey(Id))
            {
                BodyRows.Remove(Id);

                string queryDelete = $"DELETE FROM {_name} WHERE id = {Id}";

                SQLiteDBEngine.AsyncQuery.Add(queryDelete);

                return true;
            }
            else
            {
                return false;
            }
        }

       


        //public void Async()
        //{
        //    SQLiteConnector.Connection.Open();
        //    foreach (string queryGetTablesData in queriesForAsync)
        //    {
        //        SQLiteCommand sQLiteCommand = new SQLiteCommand(queryGetTablesData, SQLiteConnector.Connection);
        //        int insertedCount = sQLiteCommand.ExecuteNonQuery();

        //    }
        //    SQLiteConnector.Connection.Close();
        //}

    }
}