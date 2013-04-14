// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataEventArgs.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ZTG.WPF.Dashboard.Shared.Localization.WPF
{
    /// <summary>
    /// Generic arguments class to pass to event handlers that need to receive data.
    /// </summary>
    /// <typeparam name="TData">The type of data to pass.</typeparam>
    public class DataEventArgs<TData> : EventArgs
    {
        private readonly TData _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataEventArgs&lt;TData&gt;"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public DataEventArgs(TData value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the information related to the event.
        /// </summary>
        /// <value>Information related to the event.</value>
        public TData Value
        {
            get { return _value; }
        }
    }
}