using Contacts.Data.DTO;
using Contacts.Logic.Entities;
using Contacts.Logic.Managers;
using NUnit.Framework;

namespace Contacts.Tests
{
    [TestFixture]
    public class ContactTest
    {
        private void ClearContacts()
        {
            var contacts = ContactManager.GetAllContacts();
            foreach (var contact in contacts)
            {
                ContactManager.DeleteContact(contact.Id);
            }
        }

        [Test]
        public long CreateContact()
        {
            
            ClearContacts();

            var contact = new Contact();
            contact.Title = Title.Mr;
            contact.Gender = Gender.Male;
            contact.FirstName = "No First Name";
            contact.LastName = "No Last Name";
            contact.Phones.Add(new Phone
            {
                Number = "000001111", 
                Type = PhoneType.Mobile, 
                IsPrimary = true
            });
            contact.Phones.Add(new Phone
            {
                Number = "2223333444",
                Type = PhoneType.Other,
                IsPrimary = false
            });
            contact.Addresses.Add(new Address
            {
                AddressVal = "'No Name' street, No5",
                Country = "Bulgaria",
                Town = "Sofia",
                PostCode = "1000"
            });
            contact.EMails.Add(new EMail
            {
                Email = "test.test@mai.com",
                IsPrimary = false
            });

            ContactManager.StoreContact(contact);

            var contacts = ContactManager.GetAllContacts();
            Assert.AreEqual(1, contacts.Count);

            var testConcact = contacts[0];
            Assert.AreEqual(2, testConcact.Phones.Count);
            Assert.AreEqual(1, testConcact.Addresses.Count);
            Assert.AreEqual(1, testConcact.EMails.Count);
            Assert.AreEqual("test.test@mai.com", testConcact.EMails[0].Email);
            return testConcact.Id;
        }

        [Test]
        public void AddMail()
        {
            var id = CreateContact();
            var contact = ContactManager.GetContact(id);
            const string mail = "test1@mail.com";
            contact.EMails.Add(new EMail { Email = mail, IsPrimary = true });
            ContactManager.UpdateContact(contact);

            var testContact = ContactManager.GetContact(id);
            Assert.AreEqual(2, testContact.EMails.Count);
            Assert.AreEqual(mail, testContact.EMail.Email);
        }

    }
}
