using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace VKClient
{
    class Program
    {
        const string appId = "7175350";
        const string access_token = "fc0f42effc0f42effc0f42ef68fc623e59ffc0ffc0f42efa1a6dc0532d35b859091489d";
        const int myId = 167809411;
        const string myToken = "L0IjMZTxVdreb3YRuaRj";

        private static Dictionary<int, string> Attitudes = new Dictionary<int, string>
        {
            {0, "не указано" },
            {1, "резко негативное" },
            {2, "негативное" },
            {3, "компромиссное" },
            {4, "нейтральное" },
            {5, "положительное" },
        };

        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            await GetFriendOnline(httpClient);
            await GetUsersInfoById(1, httpClient);

            //var authString = $"https://oauth.vk.com/authorize?client_id={appId}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.52";
            //authString = authString.Replace("&", "^&");
            //Process.Start(new ProcessStartInfo(
            //"cmd",
            //$"/c start {authString}")
            //{ CreateNoWindow = true });
        }

        public static async Task GetFriendOnline(HttpClient httpClient)
        {
            var request = $"https://api.vk.com/method/friends.getOnline?user_id={myId}&v=5.89&access_token={myToken}";
            var response = await httpClient.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var friendsResponse = JsonConvert.DeserializeObject<IdListResponse>(content);
                foreach (var user in friendsResponse.response)
                {
                    await GetUsersInfoById(user, httpClient);
                }
            }
        }

        public static async Task GetUsersInfoById(int id, HttpClient httpClient)
        {
            var request = $"https://api.vk.com/method/users.get?user_id={id}&v=5.89&access_token={access_token}&fields=personal";
            var response = await httpClient.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserResponse>(content);
                var alcoholAttitude = user.response[0].personal?.alcohol ?? 0;
                Console.WriteLine($"{user.response[0].first_name}, {user.response[0].last_name}, отношение к алкоголю: {Attitudes[alcoholAttitude]}");
            }
        }
    }
}
