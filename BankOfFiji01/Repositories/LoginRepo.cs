using BankOfFiji01.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BankOfFiji01.Repositories
{
    public class LoginRepo
    {
        /// <summary>
        /// Sends user input from login for to API for verification and validation
        /// </summary>
        /// <param name="info"> Contains a username and password </param>
        /// <returns> A string which contains the validation message once verifcation has been processed. </returns>
        public static async Task<string> ValidateUser(Login info)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/authenticate", httpContent);

            var responseString = await response.Content.ReadAsStringAsync();

            string Trim = responseString.Trim('"');

            return Trim;
        }

        /// <summary>
        /// Sends user input from login for to API for verification and validation
        /// </summary>
        /// <param name="info"> Contains a username and password </param>
        /// <returns> A string which contains the validation message once verifcation has been processed. </returns>
        public static async Task<UserDetails> RetreiveIDs(Login info)
        {
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(info);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:55303/idrequest", httpContent);

            UserDetails responseString = null;
            var jsonString = await response.Content.ReadAsStringAsync();
            responseString = JsonConvert.DeserializeObject<UserDetails>(jsonString);
            return responseString;
        }
    }
}