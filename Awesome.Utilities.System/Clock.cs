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
        private static bool IsPaused { get; set; }

        /// <summary>
        /// Gets the now.
        /// </summary>
        public static DateTime Now
        {
            get { return IsPaused ? now.Value : DateTime.Now; }
        }

        /// <summary>
        /// Returns a DateTime representing the current date and time. The
        /// resolution of the returned value depends on the system timer. For Windows NT 3.5 and later the timer resolution is approximately 10ms, 
        /// for Windows NT 3.1 it is approximately 16ms, and for Windows 95 and 98 it is approximately 55ms. 
        /// </summary>
        public static DateTime UtcNow
        {
            get { return IsPaused ? utcNow.Value : DateTime.UtcNow; }
        }

        /// <summary>
        /// Returns a DateTime representing the current date and time in UTC format. The
        /// resolution of the returned value depends on the system timer. For Windows NT 3.5 and later the timer resolution is approximately 10ms, 
        /// for Windows NT 3.1 it is approximately 16ms, and for Windows 95 and 98 it is approximately 55ms. 
        /// </summary>
        public static IDisposable Pause()
        {
            IsPaused = true;
            now = DateTime.Now;
            utcNow = DateTime.UtcNow;
            return new DisposableAction(() => IsPaused = false);
        }

        private class DisposableAction : IDisposable
        {
            private readonly Action action;

            public DisposableAction(Action action)
            {
                this.action = action;
            }
            
            public void Dispose()
            {
                action.Invoke();
            }
        }
    }
}
