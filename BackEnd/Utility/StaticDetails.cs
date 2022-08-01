using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Utility
{
    public class StaticDetails
    {
        public const string StatusPending = "Pending Payment";
        public const string StatusSubmitted = "Submitted Payment Approved";
        public const string StatusInProcess = "In Process";
        public const string StatusRejected = "Payment Rejected";
        public const string StatusReady = "Ready for Pickup";
        public const string StatusInTransit = "In Transit";
        public const string StatusCancelled = "Cancelled";
        public const string StatusCompleted = "Completed";
    }
}
