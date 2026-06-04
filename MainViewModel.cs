using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using ParabolaAnimation.ViewModels;
using ParabolaAnimationApp.Services;

namespace ParabolaAnimationApp.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{

		private readonly ILaunchDatabase _launchDatabase;

		private double _objectX;
		private double _objectY;
		private bool _isAnimating;
		private DispatcherTimer _timer;

		private double _startX = 0;
		private double _startY;
		private double _stepX = 2.0;
		private double _a = -0.01;
		private double _vertexX = 200;
		private double _vertexY = 300;

		public MainViewModel(ILaunchDatabase launchDatabase)
		{
			_launchDatabase = launchDatabase ?? throw new ArgumentNullException(nameof(launchDatabase));
			StartCommand = new RelayCommand(async _ => await StartAnimation(), _ => !IsAnimating);

			_objectX = 0;
			_objectY = 400;
			_startY = _objectY;
		}

		public double ObjectX
		{
			get => _objectX;
			set { _objectX = value; OnPropertyChanged(); }
		}

		public double ObjectY
		{
			get => _objectY;
			set { _objectY = value;OnPropertyChanged(); }
		}

		public bool IsAnimating
		{
			get => _isAnimating;
			set { _isAnimating = value;OnPropertyChanged(); }
		}

		public ICommand StartCommand { get; }
		private async Task StartAnimation()
		{
			await _launchDatabase.AddLaunchAsync(DateTime.Now);

			ObjectX = _startX;
			ObjectY = _startY;

			IsAnimating = true;
			_timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };

			_timer.Tick += OnTimerTick;
			_timer.Start();
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			ObjectX += _stepX;

			double relativeX = ObjectX - _vertexX;
			double newY = _a * relativeX * relativeX + _vertexY;

			ObjectY = _startY - newY;

			if (ObjectX > 400)
			{
				_timer.Stop();
				IsAnimating = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
