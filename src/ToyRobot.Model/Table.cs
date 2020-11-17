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
    using System.Collections.Generic;

    public class Table
    {
        private readonly Dictionary<Guid, Data> data;

        private readonly int height;
        private readonly int width;

        /// <summary>Initializes a new instance of the <see cref="Table"/> class.</summary>
        /// <param name="height">The height of the table.</param>
        /// <param name="width">The width of the table.</param>
        public Table(int height, int width)
        {
            this.width = width;
            this.height = height;

            this.data = new Dictionary<Guid, Data>();
        }

        /// <summary>Gets the <see cref="Data"/> for the specified robot id.</summary>
        /// <param name="x">The x coordinate of the table to retrieve.</param>
        /// <param name="y">The y coordinate of the table to retrieve.</param>
        /// <returns>The data for the specified robot id.</returns>
        public Data this[Guid id]
        {
            get
            {
                return this.data.ContainsKey(id)
                    ? this.data[id]
                    : null;
            }
        }

        /// <summary>Moves the robot 1 position forward.</summary>
        /// <param name="bearing">The Position and Orientation of the table square to move.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Move(Robot robot)
        {
            if (!this.data.ContainsKey(robot.Id))
                return Status<Table>.Error("Robot cannot be moved", this);

            var data = this.data[robot.Id];
            var newBearing = data.Bearing.Move();

            if (!ValidatePosition(newBearing.Position))
                return Status<Table>.Ok(this);

            this.data[robot.Id] = new Data
            {
                Robot = robot,
                Bearing = newBearing
            };

            return Status<Table>.Ok(this);
        }

        /// <summary>Places the specified <paramref name="robot"/>.</summary>
        /// <param name="robot">The robot.</param>
        /// <param name="bearing">The bearing.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Place(Robot robot, Bearing bearing)
        {
            if (!ValidatePosition(bearing.Position))
                return Status<Table>.Ok(this);

            this.data[robot.Id] = new Data
            {
                Robot = robot,
                Bearing = bearing
            };

            return Status<Table>.Ok(this);
        }

        /// <summary>Turns the specified <paramref name="robot"/>.</summary>
        /// <param name="robot">The robot.</param>
        /// <param name="bearing">The bearing.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Turn(Robot robot, Direction direction)
        {
            var data = this.data[robot.Id];
            var newBearing = data.Bearing.Turn(direction);

            this.data[robot.Id] = new Data
            {
                Robot = robot,
                Bearing = newBearing
            };

            return Status<Table>.Ok(this);
        }

        /// <summary>Ensure the position falls inside the table.</summary>
        /// <param name="position">The position.</param>
        /// <returns>Status.</returns>
        private Status ValidatePosition(Position position)
        {
            if (position.X < 0 || position.X >= this.width)
                return Status.Error("Index out of Bounds");

            if (position.Y < 0 || position.Y >= this.height)
                return Status.Error("Index out of Bounds");

            return Status.Ok();
        }

        /// <summary>Internal table structure</summary>
        /// <value>The data.</value>
        public record Data
        {
            public Robot Robot;
            public Bearing Bearing;
        }
    }
}