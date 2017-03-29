using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Demo2
{
    internal interface ISender
    {
        void SendData(byte[] bytes);
    }
    internal interface IReceiver
    {
        byte[] ReceiveData();
    }

    interface ILogger
    {
        void Log(string error);
    }


    class Sender : ISender
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



    class Receiver : IReceiver
    {
        private string ipAddress;

        public Receiver(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public byte[] ReceiveData()
        {
            return new byte[] { 123, 11, 22, 211 };
        }

    }


    class Logger : ILogger
    {
        public void Log(string error)
        {

        }
    }

    class Modem
    {

        private readonly ISender _sender;
        private readonly IReceiver _receiver;
        private readonly ILogger _logger;

        public Modem(ISender sender, IReceiver receiver, ILogger logger)
        {
            _sender = sender;
            _receiver = receiver;
            _logger = logger;
        }

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

    class Program
    {
        private string _senderIpAddress= "123.123.123.123";
        private string _receiverIpAddress = "211.222.215.123";

        void Main()
        {
            var container = new UnityContainer();
            container.RegisterType<ISender, Sender>(new InjectionFactory(c => new Sender(_senderIpAddress)));
            container.RegisterType<IReceiver, Receiver>(new InjectionFactory(c => new Receiver(_receiverIpAddress)));
            container.RegisterType<ILogger,Logger>(new ContainerControlledLifetimeManager());

            var modem = container.Resolve<Modem>();
        }
    }
}
