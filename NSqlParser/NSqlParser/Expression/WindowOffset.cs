using System.Text;

namespace NSqlParser.Expression
{
    public class WindowOffset {

        public enum Type {

            PRECEDING,
            FOLLOWING,
            CURRENT,
            EXPR
        }
    
        private IExpression expression;
        private Type type;

        public IExpression getExpression() {
            return expression;
        }

        public void setExpression(IExpression expression) {
            this.expression = expression;
        }

        public Type getType() {
            return type;
        }

        public void setType(Type type) {
            this.type = type;
        }

    
        public override string ToString() {
            StringBuilder buffer = new StringBuilder();
            if (expression != null) {
                buffer.Append(' ').Append(expression);
                buffer.Append(' ');
                buffer.Append(type);
            } else {
                switch (type) {
                    case Type.PRECEDING:
                        buffer.Append(" UNBOUNDED PRECEDING");
                        break;
                    case Type.FOLLOWING:
                        buffer.Append(" UNBOUNDED FOLLOWING");
                        break;
                    case Type.CURRENT:
                        buffer.Append(" CURRENT ROW");
                        break;
                    default:
                        break;
                }
            }
            return buffer.ToString();
        }
    }
}
