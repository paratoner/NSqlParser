/**
 * Modulo expression (a % b).
 *
 * @author toben
 */
namespace NSqlParser.Expression.Operators.Arithmetic
{
    public class Modulo : BinaryExpression
    {

        public Modulo()
        {
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string getStringExpression()
        {
            return "%";
        }
    }
}
