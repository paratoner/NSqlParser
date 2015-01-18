
using System.Text;

namespace NSqlParser.Expression
{
    public class WindowRange {

        private WindowOffset start;
        private WindowOffset end;

        public WindowOffset getEnd() {
            return end;
        }

        public void setEnd(WindowOffset end) {
            this.end = end;
        }

        public WindowOffset getStart() {
            return start;
        }

        public void setStart(WindowOffset start) {
            this.start = start;
        }

    
        public override string ToString() {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(" BETWEEN");
            buffer.Append(start);
            buffer.Append(" AND");
            buffer.Append(end);
            return buffer.ToString();
        }
    }
}
