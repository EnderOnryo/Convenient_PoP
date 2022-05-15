using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modding;
using UnityEngine;

namespace Convenient_PoP
{
    public class Convenient_PoP : Mod
    {
        public Convenient_PoP() : base("Convenient PoP") { }
        public override string GetVersion() => "1.0";


        public override void Initialize()
        {
            ModHooks.SlashHitHook += SlashHitHandler;
        }

        
        /* This causes the player's soul to be filled when the player hits a PoP Soul Totem or a Wingsmould with their Nail.
         * This does not work with any Narts.
         */
        public void SlashHitHandler(Collider2D otherCollider, GameObject slash)
        {
            var soulTotemFsm = otherCollider.gameObject.LocateMyFSM("soul_totem");
            var whitePalaceFlyFsm = otherCollider.gameObject.LocateMyFSM("Control");
            if (soulTotemFsm != null)
            {
                // check if infinite, as they miss the "Mesh Renderer Off" & "Depleted" states
                bool isInfinite = true;
                foreach (var state in soulTotemFsm.FsmStates)
                {
                    if (state.Name is "Depleted" or "Mesh Renderer Off")
                    {
                        isInfinite = false;
                    }
                }

                if (isInfinite)
                {
                    HeroController.instance.AddMPCharge(PlayerData.instance.GetInt(nameof(PlayerData.instance.MPReserveMax)));
                }
            }
            else if (whitePalaceFlyFsm != null)
            {
                // check if actually white palace fly
                int isWPFlyCount = 0;
                List<string> wpFlyStates = new List<string>()
                {
                    "Init", "Idle", "Stationary", "Audio", "Wait", "Journal Update?", "Journal Entry?", "Wound"
                };
                foreach (var state in soulTotemFsm.FsmStates)
                {
                    if (!wpFlyStates.Contains(state.Name))
                    {
                        isWPFlyCount++;
                    }
                }

                if (isWPFlyCount == wpFlyStates.Count)
                {
                    HeroController.instance.AddMPCharge(PlayerData.instance.GetInt(nameof(PlayerData.instance.MPReserveMax)));
                }
            }
        }
    }
}

/*To do
 * Make Narts work
 * Make Soul Orbs not spawn
 * Add a canFocus check and if yes heal the player to full
 * Make the mod appear in the Mod Menu so it can be disabled
 */
