using System;
using System.Collections.Generic;
using System.Linq;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands
{
    static class CommandLib
    {
        public static List<string> ParseArguments<T>(IEnumerable<string> args, out List<T> result)
            where T : IConvertible
        {
            var errors = new List<string>();
            result = new List<T>();
            foreach (var argument in args)
            {
                try
                {
                    var temp = (T) Convert.ChangeType(argument, typeof(T));
                    result.Add(temp);
                }
                catch (Exception e) when (e is OverflowException || e is FormatException)
                {
                    errors.Add(argument);
                }
            }
            return errors;
        }

        public static List<List<int>> ParseIndexes(IEnumerable<string> args,
                                                   out List<string> errors)
        {
            errors = new List<string>();
            var result = new List<List<int>>();
            foreach (var argument in args)
            {
                var nums = argument.Split(':');
                try
                {
                    var current = nums.Select(num => Convert.ToInt32(num)).ToList();
                    result.Add(current);
                }
                catch
                {
                    errors.Add(argument);
                }
            }
            return result;
        }

        public static List<List<int>> CheckIndexesExistence(IEnumerable<List<int>> indexes,
                                                            out List<string> errors, IShape root)
        {
            errors = new List<string>();
            var result = new List<List<int>>();
            foreach (var compoundIndex in indexes)
            {
                try
                {
                    root.GetShapeAt(compoundIndex);
                    if (!result.Contains(compoundIndex))
                    {
                        result.Add(compoundIndex);
                    }
                }
                catch
                {
                    var compoundString = string.Join(":", compoundIndex);
                    errors.Add(compoundString);
                }
            }
            result.Sort((first, second) =>
                            string.CompareOrdinal(string.Join("", first), string.Join("", second)));
            result.Reverse();
            return result;
        }
    }
}