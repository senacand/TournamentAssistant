﻿using System;
using TournamentAssistantShared;
using TournamentAssistantShared.Models.Packets;
using static TournamentAssistantShared.Packet;

namespace TournamentAssistantUI.Misc
{
    class ScoreboardClient : SystemClient
    {
        public event Action PlaySongSent;

        public ScoreboardClient(string endpoint, int port) : base(endpoint, port, "[Scoreboard]", Connect.ConnectTypes.Coordinator) { }

        protected override void Client_PacketRecieved(Packet packet)
        {
            base.Client_PacketRecieved(packet);

            if (packet.Type == PacketType.PlaySong)
            {
                PlaySongSent?.Invoke();
            }
        }
    }
}
