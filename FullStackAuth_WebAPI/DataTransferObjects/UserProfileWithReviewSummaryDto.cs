using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class UserProfileWithReviewSummaryDto
    {
        public double TotalReviewsJobs { get; set; }
        public double AvgOverallScore { get; set; }
        public double AvgAdherence { get; set;}
        public double AvgQuality { get; set;}
        public double AvgTimeliness { get; set; }
        public double AvgWouldRepeat { get; set; }
        public double AvgCommunication { get; set; }
        public double? AvgAdaptability { get; set; }
        public bool IsWorker { get; set; }

        public UserForProfileDto User { get; set; }

    }
}
 