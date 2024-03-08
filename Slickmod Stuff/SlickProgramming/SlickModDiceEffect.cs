using System;
using LOR_DiceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using UnityEngine;

namespace SlickRuinaMod
{
    #region --COURIERS 1--

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

    #region --SNOW COYOTE OFFICE--

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
    // [On Hit] Inflict 2 Fragile and 1 Bind next Scene; Inflict 2 Bind to self next Scene
    public class DiceCardAbility_SlickMod_2Fragile1Bind2SelfBind : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 2 Fragile and 1 Bind next Scene; Inflict 2 Bind to self next Scene";

        public override string[] Keywords => new string[2] { "Vulnerable_Keyword", "Binding_Keyword" };

        public override void OnSucceedAttack()
        {
            this.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable, 2, this.owner);
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, base.owner);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, base.owner);
        }
    }

    // [On Hit] Inflict 3 Fragile
    public class DiceCardAbility_SlickMod_3Fragile : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 3 Fragile next Scene";

        public override string[] Keywords => new string[1] { "Vulnerable_Keyword" };

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable, 3, base.owner);
        }
    }

    // [On Hit] Inflict 3 Rupture
    // ([On Hit] Give all other allied Snow Coyote Fixers a single-use copy of 'Pack Hunting Tactics' that exhausts at the end of the Scene;
    // user and all allied Snow Coyote Fixers restore 2 Light)
    public class DiceCardAbility_SlickMod_TurnerFuckening : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 3 Fragile next Scene";

        public override string[] Keywords => new string[2] { "Vulnerable_Keyword", "Energy_Keyword" };

        public override void OnSucceedAttack()
        {
            // Inflict 3 Rupture; Restore 2 Light
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable, 3, base.owner);
            base.owner.cardSlotDetail.RecoverPlayPointByCard(2);

            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(base.owner.faction))
            {
                // Checks for only Snow Coyote Fixers (enemy and player)
                bool flag3 = battleUnitModel != base.owner;
                if ( flag3 && battleUnitModel.Book.BookId == new LorId("SlickMod", 1502001) || flag3 && battleUnitModel.Book.BookId == new LorId("SlickMod", 1502003) || flag3 && battleUnitModel.Book.BookId == new LorId("SlickMod", 2502001) )
                {
                    // Adds page with temporary card buf; Restores 2 Light
                    battleUnitModel.allyCardDetail.AddNewCard(new LorId("SlickMod", 0502009)).AddBuf(new BattleDiceCardBuf_SlickMod_Temp());
                    battleUnitModel.cardSlotDetail.RecoverPlayPointByCard(2);
                }
            }
        }
    }

    #endregion

    #region --INFERNAL CORPS 1--

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

    #region --UN GOLDEN SPARK--

    // Tatsumaki: Dice Effect 1
    // This die is rolled twice if user has 10+ Samsara
    public class DiceCardAbility_SparkRepeatSamsara : DiceCardAbilityBase
    {
        public static string Desc = "This die is rolled twice if user has 10+ Samsara";

        private int _repeatCount;

        public override void AfterAction()
        {
            BattleUnitBuf activatedBuf = owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara);
            if (activatedBuf != null && activatedBuf.stack >= 10)
            {
                if (!base.owner.IsBreakLifeZero() && _repeatCount < 1)
                {
                    _repeatCount++;
                    ActivateBonusAttackDice();
                }
            }
        }
    }

    #endregion

    #region --BACKSTREET SLUGGERS--

    // [On Clash Lose] Deal 2 Stagger damage to target
    public class DiceCardAbility_SlickMod_BackstreetShieldShards : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Lose] Deal 2 Stagger damage";

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            card.target?.TakeBreakDamage(2, DamageType.Card_Ability, owner);
        }
    }

    // [On Clash Win] Inflict 1 Burn to each other
    public class DiceCardAbility_SlickMod_BackstreetCWBurnEachother1 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Win] Inflict 1 Burn to each other";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override void OnWinParrying()
        {
            base.OnWinParrying();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, owner);
            card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, owner);
        }
    }

    // [On Clash Lose] Inflict 1 Burn to each other
    public class DiceCardAbility_SlickMod_BackstreetCLBurnEachother1 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Lose] Inflict 1 Burn to each other";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, owner);
            card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, owner);
        }
    }

    // [On Clash Lose] Destroy opponent's next die
    public class DiceCardAbility_SlickMod_BackstreetCLWheels : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Lose] Destroy opponent's next die";

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            card?.target?.currentDiceAction?.DestroyDice(DiceMatch.NextDice);
        }
    }

    // [On Hit] Inflict 5 Burn; Inflict 10 Burn to self
    public class DiceCardAbility_SlickMod_BackstreetBigBurn : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 5 Burn; Inflict 10 Burn to self";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, owner);
            target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 10, owner);
        }
    }

    #endregion

    #region --MIDNIGHT OFFICE--

    // [On Clash Lose] Inflict 3 Burn to each other
    public class DiceCardAbility_SlickMod_MidnightCLBurnEachother1 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Lose] Inflict 3 Burn to each other";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 3, owner);
            card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 3, owner);
        }
    }

    // [On Hit] Inflict 2 Burn to all characters
    public class DiceCardAbility_SlickMod_MidnightBurnEveryone2 : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 2 Burn to all characters";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList())
            {
                alive.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 2, owner);
            }
        }
    }

    // [Clash Win] Last die gains +2 Power
    public class DiceCardAbility_SlickMod_MidnightCWLastPow2 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Win] Last die gains +2 Power";

        public override void OnWinParrying()
        {
            base.OnWinParrying();
            card?.ApplyDiceStatBonus(DiceMatch.LastDice, new DiceStatBonus
            {
                power = 2
            });
        }
    }

    // [Clash Win] Next die gains +4 Power
    public class DiceCardAbility_SlickMod_MidnightCWNextPow4 : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Win] Next die gains +4 Power";

        public override void OnWinParrying()
        {
            base.OnWinParrying();
            card?.ApplyDiceStatBonus(DiceMatch.NextDice, new DiceStatBonus
            {
                power = 4
            });
        }
    }

    // [Clash Win] Inflict 2 Fragile this Scene and next Scene
    public class DiceCardAbility_SlickMod_MidnightVulnerableNowLater : DiceCardAbilityBase
    {
        public static string Desc = "[On Clash Win] Inflict 2 Fragile this Scene and next Scene";

        public override string[] Keywords => new string[1] { "Vulnerable_Keyword" };

        public override void OnWinParrying()
        {
            base.OnWinParrying();
            card.target?.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Vulnerable, 2, owner);
            card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable, 2, owner);
        }
    }

    // [On Hit] Inflict 20 Burn to each other; Inflict 4 Burn to all other characters
    public class DiceCardAbility_SlickMod_MidnightBurnEverything : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict 20 Burn to each other and inflict 4 Burn to all characters";

        public override string[] Keywords => new string[1] { "Burn_Keyword" };

        public override void OnSucceedAttack(BattleUnitModel target)
        {
            base.OnSucceedAttack(target);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 20, owner);
            target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 20, owner);
            List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList();
            aliveList.Remove(owner);
            aliveList.Remove(target);
            foreach (BattleUnitModel alive in aliveList)
            {
                alive.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 4, owner);
            }
        }
    }

    // Currently just added to dice from an on use effect
    // [On Hit] Deal Stagger damage to self equal to the roll's value
    public class DiceCardAbility_SlickMod_SelfStagger : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Deal Stagger damage to self equal to the roll's value";

        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            owner.breakDetail.TakeBreakDamage(behavior.DiceResultValue, DamageType.Card_Ability);
        }
    }

    #endregion

    #region --DANS SOLUTIONS--
    public class DiceCardAbility_SlickMod_Dan1StaggerFragile : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable_break, 1, this.owner);
        }
    }

    public class DiceCardAbility_SlickMod_DanShittyDodge : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.owner.TakeBreakDamage(3, DamageType.Card_Ability);
        }
    }

    public class DiceCardAbility_SlickMod_DanWin1Ink : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 1, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_Dan1Ink1Paralysis : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 1, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_Dan1Ink : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_BlackTieInk, 1, this.owner);
        }
    }

    #endregion

    #region --WORTHLESS AUTOMATONS--

    // Add +2 Power if user has Paralysis
    public class DiceCardAbility_SlickMod_PowerUp2SelfParalysis : DiceCardAbilityBase
    {
        public static string Desc = "Add +2 Power if user has Paralysis";
        
        public override string[] Keywords => new string[1] { "Paralysis_Keyword" };

        public override void BeforeRollDice()
        {
            BattleUnitModel owner = base.card.owner;
            if (owner != null && owner.bufListDetail.GetKewordBufStack(KeywordBuf.Paralysis) > 0)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 2
                });
            }
        }
    }

    // [On Hit] Gain 1 Focus next Scene; Inflict 1 Fragile next Scene
    public class DiceCardAbility_SlickMod_1Focus1Fragile : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Gain 1 Focus next Scene; Inflict 1 Fragile next Scene";

        public override string[] Keywords => new string[2] { "SlickMod_Focus", "Vulnerable_Keyword" };

        public override void OnSucceedAttack()
        {
            base.owner.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Orb_Focus, 1, base.owner);
            base.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Vulnerable, 1, base.owner);
        }
    }

    // [On Hit] Inflict Lock-On next Scene
    public class DiceCardAbility_SlickMod_LockOnAtk : DiceCardAbilityBase
    {
        public static string Desc = "[On Hit] Inflict Lock-On next Scene";

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddReadyBuf(new BattleUnitBuf_SlickMod_Orb_Lockon());
        }
    }

    #endregion

}
