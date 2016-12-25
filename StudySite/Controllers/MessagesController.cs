using System;
using System.Web.Http;
using StudySite.Services;

namespace StudySite.Controllers
{
    [RoutePrefix("api/Messages")]
    public class MessagesController : ApiController
    {
        private MessagesService _messagesService = new MessagesService();

        [HttpGet]
        public string Get(int count, int timeZone, string dateTime = null)
        {
            return _messagesService.GetMessages(dateTime, count, timeZone);
        }

        [HttpPost]
        public void Post(dynamic obj)
        {
            try
            {
                _messagesService.InsertMessage(obj.userGuid.Value, obj.message.Value);
            }
            catch (ArgumentException)
            {
                NotFound();
            }
        }
    }
}
