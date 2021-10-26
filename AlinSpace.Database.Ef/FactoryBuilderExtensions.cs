using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Extensions for <see cref="TransactionFactoryBuilder{TContext}"/>.
    /// </summary>
    public static class FactoryBuilderExtensions
    {
        /// <summary>
        /// Configures the factory with an EntityFramework transaction.
        /// </summary>
        /// <typeparam name="TContext">Type of database context.</typeparam>
        /// <param name="builder">Builder.</param>
        /// <param name="registryConfigurator">Registry configurator.</param>
        //public static TransactionFactoryBuilder<TContext> WithEfRegistry<TContext>(
        //    this TransactionFactoryBuilder<TContext> builder,
        //    Action<TContext, RepositoryRegistryBuilder> registryConfigurator) where TContext : DbContext
        //{
        //    builder.WithRegistry((c,b) =>
        //    {
        //        // TODO
        //    });
        //    return builder;
        //}

        /// <summary>
        /// Configures the factory with an EntityFramework transaction.
        /// </summary>
        /// <typeparam name="TContext">Type of database context.</typeparam>
        /// <param name="builder">Builder.</param>
        public static TransactionFactoryBuilder<TContext> WithEfTransaction<TContext>(
            this TransactionFactoryBuilder<TContext> builder) where TContext : DbContext
        {
            builder.WithTransaction((c, r) => new Transaction(c, r));
            return builder;
        }
    }
}
