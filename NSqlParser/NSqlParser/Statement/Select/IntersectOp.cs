/**
 *
 * @author Paratoner
 */

namespace NSqlParser.Statement.Select
{
    public class IntersectOp : SetOperation
    {
        public IntersectOp():base(SetOperationType.INTERSECT)
        {
        }
    }
}
