using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using Contacts.Data.Adapters;
using Contacts.Data.DTO;
using Contacts.Logic.Entities;

namespace Contacts.Logic.Managers
{
    public static class ContactManager
    {
        private static void CheckPrimary(Contact contact)
        {
            if (contact.Phones.Count(p => p.IsPrimary) > 1)
                throw new Exception("Only one phone should be primary!!!");

            if (contact.EMails.Count(m => m.IsPrimary) > 1)
                throw new Exception("Only one e-mail should be primary!!!");

            if (contact.Addresses.Count(a => a.IsPrimary) > 1)
                throw new Exception("Only one address should be primary!!!");
        }

        public static void StoreContact(Contact newContact)
        {
            CheckPrimary(newContact);

            using (var adapter = new ContactAdapter())
            {
                try
                {
                    adapter.BeginTransaction();
                    var personDTO = adapter.CreatePerson(newContact.Title, newContact.Gender, newContact.FirstName, newContact.LastName);
                    foreach (var phone in newContact.Phones)
                    {
                        adapter.AddPhone(personDTO, phone.Number, phone.Type, phone.IsPrimary);
                    }
                    foreach (var mail in newContact.EMails)
                    {
                        adapter.AddMail(personDTO, mail.Email, mail.IsPrimary);
                    }
                    foreach (var address in newContact.Addresses)
                    {
                        adapter.AddAddress(personDTO, address.Country, address.Town, address.AddressVal, address.PostCode, address.IsPrimary);
                    }
                    adapter.Commit();
                }
                catch
                {
                    adapter.Rollback();
                    throw;
                }
            }
        }

        public static void UpdateContact(Contact contact)
        {
            CheckPrimary(contact);
            using (var adapter = new ContactAdapter())
            {
                try
                {
                    adapter.BeginTransaction();
                    var personDTO = adapter.UpdatePerson(contact.Id, contact.Title, contact.Gender, contact.FirstName, contact.LastName);
                    
                    adapter.ClearPhones(personDTO);
                    foreach (var phone in contact.Phones)
                    {
                        adapter.AddPhone(personDTO, phone.Number, phone.Type, phone.IsPrimary);
                    }
                    
                    adapter.ClearMails(personDTO);
                    foreach (var mail in contact.EMails)
                    {
                        adapter.AddMail(personDTO, mail.Email, mail.IsPrimary);
                    }
                    
                    adapter.ClearAddresses(personDTO);
                    foreach (var address in contact.Addresses)
                    {
                        adapter.AddAddress(personDTO, address.Country, address.Town, address.AddressVal, address.PostCode, address.IsPrimary);
                    }
                    adapter.Commit();
                }
                catch
                {
                    adapter.Rollback();
                    throw;
                }
            }
        }

        public static List<ContactInfo> GetContacts()
        {
            using (var adapter = new ContactAdapter())
            {
                return adapter.GetPersons().Select(p => new Contact(p)).Select(x => new ContactInfo(x)).OrderBy(x => x.FullName).ToList();
            }
        }

        public static List<Contact> GetAllContacts()
        {
            using (var adapter = new ContactAdapter())
            {
                return adapter.GetPersons().Select(p => new Contact(p)).OrderBy(x => x.FullName).ToList();
            }
        }

        public static Contact GetContact(long id)
        {
            using (var adapter = new ContactAdapter())
            {
                return new Contact(adapter.GetPerson(id));
            }
        }

        public static Stream GetContactAvatar(long id)
        {
            //On new contact use male avatar by default.
            if (id == -1)
            {
                return GetMaleDefaultAvatar();
            }
            if (id == -2)
            {
                return GetFemaleDefaultAvatar();
            }
            using (var adapter = new ContactAdapter())
            {
                var person = adapter.GetPersonWithAvatar(id);
                if (person.Avatar.Image == null)
                {
                    return person.Gender == Gender.Female ? GetFemaleDefaultAvatar() : GetMaleDefaultAvatar();
                }
                return new MemoryStream(person.Avatar.Image);
            }
        }

        private static Stream GetMaleDefaultAvatar()
        {
            const string imageResourceName = "avatar_male.png";
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResourceName);
        }

        private static Stream GetFemaleDefaultAvatar()
        {
            const string imageResourceName = "avatar_female.png";
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResourceName);
        }

        public static void DeleteContact(long contactId)
        {
            using (var adapter = new ContactAdapter())
            {
                try
                {
                    adapter.BeginTransaction();
                    adapter.DeletePerson(contactId);
                    adapter.Commit();
                }
                catch
                {
                    adapter.Rollback();
                    throw;
                }
            }
        }

        public static void ChangePhoto(long contactId, byte[] data)
        {
            using (var adapter = new ContactAdapter())
            {
                try
                {
                    adapter.BeginTransaction();
                    adapter.ChangePhoto(contactId, data);
                    adapter.Commit();
                }
                catch
                {
                    adapter.Rollback();
                    throw;
                }
            }
        }
    }
}
