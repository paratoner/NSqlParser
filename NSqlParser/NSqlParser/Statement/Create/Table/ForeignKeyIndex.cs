using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.Table
{
    public class ForeignKeyIndex : NamedConstraint {
        private Schema.Table table;
        private List<string> referencedColumnNames;

        public Schema.Table getTable() {
            return table;
        }

        public void setTable(Schema.Table table) {
            this.table = table;
        }

        public List<string> getReferencedColumnNames() {
            return referencedColumnNames;
        }

        public void setReferencedColumnNames(List<string> referencedColumnNames) {
            this.referencedColumnNames = referencedColumnNames;
        }

        public override string ToString() {
            return base.ToString()
                   + " REFERENCES " + table + PlainSelect.getStringList(getReferencedColumnNames(), true, true);
        }
    }
}
