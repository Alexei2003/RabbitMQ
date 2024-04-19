using RabbitMQ.Client; // Подключение к библиотеке RabbitMQ.Client для работы с RabbitMQ
using System.Text;

namespace RabbitMQ
{
    public class RabbitMQPublisher
    {
        private readonly ConnectionFactory _factory; // Фабрика соединений для создания подключения к RabbitMQ
        private IConnection _connection; // Подключение к RabbitMQ
        private IModel _channel; // Канал для обмена сообщениями с RabbitMQ
        private readonly string _queueName; // Название очереди

        // Конструктор класса, принимающий параметры для подключения к RabbitMQ и название очереди
        public RabbitMQPublisher(string hostName, int port, string userName, string password, string queueName)
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

        // Метод для отправки сообщения в очередь RabbitMQ
        public void SendMessage(string message)
        {

            // Кодирование сообщения в массив байтов
            var body = Encoding.UTF8.GetBytes(message);

            // Отправка сообщения в очередь с указанием названия очереди и тела сообщения
            _channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);

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
