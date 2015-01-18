using System.Collections.Generic;
using NSqlParser.Statement.Select;

namespace NSqlParser.Statement.Create.Table
{
    public class ColDataType
    {

        private string dataType;
        private List<string> argumentsstringList;
        private string characterSet;

        public List<string> getArgumentsstringList()
        {
            return argumentsstringList;
        }

        public string getDataType()
        {
            return dataType;
        }

        public void setArgumentsstringList(List<string> list)
        {
            argumentsstringList = list;
        }

        public void setDataType(string strings)
        {
            dataType = strings;
        }

        public string getCharacterSet()
        {
            return characterSet;
        }

        public void setCharacterSet(string characterSet)
        {
            this.characterSet = characterSet;
        }

        public override string ToString()
        {
            return dataType
                   + (argumentsstringList != null ? " " + PlainSelect.getStringList(argumentsstringList, true, true) : "")
                   + (characterSet != null ? " CHARACTER SET " + characterSet : "");
        }
    }
}
