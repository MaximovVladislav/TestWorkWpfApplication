using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestWork.DAL;
using TestWork.Model;

namespace TestWork.BLL
{
    public class RateManager
    {
        public ObservableCollection<Rate> GetRates()
        {
            return RateRepository.AllRates;
        }

        public void SaveRates(ObservableCollection<Rate> rates)
        {
            RateRepository.Save(rates);
        }

        /// <summary>
        /// Добавить ставки в список на удаление
        /// </summary>
        /// <param name="deletedRates"></param>
        public void AddDeletedRates(IList deletedRates)
        {
            RateRepository.DeletedRates.AddRange(deletedRates.Cast<Rate>().Where(x => x.Id > 0));
        }
    }
}
