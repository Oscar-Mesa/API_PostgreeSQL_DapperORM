using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Model
{
    public class Book
    {
        public int PKId { get; set; }
        public string titulo {  get; set; }
        public string autor {  get; set; }
        public string genero { get; set; }
        public int anio { get; set; }
    }
}
