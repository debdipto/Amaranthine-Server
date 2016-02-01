using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmaranthineServer
{
    public class MessageContainer
    {
        public String username;
        public String message;

        public MessageContainer()
        {

        }

        public MessageContainer(String _username,String _message)
        {
            username = _username;
            message = _message;
        }
    }
}
