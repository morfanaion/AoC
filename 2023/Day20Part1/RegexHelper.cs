namespace Day20Part1
{
    internal partial class RegexHelper
    {
        private static RegexHelper? _instance;
        public static RegexHelper Instance => _instance ?? (_instance = new RegexHelper());

        [GeneratedRegex("(?<type>[%&]?)(?<id>[a-z]*) -> (?<outputmodules>[a-z, ]*)")]
        public partial Regex ModuleRegex();
    }
}
