using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using StrEnum = System.Collections.Generic.IEnumerable<string>;


namespace Fwk.Utils
{
    public class Spelling
    {
        const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static StrEnum Edits1(string w)
        {
            // Deletion
            return (from i in Enumerable.Range(0, w.Length)
                    select w.Substring(0, i) + w.Substring(i + 1))
                // Transposition
             .Union(from i in Enumerable.Range(0, w.Length - 1)
                    select w.Substring(0, i) + w.Substring(i + 1, 1) +
                           w.Substring(i, 1) + w.Substring(i + 2))
                // Alteration
             .Union(from i in Enumerable.Range(0, w.Length)
                    from c in Alphabet
                    select w.Substring(0, i) + c + w.Substring(i + 1))
                // Insertion
             .Union(from i in Enumerable.Range(0, w.Length + 1)
                    from c in Alphabet
                    select w.Substring(0, i) + c + w.Substring(i));
        }
    }
}