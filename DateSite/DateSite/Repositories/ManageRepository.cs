using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ManageRepository
    {
        /// <summary>
        /// Getting user about information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getPAboutById(int id)
        {
            using (var context = new UserDBEntities())
            {
                var about = (from a in context.Profiles
                             where (a.Id == id)
                             select a.About).Single();
                return about;
            }
        }

        /// <summary>
        /// kollar om en user är hidden eller inte
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool getHide(int id)
        {
            using (var context = new UserDBEntities())
            {
                var hide = (from a in context.SECURITY
                            where (a.PID == id)
                            select a.VISIBILITY).SingleOrDefault();
                return hide;
            }
        }

        /// <summary>
        /// Settar en user hittden elelr inte!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="choice"></param>
        public void setHide(int id, bool choice)
        {
            using (var context = new UserDBEntities())
            {
                var hide = (from a in context.SECURITY
                            where (a.PID == id)
                            select a).SingleOrDefault();
                if (choice == true)
                    hide.VISIBILITY = true;
                if (choice == false)
                    hide.VISIBILITY = false;
                context.SaveChanges();
            }
        }


        /// <summary>
        /// sätter ettt profile picture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filename"></param>
        public void setPic(int id, string filename)
        {
            using (var context = new UserDBEntities())
            {
                var user = (from a in context.Profiles
                            where (a.Id == id)
                            select a).SingleOrDefault();
                user.Pic = filename;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// hämtar ett profile pictures
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getPic(int id)
        {
            using (var context = new UserDBEntities())
            {
                var user = (from a in context.Profiles
                            where (a.Id == id)
                            select a).SingleOrDefault();
                return user.Pic;
            }
        }

        /// <summary>
        /// Settar en users about text by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="about"></param>
        public void setPAboutById(int id, string about)
        {
            using (var context = new UserDBEntities())
            {
                var user = (from a in context.Profiles
                            where (a.Id == id)
                            select a).SingleOrDefault();
                user.About = about;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// getting user name by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getName(int id)
        {
            using (var context = new UserDBEntities())
            {
                var user = (from a in context.Profiles
                            where (a.Id == id)
                            select a).SingleOrDefault();
                return user.Firstname;
            }
        }

        /// <summary>
        /// update user password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newpass"></param>
        public void UpdatePassword(int id, string newpass)
        {
            using (var context = new UserDBEntities())
            {
                var user = (from a in context.SECURITY
                            where (a.PID == id)
                            select a).SingleOrDefault();
                user.PASSWORD = newpass;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// jämför gammalt pass med nytt pass för att kolla så dem inte är samma
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldpass"></param>
        /// <returns></returns>
        public bool comparePassword(int id, string oldpass)
        {
            using (var context = new UserDBEntities())
            {
                var user = (from a in context.SECURITY
                            where (a.PID == id)
                            select a).SingleOrDefault();
                if (user.PASSWORD == oldpass)
                    return true;
                else
                    return false;
            }
        }
        public string getPassword(int id)
        {
            using (var context = new UserDBEntities())
            {
                var password = (from a in context.SECURITY
                                where (a.PID == id)
                                select a.PASSWORD).SingleOrDefault();
                return password;
            }
        }
    }
}
