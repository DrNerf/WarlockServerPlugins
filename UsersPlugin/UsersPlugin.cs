using CommunicationLayer;
using CommunicationLayer.CommunicationModels;
using CommunicationLayer.CommunicationModels.Responses;
using DarkRift;
using System;
using WarlockServerDAL;
using WarlockServerDAL.Managers;

namespace UsersPlugin
{
    public class UsersPlugin : Plugin
    {
        #region Information
        public override string name
        {
            get { return "Users Plugin"; }
        }

        public override string version
        {
            get { return "1.0"; }
        }

        public override Command[] commands
        {
            get 
            {
                return new Command[]
                {
                    new Command("RegisterUser", "Registers a new user in the database.", RegisterCommand),
                }; 
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

        public UsersPlugin()
        {
            ConnectionService.onData += OnDataReceived;
        }

        private void OnDataReceived(ConnectionService con, ref NetworkMessage data)
        {
            if(data.tag == (int)UsersPluginRequestTags.TryLoginRequest)
            {
                var requestPayload = data.data as GenericPayload<TryLoginRequestModel>;
                if(requestPayload == null)
                {
                    Interface.LogError("Could not parse the payload");
                    Interface.LogError(data.ToString());
                    Interface.LogError(data.GetType().ToString());
                    return;
                }
                using (UsersManager manager = new UsersManager())
                {
                    var payload = new GenericPayload<TryLoginResponseModel>();
                    GameUser user = null;
                    bool isSuccess = manager.TryLogin(requestPayload.Value.Username,
                                                requestPayload.Value.Password,
                                                out user);
                    payload.Value = new TryLoginResponseModel
                    {
                        IsSuccess = isSuccess,
                        Username = user == null ? string.Empty : user.Username,
                    };

                    con.SendReply((int)UsersPluginResponseTags.TryLoginResponse,
                                data.subject,
                                payload);
                }
            }
        }

        public void RegisterCommand(string[] args)
        {
            if (args.Length != 2)
            {
                Interface.LogError("Register needs 2 arguments, username and password!");
                return;
            }

            try
            {
                using (UsersManager manager = new UsersManager())
                {
                    var user = manager.RegisterUser(args[0], "default@default.def", args[1]);
                    Interface.Log(string.Format("User {0} registered!", user.Username));      
                }
            }
            catch (Exception e)
            {
                Interface.LogError("User could not be registered!");
                Interface.LogError(e.Message);
                throw;
            }
        }

    }
}
