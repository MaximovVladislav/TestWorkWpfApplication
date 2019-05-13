using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestWork.DAL;
using TestWork.Model;

namespace TestWork.BLL
{
    public class ReportManager
    {
        /// <summary>
        /// Сформировать отчет
        /// </summary>
        /// <param name="startReportDate">Дата с</param>
        /// <param name="endReportDate">Дата по</param>
        /// <returns>Отчет</returns>
        public ObservableCollection<ReportEntry> GenerateReport(DateTime startReportDate, DateTime endReportDate)
        {
            Report = new ObservableCollection<ReportEntry>();
            var manningTable = ManningTableEntryRepository.AllManningTable;

            // Сортируем записи по дате и группируем по подразделению
            var groupedByDivisionManningTable = manningTable.OrderBy(x => x.Rate.ValidDate).GroupBy(x => x.Division.Id);

            foreach (var subManningTable in groupedByDivisionManningTable.ToList())
            {
                bool isFirstSuitable = true; // Это первый подходящий элемент

                // Здесь будет храниться ФОТ для каждой должности в отдельности, чтобы потом просуммировать их
                var payRollForDivision = new Dictionary<JobPosition, int>();

                for (int i = 0; i < subManningTable.Count(); i++)
                {
                    // Дата действия для текущейзаписи в штатном расписании
                    var validDate = subManningTable.ElementAt(i).Rate.ValidDate;

                    // Если дата не попадает в интервал, переходим к следующей итерации
                    if ((startReportDate > validDate) || (validDate >= endReportDate)) continue;

                    ReportEntry entry = new ReportEntry(); // Запись в отчете

                    ManningTableEntry manningTableEntry; // Запись в штатном расписании

                    // Если элемент не первый из подходящих под интервал, или выполняются остальные условия,
                    // то записываем в отчет текущий элемент
                    if (!isFirstSuitable || i == 0 || validDate == startReportDate)
                    {
                        manningTableEntry = subManningTable.ElementAt(i);
                        entry.Division = manningTableEntry.Division;
                        entry.StartDate = validDate;
                        //entry.PayRoll = manningTableEntry.HeadCount * manningTableEntry.Rate.RateValue;

                        isFirstSuitable = false;
                    }
                    // Если же элемент первый подходящий, но не первый в коллекции 
                    // и его дата действия больше даты начала интервала,
                    // то записываем в отчет данные с предыдущего элемента
                    else
                    {
                        manningTableEntry = subManningTable.ElementAt(i - 1);
                        entry.Division = manningTableEntry.Division;
                        entry.StartDate = startReportDate;
                        //entry.PayRoll = manningTableEntry.HeadCount * manningTableEntry.Rate.RateValue;

                        isFirstSuitable = false;
                    }

                    // Аналогично проверяем, какую дату записать в качестве конечной
                    if (i + 1 < subManningTable.Count() && 
                        subManningTable.ElementAt(i + 1).Rate.ValidDate < endReportDate)
                    {
                        entry.EndDate = subManningTable.ElementAt(i + 1).Rate.ValidDate;
                    }
                    else
                    {
                        entry.EndDate = endReportDate;
                    }

                    // Если в один отдел записано две ставки с одной датой, 
                    // то этот интервал можно объединить, 
                    // а ФОТ отдела за этот период будет просуммирована
                    if (Report.Count > 0)
                    {
                        var preReportEntry = Report[Report.Count - 1];

                        if (preReportEntry.StartDate == entry.StartDate)
                        {
                            Report.Remove(preReportEntry);
                        }
                    }

                    // Высчитываем ФОТ отдела из ФОТ на каждую ставку внутри отдела, 
                    // действующую за данный период
                    var pos = manningTableEntry.Rate.Position;

                    if (payRollForDivision.ContainsKey(pos))
                    {
                        payRollForDivision.Remove(pos);
                    }

                    payRollForDivision.Add(pos, manningTableEntry.HeadCount * manningTableEntry.Rate.RateValue);

                    foreach (var value in payRollForDivision.Values)
                    {
                        entry.PayRoll += value;
                    }

                    Report.Add(entry); // Добавляем запись в коллекцию
                }
            }

            return Report;
        }

        public DateTime StartReportDate
        {
            get { return ReportEntryRepository.StartReportDate; }
            set { ReportEntryRepository.StartReportDate = value; }
        }

        public DateTime EndReportDate
        {
            get { return ReportEntryRepository.EndReportDate; }
            set { ReportEntryRepository.EndReportDate = value; }
        }

        public ObservableCollection<ReportEntry> Report
        {
            get { return ReportEntryRepository.Report; }
            set { ReportEntryRepository.Report = value; }
        }

    }
}