using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages
{
    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Serialization;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    [AoContract((int)N3MessageType.Action)]
    public class ActionMessage : N3Message
    {
        public ActionMessage()
        {
            this.N3MessageType = N3MessageType.Action;
        }

        [AoMember(1)]
        [AoFlags("flag")]
        public int Unknown1 { get; set; }

        [AoUsesFlags("flag", typeof(int), FlagsCriteria.HasAny, new[] { 1 })]
        [AoMember(2)]
        public int Unknown2 { get; set; }

        [AoUsesFlags("flag", typeof(int), FlagsCriteria.HasAny, new[] { 1 })]
        [AoMember(3)]
        public Identity Unknown3 { get; set; }

    }
}
