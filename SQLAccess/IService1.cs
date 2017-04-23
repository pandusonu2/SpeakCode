using SQLAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SQLAccess
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IList<User> QueryUser();

        [OperationContract]
        Boolean AddUser(string user, string password);

        [OperationContract]
        Boolean UpdProgress(User u);

        [OperationContract]
        Boolean AddLang(UserLang l);

        [OperationContract]
        IList<UserLang> GetLangs(string user);
    }
}
