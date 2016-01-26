using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repositories
{
    public class FriendRepository
    {
        public List<int> getFriends(int id) // Hämtar friendslist för ID
        {
            using (var context = new UserDBEntities())
            {

                var friendsid = context.Database.SqlQuery<int>(
                       "SELECT friendid FROM dbo.Friends where userid=" + id).ToList();
                return friendsid;
            }
        }

        public List<int> getFRequests(int id) // Lista på folk som requestat friends med mig.
        {
            using (var context = new UserDBEntities())
            {
                var requestsid = context.Database.SqlQuery<int>(
                       "SELECT requesterid FROM dbo.FriendRequest where friendid=" + id).ToList();
                return requestsid;
            }
        }

        public List<int> getmysentFRequests(int id) // Lista på mina skickade requests.
        {
            using (var context = new UserDBEntities())
            {
                var requestsid = context.Database.SqlQuery<int>(
                       "SELECT friendid FROM dbo.FriendRequest where requesterid=" + id).ToList();
                return requestsid;
            }
        }

        public bool isFriends(int id, int friendid) // Kollar om användarna är vänner dvs om vännen finns tillagd hos båda i friendslist.
        {
            using (var context = new UserDBEntities())
            {
                var list = getFriends(id);
                var list1 = getFriends(friendid);
                if (list.Contains(friendid) && list1.Contains(id))
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public bool alreadyRequest(int id, int friendid) // Kollar om friendrequest redan har skickats, då skickas ej igen.
        {
            using (var context = new UserDBEntities())
            {
                var list = getmysentFRequests(id);
                if (list.Contains(friendid))
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public void addFriend(int id, int friendid) // Denna funktionen lägger till vän i BÅDAS vännerlista. Dvs user 1 e friend med 0 och 0 e friend med 1
        {
            using (var context = new UserDBEntities())
            {
                if (!isFriends(id, friendid) && !isFriends(friendid, id)) // Kollar att de redan inte är friends i bådas listor.
                {
                    var deletereq = // Denna och deletereq1 tar bort friendrequests åt båda hållen ifall requestet acceptas.
                            (from friend in context.FriendRequest
                             where (friend.friendid == id && friend.requesterid == friendid)
                             select friend).FirstOrDefault();
                    var deletereq1 =
                        (from friend in context.FriendRequest
                         where (friend.requesterid == id && friend.friendid == friendid)
                         select friend).FirstOrDefault();

                    try
                    {
                        context.FriendRequest.Remove(deletereq);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    try  // Försöker rensa friendrequest om den även skickats åt andra hållet.
                    {
                        context.FriendRequest.Remove(deletereq1);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    var newfriend = new Friends(); // Lägger till friends i båda personernas friendlist. Viss redundans men vet ej hur annars sköta detta.
                    var newfriend1 = new Friends();
                    newfriend.userid = id;
                    newfriend.friendid = friendid;
                    newfriend1.userid = friendid;
                    newfriend1.friendid = id;

                    context.Friends.Add(newfriend); // Addar friend hos första personen.
                    context.Friends.Add(newfriend1); // Addar friend åt andra personen.
                try
                {
                       context.SaveChanges();
                    }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
                }

            }
        }

        public void DenyFriend(int id, int requestid)
        {
            using (var context = new UserDBEntities())
            {
                var deletereq = // Denna och deletereq1 tar bort friendrequests åt båda hållen(för säkerhets skull.)
                            (from friend in context.FriendRequest
                             where (friend.friendid == id && friend.requesterid == requestid)
                             select friend).FirstOrDefault();
                var deletereq1 =
                    (from friend in context.FriendRequest
                     where (friend.requesterid == id && friend.friendid == requestid)
                     select friend).FirstOrDefault();

                try
                {
                    context.FriendRequest.Remove(deletereq);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try  // Försöker rensa friendrequest om den även skickats åt andra hållet.
                {
                    context.FriendRequest.Remove(deletereq1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void SendFRequest(int id, int friendid)
        {
            using (var context = new UserDBEntities())
            {
                if (!alreadyRequest(id, friendid) && !isFriends(id, friendid) && !isFriends(friendid, id)) // Kollar så att man inte redan skickat request.
                {
                    var request = new FriendRequest();
                    request.requesterid = id;
                    request.friendid = friendid;
                    try
                    {
                        context.FriendRequest.Add(request);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
        }

        public void DeleteFriend(int id, int friendid)
        {
            using (var context = new UserDBEntities())
            {
                var deletereq = // Denna och deletereq1 tar bort friends åt båda hållen(för säkerhets skull.)
                            (from friend in context.Friends
                             where (friend.userid == id && friend.friendid == friendid)
                             select friend).FirstOrDefault();
                var deletereq1 =
                    (from friend in context.Friends
                     where (friend.userid == friendid && friend.friendid == id)
                     select friend).FirstOrDefault();

                try
                {
                    context.Friends.Remove(deletereq);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try  // Rensar friends åt andra hållet också.
                {
                    context.Friends.Remove(deletereq1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

}