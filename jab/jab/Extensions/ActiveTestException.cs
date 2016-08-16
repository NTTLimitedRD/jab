using System;

namespace jab.Extensions
{
    public class ActiveTestException :Exception
    {
        public ActiveTestException(string reason) :
            base(reason)
        { }
    }
}
