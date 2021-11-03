namespace AsyncInteractionApp.Login
{
  using AsyncInteraction.ComponentModel;

  public class LoginViewModel : Notifiable, ILoginViewModel
  {
    public string UserName { get; set; }
    
    public string Password { get; set; }
  }
}