using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using NUnit.Engine;

namespace jab.console
{
    /// <summary>
    /// Called after a test case has executed.
    /// </summary>
    /// <param name="name">
    /// The test name.
    /// </param>
    /// <param name="result">
    /// The test result.
    /// </param>
    /// <param name="message">
    /// The message (usually only supplied on a failure).
    /// </param>
    public delegate void TestCaseResultHandler(string name, TestResult result, string message);

    /// <summary>
    /// Fires events for NUnit test run progress.
    /// </summary>
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
        /// <param name="report">
        /// The details passed from NUnit.
        /// </param>
        public void OnTestEvent(string report)
        {
            XDocument xDocument;
            XElement root;

            xDocument = XDocument.Parse(report);
            root = xDocument.Root;
            if (root != null && root.Name == TestCaseNodeName)
            {
                OnTestCaseResult?.Invoke(
                    root.Attribute(XName.Get(NameAttributeName)).Value,
                    (TestResult) Enum.Parse(typeof (TestResult), root.Attribute(XName.Get(ResultAttributeName)).Value, true),
                    root.XPathSelectElement(MessageXPath)?.Value);
            }
        }

        /// <summary>
        /// Called after a test case has executed.
        /// </summary>
        public event TestCaseResultHandler OnTestCaseResult;
    }
}
