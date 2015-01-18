using System;

namespace NSqlParser.Expression.Operators.Relational
{
    public abstract class OldOracleJoinBinaryExpression : BinaryExpression, ISupportsOldOracleJoinSyntax
    {

        private int oldOracleJoinSyntax = NO_ORACLE_JOIN;
	
        private int oraclePriorPosition = NO_ORACLE_PRIOR;

	
        public void setOldOracleJoinSyntax(int oldOracleJoinSyntax) {
            this.oldOracleJoinSyntax = oldOracleJoinSyntax;
            if (oldOracleJoinSyntax < 0 || oldOracleJoinSyntax > 2) {
                throw new ArgumentException("unknown join type for oracle found (type=" + oldOracleJoinSyntax + ")");
            }
        }


        public override string ToString()
        {
            return (isNot() ? "NOT " : "") 
                   + (oraclePriorPosition == ORACLE_PRIOR_START ? "PRIOR " : "")
                   + getLeftExpression() 
                   + (oldOracleJoinSyntax == ORACLE_JOIN_RIGHT ? "(+)" : "") + " " 
                   + getStringExpression() + " " 
                   + (oraclePriorPosition == ORACLE_PRIOR_END ? "PRIOR " : "")
                   + getRightExpression() 
                   + (oldOracleJoinSyntax == ORACLE_JOIN_LEFT ? "(+)" : "");
        }

	
        public int getOldOracleJoinSyntax() {
            return oldOracleJoinSyntax;
        }

	
        public int getOraclePriorPosition() {
            return oraclePriorPosition;
        }

	
        public void setOraclePriorPosition(int oraclePriorPosition) {
            this.oraclePriorPosition = oraclePriorPosition;
        }
    }
}
