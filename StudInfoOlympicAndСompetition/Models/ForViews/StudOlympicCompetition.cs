using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudInfoOlympicAndСompetition.Models.ForViews
{
    public class StudOlympicCompetition
    {
        public string NameStudent { get; set; }
        public int AgeStudent { get; set; }
        public string GroupStudent { get; set; }
        public int CourseStudent { get; set; }
        public OlyComp TypeOlyComp { get; set; }
        public string NameOlyComp { get; set; }
        public string CityOlyComp { get; set; }
        public Stage StageOlyComp { get; set; }
        public DateTime DataStartOlyComp { get; set; }
        public DateTime DataEndOlyComp { get; set; }
    }
}