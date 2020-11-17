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

    /// <summary>Command to report on the robot on the table.</summary>
    /// <seealso cref="ToyRobot.Service.Commands.ICommand`1" />
    /// Implements the <see cref="ToyRobot.Service.Commands.ICommand`1" />
    public class ReportCommand : ICommand<string>
    {
        private readonly Robot robot;

        /// <summary>Initializes a new instance of the <see cref="ReportCommand"/> class.</summary>
        /// <param name="robot">The robot to execute the command for.</param>
        public ReportCommand(Robot robot)
        {
            this.robot = robot;
        }

        /// <summary>Executes the command on the table.</summary>
        /// <param name="table">The table the command is to be executed over.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<string> Execute(Table table)
        {
            if (table is null)
                return Status<string>.Error("Table not defined", string.Empty);

            if (robot is null)
                return Status<string>.Error("Robot not defined", string.Empty);

            var data = table[robot.Id];

            return data is not null
                ? Status<string>.Ok($"{data.Bearing.Position.X},{data.Bearing.Position.Y},{data.Bearing.Orientation}")
                : Status<string>.Ok(string.Empty);
        }
    }
}