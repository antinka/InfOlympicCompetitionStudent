using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace StudInfoOlympicAndСompetition.Models
{
    [Serializable]
    public class Student
    {
        private  static int id;
        public int Id { get; set; }
        public string Name { get; set; }
        private int age;
        public int Age {
            set
            {
                if (value > 15&& value<100)
                {
                    age = value;
                }
                else
                {
                    age = 16;
                }
            }
            get { return age; }
        }
        public string Group { get; set; }
        public int Course { get; set; }
        public Student()
        {   id++;
            Id = id;
        }
        public Student(string Name, int Age, string Group, int Course)
        {
            id++;
            Id = id;
           
            this.Name = Name;
            this.Age = Age;
            this.Group = Group;
            this.Course = Course;
        }

        public static List<Student> ReadAndDeserializeStudent()
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Student.xml")))
            {
                return (List<Student>)serializer.Deserialize(reader);
            }
        }

        public static void SerializeAndSaveStudent(List<Student> data)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/Student.xml")))
            {
                serializer.Serialize(writer, data);
            }
        }

    }
}