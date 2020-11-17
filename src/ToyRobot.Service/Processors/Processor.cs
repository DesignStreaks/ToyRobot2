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

namespace ToyRobot.Service.Processors
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ToyRobot.Model;
    using ToyRobot.Service.Commands;

    public class Processor
    {
        private readonly TextWriter output;

        /// <summary>Initializes a new instance of the <see cref="Processor"/> class.</summary>
        /// <param name="output">StdOut : The output device where any text based commands are written to.</param>
        public Processor(TextWriter output)
        {
            this.output = output;
        }

        /// <summary>Processes all <paramref name="commands"/> over the <paramref name="table"/>.</summary>
        /// <param name="table">The table.</param>
        /// <param name="commands">The list of commands to execute.</param>
        /// <returns>The updated table after all commands have been processed.</returns>
        public Table Process(Table table, List<object> commands)
        {
            var newTable = table.Copy();

            if (commands is null)
                return newTable;

            foreach (var command in commands)
            {
                switch (command)
                {
                    case ICommand<string>:
                        var statusString = (command as ICommand<string>).Execute(newTable);
                        if (statusString)
                        {
                            if (output.ToString().Length != 0)
                                output.WriteLine();
                            this.output.Write(statusString.Data);
                        }
                        break;

                    case ICommand<Table>:
                        var statusTable = (command as ICommand<Table>).Execute(newTable);
                        if (statusTable)
                            newTable = statusTable.Data;
                        break;
                }
            }

            return newTable;
        }
    }
}