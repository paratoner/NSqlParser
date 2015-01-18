/**
 * Values of an "INSERT" statement (for example a SELECT or a list of
 * expressions)
 */
namespace NSqlParser.Expression.Operators.Relational
{
    public interface IItemsList {

        void Accept(IItemsListVisitor itemsListVisitor);
    }
}
