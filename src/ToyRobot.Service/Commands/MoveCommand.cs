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

    /// <summary>Command to move the contents of a table cell.</summary>
    /// <seealso cref="ToyRobot.Service.Commands.ICommand" />
    /// <see cref="ToyRobot.Service.Commands.ICommand" />
    public class MoveCommand : ICommand
    {
        private readonly Bearing bearing;

        /// <summary>Initializes a new instance of the <see cref="MoveCommand"/> class.</summary>
        /// <param name="bearing">The bearing.</param>
        public MoveCommand(Bearing bearing)
        {
            this.bearing = bearing;
        }

        /// <summary>Executes the command on the scene.</summary>
        /// <param name="scene">The scene the command is to be executed over.</param>
        /// <returns>The status of the command execution along with the updated scene.</returns>
        public Status<Scene> Execute(Scene scene)
        {
            if (scene is null)
                return Status<Scene>.Error("Scene not defined", scene);

            if (scene.Table is null)
                return Status<Scene>.Error("Table not defined", scene);

            if (scene.Robot is null)
                return Status<Scene>.Error("Robot not defined", scene);

            var newTable = scene.Table.Copy();

            var status = newTable.Move(bearing);

            return status
                ? Status<Scene>.Ok(scene with { Table = status.Data })
                : Status<Scene>.Error(status.Message, scene);
        }
    }
}