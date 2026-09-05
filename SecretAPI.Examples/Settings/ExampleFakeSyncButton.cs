namespace SecretAPI.Examples.Settings;

using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using SecretAPI.Extensions;
using SecretAPI.Features.UserSettings;

/// <summary>
/// Example version for fake syncing on a <see cref="CustomButtonSetting"/>.
/// </summary>
public class ExampleFakeSyncButton : CustomButtonSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleFakeSyncButton"/> class.
    /// </summary>
    public ExampleFakeSyncButton()
        : base(typeof(ExampleFakeSyncButton).FullName?.GetHashCode(), "Example Fake Sync Button", "Fake!")
    {
    }

    /// <inheritdoc />
    public override CustomHeader Header => CustomHeader.Examples;

    /// <inheritdoc/>
    protected override bool CanView(Player player) => player.RemoteAdminAccess;

    /// <inheritdoc />
    protected override CustomSetting CreateDuplicate() => new ExampleFakeSyncButton();

    /// <inheritdoc />
    protected override void HandleSettingUpdate()
    {
        if (KnownOwner == null)
            return;

        TextToy textToy = TextToy.Create(KnownOwner.Position, KnownOwner.Rotation);
        textToy.TextFormat = "{0}";
        textToy.Arguments.Add("Default Text!");

        Timing.CallDelayed(5, () =>
        {
            MirrorExtensions.SyncListChange<string> change = new()
            {
                Index = 0,
                Item = "Fake synced text!",
                Operation = SyncList<string>.Operation.OP_SET,
            };

            KnownOwner.SendFakeSyncListData(textToy.Base, 1L, change);
        });
    }
}