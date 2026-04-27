using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Cards.TreasureCards
{
    internal class OneShotCard : TreasureCard
    {
        public string OneShotName;
        public int Modifier;
        public OneShotTargetType TargetType;
    }

    public enum OneShotTargetType
    {
        Player,
        Enemy,
        InstantWin
    }
}

