using System;
using System.Collections.Generic;
using ES.Analysis.Indicator;
using ES.Analysis.Infrastructure;
using ES.Domain.Interfaces;

namespace ES.Analysis.Extension
{
    public static class OhlcvExtension
    {
        public static IReadOnlyList<Analysis<decimal?>> Func(this IEnumerable<IOhlcv> candles, Func<IReadOnlyList<IOhlcv>, int, IReadOnlyList<decimal>, IAnalyzeContext<IOhlcv>, decimal?> func, params decimal[] parameters)
            => func.AsAnalyzable(candles, parameters).Compute();

        public static IReadOnlyList<IAnalysis<decimal?>> Func(this IEnumerable<IOhlcv> candles, string name, params decimal[] parameters)
            => FuncAnalyzableFactory.CreateAnalyzable(name, candles, parameters).Compute();

        public static IReadOnlyList<Analysis<decimal?>> AccumDist(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new AccumulationDistributionLine(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? Up, decimal? Down)>> Aroon(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new Aroon(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> AroonOsc(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new AroonOscillator(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Adx(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new AverageDirectionalIndex(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Adxr(this IEnumerable<IOhlcv> candles, int periodCount, int adxrPeriodCount, int? startIndex = null, int? endIndex = null)
            => new AverageDirectionalIndexRating(candles, periodCount, adxrPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Atr(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new AverageTrueRange(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? LowerBand, decimal? MiddleBand, decimal? UpperBand)>> Bb(this IEnumerable<IOhlcv> candles, int periodCount, decimal sdCount, int? startIndex = null, int? endIndex = null)
            => new BollingerBands(candles, periodCount, sdCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> BbWidth(this IEnumerable<IOhlcv> candles, int periodCount, decimal sdCount, int? startIndex = null, int? endIndex = null)
            => new BollingerBandWidth(candles, periodCount, sdCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Cci(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new CommodityChannelIndex(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? Long, decimal? Short)>> Chandlr(this IEnumerable<IOhlcv> candles, int periodCount, decimal atrCount, int? startIndex = null, int? endIndex = null)
            => new ChandelierExit(candles, periodCount, atrCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Mtm(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new Momentum(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Mtm(this IEnumerable<IOhlcv> candles, int numberOfDays, int? startIndex = null, int? endIndex = null)
            => new Momentum(candles, numberOfDays).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Roc(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new RateOfChange(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Roc(this IEnumerable<IOhlcv> candles, int numberOfDays, int? startIndex = null, int? endIndex = null)
            => new RateOfChange(candles, numberOfDays).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Dmi(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new DirectionalMovementIndex(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Dymoi(this IEnumerable<IOhlcv> candles, int sdPeriod, int smoothedSdPeriod, int rsiPeriod, int upLimit, int lowLimit, int? startIndex = null, int? endIndex = null)
            => new DynamicMomentumIndex(candles, sdPeriod, smoothedSdPeriod, rsiPeriod, upLimit, lowLimit).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Er(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new EfficiencyRatio(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Ema(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new ExponentialMovingAverage(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> EmaOsc(this IEnumerable<IOhlcv> candles, int periodCount1, int periodCount2, int? startIndex = null, int? endIndex = null)
            => new ExponentialMovingAverageOscillator(candles, periodCount1, periodCount2).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> HighHigh(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new HighestHigh(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> HistHighHigh(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new HistoricalHighestHigh(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> HistHighClose(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new HistoricalHighestClose(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> HighClose(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new HighestClose(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? ConversionLine, decimal? BaseLine, decimal? LeadingSpanA, decimal? LeadingSpanB, decimal? LaggingSpan)>> Ichimoku(this IEnumerable<IOhlcv> candles, int shortPeriodCount, int middlePeriodCount, int longPeriodCount, int? startIndex = null, int? endIndex = null)
            => new IchimokuCloud(candles, shortPeriodCount, middlePeriodCount, longPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Kama(this IEnumerable<IOhlcv> candles, int periodCount, int emaFastPeriodCount, int emaSlowPeriodCount, int? startIndex = null, int? endIndex = null)
            => new KaufmanAdaptiveMovingAverage(candles, periodCount, emaFastPeriodCount, emaSlowPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> LowLow(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new LowestLow(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> HistLowLow(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new HistoricalLowestLow(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> HistLowClose(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new HistoricalLowestClose(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> LowClose(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new LowestClose(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Mdi(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new MinusDirectionalIndicator(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Mdm(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new MinusDirectionalMovement(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Mma(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new ModifiedMovingAverage(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? MacdLine, decimal? SignalLine, decimal? MacdHistogram)>> Macd(this IEnumerable<IOhlcv> candles, int emaPeriodCount1, int emaPeriodCount2, int demPeriodCount, int? startIndex = null, int? endIndex = null)
            => new MovingAverageConvergenceDivergence(candles, emaPeriodCount1, emaPeriodCount2, demPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> MacdHist(this IEnumerable<IOhlcv> candles, int emaPeriodCount1, int emaPeriodCount2, int demPeriodCount, int? startIndex = null, int? endIndex = null)
            => new MovingAverageConvergenceDivergenceHistogram(candles, emaPeriodCount1, emaPeriodCount2, demPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Nmo(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new NetMomentumOscillator(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Nvi(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new NegativeVolumeIndex(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Obv(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new OnBalanceVolume(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Pdi(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new PlusDirectionalIndicator(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Pdm(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new PlusDirectionalMovement(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Pvi(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new PositiveVolumeIndex(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Rm(this IEnumerable<IOhlcv> candles, int rmiPeriod, int mtmPeriod, int? startIndex = null, int? endIndex = null)
            => new RelativeMomentum(candles, rmiPeriod, mtmPeriod).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Rmi(this IEnumerable<IOhlcv> candles, int rmiPeriod, int mtmPeriod, int? startIndex = null, int? endIndex = null)
            => new RelativeMomentumIndex(candles, rmiPeriod, mtmPeriod).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Rsv(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new RawStochasticsValue(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Rs(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new RelativeStrength(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Rsi(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new RelativeStrengthIndex(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Sma(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new SimpleMovingAverage(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> SmaOsc(this IEnumerable<IOhlcv> candles, int periodCount1, int periodCount2, int? startIndex = null, int? endIndex = null)
            => new SimpleMovingAverageOscillator(candles, periodCount1, periodCount2).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Sd(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new StandardDeviation(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? K, decimal? D, decimal? J)>> FastSto(this IEnumerable<IOhlcv> candles, int periodCount, int smaPeriodCount, int? startIndex = null, int? endIndex = null)
            => new Stochastics.Fast(candles, periodCount, smaPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? K, decimal? D, decimal? J)>> FullSto(this IEnumerable<IOhlcv> candles, int periodCount, int smaPeriodCountK, int smaPeriodCountD, int? startIndex = null, int? endIndex = null)
            => new Stochastics.Full(candles, periodCount, smaPeriodCountK, smaPeriodCountD).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? K, decimal? D, decimal? J)>> SlowSto(this IEnumerable<IOhlcv> candles, int periodCount, int smaPeriodCountD, int? startIndex = null, int? endIndex = null)
            => new Stochastics.Slow(candles, periodCount, smaPeriodCountD).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> FastStoOsc(this IEnumerable<IOhlcv> candles, int periodCount, int smaPeriodCount, int? startIndex = null, int? endIndex = null)
            => new StochasticsOscillator.Fast(candles, periodCount, smaPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> FullStoOsc(this IEnumerable<IOhlcv> candles, int periodCount, int smaPeriodCountK, int smaPeriodCountD, int? startIndex = null, int? endIndex = null)
            => new StochasticsOscillator.Full(candles, periodCount, smaPeriodCountK, smaPeriodCountD).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> SlowStoOsc(this IEnumerable<IOhlcv> candles, int periodCount, int smaPeriodCountD, int? startIndex = null, int? endIndex = null)
            => new StochasticsOscillator.Slow(candles, periodCount, smaPeriodCountD).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> StochRsi(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new StochasticsRsiOscillator(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Tr(this IEnumerable<IOhlcv> candles, int? startIndex = null, int? endIndex = null)
            => new TrueRange(candles).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Median(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new Median(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Percentile(this IEnumerable<IOhlcv> candles, int periodCount, decimal percent, int? startIndex = null, int? endIndex = null)
            => new Percentile(candles, periodCount, percent).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Sar(this IEnumerable<IOhlcv> candles, decimal step, decimal maximumStep, int? startIndex = null, int? endIndex = null)
            => new ParabolicStopAndReverse(candles, step, maximumStep).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Smi(this IEnumerable<IOhlcv> candles, int periodCount, int smoothingPeriodA, int smoothingPeriodB, int? startIndex = null, int? endIndex = null)
            => new StochasticsMomentumIndex(candles, periodCount, smoothingPeriodA, smoothingPeriodB).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Wma(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new WeightedMovingAverage(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Hma(this IEnumerable<IOhlcv> candles, int periodCount, int? startIndex = null, int? endIndex = null)
            => new HullMovingAverage(candles, periodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<(decimal? LowerChannel, decimal? Middle, decimal? UpperChannel)>> Kc(this IEnumerable<IOhlcv> candles, int periodCount, decimal sdCount, int atrPeriodCount, int? startIndex = null, int? endIndex = null)
            => new KeltnerChannels(candles, periodCount, sdCount, atrPeriodCount).Compute(startIndex, endIndex);

        public static IReadOnlyList<Analysis<decimal?>> Vwap(this IEnumerable<IOhlcv> candles, int? period = null, int? startIndex = null, int? endIndex = null)
           => new VolumeWeightedAveragePrice(candles, period).Compute(startIndex, endIndex);
    }
}
