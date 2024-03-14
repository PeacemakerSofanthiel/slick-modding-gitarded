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
    public class DiceCardAbility_SlickMod_SparkRepeatSamsara : DiceCardAbilityBase
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

    // Tatsumaki: Dice Effect 2
    // [On Hit] Inflict 2 Thunderstuck next Scene
    public class DiceCardAbility_SlickMod_OnHitThunderStruck2 : DiceCardAbilityBase
    {
        public override string[] Keywords => new string[1] { "Aftermath_Thunderstruck_Keyword" };

        public static string Desc = "[On Hit] Inflict 2 Thunderstuck next Scene";

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(AftermathBufs.AftermathBufs.Thunderstruck, 2, base.owner);
        }
    }

    // Karyuken: Dice Effect 2
    // [On Hit] Inflict 5 Thunderstuck next Scene
    public class DiceCardAbility_SlickMod_OnHitThunderStruck5 : DiceCardAbilityBase
    {
        public override string[] Keywords => new string[1] { "Aftermath_Thunderstruck_Keyword" };

        public static string Desc = "[On Hit] Inflict 5 Thunderstuck next Scene";

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(AftermathBufs.AftermathBufs.Thunderstruck, 5, base.owner);
        }
    }

    // Sakanagi: Dice Effect 1
    // [On Hit] Inflict Thunderstuck next Scene equal to Samsara on self (max. 10)
    public class DiceCardAbility_SlickMod_OnHitThunderStruckSamsara : DiceCardAbilityBase
    {
        public override string[] Keywords => new string[1] { "Aftermath_Thunderstruck_Keyword" };

        public static string Desc = "[On Hit] Inflict Thunderstuck next Scene equal to Samsara on self (max. 10)";

        public override void OnSucceedAttack()
        {
            BattleUnitBuf activatedBuf = owner.bufListDetail.GetActivatedBuf(MyKeywordBufs.SlickMod_SparkSamsara);
            base.card.target?.bufListDetail.AddKeywordBufByCard(AftermathBufs.AftermathBufs.Thunderstruck, (activatedBuf.stack < 10) ? activatedBuf.stack : 10, base.owner);
        }
    }

    // Wind Step: Dice Effect 1
    // [On Hit] Inflict 3 Thunderstuck next Scene
    public class DiceCardAbility_SlickMod_OnHitThunderStruck3 : DiceCardAbilityBase
    {
        public override string[] Keywords => new string[1] { "Aftermath_Thunderstruck_Keyword" };

        public static string Desc = "[On Hit] Inflict 3 Thunderstuck next Scene";

        public override void OnSucceedAttack()
        {
            base.card.target?.bufListDetail.AddKeywordBufByCard(AftermathBufs.AftermathBufs.Thunderstruck, 3, base.owner);
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

    #region --DROWNED--
    //In time, you will know the tragic extent of my failings.
    public class DiceCardAbility_SlickMod_Drowned1Paralysis1Sinking : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 1, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedBrainViolence : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, 3, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 2, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedDepressionDice1 : DiceCardAbilityBase
    {
        public override bool Invalidity
        {
            get
            {
                BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf == null)
                {
                    return true;
                }
                else if (battleUnitBuf.stack < 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 2, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedDepressionDice2 : DiceCardAbilityBase
    {
        public override bool Invalidity
        {
            get
            {
                BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf == null)
                {
                    return true;
                }
                else if (battleUnitBuf.stack < 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 2, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedDepressionDice3 : DiceCardAbilityBase
    {
        public override bool Invalidity
        {
            get
            {
                BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf == null)
                {
                    return true;
                }
                else if (battleUnitBuf.stack < 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 2, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedLoudNoiseDie : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            base.behavior.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 2, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedDropDie : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                if (battleUnitModel != this.card.target)
                {
                    battleUnitModel.TakeBreakDamage(7, DamageType.Card_Ability);
                    battleUnitModel.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 2, this.owner);
                    battleUnitModel.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, 1, this.owner);
                }
            }
        }
    }
    public class DiceCardAbility_SlickMod_DrownedTwistingEvade : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 2, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, 1, this.owner);
        }
    }

    public class DiceCardAbility_SlickMod_Drowned1Drowning : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 1, this.owner);
        }
    }

    public class DiceCardAbility_SlickMod_Drowned2Sinking : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 2, this.owner);
        }
    }

    public class DiceCardAbility_SlickMod_Drowned1DrowningPW : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 1, this.owner);
        }
    }

    public class DiceCardAbility_SlickMod_DrownedMeaninglessBlockDie : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, 2, this.owner);
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 2, this.owner);
        }
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack > 5)
                {
                    battleUnitBuf.stack += -5;
                }
                else
                {
                    battleUnitBuf.Destroy();
                }
            }
        }


    }

    public class DiceCardAbility_SlickMod_DrownedMeaninglessAnnihilate : DiceCardAbilityBase
    {
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf == null)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = -5
                });
            }
            else if (battleUnitBuf.stack < 5)
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = -5
                });
            }
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 3, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, 1, this.owner);
        }
    }
    public class DiceCardAbility_SlickMod_DrownedMeaninglessReroll : DiceCardAbilityBase
    {
        private int count;
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            if (this.count < 3)
            {
                this.ActivateBonusAttackDice();
                this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedOmegaCringe, 2, this.owner);
                this.count++;
            }
        }
    }
    public class DiceCardAbility_SlickMod_DrownedNumbnessCounter : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            base.OnWinParrying();
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 3, this.owner);
            this.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_SinkingCount, 2, this.owner);
            this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 2, this.owner);
        }
        public override void OnLoseParrying()
        {
            base.OnLoseParrying();
            BattleUnitBuf battleUnitBuf = this.card.target.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack > 3)
                {
                    battleUnitBuf.stack += -3;
                }
                else
                {
                    battleUnitBuf.Destroy();
                }
            }
        }
    }
    public class DiceCardAbility_SlickMod_DrownedEmptyDie : DiceCardAbilityBase
    {
        //Surelythiswillwork
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            BattleDiceCardModel sus = BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(new LorId("SlickMod", 170103), false));
            List<BattleUnitModel> list = new List<BattleUnitModel>();
            if (this.owner.GetFixedTargets() != null && this.owner.GetFixedTargets().FindAll(x => x.IsTargetable(this.owner)).Count > 0)
            {
                list = this.owner.GetFixedTargets().FindAll(x => x.IsTargetable(this.owner));
            }
            else
            {
                list = BattleObjectManager.instance.GetAliveList_opponent(this.owner.faction).FindAll(x => x.IsTargetable(this.owner));
            }
            if (list.Count > 0 && sus != null)
            {
                BattleUnitModel battleUnitModel2 = RandomUtil.SelectOne(list);
                BattleDiceCardModel battleDiceCardModel = BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(sus.GetID()));
                if (battleDiceCardModel != null)
                {
                    battleDiceCardModel.costSpended = true;
                    DiceCardSelfAbilityBase diceCardSelfAbilityBase = battleDiceCardModel.CreateDiceCardSelfAbilityScript();
                    if (diceCardSelfAbilityBase != null)
                    {
                        BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = new BattlePlayingCardDataInUnitModel
                        {
                            card = battleDiceCardModel,
                            owner = this.owner,
                            earlyTarget = battleUnitModel2,
                            earlyTargetOrder = RandomUtil.Range(0, battleUnitModel2.speedDiceResult.Count - 1),
                            slotOrder = 0,
                            speedDiceResultValue = this.owner.GetSpeed(0),
                            cardAbility = diceCardSelfAbilityBase
                        };
                        diceCardSelfAbilityBase.card = battlePlayingCardDataInUnitModel;
                        battlePlayingCardDataInUnitModel.owner = this.owner;
                        battlePlayingCardDataInUnitModel.target = battlePlayingCardDataInUnitModel.earlyTarget;
                        battlePlayingCardDataInUnitModel.targetSlotOrder = battlePlayingCardDataInUnitModel.earlyTargetOrder;
                        foreach (BattleUnitModel battleUnitModel22 in BattleObjectManager.instance.GetAliveList(Faction.Player))
                        {
                            bool flag3 = battleUnitModel22 != null && battleUnitModel22 != battlePlayingCardDataInUnitModel.target;
                            if (flag3)
                            {
                                battlePlayingCardDataInUnitModel.subTargets.Add(new BattlePlayingCardDataInUnitModel.SubTarget
                                {
                                    target = battleUnitModel22,
                                    targetSlotOrder = RandomUtil.Range(0, battleUnitModel22.speedDiceResult.Count - 1)
                                });
                            }
                        }
                        battlePlayingCardDataInUnitModel.ResetCardQueueWithoutStandby();
                        Singleton<StageController>.Instance.GetAllCards().Insert(0, battlePlayingCardDataInUnitModel);
                    }
                    else
                    {
                        BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel = new BattlePlayingCardDataInUnitModel
                        {
                            card = battleDiceCardModel,
                            owner = this.owner,
                            earlyTarget = battleUnitModel2,
                            earlyTargetOrder = RandomUtil.Range(0, battleUnitModel2.speedDiceResult.Count - 1),
                            slotOrder = 0,
                            speedDiceResultValue = this.owner.GetSpeed(0)
                        };
                        battlePlayingCardDataInUnitModel.owner = this.owner;
                        battlePlayingCardDataInUnitModel.target = battlePlayingCardDataInUnitModel.earlyTarget;
                        battlePlayingCardDataInUnitModel.targetSlotOrder = battlePlayingCardDataInUnitModel.earlyTargetOrder;
                        foreach (BattleUnitModel battleUnitModel222 in BattleObjectManager.instance.GetAliveList(Faction.Player))
                        {
                            bool flag3 = battleUnitModel222 != null && battleUnitModel222 != battlePlayingCardDataInUnitModel.target;
                            if (flag3)
                            {
                                battlePlayingCardDataInUnitModel.subTargets.Add(new BattlePlayingCardDataInUnitModel.SubTarget
                                {
                                    target = battleUnitModel222,
                                    targetSlotOrder = RandomUtil.Range(0, battleUnitModel222.speedDiceResult.Count - 1)
                                });
                            }
                        }
                        battlePlayingCardDataInUnitModel.ResetCardQueueWithoutStandby();
                        Singleton<StageController>.Instance.GetAllCards().Insert(0, battlePlayingCardDataInUnitModel);
                    }
                }
            }
        }
    }
    public class DiceCardAbility_SlickMod_DrownedGnawingDie : DiceCardAbilityBase
    {
        private bool didihitthem;
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            didihitthem = false;
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            didihitthem = true;
            base.behavior.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 2, this.owner);
            base.behavior.card.target.bufListDetail.AddKeywordBufByCard(MyKeywordBufs.SlickMod_Sinking, 5, this.owner);
        }
        public override void AfterAction()
        {
            base.AfterAction();
            if (!didihitthem)
            {
                foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
                {
                    BattleUnitBuf battleUnitBuf = battleUnitModel.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                    BattleUnitBuf battleUnitBuf2 = battleUnitModel.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_SinkingCount);
                    if (battleUnitBuf != null)
                    {
                        if (battleUnitBuf.stack <= 3)
                        {
                            battleUnitBuf.Destroy();
                        }
                        else
                        {
                            battleUnitBuf.stack -= 3;
                        }
                    }
                    if (battleUnitBuf2 != null)
                    {
                        battleUnitBuf2.Destroy();
                    }
                }
            }
        }
    }
    public class DiceCardAbility_SlickMod_DrownedNoMemories : DiceCardAbilityBase
    {
        private bool didihitthem;
        public override void BeforeRollDice()
        {
            base.BeforeRollDice();
            didihitthem = false;
        }
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            didihitthem = true;
            BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
            if (battleUnitBuf != null)
            {
                if (battleUnitBuf.stack <= 2)
                {
                    battleUnitBuf.Destroy();
                }
                else
                {
                    battleUnitBuf.stack -= 2;
                }
            }
            base.behavior.card.target.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, 2, this.owner);
        }
        public override void AfterAction()
        {
            base.AfterAction();
            if (!didihitthem)
            {
                foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
                {
                    BattleUnitBuf battleUnitBuf = battleUnitModel.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                    if (battleUnitBuf != null)
                    {
                        if (battleUnitBuf.stack <= 5)
                        {
                            battleUnitBuf.Destroy();
                        }
                        else
                        {
                            battleUnitBuf.stack -= 5;
                        }
                    }
                }
                this.owner.TakeBreakDamage(100, DamageType.ETC);
            }
        }
    }
    public class DiceCardAbility_SlickMod_DrownedFuckYou : DiceCardAbilityBase
    {
        public override void OnSucceedAttack()
        {
            base.OnSucceedAttack();
            foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList((base.owner.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy))
            {
                BattleUnitBuf battleUnitBuf = battleUnitModel.bufListDetail.GetActivatedBufList().Find((BattleUnitBuf x) => x is BattleUnitBuf_SlickMod_DrownedDrowning);
                if (battleUnitBuf != null)
                {
                    int inwaterchimpswilldrown = battleUnitBuf.stack;
                    battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(MyKeywordBufs.SlickMod_DrownedDrowning, inwaterchimpswilldrown, this.owner);
                }
            }
        }
    }
    #endregion

}
