﻿using Microsoft.Extensions.DependencyInjection;

namespace ComponentBuilder.Test;
public abstract class AutoTestBase: TestBase
{
    private readonly ServiceProvider _builder;

    protected AutoTestBase():base()
    {
        var services = new ServiceCollection();

        services.AddComponentBuilder();
        _builder = services.BuildServiceProvider();

        TestContext.Services.AddComponentBuilder();
        //_builder = TestContext.Services.BuildServiceProvider();
    }

    protected T GetService<T>() => _builder.GetService<T>();
}
