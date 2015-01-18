using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Statement;

namespace NSqlParser.Parser
{
    public interface INSqlParser
    {
        IStatement Parse(StreamReader statementReader);
    }
}
