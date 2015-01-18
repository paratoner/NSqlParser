using System.Text;

namespace NSqlParser.Expression
{
    public class WindowElement {

        public enum Type {

            ROWS,
            RANGE
        }

        private Type type;
        private WindowOffset offset;
        private WindowRange range;

        public Type getType() {
            return type;
        }

        public void setType(Type type) {
            this.type = type;
        }

        public WindowOffset getOffset() {
            return offset;
        }

        public void setOffset(WindowOffset offset) {
            this.offset = offset;
        }

        public WindowRange getRange() {
            return range;
        }

        public void setRange(WindowRange range) {
            this.range = range;
        }

    
        public override string ToString() {
            StringBuilder buffer = new StringBuilder(type.ToString());
        
            if (offset != null) {
                buffer.Append(offset.ToString());
            } else if (range != null) {
                buffer.Append(range.ToString());
            }

            return buffer.ToString();
        }
    }
}
