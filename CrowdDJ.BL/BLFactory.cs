using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.BL
{
    public static class BLFactory
    {
        private static ICrowdDJBL bl;
        public static ICrowdDJBL GetGeoCacheBL()
        {
            if (bl == null)
                bl = new CrowdDJBL();
            return bl;
        }
    }
}
