using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationLayer
{
    public enum RoomsPluginRequestTags
    {
        QueueRequest = 7,
        CancelQueueRequest = 8,
        BroadcastToRoom = 10,
    }

    public enum RoomsPluginResponseTags
    {
        RoomJoinedResponse = 9,
    }
}
