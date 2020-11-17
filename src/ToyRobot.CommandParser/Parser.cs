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
    using System.Linq;
    using ToyRobot.Model;
    using ToyRobot.Service.Commands;

    public class Parser
    {
        protected string[] ParseLine(string line)
        {
            var parts = line.Split(' ');

            CommandTypes commandType;
            if (!Enum.TryParse(parts[0], true, out commandType))
                return null;

            return parts;
        }

        protected object CreateCommand(CommandTypes commandType, string[] commandArgs, Robot robot)
        {
            switch (commandType)
            {
                case CommandTypes.Place:
                    Bearing bearing;

                    if (ValidatePlaceCommand(commandArgs, out bearing))
                        return new PlaceCommand(robot, bearing);

                    break;

                case CommandTypes.Move:
                    if (ValidateMoveCommand(commandArgs))
                        return new MoveCommand(robot);

                    break;

                case CommandTypes.Left:
                    if (ValidateLeftTurnCommand(commandArgs))
                        return new TurnCommand(robot, Direction.Left);

                    break;

                case CommandTypes.Right:
                    if (ValidateRightTurnCommand(commandArgs))
                        return new TurnCommand(robot, Direction.Right);

                    break;

                case CommandTypes.Report:
                    if (ValidateReportCommand(commandArgs))
                        return new ReportCommand(robot);

                    break;
            }

            return null;
        }

        private static Status ValidateLeftTurnCommand(string[] parts)
        {
            return (parts.Length != 1 && !string.IsNullOrWhiteSpace(string.Join("", parts, 1, parts.Length - 1)))
                ? Status.Error("Left command invalid argument count")
                : Status.Ok();
        }

        private static Status ValidateMoveCommand(string[] parts)
        {
            return (parts.Length != 1 && !string.IsNullOrWhiteSpace(string.Join("", parts, 1, parts.Length - 1)))
                ? Status.Error("Move command invalid argument count")
                : Status.Ok();
        }

        private static Status ValidatePlaceCommand(string[] parts, out Bearing bearing)
        {
            bearing = null;

            if (parts.Length == 1)
                return Status.Error("Place command requires arguments");

            // Some hocus-pocus to remove any possible superfluous white spaces around command parameters.
            var arguments = string.Join("", parts.Skip(1).Where(i => !string.IsNullOrWhiteSpace(i))).Split(',');

            if (arguments.Length != 3)
                return Status.Error("Place command requires 3 arguments");

            if (!int.TryParse(arguments[0], out int x))
                return Status.Error("Place command invalid 'x' argument");

            if (!int.TryParse(arguments[1], out int y))
                return Status.Error("Place command invalid 'y' argument");

            if (!Enum.TryParse(arguments[2], true, out Orientation orientation))
                return Status.Error("Place command invalid 'orientation' argument");

            bearing = new Bearing
            {
                Position = new Position(x, y),
                Orientation = orientation
            };

            return Status.Ok();
        }

        private static Status ValidateReportCommand(string[] parts)
        {
            return (parts.Length != 1 && !string.IsNullOrWhiteSpace(string.Join("", parts, 1, parts.Length - 1)))
                ? Status.Error("Report command invalid argument count")
                : Status.Ok();
        }

        private static Status ValidateRightTurnCommand(string[] parts)
        {
            return (parts.Length != 1 && !string.IsNullOrWhiteSpace(string.Join("", parts, 1, parts.Length - 1)))
                ? Status.Error("Right command invalid argument count")
                : Status.Ok();
        }
    }
}