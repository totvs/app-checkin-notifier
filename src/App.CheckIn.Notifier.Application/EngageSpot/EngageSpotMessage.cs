using Newtonsoft.Json;

namespace App.CheckIn.Notifier.Application.EngageSpot
{
    public class EngageSpotMessage
    {
        [JsonProperty("campaign_name")]
        public string CampaignName { get; set; }

        [JsonProperty("notification")]
        public EngageSpotNotification Notification { get; set; }

        [JsonProperty("send_to")]
        public string SendTo { get; set; }

        [JsonProperty("identifiers")]
        public string[] Identifiers { get; set; }

        public static EngageSpotMessage BuildDefaultMessage(string identifier, string title, string message)
        {
            return new EngageSpotMessage
            {
                CampaignName = "app-checkin-test",
                Identifiers = new[] { identifier },
                SendTo = "identifiers",
                Notification = new EngageSpotNotification
                {
                    Title = title,
                    Message = message,
                    Icon = "http://engagespot.co/blog/wp-content/uploads/2017/02/wpengage.jpg",
                    Url = "http://google.com"
                }
            };
        }
    }

    public class EngageSpotNotification
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
