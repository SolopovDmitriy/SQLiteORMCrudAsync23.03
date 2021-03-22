using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class SQLiteRow:IEnumerable
    {
        private readonly List<SQLiteColumn> _columns;
        private int _countColumn;
        public SQLiteRow(int fieldCount)
        {
            _countColumn = fieldCount;
            if (fieldCount <= 0) throw new ArgumentOutOfRangeException();
            _columns = new List<SQLiteColumn>();
        }
        public void AddColumn(SQLiteColumn column)
        {
            if (_columns.Count + 1 < _countColumn)
            {
                _columns.Add(column);
            }
            else
            {
                throw new Exception("Many columns");
            }
        }
        public SQLiteColumn GetColumn(int index)
        {
            return _columns[index];
        }
        public IEnumerator GetEnumerator()
        {
            return _columns.GetEnumerator();
        }
        public object this [int index]
        {
            get
            {
                return _columns[index];
            }
        }
        public SQLiteColumn this[string nameCol]
        {
            get
            {
                foreach (SQLiteColumn item in _columns)
                {
                    if (item.Name.ToLower().Equals(nameCol.ToLower()))
                    {
                        return item;
                    }
                }
                throw new ArgumentException("Incorrect column name");
            }
        }
    }
}
