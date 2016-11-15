using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace apivk
{
    public class XmlReader
    {
        private string _data;
        public XmlReader(string data)
        {
            _data = data;
        }
        private string readField(string field, string data)
        {
            if (data == null)
                data = _data;
            if (!data.Contains($"<{field}>") || !data.Contains($"</{field}>"))
                return string.Empty;
            return data.Substring($"<{field}>", $"</{field}>");
        }
        public string GetData (string field, string data = null)
        {
            return readField(field, data);
        }
        public int GetIntData (string field, string data = null)
        {
            return int.Parse(readField(field, data));
        }
        public bool GetBoolData (string field, string data = null)
        {
            string result = readField(field, data);
            return (result == "1") ? true : false;
        }
    }
}
