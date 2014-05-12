﻿#region License

// Copyright (c) 2005-2014, CellAO Team
// 
// 
// All rights reserved.
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

#endregion

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages.OrgServerMessages
{
    #region Usings ...

    using SmokeLounge.AOtomation.Messaging.Serialization;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    #endregion

    [AoContract((byte)OrgServerMessageType.OrgInfo)]
    public class OrgInfoMessage : OrgServerMessage
    {
        #region Constructors and Destructors

        public OrgInfoMessage()
        {
            this.OrgServerMessageType = OrgServerMessageType.OrgInfo;
        }

        #endregion

        #region AoMember Properties

        [AoMember(0, SerializeSize = ArraySizeType.Int16)]
        public string Description { get; set; }

        [AoMember(1, SerializeSize = ArraySizeType.Int16)]
        public string Objective { get; set; }

        [AoMember(2, SerializeSize = ArraySizeType.Int16)]
        public string History { get; set; }

        [AoMember(3, SerializeSize = ArraySizeType.Int16)]
        public string GoverningForm { get; set; }

        [AoMember(4, SerializeSize = ArraySizeType.Int16)]
        public string LeaderName { get; set; }

        [AoMember(5, SerializeSize = ArraySizeType.Int16)]
        public string Rank { get; set; }

        [AoMember(6, SerializeSize = ArraySizeType.X3F1)]
        public object[] Unknown3 { get; set; }

        #endregion
    }
}