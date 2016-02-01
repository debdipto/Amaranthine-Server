using AmaranthineServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusClient
{
    class IMPacket
    {
        public String SourceUsername, TargetUsername, Message;
        public Mnemonics.Actions Action;

        public IMPacket()
        {
            SourceUsername = String.Empty;
            TargetUsername = String.Empty;
            Message = String.Empty;
        }
    }
}
