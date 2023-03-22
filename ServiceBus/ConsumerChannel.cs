﻿using RabbitMQ.Client;

namespace ServiceBus
{
    public class ConsumerChannel
    {
        public string EventName { get; set; }
        public int ChannelNumber { get; set; }
        public IModel Model { get; set; }

        public ConsumerChannel(string eventName, int channelNumber, IModel model)
        {
            EventName = eventName;
            ChannelNumber = channelNumber;
            Model = model;
        }
    }
}
