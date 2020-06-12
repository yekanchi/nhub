// Type: RiderFirstTestProject.Program 
// Assembly: RiderFirstTestProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5B88C5B5-638D-44A6-A56B-D28AFD1824F7
// Location: C:\Users\Morteza Talebi\RiderProjects\RiderFirstTestProject\bin\Debug\netcoreapp3.1\RiderFirstTestProject.dll
// Sequence point data from C:\Users\Morteza Talebi\RiderProjects\RiderFirstTestProject\bin\Debug\netcoreapp3.1\RiderFirstTestProject.pdb

.class public auto ansi beforefieldinit
  RiderFirstTestProject.Program
    extends [System.Runtime]System.Object
{

  .class nested private sealed auto ansi serializable beforefieldinit
    '<>c'
      extends [System.Runtime]System.Object
  {
    .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor()
      = (01 00 00 00 )

    .field public static initonly class RiderFirstTestProject.Program/'<>c' '<>9'

    .field public static class [System.Runtime]System.Action`1<class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder> '<>9__1_0'

    .method private hidebysig static specialname rtspecialname void
      .cctor() cil managed
    {
      .maxstack 8

      IL_0000: newobj       instance void RiderFirstTestProject.Program/'<>c'::.ctor()
      IL_0005: stsfld       class RiderFirstTestProject.Program/'<>c' RiderFirstTestProject.Program/'<>c'::'<>9'
      IL_000a: ret

    } // end of method '<>c'::.cctor

    .method public hidebysig specialname rtspecialname instance void
      .ctor() cil managed
    {
      .maxstack 8

      IL_0000: ldarg.0      // this
      IL_0001: call         instance void [System.Runtime]System.Object::.ctor()
      IL_0006: nop
      IL_0007: ret

    } // end of method '<>c'::.ctor

    .method assembly hidebysig instance void
      '<CreateHostBuilder>b__1_0'(
        class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder webBuilder
      ) cil managed
    {
      .maxstack 8

      // [21 57 - 21 58]
      IL_0000: nop

      // [21 59 - 21 92]
      IL_0001: ldarg.1      // webBuilder
      IL_0002: call         class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder [Microsoft.AspNetCore.Hosting]Microsoft.AspNetCore.Hosting.WebHostBuilderExtensions::UseStartup<class RiderFirstTestProject.Startup>(class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder)
      IL_0007: pop

      // [21 93 - 21 94]
      IL_0008: ret

    } // end of method '<>c'::'<CreateHostBuilder>b__1_0'
  } // end of class '<>c'

  .method public hidebysig static void
    Main(
      string[] args
    ) cil managed
  {
    .entrypoint
    .maxstack 8

    // [15 9 - 15 10]
    IL_0000: nop

    // [16 13 - 16 51]
    IL_0001: ldarg.0      // args
    IL_0002: call         class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHostBuilder RiderFirstTestProject.Program::CreateHostBuilder(string[])
    IL_0007: callvirt     instance class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHost [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHostBuilder::Build()
    IL_000c: call         void [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions::Run(class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHost)
    IL_0011: nop

    // [17 9 - 17 10]
    IL_0012: ret

  } // end of method Program::Main

  .method public hidebysig static class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHostBuilder
    CreateHostBuilder(
      string[] args
    ) cil managed
  {
    .maxstack 8

    // [20 13 - 21 95]
    IL_0000: ldarg.0      // args
    IL_0001: call         class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHostBuilder [Microsoft.Extensions.Hosting]Microsoft.Extensions.Hosting.Host::CreateDefaultBuilder(string[])
    IL_0006: ldsfld       class [System.Runtime]System.Action`1<class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder> RiderFirstTestProject.Program/'<>c'::'<>9__1_0'
    IL_000b: dup
    IL_000c: brtrue.s     IL_0025
    IL_000e: pop
    IL_000f: ldsfld       class RiderFirstTestProject.Program/'<>c' RiderFirstTestProject.Program/'<>c'::'<>9'
    IL_0014: ldftn        instance void RiderFirstTestProject.Program/'<>c'::'<CreateHostBuilder>b__1_0'(class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder)
    IL_001a: newobj       instance void class [System.Runtime]System.Action`1<class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder>::.ctor(object, native int)
    IL_001f: dup
    IL_0020: stsfld       class [System.Runtime]System.Action`1<class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder> RiderFirstTestProject.Program/'<>c'::'<>9__1_0'
    IL_0025: call         class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHostBuilder [Microsoft.AspNetCore]Microsoft.Extensions.Hosting.GenericHostBuilderExtensions::ConfigureWebHostDefaults(class [Microsoft.Extensions.Hosting.Abstractions]Microsoft.Extensions.Hosting.IHostBuilder, class [System.Runtime]System.Action`1<class [Microsoft.AspNetCore.Hosting.Abstractions]Microsoft.AspNetCore.Hosting.IWebHostBuilder>)
    IL_002a: ret

  } // end of method Program::CreateHostBuilder

  .method public hidebysig specialname rtspecialname instance void
    .ctor() cil managed
  {
    .maxstack 8

    IL_0000: ldarg.0      // this
    IL_0001: call         instance void [System.Runtime]System.Object::.ctor()
    IL_0006: nop
    IL_0007: ret

  } // end of method Program::.ctor
} // end of class RiderFirstTestProject.Program
