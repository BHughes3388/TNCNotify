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

        public async Task<FirestoreDb> DataBase()
        {
            FirebaseAuthService authService = new FirebaseAuthService();

            var auth = authService.LoadAuth();

            var firebaseToken = auth.FirebaseToken;

            if (auth.IsExpired())
            {
                firebaseToken = await authService.GetFreshTokenAsync();
            }

            var credential = GoogleCredential.FromAccessToken(firebaseToken);

            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.Host, FirestoreClient.DefaultEndpoint.Port, credential.ToChannelCredentials());

            FirestoreClient client = FirestoreClient.Create(channel);

            return FirestoreDb.Create(project, client);

        }

        public async Task<List<Machine>> GetMachines()
        {
            List<Machine> machines = new List<Machine>();

            FirestoreDb db = await DataBase();
            CollectionReference collection = db.Collection("Machines");

            FirebaseAuthService authService = new FirebaseAuthService();
            var userUID = await authService.GetUserUID();

            Query userMachinesQuery = collection.WhereEqualTo("Creator", userUID);
            QuerySnapshot userMachines = await userMachinesQuery.GetSnapshotAsync();

            foreach (DocumentSnapshot document in userMachines.Documents)
            {
                Machine machine = document.ConvertTo<Machine>();
                machines.Add(machine);
                //Console.WriteLine($"{machine.Name}: {machine.Ip}");
            }

            return machines;
        }

        public async Task UpdateMachine(Machine machine)
        {
            FirestoreDb db = await DataBase();
            DocumentReference document = db.Document($"Machines/{machine.ReferenceId}");

            try
            {
                WriteResult result = await document.SetAsync(machine, SetOptions.MergeAll);

                var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                var updateDate = TimeZoneInfo.ConvertTimeFromUtc(result.UpdateTime.ToDateTime(), easternZone);

                Console.WriteLine("{0} Updated at: {1}", machine.Name, updateDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateMachine Exception: {0}", ex.ToString());
            }

        }

        public async Task SaveError(Error error)
        {
            FirestoreDb db = await DataBase();
            CollectionReference collection = db.Collection("Errors");

            try
            {
                DocumentReference document = await collection.AddAsync(error);
                Console.WriteLine("Error Saved ");
                //DocumentSnapshot snapshot = await document.GetSnapshotAsync();
                //Error snapshotError = snapshot.ConvertTo<Error>();
                //Console.WriteLine("Snapshot of error: {0}", snapshotError.ErrorText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveError Exception: {0}", ex.ToString());
            }
        }

    }
}
