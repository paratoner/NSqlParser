using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Expression;
using NSqlParser.Statement.Select;

namespace NSqlParser.Schema
{
    public class Table : IFromItem, IMultiPartName
    {

        private Database database;
        private string schemaName;
        private string name;

        private Alias alias;
        private Pivot pivot;

        public Table()
        {
        }

        public Table(string name)
        {
            this.name = name;
        }

        public Table(string schemaName, string name)
        {
            this.schemaName = schemaName;
            this.name = name;
        }

        public Table(Database database, string schemaName, string name)
        {
            this.database = database;
            this.schemaName = schemaName;
            this.name = name;
        }

        public Database getDatabase()
        {
            return database;
        }

        public void setDatabase(Database database)
        {
            this.database = database;
        }

        public string getSchemaName()
        {
            return schemaName;
        }

        public void setSchemaName(string strings)
        {
            schemaName = strings;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string stringn)
        {
            name = stringn;
        }


        public Alias getAlias()
        {
            return alias;
        }


        public void setAlias(Alias alias)
        {
            this.alias = alias;
        }


        public string GetFullyQualifiedName()
        {
            string fqn = "";

            if (database != null)
            {
                fqn += database.GetFullyQualifiedName();
            }
            if (!string.IsNullOrEmpty(fqn))
            {
                fqn += ".";
            }

            if (schemaName != null)
            {
                fqn += schemaName;
            }
            if (!string.IsNullOrEmpty(fqn))
            {
                fqn += ".";
            }

            if (name != null)
            {
                fqn += name;
            }

            return fqn;
        }


        public void Accept(IFromItemVisitor fromItemVisitor)
        {
            fromItemVisitor.Visit(this);
        }

        public void Accept(IIntoTableVisitor intoTableVisitor)
        {
            intoTableVisitor.Visit(this);
        }


        public Pivot getPivot()
        {
            return pivot;
        }


        public void setPivot(Pivot pivot)
        {
            this.pivot = pivot;
        }


        public override string ToString()
        {
            return GetFullyQualifiedName()
                   + ((pivot != null) ? " " + pivot : "")
                   + ((alias != null) ? alias.ToString() : "");
        }
    }
}
