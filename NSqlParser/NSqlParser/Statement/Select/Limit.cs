/**
 * A limit clause in the form [LIMIT {[offset,] row_count) | (row_count | ALL)
 * OFFSET offset}]
 */
namespace NSqlParser.Statement.Select
{
    public class Limit
    {

        private long offset;
        private long rowCount;
        private bool rowCountJdbcParameter = false;
        private bool offsetJdbcParameter = false;
        private bool limitAll;
        private bool limitNull = false;

        public long getOffset()
        {
            return offset;
        }

        public long getRowCount()
        {
            return rowCount;
        }

        public void setOffset(long l)
        {
            offset = l;
        }

        public void setRowCount(long l)
        {
            rowCount = l;
        }

        public bool isOffsetJdbcParameter()
        {
            return offsetJdbcParameter;
        }

        public bool isRowCountJdbcParameter()
        {
            return rowCountJdbcParameter;
        }

        public void setOffsetJdbcParameter(bool b)
        {
            offsetJdbcParameter = b;
        }

        public void setRowCountJdbcParameter(bool b)
        {
            rowCountJdbcParameter = b;
        }

        /**
     * @return true if the limit is "LIMIT ALL [OFFSET ...])
     */
        public bool isLimitAll()
        {
            return limitAll;
        }

        public void setLimitAll(bool b)
        {
            limitAll = b;
        }

        /**
     * @return true if the limit is "LIMIT NULL [OFFSET ...])
     */
        public bool isLimitNull() { return limitNull; }

        public void setLimitNull(bool b) { limitNull = b; }

        public override string ToString()
        {
            string retVal = "";
            if (limitNull)
            {
                retVal += " LIMIT NULL";
            }
            else if (rowCount >= 0 || rowCountJdbcParameter)
            {
                retVal += " LIMIT " + (rowCountJdbcParameter ? "?" : rowCount + "");
            }
            if (offset > 0 || offsetJdbcParameter)
            {
                retVal += " OFFSET " + (offsetJdbcParameter ? "?" : offset + "");
            }
            return retVal;
        }
    }
}
