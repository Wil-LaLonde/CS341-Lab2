using System.Collections.ObjectModel;

namespace Lab2;
public partial class MainPage : ContentPage {
	private ObservableCollection<Entry> entries = new ObservableCollection<Entry>();
	private IBusinessLogic businessLogic;
	public MainPage() {
		InitializeComponent();
		businessLogic = new BusinessLogic();
		entries = businessLogic.GetEntries();
		EntryList.ItemsSource = entries;
		//Might have to set our entries list here???
	}
	private void AddEntryButtonClick(Object sender, EventArgs e) {
		//Getting input from the user interface.
		String clueStr = ClueEntry.Text ?? String.Empty;
		String answerStr = AnswerEntry.Text ?? String.Empty;
		String difficultyStr = DifficultyEntry.Text ?? String.Empty;
		String dateStr = DateEntry.Text ?? String.Empty;
		//Calling businessLogic to try and add the entry.
		String validationMessage = businessLogic.AddEntry(clueStr, answerStr, difficultyStr, dateStr);
		//Checking to see if it was added successfully.
		if(IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
			//Do something here to maybe update our list?
		} else {
            DisplayCompleteAlert(DataConstants.UIAddEntryErrorHeader, DataConstants.UIAddEntryErrorBody, validationMessage);
        }
	}

	private void DeleteEntryButtonClick(Object sender, EventArgs e) {
		//Getting input from the user interface (what entry is selected)
		Entry selectedEntry = (Entry) EntryList.SelectedItem;
		int id = selectedEntry.Id;
		//Calling businessLogic to try and delete the entry.
		String validationMessage = businessLogic.DeleteEntry(id);
		//Checking to see if it was deleted successfully.
		if(IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
			//Do something here to maybe update our list?
		} else {
			DisplayCompleteAlert(DataConstants.UIDeleteEntryErrorHeader, DataConstants.UIDeleteEntryErrorBody, validationMessage);
		}
		

	}

	private void EditEntryButtonClick(Object sender, EventArgs e) {
		//Getting input from the user interface (what entry they want to edit).
		Entry selectedEntry = (Entry)EntryList.SelectedItem;
		int id = selectedEntry.Id;
        //Getting values to edit.
        String clueStr = ClueEntry.Text ?? String.Empty;
        String answerStr = AnswerEntry.Text ?? String.Empty;
        String difficultyStr = DifficultyEntry.Text ?? String.Empty;
        String dateStr = DateEntry.Text ?? String.Empty;
		//Calling businessLogic to try and edit the entry.
		String validationMessage = businessLogic.EditEntry(clueStr, answerStr, difficultyStr, dateStr, id);
		//Checking to see if it was edited successfully.
		if(IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
			//Do something here to maybe update our list?
		} else {
			DisplayCompleteAlert(DataConstants.UIEditEntryErrorHeader, DataConstants.UIEditEntryErrorBody, validationMessage);
		}
    }
	
	private void DisplayCompleteAlert(String errorHeader, String errorBody, String errorMessage) {
		DisplayAlert(errorHeader, errorBody + errorMessage, DataConstants.DisplayAlertOK);
	}
}

