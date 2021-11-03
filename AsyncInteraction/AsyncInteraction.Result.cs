namespace AsyncInteraction
{
  using System;

  /// <summary>
  /// AsyncInteractionResult
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class AsyncInteractionResult<T> where T : class
  {
    public readonly bool IsConfirmed;
    
    public readonly T Result;

    public AsyncInteractionResult(bool isConfirmed, T result)
    {
      IsConfirmed = isConfirmed;
      Result = result ?? throw new ArgumentNullException(nameof(result));
    }
  }
}