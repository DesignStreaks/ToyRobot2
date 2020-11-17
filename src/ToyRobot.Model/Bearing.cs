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

namespace ToyRobot.Model
{
    using System;

    /// <summary>Representation of a Position and Orientation</summary>
    public record Bearing
    {
        public Orientation Orientation { get; init; }

        public Position Position { get; init; }

        /// <summary>Returns a new Bearing with the position incremented by 1 unit in the diretion of the orientation.</summary>
        public Bearing Move()
        {
            return Orientation switch
            {
                Orientation.North => this with { Position = this.Position with { Y = this.Position.Y + 1 } },
                Orientation.South => this with { Position = this.Position with { Y = this.Position.Y - 1 } },
                Orientation.East => this with { Position = this.Position with { X = this.Position.X + 1 } },
                Orientation.West => this with { Position = this.Position with { X = this.Position.X - 1 } },
                _ => this.Copy()
            };
        }

        /// <summary>Returns a new Bearing with the Orientation re-aligned 90° left or right.</summary>
        public Bearing Turn(Direction direction)
        {
            return Orientation switch
            {
                Orientation.North => this with
                {
                    Orientation = direction == Direction.Left
                        ? Orientation.West
                        : Orientation.East
                },
                Orientation.South => this with
                {
                    Orientation = direction == Direction.Left
                        ? Orientation.East
                        : Orientation.West
                },
                Orientation.East => this with
                {
                    Orientation = direction == Direction.Left
                        ? Orientation.North
                        : Orientation.South
                },
                Orientation.West => this with
                {
                    Orientation = direction == Direction.Left
                        ? Orientation.South
                        : Orientation.North
                },
                _ => this.Copy()
            };
        }
    }
}