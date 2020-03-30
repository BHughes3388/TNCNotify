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
            authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCeeKnHz28M_ufJMHGmaUVNfY9KJ3GvxLA"));
        }

        public async Task<FirebaseAuthLink> LoginUser(string email, string password)
        {
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            SaveAuth(auth);
            Console.WriteLine("signed in new token: {0}", auth.FirebaseToken);
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
            Settings.Default.LoggedIn = true;
            Settings.Default.Save();
        }

        public async Task<string> GetFreshTokenAsync()
        {
            Console.WriteLine("token before: {0}", LoadAuth().FirebaseToken);
            var auth = new FirebaseAuthLink(authProvider, LoadAuth());

            auth = await auth.GetFreshAuthAsync();
            Console.WriteLine("token after: {0}", auth.FirebaseToken);

            SaveAuth(auth);

            return auth.FirebaseToken;
        }

        public Task<string> GetFreshToken()
        {
            //load token from storage
            var auth = new FirebaseAuthLink(authProvider, LoadAuth()); // LoadAuth returns FirebaseAuth, that can be saved in local storage

            auth.FirebaseAuthRefreshed += (s, e) => SaveAuth(e.FirebaseAuth);
            // use the token and let it refresh automatically (can be part of FirebaseOptions for access to Firebase DB)
            // Return token

            return Task.FromResult(auth.FirebaseToken);
        }

        public FirebaseAuth LoadAuth()
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
            Settings.Default.LoggedIn = false;
            Settings.Default.Save();

            return null;
        }

    }
}

