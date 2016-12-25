using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using StudySite.Models;
using StudySite.Services;

namespace StudySite.Controllers
{
    [RoutePrefix("")]
    public class AuthController : Controller
    {
        UserService _userService = new UserService();

        [Route("CompleteAuthorization")]
        public async Task<ActionResult> CompleteAuthorization(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            string authModelSerialized = GetAuthModelSerialized(code);

            var authModel = (AuthorizationModel)JsonConvert.DeserializeObject(authModelSerialized, typeof(AuthorizationModel));

            UserModel user = await GetUser(authModel.access_token);
            string guid = _userService.SaveUser(user);
            HttpContext.Response.Cookies["guid"].Value = guid;
            return RedirectToAction("Index", "Home");
        }

        private async Task<UserModel> GetUser(string accessToken)
        {
            var request = (HttpWebRequest)WebRequest.Create($@"https://www.googleapis.com/oauth2/v1/userinfo?access_token={accessToken}");
            request.Method = "GET";

            WebResponse response = await request.GetResponseAsync();
            var encoding = ASCIIEncoding.UTF8;
            string responseText = string.Empty;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }

            return (UserModel)(JsonConvert.DeserializeObject(responseText, typeof(UserModel)));
        }

        private string GetAuthModelSerialized(string code)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            string postData = $@"code={code}&client_id={clientId}&client_secret={clientSecret}&redirect_uri={redirectUrl}&grant_type=authorization_code";
            byte[] byteArr = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = byteArr.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteArr, 0, byteArr.Length);
            }

            WebResponse response = request.GetResponse();
           
            var encoding = ASCIIEncoding.ASCII;
            string responseText = string.Empty;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }

            return responseText;
        }

        [HttpGet]
        public ActionResult FailAuthorization(string error)
        {
            return View("Error");
        }
    }
}
