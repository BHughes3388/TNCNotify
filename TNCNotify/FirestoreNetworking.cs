using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using Grpc.Core;
using Firebase.Auth;
using WriteResult = Google.Cloud.Firestore.WriteResult;

namespace TNCNotify
{
    class FirestoreNetworking
    {
        String project = "tncnotify";
        //FirebaseAuthService authService;
        FirestoreDb db;



        public async Task Start()
        {
            FirebaseAuthService authService = new FirebaseAuthService();

            var firebaseToken = await authService.GetFreshTokenAsync();
            var credential = GoogleCredential.FromAccessToken(firebaseToken);

            Channel channel = new Channel(
            FirestoreClient.DefaultEndpoint.Host, FirestoreClient.DefaultEndpoint.Port,
            credential.ToChannelCredentials());
            FirestoreClient client = FirestoreClient.Create(channel);

            db = FirestoreDb.Create(project, client);
        }

        public async Task<List<Machine>> GetMachines()
        {
            List<Machine> machines = new List<Machine>();

            CollectionReference collection = db.Collection("Machines");
            FirebaseAuthService authService = new FirebaseAuthService();
            var userUID = await authService.GetUserUID();
            Query userMachinesQuery = collection.WhereEqualTo("Creator", userUID);

            QuerySnapshot userMachines = await userMachinesQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot document in userMachines.Documents)
            {
                // Do anything you'd normally do with a DocumentSnapshot
                Machine machine = document.ConvertTo<Machine>();
                machines.Add(machine);
                //Console.WriteLine($"{machine.Name}: {machine.Ip}");
            }

            return machines;
        }

        public async Task UpdateMachine(Machine machine)
        {

            FirebaseAuthService authService = new FirebaseAuthService();
            var auth = authService.LoadAuth();
            if(auth.IsExpired())
            {
                Console.WriteLine("token is expired");
                await Start();
                List<Machine> machines = await GetMachines();
                Console.WriteLine("First machines: {0}", machines[0]);
            }
            //else
            //{
                DocumentReference document = machine.Reference;
                try
                {
                    WriteResult result = await document.SetAsync(machine, SetOptions.MergeAll);
                    var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    var updateDate = TimeZoneInfo.ConvertTimeFromUtc(result.UpdateTime.ToDateTime(), easternZone);
                    Console.WriteLine("Updated at: {0}", updateDate);

                }
                catch (Exception ex)
                {

                    Console.WriteLine("exception: {0}", ex.ToString());
                }

            //}
            
        }

    }
}
