using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TestWork.Model;

namespace TestWork.DAL
{
    /// <summary>
    /// Хранилище подразделений
    /// </summary>
    public static class DivisionRepository
    {
        //private DivisionRepository() { }

        private static object _lock = new object();

        private static ObservableCollection<Division> _divisions;

        /// <summary>
        /// Все подразделения
        /// </summary>
        public static ObservableCollection<Division> AllDivisions
        {
            get
            {
                lock (_lock)
                {
                    return _divisions ?? (_divisions = GetDivisions());
                }
            }
        }

        private static ObservableCollection<Division> GetDivisions()
        {
            ObservableCollection<Division> divisions = new ObservableCollection<Division>();

            string query = "SELECT Id, Name FROM Divisions ";

            SqlConnection connection = new SqlConnection(ConnectionManager.GetConnectionString());

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        Division division = new Division
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                        divisions.Add(division);
                    }
                }
            }

            return divisions;
        }
    }
}