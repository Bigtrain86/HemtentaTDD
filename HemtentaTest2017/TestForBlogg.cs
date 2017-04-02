using System;
using Xunit;
using Moq;
using HemtentaTdd2017.blog;
using HemtentaTdd2017;

namespace HemtentaTest2017
{
    public class TestForBlogg
    {

        const string _userName = "username";
        IBlog _blog;
        Mock<IAuthenticator> _auth;
        User _user = new User(_userName);
        Page _validPage = new Page { Title = "TestTitle", Content = "About Tests" };

        public TestForBlogg()
        {
            _auth = new Mock<IAuthenticator>();
            _blog = new Blog(_auth.Object);

            _auth.Setup(u => u.GetUserFromDatabase(_userName)).Returns(new User(_userName));
        }

        [Fact]
        public void UserLogIn_Success()
        {
            _blog.LoginUser(_user);
            _auth.Verify((u) => u.GetUserFromDatabase(_userName), Times.Exactly(1));
            Assert.True(_blog.UserIsLoggedIn);
        }

        [Fact]
        public void UserLogIn_Fail()
        {
            Assert.False(_blog.UserIsLoggedIn);
        }
        [Fact]
        public void LogIn_InvalidObject()
        {
            Assert.Throws<Exception>(() => _blog.LoginUser(null));
        }

        [Fact]
        public void LogOut_Success()
        {
            _blog.LoginUser(_user);
            _blog.LogoutUser(_user);

            Assert.False(_blog.UserIsLoggedIn);
        }
        [Fact]
        public void LogOut_ThrowException()
        {
            Assert.Throws<Exception>(() => _blog.LogoutUser(null));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(null, "")]
        public void PublishPage_IncorrectValues(string title, string content)
        {
            var page = new Page { Title = title, Content = content };

            Assert.Throws<Exception>(() => _blog.PublishPage(page));
        }

        [Fact]
        public void PublishPage_PageIsNull()
        {
            //Kastar ett exception när page objectet är Null.
            Assert.Throws<Exception>(() => _blog.PublishPage(null));
        }

        [Fact]
        public void PublishPage__Success()
        {
            _blog.LoginUser(_user);

            _auth.Verify((x) => x.GetUserFromDatabase(_userName), Times.Exactly(1));

            var result = _blog.PublishPage(_validPage);

            Assert.True(result);
        }

        [Fact]
        public void PublishPage_Not_LoggedIn()
        {
            var result = _blog.PublishPage(_validPage);

            Assert.False(result);
        }

        [Theory]
        [InlineData(null, null, "")]
        [InlineData(null, "", null)]
        [InlineData("", null, null)]
        [InlineData(null, null, null)]
        [InlineData("", null, "")]
        [InlineData("", "", null)]
        [InlineData(null, "", "")]
        [InlineData("", "", "")]
        public void SendEmail_String_Is_Null_Or_Empty(string address, string caption, string body)
        {
            _blog.LoginUser(_user);

            var result = _blog.SendEmail(address, caption, body);

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("Adress", "Caption", "Body")]
        public void SendEmail_Sending_Email_Successfully(string address, string caption, string body)
        {
            _blog.LoginUser(_user);

            var result = _blog.SendEmail(address, caption, body);

            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData("Adress", "Caption", "Body")]
        public void SendEmail_Sending_Email_With_CorrectValue_But_Not_Logged_In(string address, string caption, string body)
        {
            var result = _blog.SendEmail(address, caption, body);

            Assert.Equal(0, result);
        }
    }
}

