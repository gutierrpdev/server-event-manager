using System;

namespace ServerManagement
{
    // simple data holder for gameEvents (here named serverEvents to prevent ambiguity).
    [Serializable]
    public class ServerEvent
    {
        public int userId;
        public int timestamp;
        public string gameName;
        public string name;
        public ServerEventParameter[] parameters;
        public ServerEvent(int userId, int timestamp, string gameName, string name, ServerEventParameter[] parameters = null)
        {
            this.userId = userId;
            this.gameName = gameName;
            this.name = name;
            this.parameters = parameters;
            this.timestamp = timestamp;
        }

        #region event_constants
        public const string EXPERIMENT_START = "EXPERIMENT_START";
        public const string EXPERIMENT_END = "EXPERIMENT_END";
        public const string TUTORIAL_START = "TUTORIAL_START";
        public const string TUTORIAL_END = "TUTORIAL_END";
        public const string LEVEL_START = "LEVEL_START";
        public const string LEVEL_END = "LEVEL_END";
        public const string PLAYER_DEATH = "PLAYER_DEATH";
        public const string GAME_PAUSE = "GAME_PAUSE";
        public const string GAME_RESUME = "GAME_RESUME";
        #endregion
    }
}