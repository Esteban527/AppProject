namespace AppProject.Web.Data.Entities
{
    public class Chronometer
    {
        public DateTime StartTime { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public List<TimeSpan> LapTimes { get; set; } = new List<TimeSpan>();
        public bool IsRunning { get; set; }
    }
}
