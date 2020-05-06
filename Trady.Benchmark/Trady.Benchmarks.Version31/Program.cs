using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using ES.DataImporter.Csv;
using ES.Domain.Interfaces;
using ES.Domain.Models;
using ES.Domain.Period;
using ES.Infrastructure.Extensions;
using ES.Analysis.Extension;
using ES.Analysis;

namespace Trady.Benchmarks.Version31
{
    [Config(typeof(Config))]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31)]
    public class Benchmark
    {
        private const int _n = 10000;
        private readonly IOhlcv[] _data;
        private readonly ITickTrade[] _tradeData;


        public Benchmark()
        {
            var config = new CsvImportConfiguration()
            {
                Culture = "en-US",
                Delimiter = ";",
                DateFormat = "yyyyMMdd HHmmss",
                HasHeaderRecord = false
            };
            var importer = new CsvImporter("Data/EURUSD.csv", config);
            _data = importer.ImportAsync("EURUSD").Result.ToArray();

            _tradeData = new ITickTrade[_n];
            var d = DateTimeOffset.Now;
            for (int i = 0; i < _n; i++)
            {
                _tradeData[i] = new Trade(d.AddSeconds(i), 1, 1);
            }

        }

        private class Config : ManualConfig
        {
            public Config()
            {
                Add(StatisticColumn.P90);
            }
        }

        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformToMonthly() => _data.Transform<PerMinute, Monthly>();

        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformToDaily() => _data.Transform<PerMinute, Daily>();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> RelativeStrengthIndex() => _data.Rsi(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> ExponentialMovingAverage() => _data.Ema(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> SimpleMovingAverage() => _data.Sma(30);

        [Benchmark]
        public IReadOnlyList<Analysis<(decimal?, decimal?, decimal?)>> MACD() => _data.Macd(12, 26, 9);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> CommodityChannelIndex() => _data.Cci(20);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> StochasticMomentumIndex() => _data.Smi(15, 6, 6);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> StochasticRelativeStrengthIndex() => _data.StochRsi(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> NetMomentumOscillator() => _data.Nmo(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> RelativeMomentum() => _data.Rm(20, 4);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> RelativeMomentumIndex() => _data.Rmi(20, 4);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> DynamicMomentumIndex() => _data.Dymoi(5, 10, 14, 30, 5);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> ParabolicStopAndReverse() => _data.Sar(0.02m, 0.2m);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> Median() => _data.Median(20);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> Percentile() => _data.Percentile(30, 0.7m);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> AccumulationDistributionLine() => _data.AccumDist(20);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?)>> Aroon() => _data.Aroon(25);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> AroonOscillator() => _data.AroonOsc(25);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> AverageTrueRange() => _data.Atr(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?, decimal?)>> BollingerBands() => _data.Bb(20, 2);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> BollingerBandWidth() => _data.BbWidth(20, 2);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?)>> ChandelierExit() => _data.Chandlr(22, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> Momentum() => _data.Mtm();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> RateOfChange() => _data.Roc();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> PlusDirectionalIndicator() => _data.Pdi(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> MinusDirectionalIndicator() => _data.Mdi(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> AverageDirectionalIndex() => _data.Adx(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> AverageDirectionalIndexRating() => _data.Adxr(14, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> EfficiencyRatio() => _data.Er(10);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> KaufmanAdaptiveMovingAverage() => _data.Kama(10, 2, 30);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> ExponentialMovingAverageOscillator() => _data.EmaOsc(10, 30);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HighestHigh() => _data.HighHigh(10);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HighestClose() => _data.HighClose(10);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HistoricalHighestHigh() => _data.HistHighHigh();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HistoricalHighestClose() => _data.HistHighClose();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> LowestLow() => _data.LowLow(10);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> LowestClose() => _data.LowClose(10);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HistoricalLowestLow() => _data.HistLowLow();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HistoricalLowestClose() => _data.HistLowClose();

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?, decimal?, decimal?, decimal?)>> IchimokuCloud() => _data.Ichimoku(9, 26, 52);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> ModifiedMovingAverage() => _data.Mma(30);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> MacdHist() => _data.MacdHist(12, 26, 9);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> OnBalanceVolume() => _data.Obv();

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> RawStochasticsValue() => _data.Rsv(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> RelativeStrength() => _data.Rs(14);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> SimpleMovingAverageOscillator() => _data.SmaOsc(10, 30);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> StandardDeviation() => _data.Sd(10);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?, decimal?)>> Stochastics_Fast() => _data.FastSto(14, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?, decimal?)>> Stochastics_Slow() => _data.SlowSto(14, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?, decimal?)>> Stochastics_Full() => _data.FullSto(14, 3, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> StochasticsOscillator_Fast() => _data.FastStoOsc(14, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> StochasticsOscillator_Slow() => _data.SlowStoOsc(14, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> StochasticsOscillator_Full() => _data.FullStoOsc(14, 3, 3);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> WeightedMovingAverage() => _data.Wma(20);

        [Benchmark]
        public IReadOnlyList<IAnalysis<decimal?>> HullMovingAverage() => _data.Hma(30);

        [Benchmark]
        public IReadOnlyList<IAnalysis<(decimal?, decimal?, decimal?)>> KeltnerChannels() => _data.Kc(20, 2, 10);
        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformFromTradesToMinute() => _tradeData.TransformToCandles<PerMinute>();
        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformFromTradesToHourly() => _tradeData.TransformToCandles<Hourly>();
        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformFromTradesToBeHourly() => _tradeData.TransformToCandles<BiHourly>();
        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformFromTradesToDaily() => _tradeData.TransformToCandles<Daily>();
        [Benchmark]
        public IReadOnlyList<IOhlcv> TransformFromTradesToWeekly() => _tradeData.TransformToCandles<Weekly>();
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>();
        }
    }
}
