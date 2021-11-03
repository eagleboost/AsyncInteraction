namespace AsyncInteraction.Tasks
{
  using System;
  using System.Threading.Tasks;

  public readonly struct DispatchTaskAwaiterHelper
  {
    private readonly Task _task;
    private readonly IDispatcherWaiter _dispatcher;
    
    public DispatchTaskAwaiterHelper(Task task, IDispatcherWaiter dispatcher)
    {
      _task = task;
      _dispatcher = dispatcher;
    }
    
    public bool IsCompleted => _task.IsCompleted && _dispatcher.CheckAccess();

    public void UnsafeOnCompleted(Action continuation)
    {
      var tmp = this;
      _task.ContinueWith(t => tmp._dispatcher.WaitAsync().OnCompleted(continuation));
    }
    
    public void GetResult()
    {
      VerifyException();
    }
    
    public T GetResult<T>()
    {
      VerifyException();
      
      return ((Task<T>) _task).Result;
    }
    
    private void VerifyException()
    {
      if (!_task.IsCompleted)
      {
        throw new InvalidOperationException("Task is unexpectedly not completed");
      }
      
      if (_task.Exception != null)
      {
        throw _task.Exception;
      }
    }
  }
}