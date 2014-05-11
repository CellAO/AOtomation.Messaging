using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages
{
    using SmokeLounge.AOtomation.Messaging.Serialization;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    [AoContract((int)N3MessageType.Action)]
    public class ActionMessage:N3Message
    {
        public ActionMessage()
        {
            this.N3MessageType = N3MessageType.Action;
        }

  [AoMember(1)]
        [AoFlags("flag")]
        public int Unknown1 { get; set; }

        [AoUsesFlags("flag",FlagsCriteria.HasAll,new[]{1})]


/*
Packet length: 45
 01 05 00 0A 00 01 00 2D 00 00 0F 3E 00 32 BC 90     .......-...>.2..
 20 49 52 7C 00 00 C7 6A 00 00 08 00 01 00 00 00      IR|...j........
 01 00 00 00 66 00 00 C3 50 00 32 BC 90              ....f...P.2..
*/
    }
}
