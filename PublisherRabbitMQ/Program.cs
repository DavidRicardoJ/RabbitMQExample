using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};
using (var connection = factory.CreateConnection())

using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "fila_1"
        , false,
        false,
        false,
        null);

    string message = "Bem vindo ao RabbitMQ";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("",
        routingKey: "fila_1",
         null,
         body);
    Console.WriteLine($"[X] Enviada: {message}");

}

