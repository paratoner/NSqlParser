using System;

namespace NSqlParser.Expression.Operators.Relational
{
    public class RegExpMySQLOperator : BinaryExpression
    {

        private RegExpMatchOperatorType operatorType;

        public RegExpMySQLOperator(RegExpMatchOperatorType operatorType)
        {
            this.operatorType = operatorType;
        }

        public RegExpMatchOperatorType getOperatorType()
        {
            return operatorType;
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string getStringExpression()
        {
            switch (operatorType)
            {
                case RegExpMatchOperatorType.MATCH_CASESENSITIVE:
                    return "REGEXP BINARY";
                case RegExpMatchOperatorType.MATCH_CASEINSENSITIVE:
                    return "REGEXP";
                default:
                    break;
            }
            return null;
        }
    }
}
