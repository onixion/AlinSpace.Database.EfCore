﻿using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the factory builder.
    /// </summary>
    /// <typeparam name="TContext">Type of context.</typeparam>
    public class FactoryBuilder<TContext> where TContext : class
    {
        private Action<TContext, RepositoryRegistryBuilder> registryConfigurator;
        private Func<TContext, RepositoryRegistry, ITransaction> transactionProvider;

        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>Factory builder.</returns>
        public static FactoryBuilder<TContext> New()
        {
            return new FactoryBuilder<TContext>();
        }

        /// <summary>
        /// With registry.
        /// </summary>
        /// <param name="registryConfigurator">Registry configurator.</param>
        public FactoryBuilder<TContext> WithRegistry(Action<TContext, RepositoryRegistryBuilder> registryConfigurator)
        {
            this.registryConfigurator = registryConfigurator;
            return this;
        }

        /// <summary>
        /// With transaction provider.
        /// </summary>
        /// <param name="transactionProvider">Transaction provider.</param>
        public FactoryBuilder<TContext> WithTransaction(Func<TContext, RepositoryRegistry, ITransaction> transactionProvider)
        {
            this.transactionProvider = transactionProvider;
            return this;
        }

        /// <summary>
        /// Builds factory.
        /// </summary>
        /// <returns>Factory.</returns>
        public Factory<TContext> Build()
        {
            return new Factory<TContext>(registryConfigurator, transactionProvider);
        }
    }
}
