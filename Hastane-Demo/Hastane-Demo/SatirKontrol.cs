using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Demo
{
    internal class SatirKontrol
    { 
         
         public bool check;
        public void kontrol(string a) { 

            if (a.Length!=11)
            {
                check = false;
            }
            else
            {
                check=true;
            }
        }
    }
}
