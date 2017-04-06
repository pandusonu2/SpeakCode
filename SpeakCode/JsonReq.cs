using System;
using System.Collections.Generic;
using System.IO;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.UI.Popups;

namespace SpeakCode
{
    public class JsonReq
    {
        public async static Task<Question> ques(string pcode)
        {
            var http = new System.Net.Http.HttpClient();
            var response = await http.GetAsync("http://api.pandusonu.com/problems/" + pcode);
            var result = await response.Content.ReadAsStringAsync();
            var serial = new DataContractJsonSerializer(typeof(Question));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            Question ret = (Question)serial.ReadObject(ms);
            return ret;
        }
        public async static Task<List<Question>> ques()
        {
            var http = new System.Net.Http.HttpClient();
            var response = await http.GetAsync("http://api.pandusonu.com/problems/");
            var result = await response.Content.ReadAsStringAsync();
            var serial = new DataContractJsonSerializer(typeof(List<Question>));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            List<Question> ret = (List<Question>)serial.ReadObject(ms);
            return ret;
        }
        public async static Task<List<Language>> langlist()
        {
            var http = new System.Net.Http.HttpClient();
            var response = await http.GetAsync("http://api.pandusonu.com/languages");
            var result = await response.Content.ReadAsStringAsync();
            var serial = new DataContractJsonSerializer(typeof(List<Language>));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            List<Language> ret = (List<Language>)serial.ReadObject(ms);
            return ret;
        }
        public async static Task<string> postReq(string pcode, string submission)
        {
            Uri reqUti = new Uri("http://api.pandusonu.com/submit");
            dynamic dynamicJson = new ExpandoObject();
            dynamicJson.pcode = pcode;
            dynamicJson.user_source_code = submission;
            string json = "{\"pcode\":\"" + pcode + "\",\"user_source_code\":\"";
            foreach(char c in submission.ToCharArray())
            {
                if (c == '"')
                    json += "\\\"";
                else if (c == '\\')
                    json += "\\";
                else if (c == '\n')
                    continue;
                else json += c;
            }    
            json += "\"}";
            var http = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage respon = await http.PostAsync(reqUti, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responseJsonText = await respon.Content.ReadAsStringAsync();
            await new MessageDialog(responseJsonText).ShowAsync();
            return responseJsonText;
        }
    }
    [DataContract]
    public class Status
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string status_code { get; set; }
        [DataMember]
        public string error_desc { get; set; }  
    }
    [DataContract]
    public class Language
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string lang_code { get; set; }
        [DataMember]
        public string equal { get; set; }
        [DataMember]
        public string greater { get; set; }
        [DataMember]
        public string smaller { get; set; }
        [DataMember]
        public string double_equals { get; set; }
        [DataMember]
        public string and { get; set; }
        [DataMember]
        public string or { get; set; }
        [DataMember]
        public string @int { get; set; }
        [DataMember]
        public string @string { get; set; }
        [DataMember]
        public string print { get; set; }
        [DataMember]
        public string read { get; set; }
        [DataMember]
        public string function { get; set; }
        [DataMember]
        public string @return { get; set; }
        [DataMember]
        public string if_else { get; set; }
        [DataMember]
        public string @for { get; set; }
        [DataMember]
        public string @while { get; set; }
        [DataMember]
        public string do_while { get; set; }
        [DataMember]
        public string @switch { get; set; }
        [DataMember]
        public string @continue { get; set; }
        [DataMember]
        public string @break { get; set; }
        [DataMember]
        public string start_bkt { get; set; }
        [DataMember]
        public string end_bkt { get; set; }
        [DataMember]
        public string created_at { get; set; }
        [DataMember]
        public string updated_at { get; set; }
    }

    [DataContract]
    public class Question
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string pname { get; set; }
        [DataMember]
        public string pcode { get; set; }
        [DataMember]
        public string statement { get; set; }
        [DataMember]
        public bool state { get; set; }
        [DataMember]
        public string created_at { get; set; }
        [DataMember]
        public string updated_at { get; set; }
        [DataMember]
        public object submissions_count { get; set; }
    }
}
