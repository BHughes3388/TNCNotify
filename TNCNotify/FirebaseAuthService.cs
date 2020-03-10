using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;
using TNCNotify.Properties;



namespace TNCNotify
{
    class FirebaseAuthService
    {
        IFirebaseAuthProvider authProvider; 

        public FirebaseAuthService()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBKpPKz-i778h50HSLn3F8frz7zjINy-7g"));
        }

        public async Task<FirebaseAuthLink> LoginUser(string email, string password)
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            SaveAuth(auth);
            // save the new token each time it is refreshed
            auth.FirebaseAuthRefreshed += (s, e) => SaveAuth(e.FirebaseAuth);
            // use the token and let it refresh automatically (can be part of FirebaseOptions for access to Firebase DB)
            return await auth.GetFreshAuthAsync();
        }


        public async Task<FirebaseAuthLink> SignupUser(string email, string password)
        {
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

            SaveAuth(auth);
            // save the new token each time it is refreshed
            auth.FirebaseAuthRefreshed += (s, e) => SaveAuth(e.FirebaseAuth);
            // use the token and let it refresh automatically (can be part of FirebaseOptions for access to Firebase DB)
            return await auth.GetFreshAuthAsync();
        }


        private void SaveAuth(FirebaseAuth auth)
        {
            string json = JsonConvert.SerializeObject(auth);
            Settings.Default.FirebaseAuthJson = json;
            //Preferences.Set("logged", true);
        }

        public Task<string> GetFreshToken()
        {
            //load token from storage
            var auth = new FirebaseAuthLink(authProvider, LoadAuth()); // LoadAuth returns FirebaseAuth, that can be saved in local storage
                                                                       // save the new token each time it is refreshed
            auth.FirebaseAuthRefreshed += (s, e) => SaveAuth(e.FirebaseAuth);
            // use the token and let it refresh automatically (can be part of FirebaseOptions for access to Firebase DB)
            // Return token

            return Task.FromResult(auth.FirebaseToken);
        }

        private FirebaseAuth LoadAuth()
        {
            string json = Settings.Default.FirebaseAuthJson;

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<FirebaseAuth>(json);
            }

        }

        public Task<string> GetUserUID()
        {
            var auth = LoadAuth();

            if (auth != null)
            {
                return Task.FromResult(auth.User.LocalId);
            }

            return null;
        }

        public Task Signout()
        {
            //Preferences.Clear();

            return null;
        }

    }
}

