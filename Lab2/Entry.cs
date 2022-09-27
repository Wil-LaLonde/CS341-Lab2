using CommunityToolkit.Mvvm.ComponentModel;

namespace Lab2 {
    /**
     * The Entry class represents different crossword entries.
     */
    [Serializable()]
    public class Entry : ObservableObject {
        //Instance variables used for getters/setters.
        private int id;
        private String clue;
        private String answer;
        private int difficulty;
        private String date;

        //Entry constants
        public const int MinClueLength = 1;
        public const int MaxClueLength = 250;
        public const int MinAnswerLength = 1;
        public const int MaxAnswerLength = 25;
        public readonly int[] ValidDifficulties = { 0, 1, 2 };
        public const String DateFormat = "mm/dd/yyyy";

        //Invalid entry constants.
        public const int InvalidIdEntry = 0;
        public const int InvalidDifficultyEntry = -1;
        public const String InvalidStringEntry = "";

        public int Id {
            get { return id; }
            set {
                //Valid id -> any int value that is greater than 0.
                if(value > InvalidIdEntry) {
                    SetProperty(ref id, value);
                } else {
                    SetProperty(ref id, InvalidIdEntry);
                }
            }
        }

        public String Clue {
            get { return clue; }
            set {
                int clueLength = value.Length;
                //Valid clue -> any String value with length between 1-250.
                if(clueLength >= MinClueLength && clueLength <= MaxClueLength) {
                    SetProperty(ref clue, value);
                } else {
                    SetProperty(ref clue, InvalidStringEntry);
                }
            }
        }

        public String Answer {
            get { return answer; }
            set {
                int answerLength = value.Length;
                //Valid answer -> any String value with length between 1-25.
                if(answerLength >= MinAnswerLength || answerLength <= MaxAnswerLength) {
                    SetProperty(ref answer, value);
                } else {
                    SetProperty(ref answer, InvalidStringEntry);
                }
            }
        }

        public int Difficulty {
            get { return difficulty; }
            set {
                //Valid difficulty -> any int with a value of 0, 1, or 2.
                if(ValidDifficulties.Contains(value)) {
                    SetProperty(ref difficulty, value);
                } else {
                    SetProperty(ref difficulty, InvalidDifficultyEntry);
                }
            }
        }

        public String Date {
            get { return date; }
            set {
                bool validDate = DateTime.TryParseExact(value, DateFormat,
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out _);
                //Valid date -> any String with a date format of mm/dd/yyyy.
                if (validDate) {
                    SetProperty(ref date, value);
                } else {
                    SetProperty(ref date, InvalidStringEntry);
                }
            }
        }

        /**
         * Entry constructor. Will only set the values to what is passed in if they meet 
         * the setter requirements.
         * 
         * @param int id -> Entry id (any value over 0)
         * @param String clue -> Entry clue (any value with length between 1-250)
         * @param String answer -> Entry answer (any value with length between 1-25)
         * @param int difficulty -> Entry difficulty (any value that's either 0, 1, or 2)
         * @param String date -> Entry date (any value with date format mm/dd/yyyy)
         */
        public Entry(int id, String clue, String answer, int difficulty, String date) {
            Id = id;
            Clue = clue;
            Answer = answer;
            Difficulty = difficulty;
            Date = date;
        }
    }
}
