using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaranthineServer
{
    public class ReplyPayload<T>
    {
        public Mnemonics.Actions Action;

        public String source;

        public T[] reply;

        public Int16 status;
        
    }
}
