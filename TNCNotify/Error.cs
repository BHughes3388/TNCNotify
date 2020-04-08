using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace TNCNotify
{
    [FirestoreData]
    public class Error
    {
        [FirestoreDocumentId]
        public string ReferenceId { get; set; }

        [FirestoreDocumentId]
        public DocumentReference Reference { get; set; }

        [FirestoreDocumentCreateTimestamp]
        public Timestamp CreateTime { get; set; }

        [FirestoreDocumentUpdateTimestamp]
        public Timestamp UpdateTime { get; set; }

        [FirestoreDocumentReadTimestamp]
        public Timestamp ReadTime { get; set; }

        [FirestoreProperty]
        public string MachineId { get; set; }

        [FirestoreProperty]
        public string ErrorNumber { get; set; }

        [FirestoreProperty]
        public string ErrorText { get; set; }

        [FirestoreProperty]
        public string ErrorGroup { get; set; }

        [FirestoreProperty]
        public string ErrorClass { get; set; }
    }
}
