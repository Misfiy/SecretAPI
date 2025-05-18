namespace SecretAPI.Extensions
{
    using System;
    using LabApi.Features.Wrappers;
    using Mirror;
    using Respawning;

    /// <summary>
    /// Extensions related to Mirror.
    /// </summary>
    public static class MirrorExtensions
    {
        /// <summary>
        /// Sends a fake cassie message to a player.
        /// </summary>
        /// <param name="target">The target to send the cassie message to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="isHeld">Whether the cassie is held.</param>
        /// <param name="isNoisy">Whether the cassie is noisy.</param>
        /// <param name="isSubtitles">Whether there is subtitles on the cassie.</param>
        /// <param name="customSubtitles">The custom subtitles to use for the cassie.</param>
        public static void SendFakeCassieMessage(
            this Player target,
            string message,
            bool isHeld = false,
            bool isNoisy = true,
            bool isSubtitles = true,
            string customSubtitles = "")
        {
            foreach (RespawnEffectsController allController in RespawnEffectsController.AllControllers)
            {
                SendFakeRpcMessage(target, allController, typeof(RespawnEffectsController), nameof(RespawnEffectsController.RpcCassieAnnouncement), message, isHeld, isNoisy, isSubtitles, customSubtitles);
            }
        }

        /// <summary>
        /// Send a fake rpc message to a player.
        /// </summary>
        /// <param name="target">The target to send the rpc to.</param>
        /// <param name="behaviour">The network behaviour containing the rpc.</param>
        /// <param name="type">The type containing the rpc.</param>
        /// <param name="rpcName">The name of the rpc to call.</param>
        /// <param name="values">The values to write to the writer.</param>
        public static void SendFakeRpcMessage(this Player target, NetworkBehaviour behaviour, Type type, string rpcName, params object[] values)
        {
            NetworkWriterPooled writer = NetworkWriterPool.Get();

            foreach (object obj in values)
                writer.Write(obj);

            RpcMessage rpcMessage = new()
            {
                netId = behaviour.netId,
                componentIndex = behaviour.ComponentIndex,
                functionHash = (ushort)ReflectionExtensions.GetLongFuncName(type, rpcName).GetStableHashCode(),
                payload = writer.ToArraySegment(),
            };

            target.Connection.Send(rpcMessage);
            NetworkWriterPool.Return(writer);
        }
    }
}