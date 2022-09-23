using System.Collections.ObjectModel;
using System.Text;

namespace Lab2 {
    /**
     * BusinessLogic class that checks all user input from the UI.
     * This will return messaging regarding if the values from the UI
     * are correct or not. It will also notify the UI if any "database"
     * errors occur.
     */
    public class BusinessLogic : IBusinessLogic {
        private IDatabase flatDatabase;

        /**
         * BusinessLogic constructor that creates a new object
         * of the FlatDatabase.
         */
        public BusinessLogic() {
            flatDatabase = new FlatDatabase();
        }

        /**
         * GetEntries gets all the entries from the "database"
         * 
         * @return Collection of Entries
         */
        public ObservableCollection<Entry> GetEntries() {
            return flatDatabase.GetEntries();
        }

        /**
         * AddEntry takes in all the parameters needed to create a new Entry and
         * and adding it to the "database".
         * 
         * @param String clue -> Entry clue
         * @param String answer -> Entry answer
         * @param String difficulty -> Entry difficulty
         * @param String date -> Entry date
         * @return String validationMessage -> success/error message(s)
         */
        public String AddEntry(String clue, String answer, String difficulty, String date) {
            //Create a new entry.
            Entry addEntry = CreateEntry(clue, answer, difficulty, date);
            //Check to see if the values were properly added???
            String validationMessage = CheckInputEntry(addEntry);
            if(IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
                //Make a call to the "database" to add the entry.
                bool successfulDbCall = flatDatabase.AddEntry(addEntry);
                if(!successfulDbCall) {
                    validationMessage = DataConstants.DatabaseAddErrorMessage;
                }
            } 
            return validationMessage;
        }

        /**
         * DeleteEntry takes in an id value to try and delete from the "database".
         * 
         * @param int id -> Entry id to delete
         * @return String validationMessage -> success/error message(s)
         */
        public String DeleteEntry(int id) {
            String validationMessage = IBusinessLogic.SuccessActionMessage;
            //Check if the id exists.
            if(DoesEntryExist(id)) {
                //Make a call to the "database" to delete the entry.
                bool successfulDbCall = flatDatabase.DeleteEntry(id);
                if(!successfulDbCall) {
                    validationMessage = DataConstants.DatabaseDeleteErrorMessage;
                } 
            } else {
                validationMessage = DataConstants.EntryNotFoundErrorMessage;
            }
            return validationMessage;
        }

        /**
         * EditEntry takes in all the parameters needed to create a new Entry. This new
         * Entry will replace whatever is currently in the "database" if it exists.
         * 
         * @param String clue -> Entry clue
         * @param String answer -> Entry answer
         * @param String difficulty -> Entry difficulty
         * @param String date -> Entry date
         * @param int id -> Entry id to edit
         * @return String validationMessage -> success/error messagees(s)
         */
        public String EditEntry(String clue, String answer, String difficulty, String date, int id) {
            String validationMessage;
            //Check if the given id exists.
            if(DoesEntryExist(id)) {
                //Create a new Entry.
                Entry editEntry = CreateEntry(clue, answer, difficulty, date, id);
                //Check if the Entry is valid.
                validationMessage = CheckInputEntry(editEntry);
                //If the Entry is valid, make a call to the "database" to edit the entry.
                if(IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
                    bool successfulDbCall = flatDatabase.EditEntry(editEntry, id);
                    if (!successfulDbCall) {
                        validationMessage = DataConstants.DatabaseEditErrorMessage;
                    }
                } 
            } else {
                validationMessage = DataConstants.EntryNotFoundErrorMessage;
            }
            return validationMessage;
        }

        /**
         * CreateEntry takes in all the needed parameters to create a new Entry.
         * This will also create a new id if needed and parse the difficulty.
         * 
         * @param String clue -> Entry clue
         * @param String answer -> Entry answer
         * @param String difficultyStr -> Entry difficulty
         * @param String date -> Entry date
         * @param (optional) int id -> Entry id
         * @return Entry newEntry
         */
        private Entry CreateEntry(String clue, String answer, String difficultyStr, String date, int id = Entry.InvalidIdEntry) {
            //Determine new id value only if needed (when adding an entry)
            if(id == Entry.InvalidIdEntry) {
                id = flatDatabase.GetCurrentCollectionSize();
                id++;
            }
            //Try to convert difficulty to an int.
            int difficulty = int.TryParse(difficultyStr, out difficulty) ? difficulty : Entry.InvalidDifficultyEntry;
            //Return the new Entry.
            return new Entry(id, clue, answer, difficulty, date);
        }

        /**
         * DoesEntryExist takes in an id and checks if it exists 
         * in the "database". We can use the list size to check
         * if the id exists or not.
         * 
         * @param int id -> Entry id
         * @return true/false
         */
        private bool DoesEntryExist(int id) {
            return flatDatabase.GetCurrentCollectionSize() > id;
        }

        /**
         * CheckInputEntry takes in an Entry and checks if all
         * the values were set correctly. If so, it will return 
         * that it is a valid Entry. If not, it will return all 
         * the errors seperated by a comma and space.
         * 
         * @param Entry inputEntry -> Entry to parse
         * @return String success/error message(s)
         */
        private String CheckInputEntry(Entry inputEntry) {
            StringBuilder errorBuilder = new StringBuilder();
            //Id check
            if(inputEntry.Id == Entry.InvalidIdEntry) {
                errorBuilder.Append(DataConstants.InvalidIdErrorMessage);
                errorBuilder.Append(DataConstants.CommaSpace);
            }
            //Clue check
            if(inputEntry.Clue == Entry.InvalidStringEntry) {
                errorBuilder.Append(DataConstants.InvalidClueErrorMessage);
                errorBuilder.Append(DataConstants.CommaSpace);
            }
            //Answer check
            if(inputEntry.Answer == Entry.InvalidStringEntry) {
                errorBuilder.Append(DataConstants.InvalidAnswerErrorMessage);
                errorBuilder.Append(DataConstants.CommaSpace);
            }
            //Difficulty check
            if(inputEntry.Difficulty == Entry.InvalidDifficultyEntry) {
                errorBuilder.Append(DataConstants.InvalidDifficultyErrorMessage);
                errorBuilder.Append(DataConstants.CommaSpace);
            }
            //Date check
            if(inputEntry.Date == Entry.InvalidStringEntry) {
                errorBuilder.Append(DataConstants.InvalidDateErrorMessage);
                errorBuilder.Append(DataConstants.CommaSpace);
            }
            //Return errorMessage without the comma and space if there is anything in the errorBuilder.
            String errorMessage = errorBuilder.ToString();
            return String.Empty.Equals(errorMessage) ? IBusinessLogic.SuccessActionMessage : errorMessage.Substring(DataConstants.Zero, errorMessage.Length - DataConstants.CommaSpace.Length);
        }
    }
}
