using TestWork.Model.Base;

namespace TestWork.Model
{
    /// <summary>
    /// Подразделение
    /// </summary>
    public class Division : Entity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}