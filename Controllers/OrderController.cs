using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderItem.Models;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderItem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public static HttpClient client = new HttpClient();
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost("{menuItemid}")]
        public Cart Post(int menuItemid)
        {
            string token = GetToken("http://52.184.92.234/api/Token");

           Cart orderItem = new Cart();
            orderItem.Id = 1;
            orderItem.userId = 1;
            orderItem.menuItemId = menuItemid;
            orderItem.menuItemName = getMenuName(menuItemid, token).ToString();
            return orderItem;

            
        }
        static string GetToken(string url)
        {
            var user = new User { Name = "Ankit", Password = "Ankit123" };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client= new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(name);
                return details.token;
            }
        }
        
        // POST api/<OrderController>
        

        private string getMenuName(int menuItemid,string token)
        {
            string name;
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://52.184.92.234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response=client.GetAsync("api/MenuItem/"+menuItemid).Result;
                
                    string name1 = response.Content.ReadAsStringAsync().Result;
                    name = JsonConvert.DeserializeObject<string>(name1);
                
                
            }
            return name;

        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
