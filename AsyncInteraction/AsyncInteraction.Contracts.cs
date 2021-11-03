namespace AsyncInteraction
{
  using System.Threading.Tasks;

  /// <summary>
  /// IAsyncInteraction
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IAsyncInteraction<T> where T : class
  {
    Task<AsyncInteractionResult<T>> StartAsync(AsyncInteractionArgs args);
  }
}