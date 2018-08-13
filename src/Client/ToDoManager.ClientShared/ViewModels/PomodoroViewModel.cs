using System;
using System.Collections.Generic;
using ToDoManager.Model;
using ToDoManager.Services;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;
using ToDoManager.ClientShared.Tools;
using ToDoManager.ClientShared.Services;
using ToDoManager.ClientShared;

namespace ToDoManager.ViewModels
{
	public class PomodoroViewModel : BaseViewModel
	{
		private IPomodoroDataStorage _dataStorage = new CloudPomodoroDataStorage();
		private INavigation _navigation;

		public ToDoItemModel SelectedItem { get; set; }

		private String startStopButtonText = "Start";
		public String StartStopButtonText
		{
			get => startStopButtonText;
			private set => SetProperty(ref startStopButtonText,  value);
		}

		private ObservableCollection<String> pomodoroLengthOptions;
		public ObservableCollection<String> PomodoroLengthOptions
		{
			get => pomodoroLengthOptions;
			set => SetProperty(ref pomodoroLengthOptions, value);
		}
		public String SelectedPomodoroLength { get; set; }

		public Command StartButtonTouched;

		private String timeLeftTxt = "Start Pomodoro";
		public String TimeLeftText
		{
			get { return timeLeftTxt; }
			set { SetProperty(ref timeLeftTxt, value); }
		}

		private TimeSpan _remainingTime;

		private int _numberOfInterruptions = 0;
		private bool _isTimerRunning;
		CancellationTokenSource timerCancellationTokeSource = new CancellationTokenSource();

		public PomodoroViewModel(ToDoItemModel selectedItem, INavigation navigation)
		{
			_navigation = navigation;
			PomodoroLengthOptions = new ObservableCollection<string> { "5min", "10min", "15min", "25min" };
#if DEBUG
			PomodoroLengthOptions.Insert(0, "test2sec");
			SelectedPomodoroLength = PomodoroLengthOptions[0];
#else
			SelectedPomodoroLength = PromodoLengthOptions.Last();
#endif
			SelectedItem = selectedItem;
			Title = $"Pomodoro for {selectedItem?.Title}";

			StartButtonTouched = new Command(() =>
			{
				if (!_isTimerRunning)
				{
					StartTimer();
				}
				else
				{
					StopTimer();
				}
			});
		}

		private void FinishedPomodoro(int LengthInMinutes)
		{
			_dataStorage.AddNewPomodoroItemAsync(new PomodoroItemModel()
			{
				DateTimeInUtc = DateTime.UtcNow,
				NumberOfInterruptions = _numberOfInterruptions,
			});

			_numberOfInterruptions = 0;
		}

		private void StopTimer()
		{
			StartStopButtonText = "Start";
			timerCancellationTokeSource.Cancel();
			_isTimerRunning = false;
			TimeLeftText = "Start Pomodoro";
		}

		private void StartTimer()
		{
			_remainingTime = TimeSpan.FromMinutes(ConvertPomodoroLengthStrToInt(SelectedPomodoroLength));
			StartStopButtonText = "Stop";
			_isTimerRunning = true;
			timerCancellationTokeSource.Cancel();
			timerCancellationTokeSource = new CancellationTokenSource();
			DoWork(timerCancellationTokeSource.Token);

			async void DoWork(CancellationToken cancellationToken)
			{
				try
				{
					while (_isTimerRunning)
					{
						await Task.Delay(1000);

						cancellationToken.ThrowIfCancellationRequested();
						UpdateTimeLabel();
						_remainingTime -= TimeSpan.FromSeconds(1);

						if (_remainingTime < TimeSpan.FromSeconds(0))
						{
							var lengthInMin = (int)ConvertPomodoroLengthStrToInt(SelectedPomodoroLength);
							_isTimerRunning = false;

							var newItem = new PomodoroItemModel
							{
								DateTimeInUtc = DateTime.UtcNow,
								LengthInSec = lengthInMin*60,
								ToDoItemGuid = SelectedItem.Id,
							};

							try
							{
								var res = await _dataStorage.AddNewPomodoroItemAsync(newItem);

								MessagingCenter.Send(this, Consts.AddNewToDoItemStr, res);
								await _navigation.PopAsync();
							}
							catch(Exception e)
							{

							}
							
						}
					}
				}
				catch (OperationCanceledException)
				{
				}

				void UpdateTimeLabel() => TimeLeftText = $"{_remainingTime.Minutes}:{_remainingTime.Seconds.ToString("00")}";
			}

		}

		private double ConvertPomodoroLengthStrToInt(String lenght)
		{
			switch (lenght)
			{
				case "5min":
					return 5;
				case "10min":
					return 10;
				case "15min":
					return 15;
				case "25min":
					return 25;
				default:
					return 5; //just for testing
			}
		}
	}
}
