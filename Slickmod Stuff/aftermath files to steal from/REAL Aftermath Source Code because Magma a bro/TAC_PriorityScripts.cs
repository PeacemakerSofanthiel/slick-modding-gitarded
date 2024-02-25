using System.Linq;

namespace The_Aftermath_Collection 
{

    #region - GENERAL STUFF -
    public class DiceCardPriority_Aftermath_DoNotUse : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            return -9;
        }
    }


    #endregion


    #region - COALITION OF BACKSTREETS LIBERATORS I -


    public class DiceCardPriority_Aftermath_UseAtMore4Venom : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword")).Count >= 4 && owner.cardSlotDetail.cardQueue.ToList().FindAll(x => x.card.CheckForKeyword("Venom_Use_Keyword")).Count < 2)
            {
                return 8;
            }
            return -9;
        }
    }

    public class DiceCardPriority_Aftermath_UseAt5Venom : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword")).Count == 5 || owner.allyCardDetail.GetHand().Count < 3)
            {
                return 2;
            }
            return 0;
        }
    }

    public class DiceCardPriority_Aftermath_GoCrazy : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.allyCardDetail.GetHand().FindAll(x => x.CheckForKeyword("Venom_Keyword")).Count > 5)
            {
                return 6;
            }
            return -1;
        }
    }

    public class DiceCardPriority_Aftermath_HelpingHand : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            foreach (BattleUnitModel ally in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                int count = 0;
                foreach (BattleUnitBuf buf in ally.bufListDetail.GetActivatedBufList())
                {
                    if (buf.positiveType == BufPositiveType.Negative)
                    {
                        count += buf.stack;
                    }

                    if (count >= 8)
                    {
                        return 3;
                    }
                }
            }

            return -9;
        }
    }

    public class DiceCardPriority_Aftermath_Camaraderie : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            int count = 0;

            foreach (BattleUnitModel ally in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                foreach (BattleUnitBuf buf in ally.bufListDetail.GetActivatedBufList())
                {
                    if (buf.positiveType == BufPositiveType.Negative)
                    {
                        count += buf.stack;
                    }
                }
            }

            if (count >= 20)
            {
                return 3;
            }

            return -1;
        }
    }
    #endregion

    #region - LIU REMNANTS -

    // priority for Onward!
    public class DiceCardPriority_Aftermath_OnwardPriorityShitass : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.emotionDetail.EmotionLevel >= 5) return 999;
            else if (owner.emotionDetail.EmotionLevel >= 4) return -1;
            else if (owner.emotionDetail.EmotionLevel >= 3) return -2;
            return -9999;
        }
    }

    #endregion
  
    #region - COLOR CHUN -

    // priority for Flaming Blitz
    public class DiceCardPriority_Aftermath_ColorChunFunnies : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.allyCardDetail.GetHand().FindAll(x => x.XmlData.Script == "ColorChunFunnyPage").Count > 1 && owner.cardSlotDetail.cardAry.Find(x => x is BattlePlayingCardDataInUnitModel cord && cord.card.XmlData.Script == "ColorChunFunnyPage") == null)
            {
                return 9999;
            }
            return -9999;
        }
    }

    // priority for The Potential to be Enormous
    public class DiceCardPriority_Aftermath_ColorChunEnormous : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.cardSlotDetail.cardAry.Find(x => x is BattlePlayingCardDataInUnitModel cord && cord.card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 103)) != null)
                return 9999;
            return -9999;
        }
    }

    // priority for Light the Flames
    public class DiceCardPriority_Aftermath_ColorChunLighttheFlames : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.cardSlotDetail.cardAry.Find(x => x is BattlePlayingCardDataInUnitModel cord && cord.card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 105)) != null)
                return 9999;
            return base.GetPriorityBonus(owner);
        }
    }

    #endregion

    #region - RETURN OF THE INDEX -

    public class DiceCardPriority_OpheliaPleaseOnlyUseAt8SpeedOrHigher : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner, int speed)
        {
            if (speed >= 8) return 50;
            return -99;
        }
    }
                        
    #endregion

    #region - C.B.L. I -

    public class DiceCardPriority_OnlyUseWithHomeRun : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            if (owner.allyCardDetail.GetHand().Find(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20101)) != null) return 500;
            return -99;                    
        }
    }

    #endregion

    #region - MOBIUS OFFICE -
    public class DiceCardPriority_Aftermath_HyperGuardDontFuckingUseWithoutOvercharge : DiceCardPriorityBase
    {
        public override int GetPriorityBonus(BattleUnitModel owner)
        {
            BattleUnitBuf buf = owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge);
            if (buf != null && buf.stack > 0) return 25;
            return -10;
        }
    }

    #endregion
}