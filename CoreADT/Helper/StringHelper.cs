using System;
using System.Collections.Generic;

namespace CoreADT.Helper
{
    public static class StringHelper
    {
        public static List<string> NullTerminatedStringsToList(byte[] bytes)
        {
            var filelist = new List<string>();
            string filename = string.Empty;
            foreach (var b in bytes)
            {
                var character = Convert.ToChar(b);
                if (character.Equals('\0'))
                {
                    if (!string.IsNullOrWhiteSpace(filename))
                    {
                        filelist.Add(filename);
                        filename = string.Empty;
                    }
                }
                else
                    filename += character;
            }
            return filelist;
        }
    }
}
