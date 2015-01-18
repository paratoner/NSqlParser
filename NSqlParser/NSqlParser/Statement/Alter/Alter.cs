using NSqlParser.Schema;
using NSqlParser.Statement.Create.Table;

namespace NSqlParser.Statement.Alter
{
    public class Alter : IStatement {
        private Table table;
        private string columnName;
        private ColDataType dataType;

        public Table getTable() {
            return table;
        }

        public void setTable(Table table) {
            this.table = table;
        }

        public string getColumnName() {
            return columnName;
        }

        public void setColumnName(string columnName) {
            this.columnName = columnName;
        }

        public ColDataType getDataType() {
            return dataType;
        }

        public void setDataType(ColDataType dataType) {
            this.dataType = dataType;
        }

	
        public void Accept(IStatementVisitor statementVisitor) {
            statementVisitor.Visit(this);
        }


        public override string ToString()
        {
            return "ALTER TABLE " + table.GetFullyQualifiedName() + " ADD COLUMN " + columnName + " " + dataType.ToString();
        }
    }
}
