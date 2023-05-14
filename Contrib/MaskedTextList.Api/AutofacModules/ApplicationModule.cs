using Autofac;
using RecAll.Infrastructure.EventBus.Abstractions;
using System.Reflection;
using RecAll.Contrib.MaskedTextList.Api.Controllers;
using Module = Autofac.Module;

namespace RecAll.Contrib.MaskedTextList.Api.AutofacModules;

public class ApplicationModule : Module {
    protected override void Load(ContainerBuilder builder) {
        builder.RegisterAssemblyTypes(typeof(ItemController).GetTypeInfo()
            .Assembly).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
    }
}