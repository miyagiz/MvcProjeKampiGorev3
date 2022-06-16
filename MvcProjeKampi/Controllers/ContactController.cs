using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact

        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv = new ContactValidator();

        public ActionResult Index()
        {
            var contacValues = cm.GetList();
            return View(contacValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = cm.GetById(id);
            return View(contactValues);
        }

        public PartialViewResult MessageListMenu()
        {
            var contactCount = cm.GetList().Count();
            ViewBag.contactCount = contactCount;

            var inboxCount = mm.GetListInbox().Count();
            ViewBag.inboxCount = inboxCount;

            var sendboxCount = mm.GetListSendbox().Count();
            ViewBag.sendboxCount = sendboxCount;

            var draftMessageCount = mm.GetDraftMessageList().Count();
            ViewBag.draftMessageCount = draftMessageCount;

            var inboxIsRead = mm.GetIsReadMessageList().Count();
            ViewBag.inboxIsRead = inboxIsRead;

            var inboxNotIsRead = mm.GetIsNotReadMessageList().Count();
            ViewBag.inboxNotIsRead = inboxNotIsRead;


            return PartialView();
        }


    }
}