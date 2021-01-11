using System;
using System.IO;
using System.Collections;
using System.Text;

namespace BirdieYa
{
    public class QueryReader
    {
        public Hashtable Querys = new Hashtable();

        public QueryReader()
        {
            this.addQuerys();
        }

        private void addQuerys()
        {
            DirectoryInfo di = new DirectoryInfo(System.Web.HttpRuntime.AppDomainAppPath + "\\query");
            if (di.Exists)
            {
                FileInfo[] fis = di.GetFiles("*.sql");

                foreach (FileInfo fi in fis)
                {
                    this.readFile(fi.FullName);
                }
            }
            else
            {
                
            }
        }

        private void readFile(string path)
        {
            StreamReader sr = new StreamReader(path);

            string sLine = null;

            while ((sLine = sr.ReadLine()) != null)
            {
                this.readChar(sLine);
            }
            sr.Close();
        }

        private StringBuilder temp = new StringBuilder();
        private string tempValue = String.Empty;
        private string tempkey = String.Empty;

        private bool readChar(string str)
        {
            bool b = false;
            string key = "";
            char[] chars = str.ToCharArray();
            int cnt = 0;

            foreach (char c in chars)
            {
                if (cnt == 0 && c.Equals('#'))
                {
                    break;
                }
                else if (cnt == 0 && c.Equals('$'))
                {
                    this.tempValue = temp.ToString();
                    temp = null;
                    temp = new StringBuilder();
                    if (!this.tempkey.Equals(String.Empty) && !this.tempValue.Equals(String.Empty))
                    {
                        Querys.Add(this.tempkey, this.tempValue);
                    }
                    b = true;
                }
                else if (cnt == 0)
                {
                    temp.Append(str + " ");
                    break;
                }
                if (b && cnt != 0)
                {
                    key += c;
                }
                cnt++;
            }
            if (b)
            {
                tempkey = key;
            }
            return b;
        }

        public string getQuery(string key)
        {
            foreach (DictionaryEntry de in Querys)
            {
                if (de.Key.ToString().Equals(key.Trim()))
                {
                    return Convert.ToString(de.Value);
                }

            }
            return String.Empty;
        }
    }
}
