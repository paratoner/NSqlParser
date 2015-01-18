using NSqlParser.Expression;

namespace NSqlParser.Statement.Select
{
    public class FunctionItem
    {

        private Function function;
        private Alias alias;

        public Alias getAlias()
        {
            return alias;
        }

        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }

        public Function getFunction()
        {
            return function;
        }

        public void setFunction(Function function)
        {
            this.function = function;
        }

        public override string ToString()
        {
            return function + ((alias != null) ? alias.ToString() : "");
        }
    }
}
