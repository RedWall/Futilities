using System;
using System.Collections.Generic;
using System.Linq;

namespace Futilities.StringParsing
{
    public static class StringParsing
    {
        public static IEqualityComparer<string> IgnoreCaseComparer => new IgnoreCaseComparer();
        public static string Substring(this string input, int start, int? length = null, bool trim = true, bool returnNullForEmptyValues = true)
        {
            string output = string.Empty;

            if (length.HasValue)
            {
                if (input.Length >= start + length.Value)
                    output = input.Substring(start, length.Value);
            }
            else
            {
                if (input.Length > start)
                    output = input.Substring(start);
            }

            if (trim)
                output = output.Trim();

            if (output.Length == 0 && returnNullForEmptyValues)
                output = null;

            return output;

        }


        public static bool TrySplit(this string input, out List<string> output, char delimiter = ',', bool hasQuotedFields = true, List<string> commentTokens = null, bool returnNullForEmptyValues = false)
        {
            try
            {
                output = new List<string>();

                if (string.IsNullOrWhiteSpace(input))
                    return false;

                var fields = input.Split(delimiter);

                if (commentTokens != null && commentTokens.Any(c => fields[0].StartsWith(c)))
                    return false;

                for (int i = 0; i < fields.Length; i++)
                {
                    string field = fields[i];
                    if (string.IsNullOrWhiteSpace(field))
                    {
                        output.Add(returnNullForEmptyValues ? null : string.Empty);
                        continue;
                    }

                    // first char quote?
                    if (!hasQuotedFields || field[0] != '"')
                    {
                        string value = field.Trim();

                        if (returnNullForEmptyValues && value.Length == 0)
                            value = null;

                        output.Add(value);
                    }
                    else
                    {
                        string tempField = field;
                        for (int j = ++i; j < fields.Length; j++)
                        {
                            string field2 = fields[j];
                            //put the comma back
                            tempField += $",{field2}";

                            //Found the end quote or the end of the line
                            if (field2.EndsWith("\"") || j == fields.Length - 1)
                            {
                                string value = tempField.Trim('"', ' ');

                                if (returnNullForEmptyValues && value.Length == 0)
                                    value = null;

                                output.Add(value);

                                // move the iterator to the current position
                                i = j;
                                //leave the j loop
                                break;
                            }
                        }
                    }
                }


                return true;
            }
            catch
            {
                output = null;
                return false;
            }
        }

        public static string Right(this string s, int Length)
        {
            if (s == null)
                return s;

            if (s.Length > Length)
            {
                return s.Substring(s.Length - Length);
            }
            else
                return s;
        }
        public static string Left(this string s, int Length)
        {
            if (s == null)
                return s;

            if (s.Length > Length)
            {
                return s.Substring(0, Length);
            }
            else
                return s;
        }
    }
}
