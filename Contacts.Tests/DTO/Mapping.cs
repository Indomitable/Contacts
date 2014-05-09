using System.Linq;
using Contacts.Data.DTO;
using NHibernate.Linq;
using NUnit.Framework;

namespace Contacts.Tests.DTO
{
    [TestFixture]
    public class Mapping : BaseDBTest
    {
        [Test]
        //If mapping is ok no exception here
        public void CheckMapping()
        {
            var query = from p in Session.Query<PersonDTO>()
                                .FetchMany(x => x.Addresses).ThenFetch(x => x.Town).ThenFetch(x => x.Country)
                                .FetchMany(x => x.Mails)
                                .FetchMany(x => x.Phones)
                                .Fetch(x => x.Avatar)
                                .FetchMany(x => x.Groups)
                        select p;
            query.ToList();
            Assert.Pass(); 
        }
    }
}
