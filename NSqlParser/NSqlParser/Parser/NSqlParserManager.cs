using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Statement;

namespace NSqlParser.Parser
{
    public class NSqlParserManager:INSqlParser
    {
        public IStatement Parse(System.IO.StreamReader statementReader)
        {
            //CCJSqlParser parser = new CCJSqlParser(statementReader);
            //try
            //{
            //    return parser.Statement();
            //}
            //catch (Exception ex)
            //{
            //    throw new JSQLParserException(ex);
            //}
            //todo: 
            return null;
        }
    }
}
