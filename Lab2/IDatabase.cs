using System;
using System.Collections.ObjectModel;

namespace Lab2 {
    public interface IDatabase {
        public ObservableCollection<Entry> GetEntries();
        public int GetCurrentCollectionSize();
        public bool AddEntry(Entry entry);
        public bool DeleteEntry(int id);
        public bool EditEntry(Entry entry, int id);
    }
}
