namespace AsyncInteraction
{
  using System.Threading.Tasks;

  /// <summary>
  /// AsyncInteractionExt
  /// </summary>
  public static class AsyncInteractionExt
  {
    public static Task<AsyncInteractionResult<T>> StartAsync<T>(this IAsyncInteraction<T> asyncInteraction, T data) where T : class
    {
      return asyncInteraction.StartAsync(new AsyncInteractionArgs(data));
    }
  }
}