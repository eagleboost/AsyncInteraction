namespace AsyncInteraction.ComponentModel
{
  using System.ComponentModel;
  using System.Runtime.CompilerServices;

  public class Notifiable : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}