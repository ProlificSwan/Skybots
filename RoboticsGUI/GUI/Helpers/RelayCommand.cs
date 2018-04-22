using System;
using System.Windows.Input;

namespace Robotics.GUI.Helpers
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute = null;
        readonly Func<object, bool> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when <see cref="Execute"/> is called on the command.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<object> execute) : this(execute, null) { }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when <see cref="Execute"/> is called on the command.</param>
        /// <param name="canExecute">The execution status logic. This can be <see langword="null"/> to just hook up an <see cref="Execute"/> delegate.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <see langword="null"/></exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        ///     Fires when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null"/>.</param>
        /// <returns>
        ///     <see langword="true"/> if this command can be executed; otherwise, <see langword="false"/> .
        /// </returns>
        public bool CanExecute(object parameter) => _canExecute?.Invoke((object)parameter) ?? true;
        
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null"/>.</param>
        public void Execute(object parameter) => _execute((object)parameter);

        #endregion
    }
}
