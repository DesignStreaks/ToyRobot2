// *
// * DESIGNSTREAKS CONFIDENTIAL
// * __________________ *
// *  Copyright © Design Streaks - 2010 - 2020
// *  All Rights Reserved. *
// * NOTICE: All information contained herein is, and remains
// * the property of DesignStreaks and its suppliers, if any.
// * The intellectual and technical concepts contained
// * herein are proprietary to DesignStreaks and its suppliers and may
// * be covered by Australian, U.S. and Foreign Patents,
// * patents in process, and are protected by trade secret or copyright law.
// * Dissemination of this information or reproduction of this material
// * is strictly forbidden unless prior written permission is obtained
// * from DesignStreaks.

namespace ToyRobot.Model
{
    using System;
    using System.Diagnostics;

    /// <summary>Status class used to return the status of an operation.</summary>
    public record Status
    {
        /// <summary>The state values for the <see cref="Status" /> class.</summary>
        public enum States
        {
            /// <summary>Ok state.</summary>
            Ok,

            /// <summary>Error state. <see cref="Message" /> property will contain the error message.</summary>
            Error
        }

        private readonly string message;

        /// <summary>Returns the status error message set when the status was generated.</summary>
        public string Message => string.IsNullOrWhiteSpace(this.message)
            ? this.State.ToString()
            : this.message;

        /// <summary>Returns the state value of the <see cref="Status" /> .</summary>
        public States State { get; }

        /// <summary>Initializes a new instance of the <see cref="Status" /> class.</summary>
        /// <param name="state">The state.</param>
        [DebuggerStepThrough]
        protected Status(States state)
        {
            this.State = state;
            this.message = string.Empty;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Status" /> class, automatically setting the <see cref="State" /> property to <see cref="States.Error" />.
        /// </summary>
        /// <param name="message">The error message to attach to the <see cref="States.Error" /> state.</param>
        [DebuggerStepThrough]
        protected Status(string message)
        {
            this.message = message;
            this.State = States.Error;
        }

        /// <summary>Creates a new <see cref="States.Error" /><see cref="Status" /> object with a custom <see cref="Message" />.</summary>
        /// <param name="message">The custom message to set for the <see cref="Status" />.</param>
        /// <returns>Returns an <see cref="States.Error" /><see cref="Status" /> object.</returns>
        [DebuggerStepThrough]
        public static Status Error(string message) => new Status(message);

        /// <summary>
        ///   Performs an implicit conversion from <see cref="Status" /> to <see cref="System.Boolean" />, converting a <see cref="Status" />
        ///   with a <see cref="State" /> value of <see cref="States.Ok" /> to true.
        /// </summary>
        /// <param name="status">The <see cref="Status" /> object to be converted.</param>
        /// <returns>The <c>true</c> if <see cref="State" /> is <see cref="States.Ok" />; <c>false</c> otherwise..</returns>
        [DebuggerStepThrough]
        public static implicit operator bool(Status status) => status.State == States.Ok;

        /// <summary>
        ///   Helper function to performs an implicit conversion from <see cref="Status" /> to <see cref="States" /> by simply returning the
        ///   <see cref="State" /> value.
        /// </summary>
        /// <param name="status">The <see cref="Status" /> object to be converted.</param>
        /// <returns>The value of the <see cref="State" /> property..</returns>
        [DebuggerStepThrough]
        public static implicit operator States(Status status) => status.State;

        /// <summary>Creates a new <see cref="States.Ok" /><see cref="Status" /> object.</summary>
        /// <returns>Returns an <see cref="States.Ok" /><see cref="Status" /> object.</returns>
        [DebuggerStepThrough]
        public static Status Ok() => new Status(States.Ok);
    }

    /// <summary>Status class containing a data payload used to return the status of an operation.</summary>
    /// <typeparam name="T">The data type of the data payload.</typeparam>
    /// <seealso cref="ToyRobot.Library.Status" />
    public record Status<T> : Status
    {
        /// <summary>The data payload of the this <see cref="Status{T}" /> instance.</summary>
        /// <value>The data.</value>
        public T Data { get; }

        /// <summary>Initializes a new instance of the <see cref="Status" /> class.</summary>
        /// <param name="state">The <see cref="Status.State" /> value to set the <see cref="Status{T}" /> to.</param>
        /// <param name="data">The data payload of the <see cref="Status{T}" />.</param>
        [DebuggerStepThrough]
        protected Status(States state, T data) : base(state)
        {
            this.Data = data;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Status{T}" /> class, automatically setting the <see cref="Status.State" /> to <see cref="Status.States.Error" />.
        /// </summary>
        /// <param name="message">The error message to attach to the <see cref="Status.States.Error" /> state.</param>
        /// <param name="data">The data payload of the <see cref="Status{T}" />.</param>
        [DebuggerStepThrough]
        protected Status(string message, T data) : base(message)
        {
            this.Data = data;
        }

        /// <summary>&gt;Creates a new <see cref="Status.States.Error" /><see cref="Status{T}" /> object.</summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data payload of the <see cref="Status{T}" />.</param>
        /// <returns>Returns an <see cref="Status.States.Error" /><see cref="Status{T}" /> object.</returns>
        [DebuggerStepThrough]
        public static Status<T> Error(string message, T data) => new Status<T>(message, data);

        /// <summary>Creates a new <see cref="Status.States.Ok" /><see cref="Status{T}" /> object.</summary>
        /// <param name="data">The data payload of the <see cref="Status{T}" />.</param>
        /// <returns>Returns an <see cref="Status.States.Ok" /><see cref="Status{T}" /> object.</returns>
        [DebuggerStepThrough]
        public static Status<T> Ok(T data) => new Status<T>(States.Ok, data);
    }
}