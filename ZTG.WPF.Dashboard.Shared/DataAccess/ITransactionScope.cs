// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITransactionScope.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ZTG.WPF.Dashboard.Shared.DataAccess
{
    /// <summary>
    /// Custom transaction scope to abstract System.TransactionScope, NHibernate MSTransactionScope, or other into
    /// independant transaction context.
    /// This allows the "using() { .Commit(); }" semantic.
    /// </summary>
    public interface ITransactionScope : IDisposable
    {
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        void Rollback();
    }
}
