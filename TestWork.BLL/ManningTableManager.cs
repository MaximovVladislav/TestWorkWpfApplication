using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using TestWork.DAL;
using TestWork.Model;

namespace TestWork.BLL
{
    public class ManningTableManager
    {
        public ObservableCollection<ManningTableEntry> GetManningTable()
        {
            return ManningTableEntryRepository.AllManningTable;
        }

        public void SaveManningTable(ObservableCollection<ManningTableEntry> manningTable)
        {
            ManningTableEntryRepository.Save(manningTable);
        }

        /// <summary>
        /// Добавить записи в список на удаление
        /// </summary>
        /// <param name="deletedEntries"></param>
        public void AddDeletedManningTableEntries(IList deletedEntries)
        {
            ManningTableEntryRepository.DeletedEntries.AddRange(
                deletedEntries.Cast<ManningTableEntry>().Where(x => x.Id > 0));
        }
    }
}