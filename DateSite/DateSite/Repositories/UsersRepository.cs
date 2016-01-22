using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UsersRepository
    {
        /// <summary>
        /// Get a user by specific ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Profiles getUserByID(int id)
        {
            Profiles user;
            using (var context = new UserDBEntities())
            {
                user = context.Profiles.Find(id);
            }
            return user;
        }


        /// <summary>
        ///  Hämtar en user med visst namn
        /// </summary>
        public List<Profiles> findProfilesByName(string name)
        {
            using (var context = new UserDBEntities())
            {

                context.Database.Connection.Open();
                List<Profiles> profile = (from a in context.Profiles
                                          where (a.Lastname.Contains(name) || a.Firstname.Contains(name))
                                          select a).ToList();
                return profile;
            }
        }

        /// <summary>
        /// Hämtar alla användare
        /// </summary>
        public List<Profiles> fetchProfiles()
        {
            using (var context = new UserDBEntities())
            {
                context.Database.Connection.Open();
                return context.Profiles.ToList();
            }
        }


        /// <summary>
        /// Hämtar 5 random profiler
        /// </summary>
        /// <returns></returns>
        public List<Profiles> getRandomProfiles()
        {

            using (var context = new UserDBEntities())
            {
                context.Database.Connection.Open();
                List<Profiles> list = context.Profiles.ToList();
                List<int> ids = new List<int>();
                foreach (var i in list)
                {
                    ids.Add(i.Id);
                }

                List<Profiles> filteredList = new List<Profiles>();
                Random random = new Random(); //initierar ny random
                List<int> ranNumbers = new List<int>();
                int c = 0;
                //körs tills 5 unika profiler hittas
                while (c < 5)
                {
                    int ran = ids[random.Next(ids.Count)];

                    if (!ranNumbers.Contains(ran))
                    {
                        ranNumbers.Add(ran);
                        filteredList.Add(getUserByID(ran));
                        c++;
                    }
                }


                return filteredList;
            }
        }


        /// <summary>
        /// Lägger till en användare i databasen
        /// </summary>
        public void insertUser(Profiles profile, SECURITY security)
        {
            try
            {
                using (var context = new UserDBEntities())
                {
                    context.Database.Connection.Open();
                    context.Profiles.Add(profile);
                    context.SaveChanges();
                    security.PID = profile.Id;
                    context.SECURITY.Add(security);
                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {

            }

        }

        /// <summary>
        /// Loggar in en user där namn och lösen matchar i en tupel
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public SECURITY loginUser(SECURITY user)
        {
            using (var context = new UserDBEntities())
            {
                context.Database.Connection.Open();
                SECURITY usr = null;
                try
                {
                    usr = context.SECURITY.Single(u => u.USERNAME == user.USERNAME && u.PASSWORD == user.PASSWORD);
                }
                catch
                {
                    usr = null;
                }

                return usr;
            }

        }

    }
}
