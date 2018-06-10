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
        private readonly Func<Task> _funcJob;

        /// <summary>
        /// Function to execute with parameter
        /// </summary>
        private readonly Func<object, Task> _funcJobWithParam;

        /// <summary>
        /// Action to execute
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// Key that allows to override restrictions
        /// </summary>
        private readonly bool _rightToRunAlways;

        /// <summary>
        /// A semaphore to lock the <see cref="JobLock"/>
        /// </summary>
        private static readonly SemaphoreSlim Lock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// A semaphore to lock the job
        /// </summary>
        private static SemaphoreSlim JobLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Delay in milliseconds that will be awaited after job is done
        /// </summary>
        public double Delay { get; protected set; }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = (s, e) => { };

        public SequentialCommand(Func<Task> job, bool runAlways = false, double delay = 500)
        {
            _funcJob = job;
            _rightToRunAlways = runAlways;
            Delay = delay;
        }

        public SequentialCommand(Func<object, Task> job, bool runAlways = false, double delay = 500)
        {
            _funcJobWithParam = job;
            _rightToRunAlways = runAlways;
            Delay = delay;
        }

        public SequentialCommand(Action job, bool runAlways = false, double delay = 500)
        {
            _action = job;
            _rightToRunAlways = runAlways;
            Delay = delay;
        }

        /// <summary>
        /// Returns true if Command can execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) 
        {
            if (_rightToRunAlways == true)
            {
                return true;
            }
            try
            {
                Lock.Wait();
                return JobLock.CurrentCount != 0;
            }
            finally
            {
                Lock.Release();
            }
        }

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
                // Critical section to establish if Job can be executed
                try
                {
                    await Lock.WaitAsync();
                    // Check if Job is already running
                    if (JobLock.CurrentCount == 0)
                    {
                        // if so then return
                        return;
                    }
                    else
                    {
                        // else lock semaphore
                        await JobLock.WaitAsync();
                    }
                }
                finally
                {
                    Lock.Release();
                }

                try
                {
                    // and do the job
                    await DoTheJob(parameter);
                }
                finally
                {
                    // Await delay
                    await Task.Delay(TimeSpan.FromMilliseconds(Delay));

                    // Release the semaphore
                    JobLock.Release();
                }
            }
        }

        /// <summary>
        /// Perform the job of the command
        /// </summary>
        /// <returns></returns>
        private async Task DoTheJob(object parameter)
        {
            _action?.Invoke();
            if (_funcJob != null)
                await _funcJob();
            if (_funcJobWithParam != null)
                await _funcJobWithParam(parameter);
        }
    }

}
