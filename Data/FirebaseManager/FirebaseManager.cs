using Firebase.Auth;
using Firebase.Database;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJSDotnet51.Data.FirebaseManager
{
    public class FirebaseManager : IFirebaseManager
    {
        public async Task<UserRecord> CreateUser(UserRecordArgs user, Dictionary<string, object> claims)
        {
            UserRecord userRecord = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.CreateUserAsync(user);
            await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);
             
            return userRecord;
        }

        public async Task<string> SinginWithEmailPassword(string email, string password)
        {
           
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAYvvsEylvLBcY60rHFeUb_09MAubW3N-A"));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            
            var firebase = new FirebaseClient(
              "https://fethi-510c0.firebaseio.com/",
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
              });
             
            
            
            return auth.FirebaseToken;
        }
    }
}
