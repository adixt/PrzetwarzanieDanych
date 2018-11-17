using System.Collections.Generic;

namespace DataProcessing
{
    public class OutputClass
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public int Year_of_Release { get; set; }
        public string Genre { get; set; }
        public decimal NA_Sales { get; set; }
        public decimal EU_Sales { get; set; }
        public decimal JP_Sales { get; set; }
        public decimal Other_Sales { get; set; }
        public decimal Global_Sales { get; set; }
        public string Publisher { get; set; }
        public int Critic_Score { get; set; }
        public int Critic_Count { get; set; }
        public int User_Score { get; set; }
        public int User_Count { get; set; }
        public string Rating { get; set; }
    }
    public class OutputClassEqualityComparer : IEqualityComparer<OutputClass>
    {
        public bool Equals(OutputClass x, OutputClass y)
        {
            if (x.Name == y.Name &&
                x.Platform == y.Platform &&
                x.Genre == y.Genre
                )
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(OutputClass obj)
        {
            var nameCode = obj.Name != null ? obj.Name.GetHashCode() : 0;
            var platformCode = obj.Platform != null ? obj.Platform.GetHashCode() : 0;
            var genreCode = obj.Genre != null ? obj.Genre.GetHashCode() : 0;

            var hCode = nameCode + platformCode + genreCode;
            return hCode;
        }
    }

}
