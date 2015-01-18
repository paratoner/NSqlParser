using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSqlParser.Schema
{
    public sealed class Server : IMultiPartName
    {
        public static Regex SERVER_PATTERN = new Regex("\\[([^\\]]+?)(?:\\\\([^\\]]+))?\\]");

        private string serverName;
        private string instanceName;


        public Server(string serverAndInstanceName)
        {
            if (serverAndInstanceName != null)
            {
                Match matcher = SERVER_PATTERN.Match(serverAndInstanceName);
                if (!matcher.Success)
                {
                    throw new ArgumentException(string.Format("{0} is not a valid database reference", serverAndInstanceName));
                }
                setServerName(matcher.Groups[1].Value);
                setInstanceName(matcher.Groups[2].Value);
            }
        }

        public Server(string serverName, string instanceName)
        {
            setServerName(serverName);
            setInstanceName(instanceName);
        }

        public string getServerName()
        {
            return serverName;
        }

        public void setServerName(string serverName)
        {
            this.serverName = serverName;
        }

        public string getInstanceName()
        {
            return instanceName;
        }

        public void setInstanceName(string instanceName)
        {
            this.instanceName = instanceName;
        }

        public string GetFullyQualifiedName()
        {
            if (serverName != null && !string.IsNullOrEmpty(serverName) && instanceName != null && !string.IsNullOrEmpty(instanceName))
            {
                return string.Format("[{0}\\{1}]", serverName, instanceName);
            }
            else if (serverName != null && !string.IsNullOrEmpty(serverName))
            {
                return string.Format("[{0}]", serverName);
            }
            else
            {
                return "";
            }
        }

        public override string ToString()
        {
            return GetFullyQualifiedName();
        }
    }
}
