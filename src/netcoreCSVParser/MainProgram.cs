namespace netcoreCsvParser
{
    using System;
    using System.Text;

    public class MainProgram
    {
        public static int Main(string[] args)
        {
            FileIO fileIO = new FileIO();
            try 
            {
                if (args.Length != 1)
                {
                    throw new ArgumentException("Exactly one argument required.");
                }
                var parser = new FileParser(args[0], fileIO);
                parser.ExecuteJob();
            } 
            catch (Exception caught)
            {
                fileIO.Print(fileIO.GetErrorOut(caught));
                return -1;
            }
            return 0;
        }
    }
}
