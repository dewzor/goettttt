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
        public UserModel getUserByID(int id)
        {
            UserModel user;
            using (var context = new UserDBEntities())
            {

                //denna linqsats hämtar joinar profiles med security och filterar bort profiler som inte är visible
                user = (from a in context.Profiles
                        join s in context.SECURITY on a.Id equals s.PID
                        where a.Id == id
                        select new UserModel
                        {
                            About = a.About,
                            Age = a.Age,
                            Email = a.Email,
                            Firstname = a.Firstname,
                            Gender = a.Gender,
                            Id = a.Id,
                            Lastname = a.Lastname,
                            Pic = a.Pic,
                            Visibility = s.VISIBILITY
                        }).SingleOrDefault();
            }
            return user;
        }


        /// <summary>
        ///  Hämtar en user med visst namn
        /// </summary>
        public List<UserModel> findProfilesByName(string name)
        {
            using (var context = new UserDBEntities())
            {
                //denna linqsats hämtar joinar profiles med security och filterar bort profiler som inte är visible
                List<UserModel> users = new List<UserModel>();
                    users = (from a in context.Profiles
                             join s in context.SECURITY on a.Id equals s.PID
                             where (a.Lastname.Contains(name) || a.Firstname.Contains(name)) && (s.VISIBILITY == true)  //kollar om usern innehåller angivet firstname, lastname samt om den är visible
                             select new UserModel
                             {
                                 About = a.About,
                                 Age = a.Age,
                                 Email = a.Email,
                                 Firstname = a.Firstname,
                                 Gender = a.Gender,
                                 Id = a.Id,
                                 Lastname = a.Lastname,
                                 Pic = a.Pic,
                                 Visibility = s.VISIBILITY
                             }).ToList();
                return users;
            }

        }

        /// <summary>
        /// Hämtar alla användare
        /// </summary>
        public List<UserModel> fetchProfiles()
        {
            List<UserModel> users = new List<UserModel>();

            using (var context = new UserDBEntities())
            {
                //denna linqsats hämtar joinar profiles med security och filterar bort profiler som inte är visible
                users = (from a in context.Profiles
                         join s in context.SECURITY on a.Id equals s.PID
                         where (s.VISIBILITY == true)
                         select new UserModel
                         {
                             About = a.About,
                             Age = a.Age,
                             Email = a.Email,
                             Firstname = a.Firstname,
                             Gender = a.Gender,
                             Id = a.Id,
                             Lastname = a.Lastname,
                             Pic = a.Pic,
                             Visibility = s.VISIBILITY
                         }).ToList();
            }
            return users;
        }


        /// <summary>
        /// Hämtar 5 random profiler
        /// </summary>
        /// <returns></returns>
        public List<UserModel> getRandomProfiles()
        {

            using (var context = new UserDBEntities())
            {
                context.Database.Connection.Open();

                //denna linqsats hämtar joinar profiles med security och filterar bort profiler som inte är visible
                List<UserModel> list = new List<UserModel>();
                    list = (from a in context.Profiles
                             join s in context.SECURITY on a.Id equals s.PID
                             where (s.VISIBILITY == true)
                             select new UserModel  //selectar en profile och settar props
                             {
                                 About = a.About,
                                 Age = a.Age,
                                 Email = a.Email,
                                 Firstname = a.Firstname,
                                 Gender = a.Gender,
                                 Id = a.Id,
                                 Lastname = a.Lastname,
                                 Pic = a.Pic,
                                 Visibility = s.VISIBILITY
                             }).ToList(); //skickar till list




                List<int> ids = new List<int>(); //lista som lagrar ids för user
                foreach (var i in list)
                {
                    ids.Add(i.Id);
                }

                List<UserModel> filteredList = new List<UserModel>(); //skapar den filtrerade listan som ska returnerna 5 användare
                Random random = new Random(); //initierar ny random
                List<int> ranNumbers = new List<int>(); //lista för körda nummer
                int c = 0;
                //körs tills 5 unika profiler hittas
                while (c < 5)
                {
                    int ran = ids[random.Next(ids.Count)];   //genererar en ny random id som finns i id listan

                    if (!ranNumbers.Contains(ran)) //kollar om en user redan finns i listan
                    {
                        ranNumbers.Add(ran);  //lägger till en användarens id i id listan
                        filteredList.Add(getUserByID(ran)); //lägger till användare i filtrerade listan
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
                    usr = context.SECURITY.Single(u => u.USERNAME == user.USERNAME && u.PASSWORD == user.PASSWORD);  //kollar om användarnamn och lösen matchar i en tupel
                }
                catch
                {
                    usr = null;
                }

                return usr; //returnerar användaren
            }

        }

    }
}
