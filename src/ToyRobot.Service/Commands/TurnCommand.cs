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

    /// <summary>Class TurnCommand.</summary>
    /// <seealso cref="ToyRobot.Service.Commands.ICommand{ToyRobot.Model.Table}" />
    /// Implements the <see cref="ToyRobot.Service.Commands.ICommand{ToyRobot.Model.Table}" />
    public class TurnCommand : ICommand<Table>
    {
        private readonly Direction direction;
        private readonly Robot robot;

        /// <summary>Initializes a new instance of the <see cref="MoveCommand" /> class.</summary>
        /// <param name="robot">The robot to execute the command for.</param>
        /// <param name="direction">The direction to turn the robot.</param>
        public TurnCommand(Robot robot, Direction direction)
        {
            this.robot = robot;
            this.direction = direction;
        }

        /// <summary>Executes the command on the table.</summary>
        /// <param name="table">The table the command is to be executed over.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Execute(Table table)
        {
            if (table is null)
                return Status<Table>.Error("Table not defined", table);

            if (robot is null)
                return Status<Table>.Error("Robot not defined", table);

            var newTable = table.Copy();

            var status = newTable.Turn(robot, direction);

            return status
                ? Status<Table>.Ok(status.Data)
                : Status<Table>.Error(status.Message, table);
        }
    }
}