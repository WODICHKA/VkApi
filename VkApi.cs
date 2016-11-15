using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
namespace apivk
{
    public enum Sex : int { Female = 1, Male = 2 }
    public struct UserInfo
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public bool IsOnline;
        public bool IsFriend;
        public int FollowersCount;
        public Sex Sex;
        public bool CanWritePM;
        public bool CanPost;
        public void Parse (string data)
        {
            XmlReader xmlData = new XmlReader(data);
            ID = xmlData.GetIntData("id");
            FirstName = xmlData.GetData("first_name");
            LastName = xmlData.GetData("last_name");
            IsOnline = xmlData.GetBoolData("online");
            IsFriend = xmlData.GetBoolData("is_friend");
            FollowersCount = xmlData.GetIntData("followers_count");
            Sex = (Sex)xmlData.GetIntData("sex");
            CanWritePM = xmlData.GetBoolData("can_write_private_message");
            CanPost = xmlData.GetBoolData("can_post");
        }
    }
    public class VkApi
    {
        public const string methodsUrl = "https://api.vk.com/method";
        public double ApiVersion { get; } = 5.60;
        public string AccessToken { get; private set; }
        public void SetAccessToken (string token)
        {
            AccessToken = token;
        }
        public VkApi (string access_token = null)
        {
            if (access_token != null)
                SetAccessToken(access_token);
        }
        public UserInfo GetUserInfo (string IDorScreenName, string access_token = null,
            string fields = "sex,online,followers_count,can_write_private_message,is_friend,can_post")
        {
            UserInfo info = new UserInfo();
            string data = getResponse(string.Concat(methodsUrl, "/users.get.xml?",
                $"user_ids={IDorScreenName}&access_token={access_token}&v={ApiVersion}&fields={fields}"));
            info.Parse(data);
            return info;
        }
        private string getResponse (string url)
        {
            return new HttpRequest().Get(url).ToString();
        }
    }
}
