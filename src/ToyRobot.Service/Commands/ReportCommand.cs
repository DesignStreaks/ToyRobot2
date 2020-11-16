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
    /// <seealso cref="ToyRobot.Service.Commands.ICommand" />
    /// <see cref="ToyRobot.Service.Commands.ICommand" />
    public class ReportCommand : ICommand<string>
    {
        /// <summary>Executes the command on the scene.</summary>
        /// <param name="scene">The scene the command is to be executed over.</param>
        /// <returns>The status of the command execution along with the updated scene.</returns>
        public Status<string> Execute(Scene scene)
        {
            var data = scene.Table[scene.Robot.Id];

            return data is not null
                ? Status<string>.Ok($"{data.Bearing.Position.X},{data.Bearing.Position.Y},{data.Bearing.Orientation}")
                : Status<string>.Ok(string.Empty);
        }
    }
}