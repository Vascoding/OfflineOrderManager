using OfflineOrderManager.Models.Enums;

namespace OfflineOrderManager.Web.Helpers
{
    public static class SelectedOptionHelper
    {
        public static bool IsSelected(Status optionStatus, Status modelStatus) =>
            optionStatus == modelStatus;
    }
}
