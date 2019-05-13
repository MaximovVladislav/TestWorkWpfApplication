using System;
using System.Collections.ObjectModel;
using TestWork.Model;

namespace TestWork.DAL
{
    /// <summary>
    /// Хранилище отчета
    /// </summary>
    public static class ReportEntryRepository
    {
        private static object _lock = new object();

        private static ObservableCollection<ReportEntry> _report;

        /// <summary>
        /// Отчет
        /// </summary>
        public static ObservableCollection<ReportEntry> Report
        {
            get
            {
                lock (_lock)
                {
                    return _report ?? (_report = new ObservableCollection<ReportEntry>());
                }
            }
            set {
                lock (_lock)
                {
                    _report = value;
                }
            }
        }

        private static DateTime _startReportDate;
        public static DateTime StartReportDate
        {
            get
            {

                if (_startReportDate == default(DateTime))
                    _startReportDate = DateTime.Today;
                return _startReportDate;

            }
            set { _startReportDate = value; }
        }

        private static DateTime _endReportDate;
        public static DateTime EndReportDate
        {
            get
            {
                if (_endReportDate == default(DateTime))
                    _endReportDate = DateTime.Today;
                return _endReportDate;
            }
            set { _endReportDate = value; }
        }
    }
}