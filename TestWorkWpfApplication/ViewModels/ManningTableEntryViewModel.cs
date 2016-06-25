using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TestWork.BLL;
using TestWork.Model;
using TestWork.PL.Infrastructure;
using TestWork.PL.Infrastructure.Interfaces;

namespace TestWork.PL.ViewModels
{
    public class ManningTableEntryViewModel : ViewModelBase
    {
        private readonly bool _isNewEntry;

        /// <summary>
        /// Конструктор по умолчанию, нужен для создания DataContext во View
        /// </summary>
        public ManningTableEntryViewModel() : this(null)
        {
        }

        /// <summary>
        /// Конструктор, принимающий запись штатного расписания
        /// Если запись пустая (null), он помечает _isNewEntry как true
        /// </summary>
        /// <param name="entry"></param>
        public ManningTableEntryViewModel(ManningTableEntry entry)
        {
            if (entry != null)
            {
                SelectedDivision = entry.Division;
                SelectedRate = entry.Rate;
                HeadCount = entry.HeadCount;

                ManningTableEntry = entry;
            }
            else
            {
                _isNewEntry = true;
                //ManningTableEntry = new ManningTableEntry();
            }

        }

        public override string Title => "Редактирование записи в штатном расписании";

        public ManningTableEntry ManningTableEntry { get; set; }

        private ObservableCollection<Division> _divisions;
        public ObservableCollection<Division> Divisions
        {
            get
            {
                if (_divisions == null)
                {
                    try
                    {
                        _divisions = new DivisionManager().GetDivisions();
                    }
                    catch (Exception ex)
                    {
                        ShowExceptions(ex);
                    }
                }
                return _divisions;
            }
        }

        private Division _selectedDivision;
        public Division SelectedDivision
        {
            get { return _selectedDivision; }
            set
            {
                _selectedDivision = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Rate> _rates;
        public ObservableCollection<Rate> Rates
        {
            get
            {
                if (_rates == null)
                {
                    try
                    {
                        _rates = new RateManager().GetRates();
                    }
                    catch (Exception ex)
                    {
                        ShowExceptions(ex);
                    }
                }
                return _rates;
            }
        }

        private Rate _selectedRate;
        public Rate SelectedRate
        {
            get { return _selectedRate; }
            set
            {
                _selectedRate = value; 
                OnPropertyChanged();
            }
        }

        private int _headCount;

        public int HeadCount
        {
            get { return _headCount; }
            set
            {
                _headCount = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _cancelCommand;
        public ICommand Cancel
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(ExecuteCancelCommand);
                return _cancelCommand;
            }
        }

        private RelayCommand _saveCommand;
        public ICommand Save
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
                return _saveCommand;
            }
        }

        private void ExecuteCancelCommand(object parameter)
        {
            var window = parameter as IClosableDialog;
            if (window != null)
                window.DialogResult = false;
            CloseWindow(window);
        }

        private void ExecuteSaveCommand(object parameter)
        {
            if (_isNewEntry)
                ManningTableEntry = new ManningTableEntry();

            ManningTableEntry.Division = SelectedDivision;
            ManningTableEntry.Rate = SelectedRate;
            ManningTableEntry.HeadCount = HeadCount;

            var window = parameter as IClosableDialog;
            if (window != null)
                window.DialogResult = true;
            CloseWindow(window);
           
        }

        /// <summary>
        /// Сохранение возможно только если выбраны подразделение и ставка
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteSaveCommand(object parameter)
        {
            return SelectedDivision != null && SelectedRate != null;
        }

        private void CloseWindow(IClosableDialog window)
        {
            if (window != null)
                window.Close();
        }
    }
}