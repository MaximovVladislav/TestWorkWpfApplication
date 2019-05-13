using System;
using System.Collections.ObjectModel;
using TestWork.DAL;
using TestWork.Model;

namespace TestWork.BLL
{
    public class DivisionManager
    {
        public ObservableCollection<Division> GetDivisions()
        {
            return DivisionRepository.AllDivisions;
        }
    }
}