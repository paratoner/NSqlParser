using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.Table
{
    public class NamedConstraint : Index {
        public override string ToString() {
            return (getName()!=null?"CONSTRAINT " + getName() + " ":"") 
                   +  getType() + " " + PlainSelect.getStringList(getColumnsNames(), true, true);
        }
    }
}
