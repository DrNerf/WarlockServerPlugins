using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkRift;
using RoomsPlugin.Classes;
using CommunicationLayer;
using CommunicationLayer.CommunicationModels.DataObjects;

namespace RoomsPlugin
{
    /// <summary>
    /// The rooms plugin.
    /// </summary>
    /// <seealso cref="DarkRift.Plugin" />
    public class RoomsPlugin : Plugin
    {

        #region Information
        public override string name
        {
            get { return "Rooms Plugin"; }
        }

        public override string version
        {
            get { return "1.0"; }
        }

        public override Command[] commands
        {
            get
            {
                return new Command[0];
            }
        }

        public override string author
        {
            get { return "Svetoslav Todorov"; }
        }

        public override string supportEmail
        {
            get { return "asd@asd.asd"; }
        }
        #endregion Information


        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsPlugin"/> class.
        /// </summary>
        public RoomsPlugin()
        {
            RoomsManager.Init();
            RoomsManager.OnRoomCreated += OnRoomCreated;
            ConnectionService.onData += OnDataReceived;
        }

        private void OnDataReceived(ConnectionService con, ref NetworkMessage data)
        {
            if (data.tag == (int)RoomsPluginRequestTags.BroadcastToRoom)
            {
                BroadcastToRoom(data);
            }
            if (data.tag == (int)RoomsPluginRequestTags.QueueRequest)
            {
                QueuePlayer(data);
            }
            if (data.tag == (int)RoomsPluginRequestTags.CancelQueueRequest)
            {
                CancelQueue(data);
            }
        }

        private static void CancelQueue(NetworkMessage data)
        {
            RoomsManager.CancelQueue(data.senderID);

            Interface.Log(string.Format("Player {0}:{1} canceled queue!", data.data, data.senderID));
        }

        private static void QueuePlayer(NetworkMessage data)
        {
            data.DecodeData();
            RoomsManager.EnqueuePlayer(new PlayerInfoModel
            {
                PlayerID = data.senderID,
                Name = data.data.ToString(),
            });

            Interface.Log(string.Format("Player {0}:{1} queued!", data.data, data.senderID));
        }

        private void BroadcastToRoom(NetworkMessage data)
        {
            RoomModel room = null;
            if (RoomsManager.TryGetPlayerRoom(data.senderID, out room))
            {
                foreach (var player in room.Players)
                {
                    var connection = DarkRiftServer
                        .GetConnectionServiceByID((ushort)player.PlayerID);

                    connection.SendReply((byte)data.subject, 0, data.data);
                }
            }
        }

        private void OnRoomCreated(object sender, RoomModel e)
        {
            if (e == null)
                return;

            foreach (var player in e.Players)
            {
                var connection = DarkRiftServer
                    .GetConnectionServiceByID((ushort)player.PlayerID);
                //do stuff here -> connection.SendReply()
            }
        }
    }
}
