namespace ToyRobot.CommandParser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface ICommandParser
    {
        List<object> GetCommands();
    }
}
