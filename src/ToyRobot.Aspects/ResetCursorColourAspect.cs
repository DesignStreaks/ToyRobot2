// *
// * DESIGNSTREAKS CONFIDENTIAL
// * __________________
// *
// *  Copyright © Design Streaks - 2010 - 2020
// *  All Rights Reserved.
// *
// * NOTICE:  All information contained herein is, and remains
// * the property of DesignStreaks and its suppliers, if any.
// * The intellectual and technical concepts contained
// * herein are proprietary to DesignStreaks and its suppliers and may
// * be covered by Australian, U.S. and Foreign Patents,
// * patents in process, and are protected by trade secret or copyright law.
// * Dissemination of this information or reproduction of this material
// * is strictly forbidden unless prior written permission is obtained
// * from DesignStreaks.

namespace ToyRobot.Aspects
{
    using System;
    using System.Diagnostics;

    /// <summary>Aspect used to reset the Console cursor colour properties.</summary>
    /// <remarks>
    ///   Method Boundary aspect to ensure the following Console properties are reset when the method completes.
    ///   <list type="bullet">
    ///     <item><description>Cursor Foreground Colour</description></item>
    ///     <item><description>Cursor Background Colour</description></item>
    ///   </list>
    ///   <note type="Information">This class was originally written as a PostSharp aspect. Due to licensing restrictions, PostSharp has been
    ///   removed so methods in this class are to be called manually (as opposed to being defined as a method attribute and weaved in by PostSharp)</note>
    /// </remarks>
    [Serializable]
    public class ResetCursorColourAspect
    {
        private ConsoleColor backgroundColour;
        private ConsoleColor foregroundColour;

        /// <summary>Method executed <b>before</b> the body of methods to which this aspect is applied.</summary>
        [DebuggerHidden]
        public void OnEntry()
        {
            this.foregroundColour = Console.ForegroundColor;
            this.backgroundColour = Console.BackgroundColor;
        }

        /// <summary>
        ///   Method executed <b>after</b> the body of methods to which this aspect is applied, even when the method exists with an exception
        ///   (this method is invoked from the <c>finally</c> block).
        /// </summary>
        [DebuggerHidden]
        public void OnExit()
        {
            Console.ForegroundColor = this.foregroundColour;
            Console.BackgroundColor = this.backgroundColour;
        }
    }
}
