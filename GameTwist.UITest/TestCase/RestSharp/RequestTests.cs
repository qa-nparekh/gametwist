using GTAutomation.Entities;
using GTAutomation.Framework.Configurations;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace GTAutomation.TestCase.RestSharp
{
    class RequestTests
    {
        APIPetData apiPetData = APIPetData.GetDetails;
        string petName;
        int id = 1001;
        int categoryId = 98765;
        string status = "available";

        [Test, Order(1)]
        public void PostRequestTest()
        {
            RestClient client = new RestClient(Configuration.APIEndPoint);
            var request = new RestRequest(new Uri(Configuration.APIEndPoint), Method.Post);
            request.AddHeader("accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            petName = apiPetData.PetName;

            var body = "{\r\n"
               + "  \"id\": "+id+",\r\n"
               + "  \"category\": {\r\n"
               + "    \"id\": " + categoryId + ",\r\n"
               + "    \"name\": \"net\"\r\n"
               + "  },\r\n"
               + "  \"name\": \""+petName+"\",\r\n"
               + "  \"photoUrls\": [\r\n"
               + "    \"string\"\r\n"
               + "  ],\r\n"
               + "  \"tags\": [\r\n"
               + "    {\r\n"
               + "      \"id\": 0,\r\n"
               + "      \"name\": \"string\"\r\n"
               + "    }\r\n"
               + "  ],\r\n"
               + "  \"status\": \""+ status + "\"\r\n"
               + "}";

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.AreEqual(response.StatusCode.GetHashCode(), 200);
            Console.WriteLine("post content : " +response.Content);
            Assert.That(response.Content.Contains(petName));
        }

        [Test, Order(2)]
        public void GetRequestTest()
        {
            RestClient client = new RestClient(Configuration.APIEndPoint);

            RestRequest request = new RestRequest(id.ToString(), Method.Get);
            
            var response = client.Execute(request);

            Console.WriteLine("Status code : " + response.StatusCode);
            Console.WriteLine("Response status : " + response.ResponseStatus);
            Console.WriteLine("Get content : " + response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var body = "{\r\n"
              + "  \"id\": " + id + ",\r\n"
              + "  \"category\": {\r\n"
              + "    \"id\": " + categoryId + ",\r\n"
              + "    \"name\": \"net\"\r\n"
              + "  },\r\n"
              + "  \"name\": \"" + petName + "\",\r\n"
              + "  \"photoUrls\": [\r\n"
              + "    \"string\"\r\n"
              + "  ],\r\n"
              + "  \"tags\": [\r\n"
              + "    {\r\n"
              + "      \"id\": 0,\r\n"
              + "      \"name\": \"string\"\r\n"
              + "    }\r\n"
              + "  ],\r\n"
              + "  \"status\": \"" + status + "\"\r\n"
              + "}";

            Assert.AreEqual(response.Content, body.Replace("\r\n","").Replace(" ",""));            
        }

        [Test, Order(3)]
        public void PutRequestTest()
        {
            RestClient client = new RestClient(Configuration.APIEndPoint);
            var request = new RestRequest(new Uri(Configuration.APIEndPoint), Method.Put);
            request.AddHeader("accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            APIPetData apiPetData1 = APIPetData.GetDetails;
            petName = apiPetData1.PetName;
            var body = "{\r\n"
               + "  \"id\": " + id + ",\r\n"
               + "  \"category\": {\r\n"
               + "    \"id\": " + categoryId + ",\r\n"
               + "    \"name\": \"string\"\r\n"
               + "  },\r\n"
               + "  \"name\": \"" + petName + "\",\r\n"
               + "  \"photoUrls\": [\r\n"
               + "    \"string\"\r\n"
               + "  ],\r\n"
               + "  \"tags\": [\r\n"
               + "    {\r\n"
               + "      \"id\": 0,\r\n"
               + "      \"name\": \"string\"\r\n"
               + "    }\r\n"
               + "  ],\r\n"
               + "  \"status\": \"" + status + "\"\r\n"
               + "}";

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Console.WriteLine("Put content : " + response.Content);
            Assert.That(response.Content.Contains(petName));
        }

        [Test, Order(4)]
        public void DeleteRequestTest()
        {
            RestClient client = new RestClient(Configuration.APIEndPoint);

            RestRequest request = new RestRequest(id.ToString(), Method.Delete);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Console.WriteLine("Delete content : " + response.Content);
            Console.WriteLine("Status code : " + response.StatusCode);
            Console.WriteLine("Response status : " + response.ResponseStatus);

            RestRequest request1 = new RestRequest("10556", Method.Delete);

            var response1 = client.Execute(request1);

            Console.WriteLine("Delete content : " + response1.Content);
            Console.WriteLine("Status code : " + response1.StatusCode.GetHashCode());
            Console.WriteLine("Response status : " + response1.ResponseStatus);

            Assert.That(response1.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.AreEqual(response1.StatusCode.GetHashCode(), 404);
        }
    }
}
