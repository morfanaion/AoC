namespace Day19Puzzle01
{
    internal class Blueprint
    {
        public int BlueprintNumber { get; set; }
        public int OreRequiredForOrebot { get; set; }
        public int OreRequiredForClaybot { get; set; }
        public int OreRequiredForObsidianBot { get; set; }
        public int ClayRequiredForObsidianBot { get; set; }
        public int OreRequiredForGeodeBot { get; set; }
        public int ObsidianRequiredForGeodeBot { get; set; }

        public Blueprint(string defLine)
        {
            Regex pattern = new Regex(@"Blueprint (?<blueprintno>\d*): Each ore robot costs (?<orebotcost>\d*) ore\. Each clay robot costs (?<claybotcost>\d*) ore\. Each obsidian robot costs (?<obsidianbotorecost>\d*) ore and (?<obsidianbotclaycost>\d*) clay\. Each geode robot costs (?<geodebotorecost>\d*) ore and (?<geodebotobsidiancost>\d*) obsidian\.");
            Match match = pattern.Match(defLine);
            BlueprintNumber = int.Parse(match.Groups["blueprintno"].Value);
            OreRequiredForOrebot = int.Parse(match.Groups["orebotcost"].Value);
            OreRequiredForClaybot = int.Parse(match.Groups["claybotcost"].Value);
            OreRequiredForObsidianBot = int.Parse(match.Groups["obsidianbotorecost"].Value);
            ClayRequiredForObsidianBot = int.Parse(match.Groups["obsidianbotclaycost"].Value);
            OreRequiredForGeodeBot = int.Parse(match.Groups["geodebotorecost"].Value);
            ObsidianRequiredForGeodeBot = int.Parse(match.Groups["geodebotobsidiancost"].Value);
        }

        public int Value
        {
            get => BlueprintNumber * GetMaxOpenedGeodesForStep(1, 0, 0, 0, 0, 1, 0, 0, 0);
        }

        int GetMaxOpenedGeodesForStep(int minute, int ore, int clay, int obsidian, int geodes, int orebots, int claybots, int obsidianbots, int geodebots)
        {
            if (minute == 24)
            {
                return geodebots + geodes;
            }
            int result = 0;
            if (SufficientForGeodeBot(ore, obsidian))
            {
                result = Math.Max(result, GetMaxOpenedGeodesForStep(minute + 1, ore + orebots - OreRequiredForGeodeBot, clay + claybots, obsidian + obsidianbots - ObsidianRequiredForGeodeBot, geodes + geodebots, orebots, claybots, obsidianbots, geodebots + 1));
            }
            else
            {
                if (obsidianbots < ObsidianRequiredForGeodeBot && SufficientForObsidianBot(ore, clay))
                {
                    result = Math.Max(result, GetMaxOpenedGeodesForStep(minute + 1, ore + orebots - OreRequiredForObsidianBot, clay + claybots - ClayRequiredForObsidianBot, obsidian + obsidianbots, geodes + geodebots, orebots, claybots, obsidianbots + 1, geodebots));
                }

                if (!OreStockpileToHigh(ore, orebots))
                {
                    result = Math.Max(result, GetMaxOpenedGeodesForStep(minute + 1, ore + orebots, clay + claybots, obsidian + obsidianbots, geodes + geodebots, orebots, claybots, obsidianbots, geodebots));
                }


                if (orebots < Max(OreRequiredForClaybot, OreRequiredForGeodeBot, OreRequiredForObsidianBot, OreRequiredForOrebot) && SufficientForOrebot(ore))
                {
                    result = Math.Max(result, GetMaxOpenedGeodesForStep(minute + 1, ore + orebots - OreRequiredForOrebot, clay + claybots, obsidian + obsidianbots, geodes + geodebots, orebots + 1, claybots, obsidianbots, geodebots));
                }
                if (claybots < ClayRequiredForObsidianBot && SufficientForClaybot(ore))
                {
                    result = Math.Max(result, GetMaxOpenedGeodesForStep(minute + 1, ore + orebots - OreRequiredForClaybot, clay + claybots, obsidian + obsidianbots, geodes + geodebots, orebots, claybots + 1, obsidianbots, geodebots));
                }
            }
            return result;
        }

        internal bool OreStockpileToHigh(int ore, int orebots) => ore >= Max(OreRequiredForClaybot, OreRequiredForGeodeBot, OreRequiredForObsidianBot, OreRequiredForOrebot) + Max(OreRequiredForObsidianBot, OreRequiredForGeodeBot) - orebots;

        private int Max(params int[] integers) => integers.Max();

        internal bool SufficientForOrebot(int ore) => ore >= OreRequiredForOrebot;
        internal bool SufficientForClaybot(int ore) => ore >= OreRequiredForClaybot;
        internal bool SufficientForObsidianBot(int ore, int clay) => ore >= OreRequiredForObsidianBot && clay >= ClayRequiredForObsidianBot;
        internal bool SufficientForGeodeBot(int ore, int obsidian) => ore >= OreRequiredForGeodeBot && obsidian >= ObsidianRequiredForGeodeBot;
    }
}
