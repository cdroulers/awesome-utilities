using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     A disposable action for sweet using () goodness
    /// </summary>
    public class DisposableAction : IDisposable
    {
        private readonly Action action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableAction"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DisposableAction(Action action)
        {
            Validate.Is.Not.Null(action, "action");
            this.action = action;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            action.Invoke();
        }
    }
}
