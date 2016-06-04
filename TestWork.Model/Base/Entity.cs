using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestWork.Model.Annotations;

namespace TestWork.Model.Base
{
    /// <summary>
    /// Базовый класс для модели
    /// </summary>
    public class Entity: INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}