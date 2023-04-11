using VTTiny.Components.Animator.Data;

namespace VTTiny.Components.Animator;

public interface IPackageAble
{
    /// <summary>
    /// Packages the state of this animator character for serialization.
    /// </summary>
    /// <returns>The resulting config.</returns>
    public AnimatorCharacterConfig PackageIntoConfig();
}