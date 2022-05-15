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
            if (otherCollider.name.StartsWith("White Palace Fly") || otherCollider.name.StartsWith("Soul Totem white_Infinte"))
            {
                HeroController.instance.AddMPCharge(PlayerData.instance.MPReserveMax);
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
