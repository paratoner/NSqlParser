using System.Collections.Generic;
using NSqlParser.Expression;
using NSqlParser.Schema;
/**
 * A join clause
 */

namespace NSqlParser.Statement.Select
{
    public class Join {

        private bool outer = false;
        private bool right = false;
        private bool left = false;
        private bool natural = false;
        private bool full = false;
        private bool inner = false;
        private bool simple = false;
        private bool cross = false;
        private IFromItem rightItem;
        private IExpression onExpression;
        private List<Column> usingColumns;

        /**
	 * Whether is a tab1,tab2 join
	 *
	 * @return true if is a "tab1,tab2" join
	 */
        public bool isSimple() {
            return simple;
        }

        public void setSimple(bool b) {
            simple = b;
        }

        /**
	 * Whether is a "INNER" join
	 *
	 * @return true if is a "INNER" join
	 */
        public bool isInner() {
            return inner;
        }

        public void setInner(bool b) {
            inner = b;
        }

        /**
	 * Whether is a "OUTER" join
	 *
	 * @return true if is a "OUTER" join
	 */
        public bool isOuter() {
            return outer;
        }

        public void setOuter(bool b) {
            outer = b;
        }

        /**
	 * Whether is a "LEFT" join
	 *
	 * @return true if is a "LEFT" join
	 */
        public bool isLeft() {
            return left;
        }

        public void setLeft(bool b) {
            left = b;
        }

        /**
	 * Whether is a "RIGHT" join
	 *
	 * @return true if is a "RIGHT" join
	 */
        public bool isRight() {
            return right;
        }

        public void setRight(bool b) {
            right = b;
        }

        /**
	 * Whether is a "NATURAL" join
	 *
	 * @return true if is a "NATURAL" join
	 */
        public bool isNatural() {
            return natural;
        }

        public void setNatural(bool b) {
            natural = b;
        }

        /**
	 * Whether is a "FULL" join
	 *
	 * @return true if is a "FULL" join
	 */
        public bool isFull() {
            return full;
        }

        public void setFull(bool b) {
            full = b;
        }

        public bool isCross() {
            return cross;
        }

        public void setCross(bool cross) {
            this.cross = cross;
        }

        /**
	 * Returns the right item of the join
	 */
        public IFromItem getRightItem() {
            return rightItem;
        }

        public void setRightItem(IFromItem item) {
            rightItem = item;
        }

        /**
	 * Returns the "ON" expression (if any)
	 */
        public IExpression getOnExpression() {
            return onExpression;
        }

        public void setOnExpression(IExpression expression) {
            onExpression = expression;
        }

        /**
	 * Returns the "USING" list of {@link net.sf.jsqlparser.schema.Column}s (if
	 * any)
	 */
        public List<Column> getUsingColumns() {
            return usingColumns;
        }

        public void setUsingColumns(List<Column> list) {
            usingColumns = list;
        }

        public override string ToString() {
            if (isSimple()) {
                return "" + rightItem;
            } else {
                string type = "";

                if (isRight()) {
                    type += "RIGHT ";
                } else if (isNatural()) {
                    type += "NATURAL ";
                } else if (isFull()) {
                    type += "FULL ";
                } else if (isLeft()) {
                    type += "LEFT ";
                } else if (isCross()) {
                    type += "CROSS ";
                }

                if (isOuter()) {
                    type += "OUTER ";
                } else if (isInner()) {
                    type += "INNER ";
                }

                return type + "JOIN " + rightItem + ((onExpression != null) ? " ON " + onExpression + "" : "")
                       + PlainSelect.getFormatedList(usingColumns, "USING", true, true);
            }

        }
    }
}
