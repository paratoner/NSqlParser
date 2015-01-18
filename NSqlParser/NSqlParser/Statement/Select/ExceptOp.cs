namespace NSqlParser.Statement.Select
{
    public class ExceptOp : SetOperation
    {

        public ExceptOp()
            : base(SetOperationType.EXCEPT)
        {
        }
    }
}
