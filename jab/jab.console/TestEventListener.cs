using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Engine;

namespace jab.console
{
    public class TestEventListener: ITestEventListener
    {
        /// <summary>
        /// XML Node name.
        /// </summary>
        public const string TestCaseNodeName = "test-case";

        /// <summary>
        /// Name attribute name.
        /// </summary>
        public const string NameAttributeName = "name";

        /// <summary>
        /// Result attribute name.
        /// </summary>
        public const string ResultAttributeName = "result";

        /// <summary>
        /// XPath to get the message from a failed test.
        /// </summary>
        public const string MessageXPath = "failure/message";

        /// <summary>
        /// Called when progress is made during a test run.
        /// </summary>
        /// <param name="report"></param>
        public void OnTestEvent(string report)
        {
            XDocument xDocument;
            XElement root;

            xDocument = XDocument.Parse(report);
            root = xDocument.Root;
            if (root != null && root.Name == TestCaseNodeName)
            {
                OnTestCase(
                    root.Attribute(XName.Get(NameAttributeName)).Value,
                    (TestResult) Enum.Parse(typeof(TestResult), root.Attribute(XName.Get(ResultAttributeName)).Value, true),
                    root.XPathSelectElement(MessageXPath)?.Value);
            }
        }

        private void OnTestCase(string name, TestResult result, string message)
        {
            if (message != null)
            {
                Console.Out.WriteLine($"{name}: {message}");
            }
        }
    }
}
