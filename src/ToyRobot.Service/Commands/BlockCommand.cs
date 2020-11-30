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

namespace ToyRobot.Service.Commands
{
    using System;
    using ToyRobot.Model;

    /// <summary>Command to add a blocked location to the table.</summary>
    /// <seealso cref="ToyRobot.Service.Commands.ICommand{ToyRobot.Model.Table}" />
    /// Implements the <see cref="ToyRobot.Service.Commands.ICommand{ToyRobot.Model.Table}" />
    public class BlockCommand : ICommand<Table>
    {
        private readonly Position position;

        /// <summary>Initializes a new instance of the <see cref="BlockCommand"/> class.</summary>
        /// <param name="position">The position on the table to block.</param>
        public BlockCommand(Position position)
        {
            this.position = position;
        }

        /// <summary>Executes the command on the table.</summary>
        /// <param name="table">The table the command is to be executed over.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Execute(Table table)
        {
            return table.Block(position);
        }
    }
}