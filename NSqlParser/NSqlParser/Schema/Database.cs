using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSqlParser.Schema
{
    public sealed class Database : IMultiPartName
    {
        private Server server;
        private string databaseName;

        public Database(string databaseName)
        {
            setDatabaseName(databaseName);
        }

        public Database(Server server, string databaseName)
        {
            setServer(server);
            setDatabaseName(databaseName);
        }

        public Server getServer()
        {
            return server;
        }

        public void setServer(Server server)
        {
            this.server = server;
        }

        public string getDatabaseName()
        {
            return databaseName;
        }

        public void setDatabaseName(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public string GetFullyQualifiedName()
        {
            string fqn = "";

            if (server != null)
            {
                fqn += server.GetFullyQualifiedName();
            }
            if (!string.IsNullOrEmpty(fqn))
            {
                fqn += ".";
            }

            if (databaseName != null)
            {
                fqn += databaseName;
            }

            return fqn;
        }

        public override string ToString()
        {
            return GetFullyQualifiedName();
        }
    }
}
