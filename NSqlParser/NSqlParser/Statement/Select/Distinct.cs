
/**
 * A DISTINCT [ON (expression, ...)] clause
 */

using System.Collections.Generic;

namespace NSqlParser.Statement.Select
{
    public class Distinct
    {

        private List<ISelectItem> onSelectItems;

        /**
     * A list of {@link SelectItem}s expressions, as in "select DISTINCT ON
     * (a,b,c) a,b FROM..."
     *
     * @return a list of {@link SelectItem}s expressions
     */
        public List<ISelectItem> getOnSelectItems()
        {
            return onSelectItems;
        }

        public void setOnSelectItems(List<ISelectItem> list)
        {
            onSelectItems = list;
        }

        public override string ToString()
        {
            string sql = "DISTINCT";

            if (onSelectItems != null && onSelectItems.Count > 0)
            {
                sql += " ON (" + PlainSelect.getStringList(onSelectItems) + ")";
            }

            return sql;
        }
    }
}
