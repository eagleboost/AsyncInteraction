namespace AsyncInteraction
{
  using System.Windows;

  /// <summary>
  /// AsyncInteraction
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="TWindow"></typeparam>
  public class AsyncInteractionWindow<TWindow, T> : AsyncInteraction<T>
    where TWindow : Window, new()
    where T : class
  {
    protected sealed override void StartImpl(Window owner, IAsyncInteractionState<T> state)
    {
      var viewModel = state.Args.GetValue<T>();
      var window = new TWindow
      {
        DataContext = viewModel, 
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        Owner = owner
      };
      
      var result = window.ShowDialog();
      state.SetResult(viewModel, result.GetValueOrDefault(false));
    }
  }
}