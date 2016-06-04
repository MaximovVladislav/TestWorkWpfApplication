using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TestWork.Model;

namespace TestWork.DAL
{
    /// <summary>
    /// Хранилище ставок
    /// </summary>
    public static class RateRepository
    {
        //private RateRepository() { }

        private static object _lock = new object();

        private static ObservableCollection<Rate> _rates;
        
        public static ObservableCollection<Rate> AllRates
        {
            get
            {
                lock (_lock)
                {
                    return _rates ?? (_rates = GetRates());
                }
            }
        }

        private static List<Rate> _deletedRates;
        /// <summary>
        /// Записи помеченные на удаление
        /// </summary>
        public static List<Rate> DeletedRates
        {
            get
            {
                lock (_lock)
                {
                    return _deletedRates ?? (_deletedRates = new List<Rate>());
                }
            }
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="rates">Ставки</param>
        public static void Save(ObservableCollection<Rate> rates)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.GetConnectionString()))
            {
                connection.Open();

                foreach (var rate in rates)
                {
                    SaveOrUpdateRate(rate, connection);
                }

                foreach (var rate in DeletedRates)
                {
                    DeleteRates(rate, connection);
                }

                DeletedRates.Clear();
            }
        }

        private static ObservableCollection<Rate> GetRates()
        {
            ObservableCollection<Rate> rates = new ObservableCollection<Rate>();

            //string query = "SELECT Id, Id_Position, Rate, Valid_Date FROM Rates ";

            SqlConnection connection = new SqlConnection(ConnectionManager.GetConnectionString());

            using (SqlCommand cmd = new SqlCommand(/*query, connection*/))
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Read_Rates";

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        Rate rate = new Rate();
                        rate.Id = reader.GetInt32(0);
                        rate.Position = JobPositionRepository.AllJobPositions.First(x => x.Id == reader.GetInt32(1));
                        rate.RateValue = reader.GetInt32(2);
                        rate.ValidDate = reader.GetDateTime(3);

                        rates.Add(rate);
                    }
                }
            }

            return rates;
        }

        private static void SaveOrUpdateRate(Rate rate, SqlConnection connection)
        {
            
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_SaveOrUpdate_Rates";
                cmd.Parameters.AddWithValue("Id", rate.Id);
                cmd.Parameters.AddWithValue("Id_Position", rate.Position.Id);
                cmd.Parameters.AddWithValue("Rate", rate.RateValue);
                cmd.Parameters.AddWithValue("Valid_Date", rate.ValidDate);

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                rate.Id = Convert.ToInt32(returnParameter.Value);
                //rate.Id = Convert.ToInt32(cmd.Parameters["Id"].Value);
            }
        }

        private static void DeleteRates(Rate rate, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Remove_Rate";
                cmd.Parameters.AddWithValue("Id", rate.Id);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
