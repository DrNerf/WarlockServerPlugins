using System;

namespace CommunicationLayer.CommunicationModels.Responses
{
    [Serializable]
    public class TryLoginResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Username { get; set; }
    }
}
