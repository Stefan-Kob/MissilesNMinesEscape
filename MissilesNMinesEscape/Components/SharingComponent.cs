using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape.Shared
{
    /// <summary>
    /// Class to share the Vector2 stage between the classes. Only using one class and one public variable
    /// </summary>
    public class SharingComponent
    {
        public static Vector2 stage;
    }
}
