using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Practika1.RCommand
{
    public class RealyyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Action<object?> Action { get; set; }
        public Predicate<object?> Predicate { get; set; }

        public RealyyCommand(Action<object?> action, Predicate<object?> predicate = null)
        {
            Action = action;
            Predicate = predicate;
        }

        public bool CanExecute(object? parameter)
        {
           if (Predicate is null) { return true; }
           return Predicate.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            Action.Invoke(parameter);
        }
    }
}
