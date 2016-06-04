using System;
using System.Windows.Input;

namespace TestWork.PL.Infrastructure
{
    /// <summary>
    /// Реализация интерфейса ICommand
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Создает новый экземпляр класса RelayCommand с методом, который будет выполняться
        /// </summary>
        /// <param name="execute">Выполняемый метод</param>
        public RelayCommand(Action<object> execute) : this(execute, null) { }


        /// <summary>
        /// Создает новый экземпляр класса RelayCommand с методом, который будет выполняться,
        ///  и методом, определяющим возможность выполнения комманды
        /// </summary>
        /// <param name="execute">Выполняемый метод</param>
        /// <param name="canExecute">Метод проверки условия выполнения</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}