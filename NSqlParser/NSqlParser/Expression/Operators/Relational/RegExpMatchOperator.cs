using System;

namespace NSqlParser.Expression.Operators.Relational
{
    public class RegExpMatchOperator : BinaryExpression {

        private RegExpMatchOperatorType operatorType;

        public RegExpMatchOperator(RegExpMatchOperatorType operatorType) {
            this.operatorType = operatorType;
        }

        public RegExpMatchOperatorType getOperatorType() {
            return operatorType;
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }

	
        public override string getStringExpression() {
            switch (operatorType) {
                case RegExpMatchOperatorType.MATCH_CASESENSITIVE:
                    return "~";
                case RegExpMatchOperatorType.MATCH_CASEINSENSITIVE:
                    return "~*";
                case RegExpMatchOperatorType.NOT_MATCH_CASESENSITIVE:
                    return "!~";
                case RegExpMatchOperatorType.NOT_MATCH_CASEINSENSITIVE:
                    return "!~*";
            }
            return null;
        }
    }
}
