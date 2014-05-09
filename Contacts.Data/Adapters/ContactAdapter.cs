using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Data.DTO;
using NHibernate.Linq;

namespace Contacts.Data.Adapters
{
    public class ContactAdapter : BaseAdapter
    {
        public PersonDTO CreatePerson(Title title, Gender gender, string firstName, string lastName)
        {
            var person = new PersonDTO
            {
                Title = title,
                Gender = gender,
                FirstName = firstName,
                LastName = lastName
            };
            Session.Save(person);
            return person;
        }

        public void AddPhone(PersonDTO personDTO, string number, PhoneType type, bool isPrimary)
        {
            var obj = new PhoneDTO
            {
                Number = number,
                Type = type,
                IsPrimary = isPrimary,
                Person = personDTO
            };
            personDTO.Phones.Add(obj);
            Session.Save(obj);
        }

        public void AddMail(PersonDTO personDTO, string mail, bool isPrimary)
        {
            var obj = new MailDTO
            {
                Email = mail,
                IsPrimary = isPrimary,
                Person = personDTO
            };
            personDTO.Mails.Add(obj);
            Session.Save(obj);
        }

        public void AddAddress(PersonDTO personDTO, string countryName, string townName, string addressVal, string postCode, bool isPrimary)
        {
            var countryQuery = from c in Session.Query<CountryDTO>()
                                                    .FetchMany(x => x.Towns)
                               where c.Name.ToUpper() == countryName.ToUpper()
                               select c;
            var country = countryQuery.SingleOrDefault();
            TownDTO town;
            if (country != null)
            {
                town = country.Towns.SingleOrDefault(t => string.Equals(t.Name, townName, StringComparison.OrdinalIgnoreCase));
                if (town == null)
                {
                    town = new TownDTO
                    {
                        Name = townName,
                        Country = country
                    };
                    Session.Save(town);
                }
            }
            else
            {
                country = new CountryDTO { Name = countryName };
                town = new TownDTO
                {
                    Name = townName, 
                    Country = country
                };
                Session.Save(country);
                Session.Save(town);
            }

            var obj = new AddressDTO
            {
                Town = town,
                Address = addressVal,
                PostCode = postCode,
                IsPrimary = isPrimary,
            };
            personDTO.Addresses.Add(obj);
            Session.Save(obj);
        }

        public IEnumerable<PersonDTO> GetPersons()
        {
            var query = Session.Query<PersonDTO>().FetchMany(x => x.Addresses).ThenFetch(x => x.Town).ThenFetch(x => x.Country).ToFuture();
            
            Session.Query<PersonDTO>().FetchMany(x => x.Mails).ToFuture();
            Session.Query<PersonDTO>().FetchMany(x => x.Phones).ToFuture();            
            Session.Query<PersonDTO>().FetchMany(x => x.Groups).ToFuture();
            return query.ToList();
        }

        public PersonDTO GetPerson(long id)
        {
            return Session.Get<PersonDTO>(id);
        }

        public PersonDTO GetPersonWithAvatar(long id)
        {
            return Session.Query<PersonDTO>().Fetch(p => p.Avatar).Single(p => p.Id == id);
        }

        public void DeletePerson(long id)
        {
            var person = Session.Load<PersonDTO>(id);            
            Session.Delete(person);

            //Check for orphan addresses with no person
            var queryAddresses = from a in Session.Query<AddressDTO>()
                                 where !a.Persons.Any()
                                 select a;
            foreach (var address in queryAddresses)
            {
                Session.Delete(address);
            }
        }

        public PersonDTO UpdatePerson(long id, Title title, Gender gender, string firstName, string lastName)
        {
            var person = this.GetPerson(id);
            person.Title = title;
            person.Gender = gender;
            person.FirstName = firstName;
            person.LastName = lastName;
            Session.Update(person);
            return person;
        }

        public void ClearPhones(PersonDTO personDTO)
        {
            foreach (var phone in personDTO.Phones)
            {
                Session.Delete(phone);
            }
        }

        public void ClearMails(PersonDTO personDTO)
        {
            foreach (var mail in personDTO.Mails)
            {
                Session.Delete(mail);
            }
        }

        public void ClearAddresses(PersonDTO personDTO)
        {
            foreach (var address in personDTO.Addresses)
            {
                Session.Delete(address);
            }
        }

        public void ChangePhoto(long contactId, byte[] data)
        {
            var photo = Session.Load<PersonAvatarDTO>(contactId);
            photo.Image = data;
            Session.Update(photo);
        }

        public IEnumerable<PersonDTO> GetSearchComplete(string val)
        {
            var query = from p in Session.Query<PersonDTO>()
                        where p.FirstName.Contains(val) || 
                              p.LastName.Contains(val) ||
                              p.Phones.Any(ph => ph.Number.Contains(val)) ||
                              p.Mails.Any(m => m.Email.Contains(val)) ||
                              p.Addresses.Any(a => a.Address.Contains(val)) ||
                              p.Addresses.Any(a => a.Town.Name.Contains(val)) ||
                              p.Addresses.Any(a => a.Town.Country.Name.Contains(val))
                        select p;
            return query.AsEnumerable();
        }
    }
}
