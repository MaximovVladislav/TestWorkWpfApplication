using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Model.Base;

namespace TestWork.Model
{
    /// <summary>
    /// Долность
    /// </summary>
    public class JobPosition : Entity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            JobPosition other = obj as JobPosition;

            if (other == null) return false;

            if (Id <= 0 || other.Id <= 0) return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
