using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ConsumerRabbitMQ
{
    internal class Program
    {
        static void Main(string[] args)
        {

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


                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[X] Recebida: {message}");
                };
                channel.BasicConsume("fila_1", true, consumer);
                Console.ReadLine();

            }
        }
    }
}