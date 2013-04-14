// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionScope.cs" company="Zühlke Engineering AG">
//   (c) by Zühlke Engineering AG 2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Transactions;

namespace ZTG.WPF.Dashboard.Shared.DataAccess
{
  /// <summary>
  /// Custom transaction implementation, wrapping EntityFramework transaction here (System.Transactions.Transaction) 
  /// </summary>
  public class TransactionScope : ITransactionScope
  {
    /// <summary>
    /// The wrapped transaction
    /// </summary>
    private readonly System.Transactions.TransactionScope _scope;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionScope"/> class.
    /// A new transaction is always created, even when a circumventing transaction already exists.
    /// </summary>
    public TransactionScope()
    {
      _scope = new System.Transactions.TransactionScope(TransactionScopeOption.RequiresNew);
    }

    #region ITransactionScope Members

    /// <summary>
    /// Commits the transaction.
    /// </summary>
    public void Commit()
    {
      _scope.Complete();
    }

    /// <summary>
    /// Rolls the transaction back.
    /// </summary>
    public void Rollback()
    {
      _scope.Dispose();
    }

    #endregion

    #region IDisposable Members

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _scope.Dispose();
      }
    }

    #endregion
  }
}
