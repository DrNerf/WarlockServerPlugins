using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkRift;
using WarlockServerDAL.Managers;

namespace UsersPlugin
{
    public class UsersPlugin : Plugin
    {
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

        public UsersPlugin()
        {
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
