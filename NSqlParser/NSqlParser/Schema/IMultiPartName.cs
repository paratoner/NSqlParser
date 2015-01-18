using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlParser.Schema
{
    public interface IMultiPartName
    {
        string GetFullyQualifiedName();
    }
}
