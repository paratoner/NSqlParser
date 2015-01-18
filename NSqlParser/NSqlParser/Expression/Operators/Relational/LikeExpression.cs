namespace NSqlParser.Expression.Operators.Relational
{
    public class LikeExpression : BinaryExpression
    {

        private bool not = false;
        private string escape = null;


        public override bool isNot()
        {
            return not;
        }

        public void setNot(bool b)
        {
            not = b;
        }


        public override void Accept(IExpressionVisitor expressionVisitor)
        {
            expressionVisitor.Visit(this);
        }


        public override string getStringExpression()
        {
            return ((not) ? "NOT " : "") + "LIKE";
        }


        public override string ToString()
        {
            string retval = base.ToString();
            if (escape != null)
            {
                retval += " ESCAPE " + "'" + escape + "'";
            }

            return retval;
        }

        public string getEscape()
        {
            return escape;
        }

        public void setEscape(string escape)
        {
            this.escape = escape;
        }
    }
}
