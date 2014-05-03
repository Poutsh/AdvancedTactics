using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;

namespace Advanced_Tactics.Network
{
    enum SessionProperty { GameMode, SkillLevel, ScoreToWin }
    enum GameMode { PlayervsAI, AIvsAI }
    enum SkillLevel { Beginner, Intermediate, Advanced }

    public class Session
    {
        NetworkSession session;


        void Host()
        {
            NetworkSessionProperties sessionProperties = new NetworkSessionProperties();

            sessionProperties[(int)SessionProperty.GameMode] = (int)GameMode.PlayervsAI;
            sessionProperties[(int)SessionProperty.SkillLevel] = (int)SkillLevel.Beginner;
            sessionProperties[(int)SessionProperty.ScoreToWin] = 100;

            NetworkSession session;

            session = NetworkSession.Create(NetworkSessionType.Local, 1, 2);

            session.AllowHostMigration = true;
            session.AllowJoinInProgress = true;

           
        }

        protected void AddNetworkingEvents()
        {
            session.GamerJoined += new EventHandler<GamerJoinedEventArgs>(session_GamerJoined);
            session.GamerLeft += new EventHandler<GamerLeftEventArgs>(session_GamerLeft);
            session.GameStarted += new EventHandler<GameStartedEventArgs>(session_GameStarted);
            session.GameEnded += new EventHandler<GameEndedEventArgs>(session_GameEnded);
            session.SessionEnded += new EventHandler<NetworkSessionEndedEventArgs>(session_SessionEnded);
        }

        protected void RemoveNetworkingEvents()
        {
            session.GamerJoined -= new EventHandler<GamerJoinedEventArgs>(session_GamerJoined);
            session.GamerLeft -= new EventHandler<GamerLeftEventArgs>(session_GamerLeft);
            session.GameStarted -= new EventHandler<GameStartedEventArgs>(session_GameStarted);
            session.GameEnded -= new EventHandler<GameEndedEventArgs>(session_GameEnded);
            session.SessionEnded -= new EventHandler<NetworkSessionEndedEventArgs>(session_SessionEnded);
        }

        //Event Handling
        void session_GamerJoined(object sender, GamerJoinedEventArgs e)
        {

        }
        void session_GamerLeft(object sender, GamerLeftEventArgs e)
        {
        }
        void session_GameStarted(object sender, GameStartedEventArgs e)
        {
        }
        void session_GameEnded(object sender, GameEndedEventArgs e)
        {
        }
        void session_SessionEnded(object sender, NetworkSessionEndedEventArgs e)
        {
        }

    }
}
