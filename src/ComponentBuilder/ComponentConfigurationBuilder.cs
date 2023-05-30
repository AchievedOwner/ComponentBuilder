﻿using ComponentBuilder.Interceptors;
using ComponentBuilder.Rendering;
using ComponentBuilder.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentBuilder;

/// <summary>
/// Represents a builder for configurations.
/// </summary>
public class ComponentConfigurationBuilder
{
    /// <summary>
    /// Initializes a new instance <see cref="ComponentConfigurationBuilder"/> class.
    /// </summary>
    /// <param name="options">The instance of <see cref="ComponentBuilderOptions"/> class.</param>
    /// <param name="services"></param>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> is <c>null</c>.</exception>
    internal ComponentConfigurationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> instance.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Add a component interceptor to configuration.
    /// </summary>
    /// <typeparam name="TInterceptor">The type of an interceptor.</typeparam>
    public ComponentConfigurationBuilder AddInterceptor<TInterceptor>() where TInterceptor : class, IComponentInterceptor
    {
        Services.AddTransient<IComponentInterceptor, TInterceptor>();
        return this;
    }


    /// <summary>
    /// Add a resolver of component to configuration.
    /// </summary>
    /// <typeparam name="TResolverService">The type of resolver service.</typeparam>
    /// <typeparam name="TResolverImplementation">The type of resolver implementation.</typeparam>
    public ComponentConfigurationBuilder AddResolver<TResolverService, TResolverImplementation>()
        where TResolverService : class, IComponentResolver
        where TResolverImplementation : class, TResolverService
    {
        Services.AddTransient<TResolverService, TResolverImplementation>();
        return this;
    }

    /// <summary>
    /// Add a component renderer to configuration.
    /// </summary>
    /// <typeparam name="TRenderer">The type of renderer.</typeparam>
    /// <returns></returns>
    public ComponentConfigurationBuilder AddRenderer<TRenderer>() where TRenderer : class, IComponentRender
    {
        Services.AddTransient<IComponentRender, TRenderer>();
        return this;
    }
}
