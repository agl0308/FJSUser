using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FJS.USER;
using FJS.USER.Models;
using System.Data.Entity;
using System.Security.Cryptography;
using FJS.USER.OfficeHelper.Impl;
using FJS.USER.Common;

namespace ConsoleApplication1
{
    class Program
    {
        static string getOrgPath(int id)
        {
            Console.WriteLine( "``````id````````"+id);
            string str="";
            using (var date = new UserContext())
            {
                foreach (Organization o in date.Organization.Where(r => r.ID == 1).Include("ChildOrgs"))
                {
                    Console.WriteLine("````````````" + o.DeptName);
                    foreach (Organization t in o.ChildOrgs)
                    {
                        str = t.DeptName + "=>"+str;
                        Console.WriteLine(t.DeptName + "``````````````");
                    }
                }
            }
            return str;
        }

        static void Main(string[] args)
        {
            PostImpl post = new PostImpl();
            PersonInfoImpl per = new PersonInfoImpl();
            MyExcelReader m = new MyExcelReader("E:\\方家山\\维修二处  仪控科人员个人基本信息  20150922照片版.xlsx");
            m.Init();
            m.TransToContent(post.TableName, post.Convert);
            m.TransToContent(per.TableName, per.Convert);

            CoursesCommon test = CoursesCommon.GetInstance();
            Courses n = test[仪控科人员信息.QA.ToString()];
            Console.WriteLine("名族:{0},key:{1}",n.Name,n.ID);

            //Database.SetInitializer<UserContext>(null);
            using (var date = new UserContext())
            {
                MyExcelReader reader = new MyExcelReader("C:\\Users\\agl0308\\Desktop\\方家山\\维修二处  仪控科人员个人基本信息  20150922照片版.xlsx");
                //reader.Init();

                //date.Configuration.LazyLoadingEnabled = false;
                 //us = date.Users.Create();
                Users us = date.Users.Where(p=>p.Username.Equals("wanshu")).First();
                us = us==null? date.Users.Create():us;
                us.Name = "万舒";
                us.Username = "wanshu";
                SHA256Managed _sha256 = new SHA256Managed();
                byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes("123456"));
                //return Convert.ToBase64String(_cipherText);
                us.Pwd= Convert.ToBase64String(_cipherText);
                us.Sex = SexType.男;
                us.PoliticalStatus = PoliticalTyep.党员;
                us.NationID = 1;
                us.Blood = BloodType.O型;
                //date.Users.Add(us);
                date.SaveChanges();
                //foreach (Nation a in date.Nation.Include("Users"))
                foreach (Users a in date.Users.Include("UserNation"))
                {
                    Console.WriteLine(a.Name+"__");
                    Console.WriteLine(a.Sex + "__");
                    Console.WriteLine(a.Pwd + "__");
                    Console.WriteLine("a.UserOrganization.ID------" + a.UserNation.Name + "__");
                    //Console.WriteLine(a.UserOrganization.getFullOrgPath() + "__");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++");
                    //foreach (var v in a.Users)
                    //{
                    //    Console.WriteLine(v.Name);
                    //}
                }
            }
            Console.Read();
        }
    }
}
