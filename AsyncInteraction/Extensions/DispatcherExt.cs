namespace AsyncInteraction.Extensions
{
  using System.Linq;
  using System.Reflection;
  using System.Windows;
  using System.Windows.Threading;

  public static class DispatcherExt
  {
    public static Window GetActiveWindow(this Dispatcher dispatcher)
    {
      if (dispatcher == Application.Current.Dispatcher)
      {
        return GetAppActiveWindow();
      }

      return GetNonAppActiveWindow(dispatcher);
    }

    private static Window GetAppActiveWindow()
    {
      var activeWindow = Application.Current.Windows.Cast<Window>().LastOrDefault(i => i.IsActive);
      if (activeWindow == null)
      {
        activeWindow = Application.Current.MainWindow;
      }

      return activeWindow;
    }
    
    private static Window GetNonAppActiveWindow(Dispatcher dispatcher)
    {
      var nonAppWindowsProperty=typeof(Application).GetProperty("NonAppWindowsInternal", BindingFlags.Instance | BindingFlags.NonPublic);
      var nonAppWindows = nonAppWindowsProperty.GetValue(Application.Current) as WindowCollection;
      var candidates = nonAppWindows.Cast<Window>().Where(i => i.Dispatcher == dispatcher);
      var activeWindow = candidates.LastOrDefault(i => i.IsActive);
      if (activeWindow == null)
      {
        activeWindow = candidates.LastOrDefault();
      }

      return activeWindow;
    }

    public static string GetThreadName(this Dispatcher dispatcher)
    {
      var threadName = dispatcher == Application.Current.Dispatcher ? "Main GUI" : dispatcher.Thread.Name;
      if (threadName == null)
      {
        return dispatcher.Thread.ManagedThreadId.ToString();
      }

      return threadName;
    }
  }
}