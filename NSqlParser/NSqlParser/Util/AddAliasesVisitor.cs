using System;
using System.Collections.Generic;
using NSqlParser.Expression;
using NSqlParser.Statement.Select;
/**
 * Add aliases to every column and expression selected by a select - statement.
 * Existing aliases are recognized and preserved. This class standard uses a
 * prefix of A and a counter to generate new aliases (e.g. A1, A5, ...). This
 * behaviour can be altered.
 *
 * @author tw
 */

namespace NSqlParser.Util
{
    public class AddAliasesVisitor : ISelectVisitor, ISelectItemVisitor {

        private List<string> aliases = new List<string>();
        private bool firstRun = true;
        private int counter = 0;
        private string prefix = "A";

        public void Visit(PlainSelect plainSelect) {
            firstRun = true;
            counter = 0;
            aliases.Clear();
            foreach (ISelectItem item in plainSelect.getSelectItems()) {
                item.Accept(this);
            }
            firstRun = false;
            foreach (ISelectItem item in plainSelect.getSelectItems()) {
                item.Accept(this);
            }
        }

        public void Visit(SetOperationList setOpList) {
            foreach (PlainSelect select in setOpList.getPlainSelects()) {
                select.Accept(this);
            }
        }

        public void Visit(AllTableColumns allTableColumns) {
            throw new NotSupportedException("Not supported yet.");
        }

        public void Visit(SelectExpressionItem selectExpressionItem) {
            if (firstRun) {
                if (selectExpressionItem.getAlias() != null) {
                    aliases.Add(selectExpressionItem.getAlias().getName().ToUpper());
                }
            } else {
                if (selectExpressionItem.getAlias() == null) {

                    while (true) {
                        string alias = getNextAlias().ToUpper();
                        if (!aliases.Contains(alias)) {
                            aliases.Add(alias);
                            selectExpressionItem.setAlias(new Alias(alias));
                            break;
                        }
                    }
                }
            }
        }

        /**
	 * Calculate next alias name to use.
	 *
	 * @return
	 */
        protected string getNextAlias() {
            counter++;
            return prefix + counter;
        }

        /**
	 * Set alias prefix.
	 *
	 * @param prefix
	 */
        public void setPrefix(string prefix) {
            this.prefix = prefix;
        }

        public void Visit(WithItem withItem) {
            throw new NotSupportedException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
        }

        public void Visit(AllColumns allColumns) {
            throw new NotSupportedException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
        }
    }
}
