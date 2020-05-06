using System.Globalization;

namespace ES.DataImporter.Csv
{
    public class CsvImportConfiguration
    {
        public string Delimiter { get; set; } = ",";
        public string DateFormat { get; set; }
        public string Culture { get; set; }
        public bool HasHeaderRecord { get; set; } = true;
        public CultureInfo CultureInfo => string.IsNullOrEmpty(Culture) ? null : CultureInfo.GetCultureInfo(Culture);
    }
}
