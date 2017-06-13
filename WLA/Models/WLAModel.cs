using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLA.Models
{
    public class WLAModel
    {
        private wlaEntities db;

        public int getPeriode(int PeriodeId)
        {
            if (PeriodeId == 1)
            {
                return 251;
            }
            //weekly
            else if (PeriodeId == 2)
            {
                return 52;
            }
            //monthly
            else if (PeriodeId == 3)
            {
                return 12;
            }
            //annualy
            else if (PeriodeId == 3)
            {
                return 1;
            }

            return 0;
        }

        public void saveWLA(wlaEntities db)
        {
            this.db = db;
        }
    }
}