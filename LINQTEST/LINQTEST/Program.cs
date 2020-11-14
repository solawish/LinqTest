using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTEST
{
    class Program
    {
        static void Main(string[] args)
        {
            var DBData = PrePareDBData();


            var result = DBData.GroupBy(x => new { x.ItineraryID, x.ItineraryName })
                .Select(g => new OutputData
                {
                    ItineraryID = g.Key.ItineraryID,
                    ItineraryName = g.Key.ItineraryName,
                    GroupData = g.Select(x => new GroupData
                    { GroupID = x.GroupID, GuoupName = x.GuoupName, GroupPrice = x.GroupPrice, GroupDate = x.GroupDate }).DistinctBy(x => x.GroupID).ToList(),
                    ItineraryTag = g.Select(x => x.ItineraryTag).Distinct().ToList()
                }
                );
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Console.ReadKey();
        }

        public static List<DBTripData> PrePareDBData()
        {
            var result = new List<DBTripData>();
            result.Add(new DBTripData { ItineraryID = 1, ItineraryName = "日月潭", ItineraryTag = "便宜", GroupID = 1, GuoupName = "周末團", GroupPrice = 10000, GroupDate = Convert.ToDateTime("2020/11/01") });
            result.Add(new DBTripData { ItineraryID = 1, ItineraryName = "日月潭", ItineraryTag = "便宜", GroupID = 2, GuoupName = "平日團", GroupPrice = 9000, GroupDate = Convert.ToDateTime("2020/11/03") });
            result.Add(new DBTripData { ItineraryID = 1, ItineraryName = "日月潭", ItineraryTag = "快速成團", GroupID = 1, GuoupName = "周末團", GroupPrice = 10000, GroupDate = Convert.ToDateTime("2020/11/01") });
            result.Add(new DBTripData { ItineraryID = 1, ItineraryName = "日月潭", ItineraryTag = "快速成團", GroupID = 2, GuoupName = "平日團", GroupPrice = 9000, GroupDate = Convert.ToDateTime("2020/11/03") });
            result.Add(new DBTripData { ItineraryID = 1, ItineraryName = "武陵農場", ItineraryTag = "便宜", GroupID = 3, GuoupName = "平日團", GroupPrice = 9000, GroupDate = Convert.ToDateTime("2020/11/03") });
            result.Add(new DBTripData { ItineraryID = 1, ItineraryName = "武陵農場", ItineraryTag = "少人成團", GroupID = 3, GuoupName = "平日團", GroupPrice = 9000, GroupDate = Convert.ToDateTime("2020/11/03") });

            return result;
        }

        public class DBTripData
        {
            public string ItineraryName { get; set; }
            public int ItineraryID { get; set; }
            public string GuoupName { get; set; }
            public int GroupID { get; set; }
            public int GroupPrice { get; set; }
            public DateTime GroupDate { get; set; }
            public string ItineraryTag { get; set; }
        }

        public class OutputData
        {
            public string ItineraryName { get; set; }
            public int ItineraryID { get; set; }
            public List<GroupData> GroupData { get; set; }
            public List<string> ItineraryTag { get; set; }

            public OutputData()
            {
                GroupData = new List<GroupData>();
                ItineraryTag = new List<string>();
            }
        }

        public class GroupData
        {
            public int GroupID { get; set; }
            public string GuoupName { get; set; }
            public int GroupPrice { get; set; }
            public DateTime GroupDate { get; set; }
        }
    }
}
