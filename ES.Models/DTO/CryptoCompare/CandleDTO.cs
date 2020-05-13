using System;

namespace ES.Domain.DTO.CryptoCompare
{
    public class CandleDTO
    {
        public long Time { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal VolumeFrom { get; set; }

        public decimal VolumeTo { get; set; }
    }
}
