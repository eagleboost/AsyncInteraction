namespace AsyncInteractionApp
{
  using System;
  using System.Collections.ObjectModel;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Input;
  using System.Windows.Threading;
  using AsyncInteraction;
  using AsyncInteraction.Commands;
  using AsyncInteraction.ComponentModel;
  using AsyncInteraction.Extensions;
  using AsyncInteraction.MessageBox;
  using AsyncInteraction.Tasks;
  using AsyncInteractionApp.Login;

  public class ViewModel : Notifiable
  {
    private readonly string _threadName;
    
    public ViewModel(string threadName)
    {
      _threadName = threadName;
      ClickCommand = new DelegateCommand<string>(HandleClick, () => true);
    }

    public IDispatcherWaiter DispatcherWaiter { get; set; }

    public IAsyncInteraction<MessageBoxData> MessageBox { get; set; }
    
    public IAsyncInteraction<ILoginViewModel> LoginInteraction { get; set; }

    public ICommand ClickCommand { get; }

    public ObservableCollection<string> UserChoices { get; } = new();

    private void HandleClick(string type)
    {
      if (type == "FromBackground")
      {
        Task.Run(() => HandleClickAsync(type, Thread.CurrentThread.ManagedThreadId.ToString()));
      }
      else if (type == "Login")
      {
        _ = LoginAsync();
      }
      else
      {
        _ = HandleClickAsync(type, _threadName); 
      }
    }

    private async Task HandleClickAsync(string type, string threadName)
    {
      try
      {
        Log($"[Thread: {threadName}][{type}] is invoked");
        var asyncResult = await MessageBox.StartAsync(CreateMessageBoxData(type)).ConfigureAwait(DispatcherWaiter);
        var guiThreadName = Dispatcher.CurrentDispatcher.GetThreadName();
        if (asyncResult.Result.IsCanceled)
        {
          Log($"[Thread: {guiThreadName}][{type}] Cancel");
          return;
        }

        var confirmed = asyncResult.IsConfirmed ? "Yes" : "No";
        Log($"[Thread: {guiThreadName}][{type}] {confirmed}");
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }
    
    private async Task LoginAsync()
    {
      try
      {
        Log($"[Thread: {_threadName}][LogIn] is invoked");
        var viewModel = new LoginViewModel();
        var asyncResult = await LoginInteraction.StartAsync(viewModel).ConfigureAwait(DispatcherWaiter);
        var guiThreadName = Dispatcher.CurrentDispatcher.GetThreadName();
        var confirmed = asyncResult.IsConfirmed ? "Yes" : "No";
        Log($"[Thread: {guiThreadName}][Login] {confirmed}, UserName: {viewModel.UserName}, Password: {viewModel.Password}");
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }
    
    private MessageBoxData CreateMessageBoxData(string type)
    {
      switch (type)
      {
        case "Information":
          return CreateInformation();
        case "Question":
          return CreateQuestion();
        default:
          return CreateCancel();
      }
    }
    
    private MessageBoxData CreateInformation()
    {
      return new MessageBoxData
      {
        Header = "Information",
        Text = "This is a message",
        Button = MessageBoxButton.OK,
        Image = MessageBoxImage.Information
      };
    }
    
    private MessageBoxData CreateQuestion()
    {
      return new MessageBoxData
      {
        Header = "Question",
        Text = "Do you wish to continue?",
        Button = MessageBoxButton.YesNo,
        Image = MessageBoxImage.Question
      };
    }
    
    private MessageBoxData CreateCancel()
    {
      return new MessageBoxData
      {
        Header = "Question",
        Text = "Do you wish to continue?",
        Button = MessageBoxButton.YesNoCancel,
        Image = MessageBoxImage.Question
      };
    }

    private void Log(string msg)
    {
      DispatcherWaiter.CheckedWaitAsync().OnCompleted(() => UserChoices.Add(msg));
    }
  }
}