using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


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
        channel.BasicQos(0, 1, false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            Task.Delay(TimeSpan.FromSeconds(0.5)).Wait();
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };

        string consumerTag = channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        Console.ReadLine();

        channel.BasicCancel(consumerTag);


    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }


}





#region

//ConnectionFactory factory = new();

//factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
//factory.ClientProvidedName = "rabbit reseiver1 app";

//IConnection cnn = factory.CreateConnection();

//IModel channel = cnn.CreateModel();

//string exchangeName = "DemoExchange";
//string routingKey = "demo-routing-kay";
//string queueName = "DemoQueue";

//channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
//channel.QueueDeclare(queueName, false, false, false, null);
//channel.QueueBind(queueName, exchangeName, routingKey, null);
//channel.BasicQos(0, 1, false);

//var consumer = new EventingBasicConsumer(channel);
//consumer.Received += (sender, args) =>
//{
//    Task.Delay(TimeSpan.FromSeconds(5)).Wait();

//    var body = args.Body.ToArray();

//    string message = Encoding.UTF8.GetString(body);
//    Console.WriteLine(message);

//    channel.BasicAck(args.DeliveryTag, false);

//};


//string consumerTag = channel.BasicConsume(queueName, false, consumer);

//Console.ReadLine();

//channel.BasicCancel(consumerTag);

//channel.Close();
//cnn.Close();

#endregion