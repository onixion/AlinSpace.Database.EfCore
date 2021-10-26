using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the transaction factory builder.
    /// </summary>
    /// <typeparam name="TContext">Type of context.</typeparam>
    public class TransactionFactoryBuilder<TContext> where TContext : class
    {
        private Action<TContext, RepositoryRegistryBuilder> registryConfigurator;
        private Func<TContext, RepositoryRegistry, ITransaction> transactionProvider;

        /// <summary>
        /// Shortcut constructor.
        /// </summary>
        /// <returns>Factory builder.</returns>
        public static TransactionFactoryBuilder<TContext> New()
        {
            return new TransactionFactoryBuilder<TContext>();
        }

        /// <summary>
        /// With registry.
        /// </summary>
        /// <param name="registryConfigurator">Registry configurator.</param>
        public TransactionFactoryBuilder<TContext> WithRegistry(Action<TContext, RepositoryRegistryBuilder> registryConfigurator)
        {
            this.registryConfigurator = registryConfigurator;
            return this;
        }

        /// <summary>
        /// With transaction provider.
        /// </summary>
        /// <param name="transactionProvider">Transaction provider.</param>
        public TransactionFactoryBuilder<TContext> WithTransaction(Func<TContext, RepositoryRegistry, ITransaction> transactionProvider)
        {
            this.transactionProvider = transactionProvider;
            return this;
        }

        /// <summary>
        /// Builds factory.
        /// </summary>
        /// <returns>Factory.</returns>
        public TransactionFactory<TContext> Build()
        {
            return new TransactionFactory<TContext>(registryConfigurator, transactionProvider);
        }
    }
}
