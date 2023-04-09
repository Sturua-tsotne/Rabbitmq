using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Connection settings
var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672"),
    ClientProvidedName = "rabbit sender app"
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    try
    {
        string exchangeName = "TEST-DemoExchange";
        string routingKey = "TEST-demo-routing-kay";
        string queueName = "Test";

        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, exchangeName, routingKey, null);

        // Declare the queue
        channel.QueueDeclare(queue: "Test", durable: false, exclusive: false, autoDelete: false, arguments: null);

        string message = " test  message ";

        for (int i = 0; i < 1000; i++)
        {
            var body = Encoding.UTF8.GetBytes($"{message} , ID = {i}");

            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);

            Console.WriteLine("[x] Sent {0} , ID = {1}", message, i);
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
        }

        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }


}

#region 

//ConnectionFactory factory = new()
//{
//    Uri = new Uri("amqp://guest:guest@localhost:5672"),
//    ClientProvidedName = "rabbit sender app"
//};

//IConnection cnn = factory.CreateConnection();

//IModel channel = cnn.CreateModel();

//string exchangeName = "DemoExchange";
//string routingKey = "demo-routing-kay";
//string queueName = "DemoQueue";

//channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
//channel.QueueDeclare(queueName, false, false, false, null);
//channel.QueueBind(queueName, exchangeName, routingKey, null);

//for (int i = 0; i < 100; i++)
//{
//    Console.WriteLine($"Sending Message {i}");

//    byte[] messageBodybytes = Encoding.UTF8.GetBytes($"Message #{i}");
//    channel.BasicPublish(exchangeName, routingKey, null, messageBodybytes);
//    Thread.Sleep(1000);
//}


//string routingKey1 = "demo-routing-kay1";
//string queueName1 = "DemoQueue1";

//channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
//channel.QueueDeclare(queueName1, false, false, false, null);
//channel.QueueBind(queueName1, exchangeName, routingKey1, null);

//for (int i = 0; i < 100; i++)
//{
//    Console.WriteLine($"Sending Message {i}");

//    byte[] messageBodybytes = Encoding.UTF8.GetBytes($"Message #{i}  test 1");
//    channel.BasicPublish(exchangeName, routingKey1, null, messageBodybytes);
//    Thread.Sleep(1000);
//}



//channel.Close();
//cnn.Close();

#endregion


