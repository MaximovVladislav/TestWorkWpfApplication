using System;
using TestWork.Model.Base;

namespace TestWork.Model
{
    /// <summary>
    /// Ставка
    /// </summary>
    public class Rate : Entity
    {
        /// <summary>
        /// В конструкторе устанавливается по умолчанию текущая дата
        /// </summary>
        public Rate()
        {
            ValidDate = DateTime.Today; 
        }

        /// <summary>
        /// Должность
        /// </summary>
        public JobPosition Position { get; set; }

        /// <summary>
        /// Значение ставки
        /// </summary>
        public int RateValue { get; set; }

        /// <summary>
        /// Дата действия ставки
        /// </summary>
        public DateTime ValidDate { get; set; }

        public override string ToString()
        {
            if (Position != null)
                return Position.Name + " - " + ValidDate.Date.ToString("dd.MM.yyyy");
           return base.ToString();
        }
    }
}