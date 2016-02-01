using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaranthineServer
{
    public static class Mnemonics
    {
        public enum Actions { clientlist, datafromothers, senddatanow, senddata, deviceType, extract, SaveDiaryContent,serverReply,
        volup, voldown, setVol, mute, monitorOff, monitorOn,heartBeat
        };
        public enum replyStatus { success,failure,internalFailure };
    }
}
