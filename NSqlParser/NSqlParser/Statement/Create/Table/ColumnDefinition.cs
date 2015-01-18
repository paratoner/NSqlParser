using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.Table
{
    public class ColumnDefinition {

        private string columnName;
        private ColDataType colDataType;
        private List<string> columnSpecstrings;

        /**
	 * A list of strings of every word after the datatype of the column.<br>
	 * Example ("NOT", "NULL")
	 */
        public List<string> getColumnSpecstrings() {
            return columnSpecstrings;
        }

        public void setColumnSpecstrings(List<string> list) {
            columnSpecstrings = list;
        }

        /**
	 * The {@link ColDataType} of this column definition
	 */
        public ColDataType getColDataType() {
            return colDataType;
        }

        public void setColDataType(ColDataType type) {
            colDataType = type;
        }

        public string getColumnName() {
            return columnName;
        }

        public void setColumnName(string stringc) {
            columnName = stringc;
        }

        public override string ToString() {
            return columnName + " " + colDataType + (columnSpecstrings != null ? " " + PlainSelect.getStringList(columnSpecstrings, false, false) : "");
        }
    }
}
