using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge5
{
    public class Write : IWrite
    {
        public void WriteData(string message)
        {
            string path = null;
            try
            {
                path = @"../LogFile";
            }catch(Exception ex)
            {
                throw ex.InnerException;
            }

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(message);

                }
            }
        }
    }
}
