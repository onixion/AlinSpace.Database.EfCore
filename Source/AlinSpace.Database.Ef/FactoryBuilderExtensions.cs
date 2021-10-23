using Microsoft.EntityFrameworkCore;
using System;

namespace AlinSpace.Database.Ef
{
    /// <summary>
    /// Extensions for <see cref="FactoryBuilder{TContext}"/>.
    /// </summary>
    public static class FactoryBuilderExtensions
    {
        /// <summary>
        /// Configures the factory with an EntityFramework transaction.
        /// </summary>
        /// <typeparam name="TContext">Type of database context.</typeparam>
        /// <param name="builder">Builder.</param>
        /// <param name="registryConfigurator">Registry configurator.</param>
        public static FactoryBuilder<TContext> WithEfRegistry<TContext>(
            this FactoryBuilder<TContext> builder,
            Action<TContext, RepositoryRegistryBuilder> registryConfigurator) where TContext : DbContext
        {
            builder.WithRegistry((c,b) =>
            {
                // TODO
            });
            return builder;
        }

        /// <summary>
        /// Configures the factory with an EntityFramework transaction.
        /// </summary>
        /// <typeparam name="TContext">Type of database context.</typeparam>
        /// <param name="builder">Builder.</param>
        public static FactoryBuilder<TContext> WithEfTransaction<TContext>(
            this FactoryBuilder<TContext> builder) where TContext : DbContext
        {
            builder.WithTransaction((c, r) => new Transaction(c, r));
            return builder;
        }
    }
}
