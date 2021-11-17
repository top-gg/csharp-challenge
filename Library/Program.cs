using System;
using System.IO;

namespace Library {
    class Program {
        static void Main(string[] args) {
            EQDataFrame dataFrame = new EQDataFrame();
            using (var sr = new StreamReader("../../../all_month.csv")) {
                string line;
                int count = 0;
                while ((line = sr.ReadLine()) != null) {
                    if (count > 0)
                        dataFrame.ParseLine(line);
                    ++ count;
                }
                dataFrame.MarkDone();
            }
            var test = dataFrame.QueryEndpoint(64.6648, -147.6137, DateTime.Parse("2021-10-05"), DateTime.Parse("2021-10-05"));
            int a = 0;
        }
    }
}
