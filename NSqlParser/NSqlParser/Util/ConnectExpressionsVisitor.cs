using System;
using System.Collections.Generic;
using NSqlParser.Expression;
using NSqlParser.Statement.Select;
/**
 * Connect all selected expressions with a binary expression. Out of select a,b
 * from table one gets select a || b as expr from table. The type of binary
 * expression is set by overwriting this class abstract method
 * createBinaryExpression.
 *
 * @author Paratoner
 */

namespace NSqlParser.Util
{
    public abstract class ConnectExpressionsVisitor : ISelectVisitor, ISelectItemVisitor
    {

        private string alias = "expr";
        private List<SelectExpressionItem> itemsExpr = new List<SelectExpressionItem>();

        public ConnectExpressionsVisitor()
        {
        }

        public ConnectExpressionsVisitor(string alias)
        {
            this.alias = alias;
        }

        /**
     * Create instances of this binary expression that connects all selected
     * expressions.
     *
     * @return
     */
        protected abstract BinaryExpression createBinaryExpression();

        public void Visit(PlainSelect plainSelect)
        {
            foreach (ISelectItem item in plainSelect.getSelectItems())
            {
                item.Accept(this);
            }

            if (itemsExpr.Count > 1)
            {
                BinaryExpression binExpr = createBinaryExpression();
                binExpr.setLeftExpression(itemsExpr[0].getExpression());

                for (int i = 1; i < itemsExpr.Count - 1; i++)
                {
                    binExpr.setRightExpression(itemsExpr[i].getExpression());
                    BinaryExpression binExpr2 = createBinaryExpression();
                    binExpr2.setLeftExpression(binExpr);
                    binExpr = binExpr2;
                }
                binExpr.setRightExpression(itemsExpr[itemsExpr.Count - 1].getExpression());

                SelectExpressionItem sei = new SelectExpressionItem();
                sei.setExpression(binExpr);

                plainSelect.getSelectItems().Clear();
                plainSelect.getSelectItems().Add(sei);
            }

            ((SelectExpressionItem)plainSelect.getSelectItems()[0]).setAlias(new Alias(alias));
        }

        public void Visit(SetOperationList setOpList)
        {
            foreach (PlainSelect select in setOpList.getPlainSelects())
            {
                select.Accept(this);
            }
        }

        public void Visit(WithItem withItem)
        {
        }

        public void Visit(AllTableColumns allTableColumns)
        {
            throw new NotSupportedException("Not supported yet.");
        }

        public void Visit(AllColumns allColumns)
        {
            throw new NotSupportedException("Not supported yet.");
        }

        public void Visit(SelectExpressionItem selectExpressionItem)
        {
            itemsExpr.Add(selectExpressionItem);
        }
    }
}
