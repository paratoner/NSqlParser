using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.Table
{
    public class Index
    {

        private string type;
        private List<string> columnsNames;
        private string name;

        /**
     * A list of strings of all the columns regarding this index
     */
        public List<string> getColumnsNames()
        {
            return columnsNames;
        }

        public string getName()
        {
            return name;
        }

        /**
     * The type of this index: "PRIMARY KEY", "UNIQUE", "INDEX"
     */
        public string getType()
        {
            return type;
        }

        public void setColumnsNames(List<string> list)
        {
            columnsNames = list;
        }

        public void setName(string stringn)
        {
            name = stringn;
        }

        public void setType(string stringt)
        {
            type = stringt;
        }

        public override string ToString()
        {
            return type + " " + PlainSelect.getStringList(columnsNames, true, true) + (name != null ? " " + name : "");
        }
    }
}
