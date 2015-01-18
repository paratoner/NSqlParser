/**
 *
 * @author Paratoner
 */
namespace NSqlParser.Expression
{
    public class Alias
    {

        private string name;
        private bool useAs = true;

        public Alias(string name)
        {
            this.name = name;
        }

        public Alias(string name, bool useAs)
        {
            this.name = name;
            this.useAs = useAs;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public bool isUseAs()
        {
            return useAs;
        }

        public void setUseAs(bool useAs)
        {
            this.useAs = useAs;
        }


        public override string ToString()
        {
            return (useAs ? " AS " : " ") + name;
        }
    }
}
