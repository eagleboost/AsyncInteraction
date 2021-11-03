namespace AsyncInteraction.Tasks
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using System.Windows.Threading;

  /// <summary>
  /// DispatcherTaskScheduler
  /// </summary>
  public class DispatcherTaskScheduler : TaskScheduler
  {
    private readonly Dispatcher _dispatcher;

    public DispatcherTaskScheduler(Dispatcher waiter)
    {
      _dispatcher = waiter;
    }

    #region Overrides
    public override int MaximumConcurrencyLevel => 1;
    
    protected override IEnumerable<Task> GetScheduledTasks()
    {
      return null;
    }

    protected override void QueueTask(Task task)
    {
      _dispatcher.BeginInvoke(() => TryExecuteTask(task));
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
      return _dispatcher.CheckAccess() && TryExecuteTask(task);
    }
    #endregion Overrides
  }
}