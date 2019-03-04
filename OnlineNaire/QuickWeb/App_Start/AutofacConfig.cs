using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Hangfire;
using Masuit.Tools.NoSQL;
using Quick.Common;
using Quick.Models.Application;
using QuickWeb.Models.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace QuickWeb
{
    /// <summary>
    /// Autofac配置类
    /// </summary>
    public class AutofacConfig
    {
        public static IContainer Container { get; set; }
        /// <summary>
        /// 负责调用autofac实现依赖注入，负责创建MVC控制器类的对象(调用控制器的有参构造函数)，接管DefaultControllerFactory的工作
        /// </summary>
        public static void Register()
        {
            // 第一步：注册控制器
            var builder = new ContainerBuilder();
            // 告诉autofac将来要创建的控制器类存放在哪个程序集
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
            // 把当前程序集中的 Controller 都注册                  // 自动给属性进行“注入”
            //builder.RegisterControllers(typeof(Global).Assembly).PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();

            // 第二步：注册接口: 告诉autofac注册所有的Bll，创建类的实例，以该类的接口实现实例存储

            builder.RegisterType<DataContext>().OnRelease(db => db.Dispose()).InstancePerLifetimeScope();

            // 把当前程序集中的类都注册
            builder.RegisterAssemblyTypes(Assemblies.RepositoryAssembly)
            // 注入类本身
            .AsSelf()
            // 剔除抽象类; type1.IsAssignableFrom(type2)：type2是否实现了type1的接口/或者是否集成自type1。这样就只注册实现了IBaseService接口的类，避免注册其它无关的类
            .Where(t => !t.IsAbstract)
            // 为接口注入具体类
            .AsImplementedInterfaces()
            // 自动给属性进行“注入”
            .PropertiesAutowired()
            // 同一个Lifetime生成的对象是同一个实例
            .InstancePerLifetimeScope();

            // 把当前程序集中的类都注册
            builder.RegisterAssemblyTypes(Assemblies.ServiceAssembly)
            // 剔除抽象类; type1.IsAssignableFrom(type2)：type2是否实现了type1的接口/或者是否集成自type1。这样就只注册实现了IBaseService接口的类，避免注册其它无关的类
            .Where(t => !t.IsAbstract)
            // 注入类本身
            .AsSelf()
            // 为接口注入具体类
            .AsImplementedInterfaces()
            // 自动给属性进行“注入”
            .PropertiesAutowired()
            // 同一个Lifetime生成的对象是同一个实例
            .InstancePerLifetimeScope();

            builder.RegisterType<RedisHelper>().OnRelease(db => db.Dispose()).InstancePerLifetimeScope();
            builder.RegisterType<BackgroundJobClient>().SingleInstance();//指定生命周期为单例
            builder.RegisterType<HangfireBackJob>().As<IHangfireBackJob>().PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerDependency();

            // 第三步：注册系统容器，所有对象都从这里获取。
            Container = builder.Build();

            //5.0 将当前容器交给MVC底层，保证容器不被销毁，控制器由autofac来创建
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            //注册系统级别的 DependencyResolver，这样当 MVC 框架创建 Controller 等对象的时候都是管 Autofac 要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}