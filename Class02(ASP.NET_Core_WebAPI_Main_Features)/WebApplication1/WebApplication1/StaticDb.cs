namespace WebApplication1
{
    public static class StaticDb
    {
        // Листа што ќе ја користиме како "база"
        // Static class → достапна од било каде во проектот без да креираме објект.
        // SimpleNotes е static List<string> → ја користат сите методи од контролерот како складиште на податоци.
        public static List<string> SimpleNotes = new List<string>
        {
            "Do the homework",
            "Go to the Gym",
            "Buy breakfast"
        };
    }
}
