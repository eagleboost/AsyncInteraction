namespace AsyncInteraction.MessageBox
{
  using System.Windows;

  /// <summary>
  /// MessageBoxData
  /// </summary>
  public class MessageBoxData
  {
    public string Header { get; set; }
    
    public string Text { get; set; }

    public MessageBoxButton Button { get; set; }

    public MessageBoxImage Image { get; set; }
    
    public bool IsCanceled { get; set; }
  }
}