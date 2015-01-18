using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Drop
{
    public class Drop : IStatement
    {

        private string type;
        private string name;
        private List<string> parameters;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        public string getName()
        {
            return name;
        }

        public List<string> getParameters()
        {
            return parameters;
        }

        public string getType()
        {
            return type;
        }

        public void setName(string stringn)
        {
            name = stringn;
        }

        public void setParameters(List<string> list)
        {
            parameters = list;
        }

        public void setType(string stringt)
        {
            type = stringt;
        }

        public override string ToString()
        {
            string sql = "DROP " + type + " " + name;

            if (parameters != null && parameters.Count > 0)
            {
                sql += " " + PlainSelect.getStringList(parameters);
            }

            return sql;
        }
    }
}
