using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Engine;

namespace jab.console
{
    public class TestEventListener: ITestEventListener
    {
        public void OnTestEvent(string report)
        {
            Console.WriteLine(report);
        }
    }
}
