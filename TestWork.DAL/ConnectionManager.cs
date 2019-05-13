using System.Configuration;

namespace TestWork.DAL
{
    /// <summary>
    /// Класс для получения строк подключения из конфига
    /// </summary>
    public static class ConnectionManager
    {
        /// <summary>
        /// Возвращает строку подключения из конфига, конфиг в проекте TestWork.PL
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            string connectionString = null;
            var setting = ConfigurationManager.ConnectionStrings["Source"];
            if (setting != null)
                connectionString = setting.ConnectionString;

            return connectionString;
        }
    }
}