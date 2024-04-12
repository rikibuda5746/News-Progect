namespace DAL
{
    public class Log
    {
        public static string logfile;
        static Log()//Initial definition of log file.
        {
            DateTime td = new DateTime();
            td = DateTime.Now;//Current time.
            String tds = td.Year + "" + td.Month + "" + td.Day + " T " + td.Hour + "" + td.Minute + "" + td.Second + "_" + td.Millisecond;//onvert time to text.
            logfile = @"C:\Users\USER\Desktop\הנדסאים\News\News\Logs\news_" + tds + ".txt";// file  + time to specific name.
        }

        public static void AddToLog(string p)//Add to log file.
        {
            try
            {
                using (StreamWriter o = new StreamWriter(logfile, true)) //Opened file to write massege if  the file is
                                                                         //not exist created new file.
                {
                    o.WriteLine(p + "\n");//Enter to file.
                    o.Close();
                    o.Dispose();
                }
            }
            catch (Exception e)
            {
                Log.AddToLog("Exception message:" + e.Message);
                Log.AddToLog("Exception StackTrace: " + e.StackTrace.ToString());
            }
        }

    }
}
