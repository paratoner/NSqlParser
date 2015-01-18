/**
 * A top clause in the form [TOP (row_count) or TOP row_count]
 */
namespace NSqlParser.Statement.Select
{
    public class Top {

        private long rowCount;
        private bool rowCountJdbcParameter = false;
        private bool hasParenthesis = false;
        private bool isPercentage = false;

        public long getRowCount() {
            return rowCount;
        }

        // TODO instead of a plain number, an expression should be added, which could be a NumberExpression, a GroupedExpression or a JdbcParameter
        public void setRowCount(long rowCount) {
            this.rowCount = rowCount;
        }

        public bool isRowCountJdbcParameter() {
            return rowCountJdbcParameter;
        }

        public void setRowCountJdbcParameter(bool rowCountJdbcParameter) {
            this.rowCountJdbcParameter = rowCountJdbcParameter;
        }

        public bool HasParenthesis() {
            return hasParenthesis;
        }

        public void setParenthesis(bool hasParenthesis) {
            this.hasParenthesis = hasParenthesis;
        }

        public bool IsPercentage() {
            return isPercentage;
        }

        public void setPercentage(bool percentage) {
            this.isPercentage = percentage;
        }

        public override string ToString() {
            string result = "TOP ";

            if (hasParenthesis) {
                result += "(";
            }

            result += rowCountJdbcParameter ? "?"
                : rowCount.ToString();

            if (hasParenthesis) {
                result += ")";
            }

            if (isPercentage) {
                result += " PERCENT";
            }

            return result;
        }
    }
}
