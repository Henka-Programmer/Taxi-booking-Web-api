using Firebase.Auth;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.FirebaseManager
{
    public interface IFirebaseManager
    {
        Task<UserRecord> CreateUser(UserRecordArgs user, Dictionary<string, object> claims);
        Task<string> SinginWithEmailPassword(string email, string password);
    }
}
