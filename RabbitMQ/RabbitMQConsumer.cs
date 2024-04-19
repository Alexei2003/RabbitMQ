namespace RabbitMQ // Пространство имен для класса RabbitMQConsumer
{
    using RabbitMQ.Client; // Подключение к библиотеке RabbitMQ.Client для работы с RabbitMQ
    using System.Text; // Подключение к пространству имен System.Text для работы с текстом

    public class RabbitMQConsumer // Объявление класса RabbitMQConsumer
    {
        private readonly ConnectionFactory _factory; // Фабрика соединений для создания подключения к RabbitMQ
        private IConnection _connection; // Подключение к RabbitMQ
        private IModel _channel; // Канал для обмена сообщениями с RabbitMQ
        private readonly string _queueName; // Название очереди

        // Конструктор класса, принимающий параметры для подключения к RabbitMQ и название очереди
        public RabbitMQConsumer(string hostName, int port, string userName, string password, string queueName)
        {
            // Создание фабрики соединений с указанием параметров подключения
            _factory = new ConnectionFactory()
            {
                HostName = hostName,
                Port = port,
                UserName = userName,
                Password = password,
            };

            // Создание подключения к RabbitMQ с использованием фабрики
            _connection = _factory.CreateConnection();

            // Создание канала для обмена сообщениями с RabbitMQ
            _channel = _connection.CreateModel();

            // Сохранение названия очереди
            _queueName = queueName;

            // Объявление очереди с указанием ее параметров
            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        // Метод для получения сообщения из очереди RabbitMQ
        public string GetMessage()
        {
            // Получение сообщения из очереди
            BasicGetResult result = _channel.BasicGet(queue: _queueName, autoAck: true);

            // Если сообщение получено успешно, возвращаем его в виде строки
            if (result != null)
            {
                return Encoding.UTF8.GetString(result.Body.ToArray());
            }

            // Если сообщение не получено, возвращаем null
            return null;
        }

        // Метод для закрытия соединения с RabbitMQ
        public void CloseConnection()
        {
            // Закрытие канала и соединения
            _channel.Close();
            _connection.Close();
        }
    }

}
