namespace AsyncInteraction.Commands
{
  using System;
  using System.Linq.Expressions;
  using System.Windows.Input;

  public class DelegateCommand<T> : ICommand
  {
    #region Declarations
    private readonly Action<T> _execute;
    private readonly Func<bool> _canExecute;
    #endregion Declarations

    #region ctors
    public DelegateCommand(Action<T> execute, Expression<Func<bool>> canExecute)
    {
      _execute = execute;
      _canExecute = canExecute.Compile();
    }
    #endregion ctors

    public bool CanExecute(object parameter)
    {
      return _canExecute();
    }

    public void Execute(object parameter)
    {
      _execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged;

    public void Invalidate()
    {
      var handler = CanExecuteChanged;
      if (handler != null)
      {
        handler(this, EventArgs.Empty);
      }
    }
  }
}