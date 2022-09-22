﻿using System;
using System.Collections.ObjectModel;

namespace Lab2 {
    public interface IBusinessLogic {
        public const String SuccessActionMessage = "Success";
        public ObservableCollection<Entry> GetEntries();
        public String AddEntry(String clue, String answer, String difficulty, String date);
        public String DeleteEntry(int id);
        public String EditEntry(String clue, String answer, String difficulty, String date, int id);
    }
}
