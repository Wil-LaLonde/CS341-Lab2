namespace Lab2 {
    [Serializable()]
    /**
     * The Entry class represents different crossword entries.
     */
    public class Entry {
        //Instance variables used for getters/setters.
        private int id;
        private String clue;
        private String answer;
        private int difficulty;
        private String date;

        //Invalid entry constants.
        public const int InvalidIdEntry = 0;
        public const int InvalidDifficultyEntry = -1;
        public const String InvalidStringEntry = "";

        
        public int Id {
            get { return id; }
            set {
                //Valid id -> any int value that is greater than 0.
                if(value > InvalidIdEntry) {
                    id = value;
                } else {
                    id = InvalidIdEntry;
                }
            }
        }

        public String Clue {
            get { return clue; }
            set {
                int clueLength = value.Length;
                int minClueLength = 1;
                int maxClueLength = 250;
                //Valid clue -> any String value with length between 1-250.
                if(clueLength >= minClueLength && clueLength <= maxClueLength) {
                    clue = value;
                } else {
                    clue = InvalidStringEntry;
                }
            }
        }

        public String Answer {
            get { return answer; }
            set {
                int answerLength = value.Length;
                int minAnswerLength = 1;
                int maxAnswerLength = 25;
                //Valid answer -> any String value with length between 1-25.
                if(answerLength >= minAnswerLength || answerLength <= maxAnswerLength) {
                    answer = value;
                } else {
                    answer = InvalidStringEntry;
                }
            }
        }

        public int Difficulty {
            get { return difficulty; }
            set {
                int[] validDifficulties = { 0, 1, 2 };
                //Valid difficulty -> any int with a value of 0, 1, or 2.
                if(validDifficulties.Contains(value)) {
                    difficulty = value;
                } else {
                    difficulty = InvalidDifficultyEntry;
                }
            }
        }

        public String Date {
            get { return date; }
            set {
                String dateFormat = "mm/dd/yyyy";
                bool validDate = DateTime.TryParseExact(value, dateFormat,
                                                        System.Globalization.CultureInfo.InvariantCulture,
                                                        System.Globalization.DateTimeStyles.None,
                                                        out _);
                //Valid date -> any String with a date format of mm/dd/yyyy.
                if (validDate) {
                    date = value;
                } else {
                    date = InvalidStringEntry;
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
