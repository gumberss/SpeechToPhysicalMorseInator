using System.IO.Ports;

namespace TextToPhysicalInator
{
    internal class ArduinoService
    {
        private static SerialPort serialPort;

        static ArduinoService()
        {
            var ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                Console.WriteLine("Nenhuma porta encontrada para conexão");
                return;
            }
            serialPort = new SerialPort();

            if (serialPort.IsOpen == false)
            {
                serialPort.PortName = "COM3";
            }
        }

        private static object _lock = new object();

        public static void SendMessage(String message)
        {
            lock (_lock)
            {
                serialPort.Open();

                serialPort.Write(message);

                serialPort.Close();
            }
        }
    }
}
