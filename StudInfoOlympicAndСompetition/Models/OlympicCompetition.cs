using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace StudInfoOlympicAndСompetition.Models
{
    public enum OlyComp
    {
        Competition,
        Olympic
    }
    public enum Stage
    {
        First,
        second,
        Third,
        Fourth
    }
    [Serializable]
    public class OlympicCompetition
    {
        private static int id;
        public int Id { get; set; }
        public OlyComp Type { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Stage Stage { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
        public OlympicCompetition()
        {
            id++;
            Id = id;
        }
        public OlympicCompetition(OlyComp Type, string Name, string City, Stage Stage, DateTime DataStart, DateTime DataEnd)
        {
            id++;
            Id = id;
            this.Type = Type;
            this.Name = Name;
            this.City = City;
            this.Stage = Stage;
            this.DataStart = DataStart;
            this.DataEnd = DataEnd;
        }
        public static List<OlympicCompetition> ReadAndDeserializeOlympicCompetition()
        {
            var serializer = new XmlSerializer(typeof(List<OlympicCompetition>));
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/OlympicCompetition.xml")))
            {
                return (List<OlympicCompetition>)serializer.Deserialize(reader);
            }
        }

        public static void SerializeAndSaveOlympicCompetition(List<OlympicCompetition> data)
        {
            var serializer = new XmlSerializer(typeof(List<OlympicCompetition>));
            using (var writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/OlympicCompetition.xml")))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}