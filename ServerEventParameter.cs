using System;

namespace ServerEvents
{
    // simple data holder for gameEventParameters.
    [Serializable]
    public class ServerEventParameter
    {
        public string name;
        public string value;
        public ServerEventParameter(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        #region parameter_constants
        public const string LEVEL_NUMBER = "LEVEL_NUMBER";
        public const string LOCATION_X = "LOCATION_X";
        public const string LOCATION_Y = "LOCATION_Y";
        public const string LOCATION_Z = "LOCATION_Z";
        public const string SCORE = "SCORE";
        #endregion
    }
}
