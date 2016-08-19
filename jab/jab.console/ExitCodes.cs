using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jab.console
{
    /// <summary>
    /// Return codes.
    /// </summary>
    public class ExitCodes
    {
        /// <summary>
        /// The program exited successfull.
        /// </summary>
        public const int Success = 0;

        /// <summary>
        /// One or more command line arguments were invalid.
        /// </summary>
        public const int BadArgument = 1;

        /// <summary>
        /// One or more tests failed.
        /// </summary>
        public const int TestFailed = 2;
    }
}
