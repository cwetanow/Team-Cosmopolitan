using System;
using Factory.Common.Contracts;

namespace Factory.Common
{
    public class UserMessageWriter: IUserMessageWriter
    {
        public void Show(string message)
        {
            Console.WriteLine(message);
        }
    }
}