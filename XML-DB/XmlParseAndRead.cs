using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace XML_DB
{
    public class XmlParseAndRead
    {
        private readonly XDocument _document;

        public XmlParseAndRead(String pathToFile)
        {
            _document = XDocument.Load(pathToFile);

            ReadTableName();
            ReadStructure();
        }

        public string ReadTableName()
        {
            IEnumerable<string> q = null;
            try
            {
                q = from p in _document.Descendants("table")
                    let xElement = p.Element("Name")
                    where xElement != null
                    select xElement.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " | " + ex.StackTrace);
            }

            return q != null ? q.First() : null;
        }

        public IEnumerable<IEnumerable<XElement>> ReadRecords()
        {
            IEnumerable<IEnumerable<XElement>> q = null;
            try
            {
                q = from p in _document.Descendants("Record")
                        select p.Elements();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " | " + ex.StackTrace);
            }
            
            return q;
        }

        public IEnumerable<IEnumerable<XElement>> ReadStructure()
        {
            IEnumerable<IEnumerable<XElement>> q = null;
            try
            {
                q = from p in _document.Descendants("Field")
                    select p.Elements();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " | " + ex.StackTrace);
            }

            return q;
        }
    }
}
