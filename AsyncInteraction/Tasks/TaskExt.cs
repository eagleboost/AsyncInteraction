namespace AsyncInteraction.Tasks
{
  using System.Threading.Tasks;

  public static class TaskExt
  {
    public static DispatchTaskAwaiter ConfigureAwait(this Task task, IDispatcherWaiter dispatcher)
    {
      return new DispatchTaskAwaiter(task, dispatcher);
    }
    
    public static DispatchTaskAwaiter<T> ConfigureAwait<T>(this Task<T> task, IDispatcherWaiter dispatcher)
    {
      return new DispatchTaskAwaiter<T>(task, dispatcher);
    }
  }
}