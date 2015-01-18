
/**
 * Single Set-Operation (name). Placeholder for one specific set operation, e.g.
 * union, intersect.
 *
 * @author Paratoner
 */

namespace NSqlParser.Statement.Select
{
    public abstract class SetOperation
    {

        private SetOperationType type;

        public SetOperation(SetOperationType type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}
