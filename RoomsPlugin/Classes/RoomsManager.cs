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
            lock (player)
            {
                PlayersQueue.Enqueue(player); 
            }
        }


        /// <summary>
        /// Cancels the queue.
        /// </summary>
        /// <param name="player">The player.</param>
        public static void CancelQueue(int playerID)
        {
            var item = PlayersQueue
                    .FirstOrDefault(x => x.PlayerID == playerID);
            lock (item)
            {
                if (item != null)
                    PlayersQueue.Remove(item); 
            }
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
                    var player = PlayersQueue.Dequeue();
                    player.Room = room.RoomID;
                    room.Players.Add(player);
                }

                Rooms.Add(room.RoomID, room);

                if (OnRoomCreated != null)
                    OnRoomCreated(null, room);
            }
        }

        /// <summary>
        /// Gets the player room.
        /// </summary>
        /// <returns>The room.</returns>
        public static bool TryGetPlayerRoom(int playerID, out RoomModel room)
        {
            room = Rooms.Select(x => x.Value)
                .FirstOrDefault(x => x.Players.Any(y => y.PlayerID == playerID));

            return room != null;
        }

        /// <summary>
        /// Gets the room mates.
        /// </summary>
        /// <param name="playerID">The player identifier.</param>
        /// <returns>The Room mates.</returns>
        public static IEnumerable<PlayerInfoModel> GetRoomMates(int playerID)
        {
            RoomModel room = null;
            if (TryGetPlayerRoom(playerID, out room))
            {
                return room.Players;
            }
            return Enumerable.Empty<PlayerInfoModel>();
        }
    }
}
