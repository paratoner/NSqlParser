
/**
 * One of the parts of a "WITH" clause of a "SELECT" statement
 */

using System.Collections.Generic;

namespace NSqlParser.Statement.Select
{
    public class WithItem : ISelectBody
    {

        private string name;
        private List<ISelectItem> withItemList;
        private ISelectBody selectBody;

        /**
     * The name of this WITH item (for example, "myWITH" in "WITH myWITH AS
     * (SELECT A,B,C))"
     *
     * @return the name of this WITH
     */
        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        /**
     * The {@link SelectBody} of this WITH item is the part after the "AS"
     * keyword
     *
     * @return {@link SelectBody} of this WITH item
     */
        public ISelectBody getSelectBody()
        {
            return selectBody;
        }

        public void setSelectBody(ISelectBody selectBody)
        {
            this.selectBody = selectBody;
        }

        /**
     * The {@link SelectItem}s in this WITH (for example the A,B,C in "WITH
     * mywith (A,B,C) AS ...")
     *
     * @return a list of {@link SelectItem}s
     */
        public List<ISelectItem> getWithItemList()
        {
            return withItemList;
        }

        public void setWithItemList(List<ISelectItem> withItemList)
        {
            this.withItemList = withItemList;
        }

        public override string ToString()
        {
            return name + ((withItemList != null) ? " " + PlainSelect.getStringList(withItemList, true, true) : "")
                   + " AS (" + selectBody + ")";
        }

        public void Accept(ISelectVisitor Visitor)
        {
            Visitor.Visit(this);
        }
    }
}
