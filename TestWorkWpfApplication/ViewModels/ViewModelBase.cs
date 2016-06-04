using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TestWork.PL.Annotations;

namespace TestWork.PL.ViewModels
{
    /// <summary>
    /// Базовый класс для всех ViewModel
    /// Здесь реализация INotifyPropertyChanged и прочие общие вещи
    /// </summary>
    public class ViewModelBase: INotifyPropertyChanged
    {
        public virtual string Title => "";

        protected void ShowExceptions(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}