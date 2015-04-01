using System;
using System.Collections.Generic;
using System.Linq;

namespace FLifegame.Common
{
    public static class EnumerableExtensions
    {
        public static bool IsInRange(this int value, int minimum, int maximum)
        { return value >= minimum && value <= maximum; }

        public static IEnumerable<int> Select(this int times, int startIndex)
        { return Enumerable.Range(startIndex, times); }

        public static IEnumerable<int> Select(this int times)
        { return times.Select(0); }

        public static IEnumerable<Position> Select(this Dimensions dimensions)
        {
            //return from y in dimensions.Height.Select()
            //       from x in dimensions.Width.Select()
            //       select new Position { X = x, Y = y };

            return dimensions.Height.Select().SelectMany(x => dimensions.Width.Select(), (y, x) => new Position { X = x, Y = y });
        }

        public static IEnumerable<Position> Select(this Dimensions dimensions, Position start)
        {
            //return from y in dimensions.Height.Select(start.Y)
            //       from x in dimensions.Width.Select(start.X)
            //       select new Position { X = x, Y = y };

            return dimensions.Height.Select(start.Y).SelectMany(x => dimensions.Width.Select(start.X), (y, x) => new Position { X = x, Y = y });
        }

        public static void Times(this int times, Action action)
        { Enumerable.Range(0, times).ForEach(index => action()); }

        public static void Times(this int times, int startIndex, Action<int> action)
        { times.Select(startIndex).ForEach(action); }

        public static void Times(this int times, Action<int> action)
        { times.Select().ForEach(action); }

        public static void Times(this Dimensions dimensions, Action<Position> action)
        {
            //dimensions.Height.Times(y => dimensions.Width.Times(x => action(new Position { X = x, Y = y })));
            dimensions.Select().ForEach(action);
        }

        public static void Times(this Dimensions dimensions, Position start, Action<Position> action)
        {
            //dimensions.Height.Times(start.Y, y => dimensions.Width.Times(start.X, x => action(new Position { X = x, Y = y })));
            dimensions.Select(start).ForEach(action);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> action)
        {
            var index = 0;
            foreach (var item in collection)
                action(index++, item);
        }

        public static void ParallelTimes(this int times, Action action)
        { Enumerable.Range(0, times).ParallelForAll(index => action()); }

        public static void ParallelTimes(this int times, Action<int> action)
        { Enumerable.Range(0, times).ParallelForAll(index => action(index)); }

        public static void ParallelTimes(this Dimensions dimensions, Action<Position> action)
        { dimensions.Width.ParallelTimes(x => dimensions.Height.ParallelTimes(y => action(new Position { X = x, Y = y }))); }

        public static void ParallelTimes2(this int times, Action<int> action)
        { Enumerable.Range(0, times).ParallelForAll2(index => action(index)); }

        public static void ParallelTimes2(this Dimensions dimensions, Action<Position> action)
        { dimensions.Width.ParallelTimes2(x => dimensions.Height.ParallelTimes2(y => action(new Position { X = x, Y = y }))); }

        public static void ParallelForAll2<T>(this IEnumerable<T> collection, Action<T> action)
        { collection.AsParallel().ForAll(action); }

        //public static void ParallelForAll<T>(this IEnumerable<T> collection, Action<T> action)
        //{ collection.AsParallel().ForAll(action); }

        //public static void ParallelForEach<T>(this IList<T> collection, Action<int, T> action)
        //{ Parallel.For(0, collection.Count, index => action(index, collection[index])); }

        //public static int ParallelSum<T, S>(this IEnumerable<T> collection, Func<T, int> selector)
        //{ return collection.AsParallel().Sum(selector); }

        public static void ParallelForAll<T>(this IEnumerable<T> collection, Action<T> action)
        { collection.ForEach(action); }

        public static void ParallelForEach<T>(this IList<T> collection, Action<int, T> action)
        { collection.ForEach(action); }

        public static int ParallelSum<T, S>(this IEnumerable<T> collection, Func<T, int> selector)
        { return collection.Sum(selector); }
    }
}
