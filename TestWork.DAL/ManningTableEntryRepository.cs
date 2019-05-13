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
    /// Хранилище штатного расписания
    /// </summary>
    public static class ManningTableEntryRepository
    {
        private static object _lock = new object();

        private static ObservableCollection<ManningTableEntry> _manningTable;

        /// <summary>
        /// Все штатные расписания
        /// </summary>
        public static ObservableCollection<ManningTableEntry> AllManningTable
        {
            get
            {
                lock (_lock)
                {
                    return _manningTable ?? (_manningTable = GetManningTable());
                }
            }
        }

        private static List<ManningTableEntry> _deletedEntries;
        /// <summary>
        /// Записи помеченные на удаление
        /// </summary>
        public static List<ManningTableEntry> DeletedEntries
        {
            get
            {
                lock (_lock)
                {
                    return _deletedEntries ?? (_deletedEntries = new List<ManningTableEntry>());
                }
            }
        }
        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="manningTable">Штатное расписание</param>
        public static void Save(ObservableCollection<ManningTableEntry> manningTable)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.GetConnectionString()))
            {
                connection.Open();

                foreach (var entry in manningTable)
                {
                    SaveOrUpdateEntry(entry, connection);
                }

                // удаляем записи помеченные на удаление
                foreach (var entry in DeletedEntries)
                {
                    DeleteEntries(entry, connection);
                }

                DeletedEntries.Clear();
            }
        }

        private static ObservableCollection<ManningTableEntry> GetManningTable()
        {
            ObservableCollection<ManningTableEntry> manningTable = new ObservableCollection<ManningTableEntry>();

            SqlConnection connection = new SqlConnection(ConnectionManager.GetConnectionString());

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Read_Manning_Table";

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        ManningTableEntry entry = new ManningTableEntry();
                        entry.Id = reader.GetInt32(0);
                        entry.Division = DivisionRepository.AllDivisions.First(x => x.Id == reader.GetInt32(1));
                        entry.Rate = RateRepository.AllRates.First(x => x.Id == reader.GetInt32(2));
                        entry.HeadCount = reader.GetInt32(3);

                        manningTable.Add(entry);
                    }
                }
            }

            return manningTable;
        }

        private static void SaveOrUpdateEntry(ManningTableEntry entry, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_SaveOrUpdate_Manning_Table";
                cmd.Parameters.AddWithValue("Id", entry.Id);
                cmd.Parameters.AddWithValue("Id_Division", entry.Division.Id);
                cmd.Parameters.AddWithValue("Id_Rate", entry.Rate.Id);
                cmd.Parameters.AddWithValue("Head_Count", entry.HeadCount);

                var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                entry.Id = Convert.ToInt32(returnParameter.Value);
            }

        }

        private static void DeleteEntries(ManningTableEntry entry, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Remove_ManningTableEntry";
                cmd.Parameters.AddWithValue("Id", entry.Id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}