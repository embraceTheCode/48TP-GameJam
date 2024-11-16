using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class HasInteractionPending : ConditionTask {
		
		public BBParameter<Vector3> interactionPosition;
		public BBParameter<List<string>> animationName;

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {

			if (InteractableTracker.Instance.HasInteractions)
			{
				InteractionData interactionData = InteractableTracker.Instance.GetCurrentlyInteracting();
				interactionPosition.value = interactionData.InteractionPosition.position;
				animationName.value = interactionData.InteractionAnimation;
				return true;
			}
			return false;
		}
	}
}