using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Books
{
    internal class router
    {
        static string desarrollo = "https://localhost:7159";

        static public string Books { get { return desarrollo + "/api/Books"; } }
    
    }
}
