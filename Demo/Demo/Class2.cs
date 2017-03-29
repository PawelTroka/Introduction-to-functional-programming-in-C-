using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Demo3
{
    class Modem
    {
        public static void SendData(byte[] bytes, Action<byte[]> sendingAction, Action<string> loggingAction)
        {
            try
            {
                sendingAction(bytes);
            }
            catch (Exception e)
            {
                loggingAction(e.Message);
            }
        }

        public static byte[] ReceiveData(Func<byte[]> receivingFunc, Action<string> loggingAction)
        {
            byte[] output = null;
            try
            {
                output = receivingFunc();
            }
            catch (Exception e)
            {
                loggingAction(e.Message);
            }
            return output;
        }
    }

    class Program
    {
        private string _senderIpAddress = "123.123.123.123";
        private string _receiverIpAddress = "211.222.215.123";

        void Main()
        {

        }
    }
}
