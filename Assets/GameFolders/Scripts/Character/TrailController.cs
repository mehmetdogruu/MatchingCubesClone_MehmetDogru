using Sirenix.OdinInspector;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private TrailRenderer _blueTrail;
    [SerializeField] private TrailRenderer _orangeTrail;
    [SerializeField] private TrailRenderer _yellowTrail;

	[ShowInInspector, ReadOnly, BoxGroup("Trails", false)] public TrailRenderer Trail { get; private set; }

	public void SetActiveTrail(bool isActive)
	{
		Trail.emitting = isActive;
	}

	public void ChangeTrailColor(CollectableColor color)
	{
		_blueTrail.emitting = false;
		_orangeTrail.emitting = false;
		_yellowTrail.emitting = false;

		Trail = color switch
		{
			CollectableColor.BLUE => _blueTrail,
			CollectableColor.ORANGE => _orangeTrail,
			CollectableColor.YELLOW => _yellowTrail,
			_ => Trail
		};

		Trail.emitting = true;
	}
}
