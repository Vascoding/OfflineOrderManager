using OfflineOrderManager.Models.Enums;
using System.Collections.Generic;

namespace OfflineOrderManager.Web.Helpers
{
    public static class HtmlHeleper
    {
        public static bool IsSelected(Status optionStatus, Status modelStatus) =>
            optionStatus == modelStatus;

        public static string GetClassByStatus(Status status)
        {
            var mapStatusToClass = new Dictionary<Status, string>()
            {
                { Status.Accepted, "bg-success" },
                { Status.Waiting, "bg-warning" },
                { Status.Refused, "bg-danger" }
            };

            return mapStatusToClass[status];
        }
    }
}
