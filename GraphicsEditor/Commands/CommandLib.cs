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
                    if (Collides(result, parsedIndex))
                    {
                        errors.Add(argument);
                    }
                    else
                    {
                        result.Add(parsedIndex);
                    }
                }
                else
                {
                    errors.Add(argument);
                }
            }

            return result;
        }

        private static bool Collides(IEnumerable<CompoundIndex> source, CompoundIndex index)
        {
            foreach (var compoundIndex in source)
            {
                if (compoundIndex.Count == 1 && index.Count == 1)
                {
                    continue;
                }
                if (compoundIndex.ToString().StartsWith(index.ToString()))
                {
                    return true;
                }

                if (index.ToString().StartsWith(compoundIndex.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<IShape> GetExisting(IEnumerable<CompoundIndex> indexes,
                                               out List<string> errors, IShape core)
        {
            errors = new List<string>();
            var result = new List<IShape>();
            foreach (var compoundIndex in indexes)
            {
                try
                {
                    var shape = core.GetShapeAt(compoundIndex);
                    result.Add(shape);
                }
                catch
                {
                    errors.Add(compoundIndex.ToString());
                }
            }
            return result;
        }
    }
}