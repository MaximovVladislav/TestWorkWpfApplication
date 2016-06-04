using System;
using TestWork.Model.Base;

namespace TestWork.Model
{
    /// <summary>
    /// Отчет
    /// </summary>
    public class ReportEntry : Entity
    {
        /// <summary>
        /// Подразделение
        /// </summary>
        public Division Division { get; set; }

        /// <summary>
        /// Дата С
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата По
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// ФОТ отдела в месяц
        /// </summary>
        public int PayRoll { get; set; }
    }
}