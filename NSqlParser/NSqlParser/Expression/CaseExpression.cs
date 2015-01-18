/**
 * CASE/WHEN expression.
 *
 * Syntax:
 * <code><pre>
 * CASE
 * WHEN condition THEN expression
 * [WHEN condition THEN expression]...
 * [ELSE expression]
 * END
 * </pre></code>
 *
 * <br/>
 * or <br/>
 * <br/>
 *
 * <code><pre>
 * CASE expression
 * WHEN condition THEN expression
 * [WHEN condition THEN expression]...
 * [ELSE expression]
 * END
 * </pre></code>
 *
 * See also: https://aurora.vcu.edu/db2help/db2s0/frame3.htm#casexp
 * http://sybooks.sybase.com/onlinebooks/group-as/asg1251e /commands/
 *
 * @ebt-link;pt=5954?target=%25N%15_52628_START_RESTART_N%25
 *
 *
 * @author Havard Rast Blok
 */

using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Expression
{
    public class CaseExpression : IExpression {

        private IExpression switchExpression;
        private List<IExpression> whenClauses;
        private IExpression elseExpression;

        /*
	 * (non-Javadoc)
	 * 
	 * @see net.sf.jsqlparser.expression.Expression#Accept(net.sf.jsqlparser.expression.ExpressionVisitor)
	 */
	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        /**
	 * @return Returns the switchExpression.
	 */
        public IExpression getSwitchExpression() {
            return switchExpression;
        }

        /**
	 * @param switchExpression The switchExpression to set.
	 */
        public void setSwitchExpression(IExpression switchExpression) {
            this.switchExpression = switchExpression;
        }

        /**
	 * @return Returns the elseExpression.
	 */
        public IExpression getElseExpression() {
            return elseExpression;
        }

        /**
	 * @param elseExpression The elseExpression to set.
	 */
        public void setElseExpression(IExpression elseExpression) {
            this.elseExpression = elseExpression;
        }

        /**
	 * @return Returns the whenClauses.
	 */
        public List<IExpression> getWhenClauses() {
            return whenClauses;
        }

        /**
	 * @param whenClauses The whenClauses to set.
	 */
        public void setWhenClauses(List<IExpression> whenClauses) {
            this.whenClauses = whenClauses;
        }


        public override string ToString()
        {
            return "CASE " + ((switchExpression != null) ? switchExpression + " " : "")
                   + PlainSelect.getStringList(whenClauses, false, false) + " "
                   + ((elseExpression != null) ? "ELSE " + elseExpression + " " : "") + "END";
        }
    }
}
