using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/**
 * A list of ExpressionList items. e.g. multi values of insert statements. This
 * one allows only equally sized ExpressionList.
 *
 * @author Paratoner
 */

namespace NSqlParser.Expression.Operators.Relational
{
    public class MultiExpressionList : IItemsList
    {

        private List<ExpressionList> exprList;

        public MultiExpressionList()
        {
            this.exprList = new List<ExpressionList>();
        }

        public void Accept(IItemsListVisitor itemsListVisitor)
        {
            itemsListVisitor.Visit(this);
        }

        public List<ExpressionList> getExprList()
        {
            return exprList;
        }

        public void addExpressionList(ExpressionList el)
        {
            if (exprList.Count > 0
                && exprList[0].getExpressions().Count != el.getExpressions().Count)
            {
                throw new ArgumentException("different count of parameters");
            }
            exprList.Add(el);
        }

        public void addExpressionList(List<IExpression> list)
        {
            addExpressionList(new ExpressionList(list));
        }

        public void addExpressionList(IExpression expr)
        {
            addExpressionList(new ExpressionList(new List<IExpression>() { expr }));
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            foreach (var it in exprList)
            {
                b.Append(it.ToString());
                if (!(it == exprList.Last()))
                    b.Append(", ");
            }
            return b.ToString();
        }
    }
}
