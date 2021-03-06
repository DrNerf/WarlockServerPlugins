﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomsPlugin.Classes;
using CommunicationLayer.CommunicationModels.DataObjects;
using RoomsPlugin;

namespace Tests.RoomsPlugin
{
    [TestClass]
    public class RoomsManagerTests
    {
        [TestMethod]
        public void TestQueue()
        {
            RoomsManager.Init();
            for (int i = 0; i < 2; i++)
            {
                RoomsManager.EnqueuePlayer(new PlayerInfoModel { PlayerID = i });
            }
            Assert.AreEqual<int>(0, RoomsManager.Rooms.Count);

            for (int i = 2; i < 4; i++)
            {
                RoomsManager.EnqueuePlayer(new PlayerInfoModel { PlayerID = i });
            }
            Assert.AreEqual<int>(1, RoomsManager.Rooms.Count);
        }

        [TestMethod]
        public void TestRoomGetters()
        {
            RoomsManager.Init();
            for (int i = 0; i < 4; i++)
            {
                RoomsManager.EnqueuePlayer(new PlayerInfoModel 
                {
                    PlayerID = i
                });
            }
            Assert.IsTrue(RoomsManager.GetRoomMates(0).Any());
            RoomModel room;
            Assert.IsTrue(RoomsManager.TryGetPlayerRoom(0, out room));
        }
    }
}
