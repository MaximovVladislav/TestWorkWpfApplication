using System;
using System.Windows;
using TestWork.PL.ViewModels;

namespace TestWork.PL.Infrastructure
{
    /// <summary>
    /// Класс для создания View из ViewModel, не нарушая принципы MVVM
    /// </summary>
    public class ViewFactory
    {
        /// <summary>
        /// Отображение диалогового окна для заданной ViewModel, используя имя класса этой ViewModel
        /// </summary>
        /// <typeparam name="T">ViewModel</typeparam>
        /// <param name="viewModel">тип ViewModel</param>
        public void ShowDialog<T>(T viewModel) where T : ViewModelBase
        {
            //string viewModelTypeName = viewModel.GetType().ToString();
            string viewModelTypeName = typeof(T).ToString();

            string viewTypeName = viewModelTypeName.Replace("ViewModel", "View");

            Type viewType = Type.GetType(viewTypeName);

            try
            {
                Window view = Activator.CreateInstance(viewType) as Window;

                view.DataContext = viewModel;

                view.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось создать View", ex);
            }
        }
    }
}