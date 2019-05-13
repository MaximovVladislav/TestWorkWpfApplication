using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TestWork.BLL;
using TestWork.Model;
using TestWork.PL.Infrastructure;

namespace TestWork.PL.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly ReportManager _reportManager = new ReportManager();

        public override string Title => "Отчет";

        private DateTime _startReportDate;
        public DateTime StartReportDate
        {
            get
            {
                if (_startReportDate == default(DateTime))
                    _startReportDate = _reportManager.StartReportDate;
                return _startReportDate;
            }
            set { _startReportDate = value; }
        }

        private DateTime _endReportDate;
        public DateTime EndReportDate
        {
            get
            {
                if (_endReportDate == default(DateTime))
                    _endReportDate = _reportManager.EndReportDate;
                return _endReportDate;
            }
            set { _endReportDate = value; }
        }

        private ObservableCollection<ReportEntry> _report;
        public ObservableCollection<ReportEntry> Report
        {
            get
            {
                if (_report == null)
                    _report = _reportManager.Report;
                return _report; 
                
            }
            private set
            {
                _report = value; 
                OnPropertyChanged();
            }
        }

        public ReportEntry SelectedReportEntry { get; set; }

        private RelayCommand _getReportCommand;
        public ICommand GetReport
        {
            get
            {
                if (_getReportCommand == null)
                    _getReportCommand = new RelayCommand(ExecuteGetReportCommand, CanExecuteGetReportCommand);
                return _getReportCommand;
            }
        }

        private void ExecuteGetReportCommand(object parameter)
        {
            Report = _reportManager.GenerateReport(StartReportDate, EndReportDate);
        }

        /// <summary>
        /// Формирование отчета возможно только если дата с не больше даты по
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteGetReportCommand(object parameter)
        {
            return StartReportDate <= EndReportDate;
        }
    }
}