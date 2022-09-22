using System.Collections.ObjectModel;
using System.Text;

namespace Lab2 {
    public class BusinessLogic : IBusinessLogic {
        private IDatabase flatDatabase;

        public BusinessLogic() {
            flatDatabase = new FlatDatabase();
        }

        public ObservableCollection<Entry> GetEntries() {
            return flatDatabase.GetEntries();
        }

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

        public String DeleteEntry(int id) {
            String validationMessage = IBusinessLogic.SuccessActionMessage;
            if(DoesEntryExist(id)) {
                bool successfulDbCall = flatDatabase.DeleteEntry(id);
                if(!successfulDbCall) {
                    validationMessage = DataConstants.DatabaseDeleteErrorMessage;
                } 
            } else {
                validationMessage = DataConstants.EntryNotFoundErrorMessage;
            }
            return validationMessage;
        }

        public String EditEntry(String clue, String answer, String difficulty, String date, int id) {
            String validationMessage;
            if(DoesEntryExist(id)) {
                Entry editEntry = CreateEntry(clue, answer, difficulty, date, id);
                validationMessage = CheckInputEntry(editEntry);
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

        private Entry CreateEntry(String clue, String answer, String difficultyStr, String date, int id = Entry.InvalidIdEntry) {
            //Determine new id value only if needed
            if(id == Entry.InvalidIdEntry) {
                id = flatDatabase.GetCurrentCollectionSize();
                id++;
            }
            //Try to convert difficulty to an int.
            int difficulty = int.TryParse(difficultyStr, out difficulty) ? difficulty : Entry.InvalidDifficultyEntry;
            //Return the new Entry.
            return new Entry(id, clue, answer, difficulty, date);
        }

        private bool DoesEntryExist(int id) {
            return flatDatabase.GetCurrentCollectionSize() > id;
        }

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
            String errorMessage = errorBuilder.ToString();
            return String.Empty.Equals(errorMessage) ? IBusinessLogic.SuccessActionMessage : errorMessage.Substring(DataConstants.Zero, errorMessage.Length - DataConstants.CommaSpace.Length);
        }
    }
}
