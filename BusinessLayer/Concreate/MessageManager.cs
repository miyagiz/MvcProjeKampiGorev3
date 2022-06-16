using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitiyLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetById(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetListInbox()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com" && x.MessageStatus == true);
        }

        public List<Message> GetListSendbox()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com" && x.MessageStatus == true);
        }

        public List<Message> GetDraftMessageList()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com" && x.MessageStatus == false);
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }

        public List<Message> GetIsReadMessageList()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com" && x.MessageStatus == true && x.IsRead == true);
        }

        public List<Message> GetIsNotReadMessageList()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com" && x.MessageStatus == true && x.IsRead == false);
        }
    }
}
