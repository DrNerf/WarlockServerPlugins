using System;

namespace CommunicationLayer.CommunicationModels
{
    [Serializable]
    public class TryLoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
