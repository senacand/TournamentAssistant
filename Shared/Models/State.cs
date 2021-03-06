﻿using System;
using TournamentAssistantShared.Models;

/**
 * Represents the current state of the tournament. This is intended
 * to be sent to newly-connected match coordinators, AND *NOT* used
 * as a way to propegate state changes to currently connected cordinators
 */

namespace TournamentAssistantShared.Models
{
    [Serializable]
    public class State
    {
        public ServerSettings ServerSettings { get; set; }
        public Player[] Players { get; set; }
        public MatchCoordinator[] Coordinators { get; set; }
        public Match[] Matches { get; set; }
    }
}
