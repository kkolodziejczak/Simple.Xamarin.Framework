using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SXF.Core.ViewModels
{
    public class ProgressBarViewModel : BaseComponentViewModel, IProgress
    {
        private CancellationTokenSource _token;
        private int _numberOfSteps;
        private double _stepSize;

        public int NumberOfSteps => _numberOfSteps;

        [DependsOn(nameof(ProgressValue), nameof(_stepSize))]
        public int CurrentStep => (int)(ProgressValue / _stepSize);

        public double ProgressValue
        {
            get => GetValue<double>();
            private set
            {
                SetValue(value);
                OnPropertyChanged(nameof(NumberText));
            }
        }

        [DependsOn(nameof(CurrentStep), nameof(NumberOfSteps))]
        public string NumberText => $"{CurrentStep}/{NumberOfSteps}";

        public string Text
        {
            get => GetValue<string>();
            private set => SetValue(value);
        }

        public ICommand CancelCommand { get; set; }

        public event Action OnCancel = () => { };

        public ProgressBarViewModel()
        {
            CancelCommand = new SequentialCommand(CancelProgress, true);
            ProgressValue = 0;
            _numberOfSteps = 0;
            _stepSize = 1;
        }

        private Task CancelProgress()
        {
            return Task.Run(() =>
            {
                Cancel();
                OnCancel();
            });
        }

        public void OverrideText(string text) => Text = text;

        public void Cancel() => _token.Cancel();

        public bool IsCancellationRequested => _token.IsCancellationRequested;

        public void ProgressOneStep() => ProgressXSteps(1);

        public void ProgressXSteps(int numberOfSteps)
        {
            if (ProgressValue + _stepSize <= 1)
            {
                ProgressValue += numberOfSteps * _stepSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="numberOfSteps"></param>
        /// <exception cref="OperationCanceledException"
        /// <returns></returns>
        public IProgress Show(string text, int numberOfSteps = 1)
        {
            Text = text;
            SetNumberOfSteps(numberOfSteps);
            ProgressValue = 0;

            base.Show();
            return this;
        }

        public void SetNumberOfSteps(int stepCount)
        {
            _numberOfSteps = stepCount;
            _stepSize = 1.0 / stepCount;
        }

        public void ResetProgress()
        {
            ProgressValue = 0;
        }
    }

    public interface IProgress
    {

        void SetNumberOfSteps(int stepCount);

        void ResetProgress();

        bool IsCancellationRequested { get; }

        void ProgressOneStep();

        void ProgressXSteps(int numberOfSteps);

        void Cancel();

        void OverrideText(string text);

    }
}
