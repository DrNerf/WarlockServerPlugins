using CommunicationLayer.CommunicationModels.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsPlugin
{

    /// <summary>
    /// Model representing a room.
    /// </summary>
    public class RoomModel
    {

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        public Guid RoomID { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public IList<PlayerInfoModel> Players { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="RoomModel"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public RoomModel(Guid id)
        {
            RoomID = id;
        }
    }
}
