﻿using System;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;

namespace EMG.Lambda.LocalRunner.Internal
{
    public class InnerRunnerBuilder : IRunnerBuilder
    {
        public int Port { get; set; } = 5000;

        public Func<ILambdaSerializer> SerializerFactory { get; set; } = () => new JsonSerializer();

        public IRunnerBuilder UsePort(int port)
        {
            Port = port;
            return this;
        }

        public IRunnerBuilder UseSerializer<TSerializer>(Func<TSerializer> serializerFactory)
            where TSerializer : ILambdaSerializer
        {
            SerializerFactory = () => serializerFactory();
            return this;
        }

        public IReceivingRunnerBuilder<TInput> Receives<TInput>()
        {
            return new InnerReceivingRunnerBuilder<TInput>
            {
                Port = Port,
                SerializerFactory = SerializerFactory
            };
        }
    }
}