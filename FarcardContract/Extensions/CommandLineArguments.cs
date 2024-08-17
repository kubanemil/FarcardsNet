using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace FarcardContract.Extensions
{
    public class CommandLineArguments
    {

        private StringDictionary Parameters;


        public CommandLineArguments(string[] Args)
        {
            Parameters = new StringDictionary();
            Regex spliter = new Regex(@"^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Regex remover = new Regex(@"^['""]?(.*?)['""]?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string parameter = null;
            string[] parts;


            foreach (string txt in Args)
            {

                parts = spliter.Split(txt, 3);

                switch (parts.Length)
                {

                    case 1:
                        if (parameter != null)
                        {
                            if (!Parameters.ContainsKey(parameter))
                            {
                                parts[0] =
                                    remover.Replace(parts[0], "$1");

                                Parameters.Add(parameter, parts[0]);
                            }
                            parameter = null;
                        }

                        break;


                    case 2:

                        if (parameter != null)
                        {
                            if (!Parameters.ContainsKey(parameter))
                                Parameters.Add(parameter, "true");
                        }
                        parameter = parts[1]?.ToLower();
                        break;


                    case 3:

                        if (parameter != null)
                        {
                            if (!Parameters.ContainsKey(parameter))
                                Parameters.Add(parameter, "true");
                        }

                        parameter = parts[1]?.ToLower();


                        if (!Parameters.ContainsKey(parameter))
                        {
                            parts[2] = remover.Replace(parts[2], "$1");
                            Parameters.Add(parameter, parts[2]);
                        }

                        parameter = null;
                        break;
                }
            }

            if (parameter != null)
            {
                if (!Parameters.ContainsKey(parameter))
                    Parameters.Add(parameter, "true");
            }
        }


        public string this[string Param]
        {
            get
            {
                return (Parameters[Param]);
            }
        }

        public bool IsParam(string paramName)
        {
            if (!string.IsNullOrWhiteSpace(paramName))
            {
                return Parameters.ContainsKey(paramName.ToLower());
            }
            return false;
        }

    }
}
