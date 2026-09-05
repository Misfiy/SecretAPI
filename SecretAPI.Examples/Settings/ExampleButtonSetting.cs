namespace SecretAPI.Examples.Settings;

using LabApi.Features.Wrappers;
using SecretAPI.Features.Effects;
using SecretAPI.Features.UserSettings;

/// <summary>
/// Example version of <see cref="CustomButtonSetting"/>.
/// </summary>
public class ExampleButtonSetting : CustomButtonSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExampleButtonSetting"/> class.
    /// </summary>
    public ExampleButtonSetting()
        : base(typeof(ExampleButtonSetting).FullName?.GetHashCode(), "Example Button", "Press!!")
    {
    }

    /// <inheritdoc />
    public override CustomHeader Header => CustomHeader.Examples;

    /// <inheritdoc/>
    protected override bool CanView(Player player) => player.RemoteAdminAccess;

    /// <inheritdoc />
    protected override CustomSetting CreateDuplicate() => new ExampleButtonSetting();

    /// <inheritdoc />
    protected override void HandleSettingUpdate()
    {
        if (KnownOwner == null)
            return;

        KnownOwner.EnableEffect<Depleted>(duration: 30);
        KnownOwner.EnableEffect<BlastResistance>(200, 30);
    }
}