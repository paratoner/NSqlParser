using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlParser.Statement.Select
{
    /**
     * list of set operations.
     */
    public enum SetOperationType
    {

        INTERSECT,
        EXCEPT,
        MINUS,
        UNION
    }
}
