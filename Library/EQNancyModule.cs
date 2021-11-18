using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using System.IO;

namespace Library {
    public class EQNancyModule : NancyModule {
        public EQNancyModule() {
            EQDataFrame dataFrame = new EQDataFrame();
            using (var sr = new StreamReader("../../../all_month.csv")) {
                string line;
                bool isFirstLine = true;
                while ((line = sr.ReadLine()) != null) {
                    if (!isFirstLine)
                        dataFrame.ParseLine(line);
                    else
                        isFirstLine = false;
                }
            }

            Get("/", parameters => {
                var query = Request.Query;
                double queryEqLong = (double)query.@long;
                double queryEqLat = (double)query.lat;
                string queryEqStartDate = (string)query.start_date;
                string queryEqEndDate = (string)query.end_date;

                var ret = dataFrame.QueryEndpoint(queryEqLat, queryEqLong, DateTime.Parse(queryEqStartDate), DateTime.Parse(queryEqEndDate));
                if (ret == null)
                    return HttpStatusCode.BadRequest;
                else if (ret.Count == 0)
                    return HttpStatusCode.NotFound;
                return Response.AsJson(ret);
            });
        }
    }
}
