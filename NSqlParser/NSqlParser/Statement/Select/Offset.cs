/**
 * An offset clause in the form OFFSET offset
 * or in the form OFFSET offset (ROW | ROWS)
 */
namespace NSqlParser.Statement.Select
{
    public class Offset {

        private long offset;
        private bool offsetJdbcParameter = false;
        private string offsetParam = null;

        public long getOffset() {
            return offset;
        }

        public string getOffsetParam() {
            return offsetParam;
        }

        public void setOffset(long l) {
            offset = l;
        }

        public void setOffsetParam(string s) {
            offsetParam = s;
        }

        public bool isOffsetJdbcParameter() {
            return offsetJdbcParameter;
        }

        public void setOffsetJdbcParameter(bool b) {
            offsetJdbcParameter = b;
        }

        public override string ToString() {
            return " OFFSET " + (offsetJdbcParameter ? "?" : offset.ToString()) + (offsetParam != null ? " "+offsetParam : "");
        }
    }
}
