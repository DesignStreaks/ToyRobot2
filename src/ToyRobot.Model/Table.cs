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

                return this.data[x + y * this.width];
            }
        }

        /// <summary>Places the specified robot.</summary>
        /// <param name="robot">The robot.</param>
        /// <param name="bearing">The bearing.</param>
        /// <returns>The status of the command execution along with the updated table.</returns>
        public Status<Table> Place(Robot robot, Bearing bearing)
        {
            if (bearing.Position.X < 0 || bearing.Position.X >= this.width)
                return Status<Table>.Ok(this);

            if (bearing.Position.Y < 0 || bearing.Position.Y >= this.height)
                return Status<Table>.Ok(this);

            data[bearing.Position.X + bearing.Position.Y * this.width] = new Data
            {
                Robot = robot,
                Orientation = bearing.Orientation
            };

            return Status<Table>.Ok(this);
        }

        public record Data
        {
            public Robot Robot;
            public Orientation Orientation;
        }
    }
}