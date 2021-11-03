namespace AsyncInteractionApp.Login
{
  using System.Windows;
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for LoginWindow.xaml
  /// </summary>
  public partial class LoginWindow
  {
    public LoginWindow()
    {
      InitializeComponent();
    }

    private void OkButtonClick(object sender, RoutedEventArgs e)
    {
      UserNameBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
      PasswordBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
      
      DialogResult = true;
      Close();
    }
    
    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close();
    }
  }
}