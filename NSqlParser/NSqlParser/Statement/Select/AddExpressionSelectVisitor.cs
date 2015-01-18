using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class AddExpressionSelectVisitor : ISelectVisitor
    {
        private readonly IExpression expression;
        public AddExpressionSelectVisitor(IExpression expr)
        {
            expression = expr;
        }
        public void Visit(PlainSelect plainSelect)
        {
            plainSelect.getSelectItems().Add(new SelectExpressionItem(expression));
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
