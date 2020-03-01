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

        public string[] GetShortVersionOfBig()
        {
            var big = GetBigList();
            var shortVersionOfBig = new string[4];
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

        public IEnumerable<string> GetRestOfBig()
        {
            var big = GetBigList();
            var restOfBig = new List<string>();
            for (int i = 4; i < big.Length; i++)
            {
                restOfBig.Add(big[i]);
            }
            return big;
        }

        public string[] GetAverageList()
        {
            return Average.Split(delimiter);
        }

        public string[] GetAreasList()
        {
            return Areas.Split(delimiter);
        }

        public string[] GetBigList()
        {
            return Big.Split(delimiter);
        }

        public string[] GetMiniAverageList()
        {
            var avg = GetAverageList();
            var miniAvg = new string[4];
            if (avg.Length < 4)
            {
                return avg;
            } else
            {
                Array.Copy(avg, miniAvg, 4);
                return miniAvg;
            }
        }

        public string[] GetMiniAreasList()
        {
            string[] areas = GetAreasList();
            string[] miniAreas = new string[4];
            if (areas.Length < 4)
            {
                return areas;
            }
            else
            {
                Array.Copy(areas, miniAreas, 4);
                return miniAreas;
            }
        }
    }
}
