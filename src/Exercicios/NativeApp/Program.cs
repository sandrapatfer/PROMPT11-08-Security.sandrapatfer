using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Json;

namespace NativeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://accounts.google.com/o/oauth2/auth?response_type=code&" +
  "client_id=646897896780-dd9ks620qdt6cubmtnddb422n2fj25e7.apps.googleusercontent.com&" +
  "redirect_uri=" + HttpUtility.UrlEncode("urn:ietf:wg:oauth:2.0:oob") + "&" +
  "scope=" + HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly") + "&" +
  "approval_prompt=force";
            var proc = System.Diagnostics.Process.Start("iexplore.exe", url);
/*            var title = (string)proc.MainWindowTitle;
            var ex = new Regex("Success code=([^/s]*)/s");
            var match = ex.Match(title);*/

            Console.Write("Enter the code:");
            var code = Console.ReadLine();

            var queryParams = new List<KeyValuePair<string, string>>();
            queryParams.Add(new KeyValuePair<string, string>("code", code));
            queryParams.Add(new KeyValuePair<string, string>("redirect_uri", "urn:ietf:wg:oauth:2.0:oob"));
            queryParams.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            queryParams.Add(new KeyValuePair<string, string>("client_id", "646897896780-dd9ks620qdt6cubmtnddb422n2fj25e7.apps.googleusercontent.com"));
            queryParams.Add(new KeyValuePair<string, string>("client_secret", "P3gQbdl8X4G3qb9Y6U9fMb5i"));
            var form = new FormUrlEncodedContent(queryParams);
            var httpClient = new HttpClient();
            var postResult = httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", form).Result;
            var stream = postResult.Content.ReadAsStreamAsync().Result;
            var value = JsonValue.Load(stream);

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue((string)value["token_type"], (string)value["access_token"]);
            var stream1 = httpClient.GetAsync("https://www.googleapis.com/tasks/v1/users/@me/lists?alt=json&prettyPrint=true").
                Result.Content.ReadAsStreamAsync().Result;
            var value2 = JsonValue.Load(stream1);

            Console.WriteLine(value2["items"][0]["title"]);
        }
    }
}
