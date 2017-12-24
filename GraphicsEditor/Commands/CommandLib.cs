using System;
using System.Collections.Generic;
using GraphicsEditor.Shapes;

namespace GraphicsEditor.Commands
{
    static class CommandLib
    {
        public static List<T> ParseArguments<T>(IEnumerable<string> args, out List<string> errors)
            where T : IConvertible
        {
            errors = new List<string>();
            var result = new List<T>();
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
            return result;
        }

        public static IEnumerable<CompoundIndex> ParseIndexes(
            IEnumerable<string> arguments, out List<string> errors)
        {
            errors = new List<string>();
            var result = new List<CompoundIndex>();
            foreach (var argument in arguments)
            {
                if (CompoundIndex.TryParse(argument, out var parsedIndex))
                {
                    result.Add(parsedIndex);
                }
                else
                {
                    errors.Add(argument);
                }
            }
            return result;
        }

        public static List<CompoundIndex> GetExisting(IEnumerable<CompoundIndex> indexes,
                                                      out List<string> errors, IShape root)
        {
            errors = new List<string>();
            var result = new List<CompoundIndex>();
            CompoundIndex previous = null;
            foreach (var compoundIndex in indexes)
            {
                try
                {
                    if (compoundIndex.Equals(previous))
                    {
                        continue;
                    }
                    root.GetShapeAt(compoundIndex);
                    result.Add(compoundIndex);
                    previous = compoundIndex;
                }
                catch
                {
                    errors.Add(compoundIndex.ToString());
                }
            }
            result.Sort();
            result.Reverse();
            return result;
        }
    }
}