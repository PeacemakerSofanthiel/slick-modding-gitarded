using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomMapUtility;

namespace SlickRuinaMod
{
    public class EnemyTeamStageManager_SlickMod_DrownedMapManager : EnemyTeamStageManager
    {
        CustomMapHandler cmh = CustomMapHandler.GetCMU(SlickModInitializer.packageId);
        public override void OnWaveStart()
        {
            cmh.InitCustomMap<DrownedMapManagering>("DrownedStage");
            cmh.EnforceMap(0);
        }
        public override void OnRoundStart()
        {
            base.OnRoundStart();
            cmh.EnforceMap(0);
        }

        public class DrownedMapManagering : CustomMapManager
        {
            protected override string[] CustomBGMs
            {
                get
                {
                    return new string[]
                    {
                       "DeepBlue.ogg"
                    };
                }
            }
        }
    }
}
