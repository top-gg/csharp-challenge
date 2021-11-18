using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Library {
    public class EQDataFrame {
        Dictionary<string, EQData> eqMap = new Dictionary<string, EQData>(); // <eqId, EQData>
        Dictionary<DateTime, List<string>> eqIdsByEventDate = new Dictionary<DateTime, List<string>>(); // <event date, eqId[]>

        // https://en.wikipedia.org/wiki/Haversine_formula
        double CalcDistBetweenTwoLocationsInMiles(double latA, double longA,
                                                    double latB, double longB) {
            double latDiffHalf = (latB - latA) * 0.5;
            double latSumHalf = (latB + latA) * 0.5;
            double longDiffHalf = (longB - longA) * 0.5;
            
            double sinSqLatDiffHalf = Math.Sin(latDiffHalf) * Math.Sin(latDiffHalf);
            double sinSqLatSumHalf = Math.Sin(latSumHalf) * Math.Sin(latSumHalf);
            double sinSqLongDiffHalf = Math.Sin(longDiffHalf) * Math.Sin(longDiffHalf);

            const double EarthRad = 3959.0;
            double dist = 2.0 * EarthRad * Math.Asin(Math.Sqrt(sinSqLatDiffHalf + (1.0 - sinSqLatDiffHalf - sinSqLatSumHalf) * sinSqLongDiffHalf));
            return dist;
        }

        // return null if error
        public List<EQData> QueryEndpoint(double eq_lat, double eq_long, DateTime eq_start_date, DateTime eq_end_date) {
            // https://earthquake.usgs.gov/data/comcat/index.php
            if (eq_lat < -90.0 || eq_lat > 90.0)
                return null;
            else if (eq_long < -180.0 || eq_long > 180.0)
                return null;
            else if (eq_end_date < eq_start_date)
                return null;

            List<EQData> ret = new List<EQData>();

            DateTime searchDateStart = eq_start_date.Subtract(eq_start_date.TimeOfDay);
            DateTime searchDateEnd = eq_end_date.Subtract(eq_end_date.TimeOfDay);

            int durationInDays = (searchDateEnd - searchDateStart).Duration().Days + 1;
            DateTime searchDate = searchDateStart;
            for (int i = 0; i < durationInDays; ++ i) {
                List<string> eqsOnDay;
                if (!eqIdsByEventDate.TryGetValue(searchDate, out eqsOnDay)
                    || eqsOnDay == null
                    || eqsOnDay.Count == 0)
                    continue;
                foreach (var id in eqsOnDay) {
                    var eqData = eqMap[id];
                    if (eqData == null)
                        continue;

                    double dist = CalcDistBetweenTwoLocationsInMiles(eqData.Latitude, eqData.Longitude,
                                                                        eq_lat, eq_long);
                    if (dist > (eqData.Magnitude * 100.0))
                        continue;
                    ret.Add(eqData);
                }
                searchDate = searchDate.AddDays(1.0);
            }

            ret.Sort(delegate(EQData x, EQData y) {
                return y.EventTime.CompareTo(x.EventTime);
            });
            return ret;
        }

        public void ParseLine(string lineToParse) {
            if (lineToParse == null)
                return;

            // to handle cases of commas in quote
            // https://stackoverflow.com/a/48275050
            //this regular expression splits string on the separator character NOT inside double quotes. 
            //separatorChar can be any character like comma or semicolon etc. 
            //it also allows single quotes inside the string value: e.g. "Mike's Kitchen","Jane's Room"
            Regex regx = new Regex("," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))"); 
            string[] splitted = regx.Split(lineToParse);
            //string[] splitted = lineToParse.Split(',');
            if (splitted[14] != "earthquake") // event type
                return;
            
            string timeStr = splitted[0];
            string latitudeStr = splitted[1];
            string longitudeStr = splitted[2];
            string magnitudeStr = splitted[4];
            string idStr = splitted[11];
            string placeStr = splitted[13];

            if (timeStr == string.Empty
                || latitudeStr == string.Empty 
                || longitudeStr == string.Empty
                || magnitudeStr == string.Empty
                || idStr == string.Empty)
                return;

            var eqData = new EQData(timeStr: timeStr,
                                    latitudeStr: latitudeStr,
                                    longitudeStr: longitudeStr,
                                    magnitudeStr: magnitudeStr,
                                    idStr: idStr,
                                    placeStr: placeStr);
            eqMap.Add(eqData.EventId, eqData);

            DateTime timeToStore = eqData.EventTime.Subtract(eqData.EventTime.TimeOfDay);
            if (!eqIdsByEventDate.ContainsKey(timeToStore)) {
                var eqs = new List<string>();
                eqs.Add(eqData.EventId);
                eqIdsByEventDate.Add(timeToStore, eqs);
            } else {
                var eqs = eqIdsByEventDate[timeToStore];
                eqs.Add(eqData.EventId);
                eqIdsByEventDate[timeToStore] = eqs;
            }
        }
    }
}
