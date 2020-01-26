using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class Description
    {
        public int Id { get; set; }
        public string Small { get; set; }
        public string Average { get; set; }
        public int LineId { get; set; }
        public string Areas { get; set; }
        public string Big { get; set; }

        private static char delimiter = '*'; 

        public string[] getShortVersionOfBigDesc()
        {
            string[] big = getBigList();
            string[] shortVersionOfBig = { "", "", "", "" };
            if (big.Length < 4)
            {
                return big;
            }
            else
            {
                Array.Copy(big, shortVersionOfBig, 4);
                return shortVersionOfBig;
            }
        }

        public IEnumerable<string> getRestOfBigDesc()
        {
            List<string> big = new List<string>();
            string[] list = getBigList();
            for (int i = 4; i < list.Length; i++)
            {
                big.Add(list[i]);
            }
            return big;
        }

        public string[] getAverageList()
        {
            return Average.Split(delimiter);
        }

        public string[] getAreasList()
        {
            return Areas.Split(delimiter);
        }

        public string[] getBigList()
        {
            return Big.Split(delimiter);
        }

        public string[] getMiniAverageList()
        {
            string[] avg = getAverageList();
            string[] avg2 = { "", "", "", ""};
            if (avg.Length < 4)
            {
                return avg;
            } else
            {
                Array.Copy(avg, avg2, 4);
                return avg2;
            }
        }

        public string[] getMiniAreasList()
        {
            string[] avg = getAreasList();
            string[] avg2 = { "", "", "", "" };
            if (avg.Length < 4)
            {
                return avg;
            }
            else
            {
                Array.Copy(avg, avg2, 4);
                return avg2;
            }
        }
    }
}
