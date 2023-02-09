// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/Area.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace MasterData.Area.Protos {
  /// <summary>
  /// Service Pool Area
  /// </summary>
  public static partial class AreaGrpcService
  {
    static readonly string __ServiceName = "AreaGrpcService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MasterData.Area.Protos.AreaModel> __Marshaller_AreaModel = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MasterData.Area.Protos.AreaModel.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MasterData.Area.Protos.AreaEmpty> __Marshaller_AreaEmpty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MasterData.Area.Protos.AreaEmpty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MasterData.Area.Protos.reqAreaById> __Marshaller_reqAreaById = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MasterData.Area.Protos.reqAreaById.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MasterData.Area.Protos.resAreaAll> __Marshaller_resAreaAll = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MasterData.Area.Protos.resAreaAll.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MasterData.Area.Protos.AreaModel, global::MasterData.Area.Protos.AreaEmpty> __Method_AddArea = new grpc::Method<global::MasterData.Area.Protos.AreaModel, global::MasterData.Area.Protos.AreaEmpty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddArea",
        __Marshaller_AreaModel,
        __Marshaller_AreaEmpty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MasterData.Area.Protos.reqAreaById, global::MasterData.Area.Protos.AreaModel> __Method_GetAreaById = new grpc::Method<global::MasterData.Area.Protos.reqAreaById, global::MasterData.Area.Protos.AreaModel>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAreaById",
        __Marshaller_reqAreaById,
        __Marshaller_AreaModel);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MasterData.Area.Protos.AreaEmpty, global::MasterData.Area.Protos.resAreaAll> __Method_GetAllArea = new grpc::Method<global::MasterData.Area.Protos.AreaEmpty, global::MasterData.Area.Protos.resAreaAll>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllArea",
        __Marshaller_AreaEmpty,
        __Marshaller_resAreaAll);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::MasterData.Area.Protos.AreaReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of AreaGrpcService</summary>
    [grpc::BindServiceMethod(typeof(AreaGrpcService), "BindService")]
    public abstract partial class AreaGrpcServiceBase
    {
      /// <summary>
      /// Create new pool area.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MasterData.Area.Protos.AreaEmpty> AddArea(global::MasterData.Area.Protos.AreaModel request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MasterData.Area.Protos.AreaModel> GetAreaById(global::MasterData.Area.Protos.reqAreaById request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MasterData.Area.Protos.resAreaAll> GetAllArea(global::MasterData.Area.Protos.AreaEmpty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for AreaGrpcService</summary>
    public partial class AreaGrpcServiceClient : grpc::ClientBase<AreaGrpcServiceClient>
    {
      /// <summary>Creates a new client for AreaGrpcService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public AreaGrpcServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for AreaGrpcService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public AreaGrpcServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected AreaGrpcServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected AreaGrpcServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Create new pool area.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::MasterData.Area.Protos.AreaEmpty AddArea(global::MasterData.Area.Protos.AreaModel request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddArea(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Create new pool area.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::MasterData.Area.Protos.AreaEmpty AddArea(global::MasterData.Area.Protos.AreaModel request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddArea, null, options, request);
      }
      /// <summary>
      /// Create new pool area.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::MasterData.Area.Protos.AreaEmpty> AddAreaAsync(global::MasterData.Area.Protos.AreaModel request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddAreaAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Create new pool area.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::MasterData.Area.Protos.AreaEmpty> AddAreaAsync(global::MasterData.Area.Protos.AreaModel request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddArea, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::MasterData.Area.Protos.AreaModel GetAreaById(global::MasterData.Area.Protos.reqAreaById request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAreaById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::MasterData.Area.Protos.AreaModel GetAreaById(global::MasterData.Area.Protos.reqAreaById request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAreaById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::MasterData.Area.Protos.AreaModel> GetAreaByIdAsync(global::MasterData.Area.Protos.reqAreaById request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAreaByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::MasterData.Area.Protos.AreaModel> GetAreaByIdAsync(global::MasterData.Area.Protos.reqAreaById request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAreaById, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::MasterData.Area.Protos.resAreaAll GetAllArea(global::MasterData.Area.Protos.AreaEmpty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllArea(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::MasterData.Area.Protos.resAreaAll GetAllArea(global::MasterData.Area.Protos.AreaEmpty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllArea, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::MasterData.Area.Protos.resAreaAll> GetAllAreaAsync(global::MasterData.Area.Protos.AreaEmpty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllAreaAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::MasterData.Area.Protos.resAreaAll> GetAllAreaAsync(global::MasterData.Area.Protos.AreaEmpty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllArea, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override AreaGrpcServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new AreaGrpcServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(AreaGrpcServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_AddArea, serviceImpl.AddArea)
          .AddMethod(__Method_GetAreaById, serviceImpl.GetAreaById)
          .AddMethod(__Method_GetAllArea, serviceImpl.GetAllArea).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, AreaGrpcServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_AddArea, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MasterData.Area.Protos.AreaModel, global::MasterData.Area.Protos.AreaEmpty>(serviceImpl.AddArea));
      serviceBinder.AddMethod(__Method_GetAreaById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MasterData.Area.Protos.reqAreaById, global::MasterData.Area.Protos.AreaModel>(serviceImpl.GetAreaById));
      serviceBinder.AddMethod(__Method_GetAllArea, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MasterData.Area.Protos.AreaEmpty, global::MasterData.Area.Protos.resAreaAll>(serviceImpl.GetAllArea));
    }

  }
}
#endregion
