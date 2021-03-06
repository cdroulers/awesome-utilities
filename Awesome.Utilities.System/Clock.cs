﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Using Clock.Now instead of DateTime.Now in your projects
    /// allows for a greater degree of testability.
    /// </summary>
    public static class Clock
    {
        private static DateTime? now;
        private static DateTime? utcNow;

        /// <summary>
        /// Gets a value indicating whether this instance is paused.
        /// </summary>
        public static bool IsPaused { get; private set; }

        /// <summary>
        /// Gets a DateTime representing the current date and time. The
        /// resolution of the returned value depends on the system timer. For Windows NT 3.5 and later the timer resolution is approximately 10ms, 
        /// for Windows NT 3.1 it is approximately 16ms, and for Windows 95 and 98 it is approximately 55ms. 
        /// </summary>
        public static DateTime Now
        {
            get { return Clock.ApplyTransform(Clock.IsPaused ? now.Value : DateTime.Now); }
        }

        /// <summary>
        /// Gets a DateTime representing the current date and time in UTC format. The
        /// resolution of the returned value depends on the system timer. For Windows NT 3.5 and later the timer resolution is approximately 10ms, 
        /// for Windows NT 3.1 it is approximately 16ms, and for Windows 95 and 98 it is approximately 55ms. 
        /// </summary>
        public static DateTime UtcNow
        {
            get { return Clock.ApplyTransform(Clock.IsPaused ? utcNow.Value : DateTime.UtcNow); }
        }

        /// <summary>
        /// Pauses the clock on the current time.
        /// </summary>
        /// <returns>An IDisposable object that will resume the clock on dispose.</returns>
        public static IDisposable Pause()
        {
            return Clock.Pause(DateTime.UtcNow);
        }

        /// <summary>
        /// Pauses the clock on the specified time.
        /// </summary>
        /// <param name="utcNowDate">The dateTime at which we want to pause..</param>
        /// <returns>
        /// An IDisposable object that will resume the clock on dispose.
        /// </returns>
        public static IDisposable Pause(DateTime utcNowDate)
        {
            Clock.IsPaused = true;
            Clock.now = utcNowDate.ToLocalTime();
            Clock.utcNow = utcNowDate;
            return new DisposableAction(() => Clock.IsPaused = false);
        }

        private static DateTime ApplyTransform(DateTime dateTime)
        {
            return Clock.Transform != null ? Clock.Transform(dateTime) : dateTime;
        }

        /// <summary>
        /// Gets or sets a function that will be applied to all returned DateTime values.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        public static Func<DateTime, DateTime> Transform { get; set; }
    }
}
