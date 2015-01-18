/**
 * A fetch clause in the form FETCH (FIRST | NEXT) row_count (ROW | ROWS) ONLY
 */
namespace NSqlParser.Statement.Select
{
    public class Fetch {

        private long rowCount;
        private bool fetchJdbcParameter = false;
        private bool isFetchParamFirst = false;
        private string fetchParam = "ROW";

        public long getRowCount() {
            return rowCount;
        }

        public void setRowCount(long l) {
            rowCount = l;
        }

        public bool isFetchJdbcParameter() {
            return fetchJdbcParameter;
        }

        public string getFetchParam() {
            return fetchParam;
        }

        public bool IsFetchParamFirst() {
            return isFetchParamFirst;
        }

        public void setFetchJdbcParameter(bool b) {
            fetchJdbcParameter = b;
        }

        public void setFetchParam(string s) {
            this.fetchParam = s;
        }

        public void setFetchParamFirst(bool b) {
            this.isFetchParamFirst = b;
        }

        public override string ToString() {
            return " FETCH " + (isFetchParamFirst ? "FIRST" : "NEXT") + " " + (fetchJdbcParameter ? "?" : rowCount + "") + " "+ fetchParam + " ONLY";
        }
    }
}
