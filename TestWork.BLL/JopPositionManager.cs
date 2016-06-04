using System.Collections.ObjectModel;
using TestWork.DAL;
using TestWork.Model;

namespace TestWork.BLL
{
    public class JopPositionManager
    {
        public ObservableCollection<JobPosition> GetJopPositions()
        {
            return JobPositionRepository.AllJobPositions;
        } 
    }
}