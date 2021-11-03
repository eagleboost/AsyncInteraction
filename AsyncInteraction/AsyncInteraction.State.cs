namespace AsyncInteraction
{
  using System;
  using System.Threading.Tasks;

  /// <summary>
  /// IAsyncInteractionState
  /// </summary>
  public interface IAsyncInteractionState<in T>
  {
    AsyncInteractionArgs Args { get; }
    
    void SetResult(T state, bool isConfirmed);

    void SetException(Exception ex);

    void SetCanceled();
  }
}