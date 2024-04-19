using RabbitMQ;

internal class Get
{
    //Get
    private static void Main(string[] args)
    {
        var rabbitMQConsumer0 = new RabbitMQConsumer("192.168.148.103", 5672, "Web", "Web", "DataLine0");
        var rabbitMQConsumer1 = new RabbitMQConsumer("192.168.148.103", 5672, "Web", "Web", "DataLine1");
        var rabbitMQConsumerDef = new RabbitMQConsumer("192.168.148.103", 5672, "Web", "Web", "DataLineDef");

        var messages = new string[3];

        while (true)
        {
            messages[0] = rabbitMQConsumer0.GetMessage();
            messages[1] = rabbitMQConsumer1.GetMessage();
            messages[2] = rabbitMQConsumerDef.GetMessage();
            foreach (var message in messages)
            {
                if (message != null)
                {
                    Console.WriteLine(message);
                }

            }

            Thread.Sleep(100);
        }
    }
}