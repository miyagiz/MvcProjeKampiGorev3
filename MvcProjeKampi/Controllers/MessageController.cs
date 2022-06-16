using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntitiyFramework;
using EntitiyLayer.Concreate;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message

        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();


        public ActionResult Inbox()
        {
            var messageListIn = mm.GetListInbox();
            return View(messageListIn);
        }

        public ActionResult Sendbox()
        {
            var messageListSend = mm.GetListSendbox();
            return View(messageListSend);
        }

        public ActionResult Drafts()
        {
            var draftMessageList = mm.GetDraftMessageList();
            return View(draftMessageList);
        }

        public ActionResult InboxIsRead()
        {
            var inboxIsRead = mm.GetIsReadMessageList();
            return View(inboxIsRead);
        }



        public ActionResult InboxIsNotRead()
        {
            var inboxIsNotRead = mm.GetIsNotReadMessageList();
            return View(inboxIsNotRead);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messageValidator.Validate(p);

            if (results.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.MessageStatus = true;
                mm.MessageAdd(p);

                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }


        public ActionResult NewMessageDraft(Message p)
        {
            ValidationResult results = messageValidator.Validate(p);

            if (results.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.MessageStatus = false;
                mm.MessageAdd(p);

                return RedirectToAction("Drafts");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult GetMessageDetails(int id)
        {
            var messageValues = mm.GetById(id);

            if (messageValues.IsRead == false)
            {
                messageValues.IsRead = true;
            }

            mm.MessageUpdate(messageValues);

            return View(messageValues);
        }

        public ActionResult IsRead(int id)
        {
            var results = mm.GetById(id);

            mm.MessageUpdate(results);
            return RedirectToAction("InboxIsRead");
        }
    }
}