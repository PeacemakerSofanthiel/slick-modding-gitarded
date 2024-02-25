using System;
using LOR_DiceSystem;
using KeywordUtil;
using EnumExtenderV2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Hat_Method.Hat_KeywordBuf;

namespace SlickRuinaMod
{
    // Flow State
    // Increase minimum and maximum roll value of all dice by +1. | Outdated, will rewrite with KeywordUtil systems Eventually:tm:
    public class BattleUnitBuf_SlickMod_FlowState : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_FlowState_Keyword";

        // Thing
        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_FlowState;

            }
        }

        // Neutral status; doesn't need anything to override BufPositiveType

        // Increase minimum and maximum roll value of all dice by +1
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                min = 1,
                max = 1
            });
        }

        // lose Flow State at end of Scene
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            this.Destroy();
        }
    }

    // Damage Down
    // Damage Down used to be here, but it was literally the same effect as Hat Singularity's Damage Down, so I'll just use that instead

    // Cycle
    // Upon using a page, discard 1 random page, then draw 1 page, then lose 1 Cycle.
    public class BattleUnitBuf_SlickMod_Cycle : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_Cycle";

        // Neutral status; doesn't need anything to override BufPositiveType
        
        // Thing
        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_Cycle;

            }
        }

        // Upon using a page, discard 1 random page, then draw 1 page, then lose 1 Cycle.
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base._owner.allyCardDetail.DisCardACardRandom();
            base._owner.allyCardDetail.DrawCards(1);
            this.stack--;
            if (this.stack <= 0) this._owner.bufListDetail.RemoveBuf(this);
        }
    }
}
