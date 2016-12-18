using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudySite.Services;

namespace StudySite.Controllers
{
    [RoutePrefix("api/Messages")]
    public class MessagesController : ApiController
    {
        private MessagesService _messagesService = new MessagesService();

        [HttpGet]
        public string Get(string dateTime, int count)
        {
            return _messagesService.GetMessages(dateTime, count);
        }

        [HttpPost]
        public void Post(dynamic obj)
        {
            _messagesService.InsertMessage(obj.userName.Value, obj.message.Value);
        }
    }
}
