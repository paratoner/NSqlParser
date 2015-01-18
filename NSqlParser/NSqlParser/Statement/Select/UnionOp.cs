/**
 *
 * @author Paratoner
 */

namespace NSqlParser.Statement.Select
{
    public class UnionOp : SetOperation {

        private bool distinct;
        private bool all;

        public UnionOp() :base(SetOperationType.UNION)
        {
        }

        public bool isAll() {
            return all;
        }

        public void setAll(bool all) {
            this.all = all;
        }

        public bool isDistinct() {
            return distinct;
        }

        public void setDistinct(bool distinct) {
            this.distinct = distinct;
        }

        public override string ToString() {
            string allDistinct = "";
            if (isAll()) {
                allDistinct = " ALL";
            } else if (isDistinct()) {
                allDistinct = " DISTINCT";
            }
            return base.ToString() + allDistinct;
        }
    }
}
