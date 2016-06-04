using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TestWork.Model;

namespace TestWork.DAL
{
    /// <summary>
    /// Хранилище должностей
    /// </summary>
    public static class JobPositionRepository
    {
        //private JobPositionRepository() { }

        private static object _lock = new object();

        private static ObservableCollection<JobPosition> _jobPositions;

        /// <summary>
        /// Все должности
        /// </summary>
        public static ObservableCollection<JobPosition> AllJobPositions
        {
            get
            {
                lock (_lock)
                {
                    return _jobPositions ?? (_jobPositions = GetJopPositions());
                }
            }
        }

        private static ObservableCollection<JobPosition> GetJopPositions()
        {
            ObservableCollection<JobPosition> jobPositions = new ObservableCollection<JobPosition>();

            string query = "SELECT Id, Name FROM Job_Positions ";

            SqlConnection connection = new SqlConnection(ConnectionManager.GetConnectionString());

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        JobPosition jobPosition = new JobPosition();
                        jobPosition.Id = reader.GetInt32(0);
                        jobPosition.Name = reader.GetString(1);

                        jobPositions.Add(jobPosition);
                    }
                }
            }

            return jobPositions;
        }
    }
}