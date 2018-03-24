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
        /// Function to execute with parameter
        /// </summary>
        private Func<object, Task> _funcJobWithParam;

        /// <summary>
        /// Action to execute
        /// </summary>
        private Action _job;

        /// <summary>
        /// Action to execute with parameter
        /// </summary>
        private Action<object> _jobWithParam;

        /// <summary>
        /// Key that allows to override restrictions
        /// </summary>
        private bool _rightToRunAlways;

        /// <summary>
        /// A semaphore to lock the semaphore list
        /// </summary>
        private static SemaphoreSlim SelfLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = (s, e) => { };

        public SequentialCommand(Func<Task> job, bool runAlways = false)
        {
            _funcJob = job;
            _rightToRunAlways = runAlways;
        }

        public SequentialCommand(Func<object, Task> job, bool runAlways = false)
        {
            _funcJobWithParam = job;
            _rightToRunAlways = runAlways;
        }

        public SequentialCommand(Action job, bool runAlways = false)
        {
            _job = job;
            _rightToRunAlways = runAlways;
        }

        public SequentialCommand(Action<object> job, bool runAlways = false)
        {
            _jobWithParam = job;
            _rightToRunAlways = runAlways;
        }

        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute only one task at the time
        /// </summary>
        /// <param name="parameter"></param>
        public async void Execute(object parameter)
        {
            if (_rightToRunAlways == true)
            {
                await DoTheJob(parameter);
            }
            else
            {
                // Check if Job is already running
                if (SelfLock.CurrentCount == 0)
                    // if so then return
                    return;

                // else lock semaphore
                await SelfLock.WaitAsync();

                try
                {
                    // and do the job
                    await DoTheJob(parameter);
                }
                finally
                {
                    // Release the semaphore
                    SelfLock.Release();
                }
            }
        }

        /// <summary>
        /// Perform the job of the command
        /// </summary>
        /// <returns></returns>
        private async Task DoTheJob(object parameter)
        {
            _job?.Invoke();
            _jobWithParam?.Invoke(parameter);
            if (_funcJob != null)
                await _funcJob();
            if (_funcJobWithParam != null)
                await _funcJobWithParam(parameter);
        }
    }

}
