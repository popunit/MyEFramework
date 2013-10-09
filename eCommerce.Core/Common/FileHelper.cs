using eCommerce.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eCommerce.Core.Common
{
    public static class FileHelper
    {
        public static string[] ParseText(string filePath, string separator, bool throwIfException)
        {
            if (!File.Exists(filePath))
            {
                if (throwIfException)
                    throw new CommonException(string.Format("File {0} doesn't exist", filePath));
                else
                    return new string[0];
            }

            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                var statement = String.Empty;
                while ((statement = ReadNextSection(reader, separator)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
        }

        private static string ReadNextSection(StreamReader reader, string separator)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var lines = reader.ReadLine();
                if (null == lines)
                {
                    if (sb.Length > 0)
                        return sb.ToString();
                    else
                        return null;
                }

                if (lines.TrimEnd().ToUpper() == separator)
                    break;
                sb.Append(lines + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
