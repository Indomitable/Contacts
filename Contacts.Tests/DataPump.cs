using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Contacts.Data.DTO;
using Contacts.Logic.Entities;
using Contacts.Logic.Managers;
using HtmlAgilityPack;
using NUnit.Framework;

namespace Contacts.Tests
{
    [TestFixture]
    public class DataPump
    {
        [Test]
        public void Pump()
        {
            for (int i = 0; i < 200; i++)
            {
                Contact contact = new Contact();
                var random = new Random();
                var rnd = random.Next(1000);
                contact.Gender = rnd%2 == 0 ? Gender.Male : Gender.Female;
                contact.Title = (Title)random.Next(0, 7);
                var name = GetName(contact.Gender);
                contact.FirstName = name.Item1;
                contact.LastName = name.Item2;
                contact.EMails.Add(new EMail
                {
                    Email = GetEmail(),
                    IsPrimary = true
                });
                contact.Phones.Add(new Phone
                {
                    Number = random.Next(1000000).ToString("D10"),
                    IsPrimary = true,
                    Type = (PhoneType)random.Next(0, 4)
                });
                byte[] buffer = new byte[20];
                random.NextBytes(buffer);
                contact.Addresses.Add(new Address
                {
                    Country = "Ireland",
                    Town = "Dublin",
                    AddressVal = Convert.ToBase64String(buffer),
                    IsPrimary = true,
                    PostCode = GetPostCode()
                });

                ContactManager.StoreContact(contact);
            }
        }

        [Test]
        public Tuple<string, string> GetName(Gender gender)
        {
            //Use site http://www.behindthename.com/ to generate Irish names :) use link http://www.behindthename.com/random/random.php?number=2&gender=m&surname=&all=no&usage_iri=1

            string sex = gender == Gender.Male ? "m" : "f";
            var request = (HttpWebRequest)WebRequest.Create(string.Format("http://www.behindthename.com/random/random.php?number=2&gender={0}&surname=&all=no&usage_iri=1", sex));
            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var html = reader.ReadToEnd();
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var node = doc.DocumentNode.SelectSingleNode("//span[@class='heavyhuge']");
                var elements = node.Elements("a").ToArray();
                var firstName = WebUtility.HtmlDecode(elements[0].InnerText);
                var lastName = WebUtility.HtmlDecode(elements[1].InnerText);
                return new Tuple<string, string>(firstName, lastName);
            }
        }

        public string GetPostCode()
        {
            //Use http://www.doogal.co.uk/CreateRandomPostcode.php

            var request = (HttpWebRequest)WebRequest.Create("http://www.doogal.co.uk/CreateRandomPostcode.php");
            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var result = reader.ReadToEnd();
                return result.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }

        [Test]
        public string GetEmail()
        {
            //Use http://www.yopmail.com/en/email-generator.php

            var request = (HttpWebRequest)WebRequest.Create("http://www.yopmail.com/en/email-generator.php");
            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var html = reader.ReadToEnd();
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var node = doc.DocumentNode.SelectSingleNode("//input[@id='login']");
                return WebUtility.HtmlDecode(node.GetAttributeValue("value", string.Empty));
            }
        }
    }
}
