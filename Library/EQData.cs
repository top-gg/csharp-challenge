using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library {
    public class EQData {
        // https://earthquake.usgs.gov/data/comcat/index.php
        public DateTime EventTime { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public double Magnitude { get; private set; }
        public string EventId { get; private set; }
        public string Place { get; private set; }

        public EQData(string timeStr,
                       string latitudeStr,
                       string longitudeStr,
                       string magnitudeStr,
                       string idStr,
                       string placeStr) {
            EventTime = DateTime.Parse(timeStr);
            Latitude = double.Parse(latitudeStr);
            Longitude = double.Parse(longitudeStr);
            Magnitude = double.Parse(magnitudeStr);
            EventId = idStr;
            Place = placeStr;
        }
    }
}
