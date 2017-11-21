using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Web;
using System.IO;

namespace StudInfoOlympicAndСompetition.Models
{
 
        [Serializable]
        public class StudOlympComp
        {
            public int IdStud { get; set; }
            public int IdSOlympComp { get; set; }
            public StudOlympComp(int IdStud, int IdSOlympComp)
            {
                this.IdStud = IdStud;
                this.IdSOlympComp = IdSOlympComp;
            }
            public  StudOlympComp()
            {
            }
    
            public static bool StudOlympCompEquals(StudOlympComp x, StudOlympComp y)
            {
            if (x.IdStud == y.IdStud && x.IdSOlympComp == y.IdSOlympComp)
            {
                return true;
            }
            else
                return false;
            }
       
        public static List<StudOlympComp> ReadAndDeserializeConnectStdOlymp()
        {
            var serializer = new XmlSerializer(typeof(List<StudOlympComp>));
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/connectStdOlymp.xml")))
            {
                return (List<StudOlympComp>)serializer.Deserialize(reader);
            }
        }
        public static void SerializeAndSaveConnectStdOlymp(List<StudOlympComp> data)
        {
            var serializer = new XmlSerializer(typeof(List<StudOlympComp>));
            using (var writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/connectStdOlymp.xml")))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}