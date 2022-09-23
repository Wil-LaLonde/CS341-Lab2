namespace Lab2 {
    /**
     * DataConstants class holds all values used throughout Lab2. This
     * is to ensure there are not "magic" values floating around.
     */
    public static class DataConstants {
        //General Items START
        public const int Zero = 0;
        public const int One = 1;
        public const String CommaSpace = ", ";
        //General Items END
        //Error Message Types START
        public const String InvalidIdErrorMessage = "InvalidIdEntry";
        public const String InvalidClueErrorMessage = "InvalidClueLength";
        public const String InvalidAnswerErrorMessage = "InvalidAnswerLength";
        public const String InvalidDifficultyErrorMessage = "InvalidDifficulty";
        public const String InvalidDateErrorMessage = "InvalidDateFormat";
        public const String EntryNotFoundErrorMessage = "EntryNotFound";
        public const String DatabaseAddErrorMessage = "DatabaseAddError";
        public const String DatabaseDeleteErrorMessage = "DatabaseDeleteError";
        public const String DatabaseEditErrorMessage = "DatabaseEditError";
        //Error Message Types END
    }
}
