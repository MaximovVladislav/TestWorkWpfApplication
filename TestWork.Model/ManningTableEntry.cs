using TestWork.Model.Base;

namespace TestWork.Model
{
    /// <summary>
    /// Запись в штатном расписании
    /// </summary>
    public class ManningTableEntry : Entity
    {
        private int _headCount;
        private Rate _rate;
        private Division _division;

        /// <summary>
        /// Подразделение
        /// </summary>
        public Division Division
        {
            get { return _division; }
            set
            {
                _division = value; 
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ставка
        /// </summary>
        public Rate Rate
        {
            get { return _rate; }
            set
            {
                _rate = value; 
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Количество человек
        /// </summary>
        public int HeadCount
        {
            get { return _headCount; }
            set
            {
                _headCount = value; 
                OnPropertyChanged();
            }
        }
    }
}