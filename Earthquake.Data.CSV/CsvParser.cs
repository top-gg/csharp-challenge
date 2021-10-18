using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Earthquake.Data.CSV
{
    public class CsvParser : ICsvParser, IDisposable
    {
        private readonly CsvReader _csv;
        private readonly StreamReader _reader;
        private bool _disposed;

        public CsvParser(string fullFileName)
        {
            if (!File.Exists(fullFileName))
            {
                throw new FileNotFoundException("Csv file cannot be found.");
            }

            _reader = new StreamReader(fullFileName);
            _csv = new CsvReader(_reader, CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public IEnumerable<TClass> Read<TClass, TMap>()
        {
            _csv.Context.RegisterClassMap(typeof(TMap));
            return _csv.GetRecords<TClass>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _csv.Dispose();
                _reader.Dispose();
            }

            _disposed = true;
        }
    }
}