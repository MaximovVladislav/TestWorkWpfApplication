using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestWork.PL.Infrastructure;

namespace TestWork.PL.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public override string Title => "Тестовая программа";

        private ObservableCollection<ViewModelBase> _tabs;
        
        /// <summary>
        /// Коллекция вкладок
        /// </summary>
        public ObservableCollection<ViewModelBase> Tabs => _tabs ?? (_tabs = new ObservableCollection<ViewModelBase>());

        private ViewModelBase _selectedTab;
        /// <summary>
        /// Выбранная вкладка
        /// </summary>
        public ViewModelBase SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value; 
                OnPropertyChanged();
            }
        }

        RelayCommand _openRatesCommand;
        /// <summary>
        /// Открыть ставки
        /// </summary>
        public ICommand OpenRates
        {
            get
            {
                if (_openRatesCommand == null)
                    _openRatesCommand = new RelayCommand(ExecuteOpenRatesCommand);
                return _openRatesCommand;
            }
        }

        RelayCommand _openManningTableCommand;
        /// <summary>
        /// Открыть штатное расписание
        /// </summary>
        public ICommand OpenManningTable
        {
            get
            {
                return _openManningTableCommand ??
                       (_openManningTableCommand = new RelayCommand(ExecuteOpenManningTableCommand));
            }
        }

        RelayCommand _openReportCommand;
        /// <summary>
        /// Открыть вкладку с отчетом
        /// </summary>
        public ICommand OpenReport
        {
            get { return _openReportCommand ?? (_openReportCommand = new RelayCommand(ExecuteOpenReportCommand)); }
        }

        private void ExecuteOpenRatesCommand(object parameter)
        {
            AddOrSelectTab<RatesViewModel>();

            #region comment

            //RatesViewModel ratesViewModel = Tabs.FirstOrDefault(x => x is RatesViewModel) as RatesViewModel;

            //if (ratesViewModel == null)
            //{
            //    ratesViewModel = new RatesViewModel();
            //    Tabs.Add(ratesViewModel);
            //}

            //SelectedTab = ratesViewModel;

            #endregion

        }

        private void ExecuteOpenManningTableCommand(object parameter)
        {
            AddOrSelectTab<ManningTableViewModel>();

            #region comment

            //ManningTableViewModel manningTableViewModel =
            //    Tabs.FirstOrDefault(x => x is ManningTableViewModel) as ManningTableViewModel;

            //if (manningTableViewModel == null)
            //{
            //    manningTableViewModel = new ManningTableViewModel();
            //    Tabs.Add(manningTableViewModel);
            //}

            //SelectedTab = manningTableViewModel;

            #endregion

        }

        private void ExecuteOpenReportCommand(object parameter)
        {
            AddOrSelectTab<ReportViewModel>();

            #region comment

            //ReportViewModel reportViewModel = Tabs.FirstOrDefault(x => x is ReportViewModel) as ReportViewModel;

            //if (reportViewModel == null)
            //{
            //    reportViewModel = new ReportViewModel();
            //    Tabs.Add(reportViewModel);
            //}

            //SelectedTab = reportViewModel;

            #endregion

        }

        /// <summary>
        /// Создает или выбирает вкладку
        /// </summary>
        /// <typeparam name="T">Тип ViewModel</typeparam>
        private void AddOrSelectTab<T>() where T : ViewModelBase, new()
        {
            T viewModel = Tabs.FirstOrDefault(x => x is T) as T;

            if (viewModel == null)
            {
                viewModel = new T();
                Tabs.Add(viewModel);
            }

            SelectedTab = viewModel;
        }
    }
}