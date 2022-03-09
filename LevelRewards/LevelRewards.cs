using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xLevelRewards
{
    public class LevelReward
	{
        public int
            Level,
            Faction,
            Family,
            Job,
            Type,
            TypeID,
            Count;

        public LevelReward
        (
            int Level,
            int Faction,
            int Family,
            int Job,
            int Type,
            int TypeID,
            int Count
        )
        {
            this.Level = Level;
            this.Faction = Faction;
            this.Family = Family;
            this.Job = Job;
            this.Type = Type;
            this.TypeID = TypeID;
            this.Count = Count;

        }
    }
}
