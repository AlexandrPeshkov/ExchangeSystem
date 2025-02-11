﻿using System;
using System.Collections.Generic;
using System.Linq;
using ES.Domain.Interfaces;
using ES.Domain.Models;
using ES.Domain.Period;
using ES.Infrastructure.Exceptions;

namespace ES.Infrastructure.Extensions
{
    public static class IOhlcvDataExtension
    {
        public static decimal GetUpperShadow(this IOhlcv candle) => candle.Open < candle.Close ? candle.High - candle.Close : candle.High - candle.Open;

        public static decimal GetLowerShadow(this IOhlcv candle) => candle.Open < candle.Close ? candle.Open - candle.Low : candle.Close - candle.Low;

        public static decimal GetBody(this IOhlcv candle) => Math.Abs(candle.Open - candle.Close);

        public static bool IsBull(this IOhlcv candle) => candle.Open < candle.Close;
        public static bool IsBear(this IOhlcv candle) => candle.Open > candle.Close;
        public static bool IsDoji(this IOhlcv candle) => candle.Open == candle.Close;


        #region candle list transformation

        public static IReadOnlyList<IOhlcv> Transform<TSourcePeriod, TTargetPeriod>(this IEnumerable<IOhlcv> candles)
            where TSourcePeriod : IPeriod
            where TTargetPeriod : IPeriod
        {
            if (!candles.Any())
                return candles.ToList();

            if (typeof(TSourcePeriod).Equals(typeof(TTargetPeriod)))
                return candles.ToList();

            //if (!IsTimeframesValid<TSourcePeriod>(candles, out var err))
            //    throw new InvalidTimeframeException(err.Time);

            if (!IsTransformationValid<TSourcePeriod, TTargetPeriod>())
                throw new InvalidTransformationException(typeof(TSourcePeriod), typeof(TTargetPeriod));

            var outputCandles = new List<IOhlcv>();
            TTargetPeriod periodInstance = Activator.CreateInstance<TTargetPeriod>();

            // To prevent lazy evaluated when compute
            var orderedCandles = candles.OrderBy(c => c.Time).ToList();

            DateTime periodStartTime = orderedCandles[0].Time;
            DateTime periodEndTime = periodInstance.NextTimestamp(periodStartTime);

            var tempCandles = new List<IOhlcv>();
            for (int i = 0; i < orderedCandles.Count; i++)
            {
                var indexTime = orderedCandles[i].Time;
                if (indexTime >= periodEndTime)
                {
                    periodStartTime = periodEndTime;
                    periodEndTime = periodInstance.NextTimestamp(periodStartTime);

                    AddComputedCandleToOutput(outputCandles, tempCandles);
                    tempCandles = new List<IOhlcv>();
                }

                tempCandles.Add(orderedCandles[i]);
            }

            if (tempCandles.Any())
                AddComputedCandleToOutput(outputCandles, tempCandles);

            return outputCandles;
        }

        private static void AddComputedCandleToOutput(List<IOhlcv> outputCandles, List<IOhlcv> tempCandles)
        {
            var computedCandle = ComputeCandles(tempCandles);
            if (computedCandle != null)
                outputCandles.Add(computedCandle);
        }

        private static bool IsTimeframesValid<TPeriod>(IEnumerable<IOhlcv> candles, out IOhlcv err)
            where TPeriod : IPeriod
        {
            var periodInstance = Activator.CreateInstance<TPeriod>();
            err = default;
            var offset = candles.Any() ? candles.First().Time.Hour : 0;

            for (int i = 0; i < candles.Count() - 1; i++)
            {
                var nextTime = periodInstance.NextTimestamp(candles.ElementAt(i).Time);
                var candleEndTime = new DateTimeOffset(nextTime.Date, TimeSpan.FromHours(offset));
                if (candleEndTime > candles.ElementAt(i + 1).Time)
                {
                    err = candles.ElementAt(i);
                    return false;
                }
            }
            return true;
        }

        private static bool IsTransformationValid<TSourcePeriod, TTargetPeriod>()
            where TSourcePeriod : IPeriod
            where TTargetPeriod : IPeriod
        {
            var inputInstance = Activator.CreateInstance<TSourcePeriod>();
            var outputInstance = Activator.CreateInstance<TTargetPeriod>();

            if (inputInstance is IIntradayPeriod input)
                return !(outputInstance is IIntradayPeriod output) || (input.NumberOfSecond < output.NumberOfSecond);

            var input2 = inputInstance as IInterdayPeriod;
            if (outputInstance is IIntradayPeriod output2)
                return false;

            var output3 = outputInstance as IInterdayPeriod;
            if (input2.OrderOfTransformation >= output3.OrderOfTransformation)
                return false;

            return true;
        }

        private static IOhlcv ComputeCandles(IEnumerable<IOhlcv> candles)
        {
            if (!candles.Any())
                return null;

            var dateTime = candles.First().Time;
            var open = candles.First().Open;
            var high = candles.Max(stick => stick.High);
            var low = candles.Min(stick => stick.Low);
            var close = candles.Last().Close;
            var volume = candles.Sum(stick => stick.Volume);
            return new CandleTrade(dateTime, open, high, low, close, volume);
        }


        #endregion candle list transformation
    }
}
