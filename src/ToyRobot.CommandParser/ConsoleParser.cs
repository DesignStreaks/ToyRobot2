/*
 * DESIGNSTREAKS CONFIDENTIAL
 * __________________
 *
 *  Copyright © DesignStreaks - 2010 - 2020
 *  All Rights Reserved.
 *
 * NOTICE:  All information contained herein is, and remains
 * the property of DesignStreaks and its suppliers, if any.
 * The intellectual and technical concepts contained
 * herein are proprietary to DesignStreaks and its suppliers and may
 * be covered by Australian, U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from DesignStreaks.
 */

namespace ToyRobot.CommandParser
{
    using System;
    using System.Collections.Generic;
    using ToyRobot.Model;
    using ToyRobot.Service.Commands;

    public class ConsoleParser : Parser, ICommandParser
    {
        public List<object> GetCommands()
        {
            var commands = new List<object>();

            CommandTypes userCommand;
            Robot robot = null;

            do
            {
                Console.CursorTop = 15;
                Console.CursorLeft = 3;

                userCommand = ConsoleEx.ReadEnum<CommandTypes>("Enter Command", padding: 20);
                var userCommandString = userCommand.ToString();

                if (userCommand == CommandTypes.Place)
                {
                    Console.CursorLeft = 3;
                    var arguments = ConsoleEx.ReadInput("Enter Place Parameters (x, y, orientation):");
                    userCommandString += $" {arguments}";
                    robot = new Robot();
                }

                if (userCommand == CommandTypes.Block)
                {
                    Console.CursorLeft = 3;
                    var arguments = ConsoleEx.ReadInput("Enter Block Parameters (x, y):");
                    userCommandString += $" {arguments}";
                }

                var commandArgs = ParseLine(userCommandString);
                var command = CreateCommand(userCommand, commandArgs, robot);

                commands.Add(command);                

                ConsoleEx.ClearLine(15, 3, Console.WindowWidth - 2);
                ConsoleEx.ClearLine(16, 3, Console.WindowWidth - 2);

                Console.CursorTop = Console.WindowHeight - 3;
                ConsoleEx.Write(3, "Command Count: ", ConsoleColor.Gray);
                ConsoleEx.Write(20, commands.Count.ToString(), ConsoleColor.DarkGray);
            }
            while (userCommand != CommandTypes.Report);

            return commands;
        }
    }
}