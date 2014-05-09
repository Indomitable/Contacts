using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Contacts.Logic.Entities;
using Contacts.Logic.Managers;

namespace Contacts.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            return PartialView();
        }


        [HttpGet]
        public ActionResult GetContacts()
        {
            var result = ContactManager.GetContacts();
            return JsonNet(result);
        }

        [HttpGet]
        public ActionResult GetContactInfo(long id)
        {
            return JsonNet(new ContactInfo(ContactManager.GetContact(id)));
        }

        [HttpGet]
        public ActionResult GetContact(long id)
        {
            var result = ContactManager.GetContact(id);
            return JsonNet(result);
        }

        [HttpGet]
        public FileStreamResult GetAvatar(long id)
        {
            var image = ContactManager.GetContactAvatar(id);
            return File(image, "image/png");
        }


        [HttpPost]
        public ActionResult SaveOrUpdateContact(Contact contact)
        {
            try
            {
                if (contact.Id > 0)
                    ContactManager.UpdateContact(contact);
                else
                    ContactManager.StoreContact(contact);

                return JsonNet(new { Result = true });
            }
            catch (Exception x)
            {
                return JsonNet(new { Result = false, x.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteContact(long id)
        {
            try
            {
                ContactManager.DeleteContact(id);
                return JsonNet(new { Result = true });
            }
            catch (Exception x)
            {
                return JsonNet(new { Result = false, x.Message });
            }
        }

        [HttpPost]
        public ActionResult UploadImage(long id, HttpPostedFileBase file)
        {
            try
            {
                if (id > 0 && file != null && file.ContentLength > 0)
                {
                    using (BinaryReader b = new BinaryReader(file.InputStream))
                    {
                        byte[] content = b.ReadBytes(file.ContentLength);
                        ContactManager.ChangePhoto(id, content);
                    }
                }
                return JsonNet(new { Result = true });
            }
            catch (Exception x)
            {
                return JsonNet(new { Result = false, x.Message });
            }
        }
//
//        [HttpGet]
//        public ActionResult SearchCompletion(string val)
//        {
//            return JsonNet(ContactManager.GetSearchComplete(val));
//        }
    }
}
