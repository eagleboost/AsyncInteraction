namespace AsyncInteraction
{
  using System.Threading.Tasks;
  using System.Windows;

  /// <summary>
  /// AsyncInteractionWindow
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract partial class AsyncInteraction<T> : AsyncInteraction, IAsyncInteraction<T> where T : class
  {
    #region IAsyncInteraction
    public Task<AsyncInteractionResult<T>> StartAsync(AsyncInteractionArgs args)
    {
      var impl = new AsyncInteractionImpl(args, TaskScheduler, StartImpl);
      return impl.StartAsync();
    }
    #endregion IAsyncInteraction
    
    #region Virtuals
    protected abstract void StartImpl(Window owner, IAsyncInteractionState<T> state);
    #endregion Virtuals
  }
}