using System.IO;
using System.Text;

namespace Frends.Community.CombineXML
{
    public class UTF8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
