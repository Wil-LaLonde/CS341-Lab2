using System.Collections.ObjectModel;
using System.Text.Json;

namespace Lab2 {
    /**
     * FlatDatabase class gets, adds, deletes, and edits different 
     * entries in the "database".
     */
    public class FlatDatabase : IDatabase {
        private String fileName;
        private String appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private ObservableCollection<Entry> entries = new ObservableCollection<Entry>();

        /**
         * FlatDatabase constructor that gets all the stored entries on application
         * start up. This is stored in a list so we're not accessing the "database"
         * every time.
         */
        public FlatDatabase() {
            //File name can be changed to anything.
            fileName = $"{appDataPath}/UserEntries.json";
            entries = GetStoredEntries();
        }

        /**
         * GetStoredEntries gets all the entries from the "database"
         * 
         * @return Collection of Entries
         */
        private ObservableCollection<Entry> GetStoredEntries() {
            ObservableCollection<Entry> entries = new ObservableCollection<Entry>();
            //Check if the file exists.
            if (File.Exists(fileName)) {
                String jsonData = File.ReadAllText(fileName);
                //If there is data, convert to a list of entries.
                if (!String.Empty.Equals(jsonData)) {
                    entries = JsonSerializer.Deserialize<ObservableCollection<Entry>>(jsonData);
                }
            }
            return entries;
        }

        /**
         * GetEntries gets all the current entries in the list.
         * 
         * @return Collection of Entries
         */
        public ObservableCollection<Entry> GetEntries() {
            return entries;
        }

        /**
         * GetCurrentCollectionSize returns the current size of the list.
         * 
         * @return int Entries list size
         */
        public int GetCurrentCollectionSize() {
            return entries.Count;
        }

        /**
         * AddEntry takes in an Entry and adds it to the "database".
         * 
         * @param Entry entry to add
         * @return true/false
         */
        public bool AddEntry(Entry entry) {
            //Add passed in entry to the list.
            entries.Add(entry);
            //Update the "database" and return if it was successful or not.
            return WriteCurrentDataToFile();
        }

        /**
         * DeleteEntry takes in an Entry id and deletes it from the "database".
         * 
         * @param int Entry id to delete
         * @return true/false
         */
        public bool DeleteEntry(int id) {
            //Removing the given id.
            entries.RemoveAt(id);
            //When removing the entry, need to re-index all the other entries (id values).
            int index = DataConstants.One;
            foreach (Entry entry in entries) {
                entry.Id = index++;
            }
            //Update the "database" and return if it was successful or not.
            return WriteCurrentDataToFile();
        }

        /**
         * EditEntry takes in an Entry and Entry id and edits the one in the "database".
         * 
         * @param Entry entry to edit
         * @param int Entry id to edit
         * @return true/false
         */
        public bool EditEntry(Entry entry, int id) {
            //Getting old entry and the given index.
            Entry storedEntry = entries[id];
            //Replace all values with the new entry.
            storedEntry.Clue = entry.Clue;
            storedEntry.Answer = entry.Answer;
            storedEntry.Difficulty = entry.Difficulty;
            storedEntry.Date = entry.Date;
            //Update the "database" and return if it was successful or not.
            return WriteCurrentDataToFile();
        }

        /**
         * WriteCurrentDataToFile updates the "database" with the newest version of the Entries list.
         * 
         * @return true/false
         */
        private bool WriteCurrentDataToFile() {
            bool successFlatDatabaseCall;
            try {
                //Try adding the data to the text file.
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                String jsonData = JsonSerializer.Serialize(entries, options);
                File.WriteAllText(fileName, jsonData);
                successFlatDatabaseCall = true;
            } catch(Exception) {
                //If anything happens, return that it failed.
                successFlatDatabaseCall = false;
            }
            return successFlatDatabaseCall;
        }
    }
}
