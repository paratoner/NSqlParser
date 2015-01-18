using System;
using System.Collections.Generic;
using System.Text;
/**
 * A database set operation. This operation consists of a list of plainSelects
 * connected by set operations (UNION,INTERSECT,MINUS,EXCEPT). All these
 * operations have the same priority.
 *
 * @author Paratoner
 */

namespace NSqlParser.Statement.Select
{
    public class SetOperationList : ISelectBody {

        private List<PlainSelect> plainSelects;
        private List<SetOperation> operations;
        private List<OrderByElement> orderByElements;
        private Limit limit;
        private Offset offset;
        private Fetch fetch;

        public void Accept(ISelectVisitor selectVisitor) {
            selectVisitor.Visit(this);
        }

        public List<OrderByElement> getOrderByElements() {
            return orderByElements;
        }

        public List<PlainSelect> getPlainSelects() {
            return plainSelects;
        }

        public List<SetOperation> getOperations() {
            return operations;
        }

        public void setOrderByElements(List<OrderByElement> orderByElements) {
            this.orderByElements = orderByElements;
        }

        public void setOpsAndSelects(List<PlainSelect> select, List<SetOperation> ops) {
            plainSelects = select;
            operations = ops;

            if (select.Count - 1 != ops.Count) {
                throw new ArgumentException("list sizes are not valid");
            }
        }

        public Limit getLimit() {
            return limit;
        }

        public void setLimit(Limit limit) {
            this.limit = limit;
        }

        public Offset getOffset() {
            return offset;
        }

        public void setOffset(Offset offset) {
            this.offset = offset;
        }

        public Fetch getFetch() {
            return fetch;
        }

        public void setFetch(Fetch fetch) {
            this.fetch = fetch;
        }

        public override string ToString() {
            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < plainSelects.Count; i++) {
                if (i != 0) {
                    buffer.Append(" ").Append(operations[i - 1].ToString()).Append(" ");
                }
                buffer.Append("(").Append(plainSelects[i].ToString()).Append(")");
            }

            if (orderByElements != null) {
                buffer.Append(PlainSelect.orderByToString(orderByElements));
            }
            if (limit != null) {
                buffer.Append(limit.ToString());
            }
            if (offset != null) {
                buffer.Append(offset.ToString());
            }
            if (fetch != null) {
                buffer.Append(fetch.ToString());
            }
            return buffer.ToString();
        }


    }
}
