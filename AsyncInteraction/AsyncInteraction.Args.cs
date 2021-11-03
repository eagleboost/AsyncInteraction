namespace AsyncInteraction
{
  using System.Collections.ObjectModel;

  /// <summary>
  /// AsyncInteractionResult
  /// </summary>
  public sealed class AsyncInteractionArgs : ReadOnlyCollection<object>
  {
    public AsyncInteractionArgs(params object[] args) : base(args)
    {
    }
    
    public T GetValue<T>()
    {
      foreach (var item in this)
      {
        if (item is T result)
        {
          return result;
        }
      }

      return default;
    }
  }
}