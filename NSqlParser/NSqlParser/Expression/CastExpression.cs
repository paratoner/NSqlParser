/**
 * 
 * @author Paratoner
 */

using NSqlParser.Statement.Create.Table;

namespace NSqlParser.Expression
{
    public class CastExpression : IExpression
    {

        private IExpression leftExpression;
        private ColDataType type;
        private bool useCastKeyword = true;

        public ColDataType getType() {
            return type;
        }

        public void setType(ColDataType type) {
            this.type = type;
        }

        public IExpression getLeftExpression() {
            return leftExpression;
        }

        public void setLeftExpression(IExpression expression) {
            leftExpression = expression;
        }

	
        public void Accept(IExpressionVisitor expressionVisitor) {
            expressionVisitor.Visit(this);
        }

        public bool isUseCastKeyword() {
            return useCastKeyword;
        }

        public void setUseCastKeyword(bool useCastKeyword) {
            this.useCastKeyword = useCastKeyword;
        }


        public override string ToString()
        {
            if (useCastKeyword) {
                return "CAST(" + leftExpression + " AS " + type.ToString() + ")";
            } else {
                return leftExpression + "::" + type.ToString();
            }
        }
    }
}
