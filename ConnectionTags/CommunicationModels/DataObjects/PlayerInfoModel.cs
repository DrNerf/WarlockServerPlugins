using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationLayer.CommunicationModels.DataObjects
{
    [Serializable]
    public class PlayerInfoModel
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public Guid Room { get; set; }
    }
}
