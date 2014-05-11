using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages
{
    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Serialization;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    [AoContract((int)N3MessageType.ResearchUpdate)]
    public class ResearchUpdateMessage : N3Message
    {
        public ResearchUpdateMessage()
        {
            this.N3MessageType = N3MessageType.ResearchUpdate;
        }

        [AoMember(1)]
        public byte Unknown1 { get; set; }

        [AoMember(1,SerializeSize = ArraySizeType.NullTerminated)]
        public ResearchUpdateEntry[] Entries { get; set; }

    }

}
