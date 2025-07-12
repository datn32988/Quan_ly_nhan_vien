
namespace Domain.Common
{
    public static class WorkTimeConstants
    {
        public const int FullDayRequiredHours = 9;
        public const int HalfDayRequiredHours = 4;
        public static TimeSpan EarliestStartTime = new TimeSpan(7, 0, 0); 
        public static TimeSpan LatestEndTime = new TimeSpan(19, 0, 0); 
        public static TimeSpan LateArrivalThreshold = new TimeSpan(10, 0, 0);
        public static TimeSpan EarlyDepartureThreshold = new TimeSpan(16, 0, 0); 
    }
}
