using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts
{
    /// <summary>
    /// Represents a Void type ala F#, since Void is not a true type in C#
    /// </summary>
    public sealed class Unit
    {
        static Unit()
        {
            // Tell the compiler not to mark the class as beforefieldinit, see Jon Skeet's blog
        }

        private Unit()
        {
            // Don't allow people to initialize values of this class and do funny stuff
        }

        /// <summary>
        /// Default and only value of Unit type
        /// </summary>
        private static readonly Unit instance = new Unit();

    }
}
