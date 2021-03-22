using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteORM
{
    class SQLiteColumn
    {
        private int _cid;
        private string _name;
        private SQLiteDataTypes _dataType;
        private bool _notNull;
        private string _defValue;
        private bool _pk;
        public SQLiteColumn(string[] str)
        {
            _cid = Convert.ToInt32(str[0]);
            _name = str[1];
            _dataType = GetDateType(str[2]);
            _notNull = str[3] == "1" ? true : false;
            _defValue = str[4];
            _pk = str[5] == "1" ? true : false;
        }
        public SQLiteDataTypes GetDateType(string dateType)
        {
            foreach (var item in Enum.GetValues(typeof(SQLiteDataTypes)))
            {
                if (dateType.IndexOf(item.ToString()) != -1)
                {
                    return (SQLiteDataTypes)Enum.Parse(typeof(SQLiteDataTypes), item.ToString(), true);
                }
            }
            throw new ArgumentException("Incorrect arguments");
        }
        public SQLiteDataTypes DataType
        {
            get
            {
                return _dataType;
            }
        }
        public int Cid
        {
            get
            {
                return _cid;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public bool NotNull
        {
            get
            {
                return _notNull;
            }
        }
        public string DefaultValue
        {
            get
            {
                return _defValue;
            }
        }
        public bool IsPrimaryKey
        {
            get
            {
                return _pk;
            }
        }
        public override string ToString()
        {
            return $"CiD: {Cid}; Name: {Name}; Type: {DataType}; NOT NULL: {NotNull}; DefValue: {DefaultValue}; is PrimaryKey: {IsPrimaryKey}";
        }
    }
}
