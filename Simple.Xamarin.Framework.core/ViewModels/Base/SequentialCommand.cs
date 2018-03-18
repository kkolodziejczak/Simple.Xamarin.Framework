using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simple.Xamarin.Framework.core
{

    /// <summary>
    /// <see cref="SequentialCommand"/> prevents attempts to invoke multiple commands at the same time.
    /// They will be 
    /// </summary>
    public class SequentialCommand : ICommand
    {
        /// <summary>
        /// Function to execute
        /// </summary>
        private Func<Task> _funcJob;

        /// <summary>
        /// Action to execute
        /// </summary>
        private Action _job;

        /// <summary>
        /// A semaphore to lock the semaphore list
        /// </summary>
        private static SemaphoreSlim SelfLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = (s,e) => { };

        public SequentialCommand(Func<Task> job)
        {
            _funcJob = job;
        }

        public SequentialCommand(Action job)
        {
            _job = job;
        }

        public bool CanExecute(object parameter)
        {
            // Always execute
            return true;
        }

        /// <summary>
        /// Execute only one task at the time
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object parameter)
        {
            // If Job is already running return
            if (SelfLock.CurrentCount == 0)
                return;

            // Lock semaphore
            await SelfLock.WaitAsync();

            try
            {
                // Perform the job
                if(_job != null)
                    _job();
                else
                    await _funcJob();
            }
            finally
            {
                // Release the semaphore
                SelfLock.Release();
            }
        }
    }

}
