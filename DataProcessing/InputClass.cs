using System.Collections.Generic;

namespace DataProcessing
{
    public class InputClass
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Year_of_Release { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public decimal NA_Sales { get; set; }
        public decimal EU_Sales { get; set; }
        public decimal JP_Sales { get; set; }
        public decimal Other_Sales { get; set; }
        public decimal Global_Sales { get; set; }
        public string Critic_Score { get; set; }
        public string Critic_Count { get; set; }
        public string User_Score { get; set; }
        public string User_Count { get; set; }
        public string Developer { get; set; }
        public string Rating { get; set; }
    }

    public class InputClassEqualityComparer : IEqualityComparer<InputClass>
    {       
        public bool Equals(InputClass x, InputClass y)
        {
            if (x.Name == y.Name &&
                x.Platform == y.Platform &&
                x.Year_of_Release == y.Year_of_Release &&
                x.Genre == y.Genre
                )
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(InputClass obj)
        {
            var nameCode = obj.Name != null ? obj.Name.GetHashCode()  : 0;
            var platformCode = obj.Platform != null ? obj.Platform.GetHashCode()  : 0;
            var yearCode = obj.Year_of_Release != null ? obj.Year_of_Release.GetHashCode()  : 0;
            var genreCode = obj.Genre != null ? obj.Genre.GetHashCode()  : 0;

            var hCode = nameCode + platformCode + yearCode + genreCode;
            return hCode;
        }
    }
}
