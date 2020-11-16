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

    /// <summary>Command to place the robot on the table.</summary>
    /// <seealso cref="ToyRobot.Service.Commands.ICommand" />
    /// <see cref="ToyRobot.Service.Commands.ICommand" />
    public class PlaceCommand : ICommand<Table>
    {
        private readonly Robot robot;
        private readonly Bearing bearing;

        /// <summary>Initializes a new instance of the <see cref="PlaceCommand" /> class.</summary>
        /// <param name="robot">The robot to execute the command for.</param>
        /// <param name="bearing">The bearing of the robot.</param>
        public PlaceCommand(Robot robot, Bearing bearing)
        {
            this.robot = robot;
            this.bearing = bearing;
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

            if (bearing is null)
                return Status<Table>.Error("Bearing not defined", table);

            var newTable = table.Copy();

            var status = newTable.Place(robot, bearing);

            return status
                ? Status<Table>.Ok(newTable)
                : Status<Table>.Error(status.Message, table);
        }
    }
}