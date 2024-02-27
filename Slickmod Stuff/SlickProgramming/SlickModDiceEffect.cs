using System;
using LOR_DiceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hat_Method;
using System.Net;

namespace SlickRuinaMod
{
    #region - COURIERS 1 -

    // Lucky Slip: Dice 2 Effect
    // [On Hit] Inflict 1 Bind and 1 Bleed next Scene
    public class DiceCardAbility_SlickMod_1Bind1Bleed : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 1 Bind and 1 Bleed next Scene";

        public override string[] Keywords => new string[2] { "Binding_Keyword", "Bleeding_Keyword" };

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Bleeding, 1, base.owner);
        }
    }

    // Buzz Off!: Dice 3 Effect
    // [On Hit] If user has Cycle, inflict 1 Feeble next Scene
    public class DiceCardAbility_SlickMod_IfCycle1Feeble : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] If user has Cycle, inflict 1 Feeble next Scene";

        public override string[] Keywords => new string[2] { "SlickMod_Cycle_Keyword", "Weak_Keyword" };

        public override void OnSucceedAttack()
        {
            if (base.owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_Cycle) != null)
            {
                base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Weak, 1, base.owner);
            }
        }
    }

    #endregion

    #region - SNOW COYOTE OFFICE -

    // Mutual 2 Bind
    // [On Hit] Inflict 2 Bind to target and self next Scene
    public class DiceCardAbility_SlickMod_Mutual2Bind : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 2 Bind to each other next Scene";

        public override string[] Keywords => new string[1] { "Binding_Keyword" };

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, base.owner);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, base.owner);
        }
    }

    // Mutual 1 Bind
    // [On Hit] Inflict 1 Bind to target and self next Scene
    public class DiceCardAbility_SlickMod_Mutual1Bind : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 1 Bind to each other next Scene";

        public override string[] Keywords => new string[1] { "Binding_Keyword" };

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
        }
    }

    // [On Hit] Inflict 1 Rupture
    public class DiceCardAbility_SlickMod_1Rupture : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 1 Rupture";

        public override string[] Keywords => new string[1] { "Hat_Rupture_Keyword" };

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Rupture, 1, base.owner);
        }
    }

    // +1 Power if opponent has Bind
    public class DiceCardAbility_SlickMod_powerUp1targetBind : DiceCardAbilityBase
    {
        public static string Desc = "+1 Power if opponent has Bind";

        // Keywords
        public override string[] Keywords => new string[1] { "Binding_Keyword" };

        public override void BeforeRollDice()
        {
            BattleUnitModel target = base.card.target;
            if (target != null && target.bufListDetail.GetKewordBufStack(KeywordBuf.Binding) > 0)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }
    }

    // Pack Hunting Tactics: Dice Effect
    // [On Hit] Inflict 2 Rupture; Inflict 1 Bind next Scene; Inflict 2 Bind to self next Scene
    public class DiceCardAbility_SlickMod_2Rupture1Bind2SelfBind : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 2 Rupture; Inflict 1 Bind next Scene; Inflict 2 Bind to self next Scene";

        public override string[] Keywords => new string[2] { "Hat_Rupture_Keyword", "Binding_Keyword" };

        public override void OnSucceedAttack()
        {
            this.card.target?.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Rupture, 2, this.owner);
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, base.owner);
        }
    }

    // [On Hit] Inflict 3 Rupture
    public class DiceCardAbility_SlickMod_3Rupture : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 3 Rupture";

        public override string[] Keywords => new string[1] { "Hat_Rupture_Keyword" };

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Rupture, 3, base.owner);
        }
    }

    // [On Hit] Inflict 3 Rupture
    // ([On Hit] Give all other allied Snow Coyote Fixers a single-use copy of 'Pack Hunting Tactics' that exhausts at the end of the Scene;
    // user and all allied Snow Coyote Fixers restore 2 Light)
    public class DiceCardAbility_SlickMod_TurnerFuckening : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 3 Rupture";

        public override string[] Keywords => new string[2] { "Hat_Rupture_Keyword", "Energy_Keyword" };

        // Page Buff to make card temporary
        public class BattleDiceCardBuf_SlickModTemp : BattleDiceCardBuf
        {
            public override void OnRoundStart()
            {
                _card.temporary = true;
            }
        }

        public override void OnSucceedAttack()
        {
            // Inflict 3 Rupture; Restore 2 Light
            base.card.target?.bufListDetail.AddKeywordBufThisRoundByCard(Hat_KeywordBuf.KeywordBufs.Rupture, 3, base.owner);
            base.owner.cardSlotDetail.RecoverPlayPointByCard(2);

            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(base.owner.faction))
            {
                // Checks for only Snow Coyote Fixers (enemy and player)
                bool flag3 = battleUnitModel != base.owner;
                if ( flag3 && battleUnitModel.Book.BookId == new LorId("SlickMod", 10000002) || flag3 && battleUnitModel.Book.BookId == new LorId("SlickMod", 10000004) || flag3 && battleUnitModel.Book.BookId == new LorId("SlickMod", 2) )
                {
                    // Adds page with temporary card buf; Restores 2 Light
                    battleUnitModel.allyCardDetail.AddNewCard(new LorId("SlickMod", 14)).AddBuf(new BattleDiceCardBuf_SlickModTemp());
                    battleUnitModel.cardSlotDetail.RecoverPlayPointByCard(2);
                }
            }
        }
    }

    #endregion

    #region -INFERNAL CORPS I-

    // [On Hit] Inflict 1 Overheat to self next Scene
    public class DiceCardAbility_SlickMod_InfernalOverheat1OnHit : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 1 Overheat to self next Scene";
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 1, this.owner);
        }
    }

    // [On Hit] Inflict 2 Overheat to self next Scene; Inflict 1 Overheat next Scene
    public class DiceCardAbility_SlickMod_InfernalOverheat2Self1OnHit : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 2 Overheat to self next Scene; Inflict 1 Overheat next Scene";
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 2, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 1, this.owner);
        }
    }

    // PRISIM BLAAAAAAAAAAAAAAAAST
    // [On Hit] Inflict 2 Overheat next Scene
    public class DiceCardAbility_SlickMod_InfernalPRISIMBLAAAAAAAAAAAAAAASTonhit : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 2 Overheat next Scene";
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 2, this.owner);
        }
    }
    
    // [On Hit] Inflict 1 Overheat next Scene
    public class DiceCardAbility_SlickMod_InfernalOverheat1TargetOnHit : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 1 Overheat next Scene";
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_InfernalOverheat, 1, this.owner);
        }
    }
    
    #endregion
}
