namespace Lab2;
public partial class MainPage : ContentPage {
	//Business Logic used to check user input and call the "database".
	private IBusinessLogic businessLogic;

	/**
	 * MainPage constructor that also creates a new BusinessLogic object.
	 * This also grabs an connects all entries.
	 */
	public MainPage() {
		InitializeComponent();
		//Creating a new instance of the business logic and gathering all the entries.
		businessLogic = new BusinessLogic();
		EntryList.ItemsSource = businessLogic.GetEntries();
	}

    /**
	 * AddEntryButtonClick checks all the user input when clicked. 
	 * All user input gets sent to the business logic to get checked.
	 * If there is a problem, show all errors to the user.
	 * 
	 * @param Object sender
	 * @param EventArgs e
	 */
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
            //Clearing all user input since the addition was successful.
            ClearUserEntry();
        } else {
			//If there was a problem, show all errors to the user.
            DisplayCompleteAlert(DataConstants.UIAddEntryErrorHeader, DataConstants.UIAddEntryErrorBody, validationMessage);
        }
	}

    /**
	 * DeleteEntryButtonClick checks the entry the user selected. 
	 * All user input gets sent to the business logic to get checked.
	 * If there is a problem, show all errors to the user.
	 * 
	 * @param Object sender
	 * @param Eventargs e
	 */
    private void DeleteEntryButtonClick(Object sender, EventArgs e) {
		//Getting input from the user interface (what entry is selected).
		Entry selectedEntry = (Entry) EntryList.SelectedItem;
		if (selectedEntry != null) {
			int id = selectedEntry.Id - DataConstants.One;
			//Calling businessLogic to try and delete the entry.
			String validationMessage = businessLogic.DeleteEntry(id);
			//Checking to see if it was deleted successfully.
			if (!IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
                //If there was a problem, show all errors to the user.
                DisplayCompleteAlert(DataConstants.UIDeleteEntryErrorHeader, DataConstants.UIDeleteEntryErrorBody, validationMessage);
            } 
        } else {
			DisplayCompleteAlert(DataConstants.UIDeleteEntryErrorHeader, DataConstants.UIDeleteEntryErrorBody, DataConstants.EntryNotSelectedMessage);
		}
    }

	/**
	 * EditEndtryButtonClick checks the entry the user selected and all user input.
	 * All user input gets sent to the business logic to get checked.
	 * If there is a problem, show all errors to the user.
	 * 
	 * @param Object sender
	 * @param EventArgs e
	 */
	private void EditEntryButtonClick(Object sender, EventArgs e) {
		//Getting input from the user interface (what entry they want to edit).
		Entry selectedEntry = (Entry)EntryList.SelectedItem;
		if (selectedEntry != null) {
			int id = selectedEntry.Id - DataConstants.One;
			//Getting values to edit.
			String clueStr = ClueEntry.Text ?? String.Empty;
			String answerStr = AnswerEntry.Text ?? String.Empty;
			String difficultyStr = DifficultyEntry.Text ?? String.Empty;
			String dateStr = DateEntry.Text ?? String.Empty;
			//Calling businessLogic to try and edit the entry.
			String validationMessage = businessLogic.EditEntry(clueStr, answerStr, difficultyStr, dateStr, id);
			//Checking to see if it was edited successfully.
			if (IBusinessLogic.SuccessActionMessage.Equals(validationMessage)) {
                //Clearing all user input since the edit was successful.
                ClearUserEntry();
			} else {
				//If there was a problem, show all errors to the user.
				DisplayCompleteAlert(DataConstants.UIEditEntryErrorHeader, DataConstants.UIEditEntryErrorBody, validationMessage);
			}
		} else {
            DisplayCompleteAlert(DataConstants.UIEditEntryErrorHeader, DataConstants.UIEditEntryErrorBody, DataConstants.EntryNotSelectedMessage);
        }
    }
	
	/**
	 * DisplayCompleteAlert takes in all needed Strings to properly display
	 * an error message to the user.
	 * 
	 * @String errorHeader message at the top of the alert
	 * @String errorBody message in the body of the alert
	 * @String errorMessage message that gets appended to the body of the alert.
	 */
	private void DisplayCompleteAlert(String errorHeader, String errorBody, String errorMessage) {
		DisplayAlert(errorHeader, errorBody + errorMessage, DataConstants.DisplayAlertOK);
	}

	/**
	 * ClearUserEntry is called when either an addition or edit was successful.
	 * This ensures the user isn't having to remove all previous text before
	 * adding or editing a new entry.
	 */
	private void ClearUserEntry() {
		ClueEntry.Text = String.Empty;
		AnswerEntry.Text = String.Empty;
		DifficultyEntry.Text = String.Empty;
		DateEntry.Text = String.Empty;
	}
}
