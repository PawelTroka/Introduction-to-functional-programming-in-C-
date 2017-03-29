using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Sender
    {
        private string ipAddress;

        public Sender(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public void SendData(byte[] bytes)
        {

        }
    }

    class Receiver
    {
        private string ipAddress;

        public Receiver(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public byte[] ReceiveData()
        {
            return new byte[] {123,11,22,211};
        }
       
    }

    class Logger
    {
        public void Log(string error)
        {
            
        }
    }

    class Modem
    {

        private readonly Sender _sender = new Sender("123.123.123.123");
        private readonly Receiver _receiver = new Receiver("211.222.215.123");
        private readonly Logger _logger = new Logger();
        public void SendData(byte[] bytes)
        {
            try
            {
                _sender.SendData(bytes);
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }

        public byte[] ReceiveData()
        {
            byte[] output = null;
            try
            {
                output = _receiver.ReceiveData();
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
            return output;
        }
    }
}
