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

    public class Table
    {
        private readonly Data[] data;
        private readonly int height;
        private readonly int width;

        /// <summary>Initializes a new instance of the <see cref="Table"/> class.</summary>
        /// <param name="height">The height of the table.</param>
        /// <param name="width">The width of the table.</param>
        public Table(int height, int width)
        {
            this.width = width;
            this.height = height;
            this.data = new Data[width * height];
        }

        /// <summary>Gets the <see cref="Data"/> at the specified <paramref name="x"/> and <paramref name="y"/> coordinate.</summary>
        /// <param name="x">The x coordinate of the table to retrieve.</param>
        /// <param name="y">The y coordinate of the table to retrieve.</param>
        /// <returns>The data located at the <paramref name="x"/> and <paramref name="y"/> coordinates.</returns>
        public Data this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0)
                    return null;

                if (x >= this.width || y >= this.height)
                    return null;

                return this.data[MapCoordsToIndex(x, y)];
            }
        }

        /// <summary>Moves the contents of the table cell defined by the <paramref name="bearing"/> 1 position forward.</summary>
        /// <param name="bearing">The Position and Orientation of the table square to move.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Move(Bearing bearing)
        {
            var newBearing = bearing.Move();

            if (!ValidatePosition(newBearing.Position))
                return Status<Table>.Ok(this);

            data[MapCoordsToIndex(newBearing.Position.X, newBearing.Position.Y)] = data[MapCoordsToIndex(bearing.Position.X, bearing.Position.Y)];
            data[MapCoordsToIndex(bearing.Position.X, bearing.Position.Y)] = null;

            return Status<Table>.Ok(this);
        }

        /// <summary>Places the specified robot.</summary>
        /// <param name="robot">The robot.</param>
        /// <param name="bearing">The bearing.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Place(Robot robot, Bearing bearing)
        {
            if (!ValidatePosition(bearing.Position))
                return Status<Table>.Ok(this);

            data[MapCoordsToIndex(bearing.Position.X, bearing.Position.Y)] = new Data
            {
                Robot = robot,
                Orientation = bearing.Orientation
            };

            return Status<Table>.Ok(this);
        }

        /// <summary>Map the 2d coords to the 1d data array index.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>System.Int32.</returns>
        private int MapCoordsToIndex(int x, int y)
        {
            return x + y * width;
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

        public record Data
        {
            public Robot Robot;
            public Orientation Orientation;
        }
    }
}