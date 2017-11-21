using StudInfoOlympicAndСompetition.Models;
using StudInfoOlympicAndСompetition.Models.ForViews;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace StudInfoOlympicAndСompetition.Controllers
{
    public class HomeController : Controller
    {
        public static List<Student> std = new List<Student>();
        public static List<OlympicCompetition> OlympComp = new List<OlympicCompetition>();
        public static List<StudOlympComp> connectStdOlymp = new List<StudOlympComp>();

        public ActionResult Index(string CourseStud, string TypeOlyComp, string NameOlComp, string CityOlyComp)
        {
            connectStdOlymp = StudOlympComp.ReadAndDeserializeConnectStdOlymp();
            OlympComp = OlympicCompetition.ReadAndDeserializeOlympicCompetition();
            std = Student.ReadAndDeserializeStudent();

            ViewBag.CourseStud = new SelectList((from t in std select t.Course).Distinct());
            ViewBag.NameOlComp = new SelectList((from t in OlympComp select t.Name).Distinct());
            ViewBag.TypeOlyComp = new SelectList((from t in OlympComp select t.Type).Distinct());
            ViewBag.CityOlyComp = new SelectList((from t in OlympComp select t.City).Distinct());
         
                var look = (
                      from cStOl in connectStdOlymp
                      join st in std on cStOl.IdStud equals st.Id
                      join olC in OlympComp on cStOl.IdSOlympComp equals olC.Id
                      select new StudOlympicCompetition
                      {
                          NameStudent = st.Name,
                          AgeStudent = st.Age,
                          GroupStudent = st.Group,
                          CourseStudent = st.Course,
                          TypeOlyComp = olC.Type,
                          NameOlyComp = olC.Name,
                          CityOlyComp = olC.City,
                          StageOlyComp = olC.Stage,
                          DataStartOlyComp = olC.DataStart,
                          DataEndOlyComp = olC.DataEnd
                      }
                      ).ToList();

            if (CourseStud != null && CourseStud != "")
            {
                look = look.Where(s => s.CourseStudent.ToString() == CourseStud).ToList();
            }
            if (TypeOlyComp != null && TypeOlyComp != "")
            {
                look = look.Where(s => s.TypeOlyComp.ToString() == TypeOlyComp).ToList();
            }
            if (NameOlComp != null && NameOlComp != "")
            {
                look = look.Where(s => s.NameOlyComp == NameOlComp).ToList();
            }
            if (CityOlyComp != null && CityOlyComp != "")
            {
                look = look.Where(s => s.CityOlyComp == CityOlyComp).ToList();
            }
            return View(look);
        }
        public ActionResult ViewStudent(string CourseStud)
        {
            std=Student.ReadAndDeserializeStudent();
            ViewBag.CourseStud = new SelectList((from t in std select t.Course).Distinct());
       
            if (CourseStud != null && CourseStud != "")
            {
                std = std.Where(s => s.Course.ToString() == CourseStud).ToList();
            }
           
            return View(std.ToList());
        }
        public ActionResult DetailsStudent(int id)
        {
          
            for (int i = 0; i < std.Count; i++)
            {
                if (std[i].Id == id)
                {
                    var look = (
                      from cStOl in connectStdOlymp
                      where cStOl.IdStud == std[i].Id
                      join olC in OlympComp on cStOl.IdSOlympComp equals olC.Id
                      select new StudOlympicCompetition
                      {
                          NameStudent = std[i].Name,
                          AgeStudent = std[i].Age,
                          GroupStudent = std[i].Group,
                          CourseStudent = std[i].Course,
                          TypeOlyComp = olC.Type,
                          NameOlyComp = olC.Name,
                          CityOlyComp = olC.City,
                          StageOlyComp = olC.Stage,
                          DataStartOlyComp = olC.DataStart,
                          DataEndOlyComp = olC.DataEnd
                      }
                      ).ToList();
                    return View(look);
                }
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student stud)
        {
            std.Add(stud);
            Student.SerializeAndSaveStudent(std);
            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult AddToStudentOlympComp(int id)
        {
            connectStdOlymp = StudOlympComp.ReadAndDeserializeConnectStdOlymp();
            OlympComp = OlympicCompetition.ReadAndDeserializeOlympicCompetition();
            for (int i = 0; i < std.Count; i++)
            {
                if (std[i].Id == id)
                {
                    ViewBag.id = std[i].Id;
                    ViewBag.OlympCopm = (from c in connectStdOlymp where c.IdStud.Equals(std[i].Id) select c.IdSOlympComp).ToList();
                }
            }
            return View(OlympComp.ToList());
        }
        [HttpPost]
        public ActionResult AddToStudentOlympComp(int idStud, int[] selectedOlympComp)
        {
            connectStdOlymp = StudOlympComp.ReadAndDeserializeConnectStdOlymp();
            for (int i =0; i < connectStdOlymp.Count; i++)
                    {
                        if (connectStdOlymp[i].IdStud == idStud)
                        {
                            connectStdOlymp.RemoveAt(i);
                        }
                    }
            for (int i = 0; i < connectStdOlymp.Count; i++)
            {
                if (connectStdOlymp[i].IdStud == idStud)
                {
                    connectStdOlymp.RemoveAt(i);
                }
            }
            if (selectedOlympComp != null)
            {
                    for (int i = 0; i < selectedOlympComp.Length; i++)
                    {
                        StudOlympComp temp = new StudOlympComp(idStud, selectedOlympComp[i]);
                        connectStdOlymp.Add(temp);
                    }
                for (int q = 0; q < connectStdOlymp.Count; q++)
                {
                    for (int w = q + 1; w < connectStdOlymp.Count; w++)
                    {
                        if (connectStdOlymp[q].IdStud == idStud && connectStdOlymp[w].IdStud == idStud)
                        {
                            if (connectStdOlymp[q].IdSOlympComp.Equals(connectStdOlymp[w].IdSOlympComp))
                                connectStdOlymp.RemoveAt(w);
                        }
                    }
                }
            }
            
            StudOlympComp.SerializeAndSaveConnectStdOlymp(connectStdOlymp);
            return RedirectToAction("Index");

        }

        public ActionResult Deletetudent(int id)
        {
            for (int i = 0; i < std.Count; i++)
            {
                if (std[i].Id == id)
                {
                    std.Remove(std[i]);
                }
            }
            Student.SerializeAndSaveStudent(std);
            return RedirectToAction("Index");
        }
        public ActionResult ViewOlympicCompetition(string TypeOlyComp, string NameOlComp, string CityOlyComp)
        {
            OlympComp = OlympicCompetition.ReadAndDeserializeOlympicCompetition();
            ViewBag.NameOlComp = new SelectList((from t in OlympComp select t.Name).Distinct());
            ViewBag.TypeOlyComp = new SelectList((from t in OlympComp select t.Type).Distinct());
            ViewBag.CityOlyComp = new SelectList((from t in OlympComp select t.City).Distinct());

            if (TypeOlyComp != null && TypeOlyComp != "")
            {
                OlympComp = OlympComp.Where(s => s.Type.ToString() == TypeOlyComp).ToList();
            }
            if (NameOlComp != null && NameOlComp != "")
            {
                OlympComp = OlympComp.Where(s => s.Name == NameOlComp).ToList();
            }
            if (CityOlyComp != null && CityOlyComp != "")
            {
                OlympComp = OlympComp.Where(s => s.City == CityOlyComp).ToList();
            }
            return View(OlympComp.ToList());
        }
        public ActionResult DetailsOlympicCompetitio(int id)
        {

            for (int i = 0; i < OlympComp.Count; i++)
            {
                if (OlympComp[i].Id == id)
                {
                    var look = (
                      from cStOl in connectStdOlymp
                      where cStOl.IdSOlympComp == OlympComp[i].Id
                      join st in std on cStOl.IdStud equals st.Id
                      select new StudOlympicCompetition
                      {
                          NameStudent = st.Name,
                          AgeStudent = st.Age,
                          GroupStudent = st.Group,
                          CourseStudent = st.Course,
                          TypeOlyComp = OlympComp[i].Type,
                          NameOlyComp = OlympComp[i].Name,
                          CityOlyComp = OlympComp[i].City,
                          StageOlyComp = OlympComp[i].Stage,
                          DataStartOlyComp = OlympComp[i].DataStart,
                          DataEndOlyComp = OlympComp[i].DataEnd
                      }
                      ).ToList();
                    return View(look);
                }
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult AddOlympicCompetition()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOlympicCompetition(OlympicCompetition olComp)
        {
            OlympComp.Add(olComp);
            OlympicCompetition.SerializeAndSaveOlympicCompetition(OlympComp);
            return Redirect("Index");
        }
        public ActionResult DeleteOlympicCompetition(int id)
        {
            for (int i = 0; i < OlympComp.Count; i++)
            {
                if (OlympComp[i].Id == id)
                {
                    OlympComp.Remove(OlympComp[i]);
                }
            }
            OlympicCompetition.SerializeAndSaveOlympicCompetition(OlympComp);
            return RedirectToAction("Index");
        }

    }
}