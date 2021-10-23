using System;

namespace AlinSpace.Database
{
    /// <summary>
    /// Represents the factory.
    /// </summary>
    /// <typeparam name="TContext">Type of context.</typeparam>
    public class Factory<TContext> where TContext : class
    {
        private readonly Action<TContext, RepositoryRegistryBuilder> registryConfigurator;
        private readonly Func<TContext, RepositoryRegistry, ITransaction> transactionProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="registryConfigurator"></param>
        /// <param name="transactionProvider"></param>
        public Factory(
            Action<TContext, RepositoryRegistryBuilder> registryConfigurator,
            Func<TContext, RepositoryRegistry, ITransaction> transactionProvider)
        {
            this.registryConfigurator = registryConfigurator ?? throw new ArgumentNullException(nameof(registryConfigurator));
            this.transactionProvider = transactionProvider ?? throw new ArgumentNullException(nameof(transactionProvider));
        }
        
        /// <summary>
        /// Create transaction from the given context.
        /// </summary>
        /// <param name="context">Transaction context.</param>
        /// <returns>Transaction.</returns>
        public ITransaction CreateTransaction(TContext context)
        {
            var repositoryBuilder = new RepositoryRegistryBuilder();
            registryConfigurator(context, repositoryBuilder);

            var repository = repositoryBuilder.Build();
            return transactionProvider(context, repository);
        }
    }
}
