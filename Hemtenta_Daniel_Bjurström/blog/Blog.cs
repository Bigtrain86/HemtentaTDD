using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {
        User _currentUser;
        IAuthenticator _auth;
        public Blog(IAuthenticator auth)
        {
            this._auth = auth;
        }
        
        public bool UserIsLoggedIn
        {
            get
            {
                return _currentUser != null;
            }
        }

        public void LoginUser(User u)
        {
            if (u == null)
            {
                throw new Exception();
            }

            var currentUser = _auth.GetUserFromDatabase(u.Name);

            if (currentUser.Password == u.Password)
            {
                _currentUser = currentUser;
            }
        }

        public void LogoutUser(User u)
        {
            if (u == null)
            {
              throw new Exception();
            }
            _currentUser = null;
        }

        public bool PublishPage(Page p)
        {
            if (p == null || string.IsNullOrEmpty(p.Title) || string.IsNullOrEmpty(p.Content))
            {
                throw new Exception();
            }
            if (!UserIsLoggedIn)
            {
                return false;
            }
            return true;
        }

        public int SendEmail(string address, string caption, string body)
        {
            if (address == null || string.IsNullOrEmpty(address) || caption == null || body == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }
}
