using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class AddGroupBySelectVisitor : ISelectVisitor
    {
        private IExpression expr;

        public AddGroupBySelectVisitor(IExpression expr)
        {
            // TODO: Complete member initialization
            this.expr = expr;
        }
        public void Visit(PlainSelect plainSelect)
        {
            plainSelect.addGroupByColumnReference(expr);
        }

        public void Visit(SetOperationList setOpList)
        {
            throw new NotSupportedException("Not supported yet.");
        }

        public void Visit(WithItem withItem)
        {
            throw new NotSupportedException("Not supported yet.");
        }
    }
}
