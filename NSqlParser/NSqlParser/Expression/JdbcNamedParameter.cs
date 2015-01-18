/**
 *
 * @author Paratoner
 */
namespace NSqlParser.Expression
{
    public class JdbcNamedParameter : IExpression
    {

        private string name;

        /**
     * The name of the parameter
     *
     * @return the name of the parameter
     */
        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }


        public void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string ToString()
        {
            return ":" + name;
        }
    }
}
