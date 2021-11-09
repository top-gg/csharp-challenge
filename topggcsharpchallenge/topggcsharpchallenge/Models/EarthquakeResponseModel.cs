using System;
using System.Collections.Generic;

namespace topggcsharpchallenge.Models
{
    public class EarthquakeResponseModel
    {
        public DateTime Time { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Depth { get; set; }
        public double Mag { get; set; }
        public string MagType { get; set; }
        public int Nst { get; set; }
        public int Gap { get; set; }
        public double Dmin { get; set; }
        public double Rms { get; set; }
        public string Net { get; set; }
        public string Id { get; set; }
        public DateTime Updated { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public double HorizontalError { get; set; }
        public double DepthError { get; set; }
        public double MagError { get; set; }
        public int MagNst { get; set; }
        public string Status { get; set; }
        public string LocationSource { get; set; }
        public string MagSource { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is EarthquakeResponseModel))
            {
                return false;
            }

            EarthquakeResponseModel model = (EarthquakeResponseModel)obj;

            return Time == model.Time &&
                   Latitude == model.Latitude &&
                   Longitude == model.Longitude &&
                   Depth == model.Depth &&
                   Mag == model.Mag &&
                   MagType == model.MagType &&
                   Nst == model.Nst &&
                   Gap == model.Gap &&
                   Dmin == model.Dmin &&
                   Rms == model.Rms &&
                   Net == model.Net &&
                   Id == model.Id &&
                   Updated == model.Updated &&
                   Place == model.Place &&
                   Type == model.Type &&
                   HorizontalError == model.HorizontalError &&
                   DepthError == model.DepthError &&
                   MagError == model.MagError &&
                   MagNst == model.MagNst &&
                   Status == model.Status &&
                   LocationSource == model.LocationSource &&
                   MagSource == model.MagSource;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Time);
            hash.Add(Latitude);
            hash.Add(Longitude);
            hash.Add(Depth);
            hash.Add(Mag);
            hash.Add(MagType);
            hash.Add(Nst);
            hash.Add(Gap);
            hash.Add(Dmin);
            hash.Add(Rms);
            hash.Add(Net);
            hash.Add(Id);
            hash.Add(Updated);
            hash.Add(Place);
            hash.Add(Type);
            hash.Add(HorizontalError);
            hash.Add(DepthError);
            hash.Add(MagError);
            hash.Add(MagNst);
            hash.Add(Status);
            hash.Add(LocationSource);
            hash.Add(MagSource);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            IList<string> data = new List<string>()
            {
                Time.ToUniversalTime().ToString(Constants.DATE_FORMAT),
                Latitude.ToString(),
                Longitude.ToString(),
                Depth.ToString(),
                Mag.ToString(),
                MagType,
                Nst.ToString(),
                Gap.ToString(),
                Dmin.ToString(),
                Rms.ToString(),
                Net,
                Id,
                Updated.ToUniversalTime().ToString(Constants.DATE_FORMAT),
                Place,
                Type,
                HorizontalError.ToString(),
                DepthError.ToString(),
                MagError.ToString(),
                MagNst.ToString(),
                Status,
                LocationSource,
                MagSource
            };

            return string.Join(',', data);
        }       
    }
}
