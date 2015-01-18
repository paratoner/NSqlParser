/**
 *
 * @author Paratoner
 */

namespace NSqlParser.Statement.Select
{
    public class MinusOp : SetOperation
    {

        public MinusOp()
            : base(SetOperationType.MINUS)
        {
        }
    }
}
