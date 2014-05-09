using Contacts.Data;
using NHibernate;
using NUnit.Framework;

namespace Contacts.Tests
{
    [TestFixture]
    public class BaseDBTest
    {
        protected ISession Session;
        [SetUp]
        public void OpenSession()
        {
            Session = ConnectionManager.OpenSession();
        }

        [TearDown]
        public void CloseSession()
        {
            Session.Dispose();
        }
    }

    [SetUpFixture]
    public class DbSetUp
    {
        [SetUp]
        public void WarmUp()
        {
            using (ConnectionManager.OpenSession())
            {

            }
        }

        [TearDown]
        public void CloseFactory()
        {
            ConnectionManager.Close();
        }
    }
}
