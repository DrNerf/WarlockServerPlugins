using Common;
using CommunicationLayer.CommunicationModels.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RoomsPlugin.Classes
{

    /// <summary>
    /// Class that manages rooms.
    /// </summary>
    public static class RoomsManager
    {

        /// <summary>
        /// The room size
        /// </summary>
        public const int RoomSize = 4;


        /// <summary>
        /// The rooms
        /// </summary>
        public static Dictionary<Guid, RoomModel> Rooms = new Dictionary<Guid, RoomModel>();

        /// <summary>
        /// Event raised when a room is created.
        /// </summary>
        public static EventHandler<RoomModel> OnRoomCreated { get; set; }

        private static ListQueue<PlayerInfoModel> PlayersQueue = new ListQueue<PlayerInfoModel>();


        /// <summary>
        /// Enqueues the player.
        /// </summary>
        /// <param name="player">The player.</param>
        public static void EnqueuePlayer(PlayerInfoModel player)
        {
            PlayersQueue.Enqueue(player);
        }


        /// <summary>
        /// Cancels the queue.
        /// </summary>
        /// <param name="player">The player.</param>
        public static void CancelQueue(PlayerInfoModel player)
        {
            var item = PlayersQueue
                .FirstOrDefault(x => x.PlayerID == player.PlayerID);
            if (item != null)
                PlayersQueue.Remove(item);
        }


        /// <summary>
        /// Initializes the manager.
        /// Should be called once, when the server starts!
        /// </summary>
        public static void Init()
        {
            PlayersQueue.OnEnqueue += OnPlayerEnqueue;
        }

        private static void OnPlayerEnqueue(object sender, PlayerInfoModel e)
        {
            if (PlayersQueue.Count < RoomSize)
                return;

            var room = new RoomModel(Guid.NewGuid());
            lock (room)
            {
                room.Players = new List<PlayerInfoModel>();
                for (int i = 0; i < RoomSize; i++)
                {
                    room.Players.Add(PlayersQueue.Dequeue());
                }

                Rooms.Add(room.RoomID, room);
                OnRoomCreated(null, room);
            }
        }
    }
}
