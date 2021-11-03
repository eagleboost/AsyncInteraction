namespace AsyncInteraction
{
  using System.Threading;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Threading;
  using global::AsyncInteraction.Extensions;
  using global::AsyncInteraction.Tasks;

  /// <summary>
  /// AsyncInteraction
  /// </summary>
  public abstract class AsyncInteraction
  {
    #region Statics
    private static TaskScheduler _appScheduler;
    #endregion Statics

    #region Declarations
    private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
    private TaskScheduler _taskScheduler;
    #endregion Declarations

    #region Public Properties
    public TaskScheduler TaskScheduler
    {
      get => _taskScheduler ??= GetTaskScheduler();
      internal set => _taskScheduler = value;
    }
    #endregion Public Properties
    
    #region Private Properties
    private static TaskScheduler AppScheduler => _appScheduler ??= new DispatcherTaskScheduler(Application.Current.Dispatcher);
    #endregion Private Properties

    #region Private Methods
    private TaskScheduler GetTaskScheduler()
    {
      if (_dispatcher == Application.Current.Dispatcher || _dispatcher.Thread.GetApartmentState() != ApartmentState.STA)
      {
        return AppScheduler;
      }
      
      return new DispatcherTaskScheduler(_dispatcher);
    }
    #endregion Private Methods
    
    #region Protected
    protected static Window GetOwnerWindow()
    {
      return Dispatcher.CurrentDispatcher.GetActiveWindow();
    }
    #endregion Private Methods
  }
}