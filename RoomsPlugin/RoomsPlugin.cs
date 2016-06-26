using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkRift;
using RoomsPlugin.Classes;

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
        }

        private void OnRoomCreated(object sender, RoomModel e)
        {
            foreach (var player in e.Players)
            {
                var connection = DarkRiftServer
                    .GetConnectionServiceByID((ushort)player.PlayerID);
                //do stuff here -> connection.SendReply()
            }
        }
    }
}
