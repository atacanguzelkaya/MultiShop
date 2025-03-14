﻿using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MultiShop.RabbitMQMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("Queue", false, false, false, arguments: null);
            var messageContent = "This is a RabbitMQ queue message.";
            var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);
            channel.BasicPublish(exchange: "", routingKey: "Queue", basicProperties: null, body: byteMessageContent);
            return Ok("Your message is queued.");
        }

        private static string message;

        [HttpGet]
        public IActionResult ReadMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, x) =>
            {
                var byteMessage = x.Body.ToArray();
                var message = Encoding.UTF8.GetString(byteMessage);

            };
            channel.BasicConsume(queue: "Queue", autoAck: false, consumer: consumer);
            if (string.IsNullOrEmpty(message))
            {
                return NoContent();
            }
            else
            {
                return Ok(message);
            }
        }
    }
}