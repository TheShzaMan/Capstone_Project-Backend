using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class DisplayReviewSummaryDto
    {
        public int TotalReviewsJobs { get; set; }
        public int AvgOverallScore { get; set; }
        public int AvgAdherence { get; set;}
        public int AvgQuality { get; set;}
        public int AvgTimeliness { get; set; }
        public int AvgWouldRepeat { get; set; }
        public int AvgCommunication { get; set; }
        public int? AvgAdaptability { get; set; }
        public bool IsWorker { get; set; }

    }
}
 