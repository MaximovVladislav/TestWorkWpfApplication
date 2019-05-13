using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestWork.BLL;
using TestWork.Model;
using TestWork.PL.Infrastructure;

namespace TestWork.PL.ViewModels
{
    public class RatesViewModel : ViewModelBase
    {
        private readonly RateManager _rateManager = new RateManager();

        public RatesViewModel()
        {
            Rates.CollectionChanged += Rates_CollectionChanged;
        }

        public override string Title => "Ставки";

        private ObservableCollection<Rate> _rates;
        public ObservableCollection<Rate> Rates
        {
            get
            {
                if (_rates == null)
                {
                    try
                    {
                        _rates = _rateManager.GetRates();
                    }
                    catch (Exception ex)
                    {
                        ShowExceptions(ex);
                    }
                }
                return _rates;
            }
            set { _rates = value; }
        }

        public Rate SelectedRate { get; set; }

        private ObservableCollection<JobPosition> _jobPositions;
        public ObservableCollection<JobPosition> JobPositions
        {
            get
            {
                if (_jobPositions == null)
                {
                    try
                    {
                        _jobPositions = new JopPositionManager().GetJopPositions();
                    }
                    catch (Exception ex)
                    {
                        ShowExceptions(ex);
                    }
                }
                return _jobPositions;
            }
        }

        private RelayCommand _saveRatesCommand;

        public ICommand SaveRates
        {
            get
            {
                if(_saveRatesCommand == null)
                    _saveRatesCommand = new RelayCommand(ExecuteSaveRatesCommand);
                return _saveRatesCommand;
            }
        }

        private void ExecuteSaveRatesCommand(object parameter)
        {
            try
            {
                new RateManager().SaveRates(Rates);
            }
            catch (Exception ex)
            {
                ShowExceptions(ex);
            }
        }

        private void Rates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // если записи удаляются, объекты помечаются на удаление из базы
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _rateManager.AddDeletedRates(e.OldItems);
            }
        }
    }
}