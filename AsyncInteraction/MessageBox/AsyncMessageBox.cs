namespace AsyncInteraction.MessageBox
{
  using System.Windows;

  /// <summary>
  /// AsyncMessageBox
  /// </summary>
  public class AsyncMessageBox : AsyncInteraction<MessageBoxData>
  {
    protected override void StartImpl(Window owner, IAsyncInteractionState<MessageBoxData> state)
    {
      var data = state.Args.GetValue<MessageBoxData>();
      var result = owner != null
        ? MessageBox.Show(owner, data.Text, data.Header, data.Button, data.Image)
        : MessageBox.Show(data.Text, data.Header, data.Button, data.Image);
      
      var confirmed = result is MessageBoxResult.Yes or MessageBoxResult.OK;
      data.IsCanceled = result == MessageBoxResult.Cancel;
      state.SetResult(data, confirmed);
    }
  }
}