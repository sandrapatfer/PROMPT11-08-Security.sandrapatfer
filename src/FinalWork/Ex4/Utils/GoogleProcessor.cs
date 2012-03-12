using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Json;
using Ex4.Models;
using System.Web.Script.Serialization;
using System.Web.Mvc;

namespace Ex4.Utils
{
    public class GoogleProcessor
    {
        private HttpClient _httpClient;

        public const string GoogleClientSecret = "1HuwNjxEeNFIMd090yL1uR_X";
        public const string GoogleUser = "646897896780.apps.googleusercontent.com";
        public const string AppUrl = "http://localhost:49457/Tasks";


        public GoogleProcessor(string code)
        {
            _httpClient = new HttpClient();

            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("code", code));
            queryParams.Add(new KeyValuePair<string, string>("redirect_uri", AppUrl));
            queryParams.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            queryParams.Add(new KeyValuePair<string, string>("client_id", GoogleUser));
            queryParams.Add(new KeyValuePair<string, string>("client_secret", GoogleClientSecret));
            var form = new FormUrlEncodedContent(queryParams);            
            var postResult = _httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", form).Result;
            var stream = postResult.Content.ReadAsStreamAsync().Result;
            var value = JsonValue.Load(stream);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue((string)value["token_type"], (string)value["access_token"]);
        }

        public List<GoogleTask> GetFirstListTasks()
        {
            var stream1 = _httpClient.GetAsync("https://www.googleapis.com/tasks/v1/users/@me/lists?alt=json&prettyPrint=true").
                Result.Content.ReadAsStreamAsync().Result;
            var value2 = JsonValue.Load(stream1);
            if (value2["items"].Count == 0)
            {
                return null;
            }

            var listId = (string)value2["items"][0]["id"];
            var tasksUrl = string.Format("https://www.googleapis.com/tasks/v1/lists/{0}/tasks?alt=json", listId);
            var result = _httpClient.GetAsync(tasksUrl).Result;
            var stream2 = result.Content.ReadAsStreamAsync().Result;
            var value3 = JsonValue.Load(stream2);

            var taskArray = value3["items"] as JsonArray;
            var tasks = new List<GoogleTask>();
            var jsonSerializer = new JavaScriptSerializer();
            foreach (var jTask in taskArray)
            {
                GoogleTask gTask = jsonSerializer.Deserialize<GoogleTask>(jTask.ToString());
                tasks.Add(gTask);
            }
            return tasks;
        }

        public static ActionResult Redirect()
        {
            return new RedirectResult("https://accounts.google.com/o/oauth2/auth?" +
                    "response_type=code&client_id=" + GoogleUser +
                    "&redirect_uri=" + HttpUtility.UrlEncode(AppUrl) +
                    "&scope=" + HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly"));
        }
    }
}
