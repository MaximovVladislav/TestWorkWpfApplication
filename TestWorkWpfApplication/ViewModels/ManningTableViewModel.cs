using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using TestWork.BLL;
using TestWork.Model;
using TestWork.PL.Infrastructure;
using TestWork.PL.Views;

namespace TestWork.PL.ViewModels
{
    public class ManningTableViewModel : ViewModelBase
    {
        private readonly ManningTableManager _manningTableManager = new ManningTableManager();

        public ManningTableViewModel()
        {
            ManningTable.CollectionChanged += ManningTable_CollectionChanged;
        }

        private readonly ViewFactory _viewFactory = new ViewFactory();

        public override string Title => "Штатное расписание";

        private ObservableCollection<ManningTableEntry> _manningTable;
        public ObservableCollection<ManningTableEntry> ManningTable
        {
            get
            {
                if (_manningTable == null)
                {
                    try
                    {
                        _manningTable = _manningTableManager.GetManningTable();
                    }
                    catch (Exception ex)
                    {
                        ShowExceptions(ex);
                    }
                }
                return _manningTable;
            }
        }

        /// <summary>
        /// Текущая запись в штатном расписании
        /// </summary>
        public ManningTableEntry SelectedManningTableEntry { get; set; }

        private RelayCommand _editEntryCommand;
        public ICommand EditEntry
        {
            get
            {
                if (_editEntryCommand == null)
                    _editEntryCommand = new RelayCommand(ExecuteEditEntryCommand, CanExecuteEditEntryCommand);
                return _editEntryCommand;
            }
        }

        private RelayCommand _addEntryCommand;
        public ICommand AddEntry
        {
            get
            {
                if (_addEntryCommand == null)
                    _addEntryCommand = new RelayCommand(ExecuteAddEntryCommand);
                return _addEntryCommand;
            }
        }

        private RelayCommand _saveManningTableCommand;

        public ICommand SaveManningTable
        {
            get
            {
                if (_saveManningTableCommand == null)
                    _saveManningTableCommand = new RelayCommand(ExecuteSaveManningTableCommand);
                return _saveManningTableCommand;
            }
        }

        private void ExecuteEditEntryCommand(object parameter)
        {

            try
            {
                ManningTableEntryViewModel viewModel = new ManningTableEntryViewModel(SelectedManningTableEntry);
                _viewFactory.ShowDialog(viewModel);
            }
            catch (Exception ex)
            {
                ShowExceptions(ex);
            }


            //ManningTableEntryView view = new ManningTableEntryView();
            //view.DataContext = viewModel;
            //view.ShowDialog();

        }

        /// <summary>
        /// Редактирование возможно, только если выбрана запись
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteEditEntryCommand(object parameter)
        {
            return SelectedManningTableEntry != null;
        }

        private void ExecuteAddEntryCommand(object parameter)
        {
            try
            {
                ManningTableEntryViewModel viewModel = new ManningTableEntryViewModel();
                _viewFactory.ShowDialog(viewModel);
                if (viewModel.ManningTableEntry != null)
                    ManningTable.Add(viewModel.ManningTableEntry);
            }
            catch (Exception ex)
            {
                ShowExceptions(ex);
            }
        }

        private void ExecuteSaveManningTableCommand(object obj)
        {
            try
            {
                _manningTableManager.SaveManningTable(ManningTable);
            }
            catch (Exception ex)
            {
                ShowExceptions(ex);
            }
        }

        private void ManningTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // если записи удаляются, объекты помечаются на удаление из базы
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _manningTableManager.AddDeletedManningTableEntries(e.OldItems);
            }
        }
    }
}