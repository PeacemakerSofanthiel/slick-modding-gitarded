using System;
using LOR_DiceSystem;
using KeywordUtil;
using EnumExtenderV2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Hat_Method.Hat_KeywordBuf;
using HyperCard;
using static UnityEngine.GraphicsBuffer;

namespace SlickRuinaMod
{

    #region - NORMAL STATUSES -

    // Flow State
    // Increase minimum and maximum roll value of all dice by +1.
    public class BattleUnitBuf_SlickMod_FlowState : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_FlowState";

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

        #region Stuff that handles Index aura
        private GameObject aura;

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            aura = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect("Prefabs/Battle/SpecialEffect/IndexRelease_Aura", 1f, owner.view, owner.view)?.gameObject;
        }

        public override void OnDie()
        {
            base.OnDie();
            Destroy();
        }

        public override void Destroy()
        {
            base.Destroy();
            DestroyAura();
        }

        public void DestroyAura()
        {
            if (aura != null)
            {
                UnityEngine.Object.Destroy(aura);
                aura = null;
            }
        }
        #endregion

    }

    // Damage Down
    // Damage Down used to be here, but it was literally the same effect as Hat Singularity's Damage Down, so I'll just use that instead

    // Cycle
    // Upon using a page, discard 1 random page, then draw 1 page, then lose 1 Cycle.
    public class BattleUnitBuf_SlickMod_Cycle : BattleUnitBuf
    {

        // Get keyword
        public override string keywordId
        {
            get
            {
                if (this._owner != null && this._owner.passiveDetail.HasPassive<PassiveAbility_SlickMod_GamerCycle>())
                {
                    return "SlickMod_GamerCycle";
                }
                else
                {
                    return "SlickMod_Cycle";
                }
            }
        }

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
        // Upon using a page, discard 2 random pages, then draw 2 pages, then lose 1 Cycle.
        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (this._owner != null && this._owner.passiveDetail.HasPassive<PassiveAbility_SlickMod_GamerCycle>())
            {
                base._owner.allyCardDetail.DisCardACardRandom();
                base._owner.allyCardDetail.DisCardACardRandom();
                base._owner.allyCardDetail.DrawCards(2);
            }
            else
            {
                base._owner.allyCardDetail.DisCardACardRandom();
                base._owner.allyCardDetail.DrawCards(1);
            }
            this.stack--;
            if (this.stack <= 0) this._owner.bufListDetail.RemoveBuf(this);
        }
    }

    public class BattleUnitBuf_SlickMod_InfernalOverheat : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_InfernalOverheat";

        // Thing
        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_InfernalOverheat;

            }
        }

        // Negative status type
        public override BufPositiveType positiveType
        {
            get
            {
                return BufPositiveType.Negative;
            }
        }

        // Eat speed dice
        public override int SpeedDiceBreakedAdder()
        {
            int num = UnityEngine.Mathf.Clamp(this.stack, 0, (this._owner.speedDiceResult.Count - 1));
            return num;
        }

        // Lose 1 stack
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            this.stack--;
            if(this.stack <= 0)
            {
                this.Destroy();
            }
        }
    }

    // Samsara
    // Used to use Speed Break. Gain Samsara equal to the amount of Haste this character has at the start of each Scene. Upon using a Combo Finisher page, gain Samsara equal to that page's original Cost.
    public class BattleUnitBuf_SlickMod_SparkSamsara : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_SparkSamsara";

        // Neutral status; doesn't need anything to override BufPositiveType

        // Thing
        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_SparkSamsara;
            }
        }
    }

    // Speed Break
    // Moving faster than visible motion.
    public class BattleUnitBuf_SlickMod_SparkSpeedBreak : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_SparkSpeedBreak";

        // Neutral status; doesn't need anything to override BufPositiveType

        // Thing
        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_SparkSpeedBreak;
            }
        }
        public override void OnRoundEnd()
        {
            Destroy();
        }
    }

    // Geared up
    // ‘Handling real power!’, deal +1 damage and stagger damage with attacks and enables use of a strong page. Lost at the end of the Scene.
    public class BattleUnitBuf_SlickMod_GearedUp : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_GearedUp";

        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_GearedUp;
            }
        }

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            base.OnSuccessAttack(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 1,
                breakDmg = 1
            });
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            Destroy();
        }
    }
    
    // Dangerous
    // ‘They intend to kill.’, Dice Power +1 and enables use of an extremely powerful page. Lost at the end of the Scene.
    public class BattleUnitBuf_SlickMod_Dangerous : BattleUnitBuf
    {
        // Get keyword
        public override string keywordId => "SlickMod_Dangerous";

        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_Dangerous;
            }
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            base.OnRollDice(behavior);
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = 1
            });
        }
        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            Destroy();
        }
    }

    public class BattleUnitBuf_SlickMod_BlackTieInk : BattleUnitBuf
    {
        public override string keywordId => "SlickMod_BlackTieInk";

        public override KeywordBuf bufType
        {
            get
            {
                return MyKeywordBufs.SlickMod_BlackTieInk;

            }
        }

        public override BufPositiveType positiveType
        {
            get
            {
                return BufPositiveType.Negative;
            }
        }
        public override int GetBreakDamageIncreaseRate()
        {
            if (this._owner != null && this._owner.passiveDetail.HasPassive<PassiveAbility_SlickMod_DanSurplusInkwork>())
            {
                return this.stack / 2;
            }
            else
            {
                return this.stack;
            }
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            base.BeforeRollDice(behavior);
            if (this.stack >= 10 && base.IsDefenseDice(behavior.Detail))
            {
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
            }
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            if (this._owner != null && this._owner.passiveDetail.HasPassive<PassiveAbility_SlickMod_DanSurplusInkwork>())
            {
                int damage = this.stack / 4;
                this._owner.TakeBreakDamage(damage / 2, DamageType.Buf);
                this.stack--;
                if (this.stack <= 0)
                {
                    this.Destroy();
                }
            }
            else
            {
                this._owner.TakeBreakDamage(this.stack / 4, DamageType.Buf);
                this.stack--;
                if (this.stack <= 0)
                {
                    this.Destroy();
                }
            }
        }
    }

    #endregion

    #region - COMBO STUFF -

    // Sakanagi set
    public class BattleUnitBuf_SakanagiComboPieceA : BattleUnitBuf
    {
        public override void OnRoundEnd()
        {
            if (this._owner.bufListDetail.HasBuf<BattleUnitBuf_SakanagiComboPieceB>())
            {
                this._owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_SakanagiComboFinisher());
            }
            Destroy();
        }
    }

    public class BattleUnitBuf_SakanagiComboPieceB : BattleUnitBuf
    {
        public override void OnRoundEnd()
        {
            if (this._owner.bufListDetail.HasBuf<BattleUnitBuf_SakanagiComboPieceA>())
            {
                this._owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_SakanagiComboFinisher());
            }
            Destroy();
        }
    }

    public class BattleUnitBuf_SakanagiComboFinisher : BattleUnitBuf
    {
        public override void OnRoundStart()
        {
            bool flag = this._owner.faction > Faction.Enemy;
            if (!flag)
            {
                this._owner.allyCardDetail.AddNewCard(new LorId("SlickMod", 52)).AddBuf(new BattleDiceCardBuf_SlickModTemp());
            }
            else
            {
                this._owner.personalEgoDetail.AddCard(new LorId("SlickMod", 52));
            }
        }

        public override void OnRoundEnd()
        {
            _owner.personalEgoDetail.RemoveCard(new LorId("SlickMod", 52));
            Destroy();
        }
    }

    // Tempest set
    public class BattleUnitBuf_TempestComboPieceA : BattleUnitBuf
    {
        public override void OnRoundEnd()
        {
            if (this._owner.bufListDetail.HasBuf<BattleUnitBuf_TempestComboPieceB>())
            {
                this._owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_TempestComboFinisher());
            }
            Destroy();
        }
    }

    public class BattleUnitBuf_TempestComboPieceB : BattleUnitBuf
    {
        public override void OnRoundEnd()
        {
            if (this._owner.bufListDetail.HasBuf<BattleUnitBuf_TempestComboPieceA>())
            {
                this._owner.bufListDetail.AddReadyBuf(new BattleUnitBuf_TempestComboFinisher());
            }
            Destroy();
        }
    }

    public class BattleUnitBuf_TempestComboFinisher : BattleUnitBuf
    {
        public override void OnRoundStart()
        {
            bool flag = this._owner.faction > Faction.Enemy;
            if (!flag)
            {
                this._owner.allyCardDetail.AddNewCard(new LorId("SlickMod", 55)).AddBuf(new BattleDiceCardBuf_SlickModTemp());
            }
            else
            {
                this._owner.personalEgoDetail.AddCard(new LorId("SlickMod", 55));
            }
        }

        public override void OnRoundEnd()
        {
            _owner.personalEgoDetail.RemoveCard(new LorId("SlickMod", 55));
            Destroy();
        }
    }

    #endregion

    #region - ETC -

    public class BattleUnitBuf_SlickMod_AddBackAfterX : BattleUnitBuf
    {
        public int _count;

        public LorId _cardId = LorId.None;

        // Notes card ID and turn count
        public BattleUnitBuf_SlickMod_AddBackAfterX(LorId cardId, int turnCount)
        {
            _cardId = cardId;
            _count = turnCount;
        }

        public override void OnRoundStart()
        {
            _count--;
            if (_count <= 0)
            {
                _owner.allyCardDetail.AddNewCard(_cardId);
                Destroy();
            }
        }
    }

    public class BattleUnitBuf_SlickMod_AddedSpeedDie : BattleUnitBuf
    {
        public override int SpeedDiceNumAdder()
        {
            return 1;
        }

        public override void OnRoundEnd()
        {
            base.OnRoundEnd();
            this.Destroy();
        }
    }

    // Page Buff to make card exhaust at end of Scene whether used or not
    public class BattleDiceCardBuf_SlickModTemp : BattleDiceCardBuf
    {
        public override void OnRoundStart()
        {
            _card.temporary = true;
        }
    }

    public class BattleUnitBuf_UsingTatsumaki : BattleUnitBuf
    {
        public override void OnRoundEnd()
        {
            Destroy();
        }
    }

    public class BattleUnitBuf_UsingKaryuken : BattleUnitBuf
    {
        public override void OnRoundEnd()
        {
            Destroy();
        }
    }

    #endregion

}
