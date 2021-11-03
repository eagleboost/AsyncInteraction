namespace AsyncInteraction
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Windows;

  /// <summary>
  /// AsyncInteractionImpl
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract partial class AsyncInteraction<T> where T : class
  {
    private class AsyncInteractionImpl : IAsyncInteractionState<T>
    {
      #region Declarations
      private readonly TaskCompletionSource<AsyncInteractionResult<T>> _tcs = new();
      private readonly Action<Window, IAsyncInteractionState<T>> _startAction;
      private readonly TaskScheduler _taskScheduler;
      #endregion Declarations

      public AsyncInteractionImpl(AsyncInteractionArgs args, TaskScheduler taskScheduler, Action<Window, IAsyncInteractionState<T>> startAction)
      {
        Args = args;
        _startAction = startAction;
        _taskScheduler = taskScheduler;
      }

      #region IAsyncInteraction
      public Task<AsyncInteractionResult<T>> StartAsync()
      {
        Task.Factory.StartNew(StartCore, CancellationToken.None, TaskCreationOptions.None, _taskScheduler);
        return _tcs.Task;
      }
      #endregion IAsyncInteraction

      #region IAsyncInteractionState
      public AsyncInteractionArgs Args { get; }
      
      public void SetResult(T state, bool confirmed)
      {
        var result = new AsyncInteractionResult<T>(confirmed, state);
        _tcs.TrySetResult(result);
      }

      public void SetException(Exception ex)
      {
        _tcs.TrySetException(ex);
      }

      public void SetCanceled()
      {
        _tcs.TrySetCanceled();
      }
      #endregion IAsyncInteractionState
      
      #region Private Methods
      private void StartCore()
      {
        try
        {
          var owner = GetOwnerWindow();
          _startAction(owner, this);
        }
        catch (TaskCanceledException)
        {
          SetCanceled();
        }
        catch (Exception ex)
        {
          SetException(ex);
        }
      }
      #endregion Private Methods
    }
  }
}