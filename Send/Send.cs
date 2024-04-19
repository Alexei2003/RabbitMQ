using RabbitMQ;

internal class Send
{
    //Send
    private static void Main(string[] args)
    {
        var rabbitMQPublisher0 = new RabbitMQPublisher("192.168.148.103", 5672, "Web", "Web", "DataLine0");
        var rabbitMQPublisher1 = new RabbitMQPublisher("192.168.148.103", 5672, "Web", "Web", "DataLine1");
        var rabbitMQPublisherDef = new RabbitMQPublisher("192.168.148.103", 5672, "Web", "Web", "DataLineDef");

        string message;

        while (true)
        {
            message = Console.ReadLine();
            
            if(message != "")
            {
                switch (message.First())
                {
                    case '0':
                        rabbitMQPublisher0.SendMessage(message);
                        break;
                    case '1':
                        rabbitMQPublisher1.SendMessage(message);
                        break;
                    default:
                        rabbitMQPublisherDef.SendMessage(message);
                        break;
                }
            }
            else
            {
                rabbitMQPublisherDef.SendMessage(message);
            }

        }
    }
}