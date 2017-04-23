using System.Runtime.Serialization;

namespace SQLAccess.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string user { get; set; }

        [DataMember]
        public int sol { get; set; }

        [DataMember]
        public int acc { get; set; }

        [DataMember]
        public string ques { get; set; }

        [DataMember]
        public string password { get; set; }
    }

    [DataContract]
    public class UserLang
    {
        [DataMember]
        public string user { get; set; }

        [DataMember]
        public string lang { get; set; }

        [DataMember]
        public string integer { get; set; }

        [DataMember]
        public string strings { get; set; }

        [DataMember]
        public string ifelse { get; set; }

        [DataMember]
        public string looper { get; set; }

        [DataMember]
        public string printer { get; set; }

        [DataMember]
        public string input { get; set; }

        [DataMember]
        public string breaker { get; set; }

        [DataMember]
        public string continuex { get; set; }
    }
}