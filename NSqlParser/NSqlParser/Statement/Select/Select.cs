using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSqlParser.Statement.Select
{
    public class Select : IStatement
    {

        private ISelectBody selectBody;
        private List<WithItem> withItemsList;

        public void Accept(IStatementVisitor statementVisitor)
        {
            statementVisitor.Visit(this);
        }

        public ISelectBody getSelectBody()
        {
            return selectBody;
        }

        public void setSelectBody(ISelectBody body)
        {
            selectBody = body;
        }

        public override string ToString()
        {
            StringBuilder retval = new StringBuilder();
            if (withItemsList != null && withItemsList.Count > 0)
            {
                retval.Append("WITH ");
                foreach (var withItem in withItemsList)
                {
                    retval.Append(withItem);
                    if (!(withItem == withItemsList.Last()))
                        retval.Append(",");
                    retval.Append(" ");
                }

            }
            retval.Append(selectBody);
            return retval.ToString();
        }

        public List<WithItem> getWithItemsList()
        {
            return withItemsList;
        }

        public void setWithItemsList(List<WithItem> withItemsList)
        {
            this.withItemsList = withItemsList;
        }
    }
}
