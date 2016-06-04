using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWork.PL.ViewModels;

namespace TestWork.Tests
{
    /// <summary>
    /// Класс содержит тесты для проверки MainWindowViewModel
    /// </summary>
    [TestClass]
    public class MainWindowViewModelTest
    {
        /// <summary>
        /// Тест для проверки добавления вкладки со Ставками в коллекцию вкладок
        /// </summary>
        [TestMethod]
        public void OpenRatesTest()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();

            viewModel.Tabs.Add(new RatesViewModel());

            viewModel.OpenRates.Execute(null);

            CollectionAssert.Contains(viewModel.Tabs, viewModel.SelectedTab);
        }
    }
}
