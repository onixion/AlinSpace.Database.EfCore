using System;
using System.IO;

namespace AlinSpace.Database.Ef.Testing
{
    /// <summary>
    /// Represents the fakes.
    /// </summary>
    public static class Fakes
    {
        /// <summary>
        /// Gets the names.
        /// </summary>
        public static string[] Names { get; private set; }

        /// <summary>
        /// Initializes the fakes.
        /// </summary>
        public static void Init()
        {
            Names = File.ReadAllText("Names.txt")
                .Replace("\n", Environment.NewLine)
                .Split(Environment.NewLine);
        }
    }
}
